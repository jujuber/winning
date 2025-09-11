using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.FrameWork.Core.Common;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;

namespace Winning.Outp.BLL.PageMenu
{
    public class TransHospitalCommand:ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            object msg = null;
            bool ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.TransHospital.dll", "Winning.Outp.UI.TransHospital.StartUp", "TransHopital", out msg);
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.TransHospitalCommandID; }
        }
    }
}
