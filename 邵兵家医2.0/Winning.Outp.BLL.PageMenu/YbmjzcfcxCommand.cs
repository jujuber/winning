using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using System.Reflection;
using System.IO;
using Winning.FrameWork.Core.Common;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 医保门急诊处方查询
    /// </summary>
    public class YbmjzcfcxCommand : ICommand
    {
        public string ID
        {
            get { return "154F4F55-CEF4-4B18-A8B7-D00816C47D08"; }
        }

        public RequestResult Execute(object sender, EventArgs e)
        {
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                string strInXml =
                    string.Format(
                    @"<root>
	                    <jydm>5299</jydm>
	                    <sysid>5X</sysid>
	                    <dyly>0</dyly>
	                    <patid>{0}</patid>
	                    <jzxh>{1}</jzxh>
	                    <hzxm>{2}</hzxm>
	                    <sfzh>{3}</sfzh>
	                    <ysdm>{4}</ysdm>
	                    <ysmc>{5}</ysmc>
	                    <ksdm>{6}</ksdm>
	                    <ksmc>{7}</ksmc>
	                    <xmdm></xmdm>
	                    <xmmc></xmmc>
                    </root>", pat.Patid, pat.Ghxh, pat.Hzxm, (pat.Sfzh?.Trim() ?? "")
                    , GlobalVariable.DrInfoObj.sYsdm, GlobalVariable.DrInfoObj.sYsmc
                    , GlobalVariable.DrInfoObj.sDbKsdm, GlobalVariable.DrInfoObj.sDbKsmc);
                var ass = Assembly.LoadFile(Path.Combine(GlobalVariable.HisApp.Path.ApplicationPath, @"ThirdLib\Winning.Medicare.Mjzcfcx.Main.dll"));
                Type t = ass.GetType("Winning.Medicare.Mjzcfcx.Main.StartUp");
                object obj = Activator.CreateInstance(t);
                var methodInfo = t.GetMethod("WinningMedicareMW_OUTP", BindingFlags.Instance | BindingFlags.Public);
                var invokeArgs = new object[] { strInXml };
                methodInfo.Invoke(obj, invokeArgs);
            }
            return new RequestResult { Success = true };
        }
    }
}
