namespace Alading.Forms.Stock.SettingUp
{
    partial class ModifyGroupUnitForm
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
            this.textEditGroupName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.memoRemark = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEditUnitGroupCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGroupUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGroupName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUnitGroupCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButtonBack
            // 
            this.simpleButtonBack.Location = new System.Drawing.Point(195, 201);
            this.simpleButtonBack.Name = "simpleButtonBack";
            this.simpleButtonBack.Size = new System.Drawing.Size(60, 23);
            this.simpleButtonBack.TabIndex = 17;
            this.simpleButtonBack.Text = "取消";
            this.simpleButtonBack.Click += new System.EventHandler(this.simpleButtonBack_Click);
            // 
            // simpleButtonConfirm
            // 
            this.simpleButtonConfirm.Location = new System.Drawing.Point(108, 201);
            this.simpleButtonConfirm.Name = "simpleButtonConfirm";
            this.simpleButtonConfirm.Size = new System.Drawing.Size(60, 23);
            this.simpleButtonConfirm.TabIndex = 16;
            this.simpleButtonConfirm.Text = "确定";
            this.simpleButtonConfirm.Click += new System.EventHandler(this.simpleButtonConfirm_Click);
            // 
            // textEditGroupUnit
            // 
            this.textEditGroupUnit.Location = new System.Drawing.Point(105, 70);
            this.textEditGroupUnit.Name = "textEditGroupUnit";
            this.textEditGroupUnit.Properties.ReadOnly = true;
            this.textEditGroupUnit.Size = new System.Drawing.Size(207, 21);
            this.textEditGroupUnit.TabIndex = 15;
            // 
            // textEditGroupName
            // 
            this.textEditGroupName.Location = new System.Drawing.Point(106, 11);
            this.textEditGroupName.Name = "textEditGroupName";
            this.textEditGroupName.Size = new System.Drawing.Size(207, 21);
            this.textEditGroupName.TabIndex = 13;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(31, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "单位组名称：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(18, 74);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(84, 14);
            this.labelControl4.TabIndex = 20;
            this.labelControl4.Text = "基本单位名称：";
            // 
            // memoRemark
            // 
            this.memoRemark.Location = new System.Drawing.Point(106, 102);
            this.memoRemark.Name = "memoRemark";
            this.memoRemark.Size = new System.Drawing.Size(207, 87);
            this.memoRemark.TabIndex = 22;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(68, 108);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 14);
            this.labelControl2.TabIndex = 21;
            this.labelControl2.Text = "备注:";
            // 
            // textEditUnitGroupCode
            // 
            this.textEditUnitGroupCode.Location = new System.Drawing.Point(106, 42);
            this.textEditUnitGroupCode.Name = "textEditUnitGroupCode";
            this.textEditUnitGroupCode.Properties.ReadOnly = true;
            this.textEditUnitGroupCode.Size = new System.Drawing.Size(207, 21);
            this.textEditUnitGroupCode.TabIndex = 24;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(31, 44);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 14);
            this.labelControl3.TabIndex = 23;
            this.labelControl3.Text = "单位组编码：";
            // 
            // ModifyGroupUnitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 233);
            this.Controls.Add(this.textEditUnitGroupCode);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.memoRemark);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.simpleButtonBack);
            this.Controls.Add(this.simpleButtonConfirm);
            this.Controls.Add(this.textEditGroupUnit);
            this.Controls.Add(this.textEditGroupName);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ModifyGroupUnitForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改计量单位组";
            this.Load += new System.EventHandler(this.ModifyGroupUnitForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEditGroupUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGroupName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUnitGroupCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButtonBack;
        private DevExpress.XtraEditors.SimpleButton simpleButtonConfirm;
        private DevExpress.XtraEditors.TextEdit textEditGroupUnit;
        private DevExpress.XtraEditors.TextEdit textEditGroupName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.MemoEdit memoRemark;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEditUnitGroupCode;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}