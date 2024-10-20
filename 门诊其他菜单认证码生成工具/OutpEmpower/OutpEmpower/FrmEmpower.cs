using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OutpEmpower
{
    public partial class FrmEmpower : Form
    {
        public FrmEmpower()
        {
            InitializeComponent();
        }
        public string GetEncrypt(string inputAnsi)
        {

            int Ansitemp;
            string AsiStr, rtnstr, strTemp, subTemp;
            AsiStr = string.Empty;
            rtnstr = "";
            strTemp = "";
            subTemp = "";
            //任务单号+医院名称+菜单名
            byte[] arraylist = System.Text.Encoding.Unicode.GetBytes(inputAnsi);
            for (int i = 0; i < arraylist.Length; i++)
            {
                Ansitemp = (int)arraylist[i];
                AsiStr += FormatAscii(Ansitemp.ToString());
            }
            if (!string.IsNullOrEmpty(AsiStr))
            {
                strTemp = AsiStr;
                while (strTemp.Length > 0)
                {
                    if (strTemp.Length == 1)
                    {
                        rtnstr += strTemp;
                        strTemp = string.Empty;
                    }
                    else
                    {
                        subTemp = strTemp.Substring(0, 2);
                        strTemp = strTemp.Substring(2, strTemp.Length - 2);
                        rtnstr += subTemp[subTemp.Length - 1].ToString() + subTemp[subTemp.Length - 2].ToString();
                    }
                }
            }
            return rtnstr;
        }
        public string FormatAscii(string formatstr)
        {
            int ii = formatstr.Length;
            if (ii < 3)
            {
                return "0" + formatstr;
            }
            else
            {
                return formatstr;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(edtNeedId.Text) || string.IsNullOrEmpty(edtHosptial.Text) || string.IsNullOrEmpty(edtMenuCaption.Text))
            {
                return;
            }
            string Oldstr = edtNeedId.Text.Trim() + edtHosptial.Text.Trim() + edtMenuCaption.Text.Trim();
            string NewStr = GetEncrypt(Oldstr);
            edtEmpowerId.Text = NewStr;
        }

        private void FrmEmpower_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.edtNeedId;
        }
    }
}
