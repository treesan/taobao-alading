namespace Alading.Forms.Trade.Controls
{
    partial class OperateMessages
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gcOperateMesages = new DevExpress.XtraGrid.GridControl();
            this.gvMessages = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.operateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.operateContent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.operateMemo = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcOperateMesages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMessages)).BeginInit();
            this.SuspendLayout();
            // 
            // gcOperateMesages
            // 
            this.gcOperateMesages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcOperateMesages.Location = new System.Drawing.Point(0, 0);
            this.gcOperateMesages.MainView = this.gvMessages;
            this.gcOperateMesages.Name = "gcOperateMesages";
            this.gcOperateMesages.Size = new System.Drawing.Size(528, 222);
            this.gcOperateMesages.TabIndex = 1;
            this.gcOperateMesages.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMessages});
            // 
            // gvMessages
            // 
            this.gvMessages.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.operateTime,
            this.operateContent,
            this.operateMemo});
            this.gvMessages.GridControl = this.gcOperateMesages;
            this.gvMessages.Name = "gvMessages";
            this.gvMessages.OptionsView.ShowGroupPanel = false;
            // 
            // operateTime
            // 
            this.operateTime.Caption = "操作时间";
            this.operateTime.FieldName = "operateTime";
            this.operateTime.Name = "operateTime";
            this.operateTime.OptionsColumn.AllowEdit = false;
            this.operateTime.Visible = true;
            this.operateTime.VisibleIndex = 0;
            // 
            // operateContent
            // 
            this.operateContent.Caption = "操作内容";
            this.operateContent.FieldName = "operateContent";
            this.operateContent.Name = "operateContent";
            this.operateContent.OptionsColumn.AllowEdit = false;
            this.operateContent.Visible = true;
            this.operateContent.VisibleIndex = 1;
            // 
            // operateMemo
            // 
            this.operateMemo.Caption = "操作备注";
            this.operateMemo.FieldName = "operateMemo";
            this.operateMemo.Name = "operateMemo";
            this.operateMemo.OptionsColumn.AllowEdit = false;
            this.operateMemo.Visible = true;
            this.operateMemo.VisibleIndex = 2;
            // 
            // OperateMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcOperateMesages);
            this.Name = "OperateMessages";
            this.Size = new System.Drawing.Size(528, 222);
            ((System.ComponentModel.ISupportInitialize)(this.gcOperateMesages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMessages)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcOperateMesages;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMessages;
        private DevExpress.XtraGrid.Columns.GridColumn operateTime;
        private DevExpress.XtraGrid.Columns.GridColumn operateContent;
        private DevExpress.XtraGrid.Columns.GridColumn operateMemo;
    }
}
