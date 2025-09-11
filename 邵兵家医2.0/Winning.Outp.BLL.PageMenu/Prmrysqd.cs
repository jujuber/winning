using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Winning.FrameWork.Core.Common;
using Winning.Outp.Core;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 浦人民入院申请单 
    /// </summary>
    public class Prmrysqd : ICommand
    {
        public string ID
        {
            get { return "950B500E-39C4-4099-A5A3-C46954EA8A3A"; }
        }

        public RequestResult Execute(object sender, EventArgs e)
        {
            bool ret = false;
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {


                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                IniFile inifile = new IniFile(GlobalVariable.HisApp.Path.ApplicationPath + @"\Config\UrlConfig.ini");
                string registPatUrl = inifile.IniReadValue("Prmrysqd", "url");
                string jgdm = inifile.IniReadValue("Prmrysqd", "jgdm");
                string jgmc = inifile.IniReadValue("Prmrysqd", "jgmc");
                if (string.IsNullOrWhiteSpace(registPatUrl))
                {

                    GlobalVariable.HisApp.Prompt.Show("浦人民入院申请单接口地址未配置，请检查UrlConfig内,Prmrysqd下的子节点url！", System.Windows.Forms.MessageBoxButtons.OK);
                    return new RequestResult { Success = ret };
                }
                string url = "";
                //url = http://10.220.72.78:8060/#/ApplicationPage?ghxh={0}&hzxm={1}&hzxb={2}&hznl={3}&sfzh={4}&mzzd={5}&bqms={6}&yxksdm={7}&yxksmc={8}&yxbqdm={9}&yxbqmc={10}&jgdm={11}&jgmc={12}&sqysdm={13}&sqysmc={14}&sqysdh={15}&gtjg={16}
                string paras = "ghxh={0}&hzxm={1}&hzxb={2}&hznl={3}&sfzh={4}&mzzd={5}&bqms={6}&yxksdm={7}&yxksmc={8}&yxbqdm={9}&yxbqmc={10}&jgdm={11}&jgmc={12}&sqysdm={13}&sqysmc={14}&sqysdh={15}&gtjg={16}";
                //pat.Ghxh, pat.Hzxm, pat.Sex?.Trim(), GlobalFunction.GetAge(pat.Birth, "0", "", "0").Replace("岁", "").Replace("月", "").Replace("日", ""), pat.Sfzh, "门诊诊断", "病情描述", "意向科室代码", "意向科室名称", "意向病区代码", "意向病区名称", "机构代码", "机构名称", "社区医生代码", "社区医生名称", "社区医生电话");
                url = registPatUrl + string.Format(paras, pat.Ghxh, pat.Hzxm, pat.Sex.Trim(), GlobalFunction.GetAge(pat.Birth, "0", "", "0").Replace("岁", "").Replace("月", "").Replace("日", ""), pat.Sfzh, pat.Zdmc, "病情描述", 
                    GlobalVariable.DrInfoObj.sDbKsdm.Trim(), GlobalVariable.DrInfoObj.sDbKsmc,
                    GlobalVariable.DrInfoObj.sKsdm, GlobalVariable.DrInfoObj.sKsmc, jgdm, jgmc, 
                    GlobalVariable.DrInfoObj.sYsdm.Trim(), GlobalVariable.DrInfoObj.sYsmc.Trim(), GlobalVariable.DrInfoObj.sPhone.Trim(), "");
                Helper.WriteLog("浦人民入院申请单入参：" + url);
                //MessageBox.Show(url);
                Process.Start(url);
                //string result = Helper.HttpGet(url);
                //Helper.WriteLog("浦人民入院申请单出参：" + result);
                

            }
            return new RequestResult { Success = ret };
        }
    }


    public static class Helper
    {
        public static string HttpGet(string url)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Method = "GET";
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    using (StreamReader sReader = new StreamReader(responseStream))
                    {
                        result = sReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public static string HttpPost(string Url, string paraUrlCoded)
        {
            string retString = "";
            if (string.IsNullOrWhiteSpace(Url))
            {
                MessageBox.Show("服务接口地址未配置！");
                return retString;
            }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/json";

                byte[] payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);

                request.ContentLength = payload.Length;
                Stream myRequestStream = request.GetRequestStream();
                myRequestStream.Write(payload, 0, payload.Length);
                myRequestStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("服务接口调用错误：{0}",ex.Message));
            }

            return retString;
        }

        public static void WriteLog(string msg)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log";
            if (!System.IO.Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath);
            }
            string logPath = AppDomain.CurrentDomain.BaseDirectory + "Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            try
            {
                using (System.IO.StreamWriter sw = System.IO.File.AppendText(logPath))
                {
                    sw.WriteLine("消息：" + msg);
                    sw.WriteLine("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    sw.WriteLine("**************************************************");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (System.IO.IOException e)
            {
                using (System.IO.StreamWriter sw = System.IO.File.AppendText(logPath))
                {
                    sw.WriteLine("异常：" + e.Message);
                    sw.WriteLine("时间：" + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss.fff"));
                    sw.WriteLine("**************************************************");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
    }
}
