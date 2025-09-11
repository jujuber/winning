using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 恢复签约提示
    /// </summary>
    public class HfqytsCommand:ICommand
    {
        public string ID
        {
            get { return ""; }
        }



        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            GlobalFunction.WriteReg(@"\PatInfo\" + GlobalVariable.DrInfoObj.sYsdm.Trim() + "", "");
            bool ret = true;
            
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }
    }
}
