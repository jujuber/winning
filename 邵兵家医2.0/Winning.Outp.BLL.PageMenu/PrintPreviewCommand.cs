using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.Kernel.Enum;
//using Winning.Outp.External.PwjjZHSF;


namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 打印预览
    /// </summary>
    public class PrintPreviewCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            if (GlobalVariable.HisSys.OnlineShell.ActiveMdiChild == null)
                return new FrameWork.Core.Common.RequestResult { Success = true };
            if (GlobalVariable.HisSys.OnlineShell.ActiveMdiChild.Text.Trim() == "处方录入"
                || GlobalVariable.HisSys.OnlineShell.ActiveMdiChild.Text.Trim() == "打印预览"
                || GlobalVariable.HisSys.OnlineShell.ActiveMdiChild.Text.Trim() == "门诊病历"
                || GlobalVariable.HisSys.OnlineShell.ActiveMdiChild.Text.Trim() == "病历编辑")
            {
                //不保存好数据不允许打印的判断
                if (!CheckHasUnSaveData())
                    return new FrameWork.Core.Common.RequestResult { Success = true };             
                ProcessPrint();
               
            }
            else
            {
                GlobalVariable.HisApp.Prompt.Show("【打印预览】按钮不可放置于当前界面！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }
        private void ProcessPrint()
        {
            //ZHSFPwjjImplement zhsf = new ZHSFPwjjImplement();
            //bool res = zhsf.QueryCheckRecipeState(GlobalVariable.OrderObj.Helper.CurrRecipeNo.ToString(), GlobalVariable.PatInfoObj.CurrPatinfo.Ghxh.ToString());
            //if (!res)
            //{
            //    //MessageBox.Show("处方未审核或未通过，不能打印！");
            //    return;
            //}
            GlobalVariable.Receptacle.SwitchAddinsIdx(AddinEnum.Print);
            GlobalVariable.Receptacle.RunCurrAddinsMethod(MethodType.Open);
        }
        public string ID
        {
            get { return MenuCommandId.PrintPreviewCommandID; }
        }

        private bool CheckHasUnSaveData()
        {
            
            if (GlobalVariable.Receptacle.PlugIns.Exists(p => p.IsSave))
            {
                if (GlobalVariable.HisApp.Config.Get("HT307").Trim() != "是")
                {
                    for (int i = 0; i < GlobalVariable.Receptacle.PlugIns.Count; i++)
                    {
                        IPlugIns plug = GlobalVariable.Receptacle.PlugIns[i];
                        if (plug.IsSave)
                        {
                            if (GlobalVariable.HisApp.Prompt.Show(null, string.Format("当前病人【{0}】信息需要保存,是否保存？", plug.Title), MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                GlobalVariable.Receptacle.SwitchAddinsIdx(plug.AddInsType);
                                if (!GlobalVariable.Receptacle.RunCurrAddinsMethod(MethodType.Save))
                                    return false;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    if (GlobalVariable.HisApp.Prompt.Show(null, "当前病人有就诊信息需要保存,是否保存？", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        if (GlobalVariable.Receptacle.RunAddinsMethod(MethodType.Save))
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            return true;
        }
    }
}
