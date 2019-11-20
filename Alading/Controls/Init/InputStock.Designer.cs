namespace Alading.Controls.Init
{
    partial class InputStock
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
            this.progressBarInput = new DevExpress.XtraEditors.ProgressBarControl();
            this.progressBarUpdate = new DevExpress.XtraEditors.ProgressBarControl();
            this.labelInPut = new DevExpress.XtraEditors.LabelControl();
            this.labelUpdateItem = new DevExpress.XtraEditors.LabelControl();
            this.workerInput = new System.ComponentModel.BackgroundWorker();
            this.workerUpdate = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarUpdate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBarInput
            // 
            this.progressBarInput.Location = new System.Drawing.Point(104, 43);
            this.progressBarInput.Name = "progressBarInput";
            this.progressBarInput.Properties.EndColor = System.Drawing.SystemColors.HotTrack;
            this.progressBarInput.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.progressBarInput.Properties.ShowTitle = true;
            this.progressBarInput.Size = new System.Drawing.Size(316, 23);
            this.progressBarInput.TabIndex = 0;
            // 
            // progressBarUpdate
            // 
            this.progressBarUpdate.Location = new System.Drawing.Point(104, 94);
            this.progressBarUpdate.Name = "progressBarUpdate";
            this.progressBarUpdate.Properties.EndColor = System.Drawing.SystemColors.HotTrack;
            this.progressBarUpdate.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.progressBarUpdate.Properties.ShowTitle = true;
            this.progressBarUpdate.Size = new System.Drawing.Size(316, 23);
            this.progressBarUpdate.TabIndex = 1;
            // 
            // labelInPut
            // 
            this.labelInPut.Location = new System.Drawing.Point(24, 48);
            this.labelInPut.Name = "labelInPut";
            this.labelInPut.Size = new System.Drawing.Size(72, 14);
            this.labelInPut.TabIndex = 2;
            this.labelInPut.Text = "商品入库进度";
            // 
            // labelUpdateItem
            // 
            this.labelUpdateItem.Location = new System.Drawing.Point(23, 94);
            this.labelUpdateItem.Name = "labelUpdateItem";
            this.labelUpdateItem.Size = new System.Drawing.Size(72, 14);
            this.labelUpdateItem.TabIndex = 3;
            this.labelUpdateItem.Text = "更新商家编码";
            // 
            // workerInput
            // 
            this.workerInput.WorkerReportsProgress = true;
            this.workerInput.WorkerSupportsCancellation = true;
            this.workerInput.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerInput_DoWork);
            this.workerInput.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerInput_RunWorkerCompleted);
            this.workerInput.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.workerInput_ProgressChanged);
            // 
            // workerUpdate
            // 
            this.workerUpdate.WorkerReportsProgress = true;
            this.workerUpdate.WorkerSupportsCancellation = true;
            this.workerUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerUpdate_DoWork);
            this.workerUpdate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerUpdate_RunWorkerCompleted);
            this.workerUpdate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.workerUpdate_ProgressChanged);
            // 
            // InputStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 183);
            this.Controls.Add(this.labelUpdateItem);
            this.Controls.Add(this.labelInPut);
            this.Controls.Add(this.progressBarUpdate);
            this.Controls.Add(this.progressBarInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputStock";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "初始化库存";
            this.Load += new System.EventHandler(this.InputStock_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InputStock_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarUpdate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ProgressBarControl progressBarInput;
        private DevExpress.XtraEditors.ProgressBarControl progressBarUpdate;
        private DevExpress.XtraEditors.LabelControl labelInPut;
        private DevExpress.XtraEditors.LabelControl labelUpdateItem;
        private System.ComponentModel.BackgroundWorker workerInput;
        private System.ComponentModel.BackgroundWorker workerUpdate;
    }
}