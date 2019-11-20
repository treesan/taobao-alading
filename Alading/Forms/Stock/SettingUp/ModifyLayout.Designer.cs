namespace Alading.Forms.Stock.SettingUp
{
    partial class ModifyLayout
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textLayoutName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.memoLayoutRemark = new DevExpress.XtraEditors.MemoEdit();
            this.simpleCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxHouse = new DevExpress.XtraEditors.ComboBoxEdit();
            this.textEditLayoutCode = new DevExpress.XtraEditors.TextEdit();
            this.requiredLabel2 = new Alading.Controls.Common.RequiredLabel();
            this.requiredLabel1 = new Alading.Controls.Common.RequiredLabel();
            ((System.ComponentModel.ISupportInitialize)(this.textLayoutName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoLayoutRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxHouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLayoutCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(42, 40);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "库位名称:";
            // 
            // textLayoutName
            // 
            this.textLayoutName.Location = new System.Drawing.Point(100, 36);
            this.textLayoutName.Name = "textLayoutName";
            this.textLayoutName.Properties.MaxLength = 50;
            this.textLayoutName.Size = new System.Drawing.Size(261, 21);
            this.textLayoutName.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(40, 100);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "库位备注:";
            // 
            // memoLayoutRemark
            // 
            this.memoLayoutRemark.Location = new System.Drawing.Point(100, 91);
            this.memoLayoutRemark.Name = "memoLayoutRemark";
            this.memoLayoutRemark.Size = new System.Drawing.Size(261, 81);
            this.memoLayoutRemark.TabIndex = 5;
            // 
            // simpleCancel
            // 
            this.simpleCancel.Location = new System.Drawing.Point(212, 181);
            this.simpleCancel.Name = "simpleCancel";
            this.simpleCancel.Size = new System.Drawing.Size(60, 23);
            this.simpleCancel.TabIndex = 16;
            this.simpleCancel.Text = "取消";
            this.simpleCancel.Click += new System.EventHandler(this.simpleCancel_Click);
            // 
            // simpleConfirm
            // 
            this.simpleConfirm.Location = new System.Drawing.Point(128, 181);
            this.simpleConfirm.Name = "simpleConfirm";
            this.simpleConfirm.Size = new System.Drawing.Size(60, 23);
            this.simpleConfirm.TabIndex = 15;
            this.simpleConfirm.Text = "确定";
            this.simpleConfirm.Click += new System.EventHandler(this.simpleConfirm_Click);
            // 
            // comboBoxHouse
            // 
            this.comboBoxHouse.Location = new System.Drawing.Point(100, 11);
            this.comboBoxHouse.Name = "comboBoxHouse";
            this.comboBoxHouse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxHouse.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxHouse.Size = new System.Drawing.Size(261, 21);
            this.comboBoxHouse.TabIndex = 17;
            this.comboBoxHouse.SelectedIndexChanged += new System.EventHandler(this.comboBoxHouse_SelectedIndexChanged);
            // 
            // textEditLayoutCode
            // 
            this.textEditLayoutCode.Location = new System.Drawing.Point(100, 63);
            this.textEditLayoutCode.Name = "textEditLayoutCode";
            this.textEditLayoutCode.Properties.Mask.EditMask = "[^\\u4e00-\\u9fa5]+";
            this.textEditLayoutCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.textEditLayoutCode.Properties.MaxLength = 50;
            this.textEditLayoutCode.Properties.NullValuePrompt = "自动生成";
            this.textEditLayoutCode.Properties.NullValuePromptShowForEmptyValue = true;
            this.textEditLayoutCode.Size = new System.Drawing.Size(261, 21);
            this.textEditLayoutCode.TabIndex = 19;
            // 
            // requiredLabel2
            // 
            this.requiredLabel2.Location = new System.Drawing.Point(16, 70);
            this.requiredLabel2.Name = "requiredLabel2";
            this.requiredLabel2.Size = new System.Drawing.Size(80, 14);
            this.requiredLabel2.TabIndex = 26;
            this.requiredLabel2.Text = "库位编码";
            // 
            // requiredLabel1
            // 
            this.requiredLabel1.Location = new System.Drawing.Point(17, 10);
            this.requiredLabel1.Name = "requiredLabel1";
            this.requiredLabel1.Size = new System.Drawing.Size(80, 14);
            this.requiredLabel1.TabIndex = 25;
            this.requiredLabel1.Text = "仓库名称";
            // 
            // ModifyLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 212);
            this.Controls.Add(this.requiredLabel2);
            this.Controls.Add(this.requiredLabel1);
            this.Controls.Add(this.textEditLayoutCode);
            this.Controls.Add(this.comboBoxHouse);
            this.Controls.Add(this.simpleCancel);
            this.Controls.Add(this.simpleConfirm);
            this.Controls.Add(this.memoLayoutRemark);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.textLayoutName);
            this.Controls.Add(this.labelControl3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ModifyLayout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改库位信息";
            this.Load += new System.EventHandler(this.ModifyLayout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textLayoutName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoLayoutRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxHouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLayoutCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textLayoutName;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.MemoEdit memoLayoutRemark;
        private DevExpress.XtraEditors.SimpleButton simpleCancel;
        private DevExpress.XtraEditors.SimpleButton simpleConfirm;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxHouse;
        private DevExpress.XtraEditors.TextEdit textEditLayoutCode;
        private Alading.Controls.Common.RequiredLabel requiredLabel1;
        private Alading.Controls.Common.RequiredLabel requiredLabel2;
    }
}