namespace Alading.Forms.PropValue
{
    partial class DownloadPropValue
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
            this.btnRetry = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.popupContainerEditTBCat = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerControlItemCat = new DevExpress.XtraEditors.PopupContainerControl();
            this.treeListItemCat = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.spinEditThreadCount = new DevExpress.XtraEditors.SpinEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.listBoxCtrl = new DevExpress.XtraEditors.ListBoxControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.progressBarTotal = new DevExpress.XtraEditors.ProgressBarControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnItemCat = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.backWorker = new System.ComponentModel.BackgroundWorker();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.progressBarCurrent = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEditTBCat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControlItemCat)).BeginInit();
            this.popupContainerControlItemCat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListItemCat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditThreadCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxCtrl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarCurrent.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRetry
            // 
            this.btnRetry.Enabled = false;
            this.btnRetry.Location = new System.Drawing.Point(467, 366);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(93, 23);
            this.btnRetry.TabIndex = 4;
            this.btnRetry.Text = "下载失败类属性";
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.popupContainerEditTBCat);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.spinEditThreadCount);
            this.groupControl1.Location = new System.Drawing.Point(204, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(450, 67);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "下载设置";
            // 
            // popupContainerEditTBCat
            // 
            this.popupContainerEditTBCat.Location = new System.Drawing.Point(59, 26);
            this.popupContainerEditTBCat.Name = "popupContainerEditTBCat";
            this.popupContainerEditTBCat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.popupContainerEditTBCat.Properties.PopupControl = this.popupContainerControlItemCat;
            this.popupContainerEditTBCat.Size = new System.Drawing.Size(259, 21);
            this.popupContainerEditTBCat.TabIndex = 199;
            this.popupContainerEditTBCat.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.popupContainerEditTBCat_Closed);
            this.popupContainerEditTBCat.Popup += new System.EventHandler(this.popupContainerEditTBCat_Popup);
            // 
            // popupContainerControlItemCat
            // 
            this.popupContainerControlItemCat.Controls.Add(this.treeListItemCat);
            this.popupContainerControlItemCat.Controls.Add(this.standaloneBarDockControl1);
            this.popupContainerControlItemCat.Location = new System.Drawing.Point(5, 65);
            this.popupContainerControlItemCat.Name = "popupContainerControlItemCat";
            this.popupContainerControlItemCat.Size = new System.Drawing.Size(300, 260);
            this.popupContainerControlItemCat.TabIndex = 206;
            // 
            // treeListItemCat
            // 
            this.treeListItemCat.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn3});
            this.treeListItemCat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListItemCat.Location = new System.Drawing.Point(0, 23);
            this.treeListItemCat.Name = "treeListItemCat";
            this.treeListItemCat.OptionsBehavior.Editable = false;
            this.treeListItemCat.OptionsView.ShowCheckBoxes = true;
            this.treeListItemCat.Size = new System.Drawing.Size(300, 237);
            this.treeListItemCat.TabIndex = 2;
            this.treeListItemCat.BeforeExpand += new DevExpress.XtraTreeList.BeforeExpandEventHandler(this.treeListItemCat_BeforeExpand);
            this.treeListItemCat.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeListItemCat_AfterCheckNode);
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "淘宝类目";
            this.treeListColumn3.FieldName = "淘宝类目";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.OptionsColumn.AllowMove = false;
            this.treeListColumn3.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.treeListColumn3.OptionsColumn.AllowSort = false;
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 0;
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(300, 23);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 29);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 44;
            this.labelControl2.Text = "选择类目";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(347, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 42;
            this.labelControl1.Text = "线程数";
            // 
            // spinEditThreadCount
            // 
            this.spinEditThreadCount.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEditThreadCount.Location = new System.Drawing.Point(389, 26);
            this.spinEditThreadCount.Name = "spinEditThreadCount";
            this.spinEditThreadCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditThreadCount.Properties.IsFloatValue = false;
            this.spinEditThreadCount.Properties.Mask.EditMask = "N00";
            this.spinEditThreadCount.Properties.MaxValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEditThreadCount.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditThreadCount.Size = new System.Drawing.Size(48, 21);
            this.spinEditThreadCount.TabIndex = 40;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(303, 366);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 23);
            this.btnCancel.TabIndex = 41;
            this.btnCancel.Text = "取消下载";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(204, 366);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(93, 23);
            this.btnStart.TabIndex = 38;
            this.btnStart.Text = "开始下载属性";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // listBoxCtrl
            // 
            this.listBoxCtrl.HorizontalScrollbar = true;
            this.listBoxCtrl.Location = new System.Drawing.Point(59, 93);
            this.listBoxCtrl.Name = "listBoxCtrl";
            this.listBoxCtrl.Size = new System.Drawing.Size(378, 177);
            this.listBoxCtrl.TabIndex = 46;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.progressBarCurrent);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.progressBarTotal);
            this.groupControl2.Controls.Add(this.listBoxCtrl);
            this.groupControl2.Location = new System.Drawing.Point(204, 85);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(450, 275);
            this.groupControl2.TabIndex = 47;
            this.groupControl2.Text = "下载详情";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 164);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 209;
            this.labelControl4.Text = "详细信息";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(6, 26);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 208;
            this.labelControl3.Text = "总进度";
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Location = new System.Drawing.Point(59, 26);
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.progressBarTotal.Properties.ShowTitle = true;
            this.progressBarTotal.Properties.StartColor = System.Drawing.SystemColors.HotTrack;
            this.progressBarTotal.Properties.Step = 1;
            this.progressBarTotal.Size = new System.Drawing.Size(378, 18);
            this.progressBarTotal.TabIndex = 207;
            // 
            // panelControl1
            // 
            this.panelControl1.ContentImage = global::Alading.Properties.Resources.wizard_image;
            this.panelControl1.ContentImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.popupContainerControlItemCat);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(666, 360);
            this.panelControl1.TabIndex = 47;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnItemCat});
            this.barManager1.MaxItemId = 1;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.FloatLocation = new System.Drawing.Point(93, 266);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItemCat)});
            this.bar1.Offset = 1;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Tools";
            // 
            // btnItemCat
            // 
            this.btnItemCat.Caption = "更新淘宝类目";
            this.btnItemCat.Id = 0;
            this.btnItemCat.Name = "btnItemCat";
            this.btnItemCat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItemCat_ItemClick);
            // 
            // backWorker
            // 
            this.backWorker.WorkerReportsProgress = true;
            this.backWorker.WorkerSupportsCancellation = true;
            this.backWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backWorker_DoWork);
            this.backWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backWorker_RunWorkerCompleted);
            this.backWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backWorker_ProgressChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(566, 366);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 48;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(6, 63);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 210;
            this.labelControl5.Text = "当前进度";
            // 
            // progressBarCurrent
            // 
            this.progressBarCurrent.Location = new System.Drawing.Point(59, 61);
            this.progressBarCurrent.MenuManager = this.barManager1;
            this.progressBarCurrent.Name = "progressBarCurrent";
            this.progressBarCurrent.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.progressBarCurrent.Properties.ShowTitle = true;
            this.progressBarCurrent.Size = new System.Drawing.Size(378, 18);
            this.progressBarCurrent.TabIndex = 211;
            // 
            // DownloadPropValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 396);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnRetry);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DownloadPropValue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "类目属性管理";
            this.Load += new System.EventHandler(this.DownloadPropValue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEditTBCat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControlItemCat)).EndInit();
            this.popupContainerControlItemCat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListItemCat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditThreadCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxCtrl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarCurrent.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnRetry;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SpinEdit spinEditThreadCount;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.ListBoxControl listBoxCtrl;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEditTBCat;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControlItemCat;
        private DevExpress.XtraTreeList.TreeList treeListItemCat;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnItemCat;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.ComponentModel.BackgroundWorker backWorker;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ProgressBarControl progressBarTotal;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.ProgressBarControl progressBarCurrent;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}