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
    /// 康复治疗
    /// </summary>
    public class KangfuTreatCommand : ICommand
    {
        public string ID
        {
            get { return "056A45B9-9477-4CB1-863D-4E1B438E176A"; }
        }

        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请选择一个病人!", System.Windows.Forms.MessageBoxButtons.OK);
                return new FrameWork.Core.Common.RequestResult { Success = true };
            }
           
            string url = "";
            IniFile inifile = new IniFile(GlobalVariable.HisApp.Path.ApplicationPath + @"\Config\UrlConfig.ini");
            string registPatUrl = inifile.IniReadValue("Kfzl", "KfzlUrl");


            if (string.IsNullOrWhiteSpace(registPatUrl))
            {

                GlobalVariable.HisApp.Prompt.Show("请检查UrlConfig,Kfzl下的子节点KfzlUrl！", System.Windows.Forms.MessageBoxButtons.OK);
                return new FrameWork.Core.Common.RequestResult { Success = false };
            }
            //string url = "";
            url = registPatUrl + string.Format("?yydm={0}&ysdm={1}&ysmc={2}&patid={3}&hzxm={4}&syxh={5}&xtbz={6}&blh={7}&ksdm={8}&ksmc={9}&bqdm={10}&bqmc={11}&cwh={12}&xb={13}&nl={14}",
                   GlobalVariable.HisSys.HospitalCode.Trim(),GlobalVariable.DrInfoObj.sYsdm,GlobalVariable.DrInfoObj.sYsmc,
                   pat.Patid,pat.Hzxm.Trim(),pat.Ghxh,"0",pat.Blh, GlobalVariable.HisApp.User.Dept.DeptCode.Trim(),
                   GlobalVariable.HisApp.User.Dept.DeptName.Trim(),"","","",  pat.Sex.Trim() ,
                   GlobalFunction.GetAge(pat.Birth, "0", "", "0").Replace("岁", "").Replace("月", "").Replace("日", ""));
           
            FormKuangfuTreat frm = new FormKuangfuTreat();

            Log("康复URL地址:" + url);
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
