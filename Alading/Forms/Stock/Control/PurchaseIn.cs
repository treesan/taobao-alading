using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraVerticalGrid.Rows;
using Alading.Business;
using Alading.Entity;
using Alading.Utils;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using System.Collections;
using Alading.Core.Enum;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using Alading.Taobao;
using DevExpress.Utils;

namespace Alading.Forms.Stock.Control
{
    [ToolboxItem(false)]
    public partial class PurchaseIn : DevExpress.XtraEditors.XtraUserControl
    {
        List<string> skuOutIdList = new List<string>();

        private DataTable dTable = new DataTable();

        public PurchaseIn()
        {
            InitializeComponent();
            dTable.Columns.Add("CatName", typeof(string));
            dTable.Columns.Add("ProductName", typeof(string));
            dTable.Columns.Add("SkuOuterID", typeof(string));
            dTable.Columns.Add("StockCatName", typeof(string));
            dTable.Columns.Add("OuterID", typeof(string));
            dTable.Columns.Add("SaleProps",typeof(string));
            dTable.Columns.Add("TotalCount",typeof(int));
            dTable.Columns.Add("TotalMoney",typeof(double));
            dTable.Columns.Add("Op");
            dTable.Columns.Add("PurchaseCode", typeof(string));
            dTable.Columns.Add("StockHouse", typeof(string));
            dTable.Columns.Add("StockLayout", typeof(string));
            dTable.Columns.Add("StockHouseCode", typeof(string));
            dTable.Columns.Add("StockLayoutCode", typeof(string));
            dTable.Columns.Add(gcSpecification.FieldName, typeof(string));
            dTable.Columns.Add(gcModel.FieldName, typeof(string));
            dTable.Columns.Add(gcPrice.FieldName, typeof(double));
            dTable.Columns.Add(gcTax.FieldName, typeof(double));
            dTable.Columns.Add(gcTaxFee.FieldName, typeof(double));
            dTable.Columns.Add(gcTaxCode.FieldName, typeof(string));
            dTable.Columns.Add(gcFeeNotContainsTax.FieldName, typeof(double));
            gridCtrlProduct.DataSource = dTable;
            gvProductSJ.BestFitColumns();
            dateEditPachaseTime.DateTime = DateTime.Parse(DateTime.Now.Date.ToShortDateString());
        }

