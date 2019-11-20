using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;
using Alading.Business;
using System.Linq;
using System.IO;
using Alading.Core.Enum;
using DevExpress.XtraEditors.Controls;
namespace Alading.Forms.Print
{
    [ToolboxItem(false)]
    public partial class NewTemplet : DevExpress.XtraEditors.XtraUserControl
    {
        #region 数据区
        private Point mouse_offset;
        private IList<LabelControl> lc = new List<LabelControl>();
        private String picPath = string.Empty;
        #endregion

        public NewTemplet()
        {
            InitializeComponent();
            newTemplateRefresh();
        }
        
        public void newTemplateRefresh()//新建模板刷新
        {
            for (int i = 0; i < ntCover.Items.Count; i++)
            {
                ntCover.Items[i].CheckState = CheckState.Unchecked;                
            }
            ntName.Text = string.Empty;
            ntPicture.Text = string.Empty;
            ClearAfterSave();
        }

        private void NewTemplet_Load(object sender, EventArgs e)
        {
            initLogisticCompany();
            int x = 100, y = 20;
            for (int i = 0; i < 11; i++)
            {
                lc.Add(new LabelControl());
                lc[i].BringToFront();
                lc[i].Visible = true;
                this.pictureBox1.Controls.Add(lc[i]);
                lc[i].MouseDown += new MouseEventHandler(control_MouseDown);
                lc[i].MouseMove += new MouseEventHandler(control_MouseMove);
                if (i % 3 == 0)
                {
                    x = 100;
                    y += 40;
                }
                x += 120;
                lc[i].Location = new Point(x, y);
            }
            lc[0].Text = "快递单号";
            lc[1].Text = "城市";
            lc[2].Text = "座机";
            lc[3].Text = "买家姓名";
            lc[4].Text = "地区";
            lc[5].Text = "邮编";
            lc[6].Text = "省份";
            lc[7].Text = "详细地址";
            lc[8].Text = "收件人";
            lc[9].Text = "发件人手机号码";
            lc[10].Text = "收件人手机号码";
        }

        private void initLogisticCompany()//设置物流公司
        {
            List<LogisticCompany> logCompany = LogisticCompanyService.GetAllLogisticCompany();
            ntCompany.Properties.DataSource = logCompany;
            ntCompany.Properties.DisplayMember = "name";
            ntCompany.Properties.ValueMember = "code";
            ntCompany.EditValue = logCompany.First().code;
        }

        private void ntPicture_Click(object sender, EventArgs e)//放入新建图片
        {
            if (lc.Count == 0)
            {
                NewTemplet_Load(sender, e);
            }
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openFileDialog1.Filter = "Image   Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
                this.pictureBox1.ImageLocation = openFileDialog1.FileName;
                Image i = Image.FromFile(openFileDialog1.FileName);
                picPath = openFileDialog1.FileName;
                this.pictureBox1.Width = i.Width;
                this.pictureBox1.Height = i.Height;
                i.Dispose();
                ntPicture.Text = picPath;
            }

        }

        private void control_MouseDown(object sender, MouseEventArgs e)//获取坐标
        {
            mouse_offset = new Point(-e.X, -e.Y);//
        }

