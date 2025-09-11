using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Winning.FrameWork.Core.Common;
using Winning.Outp.Core;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 浦人民入院申请查询
    /// </summary>
    public class Prmrysqcx : ICommand
    {
        public string ID
        {
            get { return "DCE25C87-0A15-427F-AE9B-4C4229673E13"; }
        }

        public RequestResult Execute(object sender, EventArgs e)
        {
            bool ret = false;
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {


                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                IniFile inifile = new IniFile(GlobalVariable.HisApp.Path.ApplicationPath + @"\Config\UrlConfig.ini");
                string registPatUrl = inifile.IniReadValue("Prmrysqdcx", "url");
                string jgdm = inifile.IniReadValue("Prmrysqdcx", "jgdm");
                string jgmc = inifile.IniReadValue("Prmrysqdcx", "jgmc");

                if (string.IsNullOrWhiteSpace(registPatUrl))
                {

                    GlobalVariable.HisApp.Prompt.Show("浦人民入院申请查询接口地址未配置，请检查UrlConfig内,Prmrysqdcx下的子节点url！", System.Windows.Forms.MessageBoxButtons.OK);
                    return new RequestResult { Success = ret };
                }
                string url = "";
                url = string.Format(registPatUrl, jgdm, jgmc);
                Helper.WriteLog("浦人民入院申请查询入参：" + url);
                //MessageBox.Show(url);
                Process.Start(url);
                //string result = Helper.HttpGet(url);
                //Helper.WriteLog("浦人民入院申请查询出参：" + result);

            }
            return new RequestResult { Success = ret };
        }
    }
}
