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
    /// 解锁
    /// </summary>
  public  class UnlockPatCommand:ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            if (GlobalVariable.HisApp.Prompt.Show("确定将患者解锁？解锁之后该病人将不计算进您的工作量", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                GlobalVariable.HisSys.RunEvent(SystemType.HT, "LoadUnLockPatinfo");
            } 
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }


        public string ID
        {
            get
            {
                return MenuCommandId.UnlockPatCommandID;
            }
        }
    }
}
