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
using DevExpress.XtraEditors.Controls;
using System.IO;
using System.Drawing.Imaging;
using Alading.Core.Enum;

namespace Alading.Forms.Print
{
    [ToolboxItem(false)]
    public partial class ModifyTemplet : DevExpress.XtraEditors.XtraUserControl
    {
        private Point mouse_offset;
        private List<LabelControl> lc = new List<LabelControl>(); //界面上的Label标签
        public List<LogisticCompanyTemplateItem> lcti; //数据库中的标签记录
        private string templateName = string.Empty;
        public string templateCode="";
        public string templateID = "";
       
        public ModifyTemplet()
        {
            InitializeComponent();
        }
        
        public void getTemplateCode(string code, string tempid)//获得模板Code
        {
            templateCode = code;
            templateID = tempid;
            initLogisticCompany();
        }

        private void initLogisticCompany()//设置物流公司
        {
            lc.Clear(); 
            pictureBox2.Controls.Clear();

            LogisticCompanyTemplate modifyTemplate=LogisticCompanyTemplateService.GetLogisticTemplate(templateCode);

            List<LogisticCompany> logCompany = LogisticCompanyService.GetAllLogisticCompany();
            mtCompany.Properties.DataSource = logCompany;
            mtCompany.Properties.DisplayMember = "name";
            mtCompany.Text=modifyTemplate.LogisticCompanyName;
            mtCompany.Properties.ValueMember = "code";
            mtCompany.EditValue = modifyTemplate.LogisticCompanyCode;

            templateName = modifyTemplate.LogisticCompanyName;
            mtName.Text = modifyTemplate.LogisticCompanyName.ToString();

            mtCover.SelectedIndexChanged -= new EventHandler(mtCover_SelectedIndexChanged);
            string coverArea = modifyTemplate.CoverAreaList;
            string[] x = coverArea.Split(',');

            foreach (string y in x)
            {
                int count = 0;
                foreach (CheckedListBoxItem c in mtCover.Items)
                {
                    if (count >= 2)
                    {
                        if (y == c.Value.ToString())
                        {
                            c.CheckState = CheckState.Checked;
                            break;
                        }
                    }
                    count++;
                }
            }
            mtCover.SelectedIndexChanged += new EventHandler(mtCover_SelectedIndexChanged);

            MemoryStream ms = new MemoryStream(modifyTemplate.PreviewImage);
            pictureBox2.Image = Image.FromStream(ms);
            lcti = LogisticCompanyTemplateItemService.GetLogisticTemplateItems(modifyTemplate.LogisticCompanyCode);  //模版标签
            foreach (LogisticCompanyTemplateItem l in lcti)
            {
                LabelControl lctemp = new LabelControl();
                lctemp.Text = l.ItemName;
                lctemp.Location = new Point(l.ItemX, l.ItemY);
                lctemp.MouseDown += new MouseEventHandler(control_MouseDown);
                lctemp.MouseMove += new MouseEventHandler(control_MouseMove);
                pictureBox2.Controls.Add(lctemp);
                lc.Add(lctemp);
            }
        }  

        private void simpleButton2_Click(object sender, EventArgs e)   //新建标签
        {
            if (mtItem.Text != string.Empty)
            {
                LabelControl lctemp = new LabelControl();
                lc.Add(lctemp);
                int index = lc.Count - 1;
                lc[index].Text = mtItem.Text;
                lc[index].BringToFront();
                lc[index].Visible = true;
                this.pictureBox2.Controls.Add(lctemp);
                lc[index].MouseDown += new MouseEventHandler(control_MouseDown);
                lc[index].MouseMove += new MouseEventHandler(control_MouseMove);
                lc[index].Location = new Point(this.pictureBox2.Width / 2, this.pictureBox2.Height / 2);
                LogisticCompanyTemplateItem temp = new LogisticCompanyTemplateItem();
                temp.LogisticCompanyItemCode = System.Guid.NewGuid().ToString();
                temp.LogisticCompanyTemplateCode = templateCode;
                temp.ItemName = mtItem.Text;
                temp.ItemValue = mtItem.Text;
                temp.ItemX = pictureBox2.Width / 2;
                temp.ItemY = pictureBox2.Height / 2;
                LogisticCompanyTemplateItemService.AddLogisticCompanyTemplateItem(temp);
                lcti.Clear();
                lcti = LogisticCompanyTemplateItemService.GetLogisticTemplateItems(templateCode);
                mtItem.Text = string.Empty;
            }
        }

