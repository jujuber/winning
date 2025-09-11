using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Winning.FrameWork.DAL.Kernel;
using Winning.FrameWork.Kernel.Enum;
using Winning.Outp.Core;
using Winning.Outp.DAL.PatInfo.DataObject;
using Winning.Outp.External.wdpatsign.Model;
using static System.Net.Mime.MediaTypeNames;

namespace Winning.Outp.External.wdpatsign
{
    internal class PatSignService
    {
        private static IAdoDb _ISql5;
        internal static IAdoDb ISql5
        {
            get
            {
                if (_ISql5 == null)
                {
                    _ISql5 = SqlHelper.GetAdoDb();
                    _ISql5.SqlConnect(SystemType.HT);
                }
                return _ISql5;
            }
        }
        private static readonly string measusreType1001 = @"平均收缩压|平均舒张压|血压测量侧|第一次收缩压|第一次舒张压|第二次收缩压|第二次舒张压|第三次收缩压|第三次舒张压|平均脉率|第一次脉率值|第二次脉率值|第三次脉率值|第一次不规则脉搏|第二次不规则脉搏|第三次不规则脉搏|第一次手臂移动|第二次手臂移动|第三次手臂移动|第一次测量时间|第二次测量时间|第三次测量时间|是否血压异常|是否危急值血压|是否已服降压药|是否已休息至少5分钟";
        private static readonly string measusreType1003 = @"身高值|体重值";

        private static readonly string measusreType1005 = @"腰围值|臀围值";

        private static readonly string measusreType2001 = @"血糖值|单位|参考范围|异常提示代码|血糖类型|测量途径|是否危急值血糖|是否患者";

        private static readonly string measusreType4001 = @"FVC|FEV1|FEV3|FEV6|FEV1%VCMax|FEV1%FVC|FEV1%FEV6|PEF|MMEF7525|MEF75|MEF50|MEF25|FEF25|FEF50|FEF75|FET|PIF|FIF50|FEF50/FIF50|FVC预计值|FEV1预计值|FEV3预计值|FEV6预计值|FEV1%VCMax预计值|FEV1%FVC预计值|FEV1%FEV6预计值|PEF预计值|MMEF75-25预计值|MEF75预计值|MEF50预计值|MEF25预计值|FEF25预计值|FEF50预计值|FEF75预计值|FET预计值|PIF预计值|FIF50预计值|FEF50/FIF50预计值|用药标志|出生日期|身高|体重";
        public static OutputContent GetData(string mtype, PatBasicInfo patient, DateTime dtbegin, DateTime dtend)
        {         
            OutputContent result = new OutputContent();
            try
            {
                IniFile inifile = new IniFile(GlobalVariable.HisApp.Path.ApplicationPath + @"\Config\UrlConfig.ini");
                string url = inifile.IniReadValue("WDPATSIGN", "patsignurl");

                if (string.IsNullOrWhiteSpace(url))
                    throw new Exception("请检查请求地址");
                if (patient == null || string.IsNullOrWhiteSpace(patient.Sfzh))
                    throw new Exception("患者信息或者患者身份证号码没有获取到！");

                string measureType = "1001|1003|1005|2001|4001";
                string[] measureTypes = measureType.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (measureTypes == null || measureTypes.Length == 0)
                    throw new Exception("请检查体征数据获取类型是否配置！");
                FileLog.WriteLog("接口调用开始");

                var resultData = new InputDTO();
                resultData.personcard = patient.Sfzh;
                resultData.measureType = mtype;
                resultData.startDate = dtbegin.ToString("yyyy-MM-dd 00:00:00");
                resultData.endDate = dtend.ToString("yyyy-MM-dd 23:59:59");
                resultData.pageIndex = "1";
                resultData.pageSize = "1000";

                //resultData.measures = new List<measure>(); //血压、体重、腰围、血糖、肺功能

                //foreach (var type in measureTypes)
                //{
                //    var item = new measure();
                //    item.measureType = type;
                //    item.pageIndex = 1;
                //    item.pageSize = 1000;
                //    resultData.measures.Add(item);
                //}
                string jsonStr = JsonConvert.SerializeObject(resultData);
                FileLog.WriteLog("入参原文：" + jsonStr);
                string DesStr = DESUtils.EncryptString(jsonStr);
                FileLog.WriteLog("加密入参：" + DesStr);
                string token = DateTimeOffset.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff+08:00");
                FileLog.WriteLog("入参token原文：" + token);
                token = DESUtils.EncryptString(token);
                FileLog.WriteLog("加密access-token入参：" + token);
                string responseResult = Post(url + $"?param={DesStr}", "", token);
                FileLog.WriteLog("出参：" + responseResult);
                if (!string.IsNullOrEmpty(responseResult))
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<OutputContent>(responseResult);
                        if (result != null && result.total =="0")
                        {
                            GlobalVariable.HisApp.Prompt.Show("没有查询到数据", MessageBoxButtons.OK);
                            return result;
                        }
                        SaveData(result, patient);
                    }
                    catch (Exception ex)
                    {
                        GlobalVariable.HisApp.Prompt.Show("接口返回数据格式错误，无法转换" + ex.Message, MessageBoxButtons.OK); 
                        FileLog.WriteLog("接口返回数据格式错误，无法转换" + ex.Message);
                    }
                }
                FileLog.WriteLog("接口调用结束");
            }
            catch (Exception ex)
            {
                GlobalVariable.HisApp.Prompt.Show("接口调用结束" + ex.Message, MessageBoxButtons.OK);
                FileLog.WriteLog("接口调用结束" + ex.Message + ex.StackTrace);
            }
            return result;
        }

        private static void SaveData(OutputContent data,PatBasicInfo pat)
        {
            foreach (var item in data.dataList)
            {
                string sql = $@"exec usp_mz_save_grtzdata @ghxh={pat.Ghxh},@patid={pat.Patid},@personcard='{item.personcard}',
                                @name='{item.name}',@gender='{item.gender}',@birth='{item.birth}',@measureType='{item.measureType}',
                                @measureTime='{item.measureTime}',@measureSourceId='{item.measureSourceId}',@measureLocation='{item.measureLocation}',
                                @measureOrgId='{item.measureOrgId}',@measureMode='{item.measureMode}',@deviceId='{item.deviceId}',@deviceType='{item.deviceType}',
                                @measureDoc='{item.measureDoc}',@networkStatus='{item.networkStatus}',@measureData='{item.measureData}'";
                ISql5.GetDataTable(sql);
            } 
        }
        private static string Post(string url, string content, string token)
        {
            string resultOfPost = "";
            try
            {
                FileLog.WriteLog("请求地址：" + url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json;charset=utf-8";
                request.UserAgent = "imgfornote";
                request.Accept = "application/json";
                request.Timeout = 5000;
                request.Headers.Add("access-token", token);
                #region 添加Post 参数
                byte[] data = Encoding.UTF8.GetBytes(content);
                request.ContentLength = data.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion

                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    resultOfPost = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                FileLog.WriteLog(ex.Message + ex.StackTrace);
                resultOfPost = "";
            }
            return resultOfPost;
        }
    }
}
