using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 家床 KWjcglxt
    /// </summary>
    public class JiachuangCommand:ICommand
    {
        public string ID
        {
            get { return "C0EE361D-5773-459D-87AF-3E931C6F5742"; }
        }

        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {            
            if (!File.Exists(Path.Combine(Application.StartupPath + "\\jiachuang\\", "kwMain.exe")))
            {
                GlobalVariable.HisApp.Prompt.Show(Application.StartupPath + "\\jiachuang\\" + "kwMain.exe文件不存在，请联系系统管理员！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    string[] arg = new string[1];
                    arg[0] = "KWjcglxt";                    
                    var p = new System.Diagnostics.Process();
                    p.StartInfo.WorkingDirectory = Application.StartupPath + "\\jiachuang\\";
                    p.StartInfo.FileName = "kwMain.exe";
                    p.StartInfo.Arguments = string.Join(",", arg);
                    p.Start();
                }
                catch (Exception ex)
                {
                    GlobalVariable.HisApp.Prompt.Show("调用家床的kwMain.exe报错：\r\n" + ex, MessageBoxButtons.OK);
                } 
                
            }
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

    }
}
