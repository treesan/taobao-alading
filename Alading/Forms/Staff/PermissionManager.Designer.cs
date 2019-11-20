namespace Alading.Forms.StaffManager
{
    partial class PermissionManager
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
            this.navBarCtrlRoles = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroupRoles = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem2 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem3 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem4 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem5 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem6 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem7 = new DevExpress.XtraNavBar.NavBarItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeListPermission = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumnName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnFunction = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.standaloneBarDockControl2 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barBtnAddRole = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDeleteRole = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barBtnSave = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnReset = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarCtrlRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListPermission)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
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
            this.dockPanel1.ID = new System.Guid("dce3fc80-4b16-4ecb-9097-4a0fcd66dc2b");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel1.Size = new System.Drawing.Size(200, 650);
            this.dockPanel1.Text = "操作面板";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.navBarCtrlRoles);
            this.dockPanel1_Container.Controls.Add(this.standaloneBarDockControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(194, 622);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // navBarCtrlRoles
            // 
            this.navBarCtrlRoles.ActiveGroup = this.navBarGroupRoles;
            this.navBarCtrlRoles.AllowSelectedLink = true;
            this.navBarCtrlRoles.ContentButtonHint = null;
            this.navBarCtrlRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarCtrlRoles.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroupRoles});
            this.navBarCtrlRoles.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItem1,
            this.navBarItem2,
            this.navBarItem3,
            this.navBarItem4,
            this.navBarItem5,
            this.navBarItem6,
            this.navBarItem7});
            this.navBarCtrlRoles.Location = new System.Drawing.Point(0, 23);
            this.navBarCtrlRoles.Name = "navBarCtrlRoles";
            this.navBarCtrlRoles.OptionsNavPane.ExpandedWidth = 194;
            this.navBarCtrlRoles.Size = new System.Drawing.Size(194, 599);
            this.navBarCtrlRoles.TabIndex = 1;
            this.navBarCtrlRoles.Text = "角色类型";
            this.navBarCtrlRoles.View = new DevExpress.XtraNavBar.ViewInfo.SkinNavigationPaneViewInfoRegistrator();
            // 
            // navBarGroupRoles
            // 
            this.navBarGroupRoles.Caption = "角色列表";
            this.navBarGroupRoles.Expanded = true;
            this.navBarGroupRoles.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsList;
            this.navBarGroupRoles.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem1),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem2),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem3),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem4),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem5),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem6),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem7)});
            this.navBarGroupRoles.Name = "navBarGroupRoles";
            // 
            // navBarItem1
            // 
            this.navBarItem1.Caption = "老板";
            this.navBarItem1.Name = "navBarItem1";
            // 
            // navBarItem2
            // 
            this.navBarItem2.Caption = "财务";
            this.navBarItem2.Name = "navBarItem2";
            // 
            // navBarItem3
            // 
            this.navBarItem3.Caption = "出纳";
            this.navBarItem3.Name = "navBarItem3";
            // 
            // navBarItem4
            // 
            this.navBarItem4.Caption = "客服";
            this.navBarItem4.Name = "navBarItem4";
            // 
            // navBarItem5
            // 
            this.navBarItem5.Caption = "采购员";
            this.navBarItem5.Name = "navBarItem5";
            // 
            // navBarItem6
            // 
            this.navBarItem6.Caption = "仓管员";
            this.navBarItem6.Name = "navBarItem6";
            // 
            // navBarItem7
            // 
            this.navBarItem7.Caption = "代理商";
            this.navBarItem7.Name = "navBarItem7";
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(194, 23);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.treeListPermission);
            this.groupControl1.Controls.Add(this.standaloneBarDockControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(200, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(808, 650);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "权限列表";
            // 
            // treeListPermission
            // 
            this.treeListPermission.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnName,
            this.treeListColumnFunction});
            this.treeListPermission.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListPermission.Font = new System.Drawing.Font("Tahoma", 9F);
            this.treeListPermission.Location = new System.Drawing.Point(2, 46);
            this.treeListPermission.Name = "treeListPermission";
            this.treeListPermission.BeginUnboundLoad();
            this.treeListPermission.AppendNode(new object[] {
            "经营管理",
            null}, -1);
            this.treeListPermission.AppendNode(new object[] {
            "店铺",
            null}, 0);
            this.treeListPermission.AppendNode(new object[] {
            "初始化",
            null}, 1);
            this.treeListPermission.AppendNode(new object[] {
            "店铺",
            null}, 1);
            this.treeListPermission.AppendNode(new object[] {
            "宝贝",
            null}, 1);
            this.treeListPermission.AppendNode(new object[] {
            "销售",
            null}, 0);
            this.treeListPermission.AppendNode(new object[] {
            "客户",
            null}, 5);
            this.treeListPermission.AppendNode(new object[] {
            "订单",
            null}, 5);
            this.treeListPermission.AppendNode(new object[] {
            "打印",
            null}, 0);
            this.treeListPermission.AppendNode(new object[] {
            "售后",
            null}, 0);
            this.treeListPermission.AppendNode(new object[] {
            "发货",
            null}, 9);
            this.treeListPermission.AppendNode(new object[] {
            "跟单",
            null}, 9);
            this.treeListPermission.AppendNode(new object[] {
            "评价",
            null}, 9);
            this.treeListPermission.AppendNode(new object[] {
            "财务",
            null}, 0);
            this.treeListPermission.AppendNode(new object[] {
            "流水账",
            null}, 13);
            this.treeListPermission.AppendNode(new object[] {
            "报表",
            null}, 0);
            this.treeListPermission.AppendNode(new object[] {
            "统计",
            null}, 15);
            this.treeListPermission.AppendNode(new object[] {
            "分析",
            null}, 15);
            this.treeListPermission.AppendNode(new object[] {
            "系统",
            null}, 0);
            this.treeListPermission.AppendNode(new object[] {
            "插件",
            null}, 18);
            this.treeListPermission.AppendNode(new object[] {
            "风格",
            null}, 18);
            this.treeListPermission.AppendNode(new object[] {
            "帮助",
            null}, 18);
            this.treeListPermission.AppendNode(new object[] {
            "帐号",
            null}, 18);
            this.treeListPermission.AppendNode(new object[] {
            "退出",
            null}, 18);
            this.treeListPermission.AppendNode(new object[] {
            "库存管理",
            null}, -1);
            this.treeListPermission.AppendNode(new object[] {
            "采购",
            null}, 24);
            this.treeListPermission.AppendNode(new object[] {
            "补货单",
            null}, 25);
            this.treeListPermission.AppendNode(new object[] {
            "采购单",
            null}, 25);
            this.treeListPermission.AppendNode(new object[] {
            "库存",
            null}, 24);
            this.treeListPermission.AppendNode(new object[] {
            "商品管理",
            null}, 28);
            this.treeListPermission.AppendNode(new object[] {
            "组合商品",
            null}, 28);
            this.treeListPermission.AppendNode(new object[] {
            "出入库单",
            null}, 28);
            this.treeListPermission.AppendNode(new object[] {
            "库存调拨",
            null}, 28);
            this.treeListPermission.AppendNode(new object[] {
            "盘点",
            null}, 28);
            this.treeListPermission.AppendNode(new object[] {
            "库存设置",
            null}, 28);
            this.treeListPermission.AppendNode(new object[] {
            "系统管理",
            null}, -1);
            this.treeListPermission.AppendNode(new object[] {
            "系统",
            null}, 35);
            this.treeListPermission.AppendNode(new object[] {
            "初始化",
            null}, 36);
            this.treeListPermission.AppendNode(new object[] {
            "数据库",
            null}, 36);
            this.treeListPermission.AppendNode(new object[] {
            "配置",
            null}, 36);
            this.treeListPermission.AppendNode(new object[] {
            "日志",
            null}, 36);
            this.treeListPermission.AppendNode(new object[] {
            "店铺",
            null}, 35);
            this.treeListPermission.AppendNode(new object[] {
            "店铺",
            null}, 41);
            this.treeListPermission.AppendNode(new object[] {
            "类目属性",
            null}, 41);
            this.treeListPermission.AppendNode(new object[] {
            "员工",
            null}, 35);
            this.treeListPermission.AppendNode(new object[] {
            "员工",
            null}, 44);
            this.treeListPermission.AppendNode(new object[] {
            "权限",
            null}, 44);
            this.treeListPermission.AppendNode(new object[] {
            "业绩考核",
            null}, 44);
            this.treeListPermission.AppendNode(new object[] {
            "打印模板",
            null}, 35);
            this.treeListPermission.AppendNode(new object[] {
            "预览",
            null}, 48);
            this.treeListPermission.AppendNode(new object[] {
            "设计",
            null}, 48);
            this.treeListPermission.AppendNode(new object[] {
            "邮件",
            null}, 35);
            this.treeListPermission.AppendNode(new object[] {
            "邮件",
            null}, 51);
            this.treeListPermission.AppendNode(new object[] {
            "邮件模板",
            null}, 51);
            this.treeListPermission.AppendNode(new object[] {
            "报表",
            null}, 35);
            this.treeListPermission.EndUnboundLoad();
            this.treeListPermission.OptionsBehavior.Editable = false;
            this.treeListPermission.OptionsView.ShowCheckBoxes = true;
            this.treeListPermission.Size = new System.Drawing.Size(804, 602);
            this.treeListPermission.TabIndex = 2;
            this.treeListPermission.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeListPermission_AfterCheckNode);
            // 
            // treeListColumnName
            // 
            this.treeListColumnName.Caption = "功能名称";
            this.treeListColumnName.FieldName = "treeListColumn1";
            this.treeListColumnName.MinWidth = 73;
            this.treeListColumnName.Name = "treeListColumnName";
            this.treeListColumnName.Visible = true;
            this.treeListColumnName.VisibleIndex = 0;
            // 
            // treeListColumnFunction
            // 
            this.treeListColumnFunction.Caption = "功能说明";
            this.treeListColumnFunction.FieldName = "功能说明";
            this.treeListColumnFunction.Name = "treeListColumnFunction";
            this.treeListColumnFunction.Visible = true;
            this.treeListColumnFunction.VisibleIndex = 1;
            // 
            // standaloneBarDockControl2
            // 
            this.standaloneBarDockControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl2.Location = new System.Drawing.Point(2, 23);
            this.standaloneBarDockControl2.Name = "standaloneBarDockControl2";
            this.standaloneBarDockControl2.Size = new System.Drawing.Size(804, 23);
            this.standaloneBarDockControl2.Text = "standaloneBarDockControl2";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
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
            this.barBtnAddRole,
            this.barBtnDeleteRole,
            this.barBtnSave,
            this.barButtonItem4,
            this.barBtnReset});
            this.barManager1.MaxItemId = 5;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 1";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAddRole),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDeleteRole)});
            this.bar1.OptionsBar.AllowRename = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Custom 1";
            // 
            // barBtnAddRole
            // 
            this.barBtnAddRole.Caption = "新建";
            this.barBtnAddRole.Glyph = global::Alading.Properties.Resources.tb_bc;
            this.barBtnAddRole.Id = 0;
            this.barBtnAddRole.Name = "barBtnAddRole";
            this.barBtnAddRole.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnAddRole.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAddRole_ItemClick);
            // 
            // barBtnDeleteRole
            // 
            this.barBtnDeleteRole.Caption = "删除";
            this.barBtnDeleteRole.Id = 1;
            this.barBtnDeleteRole.Name = "barBtnDeleteRole";
            this.barBtnDeleteRole.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDeleteRole_ItemClick);
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 2";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnReset)});
            this.bar2.OptionsBar.AllowRename = true;
            this.bar2.StandaloneBarDockControl = this.standaloneBarDockControl2;
            this.bar2.Text = "Custom 2";
            // 
            // barBtnSave
            // 
            this.barBtnSave.Caption = "保存";
            this.barBtnSave.Id = 2;
            this.barBtnSave.Name = "barBtnSave";
            this.barBtnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSave_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "刷新";
            this.barButtonItem4.Id = 3;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barBtnReset
            // 
            this.barBtnReset.Caption = "重置";
            this.barBtnReset.Id = 4;
            this.barBtnReset.Name = "barBtnReset";
            this.barBtnReset.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnReset_ItemClick);
            // 
            // PermissionManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 650);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "PermissionManager";
            this.ShowIcon = false;
            this.Text = "权限管理";
            this.Load += new System.EventHandler(this.PermissionManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarCtrlRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListPermission)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barBtnAddRole;
        private DevExpress.XtraBars.BarButtonItem barBtnDeleteRole;
        private DevExpress.XtraNavBar.NavBarControl navBarCtrlRoles;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupRoles;
        private DevExpress.XtraNavBar.NavBarItem navBarItem1;
        private DevExpress.XtraNavBar.NavBarItem navBarItem2;
        private DevExpress.XtraNavBar.NavBarItem navBarItem3;
        private DevExpress.XtraNavBar.NavBarItem navBarItem4;
        private DevExpress.XtraNavBar.NavBarItem navBarItem5;
        private DevExpress.XtraNavBar.NavBarItem navBarItem6;
        private DevExpress.XtraNavBar.NavBarItem navBarItem7;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl2;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barBtnSave;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barBtnReset;
        private DevExpress.XtraTreeList.TreeList treeListPermission;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnFunction;
    }
}