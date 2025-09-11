using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;

namespace Winning.Outp.BLL.PageMenu
{
    public class ChssTransPatCommand : ICommand
    {
        public string ID
        {
            get { return MenuCommandId.ChssTransPatCommandID; }
        }



        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            bool ret = false;
            //var pat = ContextValueHelper.GetPatientObj();
            var pat = GlobalVariable.PatInfoObj.CurrPatinfo;
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                object ErrMsg = null;
                ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.JTYS.ChssTransPat.dll", "Winning.Outp.UI.JTYS.ChssTransPat.StartUp", "Run3", out ErrMsg, pat);
            }
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }
    }
}
