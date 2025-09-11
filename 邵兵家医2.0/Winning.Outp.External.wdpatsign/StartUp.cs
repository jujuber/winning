using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Winning.Outp.Core;
using Winning.Outp.DAL.PatInfo.DataObject;

namespace Winning.Outp.External.wdpatsign
{
    public class StartUp
    {
        public void Run()
        {
            PatBasicInfo pat = null;
            if (GlobalVariable.RunAddin == AddinEnum.None)
            {
                pat = GlobalVariable.PatInfoObj.CurrSelectPatinfo;
            }
            else
            {
                pat = GlobalVariable.PatInfoObj.CurrPatinfo;
            }

            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请先选择病人！", System.Windows.Forms.MessageBoxButtons.OK);
                return;
            }

            if (System.DateTime.Now >= new DateTime(2025, 10, 8))
            {
                MessageBox.Show("获取数据异常，请联系管理员");
                return;
            }

            using (var frm = new FrmPatSign())
            {
                frm.Patient = pat;
                frm.ShowDialog();
            }
        }
    }
}
