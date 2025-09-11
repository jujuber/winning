using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Winning.Common.Eop;

namespace Winning.EmrOutp.Interface.PhysiologicalData
{
    public class PhysiologicalDataService
    {
        private static readonly string measusreType1001 = @"平均收缩压|平均舒张压|血压测量侧|第一次收缩压|第一次舒张压|第二次收缩压|第二次舒张压|第三次收缩压|第三次舒张压|平均脉率|第一次脉率值|第二次脉率值|第三次脉率值|第一次不规则脉搏|第二次不规则脉搏|第三次不规则脉搏|第一次手臂移动|第二次手臂移动|第三次手臂移动|第一次测量时间|第二次测量时间|第三次测量时间|是否血压异常|是否危急值血压|是否已服降压药|是否已休息至少5分钟";
        private static readonly string measusreType1003 = @"身高值|体重值";

        private static readonly string measusreType1005 = @"腰围值|臀围值";

        private static readonly string measusreType2001 = @"血糖值|单位|参考范围|异常提示代码|血糖类型|测量途径|是否危急值血糖|是否患者";

        private static readonly string measusreType4001 = @"FVC|FEV1|FEV3|FEV6|FEV1%VCMax|FEV1%FVC|FEV1%FEV6|PEF|MMEF7525|MEF75|MEF50|MEF25|FEF25|FEF50|FEF75|FET|PIF|FIF50|FEF50/FIF50|FVC预计值|FEV1预计值|FEV3预计值|FEV6预计值|FEV1%VCMax预计值|FEV1%FVC预计值|FEV1%FEV6预计值|PEF预计值|MMEF75-25预计值|MEF75预计值|MEF50预计值|MEF25预计值|FEF25预计值|FEF50预计值|FEF75预计值|FET预计值|PIF预计值|FIF50预计值|FEF50/FIF50预计值|用药标志|出生日期|身高|体重";
        public static string GetData(string url, Inpatient ipatient)
        {
            string resultStr = string.Empty;
            try
            {


                if (string.IsNullOrWhiteSpace(url))
                    throw new Exception("请检查请求地址");
                if (ipatient == null || string.IsNullOrWhiteSpace(ipatient?.PersonalInformation?.IdentificationNo?.Trim()))
                    throw new Exception("患者信息或者患者身份证号码没有获取到！");
                string measureType = "1001|1003|1005|2001|4001";
                string[] measureTypes = measureType.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (measureTypes == null || measureTypes.Length == 0)
                    throw new Exception("请检查体征数据获取类型是否配置！");
                FileLog.WriteLog("接口调用开始");

                var resultData = new RequestDto();
                resultData.personcard = ipatient.PersonalInformation.IdentificationNo?.Trim();
                resultData.startDate = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                resultData.endDate = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
                resultData.measures = new List<measure>(); //血压、体重、腰围、血糖、肺功能

                foreach (var type in measureTypes)
                {
                    var item = new measure();
                    item.measureType = type;
                    item.pageIndex = 1;
                    item.pageSize = 1000;
                    resultData.measures.Add(item);
                }
                string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(resultData);
                FileLog.WriteLog("入参原文：" + jsonStr);
                string DesStr = DESUtils.EncryptString(jsonStr);
                FileLog.WriteLog("加密入参：" + DesStr);
                string token = DateTimeOffset.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff+08:00");
                FileLog.WriteLog("入参token原文：" + token);
                token = DESUtils.EncryptString(token);
                FileLog.WriteLog("加密access-token入参：" + token);
                string responseResult = Post(url + $"?param={DesStr}", "", token);
                FileLog.WriteLog("出参：" + responseResult);
                // string responseResult = File.ReadAllText("D:\\1.txt");
                if (!string.IsNullOrEmpty(responseResult))
                {
                    try
                    {
                        var responseDto = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseDto>(responseResult);
                        if (responseDto != null && responseDto.code == "1")
                        {
                            var dataList = responseDto.page;
                            if (dataList?.Any() == true)
                            {
                                //获取血压
                                var xy = dataList.FindAll(a => a.measureType == "1001").ToList();
                                StringBuilder stringBuilder = new StringBuilder("血压:");
                                try
                                {
                                    if (xy != null && xy.Any())
                                    {
                                        foreach (PageMeasure item in xy)
                                        {
                                            if (item.dataList?.Any() == true)
                                            {
                                                foreach (data d in item.dataList)
                                                {
                                                    if (!string.IsNullOrEmpty(d.measureData))
                                                    {
                                                        string[] measureData = d.measureData.Split('|');//平均收缩压、平均舒张压、平均脉率
                                                        string[] measureDatamc = measusreType1001.Split('|');
                                                        if (measureData.Length > 9)
                                                        {
                                                            stringBuilder.Append("测量时间:" + d.measureTime+" "+measureDatamc[0] + " " + measureData[0]
                                                                               + "  " + measureDatamc[1] + " " + measureData[1]
                                                                               + "  " + measureDatamc[9] + " " + measureData[9]
                                                                               );

                                                        }
                                                        else
                                                        {
                                                            stringBuilder.Append("测量时间:" + d.measureTime + " " + measureDatamc[0] + " " + measureData[0]
                                                                             + "  " + measureDatamc[1] + " " + measureData[1]
                                                                             );
                                                        }

                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {

                                    FileLog.WriteLog("组装血压出错：" + ex.StackTrace);
                                }
                                stringBuilder.Append("\r\n");
                                var sg = dataList.FindAll(a => a.measureType == "1003").ToList();
                                stringBuilder.Append("身高:");
                                try
                                {
                                    if (sg != null && sg.Any())
                                    {
                                        foreach (PageMeasure item in sg)
                                        {
                                            if (item.dataList?.Any() == true)
                                            {
                                                foreach (data d in item.dataList)
                                                {
                                                    if (!string.IsNullOrEmpty(d.measureData))
                                                    {
                                                        string[] measureData = d.measureData.Split('|');//身高值、体重值
                                                        string[] measureDatamc = measusreType1003.Split('|');

                                                        stringBuilder.Append("测量时间:" + d.measureTime + " " + measureDatamc[0] + " " + measureData[0]
                                                                         + "  " + measureDatamc[1] + " " + measureData[1]
                                                                         );

                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {

                                    FileLog.WriteLog("组装身高出错：" + ex.StackTrace);
                                }
                                stringBuilder.Append("\r\n");

                                var yw = dataList.FindAll(a => a.measureType == "1005").ToList();
                                stringBuilder.Append("腰围:");
                                try
                                {
                                    if (yw != null && yw.Any())
                                    {
                                        foreach (PageMeasure item in yw)
                                        {
                                            if (item.dataList?.Any() == true)
                                            {
                                                foreach (data d in item.dataList)
                                                {
                                                    if (!string.IsNullOrEmpty(d.measureData))
                                                    {
                                                        string[] measureData = d.measureData.Split('|');//腰围值、臀围值
                                                        string[] measureDatamc = measusreType1005.Split('|');

                                                        stringBuilder.Append("测量时间:" + d.measureTime + " " + measureDatamc[0] + " " + measureData[0]
                                                                         + "  " + measureDatamc[1] + " " + measureData[1]
                                                                         );

                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    FileLog.WriteLog("组装腰围出错：" + ex.StackTrace);
                                }
                                stringBuilder.Append("\r\n");
                                var xt = dataList.FindAll(a => a.measureType == "2001").ToList();
                                stringBuilder.Append("血糖:");
                                try
                                {
                                    if (xt != null && xt.Any())
                                    {
                                        foreach (PageMeasure item in xt)
                                        {
                                            if (item.dataList?.Any() == true)
                                            {
                                                foreach (data d in item.dataList)
                                                {
                                                    if (!string.IsNullOrEmpty(d.measureData))
                                                    {
                                                        string[] measureData = d.measureData.Split('|');//血糖值|单位
                                                        string[] measureDatamc = measusreType2001.Split('|');

                                                        stringBuilder.Append("测量时间:" + d.measureTime + " " + measureDatamc[0] + " " + measureData[0]
                                                                         + "  " + measureDatamc[1] + " " + measureData[1]
                                                                         );

                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    FileLog.WriteLog("组装血糖出错：" + ex.StackTrace);
                                }
                                stringBuilder.Append("\r\n");

                                var fgn = dataList.FindAll(a => a.measureType == "4001").ToList();
                                stringBuilder.Append("肺功能:");
                                try
                                {
                                    if (fgn != null && fgn.Any())
                                    {
                                        foreach (PageMeasure item in fgn)
                                        {
                                            if (item.dataList?.Any() == true)
                                            {
                                                foreach (data d in item.dataList)
                                                {
                                                    if (!string.IsNullOrEmpty(d.measureData))
                                                    {
                                                        string[] measureData = d.measureData.Split('|');//
                                                        string[] measureDatamc = measusreType4001.Split('|');

                                                        for (int i = 0; i < measureData.Length; i++)
                                                        {
                                                            stringBuilder.Append(" " + measureDatamc[i] + " " + measureData[i]
                                                                        );
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    FileLog.WriteLog("组装肺功能出错：" + ex.StackTrace);
                                }
                                stringBuilder.Append("\r\n");
                                resultStr = stringBuilder.ToString();
                            }
                        }
                        else
                        {
                            throw new Exception("获取数据失败！");
                        }

                    }
                    catch (Exception ex)
                    {

                        FileLog.WriteLog("接口返回数据格式错误，无法转换" + ex.Message);
                    }

                }
                FileLog.WriteLog("接口调用结束");
            }
            catch (Exception ex)
            {

                FileLog.WriteLog("接口调用结束" + ex.Message+ex.StackTrace);
            }
            return resultStr;
        }

        private static string Post(string url, string content, string token)
        {
            string resultOfPost = "";
            try
            {
                FileLog.WriteLog("请求地址："+url);
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
