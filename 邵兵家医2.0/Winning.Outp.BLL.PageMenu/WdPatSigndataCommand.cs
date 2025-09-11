using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.DAL.Kernel;
using Winning.FrameWork.Kernel.Enum;
using Winning.Outp.Core;

namespace Winning.Outp.BLL.PageMenu
{
    public class WdPatSigndataCommand : ICommand
    {
        public string ID
        {
            get { return "2F374E36-32F0-46D6-B72B-9585BA3BB622"; }
        }
        LogWriter loger;
        public RequestResult Execute(object sender, EventArgs e)
        {
            bool ret = false;
            object ErrMsg = null;
            ret = GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.External.wdpatsign.dll", "Winning.Outp.External.wdpatsign.StartUp", "Run", out ErrMsg);
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }

    }
}
