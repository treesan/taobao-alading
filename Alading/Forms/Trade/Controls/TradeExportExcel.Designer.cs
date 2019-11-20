namespace Alading.Forms.Trade.Controls
{
    partial class TradeExportExcel
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
            this.gcExportExcel = new DevExpress.XtraGrid.GridControl();
            this.gvExportExcel = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.RecordMessage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LockTradeUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LockTradeTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TradeIsLackProduct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LastReceiverName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.seller_nick = new DevExpress.XtraGrid.Columns.GridColumn();
            this.buyer_nick = new DevExpress.XtraGrid.Columns.GridColumn();
            this.created = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.EShopName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tradeTotalFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.post_fee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EShopType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CustomTid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HasTicket = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.AlipayNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.iid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sku_properties_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LeftQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.num = new DevExpress.XtraGrid.Columns.GridColumn();
            this.price = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.orderTotalFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.OrderType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ProductIsLack = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.gvOrderWaitConfirm = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gcExportExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExportExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrderWaitConfirm)).BeginInit();
            this.SuspendLayout();
            // 
            // gcExportExcel
            // 
            this.gcExportExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcExportExcel.Location = new System.Drawing.Point(0, 0);
            this.gcExportExcel.MainView = this.gvExportExcel;
            this.gcExportExcel.Name = "gcExportExcel";
            this.gcExportExcel.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2,
            this.repositoryItemImageComboBox1,
            this.repositoryItemImageEdit1,
            this.repositoryItemComboBox1,
            this.repositoryItemTextEdit2});
            this.gcExportExcel.Size = new System.Drawing.Size(1655, 406);
            this.gcExportExcel.TabIndex = 5;
            this.gcExportExcel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvExportExcel,
            this.gvOrderWaitConfirm});
            // 
            // gvExportExcel
            // 
            this.gvExportExcel.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExportExcel.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvExportExcel.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExportExcel.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            this.gvExportExcel.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvExportExcel.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gvExportExcel.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gvExportExcel.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(190)))), ((int)(((byte)(243)))));
            this.gvExportExcel.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gvExportExcel.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gvExportExcel.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvExportExcel.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gvExportExcel.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gvExportExcel.Appearance.Empty.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.gvExportExcel.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gvExportExcel.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.EvenRow.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExportExcel.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvExportExcel.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExportExcel.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            this.gvExportExcel.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvExportExcel.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gvExportExcel.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gvExportExcel.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White;
            this.gvExportExcel.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.gvExportExcel.Appearance.FixedLine.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.gvExportExcel.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gvExportExcel.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.gvExportExcel.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gvExportExcel.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExportExcel.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvExportExcel.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExportExcel.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvExportExcel.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvExportExcel.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gvExportExcel.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvExportExcel.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvExportExcel.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.gvExportExcel.Appearance.GroupButton.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gvExportExcel.Appearance.GroupButton.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvExportExcel.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvExportExcel.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gvExportExcel.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gvExportExcel.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gvExportExcel.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExportExcel.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvExportExcel.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvExportExcel.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gvExportExcel.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gvExportExcel.Appearance.GroupRow.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.GroupRow.Options.UseBorderColor = true;
            this.gvExportExcel.Appearance.GroupRow.Options.UseFont = true;
            this.gvExportExcel.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExportExcel.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvExportExcel.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExportExcel.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gvExportExcel.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvExportExcel.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gvExportExcel.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(153)))), ((int)(((byte)(228)))));
            this.gvExportExcel.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(224)))), ((int)(((byte)(251)))));
            this.gvExportExcel.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gvExportExcel.Appearance.HorzLine.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gvExportExcel.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gvExportExcel.Appearance.OddRow.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.OddRow.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.gvExportExcel.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(129)))), ((int)(((byte)(185)))));
            this.gvExportExcel.Appearance.Preview.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.Preview.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gvExportExcel.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.gvExportExcel.Appearance.Row.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.Row.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.RowSeparator.BackColor = System.Drawing.Color.White;
            this.gvExportExcel.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(126)))), ((int)(((byte)(217)))));
            this.gvExportExcel.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gvExportExcel.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvExportExcel.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvExportExcel.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gvExportExcel.Appearance.VertLine.Options.UseBackColor = true;
            this.gvExportExcel.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.RecordMessage,
            this.LockTradeUser,
            this.LockTradeTime,
            this.TradeIsLackProduct,
            this.type,
            this.LastReceiverName,
            this.seller_nick,
            this.buyer_nick,
            this.created,
            this.EShopName,
            this.tradeTotalFee,
            this.post_fee,
            this.EShopType,
            this.CustomTid,
            this.HasTicket,
            this.AlipayNo,
            this.iid,
            this.ItemName,
            this.sku_properties_name,
            this.LeftQuantity,
            this.num,
            this.price,
            this.gridColumn1,
            this.orderTotalFee,
            this.OrderType,
            this.ProductIsLack});
            this.gvExportExcel.GridControl = this.gcExportExcel;
            this.gvExportExcel.Name = "gvExportExcel";
            this.gvExportExcel.OptionsView.EnableAppearanceEvenRow = true;
            this.gvExportExcel.OptionsView.EnableAppearanceOddRow = true;
            this.gvExportExcel.OptionsView.ShowGroupPanel = false;
            // 
            // RecordMessage
            // 
            this.RecordMessage.Caption = "单号信息";
            this.RecordMessage.FieldName = "RecordMessage";
            this.RecordMessage.Name = "RecordMessage";
            this.RecordMessage.Visible = true;
            this.RecordMessage.VisibleIndex = 0;
            this.RecordMessage.Width = 74;
            // 
            // LockTradeUser
            // 
            this.LockTradeUser.Caption = "锁定备注";
            this.LockTradeUser.FieldName = "LockedUser";
            this.LockTradeUser.Name = "LockTradeUser";
            this.LockTradeUser.OptionsColumn.AllowEdit = false;
            this.LockTradeUser.Visible = true;
            this.LockTradeUser.VisibleIndex = 1;
            this.LockTradeUser.Width = 74;
            // 
            // LockTradeTime
            // 
            this.LockTradeTime.Caption = "锁定时间";
            this.LockTradeTime.FieldName = "LockedTime";
            this.LockTradeTime.Name = "LockTradeTime";
            // 
            // TradeIsLackProduct
            // 
            this.TradeIsLackProduct.Caption = "是否缺货";
            this.TradeIsLackProduct.FieldName = "TradeIsLackProduct";
            this.TradeIsLackProduct.Name = "TradeIsLackProduct";
            this.TradeIsLackProduct.OptionsColumn.AllowEdit = false;
            this.TradeIsLackProduct.Visible = true;
            this.TradeIsLackProduct.VisibleIndex = 2;
            this.TradeIsLackProduct.Width = 74;
            // 
            // type
            // 
            this.type.Caption = "交易类别";
            this.type.FieldName = "type";
            this.type.Name = "type";
            this.type.OptionsColumn.AllowEdit = false;
            this.type.OptionsColumn.ReadOnly = true;
            this.type.Visible = true;
            this.type.VisibleIndex = 3;
            this.type.Width = 74;
            // 
            // LastReceiverName
            // 
            this.LastReceiverName.Caption = "收货人姓名";
            this.LastReceiverName.FieldName = "receiver_name";
            this.LastReceiverName.Name = "LastReceiverName";
            this.LastReceiverName.OptionsColumn.AllowEdit = false;
            this.LastReceiverName.OptionsColumn.ReadOnly = true;
            this.LastReceiverName.Visible = true;
            this.LastReceiverName.VisibleIndex = 4;
            this.LastReceiverName.Width = 74;
            // 
            // seller_nick
            // 
            this.seller_nick.Caption = "卖家昵称";
            this.seller_nick.FieldName = "seller_nick";
            this.seller_nick.Name = "seller_nick";
            this.seller_nick.OptionsColumn.AllowEdit = false;
            this.seller_nick.OptionsColumn.ReadOnly = true;
            this.seller_nick.Visible = true;
            this.seller_nick.VisibleIndex = 5;
            this.seller_nick.Width = 74;
            // 
            // buyer_nick
            // 
            this.buyer_nick.Caption = "买家昵称";
            this.buyer_nick.FieldName = "buyer_nick";
            this.buyer_nick.Name = "buyer_nick";
            this.buyer_nick.OptionsColumn.AllowEdit = false;
            this.buyer_nick.OptionsColumn.ReadOnly = true;
            this.buyer_nick.Visible = true;
            this.buyer_nick.VisibleIndex = 6;
            this.buyer_nick.Width = 74;
            // 
            // created
            // 
            this.created.Caption = "创建时间";
            this.created.ColumnEdit = this.repositoryItemTextEdit2;
            this.created.FieldName = "created";
            this.created.Name = "created";
            this.created.OptionsColumn.AllowEdit = false;
            this.created.OptionsColumn.ReadOnly = true;
            this.created.Visible = true;
            this.created.VisibleIndex = 7;
            this.created.Width = 74;
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // EShopName
            // 
            this.EShopName.Caption = "所属网店";
            this.EShopName.FieldName = "ShopName";
            this.EShopName.Name = "EShopName";
            this.EShopName.OptionsColumn.AllowEdit = false;
            this.EShopName.OptionsColumn.ReadOnly = true;
            this.EShopName.Visible = true;
            this.EShopName.VisibleIndex = 8;
            this.EShopName.Width = 74;
            // 
            // tradeTotalFee
            // 
            this.tradeTotalFee.Caption = "交易实付金额";
            this.tradeTotalFee.FieldName = "TradePayment";
            this.tradeTotalFee.Name = "tradeTotalFee";
            this.tradeTotalFee.OptionsColumn.AllowEdit = false;
            this.tradeTotalFee.OptionsColumn.ReadOnly = true;
            this.tradeTotalFee.Visible = true;
            this.tradeTotalFee.VisibleIndex = 9;
            this.tradeTotalFee.Width = 74;
            // 
            // post_fee
            // 
            this.post_fee.Caption = "邮费";
            this.post_fee.FieldName = "post_fee";
            this.post_fee.Name = "post_fee";
            this.post_fee.OptionsColumn.AllowEdit = false;
            this.post_fee.OptionsColumn.ReadOnly = true;
            this.post_fee.Visible = true;
            this.post_fee.VisibleIndex = 11;
            this.post_fee.Width = 73;
            // 
            // EShopType
            // 
            this.EShopType.Caption = "所属网店类别";
            this.EShopType.FieldName = "ShopType";
            this.EShopType.Name = "EShopType";
            this.EShopType.OptionsColumn.AllowEdit = false;
            this.EShopType.OptionsColumn.ReadOnly = true;
            this.EShopType.Visible = true;
            this.EShopType.VisibleIndex = 10;
            this.EShopType.Width = 85;
            // 
            // CustomTid
            // 
            this.CustomTid.Caption = "交易ID";
            this.CustomTid.FieldName = "CustomTid";
            this.CustomTid.Name = "CustomTid";
            this.CustomTid.OptionsColumn.AllowEdit = false;
            this.CustomTid.OptionsColumn.ReadOnly = true;
            // 
            // HasTicket
            // 
            this.HasTicket.Caption = "是否开票";
            this.HasTicket.ColumnEdit = this.repositoryItemComboBox1;
            this.HasTicket.FieldName = "HasInvoice";
            this.HasTicket.Name = "HasTicket";
            this.HasTicket.Visible = true;
            this.HasTicket.VisibleIndex = 12;
            this.HasTicket.Width = 73;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "是",
            "否"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // AlipayNo
            // 
            this.AlipayNo.Caption = "买家支付宝账号";
            this.AlipayNo.FieldName = "AlipayNo";
            this.AlipayNo.Name = "AlipayNo";
            // 
            // iid
            // 
            this.iid.Caption = "商品ID";
            this.iid.FieldName = "iid";
            this.iid.Name = "iid";
            this.iid.Visible = true;
            this.iid.VisibleIndex = 13;
            this.iid.Width = 73;
            // 
            // ItemName
            // 
            this.ItemName.Caption = "商品名";
            this.ItemName.FieldName = "ItemName";
            this.ItemName.Name = "ItemName";
            this.ItemName.Visible = true;
            this.ItemName.VisibleIndex = 14;
            this.ItemName.Width = 73;
            // 
            // sku_properties_name
            // 
            this.sku_properties_name.Caption = "商品属性";
            this.sku_properties_name.FieldName = "sku_properties_name";
            this.sku_properties_name.Name = "sku_properties_name";
            this.sku_properties_name.Visible = true;
            this.sku_properties_name.VisibleIndex = 15;
            this.sku_properties_name.Width = 73;
            // 
            // LeftQuantity
            // 
            this.LeftQuantity.Caption = "库存剩余数量";
            this.LeftQuantity.FieldName = "LeftQuantity";
            this.LeftQuantity.Name = "LeftQuantity";
            this.LeftQuantity.Visible = true;
            this.LeftQuantity.VisibleIndex = 16;
            this.LeftQuantity.Width = 73;
            // 
            // num
            // 
            this.num.Caption = "购买数量";
            this.num.FieldName = "num";
            this.num.Name = "num";
            this.num.Visible = true;
            this.num.VisibleIndex = 17;
            this.num.Width = 73;
            // 
            // price
            // 
            this.price.Caption = "商品价格";
            this.price.FieldName = "price";
            this.price.Name = "price";
            this.price.Visible = true;
            this.price.VisibleIndex = 18;
            this.price.Width = 73;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "调整价格";
            this.gridColumn1.FieldName = "adjust_fee";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 22;
            // 
            // orderTotalFee
            // 
            this.orderTotalFee.Caption = "订单实付金额";
            this.orderTotalFee.FieldName = "payment";
            this.orderTotalFee.Name = "orderTotalFee";
            this.orderTotalFee.Visible = true;
            this.orderTotalFee.VisibleIndex = 19;
            this.orderTotalFee.Width = 73;
            // 
            // OrderType
            // 
            this.OrderType.Caption = "商品价格";
            this.OrderType.FieldName = "OrderType";
            this.OrderType.Name = "OrderType";
            this.OrderType.Visible = true;
            this.OrderType.VisibleIndex = 20;
            this.OrderType.Width = 73;
            // 
            // ProductIsLack
            // 
            this.ProductIsLack.Caption = "缺货与否";
            this.ProductIsLack.FieldName = "ProductIsLack";
            this.ProductIsLack.Name = "ProductIsLack";
            this.ProductIsLack.Visible = true;
            this.ProductIsLack.VisibleIndex = 21;
            this.ProductIsLack.Width = 79;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("有货", "有货", 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("缺货", "缺货", 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("部分缺货", "部分缺货", 0)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // repositoryItemImageEdit1
            // 
            this.repositoryItemImageEdit1.AutoHeight = false;
            this.repositoryItemImageEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageEdit1.Name = "repositoryItemImageEdit1";
            // 
            // gvOrderWaitConfirm
            // 
            this.gvOrderWaitConfirm.GridControl = this.gcExportExcel;
            this.gvOrderWaitConfirm.Name = "gvOrderWaitConfirm";
            this.gvOrderWaitConfirm.OptionsView.ShowGroupPanel = false;
            // 
            // TradeExportExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcExportExcel);
            this.Name = "TradeExportExcel";
            this.Size = new System.Drawing.Size(1655, 406);
            ((System.ComponentModel.ISupportInitialize)(this.gcExportExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExportExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrderWaitConfirm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcExportExcel;
        private DevExpress.XtraGrid.Views.Grid.GridView gvExportExcel;
        private DevExpress.XtraGrid.Columns.GridColumn RecordMessage;
        private DevExpress.XtraGrid.Columns.GridColumn LockTradeUser;
        private DevExpress.XtraGrid.Columns.GridColumn LockTradeTime;
        private DevExpress.XtraGrid.Columns.GridColumn TradeIsLackProduct;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn type;
        private DevExpress.XtraGrid.Columns.GridColumn LastReceiverName;
        private DevExpress.XtraGrid.Columns.GridColumn seller_nick;
        private DevExpress.XtraGrid.Columns.GridColumn buyer_nick;
        private DevExpress.XtraGrid.Columns.GridColumn created;
        private DevExpress.XtraGrid.Columns.GridColumn EShopName;
        private DevExpress.XtraGrid.Columns.GridColumn tradeTotalFee;
        private DevExpress.XtraGrid.Columns.GridColumn post_fee;
        private DevExpress.XtraGrid.Columns.GridColumn EShopType;
        private DevExpress.XtraGrid.Columns.GridColumn CustomTid;
        private DevExpress.XtraGrid.Columns.GridColumn HasTicket;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn AlipayNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit repositoryItemImageEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOrderWaitConfirm;
        private DevExpress.XtraGrid.Columns.GridColumn iid;
        private DevExpress.XtraGrid.Columns.GridColumn ItemName;
        private DevExpress.XtraGrid.Columns.GridColumn sku_properties_name;
        private DevExpress.XtraGrid.Columns.GridColumn LeftQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn num;
        private DevExpress.XtraGrid.Columns.GridColumn price;
        private DevExpress.XtraGrid.Columns.GridColumn orderTotalFee;
        private DevExpress.XtraGrid.Columns.GridColumn OrderType;
        private DevExpress.XtraGrid.Columns.GridColumn ProductIsLack;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;

    }
}
