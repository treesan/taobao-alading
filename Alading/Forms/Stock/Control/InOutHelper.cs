using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Business;
using System.Collections;
using DevExpress.XtraEditors.Repository;
using System.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Alading.Utils;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using Alading.Core.Enum;
using DevExpress.XtraBars;
using Alading.Taobao;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraGrid.Columns;

namespace Alading.Forms.Stock.Control
{
    public class InOutHelper
    {
        #region 全局变量
        //防止TextChanged事件触发两次
        bool isChange = true;
        #endregion

        /// <summary>
        /// 加载全部仓库
        /// </summary>
        /// <param name="repositoryItemComboBox"></param>
        public void LoadAllHouse(RepositoryItemComboBox repositoryItemComboBoxHouse)
        {
            //加载所有仓库
            List<StockHouse> list = StockHouseService.GetAllStockHouse();
            Hashtable table = new Hashtable();
            int i = 0;
            foreach (StockHouse sh in list)
            {
                repositoryItemComboBoxHouse.Items.Add(sh.HouseName);
                table.Add(i++, sh.StockHouseCode);
            }
            repositoryItemComboBoxHouse.Tag = table;
        }

        /// <summary>
        /// 加载库位和商品属性
        /// </summary>
        /// <param name="repositoryItemComboBoxLayout"></param>
        /// <param name="houseCode"></param>
        /// <param name="skuOuterID"></param>
        /// <param name="outerID"></param>
        /// <param name="categoryRowKeyProps"></param>
        /// <param name="categoryRowSaleProps"></param>
        /// <param name="categoryRowNotKeyProps"></param>
        /// <param name="categoryRowStockProps"></param>
        public void LoadLayoutAndProps(RepositoryItemComboBox repositoryItemComboBoxLayout, GridView gridView
            , CategoryRow categoryRowKeyProps, CategoryRow categoryRowSaleProps, CategoryRow categoryRowNotKeyProps, CategoryRow categoryRowStockProps)
        {
            DataRow row = gridView.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }

            string houseCode = row["HouseCode"] == null ? string.Empty : row["HouseCode"].ToString();
            string outerID = row["OuterID"] == null ? string.Empty : row["OuterID"].ToString();
            string skuOuterID = row["SkuOuterID"] == null ? string.Empty : row["SkuOuterID"].ToString();

            repositoryItemComboBoxLayout.Items.Clear();
            if (!string.IsNullOrEmpty(houseCode))
            {
                LoadLayout(repositoryItemComboBoxLayout, houseCode);
            }

            /*点击显示商品属性*/
            if (!string.IsNullOrEmpty(outerID) && !string.IsNullOrEmpty(skuOuterID))
            {
                StockItem stockItem = StockItemService.GetStockItemByOutId(outerID);
                StockProduct stockProduct = StockProductService.GetStockProduct(skuOuterID);
                if (stockItem != null && stockProduct != null)
                {
                    View_ShopItem item = new View_ShopItem();
                    item.props = stockItem.Props;
                    item.input_pids = stockItem.InputPids;
                    item.input_str = stockItem.InputStr;
                    item.property_alias = stockProduct.PropsAlias;
                    item.cid = stockItem.Cid;
                    UIHelper.LoadItemPropValue(item, categoryRowKeyProps, categoryRowSaleProps, categoryRowNotKeyProps, categoryRowStockProps);
                }
            }
        }

        /// <summary>
        /// 根据HouseCode加载库位
        /// </summary>
        /// <param name="repositoryItemComboBoxLayout"></param>
        /// <param name="houseCode"></param>
        public void LoadLayout(RepositoryItemComboBox repositoryItemComboBoxLayout, string houseCode)
        {
            repositoryItemComboBoxLayout.Items.Clear();
            List<StockLayout> list = StockLayoutService.GetStockLayout(c => c.StockHouseCode == houseCode);
            Hashtable layoutTable = new Hashtable();
            int i = 0;
            foreach (StockLayout sl in list)
            {
                repositoryItemComboBoxLayout.Items.Add(sl.LayoutName);
                layoutTable.Add(i++, sl.StockHouseCode);
            }
            repositoryItemComboBoxLayout.Tag = layoutTable;
        }

