using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.Kernel.Enum;
using Winning.Outp.DAL.PatInfo.DataObject;
using RequestResult = Winning.FrameWork.Core.Common.RequestResult;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 疑似院感
    /// </summary>
    public class YsygCommand : ICommand
    {
        public RequestResult Execute(object sender, EventArgs e)
        {
            RequestResult result = new RequestResult();
            PatBasicInfo pat = ContextValueHelper.GetPatientObj();
            //object msg = null;
            //GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.ZzdForKaixian.dll", "Winning.Outp.UI.ZzdForKaixian.StartUp", "Run", out msg, pat);
            //GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Cpoe.External.DiseasesReport.dll", "Winning.Cpoe.External.DiseasesReport.DiseasesReport", "YngrRep", out ErrMsg, "1");
            try
            {
                object msg = null;
                GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Cpoe.External.DiseasesReport.dll", "Winning.Cpoe.External.DiseasesReport.DiseasesReport", "YngrRep", out msg, pat);
         
            }
            catch (Exception ex)
            {
                GlobalVariable.HisApp.Prompt.Show("缺少Winning.Cpoe.External.DiseasesReport.dll !", System.Windows.Forms.MessageBoxButtons.OK);
            }

            return result;
        }

        public string ID
        {
            get { return MenuCommandId.YsygCommandID; }
        }
    }
}
