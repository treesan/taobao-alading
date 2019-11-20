using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Business;
using Alading.Entity;
using DevExpress.XtraTreeList.Nodes;
using Alading.Utils;
using DevExpress.XtraTreeList;
using System.Linq;

namespace Alading.Forms.Stock.Control
{
    public partial class ProductSelected : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 初始化加载库存类目信息
        /// </summary>
        public ProductSelected(DataTable dTable,string StockHouseCode)
        {
            InitializeComponent();
            productSelect1.dTable = dTable;
            productSelect1.stockHouseCode = StockHouseCode;
            productSelect1.AddColumns();
        }

        public ProductSelected()
        {
            InitializeComponent();
            productSelect1.dTable = new DataTable();
            productSelect1.AddColumns();
        }

        /// <summary>
        /// 商品展开加载其销售属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void tlStockItems_BeforeExpand(object sender, BeforeExpandEventArgs e)
        //{
        //    TreeListNode focusedNode = e.Node;
        //    tlStockItems.FocusedNode = focusedNode;
        //    //XtraMessageBox.Show(tlItemCat.IsUnboundMode.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    TreeListNodeTag tag = focusedNode.Tag as TreeListNodeTag;
            
        //    #region 获得当前节点的子节点
        //    if (!tag.HasExpanded)
        //    {
        //        tlStockItems.BeginUnboundLoad();
        //        List<StockProduct> stockProductList = StockProductService.GetStockProduct(i => i.OuterID == tag.Cid);
        //        List<StockItem> stockItemList=StockItemService.GetStockItem(c=>c.OuterID==tag.Cid);
        //        StockItem stockItem;
        //        if(stockItemList.Count>0)
        //        {
        //            stockItem=stockItemList.First();
        //        }
        //        else
        //        {
        //            XtraMessageBox.Show("aaaaa");
        //            return;
        //        }
        //        foreach (StockProduct stockProduct in stockProductList)
        //        {
        //            string propValue =UIHelper.GetKeyPidVidValue(stockItem.Cid, stockProduct.SkuProps);
        //            TreeListNode node = tlStockItems.AppendNode(new object[] { propValue }, focusedNode, new TreeListNodeTag(stockProduct.SkuOuterID));
        //            node.HasChildren = false;
        //        }
        //        tlStockItems.EndUnboundLoad();
        //        tag.HasExpanded = true;
        //    }
        //    #endregion
        //}

        /// <summary>
        /// 双击选择要添加的商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void tlStockItems_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    TreeListHitInfo hitInfo = tlStockItems.CalcHitInfo(new Point(e.X, e.Y));
        //    /*如果单击到单元格内*/
        //    if (hitInfo.HitInfoType == HitInfoType.Cell)
        //    {
        //        if (!hitInfo.Node.HasChildren)
        //        {
        //            TreeListNodeTag tag = hitInfo.Node.Tag as TreeListNodeTag;
        //            TreeListNodeTag fTag = hitInfo.Node.ParentNode.Tag as TreeListNodeTag;
        //            if(!skuOutIdList.Contains(tag.Cid))
        //            {
        //                List<StockProduct> stockProductList = StockProductService.GetStockProduct(i => i.SkuOuterID == tag.Cid);
        //                List<StockItem> stockItemList = StockItemService.GetStockItem(c => c.OuterID == fTag.Cid);
        //                if (stockProductList.Count > 0 && stockItemList.Count > 0)
        //                {
        //                    StockProduct stockProduct = stockProductList.First();
        //                    StockItem stockItem = stockItemList.First();
        //                    if (gridCtrlSelected.DataSource != null)
        //                    {
        //                        table = gridCtrlSelected.DataSource as DataTable;
        //                    }
                            
                            
        //                }
        //                else
        //                {
        //                    return;
        //                } 
        //            }
        //        }
        //    }
        //}

        private void btnEnter_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < productSelect1.gridViewStockItem.RowCount; i++)
            {
                DataRow fRow = productSelect1.gridViewStockItem.GetDataRow(i);
                if (Convert.ToBoolean(fRow["Select"]) && !productSelect1.skuOutIdList.Contains(fRow["SkuOuterID"].ToString()))
                {
                    DataRow row = productSelect1.dTable.NewRow();
                    row["Select"] = false;
                    row["SaleProps"] = fRow["SaleProps"];
                    row["Name"] = fRow["Name"];
                    row["SkuOuterID"] = fRow["SkuOuterID"];
                    row["Model"] = fRow["Model"];
                    row["Specification"] = fRow["Specification"];
                    row["Num"] = fRow["Num"];
                    row["StockCatName"] = fRow["StockCatName"];
                    row["CatName"] = fRow["CatName"];
                    row["Cid"] = fRow["Cid"];//淘宝类目
                    row["OuterID"] = fRow["OuterID"];
                    row["TaxCode"] = fRow["TaxCode"];
                    //用于展示所选商品的属性  
                    row["Props"] = fRow["Props"];//
                    row["InputPids"] = fRow["InputPids"];//
                    row["InputStr"] = fRow["InputStr"];//
                    row["Property_Alias"] = fRow["Property_Alias"];//

                    row["StockUnitName"] = fRow["StockUnitName"];//计量单位

                    #region 入库需要信息
                    row["SkuPrice"] = fRow["SkuPrice"];//销售价
                    row["HouseName"] = fRow["HouseName"];//仓库名称
                    row["LayoutName"] = fRow["LayoutName"];//库位名称
                    row["HouseCode"] = fRow["HouseCode"];//仓库编号
                    row["LayoutCode"] = fRow["LayoutCode"];//库位编号
                    #endregion

                    productSelect1.dTable.Rows.Add(row);
                    productSelect1.skuOutIdList.Add(row["SkuOuterID"].ToString());
                    //List<View_StockItemProduct> tempVsipList = View_StockItemProductService.GetView_StockItemProduct(c => c.SkuOuterID == row["SkuOuterID"].ToString());
                    //if (tempVsipList != null && tempVsipList.Count > 0)
                    //{
                    //    productSelect1.vsipList.Add(tempVsipList.First());
                    //}
                }
            }
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            productSelect1.dTable.Rows.Clear();
            this.Close();
        }    
    }
}