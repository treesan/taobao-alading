namespace Alading.Forms.Staff
{
    partial class AddRole
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tERoleName = new DevExpress.XtraEditors.TextEdit();
            this.ensureBtn = new DevExpress.XtraEditors.SimpleButton();
            this.cancelBtn = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEditRoles = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tERoleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditRoles.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "角色名：";
            // 
            // tERoleName
            // 
            this.tERoleName.Location = new System.Drawing.Point(77, 19);
            this.tERoleName.Name = "tERoleName";
            this.tERoleName.Properties.MaxLength = 20;
            this.tERoleName.Size = new System.Drawing.Size(245, 21);
            this.tERoleName.TabIndex = 1;
            this.tERoleName.EditValueChanged += new System.EventHandler(this.tERoleName_EditValueChanged);
            // 
            // ensureBtn
            // 
            this.ensureBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ensureBtn.Enabled = false;
            this.ensureBtn.Location = new System.Drawing.Point(102, 93);
            this.ensureBtn.Name = "ensureBtn";
            this.ensureBtn.Size = new System.Drawing.Size(75, 23);
            this.ensureBtn.TabIndex = 2;
            this.ensureBtn.Text = "确定";
            this.ensureBtn.Click += new System.EventHandler(this.ensureBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(209, 93);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "取消";
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 61);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "角色类型：";
            // 
            // comboBoxEditRoles
            // 
            this.comboBoxEditRoles.Location = new System.Drawing.Point(77, 58);
            this.comboBoxEditRoles.Name = "comboBoxEditRoles";
            this.comboBoxEditRoles.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditRoles.Properties.Items.AddRange(new object[] {
            "系统管理员",
            "财务",
            "出纳",
            "客服",
            "采购员",
            "仓管员",
            "代理商",
            "其他"});
            this.comboBoxEditRoles.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditRoles.Size = new System.Drawing.Size(245, 21);
            this.comboBoxEditRoles.TabIndex = 6;
            this.comboBoxEditRoles.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditRoles_SelectedIndexChanged);
            // 
            // AddRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 134);
            this.Controls.Add(this.comboBoxEditRoles);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.ensureBtn);
            this.Controls.Add(this.tERoleName);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddRole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加角色";
            ((System.ComponentModel.ISupportInitialize)(this.tERoleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditRoles.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit tERoleName;
        private DevExpress.XtraEditors.SimpleButton ensureBtn;
        private DevExpress.XtraEditors.SimpleButton cancelBtn;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditRoles;
    }
}