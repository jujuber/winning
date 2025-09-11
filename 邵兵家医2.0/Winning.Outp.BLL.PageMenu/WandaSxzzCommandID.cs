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
    public class WandaSxzzCommandID : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            bool ret = false;
            var pat = GlobalVariable.PatInfoObj.CurrPatinfo;//ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                object ErrMsg = null;
                ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.Twowayreferral.External.dll", "Winning.Outp.Twowayreferral.External.StartUp", "PortalButton", out ErrMsg, pat);
            }
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }


        public string ID
        {
            get { return "284BE549-CDC7-4C4B-8391-3D7ADF43F573"; }
        }
    }
}
