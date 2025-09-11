using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 工作流程
    /// </summary>
    public class WorkflowSetCommand : ICommand
    {
        public Winning.FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            object ErrMsg = null;
            bool ret =GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.DoctorWork.Setting.dll", "Winning.Outp.UI.DoctorWork.Setting.StartUp", "Run", out ErrMsg);
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }


        public string ID
        {
            get { return MenuCommandId.WorkflowSetCommandID; }
        }
    }
}
