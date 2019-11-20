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
    public partial class ListTemplet : DevExpress.XtraEditors.XtraUserControl
    {       
        public ListTemplet()
        {
            InitializeComponent();
            
        }

        public string templateCode;
        public string tempID;
        public string getTemplateCode()//获取模板Code
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                templateCode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LogisticCompanyTemplateCode").ToString();
                return templateCode;
            }
            else
                return "";
        }
        public string getTemplateID()//获取模板ID
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                tempID = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LogisticCompanyTemplateID").ToString();
                return tempID;
            }
            else
                return "";
        }
        public void getCompanyList()
        {
            listCompany.Items.Add("全选");
            listCompany.Items.Add("全部取消");
            List<LogisticCompany> logisticCompany = LogisticCompanyService.GetAllLogisticCompany();
            int count = 2;
            foreach (LogisticCompany y in logisticCompany)
            {
                listCompany.Items.Add(y.name);
                listCompany.Items[count].Value = y.code;
                listCompany.Items[count].Description = y.name;
                count++;
            }

            SelectAll();
        }

        private void SelectAll()
        {
            for (int i = 0; i < listCompany.Items.Count; i++)
            {
                if (i == 1)
                {
                    listCompany.Items[i].CheckState = CheckState.Unchecked;
                }
                else
                    listCompany.Items[i].CheckState = CheckState.Checked;
            }
            templateRefresh();
        }

        public void templateRefresh()
        {
            List<LogisticCompanyTemplate> listItem = LogisticCompanyTemplateService.GetAllLogisticCompanyTemplate();
            gridControl1.DataSource = listItem;
            gridView1.BestFitColumns();
        }

        private void listTemplet_Load(object sender, EventArgs e)
        {
            xtraScrollableControl1.Controls.Add(pictureBox1);
            getCompanyList();
            templateRefresh();
        }

        private void deleteTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)//删除
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                string logisticCode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LogisticCompanyTemplateCode").ToString();

                if (ReturnType.Success == LogisticCompanyTemplateItemService.RemoveLogisticCompanyTemplateItems(logisticCode))
                {
                    if (ReturnType.Success == LogisticCompanyTemplateService.RemoveLogisticCompanyTemplate(logisticCode))
                    {
                        List<LogisticCompanyTemplate> listItem = LogisticCompanyTemplateService.GetAllLogisticCompanyTemplate();
                        gridControl1.DataSource = listItem;
                        gridView1.BestFitColumns();
                        ClearPreviewBox();
                        XtraMessageBox.Show("模版删除成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        XtraMessageBox.Show("模板删除出错！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("模板标签出错！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)//刷新，相当于选择物流公司列表
        {
            listCompany_SelectedIndexChanged(sender, new EventArgs());
        }

        private void listCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listCompany.SelectedIndex == 0)
            {
                SelectAll();                
            }
            else if (listCompany.SelectedIndex == 1)
            {
                SelectCancelAll();
            }
            else
            {
                SelectIndex(listCompany.SelectedIndex);
            }

            ClearPreviewBox();
        }

        private void ClearPreviewBox()
        {
            pictureBox1.Image = null;            
            pictureBox1.Controls.Clear();
            pictureBox1.Refresh();
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            PreviewTemplate();
        }

        private void PreviewTemplate()
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                string logisticCode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LogisticCompanyTemplateCode").ToString();
                LogisticCompanyTemplate lct = LogisticCompanyTemplateService.GetLogisticTemplate(logisticCode);
                List<LogisticCompanyTemplateItem> logTempItem = LogisticCompanyTemplateItemService.GetLogisticTemplateItems(lct.LogisticCompanyCode);

                MemoryStream ms = new MemoryStream(lct.PreviewImage);
                pictureBox1.Image = Image.FromStream(ms);
                pictureBox1.Controls.Clear();
                foreach (LogisticCompanyTemplateItem l in logTempItem)
                {
                    LabelControl lctemp = new LabelControl();
                    lctemp.Text = l.ItemName;
                    lctemp.Location = new Point(l.ItemX, l.ItemY);
                    pictureBox1.Controls.Add(lctemp);
                }
            }
        }

        private void SelectCancelAll()
        {
            for (int i = 0; i < listCompany.Items.Count; i++)
            {
                if (i == 1)
                {
                    listCompany.Items[i].CheckState = CheckState.Checked;
                }
                else
                {
                    listCompany.Items[i].CheckState = CheckState.Unchecked;
                }
            }
            gridControl1.DataSource = null;
        }

        private void SelectIndex(int i)
        {
            for (int y = 0; y < listCompany.Items.Count; y++)
            {
                if (y == i)
                {
                    listCompany.Items[y].CheckState = CheckState.Checked;
                }
                else
                {
                    listCompany.Items[y].CheckState = CheckState.Unchecked;
                }
            }

            List<LogisticCompanyTemplate> lct = LogisticCompanyTemplateService.GetLogisticCompanyTemplate(listCompany.Items[i].Value.ToString());
            gridControl1.DataSource = lct;
            gridView1.BestFitColumns();
        }
       
    }
}
