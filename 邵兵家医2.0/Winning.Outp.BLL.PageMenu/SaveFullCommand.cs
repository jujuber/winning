using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.Kernel.Enum;
using Winning.Outp.UI.JTYS.HealthStatus;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 全部保存
    /// </summary>
  public  class SaveFullCommand:ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {            
            object obj;
            GlobalVariable.HisSys.Reflected.RunNetAppEx("Winning.Outp.UI.Order.dll", "Winning.Outp.UI.Order.SaveEvent", "BeforeClickSaveBtn", out obj);
            //
            bool ret = GlobalVariable.HisSys.RunEvent(SystemType.HT, "SaveEvent");


             GlobalVariable.HisSys.Reflected.RunNetAppEx("Winning.Outp.External.BusiCoop.dll", "Winning.Outp.External.BusiCoop.YwxtObj", "Execute", out obj, 5);


            if (GlobalVariable.HisApp.Config.Get("HT420") == "是")
            {
                //Winning.Outp.UI.JTYS.HealthStatus.HealthStatus view = new HealthStatus();
                //view.RefreshMain();
                Winning.Outp.UI.JTYS.HealthStatus.HealthStatus.RefreshStatus();
            }
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }


        public string ID
        {
            get { return MenuCommandId.SaveFullCommandID; }
        }
    }
}
