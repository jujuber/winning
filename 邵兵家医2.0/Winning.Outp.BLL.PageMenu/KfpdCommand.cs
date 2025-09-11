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
    /// 康复评定
    /// </summary>
    public class KfpdCommand:ICommand
    {
        public string ID
        {
            get { return "B875B04D-855D-4165-9CA6-E6FFC40403C0"; }
        }

        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请选择一个病人!", System.Windows.Forms.MessageBoxButtons.OK);
                return new FrameWork.Core.Common.RequestResult { Success = true };
            }
            //else
            //{
            //    object ErrMsg = null;
            //    bool ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Brgmjl.dll", "Winning.Outp.UI.Brgmjl.StartUp", "Run", out ErrMsg, pat);
            //}
            //return new FrameWork.Core.Common.RequestResult { Success = true };
            string config = GlobalVariable.HisApp.Config.Get("HT671");
            if (config == "")
            {
                GlobalVariable.HisApp.Prompt.Show("请先配置参数HT671！", System.Windows.Forms.MessageBoxButtons.OK);
                return new FrameWork.Core.Common.RequestResult { Success = true };
            }
            if (config == "0")
            {
                if (!File.Exists(Path.Combine(Application.StartupPath, "Winning.Cure.UI.TreatmentPgd.exe")))
                {
                    GlobalVariable.HisApp.Prompt.Show(Application.StartupPath + @"\\Winning.Cure.UI.TreatmentPgd.exe文件不存在，请联系系统管理员！", System.Windows.Forms.MessageBoxButtons.OK);
                }
                else
                {
                    string[] arg = new string[4];
                    arg[0] = "0";
                    arg[1] = pat.Patid.ToString();
                    arg[2] = GlobalVariable.DrInfoObj.sYsdm;
                    arg[3] = GlobalVariable.DrInfoObj.sDbKsdm;

                    var p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = Application.StartupPath + @"\\Winning.Cure.UI.TreatmentPgd.exe";
                    p.StartInfo.Arguments = string.Join(",", arg);
                    p.Start();
                }
            }
            if (config == "1")
            {
                string url = GlobalVariable.HisApp.Config.Get("HT672");
                if (url == "")
                {
                    GlobalVariable.HisApp.Prompt.Show("请先配置参数HT672！", System.Windows.Forms.MessageBoxButtons.OK);
                }
                else
                {
                    url = url + string.Format("?&syxh={0}&xtbz=0&yydm={1}&czyh={2}&czymc={3}", pat.Patid.ToString(),
                        GlobalVariable.HisSys.HospitalCode, GlobalVariable.DrInfoObj.sYsdm, GlobalVariable.DrInfoObj.sYsmc);
                    System.Diagnostics.Process.Start(url);
                }
            }

            return new FrameWork.Core.Common.RequestResult { Success = true };
        }
    }
}
