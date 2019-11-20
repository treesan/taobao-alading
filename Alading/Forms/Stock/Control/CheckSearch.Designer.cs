namespace Alading.Forms.Stock.Control
{
    partial class CheckSearch
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckSearch));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.textKeyWord = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barBtnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gridCtrlProductCheck = new DevExpress.XtraGrid.GridControl();
            this.gridViewProductCheck = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcSkuOuterID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcModel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSkuProps = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLayoutName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSpecification = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSkuQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcProfitType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBoxType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.gcCheckQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridStockCheck = new DevExpress.XtraGrid.GridControl();
            this.gvStockCheck = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcCreated = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOperator = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCheckCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcHouseName = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textKeyWord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrlProductCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProductCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStockCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStockCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.textKeyWord);
            this.groupControl1.Controls.Add(this.btnSearch);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(924, 75);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "查询";
            // 
            // textKeyWord
            // 
            this.textKeyWord.Location = new System.Drawing.Point(100, 40);
            this.textKeyWord.MenuManager = this.barManager1;
            this.textKeyWord.Name = "textKeyWord";
            this.textKeyWord.Properties.NullValuePrompt = "请输入盘点单号";
            this.textKeyWord.Properties.NullValuePromptShowForEmptyValue = true;
            this.textKeyWord.Size = new System.Drawing.Size(400, 21);
            this.textKeyWord.TabIndex = 30;
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
            this.barBtnRefresh});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnRefresh)});
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Tools";
            // 
            // barBtnRefresh
            // 
            this.barBtnRefresh.Caption = "刷新";
            this.barBtnRefresh.Id = 2;
            this.barBtnRefresh.Name = "barBtnRefresh";
            this.barBtnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnRefresh_ItemClick);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(924, 22);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
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
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanel1.ID = new System.Guid("808bb8d8-bde9-4a39-a982-e0d59aa5598a");
            this.dockPanel1.Location = new System.Drawing.Point(0, 386);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 282);
            this.dockPanel1.Size = new System.Drawing.Size(924, 282);
            this.dockPanel1.Text = "盘点单详情";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.gridCtrlProductCheck);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(918, 254);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // gridCtrlProductCheck
            // 
            this.gridCtrlProductCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCtrlProductCheck.Location = new System.Drawing.Point(0, 0);
            this.gridCtrlProductCheck.MainView = this.gridViewProductCheck;
            this.gridCtrlProductCheck.Name = "gridCtrlProductCheck";
            this.gridCtrlProductCheck.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBoxType});
            this.gridCtrlProductCheck.Size = new System.Drawing.Size(918, 254);
            this.gridCtrlProductCheck.TabIndex = 2;
            this.gridCtrlProductCheck.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewProductCheck});
            // 
            // gridViewProductCheck
            // 
            this.gridViewProductCheck.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcSkuOuterID,
            this.gcName,
            this.gcModel,
            this.gcSkuProps,
            this.gcLayoutName,
            this.gcUnitName,
            this.gcSpecification,
            this.gcSkuQuantity,
            this.gcQuantity,
            this.gcProfitType,
            this.gcCheckQuantity});
            this.gridViewProductCheck.GridControl = this.gridCtrlProductCheck;
            this.gridViewProductCheck.Name = "gridViewProductCheck";
            this.gridViewProductCheck.OptionsBehavior.Editable = false;
            this.gridViewProductCheck.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewProductCheck.OptionsView.ColumnAutoWidth = false;
            this.gridViewProductCheck.OptionsView.ShowGroupPanel = false;
            // 
            // gcSkuOuterID
            // 
            this.gcSkuOuterID.Caption = "条形码";
            this.gcSkuOuterID.FieldName = "SkuOuterID";
            this.gcSkuOuterID.Name = "gcSkuOuterID";
            this.gcSkuOuterID.OptionsColumn.AllowEdit = false;
            this.gcSkuOuterID.Visible = true;
            this.gcSkuOuterID.VisibleIndex = 2;
            // 
            // gcName
            // 
            this.gcName.Caption = "商品名称";
            this.gcName.FieldName = "Name";
            this.gcName.Name = "gcName";
            this.gcName.OptionsColumn.AllowEdit = false;
            this.gcName.Visible = true;
            this.gcName.VisibleIndex = 0;
            // 
            // gcModel
            // 
            this.gcModel.Caption = "型号";
            this.gcModel.FieldName = "Model";
            this.gcModel.Name = "gcModel";
            this.gcModel.Visible = true;
            this.gcModel.VisibleIndex = 1;
            // 
            // gcSkuProps
            // 
            this.gcSkuProps.Caption = "属性";
            this.gcSkuProps.FieldName = "SkuProps";
            this.gcSkuProps.Name = "gcSkuProps";
            this.gcSkuProps.Visible = true;
            this.gcSkuProps.VisibleIndex = 3;
            // 
            // gcLayoutName
            // 
            this.gcLayoutName.Caption = "库位";
            this.gcLayoutName.FieldName = "LayoutName";
            this.gcLayoutName.Name = "gcLayoutName";
            this.gcLayoutName.Visible = true;
            this.gcLayoutName.VisibleIndex = 4;
            // 
            // gcUnitName
            // 
            this.gcUnitName.Caption = "单位";
            this.gcUnitName.FieldName = "StockUnitName";
            this.gcUnitName.Name = "gcUnitName";
            this.gcUnitName.Visible = true;
            this.gcUnitName.VisibleIndex = 5;
            // 
            // gcSpecification
            // 
            this.gcSpecification.Caption = "规格";
            this.gcSpecification.FieldName = "specification";
            this.gcSpecification.Name = "gcSpecification";
            this.gcSpecification.OptionsColumn.AllowEdit = false;
            this.gcSpecification.Visible = true;
            this.gcSpecification.VisibleIndex = 6;
            // 
            // gcSkuQuantity
            // 
            this.gcSkuQuantity.Caption = "帐面数量";
            this.gcSkuQuantity.FieldName = "SkuQuantity";
            this.gcSkuQuantity.Name = "gcSkuQuantity";
            this.gcSkuQuantity.OptionsColumn.AllowEdit = false;
            this.gcSkuQuantity.Visible = true;
            this.gcSkuQuantity.VisibleIndex = 7;
            // 
            // gcQuantity
            // 
            this.gcQuantity.Caption = "实际数量";
            this.gcQuantity.FieldName = "Quantity";
            this.gcQuantity.Name = "gcQuantity";
            this.gcQuantity.Visible = true;
            this.gcQuantity.VisibleIndex = 8;
            // 
            // gcProfitType
            // 
            this.gcProfitType.Caption = "报溢/报损";
            this.gcProfitType.ColumnEdit = this.repositoryItemImageComboBoxType;
            this.gcProfitType.FieldName = "ProfitType";
            this.gcProfitType.Name = "gcProfitType";
            this.gcProfitType.OptionsColumn.AllowEdit = false;
            this.gcProfitType.Visible = true;
            this.gcProfitType.VisibleIndex = 9;
            // 
            // repositoryItemImageComboBoxType
            // 
            this.repositoryItemImageComboBoxType.AutoHeight = false;
            this.repositoryItemImageComboBoxType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBoxType.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("正常", 0, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("报溢", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("报损", 2, 1)});
            this.repositoryItemImageComboBoxType.LargeImages = this.imageCollection1;
            this.repositoryItemImageComboBoxType.Name = "repositoryItemImageComboBoxType";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "CS3_CheckMark_006.png");
            this.imageCollection1.Images.SetKeyName(1, "015.png");
            // 
            // gcCheckQuantity
            // 
            this.gcCheckQuantity.Caption = "报溢/报损数量";
            this.gcCheckQuantity.FieldName = "CheckQuantity";
            this.gcCheckQuantity.Name = "gcCheckQuantity";
            this.gcCheckQuantity.OptionsColumn.AllowEdit = false;
            this.gcCheckQuantity.Visible = true;
            this.gcCheckQuantity.VisibleIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(520, 39);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 23);
            this.btnSearch.TabIndex = 29;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 14);
            this.label3.TabIndex = 28;
            this.label3.Text = "关键词";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.gridStockCheck);
            this.panelControl1.Controls.Add(this.standaloneBarDockControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 75);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(924, 311);
            this.panelControl1.TabIndex = 11;
            // 
            // gridStockCheck
            // 
            this.gridStockCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridStockCheck.Location = new System.Drawing.Point(0, 22);
            this.gridStockCheck.MainView = this.gvStockCheck;
            this.gridStockCheck.Name = "gridStockCheck";
            this.gridStockCheck.Size = new System.Drawing.Size(924, 289);
            this.gridStockCheck.TabIndex = 10;
            this.gridStockCheck.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStockCheck});
            // 
            // gvStockCheck
            // 
            this.gvStockCheck.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCreated,
            this.gcOperator,
            this.gcCheckCode,
            this.gcHouseName});
            this.gvStockCheck.GridControl = this.gridStockCheck;
            this.gvStockCheck.Name = "gvStockCheck";
            this.gvStockCheck.OptionsBehavior.Editable = false;
            this.gvStockCheck.OptionsView.ColumnAutoWidth = false;
            this.gvStockCheck.OptionsView.ShowGroupPanel = false;
            this.gvStockCheck.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvStockCheck_FocusedRowChanged);
            // 
            // gcCreated
            // 
            this.gcCreated.Caption = "创建日期";
            this.gcCreated.FieldName = "Created";
            this.gcCreated.Name = "gcCreated";
            this.gcCreated.Visible = true;
            this.gcCreated.VisibleIndex = 2;
            // 
            // gcOperator
            // 
            this.gcOperator.Caption = "经办人";
            this.gcOperator.FieldName = "nick";
            this.gcOperator.Name = "gcOperator";
            this.gcOperator.Visible = true;
            this.gcOperator.VisibleIndex = 3;
            // 
            // gcCheckCode
            // 
            this.gcCheckCode.Caption = "盘点单号";
            this.gcCheckCode.FieldName = "StockCheckCode";
            this.gcCheckCode.Name = "gcCheckCode";
            this.gcCheckCode.Visible = true;
            this.gcCheckCode.VisibleIndex = 0;
            // 
            // gcHouseName
            // 
            this.gcHouseName.Caption = "盘点仓库";
            this.gcHouseName.FieldName = "HouseName";
            this.gcHouseName.Name = "gcHouseName";
            this.gcHouseName.Visible = true;
            this.gcHouseName.VisibleIndex = 1;
            // 
            // CheckSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "CheckSearch";
            this.Size = new System.Drawing.Size(924, 668);
            this.Load += new System.EventHandler(this.CheckSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textKeyWord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrlProductCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProductCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridStockCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStockCheck)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridCtrlProductCheck;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProductCheck;
        private DevExpress.XtraGrid.Columns.GridColumn gcSkuOuterID;
        private DevExpress.XtraGrid.Columns.GridColumn gcName;
        private DevExpress.XtraGrid.Columns.GridColumn gcModel;
        private DevExpress.XtraGrid.Columns.GridColumn gcSkuProps;
        private DevExpress.XtraGrid.Columns.GridColumn gcLayoutName;
        private DevExpress.XtraGrid.Columns.GridColumn gcUnitName;
        private DevExpress.XtraGrid.Columns.GridColumn gcSpecification;
        private DevExpress.XtraGrid.Columns.GridColumn gcSkuQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn gcQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn gcProfitType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBoxType;
        private DevExpress.XtraGrid.Columns.GridColumn gcCheckQuantity;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.TextEdit textKeyWord;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraBars.BarButtonItem barBtnRefresh;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridStockCheck;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStockCheck;
        private DevExpress.XtraGrid.Columns.GridColumn gcCreated;
        private DevExpress.XtraGrid.Columns.GridColumn gcOperator;
        private DevExpress.XtraGrid.Columns.GridColumn gcCheckCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcHouseName;
    }
}
