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
    /// 浦人民入院通知单
    /// </summary>
    public class Prmrytzd : ICommand
    {
        public string ID
        {
            get { return "29CDE77C-0394-49F0-8E48-0B6AC7CB854F"; }
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
                string registPatUrl = inifile.IniReadValue("Prmrytzd", "url");
                string jgdm = inifile.IniReadValue("Prmrytzd", "jgdm");
                string jgmc = inifile.IniReadValue("Prmrytzd", "jgmc");

                if (string.IsNullOrWhiteSpace(registPatUrl))
                {

                    GlobalVariable.HisApp.Prompt.Show("浦人民入院通知单接口地址未配置，请检查UrlConfig内,Prmrytzd下的子节点url！", System.Windows.Forms.MessageBoxButtons.OK);
                    return new RequestResult { Success = ret };
                }
                string url = "";
                //url=http:/10.220.72.78:8060/#/?community-ghxh={0}&community-hospitalid={1}community-hospitalname={2}&community-cardno={3}
                url = string.Format(registPatUrl, pat.Ghxh, jgdm, jgmc,pat.Cardno);
                Helper.WriteLog("浦人民入院通知单入参：" + url);
                //MessageBox.Show("浦人民入院通知单：" + url);
                Process.Start(url);
                //string result = Helper.HttpGet(url);
                //Helper.WriteLog("浦人民入院通知单出参：" + result);

            }
            return new RequestResult { Success = ret };
        }
    }
}
