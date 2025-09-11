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
    /// 医生工作量
    /// </summary>
    public class DoctorWorkloadCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            object ErrMsg = null;
            bool ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Query.DoctorWorkload.dll", "Winning.Outp.UI.Query.DoctorWorkload.StartUp", "Run", out ErrMsg);
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }


        public string ID
        {
            get { return MenuCommandId.DoctorWorkloadCommandID; }
        }
    }
}
