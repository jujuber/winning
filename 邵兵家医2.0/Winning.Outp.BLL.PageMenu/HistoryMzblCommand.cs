using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.Kernel.Enum;
using System.Windows.Forms;

namespace Winning.Outp.BLL.PageMenu
{
        /// <summary>
        /// 历史病历
        /// </summary>
        public class HistoryMzblCommand : ICommand
        {
            public string ID
            {
                get { return MenuCommandId.HistoryMzblCommandID; }
            }

            public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
            {
                if (GlobalVariable.RunAddin != AddinEnum.Mzbl)
                {
                    GlobalVariable.HisApp.Prompt.Show("历史病历按钮只可在病历录入界面使用！", MessageBoxButtons.OK);  //暂时这么处理---历史处方调用界面
                }
                else
                {
                    var pat = ContextValueHelper.GetPatientObj();
                    if (pat == null)
                    {
                        GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
                    }
                    else
                    {
                        object msg;
                        GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.MedicalRecords.dll", "Winning.Outp.UI.MedicalRecords.Plugins", "RunHistoryMzbl", out msg, pat);
                    }
                }

                return new FrameWork.Core.Common.RequestResult { Success = true };
            }
        }
}
