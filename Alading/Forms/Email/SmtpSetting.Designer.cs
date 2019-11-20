namespace Alading.Forms.Email
{
    partial class SmtpSetting
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txSmtp = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txAccount = new DevExpress.XtraEditors.TextEdit();
            this.txPassword = new DevExpress.XtraEditors.TextEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.spThread = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.spTry = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.spPort = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.spTimeout = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.spRetryInterval = new DevExpress.XtraEditors.SpinEdit();
            this.spSendInterval = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txSmtp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spThread.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTimeout.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spRetryInterval.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spSendInterval.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(137, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "发送邮件服务器(SMTP)：";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(12, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "服务器信息";
            // 
            // txSmtp
            // 
            this.txSmtp.EnterMoveNextControl = true;
            this.txSmtp.Location = new System.Drawing.Point(155, 31);
            this.txSmtp.Name = "txSmtp";
            this.txSmtp.Size = new System.Drawing.Size(193, 21);
            this.txSmtp.TabIndex = 0;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(12, 88);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "登录信息";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 115);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(40, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "用户名:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 142);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 14);
            this.labelControl5.TabIndex = 5;
            this.labelControl5.Text = "密码：";
            // 
            // txAccount
            // 
            this.txAccount.EnterMoveNextControl = true;
            this.txAccount.Location = new System.Drawing.Point(155, 112);
            this.txAccount.Name = "txAccount";
            this.txAccount.Size = new System.Drawing.Size(193, 21);
            this.txAccount.TabIndex = 2;
            // 
            // txPassword
            // 
            this.txPassword.EnterMoveNextControl = true;
            this.txPassword.Location = new System.Drawing.Point(155, 139);
            this.txPassword.Name = "txPassword";
            this.txPassword.Properties.PasswordChar = '*';
            this.txPassword.Size = new System.Drawing.Size(193, 21);
            this.txPassword.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(192, 351);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(273, 351);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(12, 173);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(52, 14);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "后台信息";
            // 
            // spThread
            // 
            this.spThread.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spThread.EnterMoveNextControl = true;
            this.spThread.Location = new System.Drawing.Point(155, 194);
            this.spThread.Name = "spThread";
            this.spThread.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spThread.Properties.MaxValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spThread.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spThread.Size = new System.Drawing.Size(193, 21);
            this.spThread.TabIndex = 4;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(12, 197);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(132, 14);
            this.labelControl7.TabIndex = 12;
            this.labelControl7.Text = "后台发送邮件线程数量：";
            // 
            // spTry
            // 
            this.spTry.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spTry.EnterMoveNextControl = true;
            this.spTry.Location = new System.Drawing.Point(155, 275);
            this.spTry.Name = "spTry";
            this.spTry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spTry.Properties.MaxValue = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.spTry.Size = new System.Drawing.Size(193, 21);
            this.spTry.TabIndex = 6;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(12, 278);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(120, 14);
            this.labelControl8.TabIndex = 14;
            this.labelControl8.Text = "失败时重复尝试次数：";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(12, 61);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(48, 14);
            this.labelControl9.TabIndex = 15;
            this.labelControl9.Text = "端口号：";
            // 
            // spPort
            // 
            this.spPort.EditValue = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.spPort.EnterMoveNextControl = true;
            this.spPort.Location = new System.Drawing.Point(155, 58);
            this.spPort.Name = "spPort";
            this.spPort.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spPort.Properties.MaxValue = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.spPort.Size = new System.Drawing.Size(193, 21);
            this.spPort.TabIndex = 1;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(12, 224);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(108, 14);
            this.labelControl10.TabIndex = 16;
            this.labelControl10.Text = "链接超时（毫秒）：";
            // 
            // spTimeout
            // 
            this.spTimeout.EditValue = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.spTimeout.EnterMoveNextControl = true;
            this.spTimeout.Location = new System.Drawing.Point(155, 221);
            this.spTimeout.Name = "spTimeout";
            this.spTimeout.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spTimeout.Properties.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.spTimeout.Properties.MaxValue = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.spTimeout.Properties.MinValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.spTimeout.Size = new System.Drawing.Size(193, 21);
            this.spTimeout.TabIndex = 5;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(12, 305);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(132, 14);
            this.labelControl11.TabIndex = 17;
            this.labelControl11.Text = "重试时间间隔（毫秒）：";
            // 
            // spRetryInterval
            // 
            this.spRetryInterval.EditValue = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.spRetryInterval.EnterMoveNextControl = true;
            this.spRetryInterval.Location = new System.Drawing.Point(155, 302);
            this.spRetryInterval.Name = "spRetryInterval";
            this.spRetryInterval.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spRetryInterval.Properties.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spRetryInterval.Properties.MaxValue = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.spRetryInterval.Properties.MinValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spRetryInterval.Size = new System.Drawing.Size(193, 21);
            this.spRetryInterval.TabIndex = 7;
            // 
            // spSendInterval
            // 
            this.spSendInterval.EditValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spSendInterval.Location = new System.Drawing.Point(155, 248);
            this.spSendInterval.Name = "spSendInterval";
            this.spSendInterval.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spSendInterval.Properties.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.spSendInterval.Properties.MaxValue = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.spSendInterval.Properties.MinValue = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.spSendInterval.Size = new System.Drawing.Size(193, 21);
            this.spSendInterval.TabIndex = 18;
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(12, 251);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(132, 14);
            this.labelControl12.TabIndex = 19;
            this.labelControl12.Text = "发送时间间隔（毫秒）：";
            // 
            // SmtpSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 388);
            this.Controls.Add(this.labelControl12);
            this.Controls.Add(this.spSendInterval);
            this.Controls.Add(this.spRetryInterval);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.spTimeout);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.spPort);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.spTry);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.spThread);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txPassword);
            this.Controls.Add(this.txAccount);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txSmtp);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "SmtpSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "发送邮件设置";
            ((System.ComponentModel.ISupportInitialize)(this.txSmtp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spThread.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTimeout.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spRetryInterval.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spSendInterval.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txSmtp;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txAccount;
        private DevExpress.XtraEditors.TextEdit txPassword;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SpinEdit spThread;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SpinEdit spTry;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.SpinEdit spPort;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.SpinEdit spTimeout;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.SpinEdit spRetryInterval;
        private DevExpress.XtraEditors.SpinEdit spSendInterval;
        private DevExpress.XtraEditors.LabelControl labelControl12;
    }
}