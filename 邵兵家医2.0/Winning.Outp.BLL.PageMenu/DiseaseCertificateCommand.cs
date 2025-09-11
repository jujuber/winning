using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.Kernel.Enum;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 疾病证明单
    /// </summary>
    public class DiseaseCertificateCommandID : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            bool ret = false;
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                object ErrMsg = null;
                ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.Setting.Jbzmd.dll", "Winning.Outp.Setting.Jbzmd.StartUp", "RunForButton", out ErrMsg, pat);
            }
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }


        public string ID
        {
            get { return MenuCommandId.DiseaseCertificateCommandID; }
        }
    }
}
