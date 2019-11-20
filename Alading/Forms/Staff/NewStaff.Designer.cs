namespace Alading.Forms.StaffManager
{
    partial class NewStaff
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
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.tLRole = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.memo_Remark = new DevExpress.XtraEditors.MemoExEdit();
            this.dateEdit = new DevExpress.XtraEditors.DateEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.rgSex = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txAddr = new DevExpress.XtraEditors.TextEdit();
            this.txPassword = new DevExpress.XtraEditors.TextEdit();
            this.txDept = new DevExpress.XtraEditors.TextEdit();
            this.txName = new DevExpress.XtraEditors.TextEdit();
            this.txMail = new DevExpress.XtraEditors.TextEdit();
            this.requiredLabel3 = new Alading.Controls.Common.RequiredLabel();
            this.txPhone = new DevExpress.XtraEditors.TextEdit();
            this.requiredLabel2 = new Alading.Controls.Common.RequiredLabel();
            this.requiredLabel1 = new Alading.Controls.Common.RequiredLabel();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txBankAddr = new DevExpress.XtraEditors.TextEdit();
            this.txAccount = new DevExpress.XtraEditors.TextEdit();
            this.bntSave = new DevExpress.XtraEditors.SimpleButton();
            this.bntCancel = new DevExpress.XtraEditors.SimpleButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tLRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memo_Remark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgSex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txAddr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txDept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txMail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txPhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txBankAddr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txAccount.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.tLRole);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl3.Location = new System.Drawing.Point(563, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(200, 478);
            this.groupControl3.TabIndex = 92;
            this.groupControl3.Text = "员工角色";
            // 
            // tLRole
            // 
            this.tLRole.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.tLRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tLRole.Location = new System.Drawing.Point(2, 23);
            this.tLRole.Name = "tLRole";
            this.tLRole.OptionsView.ShowCheckBoxes = true;
            this.tLRole.Size = new System.Drawing.Size(196, 453);
            this.tLRole.TabIndex = 0;
            this.tLRole.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.tLRole_AfterCheckNode);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "员工角色列表";
            this.treeListColumn1.FieldName = "员工角色列表";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.OptionsColumn.ReadOnly = true;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.memo_Remark);
            this.groupControl1.Controls.Add(this.dateEdit);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.rgSex);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txAddr);
            this.groupControl1.Controls.Add(this.txPassword);
            this.groupControl1.Controls.Add(this.txDept);
            this.groupControl1.Controls.Add(this.txName);
            this.groupControl1.Controls.Add(this.txMail);
            this.groupControl1.Controls.Add(this.requiredLabel3);
            this.groupControl1.Controls.Add(this.txPhone);
            this.groupControl1.Controls.Add(this.requiredLabel2);
            this.groupControl1.Controls.Add(this.requiredLabel1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(563, 248);
            this.groupControl1.TabIndex = 93;
            this.groupControl1.Text = "基本信息";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(330, 69);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 90;
            this.labelControl2.Text = "性别：";
            // 
            // memo_Remark
            // 
            this.memo_Remark.Location = new System.Drawing.Point(372, 195);
            this.memo_Remark.Name = "memo_Remark";
            this.memo_Remark.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.memo_Remark.Size = new System.Drawing.Size(153, 21);
            this.memo_Remark.TabIndex = 89;
            // 
            // dateEdit
            // 
            this.dateEdit.EditValue = null;
            this.dateEdit.Location = new System.Drawing.Point(90, 158);
            this.dateEdit.Name = "dateEdit";
            this.dateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit.Size = new System.Drawing.Size(153, 21);
            this.dateEdit.TabIndex = 87;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(48, 198);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(36, 14);
            this.labelControl6.TabIndex = 85;
            this.labelControl6.Text = "地址：";
            // 
            // rgSex
            // 
            this.rgSex.Location = new System.Drawing.Point(372, 50);
            this.rgSex.Name = "rgSex";
            this.rgSex.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "男"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "女")});
            this.rgSex.Size = new System.Drawing.Size(153, 53);
            this.rgSex.TabIndex = 86;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(48, 161);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 14);
            this.labelControl5.TabIndex = 85;
            this.labelControl5.Text = "生日：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(330, 162);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 84;
            this.labelControl3.Text = "电话：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(306, 124);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 83;
            this.labelControl1.Text = "电子邮箱：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(329, 198);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 82;
            this.labelControl4.Text = "备注：";
            // 
            // txAddr
            // 
            this.txAddr.EnterMoveNextControl = true;
            this.txAddr.Location = new System.Drawing.Point(90, 195);
            this.txAddr.Name = "txAddr";
            this.txAddr.Size = new System.Drawing.Size(153, 21);
            this.txAddr.TabIndex = 81;
            // 
            // txPassword
            // 
            this.txPassword.EnterMoveNextControl = true;
            this.txPassword.Location = new System.Drawing.Point(90, 121);
            this.txPassword.Name = "txPassword";
            this.txPassword.Size = new System.Drawing.Size(153, 21);
            this.txPassword.TabIndex = 81;
            this.txPassword.EditValueChanged += new System.EventHandler(this.EditValueChanged);
            // 
            // txDept
            // 
            this.txDept.EnterMoveNextControl = true;
            this.txDept.Location = new System.Drawing.Point(90, 84);
            this.txDept.Name = "txDept";
            this.txDept.Size = new System.Drawing.Size(153, 21);
            this.txDept.TabIndex = 81;
            this.txDept.EditValueChanged += new System.EventHandler(this.EditValueChanged);
            // 
            // txName
            // 
            this.txName.EnterMoveNextControl = true;
            this.txName.Location = new System.Drawing.Point(90, 47);
            this.txName.Name = "txName";
            this.txName.Size = new System.Drawing.Size(153, 21);
            this.txName.TabIndex = 81;
            this.txName.EditValueChanged += new System.EventHandler(this.EditValueChanged);
            // 
            // txMail
            // 
            this.txMail.EnterMoveNextControl = true;
            this.txMail.Location = new System.Drawing.Point(372, 122);
            this.txMail.Name = "txMail";
            this.txMail.Size = new System.Drawing.Size(153, 21);
            this.txMail.TabIndex = 80;
            // 
            // requiredLabel3
            // 
            this.requiredLabel3.Location = new System.Drawing.Point(36, 125);
            this.requiredLabel3.Name = "requiredLabel3";
            this.requiredLabel3.Size = new System.Drawing.Size(53, 14);
            this.requiredLabel3.TabIndex = 78;
            this.requiredLabel3.Text = "密码";
            // 
            // txPhone
            // 
            this.txPhone.EnterMoveNextControl = true;
            this.txPhone.Location = new System.Drawing.Point(372, 158);
            this.txPhone.Name = "txPhone";
            this.txPhone.Size = new System.Drawing.Size(153, 21);
            this.txPhone.TabIndex = 79;
            // 
            // requiredLabel2
            // 
            this.requiredLabel2.Location = new System.Drawing.Point(36, 87);
            this.requiredLabel2.Name = "requiredLabel2";
            this.requiredLabel2.Size = new System.Drawing.Size(53, 14);
            this.requiredLabel2.TabIndex = 78;
            this.requiredLabel2.Text = "部门";
            // 
            // requiredLabel1
            // 
            this.requiredLabel1.Location = new System.Drawing.Point(36, 50);
            this.requiredLabel1.Name = "requiredLabel1";
            this.requiredLabel1.Size = new System.Drawing.Size(53, 14);
            this.requiredLabel1.TabIndex = 77;
            this.requiredLabel1.Text = "姓名";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txBankAddr);
            this.groupControl2.Controls.Add(this.txAccount);
            this.groupControl2.Controls.Add(this.bntSave);
            this.groupControl2.Controls.Add(this.bntCancel);
            this.groupControl2.Controls.Add(this.label7);
            this.groupControl2.Controls.Add(this.label5);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 248);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(563, 230);
            this.groupControl2.TabIndex = 94;
            this.groupControl2.Text = "账户信息";
            // 
            // txBankAddr
            // 
            this.txBankAddr.EnterMoveNextControl = true;
            this.txBankAddr.Location = new System.Drawing.Point(142, 112);
            this.txBankAddr.Name = "txBankAddr";
            this.txBankAddr.Size = new System.Drawing.Size(251, 21);
            this.txBankAddr.TabIndex = 82;
            // 
            // txAccount
            // 
            this.txAccount.EnterMoveNextControl = true;
            this.txAccount.Location = new System.Drawing.Point(142, 59);
            this.txAccount.Name = "txAccount";
            this.txAccount.Size = new System.Drawing.Size(251, 21);
            this.txAccount.TabIndex = 82;
            // 
            // bntSave
            // 
            this.bntSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bntSave.Enabled = false;
            this.bntSave.Location = new System.Drawing.Point(156, 173);
            this.bntSave.Name = "bntSave";
            this.bntSave.Size = new System.Drawing.Size(75, 23);
            this.bntSave.TabIndex = 34;
            this.bntSave.Text = "保存";
            this.bntSave.Click += new System.EventHandler(this.bntSave_Click);
            // 
            // bntCancel
            // 
            this.bntCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bntCancel.Location = new System.Drawing.Point(349, 173);
            this.bntCancel.Name = "bntCancel";
            this.bntCancel.Size = new System.Drawing.Size(75, 23);
            this.bntCancel.TabIndex = 35;
            this.bntCancel.Text = "关闭";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(63, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 14);
            this.label7.TabIndex = 52;
            this.label7.Text = "开户行地址:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 14);
            this.label5.TabIndex = 51;
            this.label5.Text = "银行账户:";
            // 
            // NewStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 478);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl3);
            this.MaximizeBox = false;
            this.Name = "NewStaff";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建员工";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tLRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memo_Remark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgSex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txAddr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txDept.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txMail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txPhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txBankAddr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txAccount.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.MemoExEdit memo_Remark;
        private DevExpress.XtraEditors.DateEdit dateEdit;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.RadioGroup rgSex;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txAddr;
        private DevExpress.XtraEditors.TextEdit txPassword;
        private DevExpress.XtraEditors.TextEdit txDept;
        private DevExpress.XtraEditors.TextEdit txName;
        private DevExpress.XtraEditors.TextEdit txMail;
        private Alading.Controls.Common.RequiredLabel requiredLabel3;
        private DevExpress.XtraEditors.TextEdit txPhone;
        private Alading.Controls.Common.RequiredLabel requiredLabel2;
        private Alading.Controls.Common.RequiredLabel requiredLabel1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.TextEdit txBankAddr;
        private DevExpress.XtraEditors.TextEdit txAccount;
        private DevExpress.XtraEditors.SimpleButton bntSave;
        private DevExpress.XtraEditors.SimpleButton bntCancel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraTreeList.TreeList tLRole;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;

    }
}