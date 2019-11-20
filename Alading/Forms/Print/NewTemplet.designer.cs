namespace Alading.Forms.Print
{
    partial class NewTemplet
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
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.ntCover = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkItemNameLabel = new System.Windows.Forms.Label();
            this.checkTplateNameLabel = new System.Windows.Forms.Label();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.ntPicture = new DevExpress.XtraEditors.ButtonEdit();
            this.ntItem = new DevExpress.XtraEditors.TextEdit();
            this.ntSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.ntName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.ntCompany = new DevExpress.XtraEditors.LookUpEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.navBarControl3 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup4 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer4 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.checkedListBoxControl2 = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel2.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ntCover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ntPicture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ntItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ntName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ntCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl3)).BeginInit();
            this.navBarControl3.SuspendLayout();
            this.navBarGroupControlContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl2)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel2});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel2.ID = new System.Guid("c1c28e6a-1619-4f74-a5c0-6f2f93fc36e0");
            this.dockPanel2.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.Options.ShowCloseButton = false;
            this.dockPanel2.OriginalSize = new System.Drawing.Size(160, 200);
            this.dockPanel2.Size = new System.Drawing.Size(160, 600);
            this.dockPanel2.Text = "覆盖范围";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.ntCover);
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(154, 572);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // ntCover
            // 
            this.ntCover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntCover.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("全选", "全选"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("取消全选", "取消全选"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("北京市", "北京市"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("天津市", "天津市"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("重庆市", "重庆市"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("上海市", "上海市"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("河北省", "河北省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("山西省", "山西省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("辽宁省", "辽宁省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("吉林省", "吉林省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("黑龙江省", "黑龙江省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("江苏省", "江苏省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("浙江省", "浙江省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("安徽省", "安徽省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("福建省", "福建省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("江西省", "江西省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("山东省", "山东省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("河南省", "河南省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("湖北省", "湖北省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("湖南省", "湖南省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("广东省", "广东省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("海南省", "海南省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("四川省", "四川省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("贵州省", "贵州省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("云南省", "云南省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("陕西省", "陕西省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("甘肃省", "甘肃省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("青海省", "青海省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("台湾省", "台湾省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("内蒙古自治区", "内蒙古自治区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("广西壮族自治区", "广西壮族自治区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("宁夏回族自治区", "宁夏回族自治区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("新疆维吾尔族自治区", "新疆维吾尔族自治区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("西藏自治区", "西藏自治区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("香港特别行政区", "香港特别行政区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("澳门特别行政区", "澳门特别行政区")});
            this.ntCover.Location = new System.Drawing.Point(0, 0);
            this.ntCover.Name = "ntCover";
            this.ntCover.Size = new System.Drawing.Size(154, 572);
            this.ntCover.TabIndex = 0;
            this.ntCover.SelectedIndexChanged += new System.EventHandler(this.ntCover_SelectedIndexChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.checkItemNameLabel);
            this.groupControl1.Controls.Add(this.checkTplateNameLabel);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.ntPicture);
            this.groupControl1.Controls.Add(this.ntItem);
            this.groupControl1.Controls.Add(this.ntSave);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.ntName);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.ntCompany);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(160, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(815, 220);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "新建模板";
            // 
            // checkItemNameLabel
            // 
            this.checkItemNameLabel.AutoSize = true;
            this.checkItemNameLabel.ForeColor = System.Drawing.Color.Red;
            this.checkItemNameLabel.Location = new System.Drawing.Point(449, 45);
            this.checkItemNameLabel.Name = "checkItemNameLabel";
            this.checkItemNameLabel.Size = new System.Drawing.Size(24, 14);
            this.checkItemNameLabel.TabIndex = 22;
            this.checkItemNameLabel.Text = "[*]";
            // 
            // checkTplateNameLabel
            // 
            this.checkTplateNameLabel.AutoSize = true;
            this.checkTplateNameLabel.ForeColor = System.Drawing.Color.Red;
            this.checkTplateNameLabel.Location = new System.Drawing.Point(117, 97);
            this.checkTplateNameLabel.Name = "checkTplateNameLabel";
            this.checkTplateNameLabel.Size = new System.Drawing.Size(24, 14);
            this.checkTplateNameLabel.TabIndex = 21;
            this.checkTplateNameLabel.Text = "[*]";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(367, 139);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 19;
            this.simpleButton1.Text = "新建标签";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.AllowDrop = true;
            this.labelControl5.Location = new System.Drawing.Point(367, 45);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(72, 14);
            this.labelControl5.TabIndex = 18;
            this.labelControl5.Text = "新建标签名称";
            // 
            // ntPicture
            // 
            this.ntPicture.Location = new System.Drawing.Point(169, 141);
            this.ntPicture.Name = "ntPicture";
            this.ntPicture.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.ntPicture.Size = new System.Drawing.Size(100, 21);
            this.ntPicture.TabIndex = 17;
            this.ntPicture.Click += new System.EventHandler(this.ntPicture_Click);
            // 
            // ntItem
            // 
            this.ntItem.Location = new System.Drawing.Point(479, 42);
            this.ntItem.Name = "ntItem";
            this.ntItem.Size = new System.Drawing.Size(100, 21);
            this.ntItem.TabIndex = 13;
            this.ntItem.EditValueChanged += new System.EventHandler(this.ntItem_EditValueChanged);
            // 
            // ntSave
            // 
            this.ntSave.Location = new System.Drawing.Point(504, 139);
            this.ntSave.Name = "ntSave";
            this.ntSave.Size = new System.Drawing.Size(75, 23);
            this.ntSave.TabIndex = 10;
            this.ntSave.Text = "保存模板";
            this.ntSave.Click += new System.EventHandler(this.ntSave_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(52, 144);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "背景图片";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(52, 97);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "模板名称";
            // 
            // ntName
            // 
            this.ntName.Location = new System.Drawing.Point(169, 94);
            this.ntName.Name = "ntName";
            this.ntName.Size = new System.Drawing.Size(100, 21);
            this.ntName.TabIndex = 1;
            this.ntName.EditValueChanged += new System.EventHandler(this.ntName_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.AllowDrop = true;
            this.labelControl4.Location = new System.Drawing.Point(52, 45);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "物流公司";
            this.labelControl4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.control_MouseMove);
            this.labelControl4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.control_MouseDown);
            // 
            // ntCompany
            // 
            this.ntCompany.Location = new System.Drawing.Point(169, 42);
            this.ntCompany.Name = "ntCompany";
            this.ntCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ntCompany.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name", "公司名称")});
            this.ntCompany.Properties.NullText = "";
            this.ntCompany.Properties.PopupSizeable = false;
            this.ntCompany.Properties.ShowHeader = false;
            this.ntCompany.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.ntCompany.Size = new System.Drawing.Size(100, 21);
            this.ntCompany.TabIndex = 20;
            this.ntCompany.TabStop = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.xtraScrollableControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(160, 220);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(815, 380);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "模板";
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.pictureBox1);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.FireScrollEventOnMouseWheel = true;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(2, 23);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(811, 355);
            this.xtraScrollableControl1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(821, 355);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.navBarControl3);
            this.dockPanel1_Container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(140, 600);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // navBarControl3
            // 
            this.navBarControl3.ActiveGroup = this.navBarGroup4;
            this.navBarControl3.ContentButtonHint = null;
            this.navBarControl3.Controls.Add(this.navBarGroupControlContainer4);
            this.navBarControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl3.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup4});
            this.navBarControl3.Location = new System.Drawing.Point(0, 0);
            this.navBarControl3.Name = "navBarControl3";
            this.navBarControl3.OptionsNavPane.ExpandedWidth = 194;
            this.navBarControl3.Size = new System.Drawing.Size(140, 600);
            this.navBarControl3.TabIndex = 1;
            this.navBarControl3.Text = "navBarControl3";
            // 
            // navBarGroup4
            // 
            this.navBarGroup4.Caption = "覆盖范围";
            this.navBarGroup4.ControlContainer = this.navBarGroupControlContainer4;
            this.navBarGroup4.Expanded = true;
            this.navBarGroup4.GroupClientHeight = 390;
            this.navBarGroup4.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup4.Name = "navBarGroup4";
            // 
            // navBarGroupControlContainer4
            // 
            this.navBarGroupControlContainer4.Controls.Add(this.checkedListBoxControl2);
            this.navBarGroupControlContainer4.Name = "navBarGroupControlContainer4";
            this.navBarGroupControlContainer4.Size = new System.Drawing.Size(136, 388);
            this.navBarGroupControlContainer4.TabIndex = 1;
            // 
            // checkedListBoxControl2
            // 
            this.checkedListBoxControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxControl2.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "北京市"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "天津市"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "重庆市"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "上海市"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "河北省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "山西省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "辽宁省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "吉林省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "黑龙江省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "江苏省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "浙江省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "安徽省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "福建省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "江西省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "山东省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "河南省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "湖北省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "湖南省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "广东省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "海南省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "四川省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "贵州省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "云南省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "陕西省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "甘肃省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "青海省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "台湾省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "内蒙古自治区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "广西壮族自治区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "宁夏回族自治区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "新疆维吾尔族自治区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "西藏自治区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "香港特别行政区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "澳门特别行政区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "北京市"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "天津市"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "重庆市"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "上海市"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "河北省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "山西省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "辽宁省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "吉林省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "黑龙江省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "江苏省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "浙江省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "安徽省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "福建省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "江西省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "山东省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "河南省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "湖北省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "湖南省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "广东省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "海南省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "四川省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "贵州省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "云南省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "陕西省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "甘肃省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "青海省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "台湾省"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "内蒙古自治区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "广西壮族自治区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "宁夏回族自治区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "新疆维吾尔族自治区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "西藏自治区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "香港特别行政区"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "澳门特别行政区")});
            this.checkedListBoxControl2.Location = new System.Drawing.Point(0, 0);
            this.checkedListBoxControl2.Name = "checkedListBoxControl2";
            this.checkedListBoxControl2.Size = new System.Drawing.Size(136, 388);
            this.checkedListBoxControl2.TabIndex = 1;
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("4e8b61ea-e60b-4e1c-9abd-5740bce79966");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(140, 200);
            this.dockPanel1.Size = new System.Drawing.Size(140, 600);
            this.dockPanel1.Text = "类型设置";
            // 
            // NewTemplet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.dockPanel2);
            this.Controls.Add(this.dockPanel1);
            this.Name = "NewTemplet";
            this.Size = new System.Drawing.Size(975, 600);
            this.Load += new System.EventHandler(this.NewTemplet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel2.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ntCover)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ntPicture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ntItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ntName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ntCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.xtraScrollableControl1.ResumeLayout(false);
            this.xtraScrollableControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl3)).EndInit();
            this.navBarControl3.ResumeLayout(false);
            this.navBarGroupControlContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl2)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit ntName;
        private DevExpress.XtraEditors.SimpleButton ntSave;
        private DevExpress.XtraEditors.TextEdit ntItem;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraNavBar.NavBarControl navBarControl3;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup4;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer4;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl2;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraEditors.CheckedListBoxControl ntCover;
        private DevExpress.XtraEditors.ButtonEdit ntPicture;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraEditors.LookUpEdit ntCompany;
        private System.Windows.Forms.Label checkTplateNameLabel;
        private System.Windows.Forms.Label checkItemNameLabel;
    }
}
