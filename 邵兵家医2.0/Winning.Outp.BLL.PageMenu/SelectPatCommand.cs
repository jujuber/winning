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
    /// 选病人
    /// </summary>
    public class SelectPatCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            GlobalVariable.HisSys.RunEvent(SystemType.HT, "InputCardNo");
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }


        public string ID
        {
            get { return MenuCommandId.SelectPatCommandID; }
        }
    }
}
