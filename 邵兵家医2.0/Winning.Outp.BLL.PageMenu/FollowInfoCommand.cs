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
    /// 随访信息
    /// </summary>
    public class FollowInfoCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            object msg = null;
            GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Ybxx.dll", "Winning.Outp.UI.Ybxx.StartUp", "Run", out msg);
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.FollowInfoCommandID; }
        }
    }
}
