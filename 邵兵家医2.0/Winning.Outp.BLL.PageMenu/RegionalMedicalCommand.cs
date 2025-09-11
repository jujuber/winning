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
    /// 区域病历调阅
    /// </summary>
    public class RegionalMedicalCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {

            //GlobalVariable.HisSys.RunEvent(SystemType.HT, "Qybldy");
            //return new FrameWork.Core.Common.RequestResult { Success = true };

            bool ret = false;
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                object ErrMsg = null;
                ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.Qybldy.External.dll", "Winning.Outp.Qybldy.External.StartUpClass", "InvokeQybldyForButton", out ErrMsg, pat);
            }
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }

        public string ID
        {
            get { return MenuCommandId.RegionalMedicalCommandId; }
        }
    }
}
