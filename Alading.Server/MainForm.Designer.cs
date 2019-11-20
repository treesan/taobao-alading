namespace Alading.Server
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.wizardPage1 = new DevExpress.XtraWizard.WizardPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cbeDBType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.radioGroupType = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnTest = new DevExpress.XtraEditors.SimpleButton();
            this.textID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.notePanel = new DevExpress.Utils.Frames.NotePanel8_1();
            this.textPSW = new DevExpress.XtraEditors.TextEdit();
            this.textIP = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.wizardPage2 = new DevExpress.XtraWizard.WizardPage();
            this.txtDBAddress = new DevExpress.XtraEditors.ButtonEdit();
            this.notePanel8_11 = new DevExpress.Utils.Frames.NotePanel8_1();
            this.txtDBName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbeDBType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPSW.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textIP.Properties)).BeginInit();
            this.wizardPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.CancelText = "取消";
            this.wizardControl1.Controls.Add(this.welcomeWizardPage1);
            this.wizardControl1.Controls.Add(this.wizardPage1);
            this.wizardControl1.Controls.Add(this.completionWizardPage1);
            this.wizardControl1.Controls.Add(this.wizardPage2);
            this.wizardControl1.FinishText = "&完成";
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.NextText = "下一步 >";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomeWizardPage1,
            this.wizardPage1,
            this.wizardPage2,
            this.completionWizardPage1});
            this.wizardControl1.PreviousText = "< 上一步";
            this.wizardControl1.Text = "欢迎使用阿拉丁V2";
            this.wizardControl1.WizardStyle = DevExpress.XtraWizard.WizardStyle.WizardAero;
            this.wizardControl1.SelectedPageChanged += new DevExpress.XtraWizard.WizardPageChangedEventHandler(this.wizardControl1_SelectedPageChanged);
            this.wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_FinishClick);
            this.wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_CancelClick);
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.IntroductionText = "本安装向导将协助你部署初始化数据库到你本地的数据库引擎上\r\n";
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.ProceedText = "单击下一步继续...";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(580, 241);
            this.welcomeWizardPage1.Text = "本安装向导将协助你部署初始化数据库到你本地的数据库引擎上";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.panelControl1);
            this.wizardPage1.Cursor = System.Windows.Forms.Cursors.Default;
            this.wizardPage1.DescriptionText = "请部署本地数据库引擎配置";
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(580, 241);
            this.wizardPage1.Text = "部署配置";
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
            this.panelControl1.Controls.Add(this.textPSW);
            this.panelControl1.Controls.Add(this.textIP);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(580, 241);
            this.panelControl1.TabIndex = 6;
            // 
            // cbeDBType
            // 
            this.cbeDBType.EditValue = "SQLServer2008 Express";
            this.cbeDBType.Location = new System.Drawing.Point(178, 86);
            this.cbeDBType.Name = "cbeDBType";
            this.cbeDBType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbeDBType.Properties.Items.AddRange(new object[] {
            "SQLServer2008 Express",
            "SQLServer2008 Enterprise"});
            this.cbeDBType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbeDBType.Size = new System.Drawing.Size(306, 21);
            this.cbeDBType.TabIndex = 17;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(100, 89);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(72, 14);
            this.labelControl6.TabIndex = 16;
            this.labelControl6.Text = "数据库类别：";
            // 
            // radioGroupType
            // 
            this.radioGroupType.EditValue = "0";
            this.radioGroupType.Location = new System.Drawing.Point(178, 55);
            this.radioGroupType.Name = "radioGroupType";
            this.radioGroupType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "SQLServer认证"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "Windows认证")});
            this.radioGroupType.Size = new System.Drawing.Size(306, 25);
            this.radioGroupType.TabIndex = 15;
            this.radioGroupType.SelectedIndexChanged += new System.EventHandler(this.radioGroupType_SelectedIndexChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(112, 62);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 14);
            this.labelControl5.TabIndex = 13;
            this.labelControl5.Text = "验证模式：";
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(409, 189);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 12;
            this.btnTest.Text = "测试连接";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // textID
            // 
            this.textID.EditValue = "sa";
            this.textID.Location = new System.Drawing.Point(178, 140);
            this.textID.Name = "textID";
            this.textID.Properties.Mask.BeepOnError = true;
            this.textID.Properties.Mask.EditMask = "[a-zA-Z]+";
            this.textID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.textID.Size = new System.Drawing.Size(306, 21);
            this.textID.TabIndex = 11;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(100, 143);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "数据库帐号：";
            // 
            // notePanel
            // 
            this.notePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.notePanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notePanel.ForeColor = System.Drawing.Color.Black;
            this.notePanel.Location = new System.Drawing.Point(2, 2);
            this.notePanel.Name = "notePanel";
            this.notePanel.Size = new System.Drawing.Size(576, 33);
            this.notePanel.TabIndex = 2;
            this.notePanel.TabStop = false;
            this.notePanel.Text = "提示：阿拉丁V2公开测试版目前仅支持Sql Server 2008 Express版和Enterprise版数据库。";
            // 
            // textPSW
            // 
            this.textPSW.EditValue = "123456";
            this.textPSW.Location = new System.Drawing.Point(178, 167);
            this.textPSW.Name = "textPSW";
            this.textPSW.Properties.PasswordChar = '*';
            this.textPSW.Size = new System.Drawing.Size(306, 21);
            this.textPSW.TabIndex = 3;
            // 
            // textIP
            // 
            this.textIP.EditValue = "localhost";
            this.textIP.Location = new System.Drawing.Point(178, 113);
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(306, 21);
            this.textIP.TabIndex = 0;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(100, 170);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(72, 14);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "数据库密码：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(89, 116);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(83, 14);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "数据库IP地址：";
            // 
            // completionWizardPage1
            // 
            this.completionWizardPage1.FinishText = "已经将初始化数据库部署到本地数据库";
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.ProceedText = "单击完成按钮退出";
            this.completionWizardPage1.Size = new System.Drawing.Size(580, 241);
            this.completionWizardPage1.Text = "部署成功！";
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.txtDBAddress);
            this.wizardPage2.Controls.Add(this.notePanel8_11);
            this.wizardPage2.Controls.Add(this.txtDBName);
            this.wizardPage2.Controls.Add(this.labelControl8);
            this.wizardPage2.Controls.Add(this.labelControl3);
            this.wizardPage2.Controls.Add(this.marqueeProgressBarControl1);
            this.wizardPage2.Controls.Add(this.simpleButton1);
            this.wizardPage2.Controls.Add(this.labelControl7);
            this.wizardPage2.DescriptionText = "启动数据库部署";
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(580, 241);
            this.wizardPage2.Text = "启动数据库部署";
            // 
            // txtDBAddress
            // 
            this.txtDBAddress.Location = new System.Drawing.Point(138, 117);
            this.txtDBAddress.Name = "txtDBAddress";
            this.txtDBAddress.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDBAddress.Size = new System.Drawing.Size(252, 21);
            this.txtDBAddress.TabIndex = 23;
            this.txtDBAddress.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtDBAddress_ButtonClick);
            // 
            // notePanel8_11
            // 
            this.notePanel8_11.Dock = System.Windows.Forms.DockStyle.Top;
            this.notePanel8_11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notePanel8_11.ForeColor = System.Drawing.Color.Black;
            this.notePanel8_11.Location = new System.Drawing.Point(0, 0);
            this.notePanel8_11.Name = "notePanel8_11";
            this.notePanel8_11.Size = new System.Drawing.Size(580, 52);
            this.notePanel8_11.TabIndex = 20;
            this.notePanel8_11.TabStop = false;
            this.notePanel8_11.Text = "提示：数据库名称是阿拉丁V2存取数据的数据库名称，在阿拉丁登录配置中需要和这里的名称一致。数据库路径必须为数据库安装目录下的DATA文件夹，如X:\\Program" +
                " Files\\Microsoft SQL Server\\MSSQL10.SQLEXPRESS\\MSSQL\\DATA\\";
            // 
            // txtDBName
            // 
            this.txtDBName.EditValue = "Alading";
            this.txtDBName.Location = new System.Drawing.Point(138, 82);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Properties.Mask.BeepOnError = true;
            this.txtDBName.Properties.Mask.EditMask = "[a-zA-Z]+";
            this.txtDBName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtDBName.Size = new System.Drawing.Size(252, 21);
            this.txtDBName.TabIndex = 19;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(60, 120);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(72, 14);
            this.labelControl8.TabIndex = 18;
            this.labelControl8.Text = "数据库路径：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(60, 85);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 14);
            this.labelControl3.TabIndex = 18;
            this.labelControl3.Text = "数据库名称：";
            // 
            // marqueeProgressBarControl1
            // 
            this.marqueeProgressBarControl1.EditValue = 0;
            this.marqueeProgressBarControl1.Location = new System.Drawing.Point(138, 154);
            this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
            this.marqueeProgressBarControl1.Properties.Stopped = true;
            this.marqueeProgressBarControl1.Size = new System.Drawing.Size(340, 18);
            this.marqueeProgressBarControl1.TabIndex = 17;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(403, 111);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 15;
            this.simpleButton1.Text = "开始部署";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(72, 155);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(60, 14);
            this.labelControl7.TabIndex = 14;
            this.labelControl7.Text = "部署状态：";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 403);
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "部署初始化数据库";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbeDBType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPSW.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textIP.Properties)).EndInit();
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        private DevExpress.XtraWizard.WizardPage wizardPage1;
        private DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cbeDBType;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.RadioGroup radioGroupType;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btnTest;
        private DevExpress.XtraEditors.TextEdit textID;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.Utils.Frames.NotePanel8_1 notePanel;
        private DevExpress.XtraEditors.TextEdit textPSW;
        private DevExpress.XtraEditors.TextEdit textIP;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraWizard.WizardPage wizardPage2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtDBName;
        private DevExpress.Utils.Frames.NotePanel8_1 notePanel8_11;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private DevExpress.XtraEditors.ButtonEdit txtDBAddress;
    }
}

