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
    public class ServiceInCommand : ICommand
    {
        public string ID
        {
            get { return MenuCommandId.ServiceInCommandID; }
        }



        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
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
                    //Form childForm = null;
                    //foreach (Control item in this.Controls)
                    //{
                    //    if (item is Form)
                    //    {
                    //        childForm = item as Form;
                    //        break;
                    //    }
                    //}
                    //if (childForm != null)
                    //{
                    //    this.Controls.Remove(childForm);
                    //    this.Tag = null;
                    //}

                    Winning.Outp.UI.JTYS.GetChssForm.GetChssForm chssService = new GetChssForm();
                    Form form1 = chssService.GetForm("JTYS_FWDJ");
                    if (form1 != null)
                    {
                        //form1.MdiParent = SysHelper.GetHisSystem().OnlineShell;
                        form1.ShowDialog();
                    }
                    //this.Controls.Add(form1);
                    //this.Tag = form1;
                    //this.Show();
                    ret = true;
                }
                catch
                {

                }
            }
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }
    }
}