        /// <summary>
        /// 验证所有必须输入的是否输入完了
        /// </summary>
        /// <returns></returns>
        private bool IsAllNecessaryInput()
        {
            if (string.IsNullOrEmpty(dateEditPachaseTime.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textEditInOutCode.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(pceOperator.Text))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证列表中的仓库，库位，数量，总价是否输入完全
        /// </summary>
        /// <returns></returns>
        private bool IsAllNecessaryCellInput()
        {
            int rowCount = gvProductSJ.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = gvProductSJ.GetDataRow(i);
                if (row["StockHouse"] == null||string.IsNullOrEmpty(row["StockHouse"].ToString()))
                {
                    return false;
                }
                if(row["StockLayout"]==null||string.IsNullOrEmpty(row["StockLayout"].ToString()))
                {
                    return false;
                }
                if (row["TotalCount"] == null || string.IsNullOrEmpty(row["TotalCount"].ToString()))
                {
                    return false;
                }
                if (row["TotalMoney"] == null || string.IsNullOrEmpty(row["TotalMoney"].ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 同外界交互的接口,生成入库单及明细
        /// </summary>
        public bool GetData(Alading.Entity.StockInOut stockInOut,PayCharge payCharge, List<StockDetail> sdList, List<StockHouseProduct> shpList, List<View_StockItemProduct> vsipList)
        {
            #region 验证
            /*验证是否选择了商品*/
            if (gvProductSJ.RowCount == 0)
            {
                XtraMessageBox.Show("请先选择一个商品！",Constants.SYSTEM_PROMPT,MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }
            if (!IsAllNecessaryInput())
            {
                XtraMessageBox.Show("请填写完整的入库单详情！（带*的为必填。）", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!IsAllNecessaryCellInput())
            {
                XtraMessageBox.Show("请将列表中的仓库、库位、数量及价格信息输入完整！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            string inoutCode=textEditInOutCode.Text.Trim();
            if (StockInOutService.GetAllStockInOut().FirstOrDefault(c => c.InOutCode == inoutCode) != null)
            {
                XtraMessageBox.Show("入库单编码与数据库中已有入库单编码重复，请重输！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            #endregion

            #region StockInOut
            stockInOut.AmountTax =!string.IsNullOrEmpty(textEditAmountTax.Text)? float.Parse(textEditAmountTax.Text):0;
            stockInOut.DiscountFee = !string.IsNullOrEmpty(textEditDiscountFee.Text)?float.Parse(textEditDiscountFee.Text):0;
            stockInOut.DueFee = !string.IsNullOrEmpty(textEditTotalFee.Text)?float.Parse(textEditTotalFee.Text):0;
            stockInOut.FreightCode =pceFreightCompany.Tag!=null? pceFreightCompany.Tag.ToString():string.Empty;
            stockInOut.FreightCompany = pceFreightCompany.Text!=null?pceFreightCompany.Text:string.Empty;
            stockInOut.IncomeTime = dateEditIncomeTime.DateTime;
            stockInOut.InOutCode = textEditInOutCode.Text;
            stockInOut.InOutStatus = (int)Alading.Core.Enum.InOutStatus.AllReach;
            stockInOut.InOutTime = DateTime.Now;
            stockInOut.InOutType = (int)InOutType.PurchaseIn;            
            /*权宜之计*/
            //stockInOut.货运单号
            stockInOut.OperatorCode = pceOperator.Tag!=null?pceOperator.Tag.ToString():string.Empty;
            stockInOut.OperatorName = pceOperator.Text!=null?pceOperator.Text:string.Empty;
            stockInOut.PayTerm = 0;
            stockInOut.PayThisTime = !string.IsNullOrEmpty(textEditPayThisTime.Text)?float.Parse(textEditPayThisTime.Text):0;
            stockInOut.PayType = comboPayType.SelectedIndex + 1;
            stockInOut.TradeOrderCode = string.Empty;//
            stockInOut.IsSettled = stockInOut.PayThisTime >= stockInOut.DueFee;

            #endregion

            #region 付款信息

            payCharge.AmountTax = !string.IsNullOrEmpty(textEditAmountTax.Text) ? float.Parse(textEditAmountTax.Text) : 0;
            payCharge.ChargerCode = string.Empty;/*付款编号*/
            payCharge.ChargerName = string.Empty;//
            payCharge.DiscountFee = !string.IsNullOrEmpty(textEditDiscountFee.Text) ? float.Parse(textEditDiscountFee.Text) : 0;
            payCharge.InOutCode = stockInOut.InOutCode;
            payCharge.NeedToPay = !string.IsNullOrEmpty(textEditNeedToPay.Text) ? float.Parse(textEditNeedToPay.Text) : 0;
            payCharge.OperateTime = DateTime.Now;
            payCharge.OperatorCode = string.Empty;//
            payCharge.OperatorName=string.Empty;//
            payCharge.PayChargeCode = string.Empty;//
            payCharge.PayChargeRemark = string.Empty;
            payCharge.PayChargeType = comboPayType.SelectedIndex;
            payCharge.PayerCode = string.Empty;
            payCharge.PayerName = string.Empty;
            payCharge.PayThisTime = !string.IsNullOrEmpty(textEditPayThisTime.Text) ? float.Parse(textEditPayThisTime.Text) : 0;
            payCharge.TotalFee = !string.IsNullOrEmpty(textEditTotalFee.Text) ? float.Parse(textEditTotalFee.Text) : 0;
            payCharge.IncomeDay = !string.IsNullOrEmpty(textEditPayTerm.Text) ? int.Parse(textEditPayTerm.Text) : 0;
            payCharge.IncomeTime = dateEditIncomeTime.DateTime;

            #endregion

            #region StockHouseProduct,View_StockItemProduct,StockDetail

            int count = gvProductSJ.RowCount;

            /*找到商品在仓库中的位置，并更新该仓库中商品的数量*/
            List<StockHouseProduct> allShpList = StockHouseService.GetAllStockHouseProduct();
            //IEnumerable<View_StockItemProduct> allVispList = View_StockItemProductService.GetAllView_StockItemProduct();
            for(int i=0;i<count;i++)
            {
                DataRow row = gvProductSJ.GetDataRow(i);
                //总金额
                double totalMoney = row["TotalMoney"] != null ? double.Parse(row["TotalMoney"].ToString()) : 0;
                //入库数量
                int num = row["TotalCount"] != null ? int.Parse(row["TotalCount"].ToString()) : 0;
                /*仓库名称*/
                string houseName = row[gcStockHouse.FieldName].ToString();
                /*库位名称*/
                string layoutName = row[gcStockLayout.FieldName].ToString();
                //最新进价
                double LastStockPrice = totalMoney / num;
                /*修改仓库商品表数量增加*/
                StockHouseProduct shp = allShpList.FirstOrDefault(c => c.HouseCode == row["StockHouseCode"].ToString() && c.SkuOuterID == row["SkuOuterID"].ToString() && c.LayoutCode == row["StockLayoutCode"].ToString());
                if (shp != null)
                {
                    shp.Num += num;
                    shpList.Add(shp);
                }
                else
                {
                    shp = new StockHouseProduct();
                    shp.HouseCode = row["StockHouseCode"].ToString();
                    shp.HouseProductCode = System.Guid.NewGuid().ToString();
                    shp.LayoutCode = row["StockLayoutCode"].ToString();
                    shp.Num = num;
                    shp.HouseName = houseName;
                    shp.LayoutName = layoutName;
                    shp.SkuOuterID = row["SkuOuterID"].ToString();
                    shpList.Add(shp);
                }
                View_StockItemProduct vsip = View_StockItemProductService.GetView_StockItemProductBySkuOuterId(row["SkuOuterID"].ToString());
                if (vsip != null)
                {
                    //视图无法直接修改其属性值，所以需要new一个然后给之赋值
                    View_StockItemProduct tempVsip = new View_StockItemProduct();
                    tempVsip.SkuOuterID = vsip.SkuOuterID;
                    tempVsip.OuterID = vsip.OuterID;

                    tempVsip.LastStockPrice = LastStockPrice;
                    int lastNum = vsip.SkuQuantity;
                    tempVsip.TotalQuantity = vsip.TotalQuantity+num;
                    tempVsip.SkuQuantity =vsip.SkuQuantity+ num;
                    //平均价格=（上次剩余商品的平均价格*上次剩余数量+本次总金额）/本次剩余数量
                    tempVsip.AvgStockPrice = (vsip.AvgStockPrice * lastNum + totalMoney) / vsip.SkuQuantity;
                    vsipList.Add(tempVsip);
                }
                /*价格问题该如何处理？？？*/

                StockDetail sd = new StockDetail();
                sd.DetailRemark = string.Empty;
                sd.DetailType = (int)Alading.Core.Enum.DetailType.PurchaseIn;
                sd.DurabilityDate = DateTime.MinValue;//有效期？
                sd.InOutCode = stockInOut.InOutCode;
                sd.HouseName = houseName;
                sd.LayoutName = layoutName;
                sd.Price = float.Parse((totalMoney/num).ToString());
                sd.Quantity = num;
                sd.StockDetailCode = System.Guid.NewGuid().ToString();
                sd.ProductSkuOuterId = row["SkuOuterId"].ToString();
                sd.StockHouseCode = row["StockHouseCode"].ToString();
                sd.StockLayOutCode = row["StockLayOutCode"].ToString();
                sd.Tax =string.Empty;//税额？？
                sd.TotalFee = float.Parse(totalMoney.ToString());
                sdList.Add(sd);
            }

            #endregion

            return true;
        }

        /// <summary>
        /// 取得商品在仓库中的信息并更新数量
        /// </summary>
        /// <param name="HouseCode"></param>
        /// <param name="SkuOuterID"></param>
        /// <param name="LayoutCode"></param>
        /// <param name="num"></param>
        /// <param name="shpList"></param>
        /// <returns></returns>
        bool GetStockHouseProduct(string HouseCode, string SkuOuterID, string LayoutCode, int num, List<StockHouseProduct> shpList)
        {
            List<StockHouseProduct> tempshpList = StockHouseService.GetStockHouseProduct(c => c.HouseCode == HouseCode && c.SkuOuterID == SkuOuterID && c.LayoutCode == LayoutCode);
            StockHouseProduct shp;
            if (tempshpList.Count > 0)
            {
                shp = tempshpList.First();
            }
            else
            {
                return false;
            }
            shp.Num += num;
            shpList.Add(shp);
            return true;
        }

        /// <summary>
        /// 完成一些准备工作，如入库单号的生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PurchaseIn_Load(object sender, EventArgs e)
        {
            //加载付款方式
            inoutHelper.GetPayType(comboPayType);
            //显示到账时间
            dateEditIncomeTime.Text = DateTime.Now.ToShortDateString();
            textEditPayTerm.Text = "0";

            List<StockHouse> list = StockHouseService.GetAllStockHouse();
            Hashtable table = new Hashtable();
            int i = 0;
            foreach (StockHouse sh in list)
            {
                repositoryItemComboBoxStockHouse.Items.Add(sh.HouseName);
                table.Add(i++, sh.StockHouseCode);
            }
            repositoryItemComboBoxStockHouse.Tag = table;
        }
        
        /// <summary>
        /// 选择商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnProductAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                int oldRowCount = gvProductSJ.RowCount;
                DataTable table = new DataTable();
                ProductSelected ps = new ProductSelected(table, null);
                waitForm.Close();
                DialogResult result = ps.ShowDialog();
                //DataTable dTable = new DataTable();
                //if (gridCtrlProduct.DataSource != null)
                //{
                //    dTable = gridCtrlProduct.DataSource as DataTable;
                //}
                //else
                //{
                //    dTable.Columns.Add("CatName");
                //    dTable.Columns.Add("ProductName");
                //    dTable.Columns.Add("SkuOuterID");
                //    dTable.Columns.Add("StockCatName");
                //    dTable.Columns.Add("OuterID");
                //    dTable.Columns.Add("SaleProps");
                //    dTable.Columns.Add("TotalCount");
                //    dTable.Columns.Add("TotalMoney");
                //    dTable.Columns.Add("Op");
                //    dTable.Columns.Add("PurchaseCode");
                //    dTable.Columns.Add("StockHouse");
                //    dTable.Columns.Add("StockLayout");
                //    dTable.Columns.Add("StockHouseCode");
                //    dTable.Columns.Add("StockLayoutCode");
                //}
                waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                waitForm.Show();
                foreach (DataRow row in table.Rows)
                {
                    string sku_outId = row["SkuOuterID"].ToString();
                    if (!skuOutIdList.Contains(sku_outId))
                    {
                        skuOutIdList.Add(sku_outId);
                        DataRow dRow = dTable.NewRow();
                        dRow["CatName"] = row["CatName"];
                        dRow["ProductName"] = row["Name"];
                        dRow["SkuOuterID"] = row["SkuOuterID"];
                        dRow["StockCatName"] = row["StockCatName"];
                        dRow["OuterID"] = row["OuterID"];
                        dRow["SaleProps"] = row["SaleProps"];
                        dRow[gcStockHouseCode.FieldName] = row["HouseCode"];
                        dRow[gcStockHouse.FieldName] = row["HouseName"];
                        dRow[gcStockLayout.FieldName] = row["LayoutName"];
                        dRow[gcStockLayoutCode.FieldName] = row["LayoutCode"];
                        dRow[gcTaxCode.FieldName] = row["TaxCode"];
                        dRow[gcPrice.FieldName] = row["SkuPrice"];
                        dRow[gcModel.FieldName] = row["Model"];
                        dRow[gcSpecification.FieldName] = row["Specification"];
                        dRow[gcTaxCode.FieldName] = row["TaxCode"];
                        dTable.Rows.Add(dRow);
                    }
                }
                /*显示税率*/
                inoutHelper.DisPlayTax(oldRowCount, gvProductSJ, gcTaxCode, gcTax);
                waitForm.Close();
                gvProductSJ.BestFitColumns();
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 自动计算应付金额
        /// </summary>
        /// <returns></returns>
        double GetNeedToPay()
        {
            double totalMoney = 0;
            int rowCount = gvProductSJ.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = gvProductSJ.GetDataRow(i);
                if (row[gcTotalMoney.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalMoney.FieldName].ToString()))
                {
                    totalMoney += double.Parse(row[gcTotalMoney.FieldName].ToString());
                }
            }
            return totalMoney;
        }

        /// <summary>
        /// 点击显示商品属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProductSJ_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow row = gvProductSJ.GetFocusedDataRow();
            if (row != null && row["OuterID"] != null && row["SkuOuterID"]!=null)
            {
                StockItem stockItem = StockItemService.GetStockItemByOutId(row["OuterID"].ToString());
                StockProduct stockProduct = StockProductService.GetStockProduct(row["SkuOuterID"].ToString());
                if (stockItem != null && stockProduct != null)
                {
                    View_ShopItem item = new View_ShopItem();
                    item.props = stockItem.Props;
                    item.input_pids = stockItem.InputPids;
                    item.input_str = stockItem.InputStr;
                    item.property_alias = stockProduct.PropsAlias;
                    item.cid = stockItem.Cid;
                    UIHelper.LoadItemPropValue(item, categoryKeyProps, categorySaleProps, categoryNotKeyProps, categoryInputProps);
                }
            }
        }

        #region pcc的选择

        private void tlSupplier_MouseDown(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = tlSupplier.CalcHitInfo(new Point(e.X, e.Y));
            /*如果单击到单元格内*/
            if (hitInfo.HitInfoType == HitInfoType.Cell)
            {
                TreeListNodeTag tag = hitInfo.Node.Tag as TreeListNodeTag;
                this.pceSupplier.Text = hitInfo.Node.GetDisplayText(0);
                this.pceSupplier.Tag = tag.Cid;
                this.pceSupplier.ClosePopup();
            }        
        }

        private void tlCarryCompany_MouseDown(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = tlFreightCompany.CalcHitInfo(new Point(e.X, e.Y));
            /*如果单击到单元格内*/
            if (hitInfo.HitInfoType == HitInfoType.Cell)
            {
                TreeListNodeTag tag = hitInfo.Node.Tag as TreeListNodeTag;
                this.pceFreightCompany.Text = hitInfo.Node.GetDisplayText(0);
                this.pceFreightCompany.Tag = tag.Cid;
                this.pceFreightCompany.ClosePopup();
            }      
        }

        private void tlPurchaseOrder_MouseDown(object sender, MouseEventArgs e)
        {
            //TreeListHitInfo hitInfo = tlPurchaseOrder.CalcHitInfo(new Point(e.X, e.Y));
            ///*如果单击到单元格内*/
            //if (hitInfo.HitInfoType == HitInfoType.Cell)
            //{
            //    TreeListNodeTag tag = hitInfo.Node.Tag as TreeListNodeTag;
            //    this.pcePurchaseOrder.Text = hitInfo.Node.GetDisplayText(0);
            //    this.pcePurchaseOrder.Tag = tag.Cid;
            //    this.pcePurchaseOrder.ClosePopup();
            //}
        }

        private void tlOperator_MouseDown(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = tlOperator.CalcHitInfo(new Point(e.X, e.Y));
            /*如果单击到单元格内*/
            if (hitInfo.HitInfoType == HitInfoType.Cell)
            {
                TreeListNodeTag tag = hitInfo.Node.Tag as TreeListNodeTag;
                this.pceOperator.Text = hitInfo.Node.GetDisplayText(0);
                this.pceOperator.Tag = tag.Cid;
                this.pceOperator.ClosePopup();
            }
        }

        #endregion

        #region 供应商，货运公司及采购订单

        /*记录四个treeList是否已经加载过数据，若已经加载过数据，再次下拉时间触发时不必再次加载*/
        bool isTLSupplierLoad = false;

        bool isTLFreightCompanyLoad = false;

        bool isTLPurchaseOrderLoad = false;

        bool isTLOperater = false;

        private void pceSupplier_Click(object sender, EventArgs e)
        {
            if (!isTLSupplierLoad)
            {
                LoadTLSupplierNode();
            }
        }

        private void pceFreightCompany_Click(object sender, EventArgs e)
        {
            if (!isTLFreightCompanyLoad)
            {
                LoadTLFreightCompanyNode();
            }
        }

        private void pcePurchaseOrder_Click(object sender, EventArgs e)
        {
            if (!isTLPurchaseOrderLoad)
            {
                LoadTLPurchaseOrderNode();
            }
        }

        InOutHelper inoutHelper = new InOutHelper();

        private void pceOperator_Click(object sender, EventArgs e)
        {
            if (!isTLOperater)
            {
                inoutHelper.LoadTLBuyManNode(tlOperator);
                isTLOperater = true;
            }
        }

        /// <summary>
        /// 加载供应商结点
        /// </summary>
        private void LoadTLSupplierNode()
        {
            tlSupplier.BeginUnboundLoad();
            List<Alading.Entity.Supplier> list = SupplierService.GetAllSupplier();

            foreach (Alading.Entity.Supplier item in list)
            {
                TreeListNode node = tlSupplier.AppendNode(new object[] { item.SupplierName }, null, new TreeListNodeTag(item.SupplierCode));
                //设置是否有子节点，有则会显示一个+号
                node.HasChildren = false;
            }
            isTLSupplierLoad = true;
            tlSupplier.EndUnboundLoad();
        }

        /// <summary>
        /// 加载货运公司结点
        /// </summary>
        private void LoadTLFreightCompanyNode()
        {
            tlFreightCompany.BeginUnboundLoad();
            List<LogisticCompany> list = LogisticCompanyService.GetAllLogisticCompany();

            foreach (LogisticCompany item in list)
            {
                TreeListNode node = tlFreightCompany.AppendNode(new object[] { item.name }, null, new TreeListNodeTag(item.id.ToString()));
                //设置是否有子节点，有则会显示一个+号
                node.HasChildren = false;
            }
            isTLFreightCompanyLoad = true;
            tlFreightCompany.EndUnboundLoad();
        }

        /// <summary>
        /// 加载采购单结点
        /// </summary>
        private void LoadTLPurchaseOrderNode()
        {
            //tlPurchaseOrder.BeginUnboundLoad();
            //List<PurchaseOrder> list = PurchaseOrderService.GetAllPurchaseOrder();

            //foreach (PurchaseOrder item in list)
            //{
            //    /*!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!名称？？？？？？*/
            //    TreeListNode node = tlPurchaseOrder.AppendNode(new object[] { item.PurchaserName }, null, new TreeListNodeTag(item.PurchaseOrderCode));
            //    //设置是否有子节点，有则会显示一个+号
            //    node.HasChildren = false;
            //}
            //isTLPurchaseOrderLoad = true;
            //tlPurchaseOrder.EndUnboundLoad();
        }      
       
        #endregion

        /// <summary>
        /// 选择仓库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemComboBoxStockHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit combo = sender as ComboBoxEdit;
            gvProductSJ.BeginUpdate();
            DataRow row = gvProductSJ.GetFocusedDataRow();
            row[gcStockHouse.FieldName] = combo.Properties.Items[combo.SelectedIndex];                           
            Hashtable table = repositoryItemComboBoxStockHouse.Tag as Hashtable;
            row["StockHouseCode"] = table[combo.SelectedIndex];
            gvProductSJ.EndUpdate();

            repositoryItemComboBoxStockLayout.Items.Clear();
            List<StockLayout> list = StockLayoutService.GetStockLayout(c => c.StockHouseCode == table[combo.SelectedIndex].ToString());
            Hashtable layoutTable = new Hashtable();
            int i = 0;
            foreach (StockLayout sl in list)
            {
                repositoryItemComboBoxStockLayout.Items.Add(sl.LayoutName);
                layoutTable.Add(i++, sl.StockLayoutCode);
            }
            repositoryItemComboBoxStockLayout.Tag = layoutTable;
            gvProductSJ.BestFitColumns();
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void AllClear()
        {
            skuOutIdList.Clear();
            foreach (BaseRow row in vGridCtrl.Rows)
            {
                row.ChildRows.Clear();
            }
            if (this.gridCtrlProduct.DataSource != null)
            {
                DataTable table = gridCtrlProduct.DataSource as DataTable;
                table.Rows.Clear();
                gridCtrlProduct.DataSource = table;
            }
            foreach (System.Windows.Forms.Control ct in groupControl1.Controls)
            {
                if (ct is Label)
                {

                }
                else
                {
                    ct.Text = string.Empty;
                }
            }
            foreach (System.Windows.Forms.Control ct in groupControl2.Controls)
            {
                if (ct is Label)
                {

                }
                else
                {
                    ct.Text = string.Empty;
                }
            } 
            gvProductSJ.BestFitColumns();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProductSJ_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = gvProductSJ.GetDataRow(e.RowHandle);
            string taxCode = row[gcTaxCode.FieldName] != null ? row[gcTaxCode.FieldName].ToString() : string.Empty;
            if (e.Column == gcStockHouse)
            {
                gvProductSJ.BeginUpdate();               
                row[gcStockHouse.FieldName] = e.Value;               
                gvProductSJ.EndUpdate();
            }
            else if (e.Column == gcStockLayout)
            {
                gvProductSJ.BeginUpdate();
                row[gcStockLayout.FieldName] = e.Value;
                gvProductSJ.EndUpdate();
            }
            else if (e.Column == gcTotalMoney)
            {
                int num = row[gcTotalCount.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalCount.FieldName].ToString()) ? int.Parse(row[gcTotalCount.FieldName].ToString()) : 1;
                float totalFee = e.Value != null && e.Value.ToString() != string.Empty ? float.Parse(e.Value.ToString()) : 0;
                float taxFee = row[gcTaxFee.FieldName] != null && !string.IsNullOrEmpty(row[gcTaxFee.FieldName].ToString()) ? float.Parse(row[gcTaxFee.FieldName].ToString()) : 0;
                float tax = row[gcTax.FieldName] != null && !string.IsNullOrEmpty(row[gcTax.FieldName].ToString()) ? float.Parse(row[gcTax.FieldName].ToString()) : 0;
                float price = row[gcPrice.FieldName] != null && !string.IsNullOrEmpty(row[gcPrice.FieldName].ToString()) ? float.Parse(row[gcPrice.FieldName].ToString()) : 0;
                float feeNotContainsTax = row[gcFeeNotContainsTax.FieldName] != null && !string.IsNullOrEmpty(row[gcFeeNotContainsTax.FieldName].ToString()) ? float.Parse(row[gcFeeNotContainsTax.FieldName].ToString()) : 0;

                if (totalFee == 0)
                {
                    taxFee = 0;
                    price = 0;
                    feeNotContainsTax = 0;
                }
                else
                {
                    price = totalFee / num;
                    feeNotContainsTax = totalFee / (1 + tax);
                    taxFee = totalFee - feeNotContainsTax;
                }

                gvProductSJ.BeginUpdate();
                row[gcTotalCount.FieldName] = num;
                row[gcTotalMoney.FieldName] = Math.Round(totalFee, 2);
                row[gcTaxFee.FieldName] = Math.Round(taxFee, 2);
                row[gcPrice.FieldName] = Math.Round(price, 2);
                row[gcFeeNotContainsTax.FieldName] = Math.Round(feeNotContainsTax, 2);
                gvProductSJ.EndUpdate();
                //textEditTotalFee.Text = GetNeedToPay().ToString();
            }
            else if (e.Column == gcTax)
            {
                int num = row[gcTotalCount.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalCount.FieldName].ToString()) ? int.Parse(row[gcTotalCount.FieldName].ToString()) : 1;
                float totalFee = row[gcTotalMoney.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalMoney.FieldName].ToString()) ? int.Parse(row[gcTotalMoney.FieldName].ToString()) : 0;
                float taxFee = row[gcTaxFee.FieldName] != null && !string.IsNullOrEmpty(row[gcTaxFee.FieldName].ToString()) ? float.Parse(row[gcTaxFee.FieldName].ToString()) : 0;
                float tax = e.Value != null && e.Value.ToString() != string.Empty ? float.Parse(e.Value.ToString()) : 0;
                float price = row[gcPrice.FieldName] != null && !string.IsNullOrEmpty(row[gcPrice.FieldName].ToString()) ? float.Parse(row[gcPrice.FieldName].ToString()) : 0;
                float feeNotContainsTax = row[gcFeeNotContainsTax.FieldName] != null && !string.IsNullOrEmpty(row[gcFeeNotContainsTax.FieldName].ToString()) ? float.Parse(row[gcFeeNotContainsTax.FieldName].ToString()) : 0;

                tax = tax / 100;
                feeNotContainsTax = totalFee / (1 + tax);
                taxFee = totalFee - feeNotContainsTax;

                gvProductSJ.BeginUpdate();
                row[gcTax.FieldName] = tax;
                row[gcTotalCount.FieldName] = num;
                row[gcTotalMoney.FieldName] = Math.Round(totalFee, 2);
                row[gcTaxFee.FieldName] = Math.Round(taxFee, 2);
                row[gcPrice.FieldName] = Math.Round(price, 2);
                row[gcFeeNotContainsTax.FieldName] = Math.Round(feeNotContainsTax, 2);
                gvProductSJ.EndUpdate();
                //textEditTotalFee.Text = GetNeedToPay().ToString();
            }
            else if (e.Column == gcTotalCount)
            {
                int num = e.Value != null && e.Value.ToString() != string.Empty ? int.Parse(e.Value.ToString()) : 1;
                float totalFee = row[gcTotalMoney.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalMoney.FieldName].ToString()) ? int.Parse(row[gcTotalMoney.FieldName].ToString()) : 0;
                float taxFee = row[gcTaxFee.FieldName] != null && !string.IsNullOrEmpty(row[gcTaxFee.FieldName].ToString()) ? float.Parse(row[gcTaxFee.FieldName].ToString()) : 0;
                float tax = row[gcTax.FieldName] != null && !string.IsNullOrEmpty(row[gcTax.FieldName].ToString()) ? float.Parse(row[gcTax.FieldName].ToString()) : 0;
                float price = row[gcPrice.FieldName] != null && !string.IsNullOrEmpty(row[gcPrice.FieldName].ToString()) ? float.Parse(row[gcPrice.FieldName].ToString()) : 0;
                float feeNotContainsTax = row[gcFeeNotContainsTax.FieldName] != null && !string.IsNullOrEmpty(row[gcFeeNotContainsTax.FieldName].ToString()) ? float.Parse(row[gcFeeNotContainsTax.FieldName].ToString()) : 0;

                totalFee = price * num;
                feeNotContainsTax = totalFee / (1 + tax);
                taxFee = totalFee - feeNotContainsTax;

                gvProductSJ.BeginUpdate();
                row[gcTotalCount.FieldName] = num;
                row[gcTotalMoney.FieldName] = Math.Round(totalFee, 2);
                row[gcTaxFee.FieldName] = Math.Round(taxFee, 2);
                row[gcPrice.FieldName] = Math.Round(price, 2);
                row[gcFeeNotContainsTax.FieldName] = Math.Round(feeNotContainsTax, 2);
                gvProductSJ.EndUpdate();
            }
            else if (e.Column == gcPrice)
            {
                int num = row[gcTotalCount.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalCount.FieldName].ToString()) ? int.Parse(row[gcTotalCount.FieldName].ToString()) : 1;
                float totalFee = row[gcTotalMoney.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalMoney.FieldName].ToString()) ? int.Parse(row[gcTotalMoney.FieldName].ToString()) : 0;
                float taxFee = row[gcTaxFee.FieldName] != null && !string.IsNullOrEmpty(row[gcTaxFee.FieldName].ToString()) ? float.Parse(row[gcTaxFee.FieldName].ToString()) : 0;
                float tax = row[gcTax.FieldName] != null && !string.IsNullOrEmpty(row[gcTax.FieldName].ToString()) ? float.Parse(row[gcTax.FieldName].ToString()) : 0;
                float price = e.Value != null && e.Value.ToString() != string.Empty ? float.Parse(e.Value.ToString()) : 0; ;
                float feeNotContainsTax = row[gcFeeNotContainsTax.FieldName] != null && !string.IsNullOrEmpty(row[gcFeeNotContainsTax.FieldName].ToString()) ? float.Parse(row[gcFeeNotContainsTax.FieldName].ToString()) : 0;

                totalFee = price * num;
                feeNotContainsTax = totalFee / (1 + tax);
                taxFee = totalFee - feeNotContainsTax;

                gvProductSJ.BeginUpdate();
                row[gcTotalCount.FieldName] = num;
                row[gcTotalMoney.FieldName] = Math.Round(totalFee, 2);
                row[gcTaxFee.FieldName] = Math.Round(taxFee, 2);
                row[gcPrice.FieldName] = Math.Round(price, 2);
                row[gcFeeNotContainsTax.FieldName] = Math.Round(feeNotContainsTax, 2);
                gvProductSJ.EndUpdate();
            }
            else if (e.Column == gcFeeNotContainsTax)
            {
                int num = row[gcTotalCount.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalCount.FieldName].ToString()) ? int.Parse(row[gcTotalCount.FieldName].ToString()) : 1;
                float totalFee = row[gcTotalMoney.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalMoney.FieldName].ToString()) ? int.Parse(row[gcTotalMoney.FieldName].ToString()) : 0;
                float taxFee = row[gcTaxFee.FieldName] != null && !string.IsNullOrEmpty(row[gcTaxFee.FieldName].ToString()) ? float.Parse(row[gcTaxFee.FieldName].ToString()) : 0;
                float tax = row[gcTax.FieldName] != null && !string.IsNullOrEmpty(row[gcTax.FieldName].ToString()) ? float.Parse(row[gcTax.FieldName].ToString()) : 0;
                float price = row[gcPrice.FieldName] != null && !string.IsNullOrEmpty(row[gcPrice.FieldName].ToString()) ? float.Parse(row[gcPrice.FieldName].ToString()) : 0;
                float feeNotContainsTax = e.Value != null && e.Value.ToString() != string.Empty ? float.Parse(e.Value.ToString()) : 0; ;


                taxFee = feeNotContainsTax * tax;
                totalFee = feeNotContainsTax + taxFee;
                price = totalFee / num;

                gvProductSJ.BeginUpdate();
                row[gcTotalCount.FieldName] = num;
                row[gcTotalMoney.FieldName] = Math.Round(totalFee, 2);
                row[gcTaxFee.FieldName] = Math.Round(taxFee, 2);
                row[gcPrice.FieldName] = Math.Round(price, 2);
                row[gcFeeNotContainsTax.FieldName] = Math.Round(feeNotContainsTax, 2);
                gvProductSJ.EndUpdate();
            }
            gvProductSJ.BestFitColumns();
            //展示总金额
            GetTaxAndFee();
        }

