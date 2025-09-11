using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.Core.PageMenu;
using Winning.FrameWork.Kernel.Enum;
using Winning.Outp.DAL.PatInfo.DataObject;
namespace Winning.Outp.BLL.PageMenu
{

    /// <summary>
    /// 急观相关
    /// </summary>
    public class OutpEmergencyApplyInCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            PatBasicInfo pat = ContextValueHelper.GetPatientObj();
            if (pat != null)
                GlobalVariable.EmergencyViewObj.ApplyIn(pat);
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.OutpEmergencyApplyInCommandID; }
        }
    }

    public class OutpEmergencyApplyOutCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            PatBasicInfo pat = ContextValueHelper.GetPatientObj();
            if (pat != null)
                GlobalVariable.EmergencyViewObj.ApplyOut(pat);
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.OutpEmergencyApllyOutCommandID; }
        }
    }

    public class OutpEmergencyCancelApplyOutCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            PatBasicInfo pat = ContextValueHelper.GetPatientObj();
            if (pat != null)
                GlobalVariable.EmergencyViewObj.ExecuteCommand(pat, 3);
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.OutpEmergencyCancelApplyOutCommandID; }
        }
    }
}
