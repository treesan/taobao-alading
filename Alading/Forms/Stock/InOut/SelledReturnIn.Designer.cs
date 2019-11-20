namespace Alading.Forms.Stock.InOut
{
    partial class SelledReturnIn
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.saveBtn = new DevExpress.XtraEditors.SimpleButton();
            this.cancleBtn = new DevExpress.XtraEditors.SimpleButton();
            this.selledReturnInCtrl = new Alading.Forms.Stock.Control.SelledReturnIn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1008, 50);
            this.panelControl1.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.saveBtn);
            this.panelControl2.Controls.Add(this.cancleBtn);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 580);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1008, 50);
            this.panelControl2.TabIndex = 2;
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.saveBtn.Location = new System.Drawing.Point(424, 14);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(70, 23);
            this.saveBtn.TabIndex = 4;
            this.saveBtn.Text = "入库";
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancleBtn
            // 
            this.cancleBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cancleBtn.Location = new System.Drawing.Point(515, 14);
            this.cancleBtn.Name = "cancleBtn";
            this.cancleBtn.Size = new System.Drawing.Size(70, 23);
            this.cancleBtn.TabIndex = 3;
            this.cancleBtn.Text = "取消";
            this.cancleBtn.Click += new System.EventHandler(this.cancleBtn_Click);
            // 
            // selledReturnInCtrl
            // 
            this.selledReturnInCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selledReturnInCtrl.Location = new System.Drawing.Point(0, 50);
            this.selledReturnInCtrl.Name = "selledReturnInCtrl";
            this.selledReturnInCtrl.Size = new System.Drawing.Size(1008, 530);
            this.selledReturnInCtrl.TabIndex = 3;
            // 
            // SelledReturnIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 630);
            this.Controls.Add(this.selledReturnInCtrl);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SelledReturnIn";
            this.ShowIcon = false;
            this.Text = "销售退货单";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SelledReturnIn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton saveBtn;
        private DevExpress.XtraEditors.SimpleButton cancleBtn;
        private Alading.Forms.Stock.Control.SelledReturnIn selledReturnInCtrl;
    }
}