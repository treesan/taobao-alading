namespace Alading.Forms.Trade.Forms
{
    partial class TaobaoOrder
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
            this.tradePay1 = new Controls.TradePay();
            this.SuspendLayout();
            // 
            // tradePay1
            // 
            this.tradePay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tradePay1.Location = new System.Drawing.Point(0, 0);
            this.tradePay1.Name = "tradePay1";
            this.tradePay1.Size = new System.Drawing.Size(1016, 734);
            this.tradePay1.TabIndex = 1;
            // 
            // TaobaoOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 734);
            this.Controls.Add(this.tradePay1);
            this.Name = "TaobaoOrder";
            this.Text = "TaobaoOrder";
            this.ResumeLayout(false);

        }

        #endregion

        private Alading.Forms.Trade.Controls.TradePay tradePay1;
    }
}