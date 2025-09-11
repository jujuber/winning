using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.Outp.Core;
using Winning.FrameWork.Kernel.Enum;

namespace Winning.Outp.BLL.PageMenu
{
    
    /// <summary>
    /// 虚拟挂号（医养结合功能
    /// </summary>
    public class XnghCommandID : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            object msg = null;
            GlobalVariable.HisSys.Reflected.RunNetAppEx("Winning.Outp.UI.SetOffLineCfg.dll", "Winning.Outp.UI.SetOffLineCfg.AutoGh", "AddNewPat", out msg, "虚拟挂号", "");
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }


        public string ID
        {
            get
            {
                return MenuCommandId.XnghCommandID;
            }
        }
    }
}
