namespace Alading.Forms.Email
{
    partial class EmailManage
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
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.btAllMail = new DevExpress.XtraNavBar.NavBarItem();
            this.btToSend = new DevExpress.XtraNavBar.NavBarItem();
            this.btSend = new DevExpress.XtraNavBar.NavBarItem();
            this.btFailed = new DevExpress.XtraNavBar.NavBarItem();
            this.htxContent = new DevExpress.XtraRichEdit.RichEditControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btFirstPage = new DevExpress.XtraBars.BarButtonItem();
            this.btPrevPage = new DevExpress.XtraBars.BarButtonItem();
            this.btNextPage = new DevExpress.XtraBars.BarButtonItem();
            this.btLastPage = new DevExpress.XtraBars.BarButtonItem();
            this.btRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txSubject = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txStatus = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txEmail = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txConsumer = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gcEmailList = new DevExpress.XtraGrid.GridControl();
            this.gvEmailList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txConsumer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcEmailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmailList)).BeginInit();
            this.dockPanel2.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1,
            this.dockPanel2});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.FloatSize = new System.Drawing.Size(200, 151);
            this.dockPanel1.ID = new System.Guid("55e88054-e16a-4e22-b394-65c8e0eb2da2");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel1.Size = new System.Drawing.Size(200, 508);
            this.dockPanel1.Text = "邮件管理";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.navBarControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(194, 480);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.ContentButtonHint = null;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.btAllMail,
            this.btToSend,
            this.btSend,
            this.btFailed});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 194;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(194, 480);
            this.navBarControl1.TabIndex = 1;
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "按邮件状态";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsList;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.btAllMail),
            new DevExpress.XtraNavBar.NavBarItemLink(this.btToSend),
            new DevExpress.XtraNavBar.NavBarItemLink(this.btSend),
            new DevExpress.XtraNavBar.NavBarItemLink(this.btFailed)});
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // btAllMail
            // 
            this.btAllMail.Caption = "所有邮件";
            this.btAllMail.Name = "btAllMail";
            this.btAllMail.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.btAllMail_LinkClicked);
            // 
            // btToSend
            // 
            this.btToSend.Caption = "待发送邮件";
            this.btToSend.Name = "btToSend";
            this.btToSend.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.btToSend_LinkClicked);
            // 
            // btSend
            // 
            this.btSend.Caption = "已发送邮件";
            this.btSend.Name = "btSend";
            this.btSend.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.btSend_LinkClicked);
            // 
            // btFailed
            // 
            this.btFailed.Caption = "发送失败邮件";
            this.btFailed.Name = "btFailed";
            this.btFailed.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.btFailed_LinkClicked);
            // 
            // htxContent
            // 
            this.htxContent.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.htxContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.htxContent.Location = new System.Drawing.Point(354, 37);
            this.htxContent.MenuManager = this.barManager1;
            this.htxContent.Name = "htxContent";
            this.htxContent.ReadOnly = true;
            this.htxContent.Size = new System.Drawing.Size(439, 126);
            this.htxContent.TabIndex = 4;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btFirstPage,
            this.btPrevPage,
            this.btNextPage,
            this.btLastPage,
            this.btRefresh});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 5;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btFirstPage),
            new DevExpress.XtraBars.LinkPersistInfo(this.btPrevPage),
            new DevExpress.XtraBars.LinkPersistInfo(this.btNextPage),
            new DevExpress.XtraBars.LinkPersistInfo(this.btLastPage),
            new DevExpress.XtraBars.LinkPersistInfo(this.btRefresh)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar2.Text = "Main menu";
            // 
            // btFirstPage
            // 
            this.btFirstPage.Caption = "首页";
            this.btFirstPage.Id = 0;
            this.btFirstPage.Name = "btFirstPage";
            this.btFirstPage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btFirstPage_ItemClick);
            // 
            // btPrevPage
            // 
            this.btPrevPage.Caption = "上一页";
            this.btPrevPage.Id = 1;
            this.btPrevPage.Name = "btPrevPage";
            this.btPrevPage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btPrevPage_ItemClick);
            // 
            // btNextPage
            // 
            this.btNextPage.Caption = "下一页";
            this.btNextPage.Id = 2;
            this.btNextPage.Name = "btNextPage";
            this.btNextPage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btNextPage_ItemClick);
            // 
            // btLastPage
            // 
            this.btLastPage.Caption = "末页";
            this.btLastPage.Id = 3;
            this.btLastPage.Name = "btLastPage";
            this.btLastPage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btLastPage_ItemClick);
            // 
            // btRefresh
            // 
            this.btRefresh.Caption = "刷新";
            this.btRefresh.Id = 4;
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btRefresh_ItemClick);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(2, 283);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(804, 23);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(288, 39);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 14);
            this.labelControl6.TabIndex = 13;
            this.labelControl6.Text = "邮件内容：";
            // 
            // txSubject
            // 
            this.txSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txSubject.Location = new System.Drawing.Point(354, 10);
            this.txSubject.Name = "txSubject";
            this.txSubject.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txSubject.Properties.Appearance.Options.UseBackColor = true;
            this.txSubject.Properties.ReadOnly = true;
            this.txSubject.Size = new System.Drawing.Size(439, 21);
            this.txSubject.TabIndex = 12;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(288, 12);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 14);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "邮件主题：";
            // 
            // txStatus
            // 
            this.txStatus.Location = new System.Drawing.Point(76, 91);
            this.txStatus.Name = "txStatus";
            this.txStatus.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txStatus.Properties.Appearance.Options.UseBackColor = true;
            this.txStatus.Properties.ReadOnly = true;
            this.txStatus.Size = new System.Drawing.Size(195, 21);
            this.txStatus.TabIndex = 10;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(34, 93);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "状态：";
            // 
            // txTime
            // 
            this.txTime.Location = new System.Drawing.Point(76, 64);
            this.txTime.Name = "txTime";
            this.txTime.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txTime.Properties.Appearance.Options.UseBackColor = true;
            this.txTime.Properties.ReadOnly = true;
            this.txTime.Size = new System.Drawing.Size(195, 21);
            this.txTime.TabIndex = 8;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(10, 66);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "发送时间：";
            // 
            // txEmail
            // 
            this.txEmail.Location = new System.Drawing.Point(76, 37);
            this.txEmail.Name = "txEmail";
            this.txEmail.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txEmail.Properties.Appearance.Options.UseBackColor = true;
            this.txEmail.Properties.ReadOnly = true;
            this.txEmail.Size = new System.Drawing.Size(195, 21);
            this.txEmail.TabIndex = 6;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 39);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "客户邮箱：";
            // 
            // txConsumer
            // 
            this.txConsumer.Location = new System.Drawing.Point(76, 10);
            this.txConsumer.Name = "txConsumer";
            this.txConsumer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txConsumer.Properties.Appearance.Options.UseBackColor = true;
            this.txConsumer.Properties.ReadOnly = true;
            this.txConsumer.Size = new System.Drawing.Size(195, 21);
            this.txConsumer.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "客户昵称：";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gcEmailList);
            this.groupControl1.Controls.Add(this.standaloneBarDockControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(200, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(808, 308);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "邮件列表";
            // 
            // gcEmailList
            // 
            this.gcEmailList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcEmailList.Location = new System.Drawing.Point(2, 23);
            this.gcEmailList.MainView = this.gvEmailList;
            this.gcEmailList.Name = "gcEmailList";
            this.gcEmailList.Size = new System.Drawing.Size(804, 260);
            this.gcEmailList.TabIndex = 2;
            this.gcEmailList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvEmailList});
            // 
            // gvEmailList
            // 
            this.gvEmailList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gvEmailList.GridControl = this.gcEmailList;
            this.gvEmailList.Name = "gvEmailList";
            this.gvEmailList.OptionsBehavior.Editable = false;
            this.gvEmailList.OptionsView.ShowGroupPanel = false;
            this.gvEmailList.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvEmailList_RowClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "客户昵称";
            this.gridColumn1.FieldName = "ConsumerNick";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "邮件主题";
            this.gridColumn2.FieldName = "Subject";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "发送时间";
            this.gridColumn3.FieldName = "VisitTime";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "邮件接收地址";
            this.gridColumn4.FieldName = "Receiver";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "状态";
            this.gridColumn5.FieldName = "Status";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanel2.ID = new System.Guid("d04f59a2-01ac-48ec-95b2-2d72d636e5bc");
            this.dockPanel2.Location = new System.Drawing.Point(200, 308);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel2.Size = new System.Drawing.Size(808, 200);
            this.dockPanel2.Text = "邮件信息";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.htxContent);
            this.dockPanel2_Container.Controls.Add(this.labelControl6);
            this.dockPanel2_Container.Controls.Add(this.labelControl1);
            this.dockPanel2_Container.Controls.Add(this.txSubject);
            this.dockPanel2_Container.Controls.Add(this.txConsumer);
            this.dockPanel2_Container.Controls.Add(this.labelControl5);
            this.dockPanel2_Container.Controls.Add(this.labelControl2);
            this.dockPanel2_Container.Controls.Add(this.txStatus);
            this.dockPanel2_Container.Controls.Add(this.txEmail);
            this.dockPanel2_Container.Controls.Add(this.labelControl4);
            this.dockPanel2_Container.Controls.Add(this.labelControl3);
            this.dockPanel2_Container.Controls.Add(this.txTime);
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(802, 172);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // EmailManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 508);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.dockPanel2);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "EmailManage";
            this.Text = "EmailManage";
            this.Load += new System.EventHandler(this.EmailManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txConsumer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcEmailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmailList)).EndInit();
            this.dockPanel2.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            this.dockPanel2_Container.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarItem btAllMail;
        private DevExpress.XtraNavBar.NavBarItem btToSend;
        private DevExpress.XtraNavBar.NavBarItem btSend;
        private DevExpress.XtraNavBar.NavBarItem btFailed;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txConsumer;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txStatus;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txTime;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txEmail;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gcEmailList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvEmailList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraEditors.TextEdit txSubject;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraRichEdit.RichEditControl htxContent;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem btFirstPage;
        private DevExpress.XtraBars.BarButtonItem btPrevPage;
        private DevExpress.XtraBars.BarButtonItem btNextPage;
        private DevExpress.XtraBars.BarButtonItem btLastPage;
        private DevExpress.XtraBars.BarButtonItem btRefresh;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
    }
}