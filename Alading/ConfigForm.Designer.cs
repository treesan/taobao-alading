namespace Alading
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.notePanel = new DevExpress.Utils.Frames.NotePanel8_1();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.textIP = new DevExpress.XtraEditors.TextEdit();
            this.textDBName = new DevExpress.XtraEditors.TextEdit();
            this.textPSW = new DevExpress.XtraEditors.TextEdit();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cbeDBType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.radioGroupType = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnTest = new DevExpress.XtraEditors.SimpleButton();
            this.textID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.textIP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDBName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPSW.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbeDBType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // notePanel
            // 
            this.notePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.notePanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notePanel.ForeColor = System.Drawing.Color.Black;
            this.notePanel.Location = new System.Drawing.Point(2, 2);
            this.notePanel.Name = "notePanel";
            this.notePanel.Size = new System.Drawing.Size(389, 38);
            this.notePanel.TabIndex = 2;
            this.notePanel.TabStop = false;
            this.notePanel.Text = "提示：阿拉丁V2公开测试版目前仅支持Sql Server 2008 Express版和Enterprise版数据库。";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(42, 107);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(71, 14);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "数据库IP地址";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(53, 134);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "数据库名称";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(53, 161);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "数据库密码";
            // 
            // textIP
            // 
            this.textIP.EditValue = "localhost";
            this.textIP.Location = new System.Drawing.Point(119, 104);
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(224, 21);
            this.textIP.TabIndex = 0;
            // 
            // textDBName
            // 
            this.textDBName.EditValue = "Aladdin";
            this.textDBName.Location = new System.Drawing.Point(119, 131);
            this.textDBName.Name = "textDBName";
            this.textDBName.Size = new System.Drawing.Size(76, 21);
            this.textDBName.TabIndex = 2;
            // 
            // textPSW
            // 
            this.textPSW.EditValue = "123456";
            this.textPSW.Location = new System.Drawing.Point(119, 158);
            this.textPSW.Name = "textPSW";
            this.textPSW.Properties.PasswordChar = '*';
            this.textPSW.Size = new System.Drawing.Size(224, 21);
            this.textPSW.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(187, 190);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(268, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.cbeDBType);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.radioGroupType);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.btnTest);
            this.panelControl1.Controls.Add(this.textID);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.notePanel);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.btnOk);
            this.panelControl1.Controls.Add(this.textPSW);
            this.panelControl1.Controls.Add(this.textDBName);
            this.panelControl1.Controls.Add(this.textIP);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(393, 225);
            this.panelControl1.TabIndex = 5;
            // 
            // cbeDBType
            // 
            this.cbeDBType.EditValue = "SQLServer2008 Express";
            this.cbeDBType.Location = new System.Drawing.Point(119, 77);
            this.cbeDBType.Name = "cbeDBType";
            this.cbeDBType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbeDBType.Properties.Items.AddRange(new object[] {
            "SQLServer2008 Express",
            "SQLServer2008 Enterprise"});
            this.cbeDBType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbeDBType.Size = new System.Drawing.Size(224, 21);
            this.cbeDBType.TabIndex = 17;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(53, 80);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 14);
            this.labelControl6.TabIndex = 16;
            this.labelControl6.Text = "数据库类别";
            // 
            // radioGroupType
            // 
            this.radioGroupType.EditValue = "0";
            this.radioGroupType.Location = new System.Drawing.Point(119, 46);
            this.radioGroupType.Name = "radioGroupType";
            this.radioGroupType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "SQLServer认证"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "Windows认证")});
            this.radioGroupType.Size = new System.Drawing.Size(224, 25);
            this.radioGroupType.TabIndex = 15;
            this.radioGroupType.SelectedIndexChanged += new System.EventHandler(this.radioGroupType_SelectedIndexChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(65, 53);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 13;
            this.labelControl5.Text = "验证模式";
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(38, 190);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 12;
            this.btnTest.Text = "测试连接";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // textID
            // 
            this.textID.EditValue = "sa";
            this.textID.Location = new System.Drawing.Point(267, 131);
            this.textID.Name = "textID";
            this.textID.Size = new System.Drawing.Size(76, 21);
            this.textID.TabIndex = 11;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(202, 134);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "数据库帐号";
            // 
            // ConfigForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 225);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库配置";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textIP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDBName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPSW.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbeDBType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Frames.NotePanel8_1 notePanel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit textIP;
        private DevExpress.XtraEditors.TextEdit textDBName;
        private DevExpress.XtraEditors.TextEdit textPSW;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit textID;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnTest;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.RadioGroup radioGroupType;
        private DevExpress.XtraEditors.ComboBoxEdit cbeDBType;
        private DevExpress.XtraEditors.LabelControl labelControl6;

    }
}