        /// <summary>
        /// 加载付款方式
        /// </summary>
        /// <param name="comboBoxEdit"></param>
        public void GetPayType(ComboBoxEdit comboBoxEdit)
        {
            if (comboBoxEdit.Properties.Items.Count == 0)
            {
                comboBoxEdit.Properties.Items.Add("请选择");
                List<Code> payTypeList = CodeService.GetCode(c => c.CodeCategory.Trim() == "付款方式");
                foreach (Code payType in payTypeList)
                {
                    comboBoxEdit.Properties.Items.Add(payType.CodeName);
                }
                if (comboBoxEdit.Properties.Items.Count >= 1)
                {
                    comboBoxEdit.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 通过条形码获取商品
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="gridView"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public bool GetProByBarCode(string barCode, GridView gridView, DataTable dataTable)
        {
            View_StockItemProduct vsip = View_StockItemProductService.GetView_StockItemProductBySkuOuterId(barCode);
            StockHouseProduct houseProduct = StockHouseService.GetStockHouseProduct(barCode);
            if (vsip!=null)
            {
                DataRow dRow = dataTable.NewRow();
                int rowCount = gridView.RowCount;
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow row = gridView.GetDataRow(i);
                    if (row["SkuOuterID"].ToString() == vsip.SkuOuterID)
                    {
                        return true;
                    }
                }
                //dRow["Select"] = false;
                dRow["CatName"] = vsip.CatName;
                dRow["StockCatName"] = vsip.StockCatName;
                dRow["ProductName"] = vsip.Name;
                dRow["Num"] = vsip.SkuQuantity;
                dRow["HouseName"] = houseProduct.HouseName;
                dRow["LayoutName"] = houseProduct.LayoutName;
                dRow["SaleProps"] = vsip.SkuProps_Str;
                dRow["Specification"] = vsip.Specification;
                dRow["Model"] = vsip.Model;
                dRow["SkuOuterID"] = barCode;
                dRow["OuterID"] = vsip.OuterID;
                dRow["HouseCode"] = houseProduct.HouseCode;
                dRow["LayoutCode"] = houseProduct.LayoutCode;
                //dRow["Count"] = 0.0;
                dRow["price"] = vsip.SkuPrice;
                dRow["StockUnitName"] = vsip.StockUnitName;
                dataTable.Rows.Add(dRow);
                gridView.BestFitColumns();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加Columns
        /// </summary>
        /// <param name="dTable"></param>
        public void AddColumns(GridControl gridCtrl, DataTable dTable)
        {
            dTable.Columns.Add("Select", typeof(bool));//选择
            dTable.Columns.Add("CatName");//淘宝类目
            dTable.Columns.Add("StockCatName");//商品类目
            dTable.Columns.Add("ProductName");//商品名称
            dTable.Columns.Add("Num");//商品库存总数量
            dTable.Columns.Add("HouseName");//仓库名称
            dTable.Columns.Add("LayoutName");//库位名称
            dTable.Columns.Add("SaleProps");//销售属性
            dTable.Columns.Add("Specification");//规格
            dTable.Columns.Add("Model");//型号
            dTable.Columns.Add("StockUnitName");
            dTable.Columns.Add("SkuOuterID");
            dTable.Columns.Add("OuterID");
            dTable.Columns.Add("HouseCode");
            dTable.Columns.Add("LayoutCode");
            dTable.Columns.Add("Count", typeof(double));//出入库数量
            dTable.Columns.Add("price", typeof(double));
            dTable.Columns.Add("TotalMoney", typeof(double));
            dTable.Columns.Add("Tax", typeof(double));
            //出库备注
            dTable.Columns.Add("DetailRemark");
            gridCtrl.DataSource = dTable.DefaultView;
        }

        /// <summary>
        /// 添加Columns
        /// </summary>
        /// <param name="dTable"></param>
        public void AddTaxColumns(GridControl gridCtrl, DataTable dTable)
        {
            dTable.Columns.Add("Select", typeof(bool));//选择
            dTable.Columns.Add("CatName");//淘宝类目
            dTable.Columns.Add("StockCatName");//商品类目
            dTable.Columns.Add("ProductName");//商品名称
            dTable.Columns.Add("Num");//商品库存总数量
            dTable.Columns.Add("HouseName");//仓库名称
            dTable.Columns.Add("LayoutName");//库位名称
            dTable.Columns.Add("SaleProps");//销售属性
            dTable.Columns.Add("Specification");//规格
            dTable.Columns.Add("Model");//型号
            dTable.Columns.Add("StockUnitName");
            dTable.Columns.Add("SkuOuterID");
            dTable.Columns.Add("OuterID");
            dTable.Columns.Add("HouseCode");
            dTable.Columns.Add("LayoutCode");
            dTable.Columns.Add("Count", typeof(double));//出入库数量
            dTable.Columns.Add("price", typeof(double));
            dTable.Columns.Add("TotalFee", typeof(double));
            dTable.Columns.Add("Tax", typeof(double));
            dTable.Columns.Add("TaxFee", typeof(double));
            dTable.Columns.Add("TotalMoney", typeof(double));
            dTable.Columns.Add("FeeNotContainsTax", typeof(double));
            dTable.Columns.Add("TaxCode", typeof(string));
            dTable.Columns.Add("DetailRemark");
            gridCtrl.DataSource = dTable.DefaultView;
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="dTable"></param>
        /// <param name="row"></param>
        public void SetColumnsValue(DataTable dTable, DataTable table, List<string> skuOuterIDList)
        {
            foreach (DataRow row in table.Rows)
            {
                string skuOuterID = row["SkuOuterID"] == null ? string.Empty : row["SkuOuterID"].ToString();
                if (!skuOuterIDList.Contains(skuOuterID))
                {
                    DataRow dRow = dTable.NewRow();
                    dRow["Select"] = false;
                    dRow["CatName"] = row["CatName"];
                    dRow["StockCatName"] = row["StockCatName"];
                    dRow["ProductName"] = row["Name"];
                    dRow["Num"] = row["Num"];
                    dRow["HouseName"] = row["HouseName"];
                    dRow["LayoutName"] = row["LayoutName"];
                    dRow["SaleProps"] = row["SaleProps"];
                    dRow["Specification"] = row["Specification"];
                    dRow["Model"] = row["Model"];
                    dRow["SkuOuterID"] = row["SkuOuterID"] == null ? string.Empty : row["SkuOuterID"];
                    dRow["OuterID"] = row["OuterID"];
                    dRow["HouseCode"] = row["HouseCode"];
                    dRow["LayoutCode"] = row["LayoutCode"];
                    //dRow["Tax"] = ;
                    //dRow["Count"] = 0.0;
                    //dRow["TotalMoney"] = 0.0;
                    dRow["DetailRemark"] = string.Empty;

                    if (row["SkuPrice"] != null && !string.IsNullOrEmpty(row["SkuPrice"].ToString()))
                    {
                        dRow["price"] = double.Parse(row["SkuPrice"].ToString());
                    }
                    else
                    {
                        dRow["price"] = 0.0;
                    }

                    dRow["StockUnitName"] = row["StockUnitName"];
                    dTable.Rows.Add(dRow);
                    skuOuterIDList.Add(skuOuterID);
                }
            }
        }

        #region 出入库单分页
        /// <summary>
        /// 加载分页数据
        /// </summary>
        /// <param name="barEditPage"></param>
        /// <param name="gridCtrl"></param>
        /// <param name="gridView"></param>
        /// <param name="inoutType"></param>
        /// <param name="rowCount"></param>
        public void ClickPopup(BarEditItem barEditPage, BarButtonItem firstPage, BarButtonItem forwardPage, BarButtonItem nextPage, BarButtonItem lastPage
            , BarButtonItem skipPage, GridControl gridCtrl, GridView gridView, int inoutType)
        {
            int rowCount = 0;
            ShowBill(gridCtrl, gridView, inoutType, 1, rowCount);

            if (rowCount == 0)
            {
                //赋初值 总页数为1
                barEditPage.Tag = 1;
            }
            else
            {
                //将总页数赋值给barEditPageNo.Tag
                barEditPage.Tag = rowCount % 40 == 0 ? rowCount / 40 : rowCount / 40 + 1;
            }

            firstPage.Enabled = false;
            forwardPage.Enabled = false;
            if (int.Parse(barEditPage.Tag.ToString()) == 1)
            {
                nextPage.Enabled = false;
                lastPage.Enabled = false;
                skipPage.Enabled = false;
            }
            else
            {
                nextPage.Enabled = true;
                lastPage.Enabled = true;
                skipPage.Enabled = true;
            }
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="barEditPage"></param>
        /// <param name="gridCtrl"></param>
        /// <param name="gridView"></param>
        /// <param name="inoutType"></param>
        /// <param name="rowCount"></param>
        public void LoadFirstPage(BarEditItem barEditPage, BarButtonItem firstPage, BarButtonItem forwardPage, BarButtonItem nextPage, BarButtonItem lastPage
            , BarButtonItem skipPage, GridControl gridCtrl, GridView gridView, int inoutType)
        {
            int rowCount = 0;
            ShowBill(gridCtrl, gridView, inoutType, 1, rowCount);
            barEditPage.EditValue = 1;

            firstPage.Enabled = false;
            forwardPage.Enabled = false;
            nextPage.Enabled = true;
            lastPage.Enabled = true;
            skipPage.Enabled = true;
        }

        /// <summary>
        /// 加载上一页
        /// </summary>
        /// <param name="barEditPage"></param>
        /// <param name="gridCtrl"></param>
        /// <param name="gridView"></param>
        /// <param name="inoutType"></param>
        /// <param name="rowCount"></param>
        public void LoadForwardPage(BarEditItem barEditPage, BarButtonItem firstPage, BarButtonItem forwardPage, BarButtonItem nextPage, BarButtonItem lastPage
            , BarButtonItem skipPage, GridControl gridCtrl, GridView gridView, int inoutType)
        {
            if (barEditPage.EditValue != null)
            {
                int rowCount = 0;
                int page = int.Parse(barEditPage.EditValue.ToString()) - 1;
                ShowBill(gridCtrl, gridView, inoutType, page, rowCount);
                barEditPage.EditValue = page;

                if (page == 1)
                {
                    firstPage.Enabled = false;
                    forwardPage.Enabled = false;
                    nextPage.Enabled = true;
                    lastPage.Enabled = true;
                    skipPage.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 加载下一页
        /// </summary>
        /// <param name="barEditPage"></param>
        /// <param name="gridCtrl"></param>
        /// <param name="gridView"></param>
        /// <param name="inoutType"></param>
        /// <param name="rowCount"></param>
        public void LoadNextPage(BarEditItem barEditPage, BarButtonItem firstPage, BarButtonItem forwardPage, BarButtonItem nextPage, BarButtonItem lastPage
            , BarButtonItem skipPage, GridControl gridCtrl, GridView gridView, int inoutType)
        {
            if (barEditPage.EditValue != null)
            {
                int rowCount = 0;
                int page = int.Parse(barEditPage.EditValue.ToString()) + 1;
                ShowBill(gridCtrl, gridView, inoutType, page, rowCount);
                barEditPage.EditValue = page;

                if (page == int.Parse(barEditPage.Tag.ToString()))
                {
                    firstPage.Enabled = true;
                    forwardPage.Enabled = true;
                    nextPage.Enabled = false;
                    lastPage.Enabled = false;
                    skipPage.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 加载尾页
        /// </summary>
        /// <param name="barEditPage"></param>
        /// <param name="gridCtrl"></param>
        /// <param name="gridView"></param>
        /// <param name="inoutType"></param>
        /// <param name="rowCount"></param>
        public void LoadLastPage(BarEditItem barEditPage, BarButtonItem firstPage, BarButtonItem forwardPage, BarButtonItem nextPage, BarButtonItem lastPage
            , BarButtonItem skipPage, GridControl gridCtrl, GridView gridView, int inoutType)
        {
            if (barEditPage.EditValue != null)
            {
                int rowCount = 0;
                int page = int.Parse(barEditPage.Tag.ToString());
                ShowBill(gridCtrl, gridView, inoutType, page, rowCount);
                barEditPage.EditValue = page;

                firstPage.Enabled = true;
                forwardPage.Enabled = true;
                nextPage.Enabled = false;
                lastPage.Enabled = false;
                skipPage.Enabled = true;
            }
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="barEditPage"></param>
        /// <param name="gridCtrl"></param>
        /// <param name="gridView"></param>
        /// <param name="inoutType"></param>
        /// <param name="rowCount"></param>
        public void LoadSkipPage(BarEditItem barEditPage, BarButtonItem firstPage, BarButtonItem forwardPage, BarButtonItem nextPage, BarButtonItem lastPage
            , BarButtonItem skipPage, GridControl gridCtrl, GridView gridView, int inoutType)
        {
            int rowCount = 0;
            if (int.Parse(barEditPage.EditValue.ToString()) > int.Parse(barEditPage.Tag.ToString()))
            {
                XtraMessageBox.Show("超过总页数", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                int page = int.Parse(barEditPage.Tag.ToString());
                ShowBill(gridCtrl, gridView, inoutType, page, rowCount);
                barEditPage.EditValue = page;

                firstPage.Enabled = true;
                forwardPage.Enabled = true;
                nextPage.Enabled = false;
                lastPage.Enabled = false;
                skipPage.Enabled = true;
            }
            else
            {
                ShowBill(gridCtrl, gridView, inoutType, int.Parse(barEditPage.EditValue.ToString()), rowCount);
                firstPage.Enabled = true;
                forwardPage.Enabled = true;
                nextPage.Enabled = true;
                lastPage.Enabled = true;
                skipPage.Enabled = true;
            }
        }

        /// <summary>
        /// 显示生产单
        /// </summary>
        /// <param name="page"></param>
        public void ShowBill(GridControl gridCtrl, GridView gridView, int inoutType, int page, int rowCount)
        {
            List<View_InOutDetailProduct> viewInOutBillList = StockInOutService.GetViewInOutDetailProduct(c => c.InOutType == inoutType, page, 40, out rowCount);
            gridCtrl.DataSource = viewInOutBillList;
            gridView.BestFitColumns();
        }
        #endregion

        /// <summary>
        /// 判断是否选择了仓库
        /// </summary>
        /// <param name="gridView"></param>
        public bool HouseSelect(GridView gridView)
        {
            for (int rowHandle = 0; rowHandle < gridView.RowCount; rowHandle++)
            {
                DataRow dataRow = gridView.GetDataRow(rowHandle);
                if (dataRow["HouseName"] == null || string.IsNullOrEmpty(dataRow["HouseName"].ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断是否选择了仓库
        /// </summary>
        /// <param name="gridView"></param>
        public bool LayoutSelect(GridView gridView)
        {
            for (int rowHandle = 0; rowHandle < gridView.RowCount; rowHandle++)
            {
                DataRow dataRow = gridView.GetDataRow(rowHandle);
                if (dataRow["LayoutName"] == null || string.IsNullOrEmpty(dataRow["LayoutName"].ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 计算销售价*数量的总金额 用于不需要考虑税率的界面
        /// </summary>
        /// <param name="gridView"></param>
        public void CalcTotlaFee(GridView gridView)
        {
            DataRow dataRow = gridView.GetFocusedDataRow();
            if (dataRow != null)
            {
                double Count = 0.0;
                double price = 0.0;
                if (dataRow["Count"] != null && !string.IsNullOrEmpty(dataRow["Count"].ToString()))
                {
                    Count = double.Parse(dataRow["Count"].ToString());
                }
                if (dataRow["price"] != null && !string.IsNullOrEmpty(dataRow["price"].ToString()))
                {
                    price = double.Parse(dataRow["price"].ToString());
                }
                dataRow["TotalMoney"] = Count * price;
            }
            gridView.BestFitColumns();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="dateEditInTime"></param>
        public bool Save(GridView gridView, InOutData inoutData)
        {
            if (gridView == null || gridView.RowCount == 0)
            {
                XtraMessageBox.Show("没有可保存的数据", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            bool isSelectHouse = HouseSelect(gridView);
            if (!isSelectHouse)
            {
                XtraMessageBox.Show("需要选择仓库", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            bool isSelectLayout = LayoutSelect(gridView);
            if (!isSelectLayout)
            {
                XtraMessageBox.Show("需要选择库位", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            //用于存放要保存到数据库的StockProduct
            List<Alading.Entity.StockProduct> stockProductList = new List<StockProduct>();

            //用于存放要保存到数据库的StockHouseProduct
            List<Alading.Entity.StockHouseProduct> stockHouseProList = new List<StockHouseProduct>();

            //用于存放要保存到数据库的StockDetail
            List<Alading.Entity.StockDetail> stockDetailList = new List<StockDetail>();

            List<string> outerSkuIdList = new List<string>();

            List<string> outerIdList = new List<string>();

            for (int rowHandle = 0; rowHandle < gridView.RowCount; rowHandle++)
            {
                DataRow dataRow = gridView.GetDataRow(rowHandle);

                #region 更新StockItem  StockProduct StockHouseProduct
                Alading.Entity.StockProduct stockProduct = new StockProduct();

                stockProduct.SkuOuterID = dataRow["SkuOuterID"] == null ? string.Empty : dataRow["SkuOuterID"].ToString();
                stockProduct.OuterID = dataRow["OuterId"] == null ? string.Empty : dataRow["OuterId"].ToString();
                //获取入库数量
                double inOutNum = 0.0;
                if (dataRow["Count"] == null || string.IsNullOrEmpty(dataRow["Count"].ToString()))
                {
                    stockProduct.SkuQuantity = 0;
                }
                else if (inoutData.isIn)
                {
                    inOutNum = double.Parse(dataRow["Count"].ToString());
                    stockProduct.SkuQuantity = int.Parse(inOutNum.ToString());
                }
                else
                {
                    inOutNum = double.Parse(dataRow["Count"].ToString());
                    stockProduct.SkuQuantity = -int.Parse(inOutNum.ToString());
                }
                //存放以准备保存到数据库
                stockProductList.Add(stockProduct);

                outerSkuIdList.Add(dataRow["SkuOuterID"].ToString());
                outerIdList.Add(dataRow["OuterId"].ToString());
                #endregion

                #region 添加或更新StockHouseProduct
                StockHouseProduct stockHousePro = new StockHouseProduct();
                stockHousePro.HouseCode = dataRow["HouseCode"] == null ? string.Empty : dataRow["HouseCode"].ToString();
                stockHousePro.LayoutCode = dataRow["LayoutCode"] == null ? string.Empty : dataRow["LayoutCode"].ToString();
                stockHousePro.SkuOuterID = stockProduct.SkuOuterID;
                stockHousePro.Num = stockProduct.SkuQuantity;
                stockHousePro.HouseName = dataRow["HouseName"].ToString();
                stockHousePro.LayoutName = dataRow["LayoutName"].ToString();
                stockHouseProList.Add(stockHousePro);
                #endregion

                #region 添加到StockDetail
                Alading.Entity.StockDetail stockDetail = new StockDetail();

                stockDetail.StockDetailCode = Guid.NewGuid().ToString();
                stockDetail.ProductSkuOuterId = stockProduct.SkuOuterID;
                stockDetail.InOutCode = inoutData.InOutCode;
                //仓库编号
                stockDetail.StockHouseCode = dataRow["HouseCode"] == null ? string.Empty : dataRow["HouseCode"].ToString();
                //库位编号
                stockDetail.StockLayOutCode = dataRow["LayoutCode"] == null ? string.Empty : dataRow["LayoutCode"].ToString();
                //商品价格
                stockDetail.Price = (float)stockProduct.SkuPrice;
                stockDetail.Quantity = int.Parse(inOutNum.ToString());
                stockDetail.DetailType = inoutData.InOutType;
                stockDetail.DetailRemark = dataRow["DetailRemark"] == null ? string.Empty : dataRow["DetailRemark"].ToString();
                //税率
                stockDetail.Tax = dataRow["Tax"] == null ? string.Empty : dataRow["Tax"].ToString();
                stockDetail.TotalFee = dataRow["TotalMoney"] == null ? 0 : float.Parse(dataRow["TotalMoney"].ToString());
                //商品的保质期
                stockDetail.DurabilityDate = System.DateTime.MaxValue;

                stockDetailList.Add(stockDetail);
                #endregion
            }

            #region 添加StockInOut
            //用于存放StockInOut
            List<Alading.Entity.StockInOut> stockInOutList = new List<Alading.Entity.StockInOut>();
            Alading.Entity.StockInOut stockInOut = new Alading.Entity.StockInOut();

            //进出库单编号
            stockInOut.InOutCode = inoutData.InOutCode;
            stockInOut.InOutTime = inoutData.InOutDateTime;
            stockInOut.OperatorCode = inoutData.OperatorCode;
            //经办人员
            stockInOut.OperatorName = inoutData.Operator;
            stockInOut.InOutType = inoutData.InOutType;
            //oid赋值
            stockInOut.TradeOrderCode = string.Empty;
            //付款方式
            stockInOut.PayType = inoutData.PayType;
            //保存现金折扣
            stockInOut.DiscountFee = float.Parse(inoutData.DiscountFee.ToString());
            //保存应付应收金额
            stockInOut.DueFee = float.Parse(inoutData.NeedToPay.ToString());
            stockInOut.InOutStatus = inoutData.InOutStatus;
            stockInOut.IsSettled = true;
            stockInOut.PayThisTime = float.Parse(inoutData.PayThisTime.ToString());
            stockInOut.PayTerm = inoutData.PayTerm;
            stockInOut.IncomeTime = inoutData.IncomeTime;
            stockInOut.AmountTax = float.Parse(inoutData.AmountTax.ToString());
            stockInOut.FreightCompany = inoutData.FreightCompany;
            stockInOut.FreightCode = inoutData.FreightCode;

            stockInOutList.Add(stockInOut);
            #endregion

            #region 添加或更新PayCharge
            PayCharge payCharge = new PayCharge();
            payCharge.PayChargeCode = Guid.NewGuid().ToString();
            //付款收款方式
            payCharge.PayChargeType = inoutData.InOutType;
            payCharge.InOutCode = inoutData.InOutCode;
            //付款人编号
            payCharge.PayerCode = string.Empty;
            //付款人名字
            payCharge.PayerName = string.Empty;
            //收款人编号
            payCharge.ChargerCode = string.Empty;
            //收款人名字
            payCharge.ChargerName = string.Empty;
            //操作时间
            payCharge.OperateTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            payCharge.OperatorCode = inoutData.OperatorCode;
            payCharge.OperatorName = inoutData.Operator;
            payCharge.PayChargeRemark = string.Empty;

            payCharge.TotalFee = inoutData.TotalFee;
            payCharge.NeedToPay = inoutData.NeedToPay;
            payCharge.AmountTax = inoutData.AmountTax;
            payCharge.PayThisTime = inoutData.PayThisTime;
            payCharge.DiscountFee = inoutData.DiscountFee;
            payCharge.IncomeDay = inoutData.PayTerm;
            payCharge.IncomeTime = inoutData.IncomeTime;
            #endregion

            StockProductService.UpdateStock(stockProductList, stockHouseProList, stockInOutList, stockDetailList, payCharge, outerSkuIdList, outerIdList);
            return true;
        }

        /// <summary>
        /// 加载业务员结点
        /// </summary>
        public void LoadTLBuyManNode(TreeList tlOperator)
        {
            tlOperator.BeginUnboundLoad();
            List<User> list = UserService.GetAllUser();

            foreach (User item in list)
            {
                /*!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!名称？？？？？？*/
                TreeListNode node = tlOperator.AppendNode(new object[] { item.nick }, null, new TreeListNodeTag(item.UserCode));
                //设置是否有子节点，有则会显示一个+号
                node.HasChildren = false;
            }
            tlOperator.EndUnboundLoad();
        }

        /// <summary>
        /// 静态数据，取所有税率，防止多次实例化重复取值。
        /// </summary>
        public static List<Tax> alltaxList = StockUnitService.GetAllTax();

        /// <summary>
        /// 计算税额
        /// </summary>
        /// <param name="taxCode"></param>
        /// <param name="tax"></param>
        /// <param name="taxFee"></param>
        /// <param name="totalFee"></param>
        /// <returns></returns>
        public float CaculateTaxFee(string taxCode, float tax, float taxFee, float totalFee)
        {
            Tax t = alltaxList.FirstOrDefault(c => c.TaxCode == taxCode);
            if (t != null)
            {
                if (t.Formula == (int)Formula.First)
                {
                    taxFee = totalFee / (1 + tax) * tax;
                }
                else if (t.Formula == (int)Formula.Second)
                {
                    taxFee = totalFee * tax;
                }
            }
            else
            {
                taxFee = totalFee * tax;
            }
            return taxFee;
        }

        /// <summary>
        /// 根据税率的编码显示其进项税率
        /// </summary>
        /// <param name="oldRowCount">原来view中有多少行</param>
        /// <param name="view">要操作的view</param>
        /// <param name="taxCodeColumn">税率编码列</param>
        /// <param name="taxColumn">进项税率列</param>
        public void DisPlayTax(int oldRowCount, GridView view, GridColumn taxCodeColumn, GridColumn taxColumn)
        {
            int rowCount = view.RowCount;
            view.BeginUpdate();
            for (int i = oldRowCount; i < rowCount; i++)
            {
                DataRow row = view.GetDataRow(i);
                if (row != null)
                {
                    string taxCode = row[taxCodeColumn.FieldName].ToString();
                    Tax tax = alltaxList.FirstOrDefault(c => c.TaxCode == taxCode);
                    if (tax != null)
                    {
                        row[taxColumn.FieldName] = tax.InputTax/100;
                    }
                }
            }
            view.EndUpdate();
        }

        /// <summary>
        /// 根据选择的是税票还是收据隐藏和显示税率，税额，不含税金额等
        /// </summary>
        /// <param name="radioGroup">收据和支票</param>
        /// <param name="gcTax">税率列</param>
        /// <param name="gcTaxFee">税额列</param>
        /// <param name="gcTotalMoney">含税金额列</param>
        /// <param name="gcFeeNotContainsTax">不含税金额列</param>
        public void RadioGroupSelectIndexChange(RadioGroup radioGroup, GridColumn gcTax, GridColumn gcTaxFee, GridColumn gcTotalMoney, GridColumn gcFeeNotContainsTax, string totalMoneyCaption)
        {
            if (radioGroup.SelectedIndex == 1)
            {
                gcTax.Visible = false;
                gcTaxFee.Visible = false;
                gcTotalMoney.Caption = totalMoneyCaption;
                gcFeeNotContainsTax.Visible = false;
            }
            else
            {
                gcTax.Visible = true;
                gcTax.VisibleIndex = 9;
                gcTaxFee.Visible = true;
                gcTaxFee.VisibleIndex = 10;
                gcTotalMoney.VisibleIndex = 11;
                gcTotalMoney.Caption = totalMoneyCaption;
                gcFeeNotContainsTax.Visible = true;
                gcFeeNotContainsTax.VisibleIndex = 12;
            }
        }

        #region 到账日期
        /// <summary>
        /// 时间变化导致天数变化
        /// </summary>
        /// <param name="dateEditIncomeTime">到账日期</param>
        /// <param name="textEditPayTerm">支付期限</param>
        public void CalculateIncomeDays(DateEdit dateEditIncomeTime, TextEdit textEditPayTerm)
        {
            if (dateEditIncomeTime.DateTime < DateTime.Now.Date)
            {
                dateEditIncomeTime.DateTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            }
            else if (dateEditIncomeTime.DateTime > DateTime.Now.Date.AddDays(999))
            {
                dateEditIncomeTime.DateTime = DateTime.Parse(DateTime.Now.Date.AddDays(999).ToShortDateString());
            }
            textEditPayTerm.Text = (dateEditIncomeTime.DateTime.Date - DateTime.Now.Date).Days.ToString();
        }

        /// <summary>
        /// 天数变化导致时间变化
        /// </summary>
        /// <param name="dateEditIncomeTime"></param>
        /// <param name="textEditPayTerm"></param>
        public void CalculateInComeTime(DateEdit dateEditIncomeTime, TextEdit textEditPayTerm)
        {
            if (!string.IsNullOrEmpty(textEditPayTerm.Text))
            {
                int day = int.Parse(textEditPayTerm.Text);
                dateEditIncomeTime.DateTime = DateTime.Parse(DateTime.Now.Date.AddDays(day).ToShortDateString());
            }
        }
        #endregion

        #region 货运公司 业务员 客户
        /// <summary>
        /// 货运公司
        /// </summary>
        /// <param name="treeList"></param>
        public void GetLogisticCompany(TreeList treeList)
        {
            List<LogisticCompany> list = LogisticCompanyService.GetAllLogisticCompany();

            foreach (LogisticCompany item in list)
            {
                TreeListNode node = treeList.AppendNode(new object[] { item.name }, null, item.code);
                //设置是否有子节点，有则会显示一个+号
                node.HasChildren = false;
            }
        }

        /// <summary>
        /// 加载业务员
        /// </summary>
        public void GetOperator(TreeList treeList)
        {
            if (treeList == null || treeList.Nodes.Count == 0)
            {
                List<User> list = UserService.GetAllUser().Distinct(new UserComparer()).ToList();

                foreach (User item in list.Distinct())
                {
                    TreeListNode node = treeList.AppendNode(new object[] { item.nick }, null, item.UserCode);
                    //设置是否有子节点，有则会显示一个+号
                    node.HasChildren = false;
                }
            }
        }

        /// <summary>
        /// 客户
        /// </summary>
        /// <param name="treeList"></param>
        public void GetConsumer(TreeList treeList)
        {
            List<Alading.Entity.Consumer> list = ConsumerService.GetAllConsumer();

            foreach (Alading.Entity.Consumer item in list)
            {
                TreeListNode node = treeList.AppendNode(new object[] { item.nick }, null, item.alipay);
                //设置是否有子节点，有则会显示一个+号
                node.HasChildren = false;
            }
        }
        #endregion

        #region 计算有税率界面金额
        /// <summary>
        /// 计算全部商品的合计金额 应收金额
        /// </summary>
        /// <param name="gridView"></param>
        public double CalcTaxTotalFee(GridView gridView)
        {
            double totalFee = 0.0;
            if (gridView != null)
            {
                for (int rowHandle = 0; rowHandle < gridView.RowCount; rowHandle++)
                {
                    DataRow row = gridView.GetDataRow(rowHandle);
                    if (row["TotalMoney"] != null && !string.IsNullOrEmpty(row["TotalMoney"].ToString()))
                    {
                        double totalMoney = double.Parse(row["TotalMoney"].ToString());
                        totalFee += totalMoney;
                    }
                }
            }
            return totalFee;
        }

        /// <summary>
        /// 计算含有税率界面的
        /// </summary>
        /// <param name="gridView"></param>
        public double CalcAmountTax(GridView gridView)
        {
            double AmountTax = 0.0;
            if (gridView != null)
            {
                for (int rowHandle = 0; rowHandle < gridView.RowCount; rowHandle++)
                {
                    DataRow row = gridView.GetDataRow(rowHandle);
                    if (row["TaxFee"] != null && !string.IsNullOrEmpty(row["TaxFee"].ToString()))
                    {
                        double taxFee = double.Parse(row["TaxFee"].ToString());
                        AmountTax += taxFee;
                    }
                }
            }
            return AmountTax;
        }

        /// <summary>
        /// 本次收付款金额改变
        /// </summary>
        /// <param name="tETotalFee"></param>
        /// <param name="tENeedToPay"></param>
        /// <param name="tEDisFee"></param>
        /// <param name="tEPayThisTime"></param>
        public void PayThisTimeChange(TextEdit tETotalFee, TextEdit tENeedToPay, TextEdit tEDisFee, TextEdit tEPayThisTime)
        {
            //应付应收款=合计金额-折扣金额-本次收款金额
            if (!string.IsNullOrEmpty(tETotalFee.Text) && !string.IsNullOrEmpty(tEPayThisTime.Text)
                && !string.IsNullOrEmpty(tEDisFee.Text) && isChange)
            {
                isChange = false;
                double disFee = double.Parse(tEDisFee.EditValue.ToString());
                double PayThisTime = double.Parse(tEPayThisTime.EditValue.ToString());
                if (disFee > double.Parse(tETotalFee.EditValue.ToString()) - PayThisTime)
                {
                    tEPayThisTime.Undo();
                }
                else
                {
                    tENeedToPay.EditValue = double.Parse(tETotalFee.EditValue.ToString()) - disFee - PayThisTime;
                    tEDisFee.EditValue = disFee;
                }
            }
            isChange = true;
        }

        /// <summary>
        /// 折扣金额改变
        /// </summary>
        /// <param name="tETotalFee"></param>
        /// <param name="tENeedToPay"></param>
        /// <param name="tEDisFee"></param>
        /// <param name="tEPayThisTime"></param>
        public void DisFeeChange(TextEdit tETotalFee, TextEdit tENeedToPay, TextEdit tEDisFee, TextEdit tEPayThisTime)
        {
            //应付应收款=合计金额-折扣金额-本次收款金额
            if (!string.IsNullOrEmpty(tETotalFee.Text) && !string.IsNullOrEmpty(tEPayThisTime.Text)
                && !string.IsNullOrEmpty(tEDisFee.Text) && isChange)
            {
                isChange = false;
                double disFee = double.Parse(tEDisFee.EditValue.ToString());
                double PayThisTime = double.Parse(tEPayThisTime.EditValue.ToString());
                if (disFee > double.Parse(tETotalFee.EditValue.ToString()) - PayThisTime)
                {
                    tEPayThisTime.Undo();
                }
                else
                {
                    tENeedToPay.EditValue = double.Parse(tETotalFee.EditValue.ToString()) - disFee - PayThisTime;
                    tEDisFee.EditValue = disFee;
                }
            }
            isChange = true;
        }
        #endregion

        #region 判断出入库单号是否重复
        public bool ExistInOutCode(string inOutCode)
        {
            Alading.Entity.StockInOut stockInOut = StockInOutService.GetStockInOut(inOutCode);
            if (stockInOut != null)
            {
                //存在
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }

    /// <summary>
    /// ViewShopItemInherit自定义比较器，根据outer_id比较两个是否为同种商品
    /// </summary>
    public class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            if (x == null && y == null)
                return false;
            return x.nick == y.nick;
        }
        public int GetHashCode(User obj)
        {
            return obj.ToString().GetHashCode();
        }
    }

    /// <summary>
    /// 传值
    /// </summary>
    public class InOutData
    {
        //出入库标志
        public bool isIn { get; set; }
        //出入库单号
        public string InOutCode { get; set; }

        //出入库方式
        public int InOutType { get; set; }

        //出入库对应的订单状态
        public int InOutStatus { get; set; }

        //出入库时间
        public DateTime InOutDateTime { get; set; }

        //业务员
        public string Operator { get; set; }

        //业务员编码
        public string OperatorCode { get; set; }

        //付款方式
        public int PayType { get; set; }

        //应付金额
        public double NeedToPay { get; set; }

        //是否本次付款
        public double PayThisTime { get; set; }

        //到账日期
        public DateTime IncomeTime { get; set; }

        //合计金额
        public double TotalFee { get; set; }

        //税率
        public double Tax { get; set; }

        //税额
        public double AmountTax { get; set; }

        //折扣金额
        public double DiscountFee { get; set; }

        //支付期限
        public int PayTerm { get; set; }

        //快递公司
        public string FreightCompany { get; set; }

        //快递公司编码
        public string FreightCode { get; set; }
    }
}
