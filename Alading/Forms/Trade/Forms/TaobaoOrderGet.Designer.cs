namespace Alading.Forms.Trade.Forms
{
    partial class TaobaoOrderGet
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
            this.btnDownOrders = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.OrderDetail = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.total_process = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.OrderDown = new DevExpress.XtraEditors.ListBoxControl();
            this.progressBarCurrent = new DevExpress.XtraEditors.ProgressBarControl();
            this.progressBarTotal = new DevExpress.XtraEditors.ProgressBarControl();
            this.label6 = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cmbSelectTime = new DevExpress.XtraEditors.ComboBoxEdit();
            this.spinEditThreadCount = new DevExpress.XtraEditors.SpinEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dateEditBegin = new DevExpress.XtraEditors.DateEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.dateEditEnd = new DevExpress.XtraEditors.DateEdit();
            this.checkedShopList = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.BtnFailedOrder = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.OrderDetail)).BeginInit();
            this.OrderDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrderDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarCurrent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSelectTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditThreadCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBegin.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBegin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedShopList.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDownOrders
            // 
            this.btnDownOrders.Location = new System.Drawing.Point(210, 381);
            this.btnDownOrders.Name = "btnDownOrders";
            this.btnDownOrders.Size = new System.Drawing.Size(80, 23);
            this.btnDownOrders.TabIndex = 0;
            this.btnDownOrders.Text = "开始下载";
            this.btnDownOrders.Click += new System.EventHandler(this.btnDownOrders_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(296, 381);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消下载";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // OrderDetail
            // 
            this.OrderDetail.ContentImage = global::Alading.Properties.Resources.wizard_image;
            this.OrderDetail.ContentImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.OrderDetail.Controls.Add(this.groupControl2);
            this.OrderDetail.Controls.Add(this.groupControl1);
            this.OrderDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.OrderDetail.Location = new System.Drawing.Point(0, 0);
            this.OrderDetail.Name = "OrderDetail";
            this.OrderDetail.Size = new System.Drawing.Size(694, 375);
            this.OrderDetail.TabIndex = 2;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.total_process);
            this.groupControl2.Controls.Add(this.label5);
            this.groupControl2.Controls.Add(this.OrderDown);
            this.groupControl2.Controls.Add(this.progressBarCurrent);
            this.groupControl2.Controls.Add(this.progressBarTotal);
            this.groupControl2.Controls.Add(this.label6);
            this.groupControl2.Location = new System.Drawing.Point(210, 115);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(479, 255);
            this.groupControl2.TabIndex = 31;
            this.groupControl2.Text = "下载信息";
            // 
            // total_process
            // 
            this.total_process.AutoSize = true;
            this.total_process.Location = new System.Drawing.Point(16, 26);
            this.total_process.Name = "total_process";
            this.total_process.Size = new System.Drawing.Size(43, 14);
            this.total_process.TabIndex = 21;
            this.total_process.Text = "总进度";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 14);
            this.label5.TabIndex = 16;
            this.label5.Text = "当前进度";
            // 
            // OrderDown
            // 
            this.OrderDown.HorizontalScrollbar = true;
            this.OrderDown.Location = new System.Drawing.Point(67, 82);
            this.OrderDown.Name = "OrderDown";
            this.OrderDown.Size = new System.Drawing.Size(403, 168);
            this.OrderDown.TabIndex = 28;
            // 
            // progressBarCurrent
            // 
            this.progressBarCurrent.Location = new System.Drawing.Point(65, 54);
            this.progressBarCurrent.Name = "progressBarCurrent";
            this.progressBarCurrent.Properties.ShowTitle = true;
            this.progressBarCurrent.Size = new System.Drawing.Size(405, 18);
            this.progressBarCurrent.TabIndex = 18;
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Location = new System.Drawing.Point(65, 26);
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Properties.ShowTitle = true;
            this.progressBarTotal.Size = new System.Drawing.Size(405, 18);
            this.progressBarTotal.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 14);
            this.label6.TabIndex = 19;
            this.label6.Text = "详细信息";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cmbSelectTime);
            this.groupControl1.Controls.Add(this.spinEditThreadCount);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.dateEditBegin);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.dateEditEnd);
            this.groupControl1.Controls.Add(this.checkedShopList);
            this.groupControl1.Location = new System.Drawing.Point(210, 9);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(479, 100);
            this.groupControl1.TabIndex = 30;
            this.groupControl1.Text = "下载设置";
            // 
            // cmbSelectTime
            // 
            this.cmbSelectTime.EditValue = "近15天";
            this.cmbSelectTime.Location = new System.Drawing.Point(390, 30);
            this.cmbSelectTime.Name = "cmbSelectTime";
            this.cmbSelectTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSelectTime.Properties.Items.AddRange(new object[] {
            "近15天",
            "自定义",
            "开店至今"});
            this.cmbSelectTime.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbSelectTime.Size = new System.Drawing.Size(80, 21);
            this.cmbSelectTime.TabIndex = 13;
            this.cmbSelectTime.SelectedIndexChanged += new System.EventHandler(this.cmbSelectTime_SelectedIndexChanged);
            // 
            // spinEditThreadCount
            // 
            this.spinEditThreadCount.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEditThreadCount.Location = new System.Drawing.Point(285, 30);
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
            this.spinEditThreadCount.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "从";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 14);
            this.label2.TabIndex = 10;
            this.label2.Text = "到";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(329, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 14);
            this.label4.TabIndex = 14;
            this.label4.Text = "选择时间";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(241, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 14);
            this.label7.TabIndex = 27;
            this.label7.Text = "线程数";
            // 
            // dateEditBegin
            // 
            this.dateEditBegin.EditValue = null;
            this.dateEditBegin.Enabled = false;
            this.dateEditBegin.Location = new System.Drawing.Point(65, 64);
            this.dateEditBegin.Name = "dateEditBegin";
            this.dateEditBegin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBegin.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditBegin.Size = new System.Drawing.Size(170, 21);
            this.dateEditBegin.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 25;
            this.label3.Text = "选择店铺";
            // 
            // dateEditEnd
            // 
            this.dateEditEnd.EditValue = null;
            this.dateEditEnd.Enabled = false;
            this.dateEditEnd.Location = new System.Drawing.Point(285, 64);
            this.dateEditEnd.Name = "dateEditEnd";
            this.dateEditEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditEnd.Size = new System.Drawing.Size(185, 21);
            this.dateEditEnd.TabIndex = 12;
            // 
            // checkedShopList
            // 
            this.checkedShopList.Location = new System.Drawing.Point(65, 30);
            this.checkedShopList.Name = "checkedShopList";
            this.checkedShopList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.checkedShopList.Size = new System.Drawing.Size(170, 21);
            this.checkedShopList.TabIndex = 24;
            this.checkedShopList.EditValueChanged += new System.EventHandler(this.checkedShopList_EditValueChanged);
            // 
            // BtnFailedOrder
            // 
            this.BtnFailedOrder.Location = new System.Drawing.Point(510, 381);
            this.BtnFailedOrder.Name = "BtnFailedOrder";
            this.BtnFailedOrder.Size = new System.Drawing.Size(80, 23);
            this.BtnFailedOrder.TabIndex = 3;
            this.BtnFailedOrder.Text = "下载失败订单";
            this.BtnFailedOrder.Click += new System.EventHandler(this.BtnFailedOrder_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(600, 381);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // TaobaoOrderGet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 412);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.BtnFailedOrder);
            this.Controls.Add(this.OrderDetail);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDownOrders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaobaoOrderGet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "同步淘宝客户";
            this.Load += new System.EventHandler(this.TaobaoOrderGet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OrderDetail)).EndInit();
            this.OrderDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrderDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarCurrent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSelectTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditThreadCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBegin.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBegin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedShopList.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnDownOrders;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl OrderDetail;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSelectTime;
        private DevExpress.XtraEditors.DateEdit dateEditEnd;
        private DevExpress.XtraEditors.DateEdit dateEditBegin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.ProgressBarControl progressBarCurrent;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.ProgressBarControl progressBarTotal;
        private System.Windows.Forms.Label total_process;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.CheckedComboBoxEdit checkedShopList;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.ListBoxControl OrderDown;
        private DevExpress.XtraEditors.SpinEdit spinEditThreadCount;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraEditors.SimpleButton BtnFailedOrder;
        private DevExpress.XtraEditors.SimpleButton btnClose;

    }
}