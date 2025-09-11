using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.FrameWork.Kernel.Enum;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.Outp.Core;
using Winning.Outp.DAL.PatInfo.DataObject;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 首诊测压
    /// </summary>
    public class MeasureBloodCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            bool succ = false;
            object msg = null;
            PatBasicInfo pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
                GlobalVariable.HisApp.Prompt.Show("请先双击选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            else
                succ = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Brsmtz.dll", "Winning.Outp.UI.Brsmtz.StartUp", "Run", out msg, msg, pat);
            return new FrameWork.Core.Common.RequestResult { Success = succ };
        }

        public string ID
        {
            get { return MenuCommandId.MeasureBloodCommandID; }
        }
    }
}
