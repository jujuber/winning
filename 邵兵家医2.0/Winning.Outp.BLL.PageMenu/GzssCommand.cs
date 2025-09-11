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
    /// 工作流程
    /// </summary>
    public class GzssCommand : ICommand
    {
        public Winning.FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                object ErrMsg = null;
                bool ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.External.HealthManager.dll", "Winning.Outp.External.HealthManager.GzssInfo", "GetGzssInfo", out ErrMsg, pat);
               
            }
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }


        public string ID
        {
            get { return "AA3FC1BF-6196-4878-BC14-B60FCE7043EF"; }
        }
    }
}
