namespace Alading.Forms.Trade.Forms
{
    partial class TradeOperateDaily
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
            this.components = new System.ComponentModel.Container();
            this.txtCustomTid = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtTradeTitle = new DevExpress.XtraEditors.TextEdit();
            this.txtTitle = new DevExpress.XtraEditors.TextEdit();
            this.txtContent = new DevExpress.XtraEditors.MemoEdit();
            this.txtOperaterName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtDepartment = new DevExpress.XtraEditors.ComboBoxEdit();
            this.confirm = new DevExpress.XtraEditors.SimpleButton();
            this.canel = new DevExpress.XtraEditors.SimpleButton();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomTid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTradeTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperaterName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCustomTid
            // 
            this.txtCustomTid.Enabled = false;
            this.txtCustomTid.Location = new System.Drawing.Point(85, 12);
            this.txtCustomTid.Name = "txtCustomTid";
            this.txtCustomTid.Size = new System.Drawing.Size(213, 21);
            this.txtCustomTid.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 245);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "操作人：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "交易单号：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(36, 116);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "内容：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(36, 82);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "主题：";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 47);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 14);
            this.labelControl5.TabIndex = 5;
            this.labelControl5.Text = "交易名称：";
            // 
            // txtTradeTitle
            // 
            this.txtTradeTitle.Enabled = false;
            this.txtTradeTitle.Location = new System.Drawing.Point(85, 45);
            this.txtTradeTitle.Name = "txtTradeTitle";
            this.txtTradeTitle.Size = new System.Drawing.Size(213, 21);
            this.txtTradeTitle.TabIndex = 7;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(85, 79);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Properties.Mask.EditMask = "\\p{L}{30}";
            this.txtTitle.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtTitle.Size = new System.Drawing.Size(212, 21);
            this.txtTitle.TabIndex = 8;
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(85, 113);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(212, 82);
            this.txtContent.TabIndex = 9;
            // 
            // txtOperaterName
            // 
            this.txtOperaterName.Location = new System.Drawing.Point(85, 242);
            this.txtOperaterName.Name = "txtOperaterName";
            this.txtOperaterName.Properties.Mask.EditMask = "\\p{L}{20}";
            this.txtOperaterName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtOperaterName.Size = new System.Drawing.Size(212, 21);
            this.txtOperaterName.TabIndex = 10;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 211);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 14);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "操作环节：";
            // 
            // txtDepartment
            // 
            this.txtDepartment.Location = new System.Drawing.Point(85, 208);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDepartment.Properties.Items.AddRange(new object[] {
            "交易确认",
            "交易打印",
            "交易配货",
            "交易修改",
            "",
            ""});
            this.txtDepartment.Properties.MaxLength = 30;
            this.txtDepartment.Size = new System.Drawing.Size(212, 21);
            this.txtDepartment.TabIndex = 12;
            // 
            // confirm
            // 
            this.confirm.Image = global::Alading.Properties.Resources.order_finished;
            this.confirm.Location = new System.Drawing.Point(72, 283);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(72, 23);
            this.confirm.TabIndex = 13;
            this.confirm.Text = "确认";
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // canel
            // 
            this.canel.Image = global::Alading.Properties.Resources.shopdeleted;
            this.canel.Location = new System.Drawing.Point(186, 283);
            this.canel.Name = "canel";
            this.canel.Size = new System.Drawing.Size(70, 23);
            this.canel.TabIndex = 14;
            this.canel.Text = "取消";
            this.canel.Click += new System.EventHandler(this.canel_Click);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // TradeOperateDaily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 328);
            this.Controls.Add(this.canel);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.txtDepartment);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtOperaterName);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtTradeTitle);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtCustomTid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TradeOperateDaily";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加操作流程信息";
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomTid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTradeTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperaterName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtCustomTid;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtTradeTitle;
        private DevExpress.XtraEditors.TextEdit txtTitle;
        private DevExpress.XtraEditors.MemoEdit txtContent;
        private DevExpress.XtraEditors.TextEdit txtOperaterName;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.ComboBoxEdit txtDepartment;
        private DevExpress.XtraEditors.SimpleButton confirm;
        private DevExpress.XtraEditors.SimpleButton canel;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}