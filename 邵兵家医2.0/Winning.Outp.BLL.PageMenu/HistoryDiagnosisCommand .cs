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
    /// 历史诊断
    /// </summary>
    public class HistoryDiagnosisCommand : ICommand
    {
        public string ID
        {
            get { return MenuCommandId.HistoryDiagnosisCommandID; }
        }

        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {

            object msg = null;
            var ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Qkys.Diagnose.dll", "Winning.Outp.UI.Qkys.Diagnose.StartUp", "RunHistoryDiagnosis", out msg);

            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }
    }
}
