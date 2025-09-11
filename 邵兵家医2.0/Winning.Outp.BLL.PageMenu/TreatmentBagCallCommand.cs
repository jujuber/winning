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
    /// 诊疗包
    /// </summary>
    public class TreatmentBagCallCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {

            if (GlobalVariable.RunAddin == AddinEnum.None)
            {
                GlobalVariable.HisApp.Prompt.Show("诊疗包只可在就诊界面使用！", MessageBoxButtons.OK);  //暂时这么处理---诊疗调用界面

            }
            else
            {
                object msg = null;
                bool ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Package.dll", "Winning.Outp.UI.Package.StartUp", "Run", out msg);

            } return new FrameWork.Core.Common.RequestResult { Success = true };
        }


        public string ID
        {
            get { return MenuCommandId.TreatmentBagCallCommandID; }
        }
    }
}
