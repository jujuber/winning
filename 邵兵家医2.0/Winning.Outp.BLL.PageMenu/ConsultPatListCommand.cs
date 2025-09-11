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
    /// 会诊病人列表
    /// </summary>
    public class ConsultPatListCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
             object msg;
             bool ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Consultant.dll", "Winning.Outp.UI.Consultant.StartUp", "Run", out msg);
        
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }


        public string ID
        {
            get { return MenuCommandId.ConsultPatListCommandID; }
        }
    }
}
