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
    /// 病人范围设置
    /// </summary>
  public  class PatRangeShowSet:ICommand
    {
      public FrameWork.Core.Common.RequestResult Execute(object context, EventArgs e)
      {
          bool ret = GlobalVariable.HisSys.RunEvent(SystemType.HT, "LoadPatRange");
          return new FrameWork.Core.Common.RequestResult { Success = ret };
      }

        public string ID
        {
            get { return MenuCommandId.PatRangeShowSetCommandID; }
        }
    }
}
