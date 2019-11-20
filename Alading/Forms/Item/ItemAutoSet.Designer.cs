namespace Alading.Forms.Item
{
    partial class ItemAutoSet
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
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SuperToolTip superToolTip7 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem10 = new DevExpress.Utils.ToolTipTitleItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemAutoSet));
            DevExpress.Utils.SuperToolTip superToolTip8 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem11 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem12 = new DevExpress.Utils.ToolTipTitleItem();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.checkButtonList = new DevExpress.XtraEditors.CheckButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.checkButtonRecommend = new DevExpress.XtraEditors.CheckButton();
            this.notePanel8_11 = new DevExpress.Utils.Frames.NotePanel8_1();
            this.notePanel = new DevExpress.Utils.Frames.NotePanel8_1();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.spinEditList = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.spinEditRecommend = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditRecommend.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 21);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(72, 14);
            this.labelControl4.TabIndex = 120;
            this.labelControl4.Text = "自动上架状态";
            // 
            // checkButtonList
            // 
            this.checkButtonList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkButtonList.ImageIndex = 1;
            this.checkButtonList.ImageList = this.imageCollection1;
            this.checkButtonList.Location = new System.Drawing.Point(133, 12);
            this.checkButtonList.Name = "checkButtonList";
            this.checkButtonList.Size = new System.Drawing.Size(137, 23);
            toolTipTitleItem10.Text = "当您的店铺出售中有宝贝下架时，将其自动上架";
            superToolTip7.Items.Add(toolTipTitleItem10);
            this.checkButtonList.SuperTip = superToolTip7;
            this.checkButtonList.TabIndex = 119;
            this.checkButtonList.Text = "已关闭，点击开启";
            this.checkButtonList.CheckedChanged += new System.EventHandler(this.checkButtonList_CheckedChanged);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 59);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(72, 14);
            this.labelControl5.TabIndex = 122;
            this.labelControl5.Text = "自动推荐状态";
            // 
            // checkButtonRecommend
            // 
            this.checkButtonRecommend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkButtonRecommend.ImageIndex = 1;
            this.checkButtonRecommend.ImageList = this.imageCollection1;
            this.checkButtonRecommend.Location = new System.Drawing.Point(133, 50);
            this.checkButtonRecommend.Name = "checkButtonRecommend";
            this.checkButtonRecommend.Size = new System.Drawing.Size(137, 23);
            toolTipTitleItem11.Text = "自动橱窗推荐那些快下架了的宝贝";
            toolTipItem4.LeftIndent = 6;
            toolTipItem4.Text = "按照宝贝下架剩余时间从少到多排序";
            toolTipTitleItem12.LeftIndent = 6;
            toolTipTitleItem12.Text = "为提高橱窗使用率，会自动取消下架的宝贝的橱窗推荐";
            superToolTip8.Items.Add(toolTipTitleItem11);
            superToolTip8.Items.Add(toolTipItem4);
            superToolTip8.Items.Add(toolTipTitleItem12);
            this.checkButtonRecommend.SuperTip = superToolTip8;
            this.checkButtonRecommend.TabIndex = 121;
            this.checkButtonRecommend.Text = "已关闭，点击开启";
            this.checkButtonRecommend.CheckedChanged += new System.EventHandler(this.checkButtonRecommend_CheckedChanged);
            // 
            // notePanel8_11
            // 
            this.notePanel8_11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.notePanel8_11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notePanel8_11.ForeColor = System.Drawing.Color.Black;
            this.notePanel8_11.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.notePanel8_11.Location = new System.Drawing.Point(0, 219);
            this.notePanel8_11.Name = "notePanel8_11";
            this.notePanel8_11.Size = new System.Drawing.Size(373, 52);
            this.notePanel8_11.SubstrateGradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.notePanel8_11.TabIndex = 130;
            this.notePanel8_11.TabStop = false;
            this.notePanel8_11.Text = "自动推荐功能说明： 若您在淘宝中设置了宝贝自动重发，请配合自动上架功能避免因宝贝自动重发而造成橱窗不取消的问题，以提高橱窗利用效率。";
            // 
            // notePanel
            // 
            this.notePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.notePanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notePanel.ForeColor = System.Drawing.Color.Black;
            this.notePanel.Location = new System.Drawing.Point(0, 167);
            this.notePanel.Name = "notePanel";
            this.notePanel.Size = new System.Drawing.Size(373, 52);
            this.notePanel.SubstrateGradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.notePanel.TabIndex = 131;
            this.notePanel.TabStop = false;
            this.notePanel.Text = "自动上架功能说明：自动上架开启后，当您的店铺出售中有宝贝下架时，将其自动上架。 自动上架不处理\"我下架的宝贝\"，如若将其上架，请使用“均匀上架”功能。";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 96);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(96, 14);
            this.labelControl1.TabIndex = 132;
            this.labelControl1.Text = "自动上架时间间隔";
            // 
            // spinEditList
            // 
            this.spinEditList.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spinEditList.Location = new System.Drawing.Point(133, 93);
            this.spinEditList.Name = "spinEditList";
            this.spinEditList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditList.Properties.MaxValue = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.spinEditList.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditList.Size = new System.Drawing.Size(98, 21);
            this.spinEditList.TabIndex = 133;
            this.spinEditList.EditValueChanged += new System.EventHandler(this.spinEditList_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 133);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(96, 14);
            this.labelControl2.TabIndex = 134;
            this.labelControl2.Text = "自动推荐时间间隔";
            // 
            // spinEditRecommend
            // 
            this.spinEditRecommend.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spinEditRecommend.Location = new System.Drawing.Point(133, 130);
            this.spinEditRecommend.Name = "spinEditRecommend";
            this.spinEditRecommend.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditRecommend.Properties.MaxValue = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.spinEditRecommend.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditRecommend.Size = new System.Drawing.Size(98, 21);
            this.spinEditRecommend.TabIndex = 135;
            this.spinEditRecommend.EditValueChanged += new System.EventHandler(this.spinEditRecommend_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(246, 96);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 136;
            this.labelControl3.Text = "分钟";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(246, 133);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 14);
            this.labelControl6.TabIndex = 137;
            this.labelControl6.Text = "分钟";
            // 
            // ItemAutoSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 271);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.spinEditRecommend);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.spinEditList);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.notePanel);
            this.Controls.Add(this.notePanel8_11);
            this.Controls.Add(this.checkButtonRecommend);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.checkButtonList);
            this.Controls.Add(this.labelControl4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ItemAutoSet";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "开关自动功能";
            this.Load += new System.EventHandler(this.ItemAutoSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditRecommend.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckButton checkButtonList;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckButton checkButtonRecommend;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.Utils.Frames.NotePanel8_1 notePanel8_11;
        private DevExpress.Utils.Frames.NotePanel8_1 notePanel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit spinEditList;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit spinEditRecommend;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}