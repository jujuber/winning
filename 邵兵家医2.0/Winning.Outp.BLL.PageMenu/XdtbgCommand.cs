using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.Kernel.Enum;
namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 85464 心电接口，需要调阅报告与心电图像
    /// 心电图报告
    /// </summary>
    public class XdtbgCommand : ICommand
    {

        public string ID
        {
            get { return "8DDA3E45-AA41-48BF-8CC8-D041B80481DD"; }
        }

        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            try
            {
                var pat = ContextValueHelper.GetPatientObj();
                if (pat == null)
                {
                    GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
                    return new FrameWork.Core.Common.RequestResult { Success = false };
                }
                else
                {
                    IniFile inifile = new IniFile(GlobalVariable.HisApp.Path.ApplicationPath + @"\Config\UrlConfig.ini");
                    string registPatUrl = inifile.IniReadValue("Xdtbg", "Type");

                    string strType = inifile.IniReadValue("Xdtbg", "Type");
                    if (strType.Equals("exe"))
                    {
                        string exePath =  inifile.IniReadValue("Xdtbg", "ExePath");
                        string exePara = inifile.IniReadValue("Xdtbg", "ExePara");

                        if (string.IsNullOrEmpty(exePath))
                        {
                            GlobalVariable.HisApp.Prompt.Show("请先在UrlConfig.ini中配置心电报告的exe路径！", System.Windows.Forms.MessageBoxButtons.OK);
                            return new FrameWork.Core.Common.RequestResult { Success = false };
                        }
                        else
                        {
                            System.Diagnostics.Process process = new System.Diagnostics.Process();
                            process.StartInfo = new System.Diagnostics.ProcessStartInfo();
                            process.StartInfo.FileName = exePath;
                            process.StartInfo.WorkingDirectory = GlobalVariable.HisApp.Path.ApplicationPath;
                            process.StartInfo.Arguments = exePath.Replace("@ghxh", pat.Ghxh.ToString()).Replace("@patid", pat.Patid.ToString()).Replace("@sfzh", pat.Sfzh?.Trim());
                            process.Start();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.HisApp.Prompt.Show("调用心电图报告异常！" + ex.Message, System.Windows.Forms.MessageBoxButtons.OK);
                return new FrameWork.Core.Common.RequestResult { Success = false };
            }

            return new FrameWork.Core.Common.RequestResult { Success = true };

        }
    }
}
