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
    /// 检查中
    /// </summary>
   public class PatJczCommand:ICommand
    {
       public FrameWork.Core.Common.RequestResult Execute(object context, EventArgs e)
       {
           GlobalVariable.PatInfoObj.UpdatePatStatus_Jc();
           GlobalVariable.HisSys.RunEvent(SystemType.HT, "InputCardNo");
           return new FrameWork.Core.Common.RequestResult { Success = true };
       }

        public string ID
        {
            get { return MenuCommandId.PatJczCommandID; }
        }
    }
}
