using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Taobao;
using Alading.Business;
using Alading.Entity;
using Alading.Utils;
using Alading.Core.Enum;

namespace Alading.Forms.Item
{
    public partial class AssembleForm : DevExpress.XtraEditors.XtraForm
    {
        public AssembleForm()
        {
            InitializeComponent();
        }

        public AssembleForm(string  Iid)
        {
            InitializeComponent();
            iid = Iid;
        }

        //当前宝贝iid
        string iid = string.Empty;

        //加载所有被使用的组合商品
        private void AssembleForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<AssembleItem> itemAssembleList = ItemService.GetAllUsedAssembleItem();
                gAssembleItem.DataSource = itemAssembleList;
                gVAssembleItem.BestFitColumns();
                if (gVAssembleItem.FocusedRowHandle == 0)
                {
                    LoadProps();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //焦点行改变，加载当前焦点行组合商品的属性
        private void gvAssemble_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                LoadProps();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //加载组合商品的属性
        private void LoadProps()
        {
            AssembleItem assProduct = gVAssembleItem.GetFocusedRow() as AssembleItem;
            if (assProduct != null)
            {
                View_ShopItem item = new View_ShopItem();
                item.cid = assProduct.Cid;
                item.props = assProduct.Props;
                item.input_str = assProduct.InputStr == null ? string.Empty : assProduct.InputStr;
                item.input_pids = assProduct.InputPids == null ? string.Empty : assProduct.InputPids;
                item.property_alias = assProduct.SkuProps;
                UIHelper.LoadItemPropValue(item, categoryRowKeyProps, categoryRowSaleProps, categoryRowNotKeyProps, categoryRowStockProps);
            }
        }

        //修改宝贝outer_id为当前组合商品的OuterID与商品类型
        private void btnAssociate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                AssembleItem assProduct = gVAssembleItem.GetFocusedRow() as AssembleItem;
                if (assProduct == null || string.IsNullOrEmpty(assProduct.OuterID))
                {
                    return;
                }

                if (ReturnType.Success == ItemService.UpdateItemType(iid, assProduct.OuterID, "组合商品"))
                {
                    XtraMessageBox.Show("关联成功！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //将修改后的outer_id赋给当前Form的Tag
                    this.Tag = assProduct.OuterID;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //模糊搜索
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textKeyWord.Text))
            {
                XtraMessageBox.Show("请输入关键词", Constants.SYSTEM_PROMPT,MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            List<AssembleItem> itemAssembleList = ItemService.SelectAssembleItem(textKeyWord.Text);
            gAssembleItem.DataSource = itemAssembleList;
            gVAssembleItem.BestFitColumns();
        }
    }
}