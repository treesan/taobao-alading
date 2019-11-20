namespace Alading.Forms.Stock.SettingUp
{
    partial class NewGroupUnitForm
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
            this.simpleButtonBack = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.textEditGroupUnit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEditGroupName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGroupUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGroupName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButtonBack
            // 
            this.simpleButtonBack.Location = new System.Drawing.Point(384, 178);
            this.simpleButtonBack.Name = "simpleButtonBack";
            this.simpleButtonBack.Size = new System.Drawing.Size(60, 23);
            this.simpleButtonBack.TabIndex = 11;
            this.simpleButtonBack.Text = "取消";
            this.simpleButtonBack.Click += new System.EventHandler(this.simpleButtonBack_Click);
            // 
            // simpleButtonConfirm
            // 
            this.simpleButtonConfirm.Location = new System.Drawing.Point(291, 178);
            this.simpleButtonConfirm.Name = "simpleButtonConfirm";
            this.simpleButtonConfirm.Size = new System.Drawing.Size(60, 23);
            this.simpleButtonConfirm.TabIndex = 10;
            this.simpleButtonConfirm.Text = "确定";
            this.simpleButtonConfirm.Click += new System.EventHandler(this.simpleButtonConfirm_Click);
            // 
            // textEditGroupUnit
            // 
            this.textEditGroupUnit.Location = new System.Drawing.Point(142, 94);
            this.textEditGroupUnit.Name = "textEditGroupUnit";
            this.textEditGroupUnit.Size = new System.Drawing.Size(303, 21);
            this.textEditGroupUnit.TabIndex = 9;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(60, 97);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(84, 14);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "基本单位名称：";
            // 
            // textEditGroupName
            // 
            this.textEditGroupName.Location = new System.Drawing.Point(142, 31);
            this.textEditGroupName.Name = "textEditGroupName";
            this.textEditGroupName.Size = new System.Drawing.Size(303, 21);
            this.textEditGroupName.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(48, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(96, 14);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "计量单位组名称：";
            // 
            // NewGroupUnitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 234);
            this.Controls.Add(this.simpleButtonBack);
            this.Controls.Add(this.simpleButtonConfirm);
            this.Controls.Add(this.textEditGroupUnit);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.textEditGroupName);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.Name = "NewGroupUnitForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增计量单位组";
            ((System.ComponentModel.ISupportInitialize)(this.textEditGroupUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGroupName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButtonBack;
        private DevExpress.XtraEditors.SimpleButton simpleButtonConfirm;
        private DevExpress.XtraEditors.TextEdit textEditGroupUnit;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEditGroupName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}