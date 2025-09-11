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
    /// 体检
    /// </summary>
    public class TijianCommand:ICommand
    {
        public string ID
        {
            get { return "5A0254AF-1B63-46A9-B2D4-B49E8E918EF5"; }
        }

        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {         
            if (!File.Exists(Path.Combine(Application.StartupPath + "\\tijian\\", "WinningHis.exe")))
            {
                GlobalVariable.HisApp.Prompt.Show(Application.StartupPath + "\\tijian\\" + "WinningHis.exe文件不存在，请联系系统管理员！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {               
                try
                {                   
                    var p = new System.Diagnostics.Process();
                    p.StartInfo.WorkingDirectory = Application.StartupPath + "\\tijian\\";
                    p.StartInfo.FileName = "WinningHis.exe";
                    p.Start();
                }
                catch (Exception ex)
                {
                    GlobalVariable.HisApp.Prompt.Show("调用体检的WinningHis.exe报错：\r\n" + ex, MessageBoxButtons.OK);
                } 
                
            }            
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }
    }
}
