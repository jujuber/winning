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
using System.IO;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    ///  临床调阅(cdr-portal)
    /// </summary>
    public class CDRPortalQueryCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            try
            {
                PatBasicInfo pat = ContextValueHelper.GetPatientObj();
                if (pat == null)
                {
                    GlobalVariable.HisApp.Prompt.Show("请选择一个病人！", MessageBoxButtons.OK);
                    return new FrameWork.Core.Common.RequestResult { Success = false };
                }
                if (!File.Exists(Path.Combine(Application.StartupPath, "Winning.Cdr.UI.Portal.dll")))
                {
                    GlobalVariable.HisApp.Prompt.Show("不存在接口Winning.Cdr.UI.Portal.dll文件，请联系系统管理员！", MessageBoxButtons.OK);
                }
                else
                {
                    object msg = null;
                    GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Cdr.UI.Portal.dll", "Winning.Cdr.UI.Portal.StartUp", "RunPortal", out msg,
                        pat.Ghxh, 0);
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.HisApp.Prompt.Show(ex.StackTrace, MessageBoxButtons.OK);
            }
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }
        public string ID
        {
            get { return MenuCommandId.PortalQueryCommandID; }
        }
    }
}
