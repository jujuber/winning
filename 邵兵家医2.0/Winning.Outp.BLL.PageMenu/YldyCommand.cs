using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.Kernel.Enum;
using Winning.FrameWork.DAL.Kernel;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 医联调阅
    /// </summary>
    public class YldyCommand : ICommand
    {
        private static IAdoDb iSqlHt;
        internal static IAdoDb ISqlHt
        {
            get
            {
                if (iSqlHt == null)
                {
                    iSqlHt = SqlHelper.GetAdoDb();
                    iSqlHt.SqlConnect(SystemType.THIS4);
                }
                return iSqlHt;
            }
        }

        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            //GlobalVariable.HisSys.RunEvent(SystemType.HT, "Yldy");
            //return new FrameWork.Core.Common.RequestResult { Success=true};

            bool ret = false;
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {

                ISqlHt.GetDataTable(string.Format("exec usp_outp_yldydj '{0}','{1}','{2}','{3}'",
                   pat.Ghxh,
                   GlobalVariable.DrInfoObj.sYsdm.Trim(),
                   GlobalVariable.DrInfoObj.sYsmc,
                  DateTime.Now.ToString("yyyyMMdd HHmmss")));

                object ErrMsg = null;
                ret = GlobalVariable.HisSys.Reflected.RunNetAppEx("Winning.Outp.External.PDWS.Yldy.dll", "Winning.Outp.External.PDWS.Yldy.YlinfoMethod", "GetYlxxInfoForButton", out ErrMsg, pat);

            }
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }

        public string ID
        {
            get { return MenuCommandId.YldyCommandID; }
        }
    }
}
