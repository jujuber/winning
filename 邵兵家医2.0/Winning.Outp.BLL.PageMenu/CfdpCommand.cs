using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.Kernel.Enum;
using Winning.Outp.DAL.PatInfo.DataObject;
using RequestResult = Winning.FrameWork.Core.Common.RequestResult;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 处方点评
    /// </summary>
    public class CfdpCommand : ICommand
    {
        public RequestResult Execute(object sender, EventArgs e)
        {
           // RequestResult result = new RequestResult();
            bool ret = true;
            if (GlobalVariable.RunAddin != AddinEnum.None)
            {
                GlobalVariable.HisApp.Prompt.Show("处方点评只可在病人列表界面使用！", MessageBoxButtons.OK);  //暂时这么处理---历史处方调用界面
            }
            else
            {
                PatBasicInfo pat = ContextValueHelper.GetPatientObj();
                //GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Cfdp.UI.Ysdpfk.dll", "Winning.Cfdp.UI.Ysdpfk.StartUp", "ShowMessage", out obj);
                try
                {
                    object ErrMsg = null;
                    ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Cfdp.UI.Ysdpfk.dll", "Winning.Cfdp.UI.Ysdpfk.StartUp", "ShowMzMessage", out ErrMsg, pat);
                }
                catch (Exception ex)
                {
                    GlobalVariable.HisApp.Prompt.Show("系统找不到：ShowMzMessage,Winning.Cfdp.UI.Ysdpfk.StartUp,Winning.Cfdp.UI.Ysdpfk.dll", MessageBoxButtons.OK);  //暂时这么处理---历史处方调用界面
                }
               
            }
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }

        public string ID
        {
            get { return MenuCommandId.CfdpCommandID; }
        }
    }
}
