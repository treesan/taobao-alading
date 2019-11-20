namespace Alading.Forms.Stock.InOut
{
    partial class ProduceInForm
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
            this.SaveAndCreateBtn = new DevExpress.XtraEditors.SimpleButton();
            this.SaveBtn = new DevExpress.XtraEditors.SimpleButton();
            this.CancelBtn = new DevExpress.XtraEditors.SimpleButton();
            this.produceInCtrl = new Alading.Forms.Stock.Control.ProduceIn();
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
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.SaveAndCreateBtn);
            this.panelControl2.Controls.Add(this.SaveBtn);
            this.panelControl2.Controls.Add(this.CancelBtn);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 580);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1008, 50);
            this.panelControl2.TabIndex = 1;
            // 
            // SaveAndCreateBtn
            // 
            this.SaveAndCreateBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SaveAndCreateBtn.Location = new System.Drawing.Point(378, 14);
            this.SaveAndCreateBtn.Name = "SaveAndCreateBtn";
            this.SaveAndCreateBtn.Size = new System.Drawing.Size(70, 23);
            this.SaveAndCreateBtn.TabIndex = 5;
            this.SaveAndCreateBtn.Text = "保存并新建";
            this.SaveAndCreateBtn.Click += new System.EventHandler(this.SaveAndCreateBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SaveBtn.Location = new System.Drawing.Point(469, 14);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(70, 23);
            this.SaveBtn.TabIndex = 4;
            this.SaveBtn.Text = "保存";
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CancelBtn.Location = new System.Drawing.Point(560, 14);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(70, 23);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // produceInCtrl
            // 
            this.produceInCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.produceInCtrl.Location = new System.Drawing.Point(0, 50);
            this.produceInCtrl.Name = "produceInCtrl";
            this.produceInCtrl.Size = new System.Drawing.Size(1008, 530);
            this.produceInCtrl.TabIndex = 2;
            // 
            // ProduceInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 630);
            this.Controls.Add(this.produceInCtrl);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ProduceInForm";
            this.ShowIcon = false;
            this.Text = "生产入库单";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ProduceInForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Alading.Forms.Stock.Control.ProduceIn produceInCtrl;
        private DevExpress.XtraEditors.SimpleButton CancelBtn;
        private DevExpress.XtraEditors.SimpleButton SaveAndCreateBtn;
        private DevExpress.XtraEditors.SimpleButton SaveBtn;
    }
}