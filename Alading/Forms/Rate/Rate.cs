using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Alading.Business;
using Alading.Core.Enum;
using Alading.Entity;
using Alading.Taobao.API;
using Alading.Taobao.Entity.Extend;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Alading.Taobao;
using Alading.Utils;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using System.Configuration;
using DevExpress.XtraNavBar;

namespace Alading.Forms.Rate.Rate
{
    public partial class Rate : DevExpress.XtraEditors.XtraForm
    {
        #region 定义全局变量
        System.Threading.Timer timer = null;

        int pageSize = int.Parse(ConfigurationManager.AppSettings["StockItemPageSize"]);

        //存放查询量
        bool isQuery = false;
        Hashtable queryHash = new Hashtable(5);
        int xtraTabPage = 0;
        string nick = string.Empty;
        int rowCount = 0;
        #endregion

        public Rate()
        {
            InitializeComponent();
        }

        #region 共用界面
        /// <summary>
        /// 加载界面时显示数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TradeRate_Load(object sender, EventArgs e)
        {
            try
            {
                LoadShopList();

                navBarGroupShopList.SelectedLinkIndex = 0;

                //未评价
                RefreshNotRate();
                //客户差评
                RefreshRated(gridCtrlCBadRated, barEditCSkip, repositoryCComboBoxPage, "buyer", 1
                    , barBtnCFirstPage, barBtnCForwardPage, barBtnCNextPage, barBtnCLastPage);
                //我的差评
                RefreshRated(gridCtrlgMBadRated, barEditMSkip, repositoryMComboBoxPage, "seller", 1
                    , barBtnMFirstPage, barBtnMForwardPage, barBtnMNextPage, barBtnMLastPage);
                //其他评价
                RefreshRated(gridCtrlOtherRated, barEditOSkip, repositoryOComboBoxPage, string.Empty, 1
                    , barBtnOFirstPage, barBtnOForwardPage, barBtnONextPage, barBtnOLastPage);
                //所有订单
                RefreshRated(gridCtrlAllTrade, barEditAllSkip, repositoryAllComboBoxPage, string.Empty, 1
                    , barBtnAFirstPage, barBtnAForwardPage, barBtnANextPage, barBtnALastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击查询按钮，进行混淆查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBtn_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitFrm = null;
            try
            {
                if (string.IsNullOrEmpty(tEKeyWord.Text))
                {
                    XtraMessageBox.Show("请先输入关键字", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);

                    //根据关键字，进行模糊查询
                    QueryCheck();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击《《选为评价内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectContentBtn_Click(object sender, EventArgs e)
        {
            try
            {
                memoEditContent.Text = contentListCtrl.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击>>按钮，保存评价内容到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveContentBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //首先判断是否有内容
                if (string.IsNullOrEmpty(memoEditContent.Text))
                {
                    XtraMessageBox.Show("请输入评价内容", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    memoEditContent.Focus();
                    return;
                }
                else
                {
                    //判断是否已经存在数据库
                    foreach (string content in contentListCtrl.Items)
                    {
                        //如果已存在，就不添加
                        if (memoEditContent.Text.Trim() == content)
                        {
                            XtraMessageBox.Show("此评价内容已存在，请重新输入", Constants.SYSTEM_PROMPT
                                , MessageBoxButtons.OK, MessageBoxIcon.Information);

                            memoEditContent.Focus();
                            return;
                        }
                    }

                    /*添加评价内容到数据库表RateContent*/
                    RateContent rateContent = new RateContent();

                    if (radioGroupRate.SelectedIndex == 0)
                    {
                        rateContent.Result = "good";//好评
                    }
                    else if (radioGroupRate.SelectedIndex == 1)
                    {
                        rateContent.Result = "neutral";//中评
                    }
                    else if (radioGroupRate.SelectedIndex == 2)
                    {
                        rateContent.Result = "bad";//差评
                    }

                    rateContent.TradeRateContent = memoEditContent.Text.Trim();//去除空格
                    RateContentService.AddRateContent(rateContent);
                    contentListCtrl.Items.Add(memoEditContent.Text);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击删除评价内容按钮，从本地删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteCtntBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //先判断是否选中
                if (string.IsNullOrEmpty(contentListCtrl.SelectedItem.ToString()))
                {
                    XtraMessageBox.Show("没有可删除的内容", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (DialogResult.Yes
                    == XtraMessageBox.Show("是否确定删除", Constants.SYSTEM_PROMPT, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    //删除数据库表RateContent评价内容
                    RateContent rateContent = new RateContent();
                    rateContent.TradeRateContent = contentListCtrl.SelectedItem.ToString();
                    rateContent.Result = radioGroupRate.EditValue.ToString();
                    RateContentService.RemoveRateContent(rateContent);

                    //删除界面的数据
                    contentListCtrl.Items.Remove(rateContent.TradeRateContent);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 双击评价内容列表，选择评价内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contentListCtrl_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                memoEditContent.Text = contentListCtrl.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击启动自动评价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBtnAutoRate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBtnAutoRate.Text == "启动自动评价")
                {
                    timer = new System.Threading.Timer(new System.Threading.TimerCallback(AutoRate), null, 10000, 900000);
                }
                else if (checkBtnAutoRate.Text == "关闭自动评价" && timer != null)
                {
                    timer.Dispose();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 根据不同的评价结果，加载不同的评价内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroupRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string result = string.Empty;
                contentListCtrl.Items.Clear();//清空评价内容

                if (radioGroupRate.SelectedIndex == 0)
                {
                    result = "good";//好评
                }
                else if (radioGroupRate.SelectedIndex == 1)
                {
                    result = "neutral";//中评
                }
                else if (radioGroupRate.SelectedIndex == 2)
                {
                    result = "bad";//差评
                }

                List<RateContent> rateCtntList = RateContentService.GetRateContent(result);

                foreach (RateContent rateContent in rateCtntList)
                {
                    contentListCtrl.Items.Add(rateContent.TradeRateContent);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击评价按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //用于存放选中的评价成功的交易
                List<Alading.Entity.Trade> ratedList = new List<Alading.Entity.Trade>();

                #region 获取选中的需要评价的交易
                //用于存放选中的等待评价的交易
                List<TopTradeRate> topTradeRateList = new List<TopTradeRate>();
                //存放各个店铺的session 键为nick,值为sessionkey
                Hashtable sessionHash = new Hashtable();

                for (int index = 0; index < gViewRating.RowCount; index++)
                {
                    DataRow dataRow = gViewRating.GetDataRow(index);
                    if (dataRow["IsSelected"].ToString() == true.ToString())
                    {
                        TopTradeRate topTradeRate = new TopTradeRate();
                        topTradeRate.tid = dataRow["tid"].ToString();
                        topTradeRate.content = memoEditContent.Text;
                        topTradeRate.CustomTid = dataRow["CustomTid"].ToString();
                        //获取sessionkey
                        if (dataRow["seller_nick"] != null && !string.IsNullOrEmpty(dataRow["seller_nick"].ToString()))
                        {
                            topTradeRate.sessionkey = SystemHelper.GetSessionKey(dataRow["seller_nick"].ToString());
                        }
                        else
                        {
                            topTradeRate.sessionkey = string.Empty;
                        }

                        //评价结果
                        if (radioGroupRate.SelectedIndex == 0)
                        {
                            topTradeRate.result = "good";//好评
                        }
                        else if (radioGroupRate.SelectedIndex == 1)
                        {
                            topTradeRate.result = "neutral";//中评
                        }
                        else if (radioGroupRate.SelectedIndex == 2)
                        {
                            topTradeRate.result = "bad";//差评
                        }

                        topTradeRate.anony = false.ToString();
                        topTradeRate.role = "seller";
                        topTradeRateList.Add(topTradeRate);
                    }
                }
                #endregion

                #region 上传数据到淘宝并更新到本地
                foreach (TopTradeRate newReq in topTradeRateList)
                {
                    TradeRateReq tradeRateRep = new TradeRateReq();
                    tradeRateRep.Tid = newReq.tid;
                    tradeRateRep.Content = newReq.content;
                    tradeRateRep.Result = newReq.result;
                    tradeRateRep.Anony = newReq.anony;
                    tradeRateRep.Role = newReq.role;

                    /*上传数据到淘宝*/
                    TradeRateRsp tradeRateRsp = TopService.TradeRateListAdd(newReq.sessionkey, tradeRateRep);

                    if (tradeRateRsp == null)//状态更新到淘宝失败
                    {
                        continue;
                    }

                    /*更新到本地*/
                    Alading.Entity.Trade newTrade = TradeService.GetTrade(newReq.CustomTid);
                    newTrade.tid = tradeRateRsp.Tid;
                    newTrade.created = Convert.ToDateTime(tradeRateRsp.Created);
                    newTrade.seller_rate = true;
                    ratedList.Add(newTrade);
                }

                TradeService.UpdateTrade(ratedList);
                #endregion
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 共用界面方法
        /// <summary>
        /// 绘制GridView，并展示Trade和TradeOrder的数据
        /// </summary>
        /// <param name="tradeList"></param>
        /// <param name="tradeOrderList"></param>
        public void LoadNotRateGridView(List<Alading.Entity.Trade> tradeList, List<Alading.Entity.TradeOrder> tradeOrderList)
        {
            DataTable tradeTable = new DataTable();
            tradeTable.Columns.Add("IsSelected", typeof(bool));
            tradeTable.Columns.Add("tid");//交易编号  
            tradeTable.Columns.Add("end_time");//确认收货时间  
            tradeTable.Columns.Add("buyer_nick");//买家会员号
            tradeTable.Columns.Add("seller_nick");//卖家会员号
            tradeTable.Columns.Add("payment");//金额
            tradeTable.Columns.Add("OrderNum");//自定义 订单数量
            tradeTable.Columns.Add("title");//自定义 订单数量
            tradeTable.Columns.Add("buyer_rate");//买家是否评价
            tradeTable.Columns.Add("CustomTid");//自定义交易编号,用于联系Trade与Order

            #region 获取每笔交易的订单数 商品名称 交易金额
            //记载每一笔交易的订单数
            SortedList<string, string> teadeNumList = new SortedList<string, string>();

            //记载每一笔交易所含的商品名称
            SortedList<string, string> itemTitleList = new SortedList<string, string>();

            //记载每一笔交易的交易金额
            SortedList<string, string> tradePayMentList = new SortedList<string, string>();

            if (tradeOrderList != null)
            {
                IEnumerable<IGrouping<string, TradeOrder>> tradeOrderTemp = tradeOrderList.GroupBy(c => c.CustomTid);

                foreach (IGrouping<string, TradeOrder> igroupTradeOrder in tradeOrderTemp)
                {
                    //用于记录一笔交易中的订单数量
                    int orderNum = 0;

                    //用于记录总金额
                    double allPayMent = 0.0;

                    //用于记录商品名称
                    StringBuilder sbItemTitle = new StringBuilder();

                    foreach (TradeOrder tradeOrder in igroupTradeOrder)
                    {
                        //计算一笔交易中的订单数
                        orderNum++;

                        //计算一笔交易的总金额
                        allPayMent += Convert.ToDouble(tradeOrder.payment);
                        sbItemTitle.Append(tradeOrder.title);
                        sbItemTitle.Append("\r\n");
                        sbItemTitle.Append("\r\n");
                    }

                    teadeNumList.Add(igroupTradeOrder.Key, orderNum.ToString());//增加一笔交易的订单数
                    tradePayMentList.Add(igroupTradeOrder.Key, allPayMent.ToString());//增加一笔交易的金额
                    itemTitleList.Add(igroupTradeOrder.Key, sbItemTitle.ToString());//增加一笔交易商品名称
                }
            }
            #endregion

            #region 加载Trade的数据并显示到界面
            foreach (Alading.Entity.Trade trade in tradeList)
            {
                DataRow row = tradeTable.NewRow();
                row["IsSelected"] = false;
                row["tid"] = trade.tid;
                row["end_time"] = trade.end_time.ToString();
                row["buyer_nick"] = trade.buyer_nick;
                row["seller_nick"] = trade.seller_nick;

                row["payment"] = tradePayMentList.FirstOrDefault(c => c.Key == trade.CustomTid).Value;
                row["OrderNum"] = teadeNumList.FirstOrDefault(c => c.Key == trade.CustomTid).Value;
                row["title"] = itemTitleList.FirstOrDefault(c => c.Key == trade.CustomTid).Value;

                row["buyer_rate"] = trade.buyer_rate == false ? "否" : "是";
                row["CustomTid"] = trade.CustomTid;
                tradeTable.Rows.Add(row);
            }

            gridCtrlRating.DataSource = tradeTable.DefaultView;
            gViewRating.BestFitColumns();
            #endregion
        }

        /// <summary>
        ///  加载评价内容列表
        /// </summary>
        /// <param name="result">评价结果</param>
        public void LoadContentList(string result)
        {
            contentListCtrl.Items.Clear();
            List<RateContent> rateCtntList = RateContentService.GetRateContent(result);

            foreach (RateContent rateContent in rateCtntList)
            {
                contentListCtrl.Items.Add(rateContent.TradeRateContent);
            }
        }

        public void LoadShopList()
        {
            List<Alading.Entity.Shop> shopList = ShopService.GetAllShop();
            NavBarItem navBarItem = new NavBarItem();
            navBarItem.Caption = "所有店铺";
            nick = "所有店铺";
            navBarItem.LinkClicked += new NavBarLinkEventHandler(navBarItem_LinkClicked);
            navBarGroupShopList.ItemLinks.Add(navBarItem);
            foreach (Alading.Entity.Shop shop in shopList)
            {
                navBarItem = new NavBarItem();
                navBarItem.Caption = shop.nick;
                navBarItem.LinkClicked += new NavBarLinkEventHandler(navBarItem_LinkClicked);
                navBarGroupShopList.ItemLinks.Add(navBarItem);
            }
        }

        void navBarItem_LinkClicked(object sender, EventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                NavBarItem navBarItem = (NavBarItem)sender;
                isQuery = false;
                nick = navBarItem.Caption;

                //未评价
                RefreshNotRate();
                //客户差评
                RefreshRated(gridCtrlCBadRated, barEditCSkip, repositoryCComboBoxPage, "buyer", 1
                    , barBtnCFirstPage, barBtnCForwardPage, barBtnCNextPage, barBtnCLastPage);
                //我的差评
                RefreshRated(gridCtrlgMBadRated, barEditMSkip, repositoryMComboBoxPage, "seller", 1
                    , barBtnMFirstPage, barBtnMForwardPage, barBtnMNextPage, barBtnMLastPage);
                //其他评价
                RefreshRated(gridCtrlOtherRated, barEditOSkip, repositoryOComboBoxPage, string.Empty, 1
                    , barBtnOFirstPage, barBtnOForwardPage, barBtnONextPage, barBtnOLastPage);
                //所有订单
                RefreshRated(gridCtrlAllTrade, barEditAllSkip, repositoryAllComboBoxPage, string.Empty, 1
                    , barBtnAFirstPage, barBtnAForwardPage, barBtnANextPage, barBtnALastPage);

                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region 查询
        private void xTPageRating_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            xtraTabPage = xTPageRating.SelectedTabPageIndex;
        }

        /// <summary>
        /// 根据条件获取交易并展示
        /// </summary>
        public void QueryCheck()
        {
            //获取交易成功的数据
            List<Alading.Entity.Trade> tradeList = new List<Alading.Entity.Trade>();
            queryHash[xtraTabPage] = tEKeyWord.Text.Trim();
            isQuery = true;
            switch (xTPageRating.SelectedTabPageIndex)
            {
                case 0://未评价
                    //未评价
                    RefreshNotRate();
                    break;
                case 1://客户差评
                    //客户差评
                    RefreshRated(gridCtrlCBadRated, barEditCSkip, repositoryCComboBoxPage, "buyer", 1
                        , barBtnCFirstPage, barBtnCForwardPage, barBtnCNextPage, barBtnCLastPage);
                    break;
                case 2://我的差评
                    //我的差评
                    RefreshRated(gridCtrlgMBadRated, barEditMSkip, repositoryMComboBoxPage, "seller", 1
                        , barBtnMFirstPage, barBtnMForwardPage, barBtnMNextPage, barBtnMLastPage);
                    break;
                case 3://其他评价
                    //其他评价
                    RefreshRated(gridCtrlOtherRated, barEditOSkip, repositoryOComboBoxPage, string.Empty, 1
                        , barBtnOFirstPage, barBtnOForwardPage, barBtnONextPage, barBtnOLastPage);
                    break;
                case 4://所有订单
                    //所有订单
                    RefreshRated(gridCtrlAllTrade, barEditAllSkip, repositoryAllComboBoxPage, string.Empty, 1
                        , barBtnAFirstPage, barBtnAForwardPage, barBtnANextPage, barBtnALastPage);
                    break;
            }
        }

        /// <summary>
        /// 获取订单数据
        /// </summary>
        public void GetTradeOrder(List<Alading.Entity.Trade> tradeList)
        {
            List<string> customTidList = new List<string>();

            foreach (Alading.Entity.Trade trade in tradeList)
            {
                customTidList.Add(trade.CustomTid);
            }
            //批量获取TradeOrder
            List<Alading.Entity.TradeOrder> tradeOrderList = TradeOrderService.GetTradeOrderByCTid(customTidList);
        }
        #endregion

        #endregion

        #region 未评价
        /// <summary>
        /// 选择按钮，通过勾选确定全选与否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemCheckAllSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (((DevExpress.XtraEditors.CheckEdit)(sender)).Checked == true)//变为选中
            {
                for (int index = 0; index < gViewRating.RowCount; index++)
                {
                    DataRow dataRow = gViewRating.GetDataRow(index);
                    gViewRating.SetRowCellValue(index, IsSelected, true);
                }
            }
            else//变为全不选中
            {
                for (int index = 0; index < gViewRating.RowCount; index++)
                {
                    DataRow dataRow = gViewRating.GetDataRow(index);
                    gViewRating.SetRowCellValue(index, IsSelected, false);
                }
            }
        }

        /// <summary>
        /// 未评价界面的 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                /*导出到EXCEL的方法*/
                Export(gViewRating);
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
        private void barBtnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                //获取全部淘宝店铺的淘宝会员号
                List<string> sessionKeyList = GetSessionKey();

                #region 通过TradesSoldGet 获取所有店铺未评价数据
                foreach (string sessionKey in sessionKeyList)//获取全部淘宝店铺的数据
                {
                    int totalResults = 0;
                    int pageSize = 100;
                    int totalPageNo = 0;//用于存放总页数

                    //用于存放本账号本次获取的全部数据
                    List<Alading.Entity.Trade> tradeAllList = new List<Alading.Entity.Trade>();

                    GetNewTrades(tradeAllList, 1, pageSize, sessionKey, out totalResults);

                    int size = totalResults % totalResults == 0 ? totalPageNo = totalResults / totalResults
                        : totalPageNo = totalResults / totalResults + 1;

                    /*超过两页的循环调取数据*/
                    for (int pageno = 2; pageno <= totalPageNo; pageno++)
                    {
                        GetNewTrades(tradeAllList, pageno, pageSize, sessionKey, out totalResults);
                    }

                    //获取数据的详细信息并保存到数据库
                    GetNewTradeFullInfo(sessionKey, tradeAllList);
                }
                #endregion

                /*刷新界面*/
                RefreshNotRate();
                waitFrm.Close();
                XtraMessageBox.Show("从淘宝获取数据成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                RefreshNotRate();
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 加载处理流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gViewRating_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                DataRow row = gViewRating.GetFocusedDataRow();
                if (row != null && row["CustomTid"] != null)
                {
                    List<TradeInfo> infoList = TradeInfoService.GetTradeInfo(c => c.CustomTid == row["CustomTid"].ToString());
                    gcFlowMsg.DataSource = infoList;
                    gvFlowMsg.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 未评价方法
        /// <summary>
        /// 刷新未评价数据
        /// </summary>
        public void RefreshNotRate()
        {
            Func<Alading.Entity.Trade, bool> func = null;

            #region 查询数据
            if (isQuery)
            {
                if (nick == "所有店铺")
                {
                    //获取所有店铺数据
                    func = new Func<Alading.Entity.Trade, bool>(c => c.status == "TRADE_FINISHED" && c.seller_rate == false
                        && (c.tid.ToUpper().Contains(queryHash[1].ToString()) || c.buyer_nick.ToUpper().Contains(queryHash[1].ToString())));
                }
                else
                {
                    //获取指定店铺数据
                    func = new Func<Alading.Entity.Trade, bool>(c => c.status == "TRADE_FINISHED" && c.seller_rate == false && c.seller_nick == nick
                        && (c.tid.ToUpper().Contains(queryHash[1].ToString()) || c.buyer_nick.ToUpper().Contains(queryHash[1].ToString())));
                }
            }
            else
            {
                //不是查询
                if (nick == "所有店铺")
                {
                    //获取所有店铺数据
                    func = new Func<Alading.Entity.Trade, bool>(c => c.status == "TRADE_FINISHED" && c.seller_rate == false);
                }
                else
                {
                    //获取指定店铺数据
                    func = new Func<Alading.Entity.Trade, bool>(c => c.status == "TRADE_FINISHED" && c.seller_rate == false && c.seller_nick == nick);
                }
            }
            #endregion

            //获取交易成功，卖家还未评价的数据
            List<Alading.Entity.Trade> tradeDBList = TradeService.GetTrade(func);

            List<string> customTidList = new List<string>();
            foreach (Alading.Entity.Trade trade in tradeDBList)
            {
                customTidList.Add(trade.CustomTid);
            }
            //批量获取TradeOrder
            List<Alading.Entity.TradeOrder> tradeOrderList = TradeOrderService.GetTradeOrderByCTid(customTidList);

            //绘制GridView，并展示Trade和TradeOrder的数据
            LoadNotRateGridView(tradeDBList, tradeOrderList);

            //加载好评的评价内容列表
            LoadContentList("good");
        }

        /// <summary>
        /// 获取简单数据
        /// </summary>
        /// <param name="tradeAllList">通过TradesSoldGet 获取所有店铺未评价数据</param>
        /// <param name="pageno"></param>
        /// <param name="pageSize"></param>
        /// <param name="session"></param>
        /// <param name="totalResults"></param>
        private void GetNewTrades(List<Alading.Entity.Trade> tradeAllList,
            int pageno, int pageSize, string session, out int totalResults)
        {
            //用于存放获取回来的数据
            TradeReq tradeReq = new TradeReq();
            tradeReq.Status = TradeEnum.TRADE_FINISHED;
            tradeReq.RateStatus = "RATE_UNSELLER";//卖家未评价
            tradeReq.PageSize = pageSize;
            tradeReq.PageNo = pageno;

            TradeRsp tradeRsp = TopService.TradesSoldGet(session, tradeReq);

            //获取为空 或者 0条数据
            if (tradeRsp == null || tradeRsp.TotalResults == 0)
            {
                totalResults = 0;
                return;
            }

            totalResults = tradeRsp.TotalResults;//赋值总的结果数
            List<Alading.Entity.Shop> shopList = ShopService.GetAllShop();
            foreach (Alading.Taobao.Entity.Trade trade in tradeRsp.Trades.Trade)//获取所有交易
            {
                Alading.Entity.Trade newTrade = new Alading.Entity.Trade();
                newTrade.tid = trade.Tid;//交易单编号
                newTrade.seller_nick = trade.SellerNick;//卖家昵称
                newTrade.buyer_nick = trade.BuyerNick;//买家昵称
                newTrade.alipay_no = trade.AlipayNo == null ? string.Empty : trade.AlipayNo;//支付宝交易号

                Alading.Entity.Shop shop = shopList.Find(c => c.nick == trade.SellerNick);
                if (shop != null)
                {
                    newTrade.CustomTid = string.Format("{0}_{1}", shop.sid, trade.Tid);//自定义编号
                }

                newTrade.seller_rate = trade.SellerRate;//卖家是否已评价
                newTrade.buyer_rate = trade.BuyerRate;//买家是否已评价
                newTrade.status = trade.Status;//交易状态
                if (!string.IsNullOrEmpty(trade.EndTime))
                {
                    newTrade.end_time = DateTime.Parse(trade.EndTime);//交易成功时间
                }
                else
                {
                    newTrade.end_time = DateTime.MinValue;
                }
                tradeAllList.Add(newTrade);
            }
        }

        /// <summary>
        /// 根据Tid获取订单详细信息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="tradeList"></param>
        /// <returns></returns>
        private bool GetNewTradeFullInfo(string session, List<Alading.Entity.Trade> tradeList)
        {
            //用于存放自定义编号
            List<string> customtidList = new List<string>();
            //用于存放买家昵称和本次订单列表中的交易次数
            Hashtable nickHash = new Hashtable();
            List<string> nickList = new List<string>();

            //存放获取的Trade的添加到数据库详细信息
            List<Alading.Entity.Trade> tradeAddFullInfoList = new List<Alading.Entity.Trade>();
            //存放获取的Trade的更新到数据库详细信息
            List<Alading.Entity.Trade> tradeUpdateFullInfoList = new List<Alading.Entity.Trade>();
            //存放获取的TradeOrder的添加到数据库详细信息
            List<Alading.Entity.TradeOrder> orderAddList = new List<Alading.Entity.TradeOrder>();
            //存放获取的TradeOrder的更新到数据库详细信息
            List<Alading.Entity.TradeOrder> orderUpdateList = new List<Alading.Entity.TradeOrder>();
            //存放获取的Consumer的添加到数据库详细信息
            List<Alading.Entity.Consumer> consumerAddList = new List<Alading.Entity.Consumer>();
            //存放获取的Consumer的更新到数据库详细信息
            List<Alading.Entity.Consumer> consumerUpdateList = new List<Alading.Entity.Consumer>();

            //生成获取所有TradeOrder的参数customtidList
            foreach (Alading.Entity.Trade trade in tradeList)
            {
                customtidList.Add(trade.CustomTid);
            }

            //存放数据库的Trade的详细信息
            List<Alading.Entity.Trade> oldFullInfoList = TradeService.GetTrade(customtidList);
            //存放数据库的TradeOrder的详细信息
            List<Alading.Entity.TradeOrder> oldOrderFullInfoList = TradeOrderService.GetTradeOrderByCTid(customtidList);

            //遍历本次获取回来的数据
            foreach (Alading.Entity.Trade trade in tradeList)
            {
                Alading.Entity.Trade oldTrade = oldFullInfoList.Where(c => c.CustomTid == trade.CustomTid).FirstOrDefault();

                if (oldTrade != null)//已在数据库中存在
                {
                    oldTrade.seller_rate = trade.seller_rate;//卖家是否已评价
                    oldTrade.buyer_rate = trade.buyer_rate;//买家是否已评价
                    oldTrade.status = trade.status;//交易状态
                    oldTrade.end_time = trade.end_time;//交易成功时间

                    //改变相应的订单的状态
                    List<Alading.Entity.TradeOrder> oldOrderList = oldOrderFullInfoList.Where(c => c.CustomTid == trade.CustomTid).ToList();
                    foreach (Alading.Entity.TradeOrder oldOrder in oldOrderList)
                    {
                        oldOrder.status = trade.status;
                        orderUpdateList.Add(oldOrder);
                    }

                    tradeUpdateFullInfoList.Add(oldTrade);
                }
                else//数据库中不存在，获取详细信息
                {
                    #region 从网上获取Trade 和 TradeOrder详细信息
                    string tid = trade.tid;
                    TradeRsp fullinfoTraderspReturn = TopService.TradeFullinfoGet(session, tid);//获取特定交易的详细信息

                    if (fullinfoTraderspReturn == null)//获取数据失败，继续下一条
                    {
                        continue;
                    }

                    Alading.Taobao.Entity.Trade fullinfoTradersp = fullinfoTraderspReturn.Trade;

                    Alading.Entity.Trade newTrade = new Alading.Entity.Trade();
                    TradeCopyData(newTrade, fullinfoTradersp);
                    newTrade.post_fee = newTrade.post_fee;

                    newTrade.LastShippingType = newTrade.shipping_type;
                    newTrade.CustomTid = trade.CustomTid;
                    newTrade.ParentCustomTid = "0";
                    newTrade.IsSplited = false;
                    newTrade.LocalStatus = LocalTradeStatus.HasNotSummit;//0在此处表示没有提交打印
                    tradeAddFullInfoList.Add(newTrade);

                    //将淘宝获取的Order的数据保存到orderList准备保存到数据库
                    List<Alading.Entity.TradeOrder> newOrderList = new List<Alading.Entity.TradeOrder>();
                    foreach (Alading.Taobao.Entity.Order orderObj in fullinfoTradersp.Orders.Order)
                    {
                        Alading.Entity.TradeOrder tradeOrderInBase = new Alading.Entity.TradeOrder();
                        OrderCopyData(tradeOrderInBase, orderObj);
                        tradeOrderInBase.CustomTid = trade.CustomTid;
                        orderAddList.Add(tradeOrderInBase);
                    }
                    #endregion

                    #region 保存买家淘宝会员号 防止重复添加buyer_nick 准备获取客户信息
                    object value = nickHash[trade.buyer_nick];
                    //本次获取的订单中首次添加买家会员号
                    if (value == null)
                    {
                        nickHash.Add(trade.buyer_nick, 1);
                        nickList.Add(trade.buyer_nick);
                    }
                    else
                    {
                        nickHash.Remove(trade.buyer_nick);//去除
                        nickHash.Add(trade.buyer_nick, int.Parse(value.ToString()) + 1);
                    }
                    #endregion
                }
            }

            //获取客户信息
            GetConsumerInfo(session, nickList, nickHash, consumerAddList, consumerUpdateList);

            //保存到数据库
            TradeService.TradeOrderConsumer(tradeAddFullInfoList, tradeUpdateFullInfoList, orderAddList, orderUpdateList
                , consumerAddList, consumerUpdateList, customtidList);
            return true;
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="nickList"></param>
        /// <param name="nickHash"></param>
        /// <param name="consumerAddList"></param>
        /// <param name="consumerUpdateList"></param>
        public void GetConsumerInfo(string session, List<string> nickList, Hashtable nickHash
            , List<Alading.Entity.Consumer> consumerAddList, List<Alading.Entity.Consumer> consumerUpdateList)
        {
            //用于存放数据库的订单的详细信息
            List<Alading.Entity.Consumer> oldConsumerList = ConsumerService.GetConsumer(nickList);

            int pageNo = 0;//需要获取的页数
            //确定到淘宝去获取user的次数
            if (nickList.Count == 0)
            {
                return;
            }
            else
            {
                pageNo = nickList.Count % pageSize == 0 ? nickList.Count / pageSize : nickList.Count / pageSize + 1;
            }

            #region  买家信息
            for (int page = 0; page < pageNo; page++)//页数
            {
                StringBuilder sbNicks = new StringBuilder();
                for (int count = 0; count < pageSize; count++)//当前页数据数目
                {
                    if (pageSize * page + count < nickList.Count && !string.IsNullOrEmpty(nickList[pageSize * page + count]))
                    {
                        sbNicks.Append(nickList[pageSize * page + count]);
                        sbNicks.Append(",");
                    }
                    else
                    {
                        break;
                    }
                }
                sbNicks.Remove(sbNicks.Length - 1, 1);//创建参数 nicks

                UserRsp userRsp = TopService.UsersGet(session, sbNicks.ToString());

                foreach (Alading.Taobao.Entity.User TbUser in userRsp.Users.User)
                {
                    Alading.Entity.Consumer consumer = oldConsumerList.Find(c => c.nick == TbUser.Nick);
                    Alading.Entity.Consumer BuyerDetail = new Alading.Entity.Consumer();
                    UserCopyData(BuyerDetail, TbUser);
                    BuyerDetail.historytradetimes = int.Parse(nickHash[BuyerDetail.nick].ToString());

                    if (consumer == null)//还没有与该用户进行过交易，添加
                    {
                        consumerAddList.Add(BuyerDetail);
                    }
                    else
                    {
                        consumerUpdateList.Add(BuyerDetail);
                    }
                }
            }
            #endregion
        }
        #endregion

        #region 客户差评
        /// <summary>
        /// 客户差评 的 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnCstmExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                /*导出到EXCEL的方法*/
                Export(gViewCBadRated);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击客户差评的更新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnCstmDownload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                //获取卖家 得到的 差评 数据
                GetRatedTrades(true, "get");

                //刷新买家差评界面,当前为第一页
                RefreshRated(gridCtrlCBadRated, barEditCSkip, repositoryCComboBoxPage, "buyer", 1
                    , barBtnCFirstPage, barBtnCForwardPage, barBtnCNextPage, barBtnCLastPage);

                waitFrm.Close();
                XtraMessageBox.Show("更新成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击客户差评界面的刷新按钮，从数据库刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnCstmRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                int pageNum = int.Parse(repositoryCComboBoxPage.Tag.ToString());
                //刷新 买家 给出的差评
                RefreshRated(gridCtrlCBadRated, barEditCSkip, repositoryCComboBoxPage, "buyer", pageNum
                    , barBtnCFirstPage, barBtnCForwardPage, barBtnCNextPage, barBtnCLastPage);
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击 客户差评 的首页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnFirstPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string role = "buyer";
                //展示首页
                FirstPage(gridCtrlCBadRated, barEditCSkip, repositoryCComboBoxPage, role
                    , barBtnCFirstPage, barBtnCForwardPage, barBtnCNextPage, barBtnCLastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击 客户差评 的上一页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnCForwardPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string role = "buyer";
                //展示上一页
                ForwardPage(gridCtrlCBadRated, barEditCSkip, repositoryCComboBoxPage, role
                    , barBtnCFirstPage, barBtnCForwardPage, barBtnCNextPage, barBtnCLastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击 客户差评 的下一页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnCNextPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string role = "buyer";
                //展示下一页
                NextPage(gridCtrlCBadRated, barEditCSkip, repositoryCComboBoxPage, role
                    , barBtnCFirstPage, barBtnCForwardPage, barBtnCNextPage, barBtnCLastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击 客户差评 的尾页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnCLastPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string role = "buyer";
                //展示尾页
                LastPage(gridCtrlCBadRated, barEditCSkip, repositoryCComboBoxPage, role
                    , barBtnCFirstPage, barBtnCForwardPage, barBtnCNextPage, barBtnCLastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击 客户差评 的跳转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryCComboBoxPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string role = "buyer";
                int page = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).SelectedIndex+1;
                SkipPage(gridCtrlCBadRated, page, repositoryCComboBoxPage, role
                        , barBtnCFirstPage, barBtnCForwardPage, barBtnCNextPage, barBtnCLastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 我的差评
        /// <summary>
        /// 我的差评 的 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnMyExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //导出到EXCEL的方法
                Export(gViewMBadRated);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 我的差评 的 更新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnMyDownload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                GetRatedTrades(true, "give");

                //刷新操作
                RefreshRated(gridCtrlgMBadRated, barEditMSkip, repositoryMComboBoxPage, "seller", 1
                    , barBtnMFirstPage, barBtnMForwardPage, barBtnMNextPage, barBtnMLastPage);
                waitFrm.Close();
                XtraMessageBox.Show("更新成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 我的差评 的 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnMyRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                int pageNum = int.Parse(repositoryMComboBoxPage.Tag.ToString());
                //刷新操作
                RefreshRated(gridCtrlgMBadRated, barEditMSkip, repositoryMComboBoxPage, "seller", pageNum
                    , barBtnMFirstPage, barBtnMForwardPage, barBtnMNextPage, barBtnMLastPage);
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 我的差评 的 首页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnMFirstPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string role = "buyer";
                //展示首页
                FirstPage(gridCtrlgMBadRated, barEditMSkip, repositoryMComboBoxPage, role
                    , barBtnMFirstPage, barBtnMForwardPage, barBtnMNextPage, barBtnMLastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 我的差评 的 上一页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnMForwardPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string role = "buyer";
                //展示上一页
                ForwardPage(gridCtrlgMBadRated, barEditMSkip, repositoryMComboBoxPage, role
                    , barBtnMFirstPage, barBtnMForwardPage, barBtnMNextPage, barBtnMLastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 我的差评 的 下一页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnMNextPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string role = "buyer";
                //展示下一页
                NextPage(gridCtrlgMBadRated, barEditMSkip, repositoryMComboBoxPage, role
                    , barBtnMFirstPage, barBtnMForwardPage, barBtnMNextPage, barBtnMLastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 我的差评 的 尾页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnMLastPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string role = "buyer";
                //展示尾页
                LastPage(gridCtrlgMBadRated, barEditMSkip, repositoryMComboBoxPage, role
                    , barBtnMFirstPage, barBtnMForwardPage, barBtnMNextPage, barBtnMLastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 我的差评 的 跳转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryMComboBoxPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string role = "buyer";
                int page = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).SelectedIndex+1;
                //展示跳转页
                SkipPage(gridCtrlCBadRated, page, repositoryMComboBoxPage, role
                    , barBtnCFirstPage, barBtnCForwardPage, barBtnCNextPage, barBtnCLastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 其他评价
        /// <summary>
        /// 其他订单 的 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnOtherExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //导出到EXCEL的方法
                Export(gViewOtherRated);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 其他订单 的 更新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnOtherDownload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                GetRatedTrades(false, "get");

                //刷新操作当前展示第一页 包括卖家和买家
                RefreshRated(gridCtrlOtherRated, barEditOSkip, repositoryOComboBoxPage, string.Empty, 1
                    , barBtnOFirstPage, barBtnOForwardPage, barBtnONextPage, barBtnOLastPage);
                waitFrm.Close();
                XtraMessageBox.Show("更新成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 其他评价 的 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnOtherRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                int pageNum = int.Parse(repositoryOComboBoxPage.Tag.ToString());
                //刷新操作
                RefreshRated(gridCtrlOtherRated, barEditMSkip, repositoryOComboBoxPage, string.Empty, pageNum
                        , barBtnOFirstPage, barBtnOForwardPage, barBtnONextPage, barBtnOLastPage);
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 其他评价 的 首页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnOFirstPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FirstPage(gridCtrlOtherRated, barEditOSkip, repositoryOComboBoxPage, string.Empty
                    , barBtnOFirstPage, barBtnOForwardPage, barBtnONextPage, barBtnOLastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 其他评价 的 上一页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnOForwardPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ForwardPage(gridCtrlOtherRated, barEditOSkip, repositoryOComboBoxPage, string.Empty
                    , barBtnOFirstPage, barBtnOForwardPage, barBtnONextPage, barBtnOLastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 其他评价 的 下一页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnONextPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                NextPage(gridCtrlOtherRated, barEditOSkip, repositoryOComboBoxPage, string.Empty
                    , barBtnOFirstPage, barBtnOForwardPage, barBtnONextPage, barBtnOLastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 其他评价 的 尾页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnOLastPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                LastPage(gridCtrlOtherRated, barEditOSkip, repositoryOComboBoxPage, string.Empty
                    , barBtnOFirstPage, barBtnOForwardPage, barBtnONextPage, barBtnOLastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 其他评价 的 跳转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryOComboBoxPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int page = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).SelectedIndex+1;
                SkipPage(gridCtrlOtherRated, page, repositoryOComboBoxPage, string.Empty
                    , barBtnOFirstPage, barBtnOForwardPage, barBtnONextPage, barBtnOLastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 全部订单
        /// <summary>
        /// 点击全部订单的导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnAllExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //导出到EXCEL的方法
                Export(gViewAllTraded);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击全部订单的更新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnAllDownload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                GetRatedTrades(false, "get");

                //刷新操作
                RefreshRated(gridCtrlAllTrade, barEditAllSkip, repositoryAllComboBoxPage, string.Empty, 1
                    , barBtnAFirstPage, barBtnAForwardPage, barBtnANextPage, barBtnALastPage);
                waitFrm.Close();
                XtraMessageBox.Show("更新成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnAllRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                int pageNum = int.Parse(repositoryAllComboBoxPage.Tag.ToString());
                //刷新操作
                RefreshRated(gridCtrlAllTrade, barEditAllSkip, repositoryAllComboBoxPage, string.Empty, pageNum
                    , barBtnAFirstPage, barBtnAForwardPage, barBtnANextPage, barBtnALastPage);
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击所有订单界面的首页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnAFirstPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FirstPage(gridCtrlAllTrade, barEditAllSkip, repositoryAllComboBoxPage, string.Empty
                    , barBtnAFirstPage, barBtnAForwardPage, barBtnANextPage, barBtnALastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击所有订单界面的上一页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnAForwardPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ForwardPage(gridCtrlAllTrade, barEditAllSkip, repositoryAllComboBoxPage, string.Empty
                    , barBtnAFirstPage, barBtnAForwardPage, barBtnANextPage, barBtnALastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击所有订单界面的下一页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnANextPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                NextPage(gridCtrlAllTrade, barEditAllSkip, repositoryAllComboBoxPage, string.Empty
                    , barBtnAFirstPage, barBtnAForwardPage, barBtnANextPage, barBtnALastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击所有订单界面的尾页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnALastPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                LastPage(gridCtrlAllTrade, barEditAllSkip, repositoryAllComboBoxPage, string.Empty
                    , barBtnAFirstPage, barBtnAForwardPage, barBtnANextPage, barBtnALastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击所有订单界面的跳转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryAllComboBoxPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int page = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).SelectedIndex+1;
                SkipPage(gridCtrlAllTrade, page, repositoryAllComboBoxPage, string.Empty
                    , barBtnAFirstPage, barBtnAForwardPage, barBtnANextPage, barBtnALastPage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 加载处理流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gViewAllTraded_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gViewAllTraded.GetFocusedDataRow();
            if (row != null && row["CustomTid"] != null)
            {
                List<TradeInfo> infoList = TradeInfoService.GetTradeInfo(c => c.CustomTid == row["CustomTid"].ToString());
                gcFlowMsg.DataSource = infoList;
                gvFlowMsg.BestFitColumns();
            }
        }
        #endregion

        #region 已评价方法
        /// <summary>
        /// 循环获取已评价信息
        /// </summary>
        public void GetRatedTrades(bool isBadRate, string rateType)
        {
            //获取全部淘宝店铺的淘宝会员号
            List<string> sessionKeyList = GetSessionKey();

            //获取每一个店铺的评价数据
            foreach (string sessionKey in sessionKeyList)
            {
                int totalResults = 0;
                int pageSize = 200;
                int totalPageNo = 0;
                List<Alading.Entity.TradeRate> allTradeRateList = new List<TradeRate>();
                List<string> tidList = new List<string>();

                //从淘宝网获取数据
                GetTradeRated(allTradeRateList, tidList, sessionKey, 1, pageSize, isBadRate, rateType, out totalResults);

                if (totalResults == 0)//能除尽
                {
                    return;
                }
                else//不能除尽
                {
                    totalPageNo = totalResults % pageSize == 0 ? totalResults / pageSize : totalResults / pageSize + 1;
                }

                //超过两页的循环调取数据
                for (int pageno = 2; pageno <= totalPageNo; pageno++)
                {
                    GetTradeRated(allTradeRateList, tidList, sessionKey, pageno, pageSize, isBadRate, rateType, out totalResults);
                }

                //存放数据库中数据
                List<Alading.Entity.TradeRate> rateList = TradeRateService.GetTradeRateByTid(tidList);
                List<Alading.Entity.TradeRate> addRateList = new List<TradeRate>();

                foreach (Alading.Entity.TradeRate tradeRate in allTradeRateList)
                {
                    Alading.Entity.TradeRate oldRate = rateList.Find(c => c.tid == tradeRate.tid);
                    if (oldRate == null)//数据库中没有才添加
                    {
                        addRateList.Add(tradeRate);
                    }
                }
                TradeRateService.AddTradeRate(addRateList);
            }
        }

        /// <summary>
        /// 获取已评价的交易信息
        /// </summary>1,pageSize, true, "get",out totalResults
        /// <returns>判断是否一获取完数据</returns>
        public void GetTradeRated(List<Alading.Entity.TradeRate> allTradeRateList, List<string> tidList
            , string session, int pageno, int pageSize, bool isBadRate, string rateType, out int totalResults)
        {
            TradeRateReq traderatereq = new TradeRateReq();
            traderatereq.RateType = rateType;
            traderatereq.Role = "seller";
            traderatereq.PageSize = pageSize;
            traderatereq.PageNo = pageno;

            //判断是否是获取差评的数据
            if (isBadRate)
            {
                traderatereq.Result = "bad";
            }

            TradeRateRsp tradeRateRsp = TopService.TradeRatesGet(session, traderatereq);

            if (tradeRateRsp == null || tradeRateRsp.TotalResults == 0)
            {
                totalResults = 0;
                return;
            }

            totalResults = tradeRateRsp.TotalResults;

            foreach (Alading.Taobao.Entity.TradeRate TbTradeRate in tradeRateRsp.Rates.TradeRate)
            {
                Alading.Entity.TradeRate tradeRate = new Alading.Entity.TradeRate();
                TradeRateCopyData(tradeRate, TbTradeRate);

                allTradeRateList.Add(tradeRate);
                tidList.Add(tradeRate.tid);
            }
        }

        /// <summary>
        /// 刷新差评的数据（包括 买家 和 卖家）
        /// </summary>
        /// <param name="role"></param>
        /// <param name="pageNum"></param>
        public void RefreshRated(GridControl gCtrlRated, BarEditItem barEdit, RepositoryItemComboBox ComboBoxPage, string role, int pageNum
            , BarButtonItem firstPage, BarButtonItem forwardPage, BarButtonItem nextPage, BarButtonItem LastPage)
        {
            rowCount = 0;

            //获取数据并传给GridView
            GetData(role, pageNum, gCtrlRated);

            //赋总页数
            int pageTotalNum = 1;
            if (rowCount == 0)
            {
                firstPage.Enabled = false;
                forwardPage.Enabled = false;
                nextPage.Enabled = false;
                LastPage.Enabled = false;
            }
            else
            {
                pageTotalNum = rowCount % pageSize == 0 ? rowCount / pageSize : rowCount / pageSize + 1;
            }
            //显示所有页数
            SetComboBoxValue(barEdit, ComboBoxPage, pageTotalNum);
            //改变当前页码
            barEdit.EditValue = ComboBoxPage.Items[pageNum - 1];
            //当前页数
            ComboBoxPage.Tag = pageNum;
        }

        /// <summary>
        /// 加载全部订单
        /// </summary>
        /// <param name="tradeAndRateList"></param>
        public void LoadAllTrade(List<TradeRateInherit> tradeRateInheritList, List<Alading.Entity.Trade> tradeList)
        {
            foreach (Alading.Entity.Trade trade in tradeList)
            {
                TradeRateInherit tradeRateInherit = new TradeRateInherit();
                tradeRateInherit.tid = trade.tid;
                tradeRateInherit.buyer_nick = trade.buyer_nick;
                tradeRateInherit.seller_nick = trade.seller_nick;
                tradeRateInherit.payment = trade.payment;
                tradeRateInherit.seller_rate = trade.seller_rate;
                tradeRateInherit.buyer_rate = trade.buyer_rate;
                tradeRateInherit.item_title = trade.title;
                tradeRateInheritList.Add(tradeRateInherit);
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 从淘宝获取数据
        /// </summary>
        public List<string> GetSessionKey()
        {
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
            return sessionKeyList;
        }

        /// <summary>
        /// 导出的方法
        /// </summary>
        public void Export(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = saveFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                gridView.ExportToExcelOld(saveFileDialog.FileName);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 自动评价的方法
        /// </summary>
        public void AutoRate(object obj)
        {
            List<Alading.Entity.Trade> tradeList = TradeService.GetTrade(c => c.status == TradeEnum.TRADE_FINISHED);
            List<Alading.Entity.RateContent> contentList = RateContentService.GetRateContent("good");

            /*用于存放评价成功的数据*/
            List<Alading.Entity.Trade> ratedList = new List<Alading.Entity.Trade>();

            Random random = new Random();
            string content = contentList[random.Next(1, contentList.Count)].TradeRateContent;

            /*上传数据到淘宝并更新到本地*/
            foreach (Alading.Entity.Trade trade in tradeList)
            {
                TradeRateReq tradeRateRep = new TradeRateReq();
                tradeRateRep.Tid = trade.tid;
                tradeRateRep.Content = content;
                tradeRateRep.Result = "good";
                tradeRateRep.Anony = "false";
                tradeRateRep.Role = "seller";

                /*上传数据到淘宝*/
                if (!string.IsNullOrEmpty(trade.seller_nick))
                {
                    string session = SystemHelper.GetSessionKey(trade.seller_nick);
                    TradeRateRsp tradeRateRsp = TopService.TradeRateListAdd(session, tradeRateRep);
                    if (tradeRateRsp == null)//状态更新到淘宝失败
                    {
                        continue;
                    }

                    /*更新到本地*/
                    Alading.Entity.Trade newTrade = TradeService.GetTrade(trade.CustomTid);
                    newTrade.tid = tradeRateRsp.Tid;
                    newTrade.created = Convert.ToDateTime(tradeRateRsp.Created);
                    newTrade.seller_rate = true;
                    ratedList.Add(newTrade);
                }
                else
                {
                    continue;
                }
            }


            TradeService.UpdateTrade(ratedList);
        }

        /// <summary>
        /// 赋值给Trade表
        /// </summary>
        /// <param name="destObj"></param>
        /// <param name="srcObj"></param>
        public static void TradeCopyData(Alading.Entity.Trade destObj, Alading.Taobao.Entity.Trade srcObj)
        {
            #region    Using All Items Replace To Update ,Default UnUse
            #endregion
            destObj.status = srcObj.Status;
            destObj.tid = srcObj.Tid;
            destObj.iid = srcObj.Iid ?? string.Empty;
            destObj.CustomTid = srcObj.Tid + srcObj.SellerNick;
            destObj.seller_nick = srcObj.SellerNick ?? string.Empty;
            destObj.buyer_nick = srcObj.BuyerNick ?? string.Empty;
            destObj.title = srcObj.Title ?? string.Empty;
            destObj.type = srcObj.Type ?? string.Empty;
            destObj.created = DateTime.Parse(srcObj.Created ?? Alading.Taobao.Constants.DEFAULT_TIME);
            destObj.price = srcObj.ItemPrice;
            destObj.pic_path = srcObj.ItemUrl ?? string.Empty;
            destObj.num = srcObj.ItemNum;
            destObj.buyer_message = srcObj.BuyerMessage ?? string.Empty;
            destObj.buyer_rate = srcObj.BuyerRate;
            destObj.buyer_memo = srcObj.BuyerMemo ?? string.Empty;
            destObj.seller_rate = srcObj.SellerRate;
            destObj.seller_memo = srcObj.SellerMemo ?? string.Empty;
            destObj.shipping_type = srcObj.ShippingType ?? string.Empty;
            destObj.alipay_no = srcObj.AlipayNo ?? string.Empty;
            destObj.payment = srcObj.Payment;
            destObj.discount_fee = srcObj.DiscountFee;
            destObj.adjust_fee = srcObj.AdjustFee;
            destObj.snapshot_url = srcObj.SnapshotUrl;
            destObj.snapshot = srcObj.Snapshot ?? string.Empty;
            destObj.pay_time = DateTime.Parse(srcObj.PayTime ?? Alading.Taobao.Constants.DEFAULT_TIME);
            destObj.end_time = DateTime.Parse(srcObj.EndTime ?? Alading.Taobao.Constants.DEFAULT_TIME);
            destObj.modified = DateTime.Parse(srcObj.Modified ?? Alading.Taobao.Constants.DEFAULT_TIME);
            destObj.buyer_obtain_point_fee = srcObj.BuyerObtainPointFee;
            destObj.point_fee = string.Empty;
            destObj.real_point_fee = string.Empty;
            destObj.total_fee = srcObj.TotalFee;
            destObj.post_fee = srcObj.PostFee;
            destObj.buyer_alipay_no = srcObj.BuyerAlipayNo ?? string.Empty;
            destObj.receiver_name = srcObj.ReceiverName ?? string.Empty;
            destObj.receiver_state = srcObj.ReceiverState ?? string.Empty;
            destObj.receiver_city = srcObj.ReceiverCity ?? string.Empty;
            destObj.receiver_district = srcObj.ReceiverDistrict ?? string.Empty;
            destObj.receiver_address = srcObj.ReceiverAddress ?? string.Empty;
            destObj.receiver_zip = srcObj.ReceiverZip ?? string.Empty;
            destObj.receiver_mobile = srcObj.ReceiverMobile ?? string.Empty;
            destObj.receiver_phone = srcObj.ReceiverPhone ?? string.Empty;
            destObj.consign_time = DateTime.Parse(srcObj.ConsignTime ?? Alading.Taobao.Constants.DEFAULT_TIME);
            destObj.buyer_email = srcObj.BuyerEmail ?? string.Empty;
            destObj.commission_fee = srcObj.CommissionFee ?? string.Empty;
            destObj.seller_alipay_no = srcObj.SellerAlipayNo ?? string.Empty;
            destObj.seller_mobile = srcObj.SellerMobile ?? string.Empty;
            destObj.seller_phone = srcObj.SellerPhone ?? string.Empty;
            destObj.seller_name = srcObj.SellerName ?? string.Empty;
            destObj.seller_email = srcObj.SellerEmail ?? string.Empty;
            destObj.available_confirm_fee = srcObj.AvailableConfirmFee ?? string.Empty;
            destObj.has_post_fee = srcObj.HasPostFee;
            destObj.received_payment = srcObj.ReceivedPayment;
            destObj.cod_fee = srcObj.CodFee;
            destObj.cod_status = srcObj.CodStatus ?? string.Empty;
            destObj.timeout_action_time = DateTime.Parse(srcObj.Timeout ?? Alading.Taobao.Constants.DEFAULT_TIME);
            destObj.is_3D = false;
            destObj.shipping_type = string.Empty;
            destObj.ParentCustomTid = string.Empty;
            destObj.BuyerType = 1;//?
            destObj.type = srcObj.Type ?? string.Empty;
            destObj.SellerType = 0;
            destObj.ShippingCode = string.Empty;
            destObj.ShippingCode = LocalTradeStatus.HasNotSummit;
            destObj.LogisticCompanyCode = string.Empty;
            destObj.TemplateCode = string.Empty;
            destObj.CombineCode = string.Empty;
            destObj.LockedUserCode = string.Empty;
            destObj.LockedUserName = string.Empty;
            destObj.IsCombined = false;
            destObj.IsSplited = false;
            //Constants.DEFAULT_SHIPPING_COMPANY/*申通*/;
            destObj.TradeSourceType = TradeSourceType.TAOBAOAPI;
            destObj.HasInvoice = false;
            destObj.ConsignStatus = 0;
            destObj.TradeTimeStamp = new byte[0];
        }

        /// <summary>
        /// 赋值给TradeOrder表
        /// </summary>
        /// <param name="destObj"></param>
        /// <param name="srcObj"></param>
        private void OrderCopyData(Alading.Entity.TradeOrder destObj, Alading.Taobao.Entity.Order srcObj)
        {
            destObj.iid = Guid.NewGuid().ToString();
            destObj.CustomTid = string.Empty;
            destObj.iid = srcObj.Iid;
            destObj.sku_id = srcObj.SkuId ?? string.Empty;
            destObj.oid = srcObj.Oid;
            destObj.outer_sku_id = srcObj.OuterSkuId ?? string.Empty;
            destObj.outer_iid = srcObj.OuterIid ?? string.Empty;
            destObj.sku_properties_name = srcObj.SkuProps ?? string.Empty;
            destObj.price = srcObj.ItemPrice == null ? 0.0 : double.Parse(srcObj.ItemPrice);
            destObj.total_fee = srcObj.TotalFee == null ? 0.0 : double.Parse(srcObj.TotalFee.ToString());
            destObj.discount_fee = srcObj.DiscountFee == null ? 0.0 : double.Parse(srcObj.DiscountFee.ToString());
            destObj.adjust_fee = srcObj.AdjustFee == null ? 0.0 : double.Parse(srcObj.AdjustFee.ToString());
            destObj.payment = srcObj.Payment == null ? 0.0 : double.Parse(srcObj.Payment.ToString());
            destObj.item_meal_name = srcObj.ItemMealName ?? string.Empty;
            destObj.num = srcObj.ItemNum;
            destObj.title = srcObj.ItemTitle;
            destObj.pic_path = srcObj.SnapshotUrl ?? string.Empty;
            destObj.seller_nick = srcObj.SellerNick ?? string.Empty;
            destObj.buyer_nick = srcObj.BuyerNick ?? string.Empty;
            destObj.created = DateTime.Parse("0001-01-01 00:00:00");
            destObj.refund_status = srcObj.RefundStatus;
            destObj.status = TradeEnum.WAIT_BUYER_CONFIRM_GOODS;
            destObj.seller_type = string.Empty;
            destObj.snapshot_url = srcObj.SnapshotUrl ?? string.Empty;
            destObj.snapshot = srcObj.Snapshot ?? string.Empty;
            destObj.timeout_action_time = DateTime.Parse(srcObj.OrderTimeout ?? "0001-01-01 00:00:00");
            destObj.OrderType = "商品";
            destObj.OrderTimeStamp = new byte[0];
            destObj.HouseCode = string.Empty;
            destObj.LayoutCode = string.Empty;
            destObj.type = srcObj.OrderType ?? string.Empty;
        }

        /// <summary>
        /// 传递User表的值
        /// </summary>
        /// <param name="desObj"></param>
        /// <param name="srcObj"></param>
        private void UserCopyData(Alading.Entity.Consumer desObj, Alading.Taobao.Entity.User srcObj)
        {
            desObj.nick = srcObj.Nick;
            desObj.sex = srcObj.Sex;
            desObj.location_city = srcObj.Location.City;
            desObj.location_state = srcObj.Location.State;
            desObj.location_district = srcObj.Location.District;
            desObj.location_address = srcObj.Location.Address;
            desObj.location_country = srcObj.Location.Country;

            if (srcObj.Birthday == null || string.IsNullOrEmpty(srcObj.Birthday.ToString()))
            {
                desObj.birthday = System.DateTime.MinValue;
            }
            else
            {
                desObj.birthday = srcObj.Birthday;
            }

            #region 无法从淘宝网获取的字段
            desObj.mobilephone = string.Empty;
            desObj.phone = string.Empty;
            desObj.email = string.Empty;
            desObj.historytradetimes = 1;
            desObj.historyexpense = 0.0;
            desObj.comments = string.Empty;
            desObj.vip = false;
            desObj.alipay = string.Empty;
            desObj.buyer_zip = string.Empty;
            desObj.status = string.Empty;
            desObj.last_visit = System.DateTime.MinValue;
            desObj.is_dealer = false;
            desObj.source = 0;
            desObj.last_trade = System.DateTime.MinValue;
            desObj.buyer_name = string.Empty;
            desObj.buyer_wangwang = string.Empty;
            desObj.@checked = false;
            #endregion

            desObj.credit = JsonConvert.SerializeObject(srcObj.BuyerCredit);
            desObj.level = srcObj.BuyerCredit.Level;
            desObj.score = srcObj.BuyerCredit.Score;
            desObj.created = DateTime.Parse(srcObj.Created);

            desObj.buyer_good_num = srcObj.BuyerCredit.GoodNum;
            desObj.buyer_total_num = srcObj.BuyerCredit.TotalNum;
            desObj.last_visit = srcObj.LastVisit;

        }

        /// <summary>
        /// 传递TradeRate表的值
        /// </summary>
        /// <param name="tradeRate"></param>
        /// <param name="TbTradeRate"></param>
        private void TradeRateCopyData(Alading.Entity.TradeRate tradeRate, Alading.Taobao.Entity.TradeRate TbTradeRate)
        {
            tradeRate.tid = TbTradeRate.Tid;//交易编号
            tradeRate.oid = TbTradeRate.Oid;//交易的订单号
            tradeRate.role = TbTradeRate.Role;//评价者角色
            tradeRate.nick = TbTradeRate.RaterNick;//评价者昵称
            tradeRate.result = TbTradeRate.Result;//评价结果 好中差评
            tradeRate.created = Convert.ToDateTime(TbTradeRate.Created);//评价创建时间
            tradeRate.rated_nick = TbTradeRate.RatedNick;//被评价者昵称
            tradeRate.rated_nick = TbTradeRate.RatedNick;//被评价者昵称
            tradeRate.item_title = TbTradeRate.ItemTitle;//商品标题
            tradeRate.item_price = TbTradeRate.ItemPrice;//商品价格
            tradeRate.content = TbTradeRate.Content;//评价内容
            tradeRate.reply = TbTradeRate.Reply;//评价解释

            tradeRate.TradeRateCode = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="role"></param>
        /// <param name="pageNum"></param>
        /// <param name="rowCount"></param>
        /// <param name="gCtrlRated"></param>
        public void GetData(string role, int pageNum, GridControl gCtrlRated)
        {
            #region 获取数据
            List<Alading.Entity.TradeRate> tradeRateList = new List<TradeRate>();
            Func<Alading.Entity.TradeRate, bool> func = null;
            if (role == "buyer")
            {
                #region 客户差评交易
                if (isQuery)
                {
                    //查询数据
                    if (nick == "所有店铺")
                    {
                        //加载所有店铺数据
                        func = new Func<Alading.Entity.TradeRate, bool>(c => c.result == "bad" && c.role == role && (c.tid.ToUpper().Contains(queryHash[2].ToString())
                            || c.nick.ToUpper().Contains(queryHash[2].ToString())));
                    }
                    else
                    {
                        //加载指定店铺的数据
                        func = new Func<Alading.Entity.TradeRate, bool>(c => c.result == "bad" && c.role == role && c.rated_nick == nick
                            && (c.tid.ToUpper().Contains(queryHash[2].ToString()) || c.nick.ToUpper().Contains(queryHash[2].ToString())));
                    }
                }
                else
                {
                    //不是查询
                    if (nick == "所有店铺")
                    {
                        //加载所有店铺数据
                        func = new Func<Alading.Entity.TradeRate, bool>(c => c.result == "bad" && c.role == role);
                    }
                    else
                    {
                        //加载指定店铺的数据
                        func = new Func<Alading.Entity.TradeRate, bool>(c => c.result == "bad" && c.role == role && c.rated_nick == nick);
                    }
                }
                tradeRateList = TradeRateService.GetTradeRate(func, pageNum, pageSize, out rowCount);
                //加载TradeRate数据表中的数据
                gCtrlRated.DataSource = tradeRateList;
                gViewCBadRated.BestFitColumns();
                #endregion
            }
            else if (role == "seller")
            {
                #region 我的差评交易
                if (isQuery)
                {
                    //查询数据
                    if (nick == "所有店铺")
                    {
                        //加载所有店铺数据
                        func = new Func<Alading.Entity.TradeRate, bool>(c => c.result == "bad" && c.role == role
                            && (c.tid.ToUpper().Contains(queryHash[3].ToString()) || c.rated_nick.ToUpper().Contains(queryHash[3].ToString())));
                    }
                    else
                    {
                        //加载指定店铺的数据
                        func = new Func<Alading.Entity.TradeRate, bool>(c => c.result == "bad" && c.role == role && c.nick == nick
                            && (c.tid.ToUpper().Contains(queryHash[3].ToString()) || c.rated_nick.ToUpper().Contains(queryHash[3].ToString())));
                    }
                }
                else
                {
                    //不是查询
                    if (nick == "所有店铺")
                    {
                        //加载所有店铺数据
                        func = new Func<Alading.Entity.TradeRate, bool>(c => c.result == "bad" && c.role == role);
                    }
                    else
                    {
                        //加载指定店铺的数据
                        func = new Func<Alading.Entity.TradeRate, bool>(c => c.result == "bad" && c.role == role && c.nick == nick);
                    }
                }
                tradeRateList = TradeRateService.GetTradeRate(func, pageNum, pageSize, out rowCount);
                //加载TradeRate数据表中的数据
                gCtrlRated.DataSource = tradeRateList;
                gViewMBadRated.BestFitColumns();
                #endregion
            }
            else if (gCtrlRated == gridCtrlAllTrade)
            {
                #region 全部订单
                List<Alading.Entity.Trade> tradeList = new List<Alading.Entity.Trade>();
                if (isQuery)
                {
                    Func<Alading.Entity.Trade, bool> funcTrade = null;
                    //查询数据
                    if (nick == "所有店铺")
                    {
                        //加载所有店铺数据
                        funcTrade = new Func<Alading.Entity.Trade, bool>(c => c.status==TradeEnum.TRADE_FINISHED && (c.tid.ToUpper().Contains(queryHash[4].ToString())
                            || c.buyer_nick.ToUpper().Contains(queryHash[4].ToString())));
                        tradeList = TradeService.GetTrade(funcTrade, pageNum, pageSize, out rowCount);
                    }
                    else
                    {
                        //加载指定店铺的数据
                        funcTrade = new Func<Alading.Entity.Trade, bool>(c => c.seller_nick == nick && c.status == TradeEnum.TRADE_FINISHED
                            && (c.tid.ToUpper().Contains(queryHash[4].ToString())|| c.buyer_nick.ToUpper().Contains(queryHash[4].ToString())));
                        tradeList = TradeService.GetTrade(funcTrade, pageNum, pageSize, out rowCount);
                    }
                }
                else
                {
                    //不是查询
                    if (nick == "所有店铺")
                    {
                        //加载所有店铺数据
                        tradeList = TradeService.GetTrade(c => c.status == TradeEnum.TRADE_FINISHED, pageNum, pageSize, out rowCount);
                    }
                    else
                    {
                        //加载指定店铺的数据
                        tradeList = TradeService.GetTrade(c => c.seller_nick == nick && c.status == TradeEnum.TRADE_FINISHED, pageNum, pageSize, out rowCount);
                    }
                }

                List<TradeRateInherit> tradeRateInheritList = new List<TradeRateInherit>();
                LoadAllTrade(tradeRateInheritList, tradeList);

                //加载TradeRate数据表中的数据
                gCtrlRated.DataSource = tradeRateInheritList;
                gViewAllTraded.BestFitColumns();
                #endregion
            }
            else
            {
                #region 中评和差评交易
                if (isQuery)
                {
                    //查询数据
                    if (nick == "所有店铺")
                    {
                        //加载所有店铺数据
                        func = new Func<Alading.Entity.TradeRate, bool>(c => c.result == "neutral" || c.result == "good"
                            && (c.tid.ToUpper().Contains(queryHash[5].ToString()) || c.rated_nick.ToUpper().Contains(queryHash[5].ToString())
                            || c.rated_nick.ToUpper().Contains(queryHash[5].ToString())));
                    }
                    else
                    {
                        //加载指定店铺的数据
                        func = new Func<Alading.Entity.TradeRate, bool>(c => (c.result == "neutral" || c.result == "good") && (c.nick == nick || c.rated_nick == nick)
                        && (c.tid.ToUpper().Contains(queryHash[5].ToString()) || c.rated_nick.ToUpper().Contains(queryHash[5].ToString())
                        || c.rated_nick.ToUpper().Contains(queryHash[5].ToString())));
                    }

                }
                else
                {
                    //不是查询
                    if (nick == "所有店铺")
                    {
                        //加载所有店铺数据
                        func = new Func<Alading.Entity.TradeRate, bool>(c => c.result == "neutral" || c.result == "good");
                    }
                    else
                    {
                        //加载指定店铺的数据
                        func = new Func<Alading.Entity.TradeRate, bool>(c => (c.result == "neutral" || c.result == "good") && (c.nick == nick || c.rated_nick == nick));
                    }
                }
                tradeRateList = TradeRateService.GetTradeRate(func, pageNum, pageSize, out rowCount);

                //加载TradeRate数据表中的数据
                gCtrlRated.DataSource = tradeRateList;
                gViewOtherRated.BestFitColumns();
                #endregion
            }
            #endregion
        }

        #endregion

        #region 分页方法
        /// <summary>
        /// 加载首页数据的方法
        /// </summary>
        /// <param name="result"></param>
        /// <param name="role"></param>
        public void FirstPage(GridControl gcRatedTrade, BarEditItem barEdit, RepositoryItemComboBox ComboBoxPage, string role
            , BarButtonItem firstPage, BarButtonItem forwardPage, BarButtonItem nextPage, BarButtonItem LastPage)
        {
            rowCount = 0;

            //获取数据
            GetData(role, 1, gcRatedTrade);

            //改变当前页码
            barEdit.EditValue = ComboBoxPage.Items[0];
            //当前页数
            ComboBoxPage.Tag = 1;
            firstPage.Enabled = false;
            forwardPage.Enabled = false;
            nextPage.Enabled = true;
            LastPage.Enabled = true;
        }

        /// <summary>
        /// 加载上一页数据的方法
        /// </summary>
        /// <param name="result"></param>
        /// <param name="role"></param>
        /// <param name="barEditPage"></param>
        public void ForwardPage(GridControl gcRatedTrade, BarEditItem barEdit, RepositoryItemComboBox ComboBoxPage, string role
            , BarButtonItem firstPage, BarButtonItem forwardPage, BarButtonItem nextPage, BarButtonItem LastPage)
        {
            rowCount = 0;

            //获取数据
            GetData(role, int.Parse(ComboBoxPage.Tag.ToString()) - 1,gcRatedTrade);

            //改变当前页码
            if (int.Parse(ComboBoxPage.Tag.ToString()) - 2 >= 0)
            {
                barEdit.EditValue = ComboBoxPage.Items[int.Parse(ComboBoxPage.Tag.ToString()) - 2];
            }
            //当前页数
            ComboBoxPage.Tag = int.Parse(ComboBoxPage.Tag.ToString()) - 1;

            if (int.Parse(ComboBoxPage.Tag.ToString()) == 1)
            {
                firstPage.Enabled = false;
                forwardPage.Enabled = false;
                nextPage.Enabled = true;
                LastPage.Enabled = true;
            }
            if (int.Parse(ComboBoxPage.Tag.ToString()) == 1)
            {
                firstPage.Enabled = true;
                forwardPage.Enabled = true;
                nextPage.Enabled = true;
                LastPage.Enabled = true;
            }
        }

        /// <summary>
        /// 加载下一页数据的方法
        /// </summary>
        /// <param name="result"></param>
        /// <param name="role"></param>
        /// <param name="barEditPage"></param>
        public void NextPage(GridControl gcRatedTrade, BarEditItem barEdit, RepositoryItemComboBox ComboBoxPage, string role
             , BarButtonItem firstPage, BarButtonItem forwardPage, BarButtonItem nextPage, BarButtonItem LastPage)
        {
            rowCount = 0;

            //获取数据
            GetData(role, int.Parse(ComboBoxPage.Tag.ToString()) + 1,gcRatedTrade);

            //改变当前页码
            if (int.Parse(ComboBoxPage.Tag.ToString()) < ComboBoxPage.Items.Count)
            {
                barEdit.EditValue = ComboBoxPage.Items[int.Parse(ComboBoxPage.Tag.ToString())];
            }
            //当前页数
            ComboBoxPage.Tag = int.Parse(ComboBoxPage.Tag.ToString()) + 1;

            if (int.Parse(ComboBoxPage.Tag.ToString()) == ComboBoxPage.Items.Count)
            {
                firstPage.Enabled = true;
                forwardPage.Enabled = true;
                nextPage.Enabled = false;
                LastPage.Enabled = false;
            }
            else
            {
                firstPage.Enabled = true;
                forwardPage.Enabled = true;
                nextPage.Enabled = true;
                LastPage.Enabled = true;
            }
        }

        /// <summary>
        /// 加载尾页数据的方法
        /// </summary>
        /// <param name="result"></param>
        /// <param name="role"></param>
        /// <param name="barEditPage"></param>
        public void LastPage(GridControl gcRatedTrade, BarEditItem barEdit, RepositoryItemComboBox ComboBoxPage, string role
            , BarButtonItem firstPage, BarButtonItem forwardPage, BarButtonItem nextPage, BarButtonItem LastPage)
        {
            rowCount = 0;

            //获取数据
            GetData(role, ComboBoxPage.Items.Count,gcRatedTrade);

            //改变当前页码
            if (int.Parse(ComboBoxPage.Tag.ToString()) < ComboBoxPage.Items.Count)
            {
                barEdit.EditValue = ComboBoxPage.Items[ComboBoxPage.Items.Count - 1];
            }
            //当前页数
            ComboBoxPage.Tag = ComboBoxPage.Items.Count;

            firstPage.Enabled = true;
            forwardPage.Enabled = true;
            nextPage.Enabled = false;
            LastPage.Enabled = false;
        }

        /// <summary>
        /// 跳转的方法
        /// </summary>
        /// <param name="result"></param>
        /// <param name="role"></param>
        /// <param name="barEditPage"></param>
        public void SkipPage(GridControl gcRatedTrade, int page, RepositoryItemComboBox ComboBoxPage, string role
            , BarButtonItem firstPage, BarButtonItem forwardPage, BarButtonItem nextPage, BarButtonItem LastPage)
        {
            rowCount = 0;

            //获取数据
            GetData(role, page,gcRatedTrade);

            //当前页数
            ComboBoxPage.Tag = page;

            if (page == 1)
            {
                firstPage.Enabled = false;
                forwardPage.Enabled = false;
                nextPage.Enabled = true;
                LastPage.Enabled = true;
            }
            else if (page == ComboBoxPage.Items.Count)
            {
                firstPage.Enabled = true;
                forwardPage.Enabled = true;
                nextPage.Enabled = false;
                LastPage.Enabled = false;
            }
            else
            {
                firstPage.Enabled = true;
                forwardPage.Enabled = true;
                nextPage.Enabled = true;
                LastPage.Enabled = true;
            }
        }

        /// <summary>
        /// 加载转到页
        /// </summary>
        /// <param name="comboEdit"></param>
        /// <param name="num"></param>
        public void SetComboBoxValue(BarEditItem barEdit, RepositoryItemComboBox comboEdit, int num)
        {
            comboEdit.Items.Clear();
            for (int page = 1; page <= num; page++)
            {
                comboEdit.Properties.Items.Add(string.Format("第{0}页", page));
            }
        }
        #endregion
    }
}

#region 自定义类
/// <summary>
/// 创建一个类，用于传递评价API：taobao.TradeRate.List.Add的参数
/// </summary>
public class TopTradeRate
{
    /// <summary>
    /// 自定义交易编号
    /// </summary>
    public string CustomTid { get; set; }

    /// <summary>
    /// 交易编号
    /// </summary>
    public string tid { get; set; }

    /// <summary>
    /// 评价内容
    /// </summary>
    public string content { get; set; }

    /// <summary>
    /// 评价结果。可选值:good(好评),neutral(中评),bad(差评) 
    /// </summary>
    public string result { get; set; }

    /// <summary>
    /// 是否匿名，卖家评不能匿名 false(非匿名)。
    /// </summary>
    public string anony { get; set; }

    /// <summary>
    ///评价者角色。可选值:seller(卖家),buyer(买家)
    /// </summary>
    public string role { get; set; }

    /// <summary>
    ///评价者淘宝昵称
    /// </summary>
    public string seller_nick { get; set; }

    /// <summary>
    ///sessionkey
    /// </summary>
    public string sessionkey { get; set; }
}

/// <summary>
/// 交易和评价表的结合
/// </summary>
public class TradeRateInherit
{
    /// <summary>
    /// 交易编号
    /// </summary>
    public string tid { get; set; }

    /// <summary>
    /// 评价者昵称
    /// </summary>
    public string buyer_nick { get; set; }

    /// <summary>
    /// 被评价者昵称
    /// </summary>
    public string seller_nick { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    public double payment { get; set; }

    /// <summary>
    ///卖家是否已评
    /// </summary>
    public bool seller_rate { get; set; }

    /// <summary>
    ///买家是否已评
    /// </summary>
    public bool buyer_rate { get; set; }

    /// <summary>
    ///商品名称
    /// </summary>
    public string item_title { get; set; }

    /// <summary>
    ///商品销售价
    /// </summary>
    public double item_price { get; set; }
}
#endregion