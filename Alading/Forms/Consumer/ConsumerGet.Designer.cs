namespace Alading.Forms.Consumer
{
    partial class ConsumerGet
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
            this.total_process = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.progressBarCurrent = new DevExpress.XtraEditors.ProgressBarControl();
            this.ConsumerDown = new DevExpress.XtraEditors.ListBoxControl();
            this.progressBarTotal = new DevExpress.XtraEditors.ProgressBarControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.spinEditThreadCount = new DevExpress.XtraEditors.SpinEdit();
            this.dateEditBegin = new DevExpress.XtraEditors.DateEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.dateEditEnd = new DevExpress.XtraEditors.DateEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSelectTime = new DevExpress.XtraEditors.ComboBoxEdit();
            this.checkedShopList = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownConsumer = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarCurrent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConsumerDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditThreadCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBegin.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBegin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSelectTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedShopList.Properties)).BeginInit();
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
            this.panelControl1.Size = new System.Drawing.Size(696, 362);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.total_process);
            this.groupControl2.Controls.Add(this.label5);
            this.groupControl2.Controls.Add(this.label6);
            this.groupControl2.Controls.Add(this.progressBarCurrent);
            this.groupControl2.Controls.Add(this.ConsumerDown);
            this.groupControl2.Controls.Add(this.progressBarTotal);
            this.groupControl2.Location = new System.Drawing.Point(209, 107);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(478, 250);
            this.groupControl2.TabIndex = 47;
            this.groupControl2.Text = "下载详情";
            // 
            // total_process
            // 
            this.total_process.AutoSize = true;
            this.total_process.Location = new System.Drawing.Point(5, 23);
            this.total_process.Name = "total_process";
            this.total_process.Size = new System.Drawing.Size(43, 14);
            this.total_process.TabIndex = 38;
            this.total_process.Text = "总进度";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 14);
            this.label5.TabIndex = 36;
            this.label5.Text = "当前进度";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 14);
            this.label6.TabIndex = 45;
            this.label6.Text = "详细信息";
            // 
            // progressBarCurrent
            // 
            this.progressBarCurrent.Location = new System.Drawing.Point(63, 50);
            this.progressBarCurrent.Name = "progressBarCurrent";
            this.progressBarCurrent.Properties.ShowTitle = true;
            this.progressBarCurrent.Size = new System.Drawing.Size(410, 18);
            this.progressBarCurrent.TabIndex = 37;
            // 
            // ConsumerDown
            // 
            this.ConsumerDown.HorizontalScrollbar = true;
            this.ConsumerDown.Location = new System.Drawing.Point(63, 74);
            this.ConsumerDown.Name = "ConsumerDown";
            this.ConsumerDown.Size = new System.Drawing.Size(410, 171);
            this.ConsumerDown.TabIndex = 44;
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Location = new System.Drawing.Point(63, 26);
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Properties.ShowTitle = true;
            this.progressBarTotal.Properties.Step = 1;
            this.progressBarTotal.Size = new System.Drawing.Size(410, 18);
            this.progressBarTotal.TabIndex = 39;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.spinEditThreadCount);
            this.groupControl1.Controls.Add(this.dateEditBegin);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.dateEditEnd);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.cmbSelectTime);
            this.groupControl1.Controls.Add(this.checkedShopList);
            this.groupControl1.Location = new System.Drawing.Point(209, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(478, 96);
            this.groupControl1.TabIndex = 46;
            this.groupControl1.Text = "下载设置";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(337, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 14);
            this.label4.TabIndex = 35;
            this.label4.Text = "选择时间";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 14);
            this.label1.TabIndex = 30;
            this.label1.Text = "从";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 14);
            this.label2.TabIndex = 31;
            this.label2.Text = "到";
            // 
            // spinEditThreadCount
            // 
            this.spinEditThreadCount.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEditThreadCount.Location = new System.Drawing.Point(291, 31);
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
            this.spinEditThreadCount.Size = new System.Drawing.Size(40, 21);
            this.spinEditThreadCount.TabIndex = 43;
            // 
            // dateEditBegin
            // 
            this.dateEditBegin.EditValue = null;
            this.dateEditBegin.Location = new System.Drawing.Point(63, 67);
            this.dateEditBegin.Name = "dateEditBegin";
            this.dateEditBegin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBegin.Properties.ReadOnly = true;
            this.dateEditBegin.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditBegin.Size = new System.Drawing.Size(173, 21);
            this.dateEditBegin.TabIndex = 32;
            this.dateEditBegin.EditValueChanged += new System.EventHandler(this.dateEditBegin_EditValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(242, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 14);
            this.label7.TabIndex = 42;
            this.label7.Text = "线程数";
            // 
            // dateEditEnd
            // 
            this.dateEditEnd.EditValue = null;
            this.dateEditEnd.Location = new System.Drawing.Point(291, 67);
            this.dateEditEnd.Name = "dateEditEnd";
            this.dateEditEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEnd.Properties.ReadOnly = true;
            this.dateEditEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditEnd.Size = new System.Drawing.Size(182, 21);
            this.dateEditEnd.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 41;
            this.label3.Text = "选择店铺";
            // 
            // cmbSelectTime
            // 
            this.cmbSelectTime.EditValue = "近15天";
            this.cmbSelectTime.Location = new System.Drawing.Point(398, 31);
            this.cmbSelectTime.Name = "cmbSelectTime";
            this.cmbSelectTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSelectTime.Properties.Items.AddRange(new object[] {
            "近15天",
            "自定义",
            "开店至今"});
            this.cmbSelectTime.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbSelectTime.Size = new System.Drawing.Size(75, 21);
            this.cmbSelectTime.TabIndex = 34;
            this.cmbSelectTime.SelectedIndexChanged += new System.EventHandler(this.cmbSelectTime_SelectedIndexChanged);
            // 
            // checkedShopList
            // 
            this.checkedShopList.Location = new System.Drawing.Point(63, 31);
            this.checkedShopList.Name = "checkedShopList";
            this.checkedShopList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.checkedShopList.Size = new System.Drawing.Size(173, 21);
            this.checkedShopList.TabIndex = 40;
            this.checkedShopList.EditValueChanged += new System.EventHandler(this.checkedShopList_EditValueChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(290, 363);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消下载";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDownConsumer
            // 
            this.btnDownConsumer.Enabled = false;
            this.btnDownConsumer.Location = new System.Drawing.Point(209, 363);
            this.btnDownConsumer.Name = "btnDownConsumer";
            this.btnDownConsumer.Size = new System.Drawing.Size(75, 23);
            this.btnDownConsumer.TabIndex = 4;
            this.btnDownConsumer.Text = "开始下载";
            this.btnDownConsumer.Click += new System.EventHandler(this.btnDownOrders_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(607, 363);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ConsumerGet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 397);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDownConsumer);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConsumerGet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "同步淘宝客户";
            this.Load += new System.EventHandler(this.ConsumerGet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarCurrent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConsumerDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditThreadCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBegin.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBegin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSelectTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedShopList.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SpinEdit spinEditThreadCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.CheckedComboBoxEdit checkedShopList;
        private DevExpress.XtraEditors.ProgressBarControl progressBarTotal;
        private System.Windows.Forms.Label total_process;
        private DevExpress.XtraEditors.ProgressBarControl progressBarCurrent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSelectTime;
        private DevExpress.XtraEditors.DateEdit dateEditEnd;
        private DevExpress.XtraEditors.DateEdit dateEditBegin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnDownConsumer;
        private DevExpress.XtraEditors.ListBoxControl ConsumerDown;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnClose;
    }
}