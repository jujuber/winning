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
    /// 退费申请
    /// </summary>
    public class RefundApplyCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                object msg;
                GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Mztf.dll", "Winning.Outp.UI.Mztf.StartUp", "Run2", out msg, pat);
            }
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.RefundApplyCommandID; }
        }
    }
}
