using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using Alading.Core.Enum;

namespace Alading.Forms.Email
{
    public partial class EmailTemplateManage : DevExpress.XtraEditors.XtraForm
    {
        private List<Alading.Entity.EmailTemplateCat> catList
            = new List<Alading.Entity.EmailTemplateCat>();

        private List<Alading.Entity.EmailTemplate> tempList
            = new List<Alading.Entity.EmailTemplate>();

        private bool createNew;
        private int selectedTypeIndex = -1;
        private TreeListNode rootNode = null;
        private Alading.Entity.EmailTemplateCat selectedCat = null;
        private Alading.Entity.EmailTemplate selectedTemplate = null;

        public EmailTemplateManage()
        {
            InitializeComponent();
            rootNode = treeListCat.AppendNode(new object[] { "所有模板分类" }, null);
        }

        private List<Alading.Entity.EmailTemplate> Templates
        {
            get { return tempList; }
            set
            {
                tempList = value;
                gcTempList.DataSource = value;
            }
        }

        private bool IsCreateNewTemplate
        {
            get { return createNew; }
            set
            {
                createNew = value;
                if (value)
                {
                    btSaveTemp.Text = "创建";
                }
                else
                {
                    btSaveTemp.Text = "保存修改";
                }
            }
        }

        /// <summary>
        /// load all templates
        /// </summary>
        private void LoadTemplates()
        {
            Templates = Alading.Business.EmailTemplateService.GetAllEmailTemplate();            
            gpTempList.Text = "模板列表 - 所有模板分类"; 
        }

        /// <summary>
        /// load templates in category
        /// </summary>
        /// <param name="type"></param>
        private void LoadTemplates(Alading.Entity.EmailTemplateCat cat)
        {
            Templates = Alading.Business.EmailTemplateService.GetEmailTemplate(c => c.Type == cat.Code);
            gpTempList.Text = string.Format("模板列表 - {0}", cat.Name);
        }

        /// <summary>
        /// refresh template tree list & drop down box
        /// </summary>
        private void FillTemplateList()
        {
            rootNode.Nodes.Clear();
            cbTempCat.Properties.Items.Clear();

            if (catList != null && catList.Count > 0)
            {
                foreach (var i in catList)
                {
                    treeListCat.AppendNode(new object[] { i.Name }, rootNode, i.Code);
                    cbTempCat.Properties.Items.Add(i.Name);
                }
            }
            treeListCat.ExpandAll();
            cbTempCat.SelectedIndex = 0;            
        }

        /// <summary>
        /// clear input field
        /// </summary>
        private void ClearInputField()
        {
            txTempName.Text = string.Empty;
            txEmailTitle.Text = string.Empty;
            cbTempCat.SelectedIndex = 0;
            htxEmailContent.BodyText = string.Empty;
        }

        /// <summary>
        /// validate input field
        /// </summary>
        private void ValidateInput()
        {
            btSaveTemp.Enabled =
                (!string.IsNullOrEmpty(txTempName.Text)) &&
                (!string.IsNullOrEmpty(txEmailTitle.Text)) &&
                (selectedTypeIndex != -1);
        }
        
        /// <summary>
        /// fill template information
        /// </summary>
        private void FillInputField()
        {
            if (selectedTemplate != null)
            {
                txTempName.Text = selectedTemplate.Name;
                txEmailTitle.Text = selectedTemplate.Title;
                htxEmailContent.BodyHtml = selectedTemplate.Content;
                cbTempCat.SelectedIndex = catList.FindIndex(c => c.Code == selectedTemplate.Type);
            }
        }

        /// <summary>
        /// form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailTemplateManage_Load(object sender, EventArgs e)
        {
            IsCreateNewTemplate = true;
            catList = Alading.Business.EmailTemplateCatService.GetAllEmailTemplateCat();
            FillTemplateList();
            LoadTemplates();
        }

        /// <summary>
        /// add new template category event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAddCat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CatEditor editor = new CatEditor();
            editor.Text = "新建模板分类";
            if (DialogResult.OK == editor.ShowDialog())
            {
                Alading.Entity.EmailTemplateCat cat
                    = new Alading.Entity.EmailTemplateCat()
                    {
                        Name = editor.CatName,
                        Code = Guid.NewGuid().ToString(),
                    };

                Alading.Core.Enum.ReturnType result = Alading.Business.EmailTemplateCatService.AddEmailTemplateCat(cat);

                if (result == Alading.Core.Enum.ReturnType.Success)
                {
                    catList.Add(cat);
                    FillTemplateList();
                }
                else
                {
                    XtraMessageBox.Show("保存数据失败！");
                }
            }
        }

        /// <summary>
        /// selected template category changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListCat_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            TreeListNode focusedNode = treeListCat.FocusedNode;

