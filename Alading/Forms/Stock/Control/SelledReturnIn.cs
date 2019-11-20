using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API;
using Alading.Business;
using Alading.Core.Enum;
using System.Collections;
using Alading.Entity;
using DevExpress.Utils;
using Alading.Taobao;
using Alading.Utils;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;

namespace Alading.Forms.Stock.Control
{
    [ToolboxItem(false)]
    public partial class SelledReturnIn : DevExpress.XtraEditors.XtraUserControl
    {
        public SelledReturnIn()
        {
            InitializeComponent();
        }

        #region 全局变量
        public bool isTBRefund = true;

        InOutHelper inoutHelper;

        DataTable dTable;

        List<string> skuOuterIDList;
        #endregion

        #region 点击控件触发事件

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelledReturnIn_Load(object sender, EventArgs e)
        {
            try
            {
                inoutHelper = new InOutHelper();
                dTable = new DataTable();
                skuOuterIDList = new List<string>();

                //显示当前入库日期
                dateEditInTime.Text = DateTime.Now.ToShortDateString();
                //加载展示信息
                AddColumns(dTable);
                //从数据库获取并展示数据
                List<Alading.Entity.View_RefundTradeStock> tradeRefundList = TradeRefundService.GetTradeRefundByView(c => c.LocalStatus == LocalTradeStatus.SentNotRate
                    && c.IsRecieved == false);
                
                LoadTradeRefund(tradeRefundList);

                
                /*加载所有仓库*/
                inoutHelper.LoadAllHouse(repositoryItemComboBoxStockHouse);
                gvReturnInProduct.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 改变焦点行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvReturnInProduct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                inoutHelper.LoadLayoutAndProps(repositoryItemComboBoxStockLayout, gvReturnInProduct
                    , categoryKeyProps, categorySaleProps, categoryNotKeyProps, categoryInputProps);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击更新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnDownLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                #region 获取全部淘宝店铺的淘宝会员号
                List<Alading.Entity.Shop> shopBList = ShopService.GetShop(c => c.ShopType == (int)ShopType.TaobaoBShop);
                List<Alading.Entity.Shop> shopCList = ShopService.GetShop(c => c.ShopType == (int)ShopType.TaobaoCShop);
                List<string> sessionKeyList = new List<string>();

                foreach (Alading.Entity.Shop shop in shopBList)
                {
                    sessionKeyList.Add(Alading.Utils.SystemHelper.GetSessionKey(shop.nick));
                }
                foreach (Alading.Entity.Shop shop in shopCList)
                {
                    sessionKeyList.Add(Alading.Utils.SystemHelper.GetSessionKey(shop.nick));
                }
                #endregion

                foreach (string sessionKey in sessionKeyList)//获取全部淘宝店铺的数据
                {
                    /*获取未评价数据*/
                    int totalResults = 0;
                    int pageSize = 100;
                    int totalPageNo = 0;

                    /*用于存放从淘宝上获取回的数据*/
                    List<Alading.Entity.TradeRefund> tradeRefundList = new List<Alading.Entity.TradeRefund>();
                    //存放退货单编号
                    List<string> refundIDList = new List<string>();

                    GetNewTradeRefund(sessionKey, 1, pageSize, out totalResults, tradeRefundList, refundIDList);

                    if (totalResults % totalResults == 0)//能除尽
                    {
                        totalPageNo = totalResults / totalResults;
                    }
                    else//不能除尽
                    {
                        totalPageNo = totalResults / totalResults + 1;
                    }

                    /*超过两页的循环调取数据*/
                    for (int pageno = 2; pageno <= totalPageNo; pageno++)
                    {
                        GetNewTradeRefund(sessionKey, pageno, pageSize, out totalResults, tradeRefundList, refundIDList);
                    }

                    //以消除数据库中已存在的数据
                    GetNewRefundDetail(sessionKey, tradeRefundList, refundIDList);
                }

                //从数据库获取并展示数据
                List<Alading.Entity.View_RefundTradeStock> refundList = TradeRefundService.GetTradeRefundByView(c => c.LocalStatus == LocalTradeStatus.SentNotRate
                    && c.IsRecieved == false);

                LoadTradeRefund(refundList);
                waitFrm.Close();
                XtraMessageBox.Show("从淘宝网获取数据成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                //从数据库获取并展示数据
                List<Alading.Entity.View_RefundTradeStock> tradeRefundList = TradeRefundService.GetTradeRefundByView(c => c.LocalStatus == LocalTradeStatus.SentNotRate
                       && c.IsRecieved == false);

                LoadTradeRefund(tradeRefundList);
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 选择不同的仓库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemComboBoxStockHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBoxEdit combo = sender as ComboBoxEdit;
                gvReturnInProduct.BeginUpdate();
                DataRow row = gvReturnInProduct.GetFocusedDataRow();
                int rowHandle = gvReturnInProduct.FocusedRowHandle;
                row[gcStockHouse.FieldName] = combo.Properties.Items[combo.SelectedIndex];
                Hashtable table = repositoryItemComboBoxStockHouse.Tag as Hashtable;
                row["HouseCode"] = table[combo.SelectedIndex];
                gvReturnInProduct.BestFitColumns();
                gvReturnInProduct.EndUpdate();

                //加载库位
                inoutHelper.LoadLayout(repositoryItemComboBoxStockLayout, table[combo.SelectedIndex].ToString());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 选择不同的库位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemComboBoxStockLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBoxEdit combo = sender as ComboBoxEdit;
                combo.Text = combo.Properties.Items[combo.SelectedIndex].ToString();
                Hashtable table = repositoryItemComboBoxStockLayout.Tag as Hashtable;
                DataRow row = gvReturnInProduct.GetFocusedDataRow();
                row[gcStockLayout.FieldName] = combo.Properties.Items[combo.SelectedIndex];
                row["LayoutCode"] = table[combo.SelectedIndex];
                gvReturnInProduct.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 加载业务员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pceOperator_Popup(object sender, EventArgs e)
        {
            try
            {
                inoutHelper.GetOperator(tlOperator);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 选择业务员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlOperator_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                TreeListHitInfo hitInfo = tlOperator.CalcHitInfo(new Point(e.X, e.Y));
                //如果单击到单元格内
                if (hitInfo.HitInfoType == HitInfoType.Cell)
                {
                    TreeListNode clickedNode = hitInfo.Node;
                    if (clickedNode != null && !clickedNode.HasChildren)
                    {
                        pceOperator.Text = clickedNode.GetDisplayText(0);
                        pceOperator.Tag = clickedNode.Tag;
                        pceOperator.ClosePopup();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 方法
        public void AddColumns(DataTable selledReturnInTable)
        {
            selledReturnInTable.Columns.Add("Select", typeof(bool));//选择
            selledReturnInTable.Columns.Add("title");//商品名称  
            selledReturnInTable.Columns.Add("buyer_nick");//买家会员号  
            selledReturnInTable.Columns.Add("seller_nick");//卖家会员号
            selledReturnInTable.Columns.Add("price", typeof(double));//商品价格
            selledReturnInTable.Columns.Add("num");//商品数量
            selledReturnInTable.Columns.Add("refund_fee", typeof(double));//退还金额
            selledReturnInTable.Columns.Add("payment", typeof(double));//总金额
            selledReturnInTable.Columns.Add("reason");//退货原因
            selledReturnInTable.Columns.Add("HouseName");//仓库  
            selledReturnInTable.Columns.Add("LayoutName");//库位  
            selledReturnInTable.Columns.Add("outer_sku_id");
            selledReturnInTable.Columns.Add("OuterID");
            selledReturnInTable.Columns.Add("SkuOuterID");
            selledReturnInTable.Columns.Add("HouseCode");//仓库编码
            selledReturnInTable.Columns.Add("LayoutCode");//库位编码
            selledReturnInTable.Columns.Add("refund_id");//退货单号
            selledReturnInTable.Columns.Add("oid");//退货单号
            selledReturnInTable.Columns.Add("Count");
            selledReturnInTable.Columns.Add("IsRecieved");//是否已入库

            gcReturnInProduct.DataSource = selledReturnInTable.DefaultView;
        }

        /// <summary>
        /// 从淘宝获取最新数据
        /// </summary>
        /// <param name="pageno"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalResults"></param>
        /// <param name="tradeRefundList"></param>
        public void GetNewTradeRefund(string session, int pageno, int pageSize, out int totalResults
            , List<Alading.Entity.TradeRefund> tradeRefundList, List<string> refundIDList)
        {
            TradeReq tradeReq = new TradeReq();
            tradeReq.PageNo = pageno;//页数
            tradeReq.PageSize = pageSize;//每页的数量
            string fields = "refund_id,order_status,modified";//API参数

            TradeRsp tradeRsp = TopService.RefundsReceiveGet(session, fields, tradeReq);

            /*未获取到数据*/
            if (tradeRsp == null || tradeRsp.TotalResults == 0)
            {
                totalResults = 0;
                return;
            }

            totalResults = tradeRsp.TotalResults;
            foreach (Alading.Taobao.Entity.Refund refund in tradeRsp.Refunds.Refund)
            {
                Alading.Entity.TradeRefund newTradeRefund = new Alading.Entity.TradeRefund();
                newTradeRefund.refund_id = refund.Rid;//退货单号
                newTradeRefund.order_status = refund.OrderStatus == null ? string.Empty : refund.OrderStatus;//退款对应的订单交易状态
                newTradeRefund.modified = DateTime.Parse(refund.Modified);//对象修改时间
                tradeRefundList.Add(newTradeRefund);
                refundIDList.Add(refund.Rid);
            }
        }

        /// <summary>
        /// 获取退货单的详细信息并保存到数据库
        /// </summary>
        /// <param name="tradeRefundList"></param>
        /// <param name="refundIDList"></param>
        public void GetNewRefundDetail(string session, List<Alading.Entity.TradeRefund> tradeRefundList, List<string> refundIDList)
        {
            //存放数据库中不存在的退货详细信息
            List<Alading.Entity.TradeRefund> newRefundList = new List<Alading.Entity.TradeRefund>();

            List<Alading.Entity.TradeRefund> oldRefundList = TradeRefundService.GetTradeRefund(refundIDList);

            foreach (Alading.Entity.TradeRefund tradeRefund in tradeRefundList)
            {
                Alading.Entity.TradeRefund oldRefund = oldRefundList.Find(c => c.refund_id == tradeRefund.refund_id);
                if (oldRefund != null)//数据库中已存在此数据
                {
                    continue;
                }
                else//不存在则取淘宝网获取
                {
                    TradeRsp tradeRsp = TopService.RefundGet(session, tradeRefund.refund_id);

                    /*未获取到数据*/
                    if (tradeRsp == null)
                    {
                        continue;
                    }

                    oldRefund = new TradeRefund();
                    oldRefund.modified = tradeRefund.modified;
                    oldRefund.order_status = tradeRefund.order_status;
                    TradeRefundCopyData(oldRefund, tradeRsp.Refund);
                    newRefundList.Add(oldRefund);
                }
            }
            TradeRefundService.AddTradeRefund(newRefundList);
        }

        /// <summary>
        /// 将获取回来的数据传递给数据库表
        /// </summary>
        /// <param name="desObj"></param>
        /// <param name="srcObj"></param>
        public void TradeRefundCopyData(Alading.Entity.TradeRefund desObj, Alading.Taobao.Entity.Refund srcObj)
        {
            desObj.refund_id = srcObj.Rid;//退款单号
            desObj.tid = srcObj.Tid;//淘宝交易单号
            desObj.oid = srcObj.Oid;//子订单号。如果是单笔交易oid会等于tid
            desObj.alipay_no = srcObj.AlipayNo;//支付宝交易号
            desObj.total_fee = srcObj.TotalFee;//交易总金额
            desObj.buyer_nick = srcObj.BuyerNick;//买家昵称
            desObj.seller_nick = srcObj.SellerNick;//卖家昵称
            desObj.created = DateTime.Parse(srcObj.Created);//退款申请时间
            desObj.status = srcObj.RefundStatus;//退款状态
            desObj.good_status = srcObj.GoodStatus;//货物状态
            desObj.has_good_return = srcObj.HasGoodReturn;//买家是否需要退货。可选值:true(是),false(否)
            desObj.refund_fee = srcObj.RefundFee;//退还金额(退还给买家的金额)。
            desObj.payment = srcObj.Payment;//支付给卖家的金额(交易总金额-退还给买家的金额)。
            desObj.reason = srcObj.Reason;//退款原因
            desObj.desc = srcObj.Description;//退款说明 
            desObj.iid = srcObj.Iid;//申请退款的商品字符串编号
            desObj.title = srcObj.ItemTitle;//商品标题
            desObj.price = srcObj.ItemPrice;//商品价格
            desObj.num = srcObj.ItemNum;//商品购买数量

            if (srcObj.GoodReturnTime == null)
            {
                desObj.good_return_time = System.DateTime.MinValue;
            }
            else
            {
                desObj.good_return_time = DateTime.Parse(srcObj.GoodReturnTime);
            }

            desObj.company_name = srcObj.LogisticsCompany == null ? string.Empty : srcObj.LogisticsCompany;//物流公司名称
            desObj.sid = srcObj.Sid == null ? string.Empty : srcObj.Sid;//退货运单号
            desObj.address = srcObj.SellerAddress == null ? string.Empty : srcObj.SellerAddress;//卖家收货地址
            desObj.shipping_type = srcObj.LogisticsType == null ? string.Empty : srcObj.LogisticsType;//物流方式

            StringBuilder sbTimeOut = new StringBuilder();
            if (srcObj.Timeout == null)
            {
                desObj.refund_remind_timeout = string.Empty;
            }
            else
            {
                sbTimeOut.Append(srcObj.Timeout.RemindType);
                sbTimeOut.Append(";");
                sbTimeOut.Append(srcObj.Timeout.HasTimeout);
                sbTimeOut.Append(";");
                sbTimeOut.Append(srcObj.Timeout.TimeoutTime);
                desObj.refund_remind_timeout = sbTimeOut.ToString();//退款超时结构RefundRemindTimeout
            }

            desObj.LocalPrivyC = string.Empty;
            desObj.IsRecieved = false;
        }

        /// <summary>
        /// 加载TradeRefund数据
        /// </summary>
        /// <param name="tradeRefundList"></param>
        public void LoadTradeRefund(List<Alading.Entity.View_RefundTradeStock> tradeRefundList)
        {
            //gcReturnInProduct.DataSource = null;
            if (tradeRefundList == null || tradeRefundList.Count == 0)
            {
                return;
            }

            foreach (Alading.Entity.View_RefundTradeStock tradeRefund in tradeRefundList)
            {
                DataRow row = dTable.NewRow();
                row["Select"] = false;
                row["title"] = tradeRefund.title;
                row["buyer_nick"] = tradeRefund.buyer_nick;
                row["seller_nick"] = tradeRefund.seller_nick;
                row["num"] = tradeRefund.num;

                if (!string.IsNullOrEmpty(tradeRefund.price))
                {
                    row["price"] = double.Parse(tradeRefund.price);
                }
                if (!string.IsNullOrEmpty(tradeRefund.refund_fee))
                {
                    row["refund_fee"] = double.Parse(tradeRefund.refund_fee);
                }
                if (!string.IsNullOrEmpty(tradeRefund.payment))
                {
                    row["payment"] = double.Parse(tradeRefund.payment);
                }

                row["reason"] = tradeRefund.reason;
                row["outer_sku_id"] = tradeRefund.outer_sku_id;
                row["OuterID"] = tradeRefund.OuterID;
                row["HouseName"] = tradeRefund.HouseName == null ? string.Empty : tradeRefund.HouseName;
                row["LayoutName"] = tradeRefund.LayoutName == null ? string.Empty : tradeRefund.LayoutName;
                row["HouseCode"] = tradeRefund.HouseCode == null ? string.Empty : tradeRefund.HouseCode;
                row["LayoutCode"] = tradeRefund.LayoutCode == null ? string.Empty : tradeRefund.LayoutCode;
                row["refund_id"] = tradeRefund.refund_id;
                row["oid"] = tradeRefund.oid;
                row["IsRecieved"] = false ? "是" : "否";
                row["Count"] = tradeRefund.num;
                dTable.Rows.Add(row);
            }
            //gcReturnInProduct.MainView = gvReturnInProduct;
            //显示总金额
            textEditTotalFee.EditValue = 0.0;
            gvReturnInProduct.BestFitColumns();
            //barBtnRefresh.Enabled = true;
            //barBtnDownLoad.Enabled = true;
            //isTBRefund = true;
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        public bool SaveRefund()
        {
            //出入库时间
            if (string.IsNullOrEmpty(dateEditInTime.Text))
            {
                XtraMessageBox.Show("请填写入库日期", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateEditInTime.Focus();
                return false;
            }

            //业务员及编号
            if (string.IsNullOrEmpty(pceOperator.Text))
            {
                XtraMessageBox.Show("请填写业务员", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pceOperator.Focus();
                return false;
            }

            //用于存放需要退货入库的订单的退款单号
            List<string> refundIDList = new List<string>();

            //用于存放oid,以通过View_RefundTradeStock获取sku_outerID
            List<string> oidList = new List<string>();
            //存放修要修改库存的sku_outer_id
            List<string> outerSkuIdList = new List<string>();
            //存放修要修改库存的outer_id
            List<string> outerIdList = new List<string>();

            //用于存放需要更新的StockProduct数据
            List<StockProduct> stockProductList = new List<StockProduct>();
            //用于存放要保存到数据库的StockHouseProduct
            List<Alading.Entity.StockHouseProduct> stockHouseProList = new List<StockHouseProduct>();
            //存放StockDetail
            List<Alading.Entity.StockDetail> stockDetailList = new List<Alading.Entity.StockDetail>();

            //入库单编号
            string inOutCode = string.Empty;
            if (!string.IsNullOrEmpty(textEditInOutCode.Text))
            {
                inOutCode = textEditInOutCode.Text.Trim();
                if (inoutHelper.ExistInOutCode(inOutCode))
                {
                    XtraMessageBox.Show("此单号已存在", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textEditInOutCode.Focus();
                    return false;
                }
            }
            else
            {
                inOutCode = Guid.NewGuid().ToString();
            }

            for (int index = 0; index < gvReturnInProduct.RowCount; index++)
            {
                DataRow dataRow = gvReturnInProduct.GetDataRow(index);
                if (dataRow["Select"].ToString() == true.ToString())
                {
                    if (dataRow["HouseName"] == null || string.IsNullOrEmpty(dataRow["HouseName"].ToString()))
                    {
                        XtraMessageBox.Show("需要选择仓库", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }

                    if (dataRow["LayoutName"] == null || string.IsNullOrEmpty(dataRow["LayoutName"].ToString()))
                    {
                        XtraMessageBox.Show("需要选择库位", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }

                    refundIDList.Add(dataRow["refund_id"].ToString());
                    oidList.Add(dataRow["oid"].ToString());

                    #region 保存StockProduct信息
                    outerSkuIdList.Add(dataRow["outer_sku_id"].ToString());
                    outerIdList.Add(dataRow["OuterID"].ToString());

                    StockProduct newStockPro = new StockProduct();
                    newStockPro.SkuOuterID = dataRow["outer_sku_id"].ToString();
                    newStockPro.OuterID = dataRow["OuterID"].ToString();
                    //入库数量
                    if (string.IsNullOrEmpty(dataRow["Count"].ToString()))
                    {
                        newStockPro.SkuQuantity = 0;
                    }
                    else
                    {
                        newStockPro.SkuQuantity = int.Parse(dataRow["Count"].ToString());
                    }

                    stockProductList.Add(newStockPro);
                    #endregion

                    #region 添加或更新StockHouseProduct
                    StockHouseProduct stockHousePro = new StockHouseProduct();
                    stockHousePro.HouseCode = dataRow["HouseCode"] == null ? string.Empty : dataRow["HouseCode"].ToString();
                    stockHousePro.LayoutCode = dataRow["LayoutCode"] == null ? string.Empty : dataRow["LayoutCode"].ToString();
                    stockHousePro.SkuOuterID = newStockPro.SkuOuterID;
                    stockHousePro.Num = newStockPro.SkuQuantity;
                    stockHousePro.HouseName = dataRow["HouseName"].ToString();
                    stockHousePro.LayoutName = dataRow["LayoutName"].ToString();
                    stockHouseProList.Add(stockHousePro);
                    #endregion

                    #region 保存StockDetail
                    Alading.Entity.StockDetail stockDetail = new StockDetail();
                    stockDetail.StockDetailCode = Guid.NewGuid().ToString();
                    stockDetail.ProductSkuOuterId = dataRow["outer_sku_id"].ToString();
                    stockDetail.InOutCode = inOutCode;
                    //仓库编号
                    if (dataRow["HouseCode"] == null)
                    {
                        stockDetail.StockHouseCode = string.Empty;
                    }
                    else
                    {
                        stockDetail.StockHouseCode = dataRow["HouseCode"].ToString();
                    }
                    //库位编号
                    if (dataRow["LayoutCode"] == null)
                    {
                        stockDetail.StockLayOutCode = string.Empty;
                    }
                    else
                    {
                        stockDetail.StockLayOutCode = dataRow["LayoutCode"].ToString();
                    }
                    //商品价格price
                    stockDetail.Price = float.Parse(dataRow["price"].ToString());
                    stockDetail.Quantity = int.Parse(dataRow["Count"].ToString());
                    stockDetail.DetailType = (int)InOutType.SelledReturnIn;
                    stockDetail.DetailRemark = string.Empty;
                    stockDetail.Tax = string.Empty;
                    stockDetail.TotalFee = dataRow["payment"]==null?0:float.Parse(dataRow["payment"].ToString());
                    stockDetail.DurabilityDate = DateTime.Parse(DateTime.Now.ToShortDateString());

                    stockDetailList.Add(stockDetail);
                    #endregion
                }
            }

            if (refundIDList.Count == 0)
            {
                XtraMessageBox.Show("请先勾选数据", "系统提示", MessageBoxButtons.OK
                    , MessageBoxIcon.Information);
                return false;
            }

            /*销售退货时间，仓库，库位要求必须填写*/
            #region 将要更新的数据保存到oldRefundList
            List<Alading.Entity.TradeRefund> oldRefundList = TradeRefundService.GetTradeRefund(refundIDList);

            //用于保存等待更新到数据库表TradeRefund的数据
            List<Alading.Entity.TradeRefund> refundUpdateList = new List<Alading.Entity.TradeRefund>();

            //修改StockRefund中的IsRecieved 和 LocalPrivyC的状态
            foreach (string refundId in refundIDList)
            {
                Alading.Entity.TradeRefund refund = new TradeRefund();
                refund.refund_id = refundId;
                //记录当前库管是否收到从买家发回的货物
                refund.IsRecieved = true;
                //经办人
                refund.LocalPrivyC = pceOperator.Text;
                refundUpdateList.Add(refund);
            }
            #endregion

            #region 更新StockInOut
            string InOutTime = dateEditInTime.Text;
            //用于存放StockInOut
            List<Alading.Entity.StockInOut> stockInOutList = new List<Alading.Entity.StockInOut>();
            //添加到StockInOut
            Alading.Entity.StockInOut stockInOut = new Alading.Entity.StockInOut();
            stockInOut.InOutCode = inOutCode;//进出库单编号
            stockInOut.InOutTime = DateTime.Parse(InOutTime);//进出库时间
            stockInOut.OperatorName = pceOperator.Text == null ? string.Empty : pceOperator.Text;
            stockInOut.OperatorCode = pceOperator.Tag.ToString();
            stockInOut.InOutType = (int)InOutType.SelledReturnIn;
            //oid赋值
            stockInOut.TradeOrderCode = string.Empty; ;
            stockInOut.DiscountFee = 0;
            //保存应付应收金额
            if (string.IsNullOrEmpty(textEditTotalFee.EditValue.ToString()))
            {
                stockInOut.DueFee = 0;
            }
            else
            {
                stockInOut.DueFee = float.Parse(textEditTotalFee.EditValue.ToString());
            }

            stockInOut.InOutStatus = (int)InOutStatus.AllRefund;
            stockInOut.IsSettled = true;
            stockInOut.PayType = 0;//付款方式
            stockInOut.PayThisTime = string.IsNullOrEmpty(textEditTotalFee.EditValue.ToString()) ? 0 : float.Parse(textEditTotalFee.EditValue.ToString());
            stockInOut.PayTerm = 0;
            stockInOut.IncomeTime = DateTime.Parse(DateTime.Now.ToShortDateString());;
            stockInOut.AmountTax = 0;
            stockInOut.FreightCompany = string.Empty;
            stockInOut.FreightCode = string.Empty;

            stockInOutList.Add(stockInOut);
            #endregion

            #region 添加PayCharge
            PayCharge payCharge = new PayCharge();
            payCharge.PayChargeCode = Guid.NewGuid().ToString();
            payCharge.PayChargeType = 0;
            payCharge.InOutCode = inOutCode;
            payCharge.PayerCode = string.Empty;
            payCharge.PayerName = string.Empty;
            payCharge.ChargerCode = string.Empty;
            payCharge.ChargerName = string.Empty;
            payCharge.OperateTime = DateTime.Parse(dateEditInTime.Text);
            payCharge.OperatorCode = pceOperator.Tag.ToString();
            payCharge.OperatorName = pceOperator.Text;
            payCharge.PayChargeRemark = string.Empty;
            if (!string.IsNullOrEmpty(textEditTotalFee.Text))
            {
                payCharge.TotalFee = double.Parse(textEditTotalFee.EditValue.ToString());
            }
            else
            {
                payCharge.TotalFee = 0.0;
            }
            payCharge.NeedToPay = 0.0;
            payCharge.AmountTax = 0.0;
            payCharge.PayThisTime = payCharge.TotalFee;
            payCharge.DiscountFee = 0.0;
            payCharge.IncomeDay = 0;
            payCharge.IncomeTime = DateTime.MinValue;
            #endregion

            //更新Refund StockProduct StockItem StockInOut StockDetail
            TradeRefundService.UpdateTradeRefund(refundUpdateList, stockProductList, stockHouseProList, stockInOutList, stockDetailList
               , payCharge, refundIDList, outerSkuIdList, outerIdList);
            return true;
        }

        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            gcSelledReturnInBill.DataSource = null;

            textEditTotalFee.Text = string.Empty;
        }
        #endregion

        #region 退货单
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupContainerSelledReturn_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                inoutHelper.ClickPopup(barEditSRPageNo, barBtnFirstPage, barBtnForwardPage, barBtnNextPage, barBtnLastPage, barBtnSkipPage
                    , gcSelledReturnInBill, gvSelledReturnInBill, (int)InOutType.SelledReturnIn);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnFirstPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadFirstPage(barEditSRPageNo, barBtnFirstPage, barBtnForwardPage, barBtnNextPage, barBtnLastPage, barBtnSkipPage
                    , gcSelledReturnInBill, gvSelledReturnInBill, (int)InOutType.SelledReturnIn);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnForwardPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadForwardPage(barEditSRPageNo, barBtnFirstPage, barBtnForwardPage, barBtnNextPage, barBtnLastPage, barBtnSkipPage
                    , gcSelledReturnInBill, gvSelledReturnInBill, (int)InOutType.SelledReturnIn);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnNextPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadNextPage(barEditSRPageNo, barBtnFirstPage, barBtnForwardPage, barBtnNextPage, barBtnLastPage, barBtnSkipPage
                    , gcSelledReturnInBill, gvSelledReturnInBill, (int)InOutType.SelledReturnIn);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnLastPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadLastPage(barEditSRPageNo, barBtnFirstPage, barBtnForwardPage, barBtnNextPage, barBtnLastPage, barBtnSkipPage
                     , gcSelledReturnInBill, gvSelledReturnInBill, (int)InOutType.SelledReturnIn);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnSkipPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadLastPage(barEditSRPageNo, barBtnFirstPage, barBtnForwardPage, barBtnNextPage, barBtnLastPage, barBtnSkipPage
                    , gcSelledReturnInBill, gvSelledReturnInBill, (int)InOutType.SelledReturnIn);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 计算总金额
        private void repositoryItemCheckSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckEdit select = (CheckEdit)sender;
                DataRow row = gvReturnInProduct.GetFocusedDataRow();
                if (row != null && !string.IsNullOrEmpty(row["refund_fee"].ToString()))
                {
                    //显示总金额
                    double TotalFee = double.Parse(textEditTotalFee.EditValue.ToString());
                    if (select.Checked)
                    {
                        TotalFee += double.Parse(row["refund_fee"].ToString());
                    }
                    else
                    {
                        TotalFee -= double.Parse(row["refund_fee"].ToString());
                    }
                    textEditTotalFee.EditValue = TotalFee;
                }
                gvReturnInProduct.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}
