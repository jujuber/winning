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
    /// 会诊申请
    /// </summary>
    public class ConsultApplyCommand : ICommand
    {

        public string ID
        {
            get { return MenuCommandId.ConsultApplyCommandID; }
        }

        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            object msg;
            bool ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Consultant.dll", "Winning.Outp.UI.Consultant.StartUp", "RunHzsq", out msg);

            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }
    }
}
