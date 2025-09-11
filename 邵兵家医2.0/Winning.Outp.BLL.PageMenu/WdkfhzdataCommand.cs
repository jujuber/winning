using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.DAL.Kernel;
using Winning.FrameWork.Kernel.Enum;
using Winning.Outp.Core;

namespace Winning.Outp.BLL.PageMenu
{
    public class WdkfhzdataCommand : ICommand
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
        public string ID
        {
            get { return "D0C3681E-2FC9-430C-A109-4300048AC4D8"; }
        }
        LogWriter loger;
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
                //http://{区县部署地址}/ris/api/recoverySubmit
                loger = LogWriter.CreateLoger("wdkf");

                
                IniFile inifile = new IniFile(GlobalVariable.HisApp.Path.ApplicationPath + @"\Config\UrlConfig.ini");
                string address = inifile.IniReadValue("WDKFMZ", "kfhzurl");
                if (string.IsNullOrWhiteSpace(address))
                {

                    GlobalVariable.HisApp.Prompt.Show("地址未配置，请检查UrlConfig内WDKFMZ下的子节点kfhzurl！",
                        System.Windows.Forms.MessageBoxButtons.OK);
                    return new RequestResult { Success = ret };
                }
                loger.writeLogMessage("获取urlconfig.ini获取address=" + address);
                string data = GetPatInfo(pat.Ghxh.ToString(), pat.Patid.ToString());
                if (string.IsNullOrWhiteSpace(data))
                {
                    GlobalVariable.HisApp.Prompt.Show("未查询到数据！",
                       System.Windows.Forms.MessageBoxButtons.OK);
                    return new RequestResult { Success = ret };
                }
                
                data = string.Format("{{{0}}}", data);
                loger.writeLogMessage("加密前：" + data);
                data = EncryptString(data, "Wonders2019");
                loger.writeLogMessage("加密后：" + data);
                string result = HttpPost(address, data);
                resultdata resultdata = new resultdata();
                loger.writeLogMessage("调用url请求返回：" + result);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    resultdata = JsonConvert.DeserializeObject<resultdata>(result);
                }
                if (resultdata.code == "1")
                {
                    GlobalVariable.HisApp.Prompt.Show("上传成功",
                       System.Windows.Forms.MessageBoxButtons.OK);
                    return new RequestResult { Success = true };
                }

                GlobalVariable.HisApp.Prompt.Show(resultdata.msg,
                       System.Windows.Forms.MessageBoxButtons.OK);
                // Process.Start(url); 
            }
            return new RequestResult { Success = ret };
        }
        private string GetPatInfo(string ghxh, string patid)
        {

            string text = string.Format("exec usp_mz_kfhzxx_getdata {0},{1}", ghxh, patid);
          //  WriteLog("患者信息开始：" + text);
            DataTable dataTable = ISql5.GetDataTable(text);
            StringBuilder stringBuilder = new StringBuilder();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (i == dataTable.Columns.Count - 1)
                    {
                        stringBuilder.AppendFormat("\"{0}\":\"{1}\"", dataTable.Columns[i].ColumnName, dataTable.Rows[0][dataTable.Columns[i].ColumnName].ToString());
                    }
                    else
                    {
                        stringBuilder.AppendFormat("\"{0}\":\"{1}\",", dataTable.Columns[i].ColumnName, dataTable.Rows[0][dataTable.Columns[i].ColumnName].ToString());
                    }
                }
            }
            return stringBuilder.ToString();
        }

        public string HttpPost(string url, string body)
        {

            System.Net.ServicePointManager.Expect100Continue = false;
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=utf8";
            byte[] buffer = encoding.GetBytes(body);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }

            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
        private string EncryptString(string str, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(str);
            des.Mode = CipherMode.ECB;
            des.Key = UTF8Encoding.UTF8.GetBytes(sKey).Take(8).ToArray();
            des.IV = UTF8Encoding.UTF8.GetBytes(sKey).Take(8).ToArray();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            var retB = Base64StringURLSafe(ms.ToArray());
            return retB;
        }
        private string Base64StringURLSafe(byte[] bytes)
        {
            string base64String = Convert.ToBase64String(bytes);
            return base64String.Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }

    }

    public class resultdata
    {
        public string code { get; set; }
        public string msg { get; set; }
        public string data { get; set; }
    }
}
