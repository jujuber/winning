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
    /// 转诊申请
    /// </summary>
    public class TransferDeptApplyCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            //GlobalVariable.DrInfoObj.DrRight(20);
            //if (GlobalVariable.RunAddin != AddinEnum.None)
            //{
            //    GlobalVariable.HisApp.Prompt.Show("转诊申请按钮只能在病人列表界面使用！", System.Windows.Forms.MessageBoxButtons.OK);
            //}
            //else
            //{
            if (GlobalVariable.PatInfoObj.PatZzsq())
            {
                //object msg = null;
                //bool ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.PatInfo.dll", "Winning.Outp.UI.PatInfo.StartUpFull", "Switch", out msg, msg);
            }
            //}
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.TransferDeptApplyCommandID; }
        }
    }

    /// <summary>
    /// 转诊接收
    /// </summary>
    public class TransferDeptReciveCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            if (GlobalVariable.PatInfoObj.PatZzjs())
            {
                object msg = null;
                bool ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.PatInfo.dll", "Winning.Outp.UI.PatInfo.StartUpFull", "RefreshDate", out msg, msg);
            }
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.TransferDeptReciveCommandID; }
        }
    }

    /// <summary>
    /// 转诊取消
    /// </summary>
    public class TransferDeptCancelApplyCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            if (GlobalVariable.PatInfoObj.PatZzqx())
            {
                object msg = null;
                bool ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.PatInfo.dll", "Winning.Outp.UI.PatInfo.StartUpFull", "RefreshDate", out msg, msg);
            }
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.TransferDeptCancelApplyCommandID; }
        }
    }
}
