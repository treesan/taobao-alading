namespace Alading.Forms.Email
{
    partial class EmailTemplateManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailTemplateManage));
            this.gpTempList = new DevExpress.XtraEditors.GroupControl();
            this.gcTempList = new DevExpress.XtraGrid.GridControl();
            this.gvTempList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btAddTemp = new DevExpress.XtraBars.BarButtonItem();
            this.btDelTemp = new DevExpress.XtraBars.BarButtonItem();
            this.btRefreshTemp = new DevExpress.XtraBars.BarButtonItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.standaloneBarDockControl2 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.treeListCat = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.htxEmailContent = new Alading.HtmlEditor.Editor();
            this.btTempCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btAddMacro = new DevExpress.XtraEditors.SimpleButton();
            this.txTempName = new DevExpress.XtraEditors.TextEdit();
            this.requiredLabel1 = new Alading.Controls.Common.RequiredLabel();
            this.requiredLabel3 = new Alading.Controls.Common.RequiredLabel();
            this.btSaveTemp = new DevExpress.XtraEditors.SimpleButton();
            this.requiredLabel4 = new Alading.Controls.Common.RequiredLabel();
            this.txEmailTitle = new DevExpress.XtraEditors.TextEdit();
            this.requiredLabel2 = new Alading.Controls.Common.RequiredLabel();
            this.cbTempCat = new DevExpress.XtraEditors.ComboBoxEdit();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btAddCat = new DevExpress.XtraBars.BarButtonItem();
            this.btDelCat = new DevExpress.XtraBars.BarButtonItem();
            this.btRefreshCat = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gpTempList)).BeginInit();
            this.gpTempList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTempList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTempList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListCat)).BeginInit();
            this.dockPanel2.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txTempName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txEmailTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTempCat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            this.SuspendLayout();
            // 
            // gpTempList
            // 
            this.gpTempList.Controls.Add(this.gcTempList);
            this.gpTempList.Controls.Add(this.standaloneBarDockControl1);
            this.gpTempList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpTempList.Location = new System.Drawing.Point(200, 0);
            this.gpTempList.Name = "gpTempList";
            this.gpTempList.Size = new System.Drawing.Size(808, 430);
            this.gpTempList.TabIndex = 1;
            this.gpTempList.Text = "模板列表";
            // 
            // gcTempList
            // 
            this.gcTempList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTempList.Location = new System.Drawing.Point(2, 46);
            this.gcTempList.MainView = this.gvTempList;
            this.gcTempList.MenuManager = this.barManager1;
            this.gcTempList.Name = "gcTempList";
            this.gcTempList.Size = new System.Drawing.Size(804, 382);
            this.gcTempList.TabIndex = 3;
            this.gcTempList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTempList});
            // 
            // gvTempList
            // 
            this.gvTempList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gvTempList.GridControl = this.gcTempList;
            this.gvTempList.Name = "gvTempList";
            this.gvTempList.OptionsBehavior.Editable = false;
            this.gvTempList.OptionsView.ShowGroupPanel = false;
            this.gvTempList.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvTempList_RowClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "模板名称";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "邮件标题";
            this.gridColumn3.FieldName = "Title";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "邮件内容";
            this.gridColumn4.FieldName = "Content";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "创建时间";
            this.gridColumn5.DisplayFormat.FormatString = "yyyy/MM/dd HH:mm";
            this.gridColumn5.FieldName = "CreationTime";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
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
            this.barManager1.DockControls.Add(this.standaloneBarDockControl2);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btAddTemp,
            this.btRefreshTemp,
            this.btDelTemp});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar2.FloatLocation = new System.Drawing.Point(551, 182);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btAddTemp),
            new DevExpress.XtraBars.LinkPersistInfo(this.btDelTemp),
            new DevExpress.XtraBars.LinkPersistInfo(this.btRefreshTemp)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar2.Text = "Main menu";
            // 
            // btAddTemp
            // 
            this.btAddTemp.Caption = "新建";
            this.btAddTemp.Id = 0;
            this.btAddTemp.Name = "btAddTemp";
            this.btAddTemp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btAddTemp_ItemClick);
            // 
            // btDelTemp
            // 
            this.btDelTemp.Caption = "删除";
            this.btDelTemp.Id = 2;
            this.btDelTemp.Name = "btDelTemp";
            this.btDelTemp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btDelTemp_ItemClick);
            // 
            // btRefreshTemp
            // 
            this.btRefreshTemp.Caption = "刷新";
            this.btRefreshTemp.Id = 1;
            this.btRefreshTemp.Name = "btRefreshTemp";
            this.btRefreshTemp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btRefreshTemp_ItemClick);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(2, 23);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(804, 23);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // standaloneBarDockControl2
            // 
            this.standaloneBarDockControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl2.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl2.Name = "standaloneBarDockControl2";
            this.standaloneBarDockControl2.Size = new System.Drawing.Size(194, 23);
            this.standaloneBarDockControl2.Text = "standaloneBarDockControl2";
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
            this.dockPanel1.FloatVertical = true;
            this.dockPanel1.ID = new System.Guid("08348cc0-1596-4026-8591-634e96aac040");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 730);
            this.dockPanel1.Size = new System.Drawing.Size(200, 730);
            this.dockPanel1.Text = "模板分类";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.treeListCat);
            this.dockPanel1_Container.Controls.Add(this.standaloneBarDockControl2);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(194, 702);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // treeListCat
            // 
            this.treeListCat.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeListCat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListCat.Location = new System.Drawing.Point(0, 23);
            this.treeListCat.Name = "treeListCat";
            this.treeListCat.Size = new System.Drawing.Size(194, 679);
            this.treeListCat.TabIndex = 14;
            this.treeListCat.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListCat_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "模板分类列表";
            this.treeListColumn1.FieldName = "Name";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanel2.ID = new System.Guid("579d1e23-5953-4a60-bd62-09765b7b9c3a");
            this.dockPanel2.Location = new System.Drawing.Point(200, 430);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.OriginalSize = new System.Drawing.Size(200, 300);
            this.dockPanel2.Size = new System.Drawing.Size(808, 300);
            this.dockPanel2.Text = "模板内容";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.htxEmailContent);
            this.dockPanel2_Container.Controls.Add(this.btTempCancel);
            this.dockPanel2_Container.Controls.Add(this.btAddMacro);
            this.dockPanel2_Container.Controls.Add(this.txTempName);
            this.dockPanel2_Container.Controls.Add(this.requiredLabel1);
            this.dockPanel2_Container.Controls.Add(this.requiredLabel3);
            this.dockPanel2_Container.Controls.Add(this.btSaveTemp);
            this.dockPanel2_Container.Controls.Add(this.requiredLabel4);
            this.dockPanel2_Container.Controls.Add(this.txEmailTitle);
            this.dockPanel2_Container.Controls.Add(this.requiredLabel2);
            this.dockPanel2_Container.Controls.Add(this.cbTempCat);
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(802, 272);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // htxEmailContent
            // 
            this.htxEmailContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.htxEmailContent.BodyHtml = null;
            this.htxEmailContent.BodyText = null;
            this.htxEmailContent.DocumentText = resources.GetString("htxEmailContent.DocumentText");
            this.htxEmailContent.EditorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.htxEmailContent.EditorForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.htxEmailContent.FontSize = Alading.HtmlEditor.FontSize.Three;
            this.htxEmailContent.Location = new System.Drawing.Point(98, 65);
            this.htxEmailContent.Name = "htxEmailContent";
            this.htxEmailContent.Size = new System.Drawing.Size(690, 164);
            this.htxEmailContent.TabIndex = 11;
            // 
            // btTempCancel
            // 
            this.btTempCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btTempCancel.Location = new System.Drawing.Point(713, 240);
            this.btTempCancel.Name = "btTempCancel";
            this.btTempCancel.Size = new System.Drawing.Size(75, 23);
            this.btTempCancel.TabIndex = 5;
            this.btTempCancel.Text = "取消";
            this.btTempCancel.Click += new System.EventHandler(this.btTempCancel_Click);
            // 
            // btAddMacro
            // 
            this.btAddMacro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btAddMacro.Location = new System.Drawing.Point(98, 240);
            this.btAddMacro.Name = "btAddMacro";
            this.btAddMacro.Size = new System.Drawing.Size(75, 23);
            this.btAddMacro.TabIndex = 10;
            this.btAddMacro.Text = "插入宏";
            this.btAddMacro.Click += new System.EventHandler(this.btAddMacro_Click);
            // 
            // txTempName
            // 
            this.txTempName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txTempName.EnterMoveNextControl = true;
            this.txTempName.Location = new System.Drawing.Point(98, 9);
            this.txTempName.MenuManager = this.barManager1;
            this.txTempName.Name = "txTempName";
            this.txTempName.Size = new System.Drawing.Size(418, 21);
            this.txTempName.TabIndex = 0;
            this.txTempName.EditValueChanged += new System.EventHandler(this.requiredTextField_EditValueChanged);
            // 
            // requiredLabel1
            // 
            this.requiredLabel1.Location = new System.Drawing.Point(12, 12);
            this.requiredLabel1.Name = "requiredLabel1";
            this.requiredLabel1.Size = new System.Drawing.Size(80, 14);
            this.requiredLabel1.TabIndex = 2;
            this.requiredLabel1.Text = "模板名称";
            // 
            // requiredLabel3
            // 
            this.requiredLabel3.Location = new System.Drawing.Point(12, 41);
            this.requiredLabel3.Name = "requiredLabel3";
            this.requiredLabel3.Size = new System.Drawing.Size(80, 14);
            this.requiredLabel3.TabIndex = 2;
            this.requiredLabel3.Text = "邮件标题";
            // 
            // btSaveTemp
            // 
            this.btSaveTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSaveTemp.Location = new System.Drawing.Point(626, 240);
            this.btSaveTemp.Name = "btSaveTemp";
            this.btSaveTemp.Size = new System.Drawing.Size(75, 23);
            this.btSaveTemp.TabIndex = 4;
            this.btSaveTemp.Text = "保存";
            this.btSaveTemp.Click += new System.EventHandler(this.btSaveTemp_Click);
            // 
            // requiredLabel4
            // 
            this.requiredLabel4.Location = new System.Drawing.Point(12, 71);
            this.requiredLabel4.Name = "requiredLabel4";
            this.requiredLabel4.Size = new System.Drawing.Size(80, 14);
            this.requiredLabel4.TabIndex = 2;
            this.requiredLabel4.Text = "邮件内容";
            // 
            // txEmailTitle
            // 
            this.txEmailTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txEmailTitle.EnterMoveNextControl = true;
            this.txEmailTitle.Location = new System.Drawing.Point(98, 38);
            this.txEmailTitle.MenuManager = this.barManager1;
            this.txEmailTitle.Name = "txEmailTitle";
            this.txEmailTitle.Size = new System.Drawing.Size(690, 21);
            this.txEmailTitle.TabIndex = 2;
            this.txEmailTitle.EditValueChanged += new System.EventHandler(this.requiredTextField_EditValueChanged);
            // 
            // requiredLabel2
            // 
            this.requiredLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.requiredLabel2.Location = new System.Drawing.Point(540, 12);
            this.requiredLabel2.Name = "requiredLabel2";
            this.requiredLabel2.Size = new System.Drawing.Size(80, 14);
            this.requiredLabel2.TabIndex = 2;
            this.requiredLabel2.Text = "模板分类";
            // 
            // cbTempCat
            // 
            this.cbTempCat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTempCat.EditValue = "";
            this.cbTempCat.EnterMoveNextControl = true;
            this.cbTempCat.Location = new System.Drawing.Point(626, 9);
            this.cbTempCat.MenuManager = this.barManager1;
            this.cbTempCat.Name = "cbTempCat";
            this.cbTempCat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbTempCat.Size = new System.Drawing.Size(162, 21);
            this.cbTempCat.TabIndex = 1;
            this.cbTempCat.SelectedIndexChanged += new System.EventHandler(this.cbTempCat_SelectedIndexChanged);
            // 
            // barManager2
            // 
            this.barManager2.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager2.DockControls.Add(this.barDockControl1);
            this.barManager2.DockControls.Add(this.barDockControl2);
            this.barManager2.DockControls.Add(this.barDockControl3);
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.DockManager = this.dockManager1;
            this.barManager2.Form = this;
            this.barManager2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btAddCat,
            this.btDelCat,
            this.btRefreshCat});
            this.barManager2.MainMenu = this.bar3;
            this.barManager2.MaxItemId = 3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Main menu";
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar3.FloatLocation = new System.Drawing.Point(290, 230);
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btAddCat),
            new DevExpress.XtraBars.LinkPersistInfo(this.btDelCat),
            new DevExpress.XtraBars.LinkPersistInfo(this.btRefreshCat)});
            this.bar3.OptionsBar.MultiLine = true;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.StandaloneBarDockControl = this.standaloneBarDockControl2;
            this.bar3.Text = "Main menu";
            // 
            // btAddCat
            // 
            this.btAddCat.Caption = "添加";
            this.btAddCat.Id = 0;
            this.btAddCat.Name = "btAddCat";
            this.btAddCat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btAddCat_ItemClick);
            // 
            // btDelCat
            // 
            this.btDelCat.Caption = "删除";
            this.btDelCat.Id = 1;
            this.btDelCat.Name = "btDelCat";
            this.btDelCat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btDelCat_ItemClick);
            // 
            // btRefreshCat
            // 
            this.btRefreshCat.Caption = "刷新";
            this.btRefreshCat.Id = 2;
            this.btRefreshCat.Name = "btRefreshCat";
            this.btRefreshCat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btRefreshCat_ItemClick);
            // 
            // EmailTemplateManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.gpTempList);
            this.Controls.Add(this.dockPanel2);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Name = "EmailTemplateManage";
            this.Text = "EmailTemplateManage";
            this.Load += new System.EventHandler(this.EmailTemplateManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gpTempList)).EndInit();
            this.gpTempList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTempList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTempList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListCat)).EndInit();
            this.dockPanel2.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txTempName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txEmailTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTempCat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gpTempList;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem btAddTemp;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gcTempList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTempList;
        private DevExpress.XtraBars.BarButtonItem btRefreshTemp;
        private DevExpress.XtraBars.BarButtonItem btDelTemp;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.TextEdit txTempName;
        private Alading.Controls.Common.RequiredLabel requiredLabel1;
        private DevExpress.XtraEditors.ComboBoxEdit cbTempCat;
        private Alading.Controls.Common.RequiredLabel requiredLabel2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.TextEdit txEmailTitle;
        private Alading.Controls.Common.RequiredLabel requiredLabel3;
        private Alading.Controls.Common.RequiredLabel requiredLabel4;
        private DevExpress.XtraEditors.SimpleButton btTempCancel;
        private DevExpress.XtraEditors.SimpleButton btSaveTemp;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btAddCat;
        private DevExpress.XtraBars.BarButtonItem btDelCat;
        private DevExpress.XtraBars.BarButtonItem btRefreshCat;
        private DevExpress.XtraTreeList.TreeList treeListCat;
        private DevExpress.XtraEditors.SimpleButton btAddMacro;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private Alading.HtmlEditor.Editor htxEmailContent;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;



    }
}