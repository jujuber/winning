using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Winning.FrameWork.Core.Common;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.Outp.UI.JTYS.GetChssForm;
using RequestResult = Winning.FrameWork.Core.Common.RequestResult;

namespace Winning.Outp.BLL.PageMenu
{
    public class AssistantServiceCommand : ICommand
    {
        public RequestResult Execute(object sender, EventArgs e)
        {
            bool ret = false;
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    Winning.Outp.UI.JTYS.GetChssForm.GetChssForm chssService = new GetChssForm();
                    Form form1 = chssService.GetForm("JTYS_ZLFW");
                    if (form1 != null)
                    {
                        form1.ShowDialog();
                    }
                    ret = true;
                }
                catch
                {

                }
            }
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }

        public string ID { get { return MenuCommandId.AssistantServiceCommandID; } }
    }
}
