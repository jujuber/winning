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
    public class OutpAppointCommand_Doctor : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            GlobalVariable.OutpAppointmentObj.Appoint(0);
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.OutpAppointDoctorCommandID; }
        }
    }
    public class OutpAppointAddCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            GlobalVariable.OutpAppointmentObj.Appoint(3);
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.OutpAppointAddCommandID; }
        }
    }

    public class OutpAppointBGHCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            GlobalVariable.OutpAppointmentObj.Appoint(5);
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.OutpAppointBGHCommandID; }
        }
    }

    public class OutpAppointDeptCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            GlobalVariable.OutpAppointmentObj.Appoint(1);
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.OutpAppointDeptCommandID; }
        }
    }

    public class OutpAppointDeptZJGCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            GlobalVariable.OutpAppointmentObj.Appoint(4);
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.OutpAppointDeptZJGCommandID; }
        }
    }

    public class OutpAppointRenJiGhdjCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            GlobalVariable.OutpAppointmentObj.Appoint(2);
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.OutpAppointRenJiGhdjCommandID; }
        }
    }

    public class OutpAppointRenJICommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            GlobalVariable.OutpAppointmentObj.Appoint(0);
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.OutpAppointRenJICommandID; }
        }
    }
}
