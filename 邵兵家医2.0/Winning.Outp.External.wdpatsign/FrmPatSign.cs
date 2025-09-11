using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Winning.Outp.Core;
using Winning.Outp.DAL.PatInfo.DataObject;
using Winning.Outp.External.wdpatsign.Model;

namespace Winning.Outp.External.wdpatsign
{
    public partial class FrmPatSign : Form
    {
        public FrmPatSign()
        {
            InitializeComponent();
            this.tableLayoutPanel1.GetType()
                            .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                            .SetValue(this.tableLayoutPanel1, true, null);
        }
        public List<CellDTO> CellXys { get; set; } = new List<CellDTO>();
        public List<CellDTO> CellSgs { get; set; } = new List<CellDTO>();
        public List<CellDTO> CellXts { get; set; } = new List<CellDTO>();

        public PatBasicInfo Patient { get; set; }

        private void initcell()
        {
            CellXys.Clear();
            CellXys.Add(new CellDTO { IRow = 0, ICol = 0, Capture = "平均收缩压" });
            CellXys.Add(new CellDTO { IRow = 0, ICol = 1, Capture = "平均舒张压" });
            CellXys.Add(new CellDTO { IRow = 0, ICol = 2, Capture = "血压测量侧" });
            CellXys.Add(new CellDTO { IRow = 1, ICol = 0, Capture = "第一次收缩压" });
            CellXys.Add(new CellDTO { IRow = 1, ICol = 1, Capture = "第一次舒张压" });
            CellXys.Add(new CellDTO { IRow = 1, ICol = 2, Capture = "第二次收缩压" });
            CellXys.Add(new CellDTO { IRow = 2, ICol = 0, Capture = "第二次舒张压" });
            CellXys.Add(new CellDTO { IRow = 2, ICol = 1, Capture = "第三次收缩压" });
            CellXys.Add(new CellDTO { IRow = 2, ICol = 2, Capture = "第三次舒张压" });
            CellXys.Add(new CellDTO { IRow = 3, ICol = 0, Capture = "平均脉率" });
            CellXys.Add(new CellDTO { IRow = 3, ICol = 1, Capture = "第一次脉率值" });
            CellXys.Add(new CellDTO { IRow = 3, ICol = 2, Capture = "第二次脉率值" });
            CellXys.Add(new CellDTO { IRow = 4, ICol = 0, Capture = "第三次脉率值" });
            CellXys.Add(new CellDTO { IRow = 4, ICol = 1, Capture = "第一次不规则脉搏" });
            CellXys.Add(new CellDTO { IRow = 4, ICol = 2, Capture = "第二次不规则脉搏" });
            CellXys.Add(new CellDTO { IRow = 5, ICol = 0, Capture = "第三次不规则脉搏" });
            CellXys.Add(new CellDTO { IRow = 5, ICol = 1, Capture = "第一次手臂移动" });
            CellXys.Add(new CellDTO { IRow = 5, ICol = 2, Capture = "第二次手臂移动" });
            CellXys.Add(new CellDTO { IRow = 6, ICol = 0, Capture = "第三次手臂移动" });
            CellXys.Add(new CellDTO { IRow = 6, ICol = 1, Capture = "第一次测量时间" });
            CellXys.Add(new CellDTO { IRow = 6, ICol = 2, Capture = "第二次测量时间" });
            CellXys.Add(new CellDTO { IRow = 7, ICol = 0, Capture = "是否血压异常" });
            CellXys.Add(new CellDTO { IRow = 7, ICol = 1, Capture = "是否危急值血压" });
            CellXys.Add(new CellDTO { IRow = 7, ICol = 2, Capture = "是否已服降压药" });
            CellXys.Add(new CellDTO { IRow = 8, ICol = 0, Capture = "是否已休息至少 5 分钟" });

            CellSgs.Clear();
            CellSgs.Add(new CellDTO { IRow = 0, ICol = 0, Capture = "身高值" });
            CellSgs.Add(new CellDTO { IRow = 0, ICol = 1, Capture = "体重值" });

            CellXts.Clear();
            CellXts.Add(new CellDTO { IRow = 0, ICol = 0, Capture = "血糖值" });
            CellXts.Add(new CellDTO { IRow = 0, ICol = 1, Capture = "单位" });
            CellXts.Add(new CellDTO { IRow = 0, ICol = 2, Capture = "参考范围" });
            CellXts.Add(new CellDTO { IRow = 1, ICol = 0, Capture = "异常提示代码" });
            CellXts.Add(new CellDTO { IRow = 1, ICol = 1, Capture = "血糖类型" });
            CellXts.Add(new CellDTO { IRow = 1, ICol = 2, Capture = "测量途径" });
            CellXts.Add(new CellDTO { IRow = 2, ICol = 0, Capture = "是否危急值血糖" });
            CellXts.Add(new CellDTO { IRow = 2, ICol = 1, Capture = "是否患者" });
        }

