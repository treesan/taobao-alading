namespace Alading
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txAccount = new DevExpress.XtraEditors.TextEdit();
            this.txPassword = new DevExpress.XtraEditors.TextEdit();
            this.hyperLinkEdit1 = new DevExpress.XtraEditors.HyperLinkEdit();
            this.hyperLinkEdit2 = new DevExpress.XtraEditors.HyperLinkEdit();
            this.btnConfig = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnLogin.Enabled = false;
            this.btnLogin.Location = new System.Drawing.Point(221, 197);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "登录";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(302, 197);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(109, 114);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "帐号：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(109, 152);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "密码：";
            // 
            // txAccount
            // 
            this.txAccount.EnterMoveNextControl = true;
            this.txAccount.Location = new System.Drawing.Point(151, 111);
            this.txAccount.Name = "txAccount";
            this.txAccount.Size = new System.Drawing.Size(145, 21);
            this.txAccount.TabIndex = 0;
            this.txAccount.EditValueChanged += new System.EventHandler(this.requiredField_EditValueChanged);
            // 
            // txPassword
            // 
            this.txPassword.EnterMoveNextControl = true;
            this.txPassword.Location = new System.Drawing.Point(151, 149);
            this.txPassword.Name = "txPassword";
            this.txPassword.Properties.PasswordChar = '*';
            this.txPassword.Size = new System.Drawing.Size(145, 21);
            this.txPassword.TabIndex = 1;
            this.txPassword.EditValueChanged += new System.EventHandler(this.requiredField_EditValueChanged);
            this.txPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txPassword_KeyDown);
            // 
            // hyperLinkEdit1
            // 
            this.hyperLinkEdit1.EditValue = "注册帐号";
            this.hyperLinkEdit1.Location = new System.Drawing.Point(315, 111);
            this.hyperLinkEdit1.Name = "hyperLinkEdit1";
            this.hyperLinkEdit1.Properties.Caption = "注册帐号";
            this.hyperLinkEdit1.Size = new System.Drawing.Size(62, 21);
            this.hyperLinkEdit1.TabIndex = 4;
            this.hyperLinkEdit1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.hyperLinkEdit1_MouseClick);
            // 
            // hyperLinkEdit2
            // 
            this.hyperLinkEdit2.EditValue = "提出建议";
            this.hyperLinkEdit2.Location = new System.Drawing.Point(315, 149);
            this.hyperLinkEdit2.Name = "hyperLinkEdit2";
            this.hyperLinkEdit2.Properties.Caption = "提出建议";
            this.hyperLinkEdit2.Size = new System.Drawing.Size(62, 21);
            this.hyperLinkEdit2.TabIndex = 5;
            this.hyperLinkEdit2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.hyperLinkEdit2_MouseClick);
            // 
            // btnConfig
            // 
            this.btnConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfig.Location = new System.Drawing.Point(12, 197);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(75, 23);
            this.btnConfig.TabIndex = 6;
            this.btnConfig.Text = "配置";
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            this.BackgroundImageStore = global::Alading.Properties.Resources.aldbeta;
            this.ClientSize = new System.Drawing.Size(395, 227);
            this.Controls.Add(this.hyperLinkEdit2);
            this.Controls.Add(this.hyperLinkEdit1);
            this.Controls.Add(this.txPassword);
            this.Controls.Add(this.txAccount);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.btnLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AladdinV2 公开测试版";
            ((System.ComponentModel.ISupportInitialize)(this.txAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txAccount;
        private DevExpress.XtraEditors.TextEdit txPassword;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEdit1;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEdit2;
        private DevExpress.XtraEditors.SimpleButton btnConfig;
    }
}