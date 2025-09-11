using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.Kernel.Enum;
using Winning.FrameWork.IDAL;


namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 结束就诊
    /// </summary>
    public class PatJsjzCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object context, EventArgs e)
        {
            if (GlobalVariable.PatInfoObj.UpdatePatStatus_Jsjz_Msg(false, true) == true)
            {
                //此处暂且用反射，加个全局的来控制命令是否更好
                object msg = null;
                GlobalVariable.HisSys.Reflected.RunNetAppEx("Winning.Outp.UI.ControlCenter.dll", "Winning.Outp.UI.ControlCenter.DoctorWorkflow", "QxjzCommand", out msg);
            }

            SysLoger.Log("GDS病人结束就诊");
            DataHelper.DataObj.Execute<bool>("Winning.Outp.External.Gds", "GdsInterface", "EXIT", GlobalVariable.PatInfoObj.CurrPatinfo);
            SysLoger.Log("GDS病人结束就诊结束");

            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.PatJsjzCommandID; }
        }
    }
}
