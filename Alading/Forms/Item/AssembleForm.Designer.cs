namespace Alading.Forms.Item
{
    partial class AssembleForm
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
            this.vGridControl1 = new DevExpress.XtraVerticalGrid.VGridControl();
            this.categoryRowKeyProps = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.categoryRowSaleProps = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.categoryRowNotKeyProps = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.categoryRowStockProps = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.textKeyWord = new DevExpress.XtraEditors.TextEdit();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gAssembleItem = new DevExpress.XtraGrid.GridControl();
            this.gVAssembleItem = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcAssembleCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSimpleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAssembleDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnAssociate = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textKeyWord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gAssembleItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gVAssembleItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
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
            this.dockPanel1.ID = new System.Guid("7e5f88c9-f9aa-4561-b7de-49a13510bc79");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(175, 200);
            this.dockPanel1.Size = new System.Drawing.Size(175, 422);
            this.dockPanel1.Text = "商品属性";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.vGridControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(169, 394);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // vGridControl1
            // 
            this.vGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vGridControl1.Location = new System.Drawing.Point(0, 0);
            this.vGridControl1.Name = "vGridControl1";
            this.vGridControl1.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.categoryRowKeyProps,
            this.categoryRowSaleProps,
            this.categoryRowNotKeyProps,
            this.categoryRowStockProps});
            this.vGridControl1.Size = new System.Drawing.Size(169, 394);
            this.vGridControl1.TabIndex = 2;
            // 
            // categoryRowKeyProps
            // 
            this.categoryRowKeyProps.Name = "categoryRowKeyProps";
            this.categoryRowKeyProps.Properties.Caption = "关键属性";
            // 
            // categoryRowSaleProps
            // 
            this.categoryRowSaleProps.Name = "categoryRowSaleProps";
            this.categoryRowSaleProps.Properties.Caption = "销售属性";
            // 
            // categoryRowNotKeyProps
            // 
            this.categoryRowNotKeyProps.Name = "categoryRowNotKeyProps";
            this.categoryRowNotKeyProps.Properties.Caption = "非关键属性";
            // 
            // categoryRowStockProps
            // 
            this.categoryRowStockProps.Name = "categoryRowStockProps";
            this.categoryRowStockProps.Visible = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.textKeyWord);
            this.groupControl2.Controls.Add(this.btnSearch);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(175, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(646, 71);
            this.groupControl2.TabIndex = 10;
            this.groupControl2.Text = "商品查询";
            // 
            // textKeyWord
            // 
            this.textKeyWord.Location = new System.Drawing.Point(70, 37);
            this.textKeyWord.Name = "textKeyWord";
            this.textKeyWord.Properties.MaxLength = 50;
            this.textKeyWord.Properties.NullValuePrompt = "组合商品名称、简称、型号、规格、商家编码";
            this.textKeyWord.Properties.NullValuePromptShowForEmptyValue = true;
            this.textKeyWord.Size = new System.Drawing.Size(400, 21);
            this.textKeyWord.TabIndex = 30;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::Alading.Properties.Resources.tb_dyyl;
            this.btnSearch.Location = new System.Drawing.Point(486, 35);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 23);
            this.btnSearch.TabIndex = 29;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 14);
            this.label3.TabIndex = 28;
            this.label3.Text = "关键词";
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gAssembleItem);
            this.groupControl1.Controls.Add(this.standaloneBarDockControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(175, 71);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(646, 351);
            this.groupControl1.TabIndex = 11;
            this.groupControl1.Text = "组合商品";
            // 
            // gAssembleItem
            // 
            this.gAssembleItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gAssembleItem.Location = new System.Drawing.Point(2, 48);
            this.gAssembleItem.MainView = this.gVAssembleItem;
            this.gAssembleItem.Name = "gAssembleItem";
            this.gAssembleItem.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.repositoryItemCheckEdit1});
            this.gAssembleItem.Size = new System.Drawing.Size(642, 301);
            this.gAssembleItem.TabIndex = 13;
            this.gAssembleItem.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gVAssembleItem});
            // 
            // gVAssembleItem
            // 
            this.gVAssembleItem.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcAssembleCode,
            this.gridColumnName,
            this.gridColumn17,
            this.gcSimpleName,
            this.gcPrice,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn12,
            this.gridColumn13,
            this.gcAssembleDesc});
            this.gVAssembleItem.GridControl = this.gAssembleItem;
            this.gVAssembleItem.Name = "gVAssembleItem";
            this.gVAssembleItem.OptionsCustomization.AllowColumnMoving = false;
            this.gVAssembleItem.OptionsCustomization.AllowQuickHideColumns = false;
            this.gVAssembleItem.OptionsView.ColumnAutoWidth = false;
            this.gVAssembleItem.OptionsView.ShowGroupPanel = false;
            this.gVAssembleItem.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvAssemble_FocusedRowChanged);
            // 
            // gcAssembleCode
            // 
            this.gcAssembleCode.Caption = "组合商家编码";
            this.gcAssembleCode.FieldName = "OuterID";
            this.gcAssembleCode.Name = "gcAssembleCode";
            this.gcAssembleCode.OptionsColumn.AllowEdit = false;
            this.gcAssembleCode.Visible = true;
            this.gcAssembleCode.VisibleIndex = 0;
            this.gcAssembleCode.Width = 69;
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "组合商品名称";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.OptionsColumn.AllowEdit = false;
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 1;
            this.gridColumnName.Width = 71;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "组合属性";
            this.gridColumn17.FieldName = "SkuProps_Str";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.AllowEdit = false;
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 2;
            this.gridColumn17.Width = 58;
            // 
            // gcSimpleName
            // 
            this.gcSimpleName.Caption = "商品简称";
            this.gcSimpleName.FieldName = "SimpleName";
            this.gcSimpleName.Name = "gcSimpleName";
            this.gcSimpleName.OptionsColumn.AllowEdit = false;
            this.gcSimpleName.Visible = true;
            this.gcSimpleName.VisibleIndex = 3;
            this.gcSimpleName.Width = 58;
            // 
            // gcPrice
            // 
            this.gcPrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcPrice.Caption = "零售价";
            this.gcPrice.DisplayFormat.FormatString = "c";
            this.gcPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcPrice.FieldName = "Price";
            this.gcPrice.Name = "gcPrice";
            this.gcPrice.OptionsColumn.AllowEdit = false;
            this.gcPrice.Visible = true;
            this.gcPrice.VisibleIndex = 5;
            this.gcPrice.Width = 47;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "计量单位";
            this.gridColumn1.FieldName = "UnitName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            this.gridColumn1.Width = 50;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "淘宝类目";
            this.gridColumn2.FieldName = "CatName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 6;
            this.gridColumn2.Width = 51;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "商品型号";
            this.gridColumn3.FieldName = "Model";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 7;
            this.gridColumn3.Width = 51;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "商品规格";
            this.gridColumn12.FieldName = "Specification";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 8;
            this.gridColumn12.Width = 63;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "产品税率";
            this.gridColumn13.FieldName = "TaxName";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 9;
            this.gridColumn13.Width = 67;
            // 
            // gcAssembleDesc
            // 
            this.gcAssembleDesc.Caption = "商品介绍";
            this.gcAssembleDesc.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.gcAssembleDesc.FieldName = "AssembleDesc";
            this.gcAssembleDesc.Name = "gcAssembleDesc";
            this.gcAssembleDesc.Visible = true;
            this.gcAssembleDesc.VisibleIndex = 10;
            this.gcAssembleDesc.Width = 101;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            this.repositoryItemMemoExEdit1.ReadOnly = true;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(2, 23);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(642, 25);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
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
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnAssociate});
            this.barManager1.MaxItemId = 1;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.FloatLocation = new System.Drawing.Point(225, 262);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnAssociate, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Tools";
            // 
            // btnAssociate
            // 
            this.btnAssociate.Caption = "关联";
            this.btnAssociate.Glyph = global::Alading.Properties.Resources.shopexit;
            this.btnAssociate.Id = 0;
            this.btnAssociate.Name = "btnAssociate";
            this.btnAssociate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAssociate_ItemClick);
            // 
            // AssembleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 422);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.Name = "AssembleForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "组合商品";
            this.Load += new System.EventHandler(this.AssembleForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textKeyWord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gAssembleItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gVAssembleItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraVerticalGrid.VGridControl vGridControl1;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryRowSaleProps;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryRowNotKeyProps;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryRowKeyProps;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gAssembleItem;
        private DevExpress.XtraGrid.Views.Grid.GridView gVAssembleItem;
        private DevExpress.XtraGrid.Columns.GridColumn gcAssembleCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gcSimpleName;
        private DevExpress.XtraGrid.Columns.GridColumn gcPrice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gcAssembleDesc;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.TextEdit textKeyWord;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnAssociate;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryRowStockProps;
    }
}