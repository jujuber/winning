using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.Kernel.Enum;
using System.Diagnostics;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    ///  集成调阅（新致）
    /// </summary>
    public class XinZhiIntegrationQueryCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                string _HT351 = GlobalVariable.HisApp.Config.Get("HT351").Trim();
                string url = _HT351;
                if (!string.IsNullOrWhiteSpace(_HT351))
                {
                    if (!_HT351.EndsWith("?"))
                    {
                        url = _HT351 + "?";
                    }
                    url = string.Format("{0}IsThirdLogin=1&UID=MZYSGZZ&Pid={1}&PType=1&ysgh={2}",
                        url, pat.Patid, GlobalVariable.DrInfoObj.sYsdm);
                    Process.Start(url);
                }
                else
                    GlobalVariable.HisApp.Prompt.Show("集成调阅地址[HT351]未设置，请联系系统管理员！", System.Windows.Forms.MessageBoxButtons.OK);

            }
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.XinZhiIntegrationQueryCommandID; }
        }
    }
}
