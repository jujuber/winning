using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.Outp.DAL.PatInfo.DataObject;


namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 住院单
    /// </summary>
    public class ZyzCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            Winning.FrameWork.Core.Common.RequestResult result = new Winning.FrameWork.Core.Common.RequestResult();
            PatBasicInfo pat = ContextValueHelper.GetPatientObj();
            object msg = null;
            GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Zydjd.dll", "Winning.Outp.UI.Zydjd.StartUp", "Run", out msg, pat);
            return result;
        }

        public string ID
        {
            get { return MenuCommandId.ZyzCommandID; }
        }
    }
}
