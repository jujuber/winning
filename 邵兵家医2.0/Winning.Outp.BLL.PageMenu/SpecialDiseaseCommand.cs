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
    /// 特病
    /// </summary>
    public class SpecialDiseaseCommand : ICommand
    {
        public string ID
        {
            get { return MenuCommandId.SpecialDiseaseCommandID; }
        }

        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            string tbdm = null;
            ICommandExecutor cmdExecutor = GlobalVariable.Receptacle.PlugIns.FirstOrDefault(p => p.AddInsType == GlobalVariable.RunAddin) as ICommandExecutor;
            if (cmdExecutor != null)
            {
                Winning.Outp.Core.Common.RequestResult result = cmdExecutor.Execute(5, null);
                if (result == null)
                {
                    GlobalVariable.HisApp.Prompt.Show("【特病】按钮只允许在检查、检验、治疗申请单录入界面维护！", System.Windows.Forms.MessageBoxButtons.OK);
                    return new FrameWork.Core.Common.RequestResult { Success = false }; //表明特病按钮没有配置在申请单界面
                }
                tbdm = result.Data as string;
            }
            else
            {
                GlobalVariable.HisApp.Prompt.Show("【特病】按钮只允许在检查、检验、治疗申请单录入界面维护！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            bool ret = false;
            if (tbdm != null && !tbdm.Equals("-1"))
            {
                object returnMsg = null;
                ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.SpecialDisease.dll", "Winning.Outp.UI.SpecialDisease.StartUpClass", "RunInit", out returnMsg, tbdm);
            }
            return new FrameWork.Core.Common.RequestResult { Success = ret };

        }
    }
}
