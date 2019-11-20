namespace Alading.Forms.Init.Control
{
    partial class InitShopControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.listBoxCtrl = new DevExpress.XtraEditors.ListBoxControl();
            this.spinEditThreadCount = new DevExpress.XtraEditors.SpinEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.checkEditIsUpdate = new DevExpress.XtraEditors.CheckEdit();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.label6 = new System.Windows.Forms.Label();
            this.progressBarCtrl = new DevExpress.XtraEditors.ProgressBarControl();
            this.label5 = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeListShop = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn8 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxCtrl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditThreadCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditIsUpdate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarCtrl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListShop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btnCancel);
            this.groupControl2.Controls.Add(this.listBoxCtrl);
            this.groupControl2.Controls.Add(this.spinEditThreadCount);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.checkEditIsUpdate);
            this.groupControl2.Controls.Add(this.btnStart);
            this.groupControl2.Controls.Add(this.label6);
            this.groupControl2.Controls.Add(this.progressBarCtrl);
            this.groupControl2.Controls.Add(this.label5);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(810, 213);
            this.groupControl2.TabIndex = 32;
            this.groupControl2.Text = "下载信息";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(723, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 37;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // listBoxCtrl
            // 
            this.listBoxCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxCtrl.HorizontalScrollbar = true;
            this.listBoxCtrl.Location = new System.Drawing.Point(80, 58);
            this.listBoxCtrl.Name = "listBoxCtrl";
            this.listBoxCtrl.Size = new System.Drawing.Size(718, 114);
            this.listBoxCtrl.TabIndex = 36;
            // 
            // spinEditThreadCount
            // 
            this.spinEditThreadCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.spinEditThreadCount.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEditThreadCount.Location = new System.Drawing.Point(588, 181);
            this.spinEditThreadCount.Name = "spinEditThreadCount";
            this.spinEditThreadCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditThreadCount.Properties.IsFloatValue = false;
            this.spinEditThreadCount.Properties.Mask.EditMask = "N00";
            this.spinEditThreadCount.Properties.MaxValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spinEditThreadCount.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditThreadCount.Size = new System.Drawing.Size(48, 21);
            this.spinEditThreadCount.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(515, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 34;
            this.label1.Text = "下载线程数";
            // 
            // checkEditIsUpdate
            // 
            this.checkEditIsUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkEditIsUpdate.EditValue = true;
            this.checkEditIsUpdate.Location = new System.Drawing.Point(418, 183);
            this.checkEditIsUpdate.Name = "checkEditIsUpdate";
            this.checkEditIsUpdate.Properties.Caption = "下载宝贝图片";
            this.checkEditIsUpdate.Size = new System.Drawing.Size(96, 19);
            this.checkEditIsUpdate.TabIndex = 33;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(642, 180);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 32;
            this.btnStart.Text = "开始下载";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 30;
            this.label6.Text = "详细信息：";
            // 
            // progressBarCtrl
            // 
            this.progressBarCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarCtrl.Location = new System.Drawing.Point(80, 34);
            this.progressBarCtrl.Name = "progressBarCtrl";
            this.progressBarCtrl.Properties.ShowTitle = true;
            this.progressBarCtrl.Size = new System.Drawing.Size(718, 18);
            this.progressBarCtrl.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 14);
            this.label5.TabIndex = 28;
            this.label5.Text = "当前进度：";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.treeListShop);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 213);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(810, 289);
            this.groupControl1.TabIndex = 33;
            this.groupControl1.Text = "店铺列表";
            // 
            // treeListShop
            // 
            this.treeListShop.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn5,
            this.treeListColumn2,
            this.treeListColumn4,
            this.treeListColumn3,
            this.treeListColumn8,
            this.treeListColumn6});
            this.treeListShop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListShop.Location = new System.Drawing.Point(2, 23);
            this.treeListShop.Name = "treeListShop";
            this.treeListShop.OptionsPrint.AutoWidth = false;
            this.treeListShop.OptionsView.AutoWidth = false;
            this.treeListShop.OptionsView.ShowRoot = false;
            this.treeListShop.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.treeListShop.Size = new System.Drawing.Size(806, 264);
            this.treeListShop.TabIndex = 23;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "选择";
            this.treeListColumn1.ColumnEdit = this.repositoryItemCheckEdit1;
            this.treeListColumn1.FieldName = "IsSelected";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 50;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "状态";
            this.treeListColumn5.FieldName = "DownloadStatus";
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.Width = 109;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "店铺名称";
            this.treeListColumn2.FieldName = "title";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            this.treeListColumn2.Width = 357;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "店铺类别";
            this.treeListColumn4.FieldName = "ShopTypeName";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 2;
            this.treeListColumn4.Width = 132;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "最近同步日期";
            this.treeListColumn3.FieldName = "LastSyncTime";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 3;
            this.treeListColumn3.Width = 205;
            // 
            // treeListColumn8
            // 
            this.treeListColumn8.Caption = "nick";
            this.treeListColumn8.FieldName = "nick";
            this.treeListColumn8.Name = "treeListColumn8";
            // 
            // treeListColumn6
            // 
            this.treeListColumn6.Caption = "sid";
            this.treeListColumn6.FieldName = "sid";
            this.treeListColumn6.Name = "treeListColumn6";
            // 
            // InitShopControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl2);
            this.Name = "InitShopControl";
            this.Size = new System.Drawing.Size(810, 502);
            this.Load += new System.EventHandler(this.InitShopControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxCtrl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditThreadCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditIsUpdate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarCtrl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListShop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.ListBoxControl listBoxCtrl;
        private DevExpress.XtraEditors.SpinEdit spinEditThreadCount;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.CheckEdit checkEditIsUpdate;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.ProgressBarControl progressBarCtrl;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraTreeList.TreeList treeListShop;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn8;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
    }
}