        private void control_MouseMove(object sender, MouseEventArgs e)//鼠标移动
        {
            ((Control)sender).Cursor = Cursors.Arrow;//设置拖动时鼠标箭头
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);//设置偏移
                ((Control)sender).Location = ((Control)sender).Parent.PointToClient(mousePos);
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)//新建标签
        {
            if (ntItem.Text != string.Empty)
            {
                LabelControl lctemp = new LabelControl();
                lc.Add(lctemp);
                int index = lc.Count - 1;
                lc[index].Text = ntItem.Text;
                lc[index].BringToFront();
                lc[index].Visible = true;
                this.pictureBox1.Controls.Add(lctemp);
                lc[index].MouseDown += new MouseEventHandler(control_MouseDown);
                lc[index].MouseMove += new MouseEventHandler(control_MouseMove);
                lc[index].Location = new Point(this.pictureBox1.Width / 2, this.pictureBox1.Height / 2);
                ntItem.Text = string.Empty;
            }
        }

        private void ntSave_Click(object sender, EventArgs e)//保存新建模板
        {
            if ((ntName.Text != "") && (ntCompany.Text != "") && (picPath != ""))
            {
                string comTemplateName = ntName.Text;
                string logCompanyName = ntCompany.Text.ToString() ;
                string logCompanyCode = ntCompany.EditValue.ToString();
                string TemplateCode = System.Guid.NewGuid().ToString();
                byte[] i = SetImageToByteArray(picPath);
                string coverArea = string.Empty;
                foreach (CheckedListBoxItem c in ntCover.CheckedItems)
                {
                    if (c.Value.ToString() != "全选" && c.Value.ToString() != "取消全选")
                    {
                        coverArea += c.Value + ",";
                    }
                }
                if (coverArea != "")
                {
                    LogisticCompanyTemplate lct = new LogisticCompanyTemplate();
                    lct.CoverAreaList = coverArea;
                    lct.LogisticCompanyCode = logCompanyCode;
                    lct.LogisticCompanyName = logCompanyName;
                    lct.LogisticCompanyCode = TemplateCode;
                    lct.LogisticCompanyName = comTemplateName;
                    lct.PreviewImage = i;
                    if (LogisticCompanyTemplateService.AddLogisticCompanyTemplate(lct) == ReturnType.Success)
                    {
                        NewTemplateItems(TemplateCode);
                        DevExpress.XtraEditors.XtraMessageBox.Show("模板创建成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearAfterSave();
                    }
                    else
                    {
                        XtraMessageBox.Show("模板名称重复，请重新输入模板！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ntName.Focus();
                    }
                }
                else
                {
                    XtraMessageBox.Show("未选择覆盖区域！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("有空白项未填写！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearAfterSave()
        {            
            for (int i = 0; i < ntCover.Items.Count; i++)
            {
                ntCover.Items[i].CheckState = CheckState.Unchecked;
            }
            lc.Clear();
            ntName.Text = string.Empty;
            ntPicture.Text = string.Empty;
            pictureBox1.Controls.Clear();
            pictureBox1.Image = null;
            pictureBox1.Refresh();
        }

        private byte[] SetImageToByteArray(string fileName)//将图片转为二进制数组
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            int imageLength = (int)fs.Length;
            byte[] image = new byte[imageLength];
            fs.Read(image, 0, imageLength);
            fs.Close();
            return image;
        }

        private void NewTemplateItems(string TemplateCode)
        {
            foreach (LabelControl x in lc)
            {
                LogisticCompanyTemplateItem lctItem = new LogisticCompanyTemplateItem();
                lctItem.ItemName = x.Text;
                lctItem.ItemValue = x.Text;
                lctItem.LogisticCompanyItemCode = System.Guid.NewGuid().ToString();
                lctItem.LogisticCompanyTemplateCode = TemplateCode;
                lctItem.ItemX = x.Location.X;
                lctItem.ItemY = x.Location.Y;
                LogisticCompanyTemplateItemService.AddLogisticCompanyTemplateItem(lctItem);
            }            
        }

        private void ntName_EditValueChanged(object sender, EventArgs e)  //检查模版命名冲突
        {
            if (ntName.Text.Trim() != string.Empty)
            {
                string logisticCompanyCode = ntCompany.EditValue.ToString();
                List<LogisticCompanyTemplate> lct = LogisticCompanyTemplateService.GetLogisticCompanyTemplate(logisticCompanyCode);  //此处可以优化，把lct做成全局的，在界面初始化时创建，
                                                                                                                                     //在保存新模版时更新，该方法只是调用lct
                if (lct != null)
                {
                    int count = 0;
                    foreach (LogisticCompanyTemplate x in lct)
                    {
                        if (x.LogisticCompanyName == ntName.Text)
                        {

                            XtraMessageBox.Show("您所输入的名称在选中的物流公司模版库中已存在，请重新输入模版名称！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        count++;
                    }

                    if (count == lct.Count)  //模版名称没有重复
                    {
                        checkTplateNameLabel.Text = "√";
                    }
                    else
                    {
                        ntName.Focus();
                        ntName.SelectAll();
                        checkTplateNameLabel.Text = "[*]";
                    }
                }
                else
                {
                    checkTplateNameLabel.Text = "√";
                }
            }
            else
            {
                checkTplateNameLabel.Text = "[*]";
            }
        }

        private void ntItem_EditValueChanged(object sender, EventArgs e)
        {
            int count = 0;
            foreach (LabelControl x in lc)
            {
                if (ntItem.Text == x.Text)
                {
                    checkItemNameLabel.Text = "名称在模版标签中已存在,请重新输入";
                    ntItem.SelectAll();
                    ntItem.Focus();
                    break;
                }
                count++;
            }

            if (count == lc.Count)
            {
                checkItemNameLabel.Text = "√";
            }

            if (ntItem.Text == string.Empty)
            {
                checkItemNameLabel.Text = string.Empty;
            }

        }  //检查新建标签命名冲突

        private void ntCover_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ntCover.SelectedIndex == 0)
            {
                SelectAll();
            }
            else if (ntCover.SelectedIndex == 1)
            {
                SelectCancel();
            }
            else
            {
                ntCover.Items[0].CheckState = CheckState.Unchecked;
                ntCover.Items[1].CheckState = CheckState.Unchecked;
                ntCover.Items[ntCover.SelectedIndex].CheckState = CheckState.Checked;
            }
        }

        private void SelectCancel()
        {
            int count = 0;
            foreach (CheckedListBoxItem c in ntCover.Items)
            {
                if (count == 1)
                {
                    c.CheckState = CheckState.Checked;
                }
                else
                {
                    c.CheckState = CheckState.Unchecked;
                }
                count++;
            }
        }

        private void SelectAll()
        {
            int count = 0;
            foreach (CheckedListBoxItem c in ntCover.Items)
            {
                if (count != 1)
                {
                    c.CheckState = CheckState.Checked;
                }
                else 
                {
                    c.CheckState = CheckState.Unchecked;
                }
                count++;
            }
        }

    }
}
