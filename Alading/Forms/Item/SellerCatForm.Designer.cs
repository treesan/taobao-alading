namespace Alading.Forms.Item
{
    partial class SellerCatForm
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButtonDownLoad = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxEditShop = new DevExpress.XtraEditors.ComboBoxEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemNewFather = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemNewChid = new DevExpress.XtraBars.BarButtonItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonSave = new DevExpress.XtraEditors.SimpleButton();
            this.textEditSellerCatName = new DevExpress.XtraEditors.TextEdit();
            this.labelControlSellerCatName = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.treeListSellerCats = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditShop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSellerCatName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListSellerCats)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.simpleButtonDownLoad);
            this.panelControl1.Controls.Add(this.comboBoxEditShop);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(302, 41);
            this.panelControl1.TabIndex = 0;
            // 
            // simpleButtonDownLoad
            // 
            this.simpleButtonDownLoad.Location = new System.Drawing.Point(230, 12);
            this.simpleButtonDownLoad.Name = "simpleButtonDownLoad";
            this.simpleButtonDownLoad.Size = new System.Drawing.Size(47, 23);
            this.simpleButtonDownLoad.TabIndex = 2;
            this.simpleButtonDownLoad.Text = "下载";
            this.simpleButtonDownLoad.Click += new System.EventHandler(this.simpleButtonDownLoad_Click);
            // 
            // comboBoxEditShop
            // 
            this.comboBoxEditShop.Location = new System.Drawing.Point(82, 12);
            this.comboBoxEditShop.MenuManager = this.barManager1;
            this.comboBoxEditShop.Name = "comboBoxEditShop";
            this.comboBoxEditShop.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditShop.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditShop.Size = new System.Drawing.Size(134, 21);
            this.comboBoxEditShop.TabIndex = 1;
            this.comboBoxEditShop.SelectedValueChanged += new System.EventHandler(this.comboBoxEditShop_SelectedValueChanged);
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
            this.barButtonItemNewFather,
            this.barButtonItemNewChid});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.FloatLocation = new System.Drawing.Point(394, 281);
            this.bar1.FloatSize = new System.Drawing.Size(193, 26);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemNewFather),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemNewChid)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Tools";
            // 
            // barButtonItemNewFather
            // 
            this.barButtonItemNewFather.Caption = "添加新类目";
            this.barButtonItemNewFather.Id = 0;
            this.barButtonItemNewFather.Name = "barButtonItemNewFather";
            this.barButtonItemNewFather.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemNewFather_ItemClick);
            // 
            // barButtonItemNewChid
            // 
            this.barButtonItemNewChid.Caption = "添加子类目";
            this.barButtonItemNewChid.Id = 1;
            this.barButtonItemNewChid.Name = "barButtonItemNewChid";
            toolTipItem1.Text = "在添加子类目前，前选中一行父类目！";
            superToolTip1.Items.Add(toolTipItem1);
            this.barButtonItemNewChid.SuperTip = superToolTip1;
            this.barButtonItemNewChid.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemNewChid_ItemClick);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(302, 25);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(28, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "店铺选择";
            // 
            // simpleButtonSave
            // 
            this.simpleButtonSave.Location = new System.Drawing.Point(230, 39);
            this.simpleButtonSave.Name = "simpleButtonSave";
            this.simpleButtonSave.Size = new System.Drawing.Size(47, 25);
            this.simpleButtonSave.TabIndex = 5;
            this.simpleButtonSave.Text = " 保存";
            this.simpleButtonSave.Click += new System.EventHandler(this.simpleButtonSave_Click);
            // 
            // textEditSellerCatName
            // 
            this.textEditSellerCatName.Location = new System.Drawing.Point(82, 41);
            this.textEditSellerCatName.MenuManager = this.barManager1;
            this.textEditSellerCatName.Name = "textEditSellerCatName";
            this.textEditSellerCatName.Size = new System.Drawing.Size(122, 21);
            this.textEditSellerCatName.TabIndex = 4;
            // 
            // labelControlSellerCatName
            // 
            this.labelControlSellerCatName.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControlSellerCatName.Location = new System.Drawing.Point(16, 44);
            this.labelControlSellerCatName.Name = "labelControlSellerCatName";
            this.labelControlSellerCatName.Size = new System.Drawing.Size(48, 14);
            this.labelControlSellerCatName.TabIndex = 3;
            this.labelControlSellerCatName.Text = "修改类目";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.standaloneBarDockControl1);
            this.panelControl2.Controls.Add(this.simpleButtonSave);
            this.panelControl2.Controls.Add(this.labelControlSellerCatName);
            this.panelControl2.Controls.Add(this.textEditSellerCatName);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 263);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(302, 76);
            this.panelControl2.TabIndex = 6;
            // 
            // treeListSellerCats
            // 
            this.treeListSellerCats.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeListSellerCats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListSellerCats.Location = new System.Drawing.Point(0, 41);
            this.treeListSellerCats.Name = "treeListSellerCats";
            this.treeListSellerCats.Size = new System.Drawing.Size(302, 222);
            this.treeListSellerCats.TabIndex = 7;
            this.treeListSellerCats.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListSellerCats_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "我的类目";
            this.treeListColumn1.FieldName = "treeListColumn1";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.OptionsColumn.AllowMove = false;
            this.treeListColumn1.OptionsColumn.ReadOnly = true;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // SellerCatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 339);
            this.Controls.Add(this.treeListSellerCats);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.Name = "SellerCatForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "编辑类目";
            this.Load += new System.EventHandler(this.SellerCatForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditShop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSellerCatName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListSellerCats)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditShop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemNewFather;
        private DevExpress.XtraBars.BarButtonItem barButtonItemNewChid;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSave;
        private DevExpress.XtraEditors.TextEdit textEditSellerCatName;
        private DevExpress.XtraEditors.LabelControl labelControlSellerCatName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraTreeList.TreeList treeListSellerCats;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDownLoad;
    }
}