            if (focusedNode != null)
            {
                if (focusedNode == rootNode)
                {
                    selectedCat = null;
                    LoadTemplates();
                }
                else
                {
                    selectedCat = catList.Find(c => c.Code == focusedNode.Tag.ToString());
                    if (selectedCat != null)
                    {
                        LoadTemplates(selectedCat);
                    }
                    else
                    {
                        gcTempList.DataSource = null;
                    }
                }
            }
        }

        /// <summary>
        /// delete selected template category event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDelCat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedCat != null)
            {
                string msg = string.Format("分类 \"{0}\" 下的所有模板将被删除，是否继续？", selectedCat.Name);
                if (DialogResult.OK == XtraMessageBox.Show(msg, "提示", MessageBoxButtons.OKCancel))
                {
                    Alading.Core.Enum.ReturnType result = Alading.Business.EmailTemplateService.RemoveEmailTemplate(selectedCat.Code);
                    if (result == Alading.Core.Enum.ReturnType.Success)
                    {
                        catList.Remove(selectedCat);
                        FillTemplateList();
                        gcTempList.DataSource = null;
                    }
                    else
                    {
                        XtraMessageBox.Show("删除数据失败");
                    }
                }
            }
        }

        /// <summary>
        /// refresh template category list event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRefreshCat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            catList = Alading.Business.EmailTemplateCatService.GetAllEmailTemplateCat();
            FillTemplateList();
        }

        /// <summary>
        /// add new template event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAddTemp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsCreateNewTemplate = true;
            ClearInputField();
            txTempName.Focus();
        }

        /// <summary>
        /// delete template event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDelTemp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedTemplate != null)
            {
                ReturnType result = Alading.Business.EmailTemplateService.RemoveEmailTemplate(selectedTemplate.EmailTemplateCode);
                if (result == ReturnType.Success)
                {
                    tempList.Remove(selectedTemplate);
                    gcTempList.DataSource = null;
                    gcTempList.DataSource = tempList;
                    ClearInputField();
                }
                else
                {
                    XtraMessageBox.Show("删除数据失败！");
                }
            }
        }

        /// <summary>
        /// refresh template
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRefreshTemp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedCat == null)
            {
                LoadTemplates();
            }
            else
            {
                LoadTemplates(selectedCat);
            }
        }

        /// <summary>
        /// selected template changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvTempList_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int[] rows = gvTempList.GetSelectedRows();

            if (rows.Length > 0)
            {
                selectedTemplate = tempList[rows[0]];
                FillInputField();
                IsCreateNewTemplate = false;
            }
            else
            {
                IsCreateNewTemplate = true;
            }
        }

        /// <summary>
        /// required field text changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void requiredTextField_EditValueChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        /// <summary>
        /// save template event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSaveTemp_Click(object sender, EventArgs e)
        {
            if (IsCreateNewTemplate)
            {
                //create new template
                Alading.Entity.EmailTemplate temp = new Alading.Entity.EmailTemplate();
                temp.Name = txTempName.Text;
                temp.Title = txEmailTitle.Text;
                temp.Content = htxEmailContent.BodyHtml;
                temp.CreationTime = DateTime.Now;
                temp.EmailTemplateCode = Guid.NewGuid().ToString();
                temp.Creator = string.Empty;
                temp.Type = catList[selectedTypeIndex].Code;

                ReturnType result = Alading.Business.EmailTemplateService.AddEmailTemplate(temp);
                if (result == ReturnType.Success)
                {
                    tempList.Add(temp);
                    gcTempList.DataSource = null;
                    gcTempList.DataSource = tempList;
                    selectedTemplate = null;
                    btAddTemp_ItemClick(null, null);
                }
                else
                {
                    XtraMessageBox.Show("数据保存失败！");
                }
            }
            else if (selectedTemplate != null)
            {
                //update template
                selectedTemplate.Name = txTempName.Text;
                selectedTemplate.Title = txEmailTitle.Text;
                selectedTemplate.Content = htxEmailContent.BodyHtml;
                selectedTemplate.Type = catList[selectedTypeIndex].Code;

                ReturnType result = Alading.Business.EmailTemplateService.UpdateEmailTemplate(selectedTemplate);
                if (result == ReturnType.Success)
                {
                    gcTempList.DataSource = null;
                    gcTempList.DataSource = tempList;
                }
                else
                {
                    XtraMessageBox.Show("数据保存失败！");
                }
            }
        }

        /// <summary>
        /// cancel create template
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btTempCancel_Click(object sender, EventArgs e)
        {
            if (IsCreateNewTemplate || selectedTemplate == null)
            {
                ClearInputField();
                txTempName.Focus();
            }
            else
            {
                FillInputField();
            }
        }

        /// <summary>
        /// select category changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTempCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTempCat.SelectedIndex >= 0)
            {
                selectedTypeIndex = cbTempCat.SelectedIndex;
            }
            else
            {
                selectedTypeIndex = -1;
            }
            ValidateInput();
        }

        /// <summary>
        /// add macro event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAddMacro_Click(object sender, EventArgs e)
        {
            MacroEditor editor = new MacroEditor();
            if (DialogResult.OK == editor.ShowDialog())
            {
                htxEmailContent.InsertText(editor.MacroText);
            }
        }
    }
}