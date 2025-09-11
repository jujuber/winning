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
                            }}", GlobalVariable.DrInfoObj.sYsdm.Trim(),
                            ygjgdm,
                            pat.Patid,
                            pat.Ghxh,
                            1,
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                            );

                tokenvalue = EncryptString(tokenvalue, "Wonders2019");
                string httpst = string.Format("{0}{1}",address, tokenvalue);
                Process.Start(url); 
            }
            return new RequestResult { Success = ret };
        }
        //加密
        public static string EncryptString(string str, string sKey)
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
            var retB = Base64StringURLSafe(ms.ToArray());
            return retB;
        }
    }

    public class IniFile
    {
        public string path;  //INI文件名  
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);  //声明读写INI文件的API函数       
        public IniFile(string INIPath) //类的构造函数，传递INI文件名  
        {
            path = INIPath;
        }
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }  //读INI文件          
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(256);
            int i = GetPrivateProfileString(Section, Key, "", temp, 256, this.path);
            return temp.ToString();
        }

    }
}
