namespace Alading.Forms.Item
{
    partial class ItemsDown
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.progressBarTotal = new DevExpress.XtraEditors.ProgressBarControl();
            this.listBoxDetail = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.spinEditThreadCount = new DevExpress.XtraEditors.SpinEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.checkEditIsUpdate = new DevExpress.XtraEditors.CheckEdit();
            this.cmbNick = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnDowmItems = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnRetry = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditThreadCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditIsUpdate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNick.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.ContentImage = global::Alading.Properties.Resources.wizard_image;
            this.panelControl1.ContentImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(666, 341);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.progressBarTotal);
            this.groupControl2.Controls.Add(this.listBoxDetail);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Location = new System.Drawing.Point(206, 69);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(454, 267);
            this.groupControl2.TabIndex = 16;
            this.groupControl2.Text = "下载详情";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(18, 31);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 11;
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
            this.progressBarTotal.Size = new System.Drawing.Size(389, 18);
            this.progressBarTotal.TabIndex = 7;
            // 
            // listBoxDetail
            // 
            this.listBoxDetail.HorizontalScrollbar = true;
            this.listBoxDetail.Location = new System.Drawing.Point(60, 55);
            this.listBoxDetail.Name = "listBoxDetail";
            this.listBoxDetail.Size = new System.Drawing.Size(389, 207);
            this.listBoxDetail.TabIndex = 12;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 146);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "详细信息";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.spinEditThreadCount);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.checkEditIsUpdate);
            this.groupControl1.Controls.Add(this.cmbNick);
            this.groupControl1.Location = new System.Drawing.Point(206, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(454, 58);
            this.groupControl1.TabIndex = 15;
            this.groupControl1.Text = "下载设置";
            // 
            // spinEditThreadCount
            // 
            this.spinEditThreadCount.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEditThreadCount.Location = new System.Drawing.Point(247, 26);
            this.spinEditThreadCount.Name = "spinEditThreadCount";
            this.spinEditThreadCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditThreadCount.Properties.IsFloatValue = false;
            this.spinEditThreadCount.Properties.Mask.EditMask = "N00";
            this.spinEditThreadCount.Properties.MaxValue = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.spinEditThreadCount.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditThreadCount.Size = new System.Drawing.Size(42, 21);
            this.spinEditThreadCount.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(201, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 14);
            this.label7.TabIndex = 30;
            this.label7.Text = "线程数";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "选择店铺";
            // 
            // checkEditIsUpdate
            // 
            this.checkEditIsUpdate.EditValue = true;
            this.checkEditIsUpdate.Location = new System.Drawing.Point(305, 26);
            this.checkEditIsUpdate.Name = "checkEditIsUpdate";
            this.checkEditIsUpdate.Properties.Caption = "更新本地已存在的宝贝";
            this.checkEditIsUpdate.Size = new System.Drawing.Size(149, 19);
            this.checkEditIsUpdate.TabIndex = 3;
            // 
            // cmbNick
            // 
            this.cmbNick.Location = new System.Drawing.Point(62, 26);
            this.cmbNick.Name = "cmbNick";
            this.cmbNick.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNick.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbNick.Size = new System.Drawing.Size(133, 21);
            this.cmbNick.TabIndex = 13;
            this.cmbNick.EditValueChanged += new System.EventHandler(this.cmbNick_EditValueChanged);
            // 
            // btnDowmItems
            // 
            this.btnDowmItems.Enabled = false;
            this.btnDowmItems.Location = new System.Drawing.Point(206, 347);
            this.btnDowmItems.Name = "btnDowmItems";
            this.btnDowmItems.Size = new System.Drawing.Size(77, 23);
            this.btnDowmItems.TabIndex = 1;
            this.btnDowmItems.Text = "开始下载";
            this.btnDowmItems.Click += new System.EventHandler(this.btnDowmItems_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(289, 347);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消下载";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRetry
            // 
            this.btnRetry.Enabled = false;
            this.btnRetry.Location = new System.Drawing.Point(494, 347);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(77, 23);
            this.btnRetry.TabIndex = 5;
            this.btnRetry.Text = "下载失败宝贝";
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(577, 347);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(77, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ItemsDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 385);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRetry);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDowmItems);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ItemsDown";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "下载店铺宝贝";
            this.Load += new System.EventHandler(this.ItemsDown_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditThreadCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditIsUpdate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNick.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ProgressBarControl progressBarTotal;
        private DevExpress.XtraEditors.SimpleButton btnDowmItems;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ListBoxControl listBoxDetail;
        private DevExpress.XtraEditors.CheckEdit checkEditIsUpdate;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbNick;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SpinEdit spinEditThreadCount;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.SimpleButton btnRetry;
        private DevExpress.XtraEditors.SimpleButton btnClose;
    }
}