        /// <summary>
        /// 库位选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemComboBoxStockLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit combo = sender as ComboBoxEdit;
            combo.Text = combo.Properties.Items[combo.SelectedIndex].ToString();
            Hashtable table = repositoryItemComboBoxStockLayout.Tag as Hashtable;
            DataRow row = gvProductSJ.GetFocusedDataRow();
            row[gcStockLayout.FieldName] = combo.Properties.Items[combo.SelectedIndex];
            row["StockLayoutCode"] = table[combo.SelectedIndex];
            gvProductSJ.BestFitColumns();
        }

        /// <summary>
        /// 焦点行转变，刷新库位的选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProductSJ_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                DataRow row = gvProductSJ.GetFocusedDataRow();
                repositoryItemComboBoxStockLayout.Items.Clear();
                if (row != null && row["StockHouseCode"] != null)
                {
                    List<StockLayout> list = StockLayoutService.GetStockLayout(c => c.StockHouseCode == row["StockHouseCode"].ToString());
                    Hashtable layoutTable = new Hashtable();
                    int i = 0;
                    foreach (StockLayout sl in list)
                    {
                        repositoryItemComboBoxStockLayout.Items.Add(sl.LayoutName);
                        layoutTable.Add(i++, sl.StockHouseCode);
                    }
                    repositoryItemComboBoxStockLayout.Tag = layoutTable;
                }
                waitForm.Close();
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnProductDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow row = gvProductSJ.GetFocusedDataRow();
            if (row != null)
            {
                DialogResult result=XtraMessageBox.Show("您确定要从列表中删除该商品？", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                    waitForm.Show();
                    try
                    {
                        string skuouterID = row[gcSkuOutID.FieldName].ToString();
                        if (skuOutIdList.Contains(skuouterID))
                        {
                            skuOutIdList.Remove(skuouterID);
                        }
                        gvProductSJ.DeleteRow(gvProductSJ.FocusedRowHandle);
                        waitForm.Close();
                    }
                    catch (Exception ex)
                    {
                        waitForm.Close();
                        XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            gvProductSJ.BestFitColumns();
        }

        private void gridCtrlProduct_Click(object sender, EventArgs e)
        {

        }

        private void bbtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Get();
        }

        void Get()
        {
            if (bEditBarCode.EditValue != null && !string.IsNullOrEmpty(bEditBarCode.EditValue.ToString()))
            {
                WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                waitForm.Show();
                try
                {
                    string skuOuterID = bEditBarCode.EditValue.ToString();
                    List<View_StockItemProduct> vsipList = View_StockItemProductService.GetView_StockItemProduct(v => v.SkuOuterID == skuOuterID);
                    if (vsipList.Count > 0)
                    {
                        View_StockItemProduct vsip = vsipList.First();
                        DataRow dRow = dTable.NewRow();
                        int rowCount = gvProductSJ.RowCount;
                        for (int i = 0; i < rowCount; i++)
                        {
                            DataRow row = gvProductSJ.GetDataRow(i);
                            if (row["SkuOuterID"].ToString() == vsip.SkuOuterID)
                            {
                                return;
                            }
                        }
                        dRow["CatName"] = vsip.CatName;
                        dRow["ProductName"] = vsip.Name;
                        dRow["SkuOuterID"] = vsip.SkuOuterID;
                        dRow["StockCatName"] = vsip.StockCatName;
                        dRow["OuterID"] = vsip.OuterID;
                        dRow["SaleProps"] = vsip.SkuProps_Str;
                        dRow["TotalMoney"] = vsip.SkuPrice;
                        dTable.Rows.Add(dRow);
                        waitForm.Close();
                        gvProductSJ.BestFitColumns();
                    }
                    else
                    {
                        waitForm.Close();
                        XtraMessageBox.Show("找不到相应的商品",Constants.SYSTEM_PROMPT);
                    }                   
                }
                catch (Exception ex)
                {
                    waitForm.Close();
                    XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show("请输入条形码", Constants.SYSTEM_PROMPT);
            }
        }

        #region        

        /// <summary>
        /// 根据选择的是税票还是收据隐藏和显示税率，税额，不含税金额等
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemRadioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup radioGroup = (RadioGroup)sender;
            inoutHelper.RadioGroupSelectIndexChange(radioGroup, gcTax, gcTaxFee, gcTotalMoney, gcFeeNotContainsTax,"销售金额");
        }

        /// <summary>
        /// 时间变化导致天数变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateEditIncomeTime_DateTimeChanged(object sender, EventArgs e)
        {
            inoutHelper.CalculateIncomeDays(dateEditIncomeTime, textEditPayTerm);
        }

        /// <summary>
        /// 天数变化导致时间变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEditPayTerm_TextChanged(object sender, EventArgs e)
        {
            inoutHelper.CalculateInComeTime(dateEditIncomeTime, textEditPayTerm);
        }
        
        #endregion               

        #region 保存金额信息
        public void GetTaxAndFee()
        {
            //应付应收款
            textEditNeedToPay.EditValue = inoutHelper.CalcTaxTotalFee(gvProductSJ);
            //合计金额
            textEditTotalFee.EditValue = inoutHelper.CalcTaxTotalFee(gvProductSJ);
            //税额
            textEditAmountTax.EditValue = inoutHelper.CalcAmountTax(gvProductSJ);
            //本次收支款
            textEditPayThisTime.EditValue = 0.0;
            //折扣金额
            textEditDiscountFee.EditValue = 0.0;
        }

        /// <summary>
        /// 改变本次收款金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEditPayThisTime_TextChanged(object sender, EventArgs e)
        {
            inoutHelper.PayThisTimeChange(textEditTotalFee, textEditNeedToPay, textEditDiscountFee, textEditPayThisTime);
        }

        /// <summary>
        /// 改变折扣金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEditDiscountFee_TextChanged(object sender, EventArgs e)
        {
            inoutHelper.DisFeeChange(textEditTotalFee, textEditNeedToPay, textEditDiscountFee, textEditPayThisTime);
        }
        #endregion

        /// <summary>
        /// 响应条形码回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemTextEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextEdit textEdit = (TextEdit)sender;
                if (!string.IsNullOrEmpty(textEdit.Text))
                {
                    WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                    waitForm.Show();
                    try
                    {
                        string skuOuterID = textEdit.Text;
                        List<View_StockItemProduct> vsipList = View_StockItemProductService.GetView_StockItemProduct(v => v.SkuOuterID == skuOuterID);
                        if (vsipList.Count > 0)
                        {
                            View_StockItemProduct vsip = vsipList.First();
                            DataRow dRow = dTable.NewRow();
                            int rowCount = gvProductSJ.RowCount;
                            for (int i = 0; i < rowCount; i++)
                            {
                                DataRow row = gvProductSJ.GetDataRow(i);
                                if (row["SkuOuterID"].ToString() == vsip.SkuOuterID)
                                {
                                    return;
                                }
                            }
                            dRow["CatName"] = vsip.CatName;
                            dRow["ProductName"] = vsip.Name;
                            dRow["SkuOuterID"] = vsip.SkuOuterID;
                            dRow["StockCatName"] = vsip.StockCatName;
                            dRow["OuterID"] = vsip.OuterID;
                            dRow["SaleProps"] = vsip.SkuProps_Str;
                            dRow["TotalMoney"] = vsip.SkuPrice;
                            dTable.Rows.Add(dRow);
                            waitForm.Close();
                            gvProductSJ.BestFitColumns();
                        }
                        else
                        {
                            waitForm.Close();
                            XtraMessageBox.Show("找不到相应的商品", Constants.SYSTEM_PROMPT);
                        }
                    }
                    catch (Exception ex)
                    {
                        waitForm.Close();
                        XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    XtraMessageBox.Show("请输入条形码", Constants.SYSTEM_PROMPT);
                }
            }
        }
    }
}
