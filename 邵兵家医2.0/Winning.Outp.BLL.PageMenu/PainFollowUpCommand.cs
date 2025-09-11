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
    /// 疼痛随访
    /// </summary>
  public  class PainFollowUpCommand:ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
      {
          object msg = null;
          GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.PainFollowUp.dll", "Winning.Outp.UI.PainFollowUp.StartUp", "Run", out msg, "疼痛随访", "");
          return new FrameWork.Core.Common.RequestResult { Success=true};
        }

        public string ID
        {
            get { return MenuCommandId.PainFollowUpCommandID; }
        }
    }
}
