using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.FrameWork.Core.Common;
using Winning.Outp.Core.Common;
using Winning.Outp.Core;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 双向转诊（接诊）
    /// </summary>
    class SxzzJZCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            Winning.FrameWork.Core.Common.RequestResult result = new Winning.FrameWork.Core.Common.RequestResult();
            object msg;
            GlobalVariable.HisSys.Reflected.RunNetAppEx("Winning.Outp.External.BusiCoop.dll", "Winning.Outp.External.BusiCoop.YwxtObj", "Execute", out msg, 10);
            return result;
        }

        public string ID
        {
            get { return MenuCommandId.SxzzJZCommandID; }
        }
    }
}
