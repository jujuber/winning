using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.Outp.UI.JTYS.GetChssForm;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Web.Security;
using System.IO;

namespace Winning.Outp.BLL.PageMenu
{
    public class HealthyAssCommand : ICommand
    {
        public string ID
        {
            get { return MenuCommandId.HealthyAssCommandID; }
        }



        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            //获取机构代码和url地址，格式为机构代码+url地址 用逗号隔开；
            string str = GlobalVariable.HisApp.Config.Get("HT955").Trim();
            if (str == "")
            {
                GlobalVariable.HisApp.Prompt.Show("未配置机构代码或url地址！", MessageBoxButtons.OK);
                return new FrameWork.Core.Common.RequestResult { Success = false };
            }
            //string jgdm = "13221230400";
            string jgdm = str.Split(',')[0].Trim();
            string url = str.Split(',')[1].Trim();
            string Ysdm = GlobalVariable.DrInfoObj.sYsdm;
            string Ysmc = GlobalVariable.DrInfoObj.sYsmc;
            string token = jgdm + "&" + Ysdm + "&" + Ysmc;
            string token1 = DESEncrypt("reportauditesecret132143", token);
            //string pgUrl = "http://31.8.131.211:18080/XkHealthReportAudit/login?token=" + token1;
            string pgUrl = url + "/XkHealthReportAudit/login?token=" + token1;
            System.Diagnostics.Process.Start(pgUrl);
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DESEncrypt(string key, string str)
        {
            if (string.IsNullOrEmpty(str)) return "";
            if (key.Length < 8) throw new Exception("加密key小于8或者加密字符串为空！");
            byte[] bKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] bIV = null;
            byte[] bStr = Encoding.UTF8.GetBytes(str);
            try
            {
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                desc.Padding = PaddingMode.PKCS7;
                desc.Mode = CipherMode.ECB;
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, desc.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(bStr, 0, bStr.Length);
                        cStream.FlushFinalBlock();
                        byte[] res = mStream.ToArray();
                        return Convert.ToBase64String(res);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
