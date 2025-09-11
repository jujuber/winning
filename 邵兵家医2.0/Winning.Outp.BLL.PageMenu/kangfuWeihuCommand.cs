
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using System.IO;
using System.Windows.Forms;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 康复治疗维护
    /// </summary>
    public class kangfuWeihuCommand : ICommand
    {
        public string ID
        {
            get { return "BC431B8F-181C-4CD5-81BE-BA4C6F902007"; }
        }

        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            string url = "";
            IniFile inifile = new IniFile(GlobalVariable.HisApp.Path.ApplicationPath + @"\Config\UrlConfig.ini");
            string registPatUrl = inifile.IniReadValue("Kfzl", "KfzlwhUrl");


            if (string.IsNullOrWhiteSpace(registPatUrl))
            {

                GlobalVariable.HisApp.Prompt.Show("请检查UrlConfig,Kfzl下的子节点KfzlwhUrl！", System.Windows.Forms.MessageBoxButtons.OK);
                return new FrameWork.Core.Common.RequestResult { Success = false };
            }
            //string url = "";
            url = registPatUrl + string.Format("?ysbm={0}&yydm={1}", GlobalVariable.DrInfoObj.sYsdm, GlobalVariable.HisSys.HospitalCode.Trim());
            FormKuangfuTreat frm = new FormKuangfuTreat();
            Log("康复维护地址:" + url);
            frm.SetUrl(url);
            frm.ShowDialog();
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public static void Log(string title)
        {
            //if (GlobalVariable.HisApp.Config.Get("HT659")=="是")
            //{
            try
            {
                if (!File.Exists(string.Format(@"c:\OutpLog\OutpLog_{0:yyyyMMdd}.txt", DateTime.Now)))
                {
                    Directory.CreateDirectory(@"c:\OutpLog");
                    using (FileStream stream = File.Create(string.Format(@"c:\OutpLog\OutpLog_{0:yyyyMMdd}.txt", DateTime.Now)))
                    {
                        stream.Close();
                    }
                }

                File.AppendAllText(string.Format(@"c:\OutpLog\OutpLog_{0:yyyyMMdd}.txt", DateTime.Now),
                   string.Format("★{0:yyyy-MM-dd HH:mm:ss.fff}                  {1}\r\n\r\n", DateTime.Now, title));

            }
            catch { }
            //}
        }
    }
}
