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
    /// 过敏信息
    /// </summary>
    public class AllergyCommand : ICommand
    {
        
        public string ID
        {
            get { return MenuCommandId.AllergyCommandID; }
        }

        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请选择一个病人!", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                object ErrMsg = null;
                bool ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Brgmjl.dll", "Winning.Outp.UI.Brgmjl.StartUp", "Run", out ErrMsg, pat);
            }
            return new FrameWork.Core.Common.RequestResult { Success = true };
            
        }
    }
}
