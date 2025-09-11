using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.Outp.DAL.PatInfo.DataObject;
using Winning.FrameWork.Kernel.Enum;
using System.Windows.Forms;
namespace Winning.Outp.BLL.PageMenu
{
    public class OutpSpecialTreatCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            PatBasicInfo pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            else
            {
                string ht378 = GlobalVariable.HisApp.Config.Get("HT378");
                /// <summary>
                /// 门特医保代码集合
                /// </summary>
                string[] sMtybdm = ht378.Split(',');
                bool bShowMtzl = !string.IsNullOrWhiteSpace(ht378);
                if (bShowMtzl && !sMtybdm.Contains(pat.Ybdm.Trim()))
                {
                    GlobalVariable.HisApp.Prompt.Show("该患者不是门特病人,无需做诊疗计划", MessageBoxButtons.OK);
                }
                else
                {
                    object msg = null;
                    GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Mztb.dll", "Winning.Outp.UI.Mztb.StartUp", "RunSq", out msg);
                }
            }
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.OutpSpecialTreatCommandID; }
        }
    }
}
