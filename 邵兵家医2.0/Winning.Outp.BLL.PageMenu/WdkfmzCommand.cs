using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using Winning.FrameWork.Core.Common;
using Winning.Outp.Core;

namespace Winning.Outp.BLL.PageMenu
{
    public class WdkfmzCommand : ICommand
    {
        public string ID
        {
            get { return "7ABA3987-3C61-407C-88B7-6D3CDF9DB603"; }
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
                string address = inifile.IniReadValue("WDKFMZ", "address");
                string ygjgdm = inifile.IniReadValue("WDKFMZ", "ygjgdm");
                string url = inifile.IniReadValue("WDKFMZ", "url"); 
                if (string.IsNullOrWhiteSpace(address))
                {
                    GlobalVariable.HisApp.Prompt.Show("地址未配置，请检查UrlConfig内WDKFMZ下的子节点address！", System.Windows.Forms.MessageBoxButtons.OK);
                    return new RequestResult { Success = ret };
                }
                string tokenvalue = string.Format(@"{{
                            ""doctorId"":""{0}"",
                            ""hospitalid"":""{1}"",
                            ""patientid"":""{2}"",
                            ""outpationid"":""{3}"",
                            ""type"":""{4}"",
                            ""url"":""{5}"",
                            ""loginTime"":""{6}""
                            }}", 
                            GlobalVariable.DrInfoObj.sYsdm.Trim(), //0
                            ygjgdm,  //1
                            pat.Patid, //2
                            pat.Ghxh,  //3
                            1, //4
                            url,//5
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") //6
                            );

                tokenvalue = EncryptString(tokenvalue, "Wonders2019");
                
                string httpst = string.Format("{0}{1}",address, tokenvalue);

              //  System.Windows.Forms.MessageBox.Show(httpst);

                try
                {
                    Process.Start(ChromeHelper.GetChromePath(), httpst);
                }
                catch
                {

                    Process.Start(httpst);
                }
               // Process.Start(url); 
            }
            return new RequestResult { Success = ret };
        }
        //加密
        private string EncryptString(string str, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(str);
            des.Mode = CipherMode.ECB;
            des.Key = UTF8Encoding.UTF8.GetBytes(sKey).Take(8).ToArray(); //ASCIIEncoding.ASCII.GetBytes(sKey);// 密匙
            des.IV = UTF8Encoding.UTF8.GetBytes(sKey).Take(8).ToArray();// ASCIIEncoding.ASCII.GetBytes(sKey);// 初始化向量Ss
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            var retB = ToBase64StringURLSafe(ms.ToArray());
            return retB;
        }
        /// <summary>
        /// 将byte数组转换为java安全的base64字符串
        /// </summary>
        /// <param name="convert"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private string ToBase64StringURLSafe(byte[] bytes)
        {
            string base64String = Convert.ToBase64String(bytes);
            return base64String.Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }
        private byte[] FromBase64StringURLSafe(string SafeString)
        {
            SafeString = SafeString.Replace("-", "+").Replace("_", "/");
            var base64 = Encoding.ASCII.GetBytes(SafeString);
            var padding = base64.Length * 3 % 4;//(base64.Length*6 % 8)/2
            if (padding != 0)
            {
                SafeString = SafeString.PadRight(SafeString.Length + padding, '=');
            }
            return Convert.FromBase64String(SafeString);
        }
    }

    public class ChromeHelper
    {
        public static string GetChromePath()
        {
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot;
            string path = "";
            string chromeKey = "";
            foreach (var chrome in regKey.GetSubKeyNames())
            {
                if (chrome.ToUpper().Contains("CHROMEHTML"))
                {
                    chromeKey = chrome;
                }
            }
            if (!string.IsNullOrEmpty(chromeKey))
            {
                path = Microsoft.Win32.Registry.GetValue(@"HKEY_CLASSES_ROOT\" + chromeKey + @"\shell\open\command", null, null) as string;
                if (path != null)
                {
                    var split = path.Split('\"');
                    path = split.Length >= 2 ? split[1] : null;
                }
            }
            return path;
        }
    }
}
