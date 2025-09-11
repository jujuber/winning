using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.FrameWork.Kernel.Enum;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.Outp.Core;
using System.Windows.Forms;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 历史处方
    /// </summary>
    public class HistoryRecipeCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            if (GlobalVariable.RunAddin != AddinEnum.Recipe)
            {
                GlobalVariable.HisApp.Prompt.Show("历史处方只可在就诊界面使用！", MessageBoxButtons.OK);  //暂时这么处理---历史处方调用界面

            }
            else
               GlobalVariable.HisSys.RunEvent(SystemType.HT, "LoadHistoryRecipeEvent");
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }


        public string ID
        {
            get { return MenuCommandId.HistoryRecipeCommandID; }
        }
    }
}
