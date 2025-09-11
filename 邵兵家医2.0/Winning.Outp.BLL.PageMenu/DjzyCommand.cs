using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.FrameWork.Core.Common;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;


namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 大经中医（中医辅助）
    /// </summary>
    public class DjzyCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {

            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请选择一个病人!", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                object msg = null;
                bool ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Order.dll", "Winning.Outp.UI.Order.StartUpFull", "RunZyfzView", out msg, pat);
            }
            return new FrameWork.Core.Common.RequestResult { Success = true };
            

        }


        public string ID
        {
            get { return "81E52D28-82EC-4DA3-B93D-2233956B3DDD"; }
        }
    }
}
