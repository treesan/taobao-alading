namespace Alading.Forms.Stock.Assemble
{
    partial class AssembleAdd
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
            this.groupControlAdd = new DevExpress.XtraEditors.GroupControl();
            this.assembleAdd1 = new Alading.Forms.Stock.Control.AssembleAdd();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.simpleBtnSaveAdd = new DevExpress.XtraEditors.SimpleButton();
            this.simpleBtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.simpleBtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlAdd)).BeginInit();
            this.groupControlAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // groupControlAdd
            // 
            this.groupControlAdd.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupControlAdd.Controls.Add(this.assembleAdd1);
            this.groupControlAdd.Controls.Add(this.groupControl1);
            this.groupControlAdd.Controls.Add(this.groupControl2);
            this.groupControlAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlAdd.Location = new System.Drawing.Point(0, 0);
            this.groupControlAdd.Name = "groupControlAdd";
            this.groupControlAdd.Size = new System.Drawing.Size(1008, 730);
            this.groupControlAdd.TabIndex = 1;
            this.groupControlAdd.Text = "groupControl1";
            // 
            // assembleAdd1
            // 
            this.assembleAdd1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assembleAdd1.Location = new System.Drawing.Point(0, 39);
            this.assembleAdd1.Name = "assembleAdd1";
            this.assembleAdd1.Size = new System.Drawing.Size(1008, 648);
            this.assembleAdd1.TabIndex = 27;
            // 
            // groupControl1
            // 
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupControl1.Controls.Add(this.simpleBtnSaveAdd);
            this.groupControl1.Controls.Add(this.simpleBtnSave);
            this.groupControl1.Controls.Add(this.simpleBtnCancel);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl1.Location = new System.Drawing.Point(0, 687);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1008, 43);
            this.groupControl1.TabIndex = 26;
            this.groupControl1.Text = "groupControl1";
            // 
            // simpleBtnSaveAdd
            // 
            this.simpleBtnSaveAdd.Location = new System.Drawing.Point(607, 9);
            this.simpleBtnSaveAdd.Name = "simpleBtnSaveAdd";
            this.simpleBtnSaveAdd.Size = new System.Drawing.Size(75, 25);
            this.simpleBtnSaveAdd.TabIndex = 2;
            this.simpleBtnSaveAdd.Text = "保存并新增";
            this.simpleBtnSaveAdd.Click += new System.EventHandler(this.simpleBtnSaveAdd_Click);
            // 
            // simpleBtnSave
            // 
            this.simpleBtnSave.Location = new System.Drawing.Point(516, 9);
            this.simpleBtnSave.Name = "simpleBtnSave";
            this.simpleBtnSave.Size = new System.Drawing.Size(75, 25);
            this.simpleBtnSave.TabIndex = 0;
            this.simpleBtnSave.Text = "保存";
            this.simpleBtnSave.Click += new System.EventHandler(this.simpleBtnSave_Click);
            // 
            // simpleBtnCancel
            // 
            this.simpleBtnCancel.Location = new System.Drawing.Point(696, 9);
            this.simpleBtnCancel.Name = "simpleBtnCancel";
            this.simpleBtnCancel.Size = new System.Drawing.Size(75, 25);
            this.simpleBtnCancel.TabIndex = 1;
            this.simpleBtnCancel.Text = "取消";
            this.simpleBtnCancel.Click += new System.EventHandler(this.simpleBtnCancel_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1008, 39);
            this.groupControl2.TabIndex = 24;
            this.groupControl2.Text = "groupControl2";
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.MaxItemId = 6;
            // 
            // AssembleAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.groupControlAdd);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AssembleAdd";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建组合商品";
            this.Load += new System.EventHandler(this.AssembleAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlAdd)).EndInit();
            this.groupControlAdd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraEditors.GroupControl groupControlAdd;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private Alading.Forms.Stock.Control.AssembleAdd assembleAdd1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleBtnSaveAdd;
        private DevExpress.XtraEditors.SimpleButton simpleBtnSave;
        private DevExpress.XtraEditors.SimpleButton simpleBtnCancel;

    }
}