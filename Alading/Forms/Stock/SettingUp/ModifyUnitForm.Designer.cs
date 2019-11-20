namespace Alading.Forms.Stock.SettingUp
{
    partial class ModifyUnitForm
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
            this.textEditNum = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEditUnitName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textUnitGroup = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelBaseUnit = new DevExpress.XtraEditors.LabelControl();
            this.textEditCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.memoExEditRemark = new DevExpress.XtraEditors.MemoExEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUnitName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textUnitGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoExEditRemark.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButtonBack
            // 
            this.simpleButtonBack.Location = new System.Drawing.Point(240, 177);
            this.simpleButtonBack.Name = "simpleButtonBack";
            this.simpleButtonBack.Size = new System.Drawing.Size(60, 23);
            this.simpleButtonBack.TabIndex = 20;
            this.simpleButtonBack.Text = "取消";
            this.simpleButtonBack.Click += new System.EventHandler(this.simpleButtonBack_Click);
            // 
            // simpleButtonConfirm
            // 
            this.simpleButtonConfirm.Location = new System.Drawing.Point(140, 177);
            this.simpleButtonConfirm.Name = "simpleButtonConfirm";
            this.simpleButtonConfirm.Size = new System.Drawing.Size(60, 23);
            this.simpleButtonConfirm.TabIndex = 19;
            this.simpleButtonConfirm.Text = "确定";
            this.simpleButtonConfirm.Click += new System.EventHandler(this.simpleButtonConfirm_Click);
            // 
            // textEditNum
            // 
            this.textEditNum.Location = new System.Drawing.Point(117, 107);
            this.textEditNum.Name = "textEditNum";
            this.textEditNum.Properties.Mask.EditMask = "n3";
            this.textEditNum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditNum.Size = new System.Drawing.Size(240, 21);
            this.textEditNum.TabIndex = 14;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(57, 108);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "换算比例：";
            // 
            // textEditUnitName
            // 
            this.textEditUnitName.Location = new System.Drawing.Point(117, 43);
            this.textEditUnitName.Name = "textEditUnitName";
            this.textEditUnitName.Size = new System.Drawing.Size(240, 21);
            this.textEditUnitName.TabIndex = 12;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(33, 43);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(84, 14);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "计量单位名称：";
            // 
            // textUnitGroup
            // 
            this.textUnitGroup.Location = new System.Drawing.Point(117, 11);
            this.textUnitGroup.Name = "textUnitGroup";
            this.textUnitGroup.Properties.ReadOnly = true;
            this.textUnitGroup.Size = new System.Drawing.Size(240, 21);
            this.textUnitGroup.TabIndex = 22;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(45, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 14);
            this.labelControl3.TabIndex = 21;
            this.labelControl3.Text = "单位组名称：";
            // 
            // labelBaseUnit
            // 
            this.labelBaseUnit.Location = new System.Drawing.Point(362, 111);
            this.labelBaseUnit.Name = "labelBaseUnit";
            this.labelBaseUnit.Size = new System.Drawing.Size(48, 14);
            this.labelBaseUnit.TabIndex = 23;
            this.labelBaseUnit.Text = "基本单位";
            // 
            // textEditCode
            // 
            this.textEditCode.Location = new System.Drawing.Point(117, 75);
            this.textEditCode.Name = "textEditCode";
            this.textEditCode.Properties.ReadOnly = true;
            this.textEditCode.Size = new System.Drawing.Size(240, 21);
            this.textEditCode.TabIndex = 25;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(33, 76);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(84, 14);
            this.labelControl4.TabIndex = 24;
            this.labelControl4.Text = "计量单位编码：";
            // 
            // memoExEditRemark
            // 
            this.memoExEditRemark.Location = new System.Drawing.Point(117, 139);
            this.memoExEditRemark.Name = "memoExEditRemark";
            this.memoExEditRemark.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.memoExEditRemark.Size = new System.Drawing.Size(240, 21);
            this.memoExEditRemark.TabIndex = 26;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(33, 140);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(84, 14);
            this.labelControl5.TabIndex = 27;
            this.labelControl5.Text = "计量单位备注：";
            // 
            // ModifyUnitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 214);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.memoExEditRemark);
            this.Controls.Add(this.textEditCode);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelBaseUnit);
            this.Controls.Add(this.textUnitGroup);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.simpleButtonBack);
            this.Controls.Add(this.simpleButtonConfirm);
            this.Controls.Add(this.textEditNum);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.textEditUnitName);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ModifyUnitForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改计量单位";
            ((System.ComponentModel.ISupportInitialize)(this.textEditNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUnitName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textUnitGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoExEditRemark.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButtonBack;
        private DevExpress.XtraEditors.SimpleButton simpleButtonConfirm;
        private DevExpress.XtraEditors.TextEdit textEditNum;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEditUnitName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textUnitGroup;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelBaseUnit;
        private DevExpress.XtraEditors.TextEdit textEditCode;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.MemoExEdit memoExEditRemark;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}