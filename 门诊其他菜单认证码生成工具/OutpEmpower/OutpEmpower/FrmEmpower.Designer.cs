namespace OutpEmpower
{
    partial class FrmEmpower
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBxxx = new System.Windows.Forms.GroupBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.edtMenuCaption = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.edtNeedId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.edtHosptial = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxem = new System.Windows.Forms.GroupBox();
            this.edtEmpowerId = new System.Windows.Forms.TextBox();
            this.groupBxxx.SuspendLayout();
            this.groupBoxem.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBxxx
            // 
            this.groupBxxx.Controls.Add(this.btnOk);
            this.groupBxxx.Controls.Add(this.edtMenuCaption);
            this.groupBxxx.Controls.Add(this.label3);
            this.groupBxxx.Controls.Add(this.edtNeedId);
            this.groupBxxx.Controls.Add(this.label2);
            this.groupBxxx.Controls.Add(this.edtHosptial);
            this.groupBxxx.Controls.Add(this.label1);
            this.groupBxxx.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBxxx.Location = new System.Drawing.Point(0, 0);
            this.groupBxxx.Name = "groupBxxx";
            this.groupBxxx.Size = new System.Drawing.Size(679, 185);
            this.groupBxxx.TabIndex = 0;
            this.groupBxxx.TabStop = false;
            this.groupBxxx.Text = "基本信息";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(32, 138);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(131, 34);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "生   成";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // edtMenuCaption
            // 
            this.edtMenuCaption.Location = new System.Drawing.Point(104, 102);
            this.edtMenuCaption.Name = "edtMenuCaption";
            this.edtMenuCaption.Size = new System.Drawing.Size(348, 21);
            this.edtMenuCaption.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "菜单名";
            // 
            // edtNeedId
            // 
            this.edtNeedId.Location = new System.Drawing.Point(104, 35);
            this.edtNeedId.Name = "edtNeedId";
            this.edtNeedId.Size = new System.Drawing.Size(348, 21);
            this.edtNeedId.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "医院名称";
            // 
            // edtHosptial
            // 
            this.edtHosptial.Location = new System.Drawing.Point(104, 68);
            this.edtHosptial.Name = "edtHosptial";
            this.edtHosptial.Size = new System.Drawing.Size(348, 21);
            this.edtHosptial.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "任务单号";
            // 
            // groupBoxem
            // 
            this.groupBoxem.Controls.Add(this.edtEmpowerId);
            this.groupBoxem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxem.Location = new System.Drawing.Point(0, 185);
            this.groupBoxem.Name = "groupBoxem";
            this.groupBoxem.Size = new System.Drawing.Size(679, 424);
            this.groupBoxem.TabIndex = 1;
            this.groupBoxem.TabStop = false;
            this.groupBoxem.Text = "认证码";
            // 
            // edtEmpowerId
            // 
            this.edtEmpowerId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtEmpowerId.Location = new System.Drawing.Point(3, 17);
            this.edtEmpowerId.Multiline = true;
            this.edtEmpowerId.Name = "edtEmpowerId";
            this.edtEmpowerId.ReadOnly = true;
            this.edtEmpowerId.Size = new System.Drawing.Size(673, 404);
            this.edtEmpowerId.TabIndex = 5;
            // 
            // FrmEmpower
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 609);
            this.Controls.Add(this.groupBoxem);
            this.Controls.Add(this.groupBxxx);
            this.Name = "FrmEmpower";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "认证码生成工具";
            this.Load += new System.EventHandler(this.FrmEmpower_Load);
            this.groupBxxx.ResumeLayout(false);
            this.groupBxxx.PerformLayout();
            this.groupBoxem.ResumeLayout(false);
            this.groupBoxem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBxxx;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox edtMenuCaption;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox edtNeedId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edtHosptial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxem;
        private System.Windows.Forms.TextBox edtEmpowerId;
    }
}

