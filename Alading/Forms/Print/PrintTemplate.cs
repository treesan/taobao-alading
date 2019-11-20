using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Business;
using DevExpress.XtraTreeList.Nodes;
using Alading.Entity;
using Alading.Utils;
using DevExpress.XtraReports.UI;
using System.IO;

namespace Alading.Forms.Print
{
    /// <summary>
    /// 编辑模板点击触发事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void DoEditTemplateHandler(object sender, EditTemplateEventArgs e);

    public partial class PrintTemplate : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 定义窗体触发事件
        /// </summary>
        public event DoEditTemplateHandler OnEditTemplate;

        /// <summary>
        /// 定义窗体触发事件
        /// </summary>
        public event DoEditTemplateHandler OnNewTemplate;

        public PrintTemplate()
        {
            InitializeComponent();

        }

        #region 公开控件
        public DevExpress.XtraBars.BarButtonItem EditTemplate
        {
            get
            {
                return barButtonItemEditTemplate;
            }
        }
        #endregion

        #region 初始化/刷新
        /// <summary>
        /// 初始化/刷新
        /// </summary>
        private void Init()
        {
            try
            {
                List<Alading.Entity.LogisticCompany> logisticList = LogisticCompanyService.GetAllLogisticCompany();

                TreeListNode parentTreeList = treeListLogisticCompany.AppendNode(new object[] { "所有物流公司" }, null);

                if (logisticList != null)
                {
                    foreach (Alading.Entity.LogisticCompany logistic in logisticList)
                    {
                        treeListLogisticCompany.AppendNode(new object[] { logistic.name }, parentTreeList, logistic.code);
                    }
                }

                parentTreeList.ExpandAll();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        /// <summary>
        /// 加载界面时触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintTemplate_Load(object sender, EventArgs e)
        {
            Init();
        }

        /// <summary>
        /// 点击treeListLogisticCompany结点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListLogisticCompany_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            TreeListNode focusedNode = treeListLogisticCompany.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }

            List<Alading.Entity.LogisticCompanyTemplate> templateList = new List<Alading.Entity.LogisticCompanyTemplate>();
            if (focusedNode.Tag == null)//获取全部信息
            {
                templateList = LogisticCompanyService.GetAllLogisticCompanyTemplate();
            }
            else
            {
                templateList = LogisticCompanyService.GetLogisticCompanyTemplate(c => c.LogisticCompanyCode == focusedNode.Tag.ToString());
            }
            gcLogisticComTemplate.DataSource = templateList;
            gvLogisticComTemplate.BestFitColumns();

            //加载Template
            LoadTemplate(gvLogisticComTemplate.FocusedRowHandle);
        }

        /// <summary>
        /// GridView改变焦点行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvLogisticComTemplate_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadTemplate(e.FocusedRowHandle);
        }

        private void LoadTemplate(int focusedRowHandle)
        {
            if (focusedRowHandle < 0)
            {
                return;
            }

            LogisticCompanyTemplate template = gvLogisticComTemplate.GetRow(gvLogisticComTemplate.FocusedRowHandle) as LogisticCompanyTemplate;

            if (template != null && template.TemplateData != null)
            {
                //1、首先解压缩
                byte[] decommpressBytes = CompressHelper.Decompress(template.TemplateData);
                MemoryStream memStream = new MemoryStream(decommpressBytes);
                XtraReport xtraReport = new XtraReport();
                xtraReport.PrintingSystem = this.printingSystem;
                xtraReport.LoadLayout(memStream);
                xtraReport.CreateDocument(true);
                memStream.Close();
            }
        }

        private void barButtonItemEditTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //触发自定义事件
            if (OnEditTemplate != null)
            {
                LogisticCompanyTemplate template = gvLogisticComTemplate.GetFocusedRow() as LogisticCompanyTemplate;
                if (template != null)
                {
                    EditTemplateEventArgs edit = new EditTemplateEventArgs(template,string.Empty);
                    OnEditTemplate(this.gvLogisticComTemplate, edit);
                }
            }
        }
        /// <summary>
        /// 新建模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnNewTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //触发自定义事件
            if (OnNewTemplate != null)
            {
                if(treeListLogisticCompany.FocusedNode !=null)
                {
                    string companyName = treeListLogisticCompany.FocusedNode.GetDisplayText(0);
                    EditTemplateEventArgs edit = new EditTemplateEventArgs(null, companyName);
                    OnNewTemplate(this.gvLogisticComTemplate, edit);
                }
            }
        }

        #region 操作面板
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Init();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnAddCompany_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnRemoveCompany_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        #endregion


    }

    public class EditTemplateEventArgs : EventArgs
    {
        public LogisticCompanyTemplate template;
        public string companyName;
        public EditTemplateEventArgs(LogisticCompanyTemplate template, string companyName)
        {
            this.template = template;
            this.companyName = companyName;
        }
    }
}