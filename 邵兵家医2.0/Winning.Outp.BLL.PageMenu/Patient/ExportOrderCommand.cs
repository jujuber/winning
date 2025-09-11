using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.Outp.Core;
using Winning.Outp.DAL.PatInfo.DataObject;
using Winning.FrameWork.Kernel.Enum;

namespace Winning.Outp.BLL.PageMenu
{

    /// <summary>
    /// 导出处方（医养结合功能
    /// </summary>
    public class ExportOrderCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            object msg = null;
            GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.SetOffLineCfg.dll", "Winning.Outp.UI.SetOffLineCfg.StartUp", "RunForButton", out msg, "导出处方");
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }


        public string ID
        {
            get
            {
                return MenuCommandId.ExportOrderCommandID;
            }
        }
    }
}