        private void mtSave_Click(object sender, EventArgs e)//保存按钮
        {
            if ((mtName.Text != "") && (mtCompany.Text != ""))//判断名称与物流公司是否为空
            {
                string comTemplateName = mtName.Text;
                string logCompanyName = mtCompany.Text.ToString();
                string logCompanyCode = mtCompany.EditValue.ToString();
                string logCompanyTemplateCode = templateCode;
                byte[] i = SetImageToByteArray();//将图片转化成矩阵
                string coverArea = string.Empty;
               
                foreach (CheckedListBoxItem c in mtCover.CheckedItems)
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
                    lct.LogisticCompanyCode = templateCode;
                    lct.LogisticCompanyName = comTemplateName;
                    lct.PreviewImage = i;
                    lct.TemplateID = Convert.ToInt32(templateID);
                    if (LogisticCompanyTemplateService.UpdateLogisticCompanyTemplate(lct) == ReturnType.Success)
                    {
                        UpdateTemplateItems(templateCode);
                        DevExpress.XtraEditors.XtraMessageBox.Show("模板修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearAfterUpdate();
                        //应切换到模版列表
                    }
                    else
                    {
                        XtraMessageBox.Show("模板名称重复，请重新输入模板！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        mtName.Focus();
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

        private void mtName_EditValueChanged(object sender, EventArgs e)//判断模板名称是否重复
        {
            if (mtName.Text.Trim() != string.Empty)
            {
                string logisticCompanyCode = mtCompany.EditValue.ToString();
                List<LogisticCompanyTemplate> lct = LogisticCompanyTemplateService.GetLogisticCompanyTemplate(logisticCompanyCode);  //此处可以优化，把lct做成全局的，在界面初始化时创建，
                //在保存新模版时更新，该方法只是调用lct
                if (lct != null)
                {
                    int count = 0;
                    foreach (LogisticCompanyTemplate x in lct)
                    {
                        if (x.LogisticCompanyName == mtName.Text && x.LogisticCompanyName != templateName)
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
                        mtName.Focus();
                        mtName.SelectAll();
                        checkTplateNameLabel.Text = "[*]";
                    }
                }
                else  //换物流公司,并且物流公司模版库为空
                {
                    checkTplateNameLabel.Text = "√";
                }
            }
            else
            {
                checkTplateNameLabel.Text = "[*]";
            }
        }

        private void mtItem_EditValueChanged(object sender, EventArgs e)//判断标签名称是否重复
        {
            int count = 0;
            foreach (LabelControl x in lc)
            {
                if (mtItem.Text == x.Text)
                {
                    checkItemNameLabel.Text = "名称在模版标签中已存在,请重新输入";
                    mtItem.SelectAll();
                    mtItem.Focus();
                    break;
                }
                count++;
            }

            if (count == lc.Count)
            {
                checkItemNameLabel.Text = "√";
            }

            if (mtItem.Text == string.Empty)
            {
                checkItemNameLabel.Text = string.Empty;
            }

        }

        private void mtPicture_Click(object sender, EventArgs e)//选取图片
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openFileDialog1.Filter = "Image   Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
                this.pictureBox2.ImageLocation = openFileDialog1.FileName;
                Image i = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox2.Width = i.Width;
                this.pictureBox2.Height = i.Height;
                i.Dispose();
                mtPicture.Text = openFileDialog1.FileName;
            }
        }

        private void mtCover_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mtCover.SelectedIndex == 0)
            {
                SelectAll();
            }
            else if (mtCover.SelectedIndex == 1)
            {
                SelectCancel();
            }
            else
            {
                SelectIndex(mtCover.SelectedIndex);
            }
        }

        private void SelectAll()
        {
            int count = 0;
            foreach (CheckedListBoxItem c in mtCover.Items)
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

        private void SelectCancel()
        {
            int count = 0;
            foreach (CheckedListBoxItem c in mtCover.Items)
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

        private void SelectIndex(int index)
        {
            mtCover.Items[0].CheckState = CheckState.Unchecked;
            mtCover.Items[1].CheckState = CheckState.Unchecked;
            mtCover.Items[index].CheckState = CheckState.Checked;
        }

        private byte[] SetImageToByteArray()//将图片转为二进制数组
        {
            Image picture = this.pictureBox2.Image;
            MemoryStream ms = new MemoryStream();
            picture.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);//转换成数据流

            byte[] bPicture = ms.GetBuffer();//
            return bPicture;
        }

        private void control_MouseDown(object sender, MouseEventArgs e)//获取坐标
        {
            mouse_offset = new Point(-e.X, -e.Y);//
        }

        private void control_MouseMove(object sender, MouseEventArgs e)//移动标签
        {
            ((Control)sender).Cursor = Cursors.Arrow;//设置拖动时鼠标箭头
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);//设置偏移
                ((Control)sender).Location = ((Control)sender).Parent.PointToClient(mousePos);
            }
        }

        private void UpdateTemplateItems(string TemplateCode)//更新模板标签
        {
            int count = 0;
            foreach (LabelControl x in lc)
            {
                LogisticCompanyTemplateItem lctItem = new LogisticCompanyTemplateItem();
                lctItem.LogisticCompanyItemID = lcti[count].LogisticCompanyItemID;
                lctItem.ItemName = x.Text;
                lctItem.ItemValue = x.Text;
                lctItem.LogisticCompanyItemCode = lcti[count].LogisticCompanyItemCode;
                lctItem.LogisticCompanyTemplateCode = TemplateCode;
                lctItem.ItemX = x.Location.X;
                lctItem.ItemY = x.Location.Y;
                LogisticCompanyTemplateItemService.UpdateLogisticCompanyTemplateItem(lctItem);
                count++;
            }
        }

        private void ClearAfterUpdate()//修改成功后初始化
        {
            for (int i = 0; i < mtCover.Items.Count; i++)
            {
                mtCover.Items[i].CheckState = CheckState.Unchecked;
            }

            mtName.Text = string.Empty;
            mtPicture.Text = string.Empty;

            pictureBox2.Image = null;
            pictureBox2.Controls.Clear();
            pictureBox2.Refresh();
        }
    }
}
