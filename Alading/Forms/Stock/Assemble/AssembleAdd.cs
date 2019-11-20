using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Alading.Entity;
using Alading.Business;
using Alading.Core.Enum;
using DevExpress.XtraEditors;
using Alading.Taobao;


namespace Alading.Forms.Stock.Assemble
{
    public partial class AssembleAdd : DevExpress.XtraEditors.XtraForm
    {       
        public AssembleAdd()
        {
            InitializeComponent();
        }

        public AssembleAdd(AssembleItem assembleItem)
        {
            InitializeComponent();
            this.Text = "修改组合商品";
            //保存并新增 按钮 不可见
            simpleBtnSaveAdd.Visible = false;
            assembleAdd1.LoadAseemble(assembleItem);
        }

        private void AssembleAdd_Load(object sender, EventArgs e)
        {
            
        }


        private void simpleBtnSave_Click(object sender, EventArgs e)
        {
            SortedList<List<AssembleItem>, List<AssembleDetail>> assembleItemList = assembleAdd1.GetAssembleItemList();
            if (assembleItemList == null || assembleItemList.Count == 0 || assembleItemList.Keys[0].Count==0)
                return;

            //新建组合商品
            ReturnType type = ItemService.IsAssembleStockItemExisted(assembleItemList.Keys[0][0].OuterID, assembleItemList.Keys[0][0].SkuProps);
            if (type == ReturnType.NotExisted)
            {
                if (ItemService.AddAssembleItemDetails(assembleItemList.Keys[0], assembleItemList.Values[0]) == ReturnType.Success)
                {
                    XtraMessageBox.Show("新建组合商品成功", Constants.SYSTEM_PROMPT);
                }
                else
                {
                    XtraMessageBox.Show("新建组合商品失败", Constants.SYSTEM_PROMPT);
                }
                //清空组件值
                if (assembleAdd1.InitProps(false) == false)
                {
                    this.Close();
                }
            }
            else
            {
                if (type == ReturnType.PropertyExisted)
                {
                    XtraMessageBox.Show("组合属性重复,请重新选择", Constants.SYSTEM_PROMPT);
                }
                else
                {
                    XtraMessageBox.Show("商品编码重复,请重输", Constants.SYSTEM_PROMPT);
                }
            }
        }

        private void simpleBtnSaveAdd_Click(object sender, EventArgs e)
        {
            SortedList<List<AssembleItem>, List<AssembleDetail>> assembleItemList = assembleAdd1.GetAssembleItemList();
            if (assembleItemList == null || assembleItemList.Count == 0)
                return;

            ReturnType type = ItemService.IsAssembleStockItemExisted(assembleItemList.Keys[0][0].OuterID, assembleItemList.Keys[0][0].SkuProps);
            if (type == ReturnType.NotExisted)
            {
                if (ItemService.AddAssembleItemDetails(assembleItemList.Keys[0], assembleItemList.Values[0]) == ReturnType.Success)
                {
                    XtraMessageBox.Show("新建组合商品成功", Constants.SYSTEM_PROMPT);
                }
                else
                {
                    XtraMessageBox.Show("新建组合商品失败", Constants.SYSTEM_PROMPT);
                }
                //清空组件值
                assembleAdd1.InitProps(true);
            }
            else
            {
                if (type == ReturnType.PropertyExisted)
                {
                    XtraMessageBox.Show("组合属性重复,请重新选择", Constants.SYSTEM_PROMPT);
                }
                else
                {
                    XtraMessageBox.Show("商品编码重复,请重输", Constants.SYSTEM_PROMPT);
                }
            }                       
        }    

        private void simpleBtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        


    }   
}