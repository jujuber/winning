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
    /// 病人信息修改
    /// </summary>
    public class PatInfoModifyCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object context, EventArgs e)
        {
            object msg = null;
            GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.External.MzBrxxwh.dll", "Winning.Outp.External.MzBrxxwh.StartUp", "RunUI", out msg);
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.PatInfoModifyCommandID; }
        }
    }
}
