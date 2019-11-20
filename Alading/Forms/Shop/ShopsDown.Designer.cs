namespace Alading.Forms.Shop
{
    partial class ShopsDown
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
            this.panelCtrl = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.progressBarTotal = new DevExpress.XtraEditors.ProgressBarControl();
            this.listBoxDetail = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cmbNick = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnDowmShops = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelCtrl)).BeginInit();
            this.panelCtrl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNick.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelCtrl
            // 
            this.panelCtrl.ContentImage = global::Alading.Properties.Resources.wizard_image;
            this.panelCtrl.ContentImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.panelCtrl.Controls.Add(this.groupControl2);
            this.panelCtrl.Controls.Add(this.groupControl1);
            this.panelCtrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCtrl.Location = new System.Drawing.Point(0, 0);
            this.panelCtrl.Name = "panelCtrl";
            this.panelCtrl.Size = new System.Drawing.Size(629, 308);
            this.panelCtrl.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.progressBarTotal);
            this.groupControl2.Controls.Add(this.listBoxDetail);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Location = new System.Drawing.Point(207, 68);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(417, 235);
            this.groupControl2.TabIndex = 24;
            this.groupControl2.Text = "下载信息";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 35);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 18;
            this.labelControl3.Text = "总进度";
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Location = new System.Drawing.Point(60, 31);
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.progressBarTotal.Properties.ShowTitle = true;
            this.progressBarTotal.Properties.StartColor = System.Drawing.SystemColors.HotTrack;
            this.progressBarTotal.Properties.Step = 1;
            this.progressBarTotal.Size = new System.Drawing.Size(350, 18);
            this.progressBarTotal.TabIndex = 16;
            // 
            // listBoxDetail
            // 
            this.listBoxDetail.HorizontalScrollbar = true;
            this.listBoxDetail.Location = new System.Drawing.Point(60, 59);
            this.listBoxDetail.Name = "listBoxDetail";
            this.listBoxDetail.Size = new System.Drawing.Size(350, 171);
            this.listBoxDetail.TabIndex = 19;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 129);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 17;
            this.labelControl2.Text = "详细信息";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cmbNick);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(207, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(417, 57);
            this.groupControl1.TabIndex = 23;
            this.groupControl1.Text = "下载设置";
            // 
            // cmbNick
            // 
            this.cmbNick.Location = new System.Drawing.Point(60, 30);
            this.cmbNick.Name = "cmbNick";
            this.cmbNick.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNick.Size = new System.Drawing.Size(350, 21);
            this.cmbNick.TabIndex = 22;
            this.cmbNick.EditValueChanged += new System.EventHandler(this.cmbNick_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "选择店铺";
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(288, 314);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消下载";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDowmShops
            // 
            this.btnDowmShops.Enabled = false;
            this.btnDowmShops.Location = new System.Drawing.Point(207, 314);
            this.btnDowmShops.Name = "btnDowmShops";
            this.btnDowmShops.Size = new System.Drawing.Size(75, 23);
            this.btnDowmShops.TabIndex = 3;
            this.btnDowmShops.Text = "开始下载";
            this.btnDowmShops.Click += new System.EventHandler(this.btnDowmShops_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(542, 314);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ShopsDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 341);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDowmShops);
            this.Controls.Add(this.panelCtrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShopsDown";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "下载店铺信息";
            this.Load += new System.EventHandler(this.ShopsDown_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelCtrl)).EndInit();
            this.panelCtrl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNick.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelCtrl;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnDowmShops;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ListBoxControl listBoxDetail;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ProgressBarControl progressBarTotal;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbNick;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnClose;
    }
}