        private void PanelClear()
        {
            foreach (var item in this.panel1.Controls)
            {
                if (item is TextBox)
                {
                    (item as TextBox).Clear();
                }
            }
            foreach (var item in this.panel2.Controls)
            {
                if (item is TextBox)
                {
                    (item as TextBox).Clear();
                }
            }
        }
        private void SetHead(data data)
        {
            this.edtPersoncard.Text = data.personcard;
            this.edtName.Text = Patient.Hzxm;// data.name;
            this.edtGender.Text = Patient.Sex;// data.gender;
            this.edtBirth.Text = Patient.Birth;// data.birth;
            this.edtMeasureSourceId.Text = data.measureSourceId;
            this.edtMeasureLocation.Text = data.measureLocation;
            this.edtMeasureOrgId.Text = data.measureOrgId;
            this.edtMeasureMode.Text = data.measureMode;
            this.edtDeviceId.Text = data.deviceId;
            this.edtDeviceType.Text = data.deviceType;
            this.edtMeasureDoc.Text = data.measureDoc;
            this.edtNetworkStatus.Text = data.networkStatus;
        }
        private void SetValue(data data,List<CellDTO> listcel)
        {
            PanelClear();
            SetHead(data);
            var strdata = data.measureData;

            var arrdata = strdata.Split('|');
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.Controls.Clear();
            int i = 0;
            foreach (var item in listcel)
            {
                LabelText labText = new LabelText();
                labText.Location = new System.Drawing.Point(3, 3);
                labText.Size = new System.Drawing.Size(170, 26);
                labText.LabCapture = item.Capture;
                labText.BoxValue = arrdata[i];
                item.Value = arrdata[i];

                labText.Dock = DockStyle.Fill;
                i++;
                this.tableLayoutPanel1.Controls.Add(labText, item.ICol, item.IRow);
            }
            this.tableLayoutPanel1.ResumeLayout(true);
        }

        private void SetGroupBoxText(string code)
        {
            switch (code)
            {
                case "1001":
                    this.groupBox2.Text = "【血压】测量结果信息";
                    break;
                case "1003":
                    this.groupBox2.Text = "【身高】测量结果信息";
                    break;
                case "2001":
                    this.groupBox2.Text = "【血糖】测量结果信息";
                    break;
                default:
                    this.groupBox2.Text = "测量结果信息";
                    break;
            }
        }

        private void radioButton_CheckedChaged(object sender, EventArgs e)
        {
            var current = sender as RadioButton;
            if (current.Name.StartsWith("xy_"))
            { 
                SetValue(current.Tag as data,CellXys);
            }
            else if (current.Name.StartsWith("sg_"))
            {
                SetValue(current.Tag as data, CellSgs);
            }
            else if (current.Name.StartsWith("xt_"))
            {
                SetValue(current.Tag as data, CellXts);
            }
        }

        private void FrmPatSign_Load(object sender, EventArgs e)
        {
            initcell();
            this.dtpBegin.Value = System.DateTime.Now;
            this.dtpEnd.Value = System.DateTime.Now;
            button1.PerformClick();
        }
        /// <summary>
        /// 血压
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var data = PatSignService.GetData("1001", Patient, this.dtpBegin.Value, this.dtpEnd.Value);
            if (data.total == "" || data.total == "0")
            {
                return;
            }
            this.flowLayoutPanel1.Controls.Clear();
            foreach (var item in data.dataList)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Text = $"测量时间：{item.measureTime}";
                radioButton.Width = 250;
                radioButton.Tag = item;
                radioButton.Name = $"xy_" + System.DateTime.Now.Ticks;
                radioButton.CheckedChanged += new EventHandler(radioButton_CheckedChaged);
                if (this.flowLayoutPanel1.Controls.Count == 0)
                {
                    radioButton.Checked = true;
                }
                this.flowLayoutPanel1.Controls.Add(radioButton);
            }
            SetGroupBoxText("1001");
            //textBox1.Text= PatSignService.GetData("1001", Patient);
        }
        /// <summary>
        /// 血糖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            var data = PatSignService.GetData("2001", Patient, this.dtpBegin.Value, this.dtpEnd.Value);
            if (data.total == "" || data.total == "0")
            {
                return;
            }
            this.flowLayoutPanel1.Controls.Clear();
            foreach (var item in data.dataList)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Text = $"测量时间：{item.measureTime}";
                radioButton.Width = 250;
                radioButton.Tag = item;
                radioButton.Name = $"xt_" + System.DateTime.Now.Ticks;
                radioButton.CheckedChanged += new EventHandler(radioButton_CheckedChaged);
                if (this.flowLayoutPanel1.Controls.Count == 0)
                {
                    radioButton.Checked = true;
                }
                this.flowLayoutPanel1.Controls.Add(radioButton);
            }
            SetGroupBoxText("2001");
            //textBox1.Text = PatSignService.GetData("2001", Patient);
        }
        /// <summary>
        /// 身高
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            var data = PatSignService.GetData("1003", Patient, this.dtpBegin.Value, this.dtpEnd.Value);
            if (data.total == "" || data.total == "0")
            {
                return;
            }
            this.flowLayoutPanel1.Controls.Clear();
            foreach (var item in data.dataList)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Text = $"测量时间：{item.measureTime}";
                radioButton.Width = 250;
                radioButton.Tag = item;
                radioButton.Name = $"sg_" + System.DateTime.Now.Ticks;
                radioButton.CheckedChanged += new EventHandler(radioButton_CheckedChaged);
                if (this.flowLayoutPanel1.Controls.Count == 0)
                {
                    radioButton.Checked = true;
                }
                this.flowLayoutPanel1.Controls.Add(radioButton);
            }
            SetGroupBoxText("1003");
            // textBox1.Text = PatSignService.GetData("1003", Patient);
        }
    }
}
