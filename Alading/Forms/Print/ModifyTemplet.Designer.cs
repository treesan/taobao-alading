namespace Alading.Forms.Print
{
    partial class ModifyTemplet
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
            this.mtCover1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.mtCover = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkItemNameLabel = new System.Windows.Forms.Label();
            this.checkTplateNameLabel = new System.Windows.Forms.Label();
            this.mtName = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.mtPicture = new DevExpress.XtraEditors.ButtonEdit();
            this.mtItem = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.mtSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.mtCompany = new DevExpress.XtraEditors.LookUpEdit();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.mtCover1.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mtCover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtPicture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.mtCover1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // mtCover1
            // 
            this.mtCover1.Controls.Add(this.controlContainer1);
            this.mtCover1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.mtCover1.ID = new System.Guid("c565720a-8c94-46fc-8e59-11d5c4685b78");
            this.mtCover1.Location = new System.Drawing.Point(0, 0);
            this.mtCover1.Name = "mtCover1";
            this.mtCover1.Options.ShowCloseButton = false;
            this.mtCover1.OriginalSize = new System.Drawing.Size(160, 200);
            this.mtCover1.Size = new System.Drawing.Size(160, 600);
            this.mtCover1.Text = "覆盖范围";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.mtCover);
            this.controlContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlContainer1.Location = new System.Drawing.Point(3, 25);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(154, 572);
            this.controlContainer1.TabIndex = 0;
            // 
            // mtCover
            // 
            this.mtCover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtCover.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
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
            this.mtCover.Location = new System.Drawing.Point(0, 0);
            this.mtCover.Name = "mtCover";
            this.mtCover.Size = new System.Drawing.Size(154, 572);
            this.mtCover.TabIndex = 1;
            this.mtCover.SelectedIndexChanged += new System.EventHandler(this.mtCover_SelectedIndexChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.checkItemNameLabel);
            this.groupControl1.Controls.Add(this.checkTplateNameLabel);
            this.groupControl1.Controls.Add(this.mtName);
            this.groupControl1.Controls.Add(this.simpleButton2);
            this.groupControl1.Controls.Add(this.mtPicture);
            this.groupControl1.Controls.Add(this.mtItem);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.mtSave);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.mtCompany);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(160, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(815, 220);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "修改模板";
            // 
            // checkItemNameLabel
            // 
            this.checkItemNameLabel.AutoSize = true;
            this.checkItemNameLabel.ForeColor = System.Drawing.Color.Red;
            this.checkItemNameLabel.Location = new System.Drawing.Point(604, 52);
            this.checkItemNameLabel.Name = "checkItemNameLabel";
            this.checkItemNameLabel.Size = new System.Drawing.Size(0, 14);
            this.checkItemNameLabel.TabIndex = 23;
            // 
            // checkTplateNameLabel
            // 
            this.checkTplateNameLabel.AutoSize = true;
            this.checkTplateNameLabel.ForeColor = System.Drawing.Color.Red;
            this.checkTplateNameLabel.Location = new System.Drawing.Point(128, 100);
            this.checkTplateNameLabel.Name = "checkTplateNameLabel";
            this.checkTplateNameLabel.Size = new System.Drawing.Size(24, 14);
            this.checkTplateNameLabel.TabIndex = 22;
            this.checkTplateNameLabel.Text = "[*]";
            // 
            // mtName
            // 
            this.mtName.Location = new System.Drawing.Point(176, 97);
            this.mtName.Name = "mtName";
            this.mtName.Size = new System.Drawing.Size(100, 21);
            this.mtName.TabIndex = 21;
            this.mtName.EditValueChanged += new System.EventHandler(this.mtName_EditValueChanged);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(382, 148);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 20;
            this.simpleButton2.Text = "新建标签";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // mtPicture
            // 
            this.mtPicture.Location = new System.Drawing.Point(176, 150);
            this.mtPicture.Name = "mtPicture";
            this.mtPicture.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.mtPicture.Size = new System.Drawing.Size(100, 21);
            this.mtPicture.TabIndex = 17;
            this.mtPicture.Click += new System.EventHandler(this.mtPicture_Click);
            // 
            // mtItem
            // 
            this.mtItem.Location = new System.Drawing.Point(498, 49);
            this.mtItem.Name = "mtItem";
            this.mtItem.Size = new System.Drawing.Size(100, 21);
            this.mtItem.TabIndex = 13;
            this.mtItem.EditValueChanged += new System.EventHandler(this.mtItem_EditValueChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(382, 52);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(76, 14);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "新建标签名称 ";
            // 
            // mtSave
            // 
            this.mtSave.Location = new System.Drawing.Point(523, 148);
            this.mtSave.Name = "mtSave";
            this.mtSave.Size = new System.Drawing.Size(75, 23);
            this.mtSave.TabIndex = 10;
            this.mtSave.Text = "保存修改";
            this.mtSave.Click += new System.EventHandler(this.mtSave_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(59, 52);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "物流公司";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(59, 153);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "背景图片";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(59, 100);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "模板名称";
            // 
            // mtCompany
            // 
            this.mtCompany.Location = new System.Drawing.Point(176, 49);
            this.mtCompany.Name = "mtCompany";
            this.mtCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mtCompany.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name", "name")});
            this.mtCompany.Properties.NullText = "";
            this.mtCompany.Properties.ShowHeader = false;
            this.mtCompany.Size = new System.Drawing.Size(100, 21);
            this.mtCompany.TabIndex = 11;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(0, -1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(821, 355);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 33;
            this.pictureBox2.TabStop = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.xtraScrollableControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(160, 220);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(815, 380);
            this.groupControl2.TabIndex = 4;
            this.groupControl2.Text = "模板";
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.pictureBox2);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(2, 23);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(811, 355);
            this.xtraScrollableControl1.TabIndex = 0;
            // 
            // ModifyTemplet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.mtCover1);
            this.Name = "ModifyTemplet";
            this.Size = new System.Drawing.Size(975, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.mtCover1.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mtCover)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtPicture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.xtraScrollableControl1.ResumeLayout(false);
            this.xtraScrollableControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel mtCover1;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private DevExpress.XtraEditors.CheckedListBoxControl mtCover;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ButtonEdit mtPicture;
        private DevExpress.XtraEditors.TextEdit mtItem;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton mtSave;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraEditors.LookUpEdit mtCompany;
        private DevExpress.XtraEditors.TextEdit mtName;
        private System.Windows.Forms.Label checkTplateNameLabel;
        private System.Windows.Forms.Label checkItemNameLabel;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
    }
}
