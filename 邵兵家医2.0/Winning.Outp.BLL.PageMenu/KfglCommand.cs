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
    /// 康复管理
    /// </summary>
    public class KfglCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            bool ret = false;
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                pat.Memo = "康复管理";
                GlobalVariable.PatInfoObj.LocatePatInfo(pat.Ghxh);
                GlobalVariable.Receptacle.SwitchAddinsIdx(AddinEnum.Mzbl);
            }
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }

        public string ID
        {
            get { return "85E1565F-8197-4FCE-82F5-08C17FBFFAEF"; }
        }
    }
}
