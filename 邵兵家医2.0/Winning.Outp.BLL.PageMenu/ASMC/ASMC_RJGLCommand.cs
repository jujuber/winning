using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.Kernel.Enum;
using System.Windows.Forms;
using System.IO;

namespace Winning.Outp.BLL.PageMenu
{
    public class ASMC_RJGLCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            try
            {
                string batPath = GlobalVariable.HisApp.Path.ApplicationPath + @"\Winning.Mzys.Asmc.External.dll";
                if (File.Exists(batPath))
                {
                    object msg = null;
                    //GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Patient.MedicalRecordsQuery.dll", "Winning.Outp.UI.Patient.MedicalRecordsQuery.StartUp", "Run", out msg, GlobalVariable.PatInfoObj.CurrSelectPatinfo);
                    GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Asmc.UI.PatInfo.dll", "Winning.Asmc.UI.PatInfo.MzysStratUp", "Run", out msg, "日间管理", "");
                }
                else
                    GlobalVariable.HisApp.Prompt.Show("日间系统未部署！请部署成功后应用！", MessageBoxButtons.OK);
            }
            catch
            {
                GlobalVariable.HisApp.Prompt.Show("日间系统未成功部署！请部署成功后应用！", MessageBoxButtons.OK);
            }
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.ASMC_RJGL_CommandID; }
        }
    }
}
