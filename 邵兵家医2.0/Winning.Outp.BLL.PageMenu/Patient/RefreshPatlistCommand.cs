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
    /// 病人列表刷新
    /// </summary>
    public class RefreshPatlistCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object context, EventArgs e)
        {
            object msg = null;
            bool ret = GlobalVariable.HisSys.Reflected.RunNetAppEx("Winning.Outp.UI.PatInfo.dll", "Winning.Outp.UI.PatInfo.StartUpFull", "ReloadPatList", out msg, msg);
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }


        public string ID
        {
            get { return MenuCommandId.RefreshPatlistCommandID; }
        }
    }
}
