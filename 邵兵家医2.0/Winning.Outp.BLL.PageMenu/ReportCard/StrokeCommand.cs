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
    /// 脑卒中报告卡
    /// </summary>
    public class StrokeCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object context,EventArgs e)
        {
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请选择需要操作报告卡的病人!", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                GlobalVariable.ReportCardCenterObj.Modify(Core.Enum.ReportCardEnum.StrokeReport, pat);
            }

            return new FrameWork.Core.Common.RequestResult { Success = true };
        }


        public string ID
        {
            get { return MenuCommandId.StrokeCommandID; }
        }
    }
}
