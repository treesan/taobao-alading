namespace Alading.Forms.Stock.SettingUp
{
    partial class StockCatAdd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textEditCid = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textCatName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEditFatherCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEditFatherName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.simpleCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleConfirm = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCatName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditFatherCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditFatherName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textEditCid
            // 
            this.textEditCid.Location = new System.Drawing.Point(97, 93);
            this.textEditCid.Name = "textEditCid";
            this.textEditCid.Size = new System.Drawing.Size(168, 21);
            this.textEditCid.TabIndex = 22;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(34, 96);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 21;
            this.labelControl3.Text = "分类编码：";
            // 
            // textCatName
            // 
            this.textCatName.Location = new System.Drawing.Point(97, 66);
            this.textCatName.Name = "textCatName";
            this.textCatName.Size = new System.Drawing.Size(168, 21);
            this.textCatName.TabIndex = 20;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(34, 69);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 19;
            this.labelControl2.Text = "分类名称：";
            // 
            // textEditFatherCode
            // 
            this.textEditFatherCode.Location = new System.Drawing.Point(97, 39);
            this.textEditFatherCode.Name = "textEditFatherCode";
            this.textEditFatherCode.Properties.ReadOnly = true;
            this.textEditFatherCode.Size = new System.Drawing.Size(168, 21);
            this.textEditFatherCode.TabIndex = 26;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(22, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.TabIndex = 25;
            this.labelControl1.Text = "父类目编码：";
            // 
            // textEditFatherName
            // 
            this.textEditFatherName.Location = new System.Drawing.Point(97, 12);
            this.textEditFatherName.Name = "textEditFatherName";
            this.textEditFatherName.Properties.ReadOnly = true;
            this.textEditFatherName.Size = new System.Drawing.Size(168, 21);
            this.textEditFatherName.TabIndex = 24;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(22, 15);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(72, 14);
            this.labelControl4.TabIndex = 23;
            this.labelControl4.Text = "父类目名称：";
            // 
            // simpleCancel
            // 
            this.simpleCancel.Location = new System.Drawing.Point(167, 127);
            this.simpleCancel.Name = "simpleCancel";
            this.simpleCancel.Size = new System.Drawing.Size(60, 23);
            this.simpleCancel.TabIndex = 28;
            this.simpleCancel.Text = "返回";
            this.simpleCancel.Click += new System.EventHandler(this.simpleCancel_Click);
            // 
            // simpleConfirm
            // 
            this.simpleConfirm.Location = new System.Drawing.Point(67, 127);
            this.simpleConfirm.Name = "simpleConfirm";
            this.simpleConfirm.Size = new System.Drawing.Size(60, 23);
            this.simpleConfirm.TabIndex = 27;
            this.simpleConfirm.Text = "确定";
            this.simpleConfirm.Click += new System.EventHandler(this.simpleConfirm_Click);
            // 
            // StockCatAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 164);
            this.Controls.Add(this.simpleCancel);
            this.Controls.Add(this.simpleConfirm);
            this.Controls.Add(this.textEditFatherCode);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.textEditFatherName);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.textEditCid);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.textCatName);
            this.Controls.Add(this.labelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StockCatAdd";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增类目";
            ((System.ComponentModel.ISupportInitialize)(this.textEditCid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCatName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditFatherCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditFatherName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEditCid;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textCatName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEditFatherCode;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEditFatherName;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton simpleCancel;
        private DevExpress.XtraEditors.SimpleButton simpleConfirm;
    }
}