using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.Outp.UI.JTYS.GetChssForm;
using System.Windows.Forms;

namespace Winning.Outp.BLL.PageMenu
{
    public class SignedQueryCommand:ICommand
    {
        public string ID
        {
            get { return MenuCommandId.SignedQueryCommandID; }
        }



        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            bool ret = false;
            //var pat = ContextValueHelper.GetPatientObj();
            var pat = GlobalVariable.PatInfoObj.CurrPatinfo;
            if (GlobalVariable.PatInfoObj.CurrPatinfo == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    Winning.Outp.UI.JTYS.GetChssForm.GetChssForm chssService = new GetChssForm();
                    string result = chssService.GetYjyjyqy("JTYS_YJYJYQY");
                    if (result != "")
                    {
                        //form1.MdiParent = SysHelper.GetHisSystem().OnlineShell;
                        //form1.ShowDialog();
                    }
                    ret = true;
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }
    }
}
