using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.FrameWork.Core.Common;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using RequestResult = Winning.FrameWork.Core.Common.RequestResult;

namespace Winning.Outp.BLL.PageMenu
{
    public class CCFCallCommand : ICommand
    {
        public RequestResult Execute(object sender, EventArgs e)
        {
            bool ret = false;
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                object ErrMsg = null;
                ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.ChooseCcf.dll", "Winning.Outp.UI.ChooseCcf.StartUp", "Run", out ErrMsg, pat);
            }
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }

        public string ID { get { return MenuCommandId.CCFCallCommandID; } }
    }
}
