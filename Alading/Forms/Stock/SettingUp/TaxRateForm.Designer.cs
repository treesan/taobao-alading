namespace Alading.Forms.Stock.SettingUp
{
    partial class TaxRateForm
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
            this.textEditTaxCode = new DevExpress.XtraEditors.TextEdit();
            this.textEditTaxName = new DevExpress.XtraEditors.TextEdit();
            this.textEditInputText = new DevExpress.XtraEditors.TextEdit();
            this.textEditOutputTax = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.checkEditIsDefault = new DevExpress.XtraEditors.CheckEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.memoEditRemark = new DevExpress.XtraEditors.MemoEdit();
            this.btnSaveAndNew = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.requiredLabel4 = new Alading.Controls.Common.RequiredLabel();
            this.requiredLabel3 = new Alading.Controls.Common.RequiredLabel();
            this.requiredLabel2 = new Alading.Controls.Common.RequiredLabel();
            this.requiredLabel1 = new Alading.Controls.Common.RequiredLabel();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTaxCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTaxName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditInputText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditOutputTax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditIsDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditRemark.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textEditTaxCode
            // 
            this.textEditTaxCode.Location = new System.Drawing.Point(89, 18);
            this.textEditTaxCode.Name = "textEditTaxCode";
            this.textEditTaxCode.Properties.Mask.EditMask = "[^\\u4e00-\\u9fa5]+";
            this.textEditTaxCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.textEditTaxCode.Properties.MaxLength = 50;
            this.textEditTaxCode.Size = new System.Drawing.Size(192, 21);
            this.textEditTaxCode.TabIndex = 1;
            // 
            // textEditTaxName
            // 
            this.textEditTaxName.Location = new System.Drawing.Point(380, 18);
            this.textEditTaxName.Name = "textEditTaxName";
            this.textEditTaxName.Properties.MaxLength = 50;
            this.textEditTaxName.Size = new System.Drawing.Size(192, 21);
            this.textEditTaxName.TabIndex = 3;
            // 
            // textEditInputText
            // 
            this.textEditInputText.Location = new System.Drawing.Point(89, 48);
            this.textEditInputText.Name = "textEditInputText";
            this.textEditInputText.Properties.Mask.EditMask = "\\d{1,2}(\\.\\d+)?";
            this.textEditInputText.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.textEditInputText.Properties.Mask.ShowPlaceHolders = false;
            this.textEditInputText.Size = new System.Drawing.Size(174, 21);
            this.textEditInputText.TabIndex = 5;
            // 
            // textEditOutputTax
            // 
            this.textEditOutputTax.Location = new System.Drawing.Point(381, 48);
            this.textEditOutputTax.Name = "textEditOutputTax";
            this.textEditOutputTax.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEditOutputTax.Properties.Mask.EditMask = "\\d{1,2}(\\.\\d+)?";
            this.textEditOutputTax.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.textEditOutputTax.Properties.Mask.ShowPlaceHolders = false;
            this.textEditOutputTax.Properties.MaxLength = 10;
            this.textEditOutputTax.Size = new System.Drawing.Size(173, 21);
            this.textEditOutputTax.TabIndex = 7;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(24, 82);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "计算公式";
            // 
            // checkEditIsDefault
            // 
            this.checkEditIsDefault.Location = new System.Drawing.Point(306, 82);
            this.checkEditIsDefault.Name = "checkEditIsDefault";
            this.checkEditIsDefault.Properties.Caption = "设置为默认税率";
            this.checkEditIsDefault.Size = new System.Drawing.Size(110, 19);
            this.checkEditIsDefault.TabIndex = 9;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(89, 80);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("<Null>", "税额=含税金额/(1+税率)*税率"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "税额=含税金额*税率")});
            this.radioGroup1.Size = new System.Drawing.Size(203, 60);
            this.radioGroup1.TabIndex = 10;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(48, 149);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 14);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "备注";
            // 
            // memoEditRemark
            // 
            this.memoEditRemark.Location = new System.Drawing.Point(89, 147);
            this.memoEditRemark.Name = "memoEditRemark";
            this.memoEditRemark.Size = new System.Drawing.Size(412, 64);
            this.memoEditRemark.TabIndex = 12;
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Location = new System.Drawing.Point(147, 227);
            this.btnSaveAndNew.Name = "btnSaveAndNew";
            this.btnSaveAndNew.Size = new System.Drawing.Size(69, 23);
            this.btnSaveAndNew.TabIndex = 22;
            this.btnSaveAndNew.Text = "保存并新建";
            this.btnSaveAndNew.Click += new System.EventHandler(this.btnSaveAndNew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(350, 227);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 23);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(247, 227);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 23);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(269, 51);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(12, 14);
            this.labelControl7.TabIndex = 23;
            this.labelControl7.Text = "%";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(560, 51);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(12, 14);
            this.labelControl8.TabIndex = 24;
            this.labelControl8.Text = "%";
            // 
            // requiredLabel4
            // 
            this.requiredLabel4.Location = new System.Drawing.Point(295, 55);
            this.requiredLabel4.Name = "requiredLabel4";
            this.requiredLabel4.Size = new System.Drawing.Size(80, 14);
            this.requiredLabel4.TabIndex = 28;
            this.requiredLabel4.Text = "销项税率";
            // 
            // requiredLabel3
            // 
            this.requiredLabel3.Location = new System.Drawing.Point(295, 21);
            this.requiredLabel3.Name = "requiredLabel3";
            this.requiredLabel3.Size = new System.Drawing.Size(80, 14);
            this.requiredLabel3.TabIndex = 27;
            this.requiredLabel3.Text = "税率名称";
            // 
            // requiredLabel2
            // 
            this.requiredLabel2.Location = new System.Drawing.Point(3, 51);
            this.requiredLabel2.Name = "requiredLabel2";
            this.requiredLabel2.Size = new System.Drawing.Size(80, 14);
            this.requiredLabel2.TabIndex = 26;
            this.requiredLabel2.Text = "进项税率";
            // 
            // requiredLabel1
            // 
            this.requiredLabel1.Location = new System.Drawing.Point(3, 21);
            this.requiredLabel1.Name = "requiredLabel1";
            this.requiredLabel1.Size = new System.Drawing.Size(80, 14);
            this.requiredLabel1.TabIndex = 25;
            this.requiredLabel1.Text = "税率编码";
            // 
            // TaxRateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 272);
            this.Controls.Add(this.requiredLabel4);
            this.Controls.Add(this.requiredLabel3);
            this.Controls.Add(this.requiredLabel2);
            this.Controls.Add(this.requiredLabel1);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.btnSaveAndNew);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.memoEditRemark);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.checkEditIsDefault);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.textEditOutputTax);
            this.Controls.Add(this.textEditInputText);
            this.Controls.Add(this.textEditTaxName);
            this.Controls.Add(this.textEditTaxCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TaxRateForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增税率";
            this.Load += new System.EventHandler(this.TaxRateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEditTaxCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTaxName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditInputText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditOutputTax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditIsDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditRemark.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEditTaxCode;
        private DevExpress.XtraEditors.TextEdit textEditTaxName;
        private DevExpress.XtraEditors.TextEdit textEditInputText;
        private DevExpress.XtraEditors.TextEdit textEditOutputTax;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckEdit checkEditIsDefault;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.MemoEdit memoEditRemark;
        private DevExpress.XtraEditors.SimpleButton btnSaveAndNew;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private Alading.Controls.Common.RequiredLabel requiredLabel1;
        private Alading.Controls.Common.RequiredLabel requiredLabel2;
        private Alading.Controls.Common.RequiredLabel requiredLabel3;
        private Alading.Controls.Common.RequiredLabel requiredLabel4;
    }
}