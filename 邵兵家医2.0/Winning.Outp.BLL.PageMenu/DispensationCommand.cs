using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.FrameWork.Core.Common;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;

namespace Winning.Outp.BLL.PageMenu
{
    public class DispensationCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            object msg = null;
            bool ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Dpydj.dll", "Winning.Outp.UI.Dpydj.StartUp", "Run", out msg);

            return new FrameWork.Core.Common.RequestResult { Success = true };
        }


        public string ID
        {
            get { return MenuCommandId.DispensationCommandID; }
        }
    }
}
