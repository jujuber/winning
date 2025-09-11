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
    /// 手术通知单
    /// </summary>
    public class OperationNoticeCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            if (GlobalVariable.HisSys.OnlineShell.ActiveControl.Text.Trim() != "处方录入")
            {
                GlobalVariable.HisApp.Prompt.Show("【手术通知单】按钮只可放置于处方录入界面！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                object msg = null;
                GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.OperationEdit.dll", "Winning.Outp.UI.OperationEdit.StartUp", "Run2", out msg);
            }
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.OperationNoticeCommandId; }
        }
    }
}
