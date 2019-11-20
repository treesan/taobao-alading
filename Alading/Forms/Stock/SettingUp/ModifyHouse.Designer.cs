namespace Alading.Forms.Stock.SettingUp
{
    partial class ModifyHouse
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
            this.textHouseAddr = new DevExpress.XtraEditors.TextEdit();
            this.textHouseName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.memoHouseRemark = new DevExpress.XtraEditors.MemoEdit();
            this.simpleCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.textHouseCode = new DevExpress.XtraEditors.TextEdit();
            this.textEditHouseContact = new DevExpress.XtraEditors.TextEdit();
            this.textEditHouseTel = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.requiredLabel2 = new Alading.Controls.Common.RequiredLabel();
            this.requiredLabel1 = new Alading.Controls.Common.RequiredLabel();
            ((System.ComponentModel.ISupportInitialize)(this.textHouseAddr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textHouseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoHouseRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textHouseCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditHouseContact.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditHouseTel.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textHouseAddr
            // 
            this.textHouseAddr.Location = new System.Drawing.Point(89, 68);
            this.textHouseAddr.Name = "textHouseAddr";
            this.textHouseAddr.Properties.MaxLength = 200;
            this.textHouseAddr.Size = new System.Drawing.Size(386, 21);
            this.textHouseAddr.TabIndex = 11;
            // 
            // textHouseName
            // 
            this.textHouseName.Location = new System.Drawing.Point(89, 8);
            this.textHouseName.Name = "textHouseName";
            this.textHouseName.Properties.MaxLength = 50;
            this.textHouseName.Size = new System.Drawing.Size(150, 21);
            this.textHouseName.TabIndex = 10;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(55, 102);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 14);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "备注:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(31, 71);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "仓库地址:";
            // 
            // memoHouseRemark
            // 
            this.memoHouseRemark.Location = new System.Drawing.Point(89, 100);
            this.memoHouseRemark.Name = "memoHouseRemark";
            this.memoHouseRemark.Size = new System.Drawing.Size(386, 91);
            this.memoHouseRemark.TabIndex = 12;
            // 
            // simpleCancel
            // 
            this.simpleCancel.Location = new System.Drawing.Point(270, 197);
            this.simpleCancel.Name = "simpleCancel";
            this.simpleCancel.Size = new System.Drawing.Size(60, 23);
            this.simpleCancel.TabIndex = 16;
            this.simpleCancel.Text = "取消";
            this.simpleCancel.Click += new System.EventHandler(this.simpleCancel_Click);
            // 
            // simpleConfirm
            // 
            this.simpleConfirm.Location = new System.Drawing.Point(170, 197);
            this.simpleConfirm.Name = "simpleConfirm";
            this.simpleConfirm.Size = new System.Drawing.Size(60, 23);
            this.simpleConfirm.TabIndex = 15;
            this.simpleConfirm.Text = "确定";
            this.simpleConfirm.Click += new System.EventHandler(this.simpleConfirm_Click);
            // 
            // textHouseCode
            // 
            this.textHouseCode.Location = new System.Drawing.Point(325, 8);
            this.textHouseCode.Name = "textHouseCode";
            this.textHouseCode.Properties.Mask.EditMask = "[^\\u4e00-\\u9fa5]+";
            this.textHouseCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.textHouseCode.Properties.MaxLength = 50;
            this.textHouseCode.Properties.NullValuePrompt = "自动生成";
            this.textHouseCode.Properties.NullValuePromptShowForEmptyValue = true;
            this.textHouseCode.Size = new System.Drawing.Size(150, 21);
            this.textHouseCode.TabIndex = 18;
            // 
            // textEditHouseContact
            // 
            this.textEditHouseContact.Location = new System.Drawing.Point(89, 38);
            this.textEditHouseContact.Name = "textEditHouseContact";
            this.textEditHouseContact.Properties.MaxLength = 50;
            this.textEditHouseContact.Size = new System.Drawing.Size(150, 21);
            this.textEditHouseContact.TabIndex = 20;
            // 
            // textEditHouseTel
            // 
            this.textEditHouseTel.Location = new System.Drawing.Point(325, 38);
            this.textEditHouseTel.Name = "textEditHouseTel";
            this.textEditHouseTel.Properties.MaxLength = 50;
            this.textEditHouseTel.Size = new System.Drawing.Size(150, 21);
            this.textEditHouseTel.TabIndex = 22;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(257, 41);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(64, 14);
            this.labelControl6.TabIndex = 21;
            this.labelControl6.Text = "联系人电话:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(19, 41);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(64, 14);
            this.labelControl5.TabIndex = 19;
            this.labelControl5.Text = "联系人名称:";
            // 
            // requiredLabel2
            // 
            this.requiredLabel2.Location = new System.Drawing.Point(245, 11);
            this.requiredLabel2.Name = "requiredLabel2";
            this.requiredLabel2.Size = new System.Drawing.Size(80, 14);
            this.requiredLabel2.TabIndex = 25;
            this.requiredLabel2.Text = "仓库编码";
            // 
            // requiredLabel1
            // 
            this.requiredLabel1.Location = new System.Drawing.Point(7, 11);
            this.requiredLabel1.Name = "requiredLabel1";
            this.requiredLabel1.Size = new System.Drawing.Size(80, 14);
            this.requiredLabel1.TabIndex = 24;
            this.requiredLabel1.Text = "仓库名称";
            // 
            // ModifyHouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 226);
            this.Controls.Add(this.requiredLabel2);
            this.Controls.Add(this.requiredLabel1);
            this.Controls.Add(this.textEditHouseTel);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.textEditHouseContact);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.textHouseCode);
            this.Controls.Add(this.simpleCancel);
            this.Controls.Add(this.simpleConfirm);
            this.Controls.Add(this.memoHouseRemark);
            this.Controls.Add(this.textHouseAddr);
            this.Controls.Add(this.textHouseName);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ModifyHouse";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改仓库信息";
            ((System.ComponentModel.ISupportInitialize)(this.textHouseAddr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textHouseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoHouseRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textHouseCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditHouseContact.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditHouseTel.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textHouseAddr;
        private DevExpress.XtraEditors.TextEdit textHouseName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.MemoEdit memoHouseRemark;
        private DevExpress.XtraEditors.SimpleButton simpleCancel;
        private DevExpress.XtraEditors.SimpleButton simpleConfirm;
        private DevExpress.XtraEditors.TextEdit textHouseCode;
        private DevExpress.XtraEditors.TextEdit textEditHouseContact;
        private DevExpress.XtraEditors.TextEdit textEditHouseTel;
        private Alading.Controls.Common.RequiredLabel requiredLabel1;
        private Alading.Controls.Common.RequiredLabel requiredLabel2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}