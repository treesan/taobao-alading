using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using Alading.Forms.Trade;
using Alading.Taobao.API;
using Alading.Taobao.Entity.Extend;
using Alading.Business;
using System.Reflection;
using Alading.Core.Enum;
using Alading.Entity;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DevExpress.XtraGauges.Win.Base;
using System.Globalization;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;
using Alading.Utils;
using Alading.Forms.Trade.Forms;
using Alading.Taobao;
using System.Diagnostics;
using DevExpress.Utils;
using Alading.Properties;
using System.IO;

namespace Alading.Forms.Trade.Controls
{
    [ToolboxItem(false)]
    public partial class TradePay : DevExpress.XtraEditors.XtraUserControl
    {
        public TradePay()
        {
            InitializeComponent();
            /*手动绑定事件*/
            navBarAllTrade.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBarAllTrade_LinkClicked);
            navBarHistory.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBarHistory_LinkClicked);
            navBarPaid.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBarPaid_LinkClicked);
            navBarRefund.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBarRefund_LinkClicked);
            navBarUnpaid.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBarUnpaid_LinkClicked);
            //将界面指向已付款订单 并默认为待确认订单    
            tradeTabMain.SelectedTabPage = tabWaitConfirm;
            navBarPaid_LinkClicked(null,new EventArgs());
        }


        #region 待确认右键菜单
        private void gridViewConfirm_ShowGridMenu(object sender, DevExpress.XtraGrid.Views.Grid.GridMenuEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (gridView != null)
            {
                GridHitInfo hi = gridView.CalcHitInfo(e.Point);
                //如果为新加行，返回 
                if (hi.RowHandle == GridControl.NewItemRowHandle)
                {
                    return;
                }
                //GridView上右击不会设置FocusedRow，手动设置 
                if (hi.RowHandle >= 0)
                {
                    gridView.FocusedRowHandle = hi.RowHandle;
                }
                //在右击的地方为：数据行，或者 行指示处时，弹出上下文菜单 
                if ((hi.HitTest == GridHitTest.RowCell || hi.HitTest == GridHitTest.RowIndicator))
                {
                    //this.popupMenuConfirm.ShowPopup(Control.MousePosition);
                }
            }
        }
        #endregion

        #region 新建订单
        private void barBtnTradeAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Alading.Forms.Trade.Forms.TradeAdd frm = new Alading.Forms.Trade.Forms.TradeAdd();
            frm.ShowDialog();
            InitSelectTab();
        }
        #endregion

        #region 界面操作ToolBox事件绑定

        /// 添加交易
        private void barCreateTrade_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new TradeAdd().ShowDialog();
        }

        /// 新加一条操作流程数据到当前选中交易
        private void btnOperateMsg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(_currentTradeRow==null)
            {
                XtraMessageBox.Show("请选中一条交易！");
                return;
            }
            Alading.Forms.Trade.Forms.TradeOperateDaily tradeOperateDaily =
                new Alading.Forms.Trade.Forms.TradeOperateDaily(_currentTradeRow["CustomTid"].ToString());
            tradeOperateDaily.ShowDialog();
        }

        /// 搜索按钮
        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        /// 导出按钮
        private void btnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(_currentTradeRow==null)
            {
                XtraMessageBox.Show("当前状态无任何数据！");
                return;
            }
            new TradeExportExcel(_currentDataSet).ExportExcel();
            InitSelectTab();
        }

        /// 订单锁定
        private void Lock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow row = null;
            /*用于判断点击锁定按钮前是否选定*/
            int flag = 0;
            List<Alading.Entity.Trade> tradeList = new List<Alading.Entity.Trade>();
            /*存放处理的行号*/
            List<int> rowHandle = new List<int>();
            for (int handle = 0; handle < GetCurrentTradeGV().RowCount; handle++)
            {
                row = gvTradeWaitConfirm.GetDataRow(handle);
                if (Convert.ToBoolean(row["IsSelected"]))
                {
                    if (row["LockedUserName"].ToString() == null || row["LockedUserName"].ToString() == string.Empty || row["LockedUserName"].ToString() == "未锁定")
                    {
                        Alading.Entity.Trade trade = TradeService.GetTrade(row["CustomTid"].ToString());
                        trade.LockedUserName = SystemHelper.User.nick;
                        trade.LockedUserCode = SystemHelper.User.UserCode;
                        trade.LockedTime = System.DateTime.Now;
                        tradeList.Add(trade);
                        rowHandle.Add(handle);
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("'" + row["receiver_name"] + "' 等收货人的交易" + SystemHelper.User.nick +"锁定，不能再锁定！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                    flag++;
            }
            /*判断是否点击锁定前有无交易选中*/
            if (flag >= gvTradeWaitConfirm.RowCount)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("没有选择需要锁定的交易，请检查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            /*修改数据成功则修改界面上的值*/

            if (DialogResult.OK == DevExpress.XtraEditors.XtraMessageBox.Show("确定要锁定所选交易？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            {
                if (TradeService.UpdateTrade(tradeList) == ReturnType.Success)
                {
                    foreach (int handle in rowHandle)
                    {
                        row = gvTradeWaitConfirm.GetDataRow(handle);
                        row["IsSelected"] = false;
                        row["LockedUserName"] = SystemHelper.User.nick;
                    }
                }
                // 添加一条添加赠品流程信息到交易流程
                Alading.Utils.SystemHelper.CreateFlowMessage(row["CustomTid"].ToString(), "交易锁定", "交易锁定:" + row["title"].ToString() + "锁单时间:" + row["LockedTime"].ToString(), "交易锁定");
            }
        }

        /// 解锁
        private void unlock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int flag = 0;
            DataRow row = null;
            List<int> rowHandleList = new List<int>();
            Alading.Entity.Trade tradeObj = new Alading.Entity.Trade();
            List<Alading.Entity.Trade> tradeList = new List<Alading.Entity.Trade>();

            for (int i = 0; i < gvTradeWaitConfirm.RowCount; i++)
            {
                row = gvTradeWaitConfirm.GetDataRow(i);
                if (row == null)
                    return;
                if (Convert.ToBoolean(row["IsSelected"]) == true)
                {
                    /*对没有被锁定的交易而进行解锁（此类错误）的处理*/
                    if (row["LockedUserName"].ToString() == UNLOCKED.VALUE)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("有订单没有被锁定，不能解锁，请检查！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    /*判断所勾选的是不是由系统客户锁定*/
                    if (row["LockedUserName"].ToString() != SystemHelper.User.nick)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("'" + row["receiver_name"] + "' 等收货人的交易" + " 不是被您锁定，不能解锁！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        /*记录被处理的行*/
                        rowHandleList.Add(i);
                    }
                }
                else
                    /*用于判定是否有被选择的交易*/
                    flag++;
            }
            /* 没有勾选交易   对其进行解锁  错误处理代码*/
            if (flag >= gvTradeWaitConfirm.RowCount)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("没有选择需要解锁的交易，请检查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            /*对勾选的交易进行解锁处理代码*/
            foreach (int rowHandle in rowHandleList)
            {
                row = gvTradeWaitConfirm.GetDataRow(rowHandle);
                tradeObj = TradeService.GetTrade(row["CustomTid"].ToString());
                tradeObj.LockedUserName = UNLOCKED.VALUE;
                tradeList.Add(tradeObj);
                tradeObj = null;
            }
            if (DialogResult.OK == DevExpress.XtraEditors.XtraMessageBox.Show("确定要将所选交易解锁？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            {
                if (TradeService.UpdateTrade(tradeList) == ReturnType.Success)
                {
                    foreach (int rowhandle in rowHandleList)
                    {
                        row = gvTradeWaitConfirm.GetDataRow(rowhandle);
                        row["LockedUserName"] = UNLOCKED.VALUE;
                        row["IsSelected"] = false;
                    }
                }
                // 添加一条添加赠品流程信息到交易流程
                Alading.Utils.SystemHelper.CreateFlowMessage(row["CustomTid"].ToString(), "交易解锁", "交易解锁:" + row["title"].ToString(), "交易解锁");
            }
        }

        /// 刷新界面
        private void FlushOrder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitSelectTab();
        }

        /// 保存交易信息
        private void btnStoreReceiverMsg_Click(object sender, EventArgs e)
        {
            if(_currentTradeRow==null)
            {
                XtraMessageBox.Show("请选中一条交易！");
                return;
            }
            Alading.Entity.Trade focusedTrade = TradeService.GetTrade(_currentTradeRow["CustomTid"].ToString());
            //算法辅助量 默认Yes。如果时间戳不相等，弹出YesNoDialog，由此可决定是否更新
            DialogResult result = DialogResult.Yes;

            #region 利用时间戳来防止并发
            //调用辅助函数来比较时间戳是否相等
            if (!Alading.Utils.SystemHelper.CompareTimeStamp(_currentTradeRow["TradeTimeStamp"] as byte[], focusedTrade.TradeTimeStamp))
            {
                //如果时间戳不相等，则执行此代码段
                result = XtraMessageBox.Show("该交易的已被修改，是否继续修改(YES)/查看流程信息(NO)", "确认修改", MessageBoxButtons.YesNo);
            }
            #endregion

            #region 根据result的值更新数据库

            if (result == DialogResult.Yes)
            {
                #region 构造一条交易数据修改信息
                StringBuilder builder = new StringBuilder();
                try
                {
                    if (focusedTrade.receiver_name != txtBuyerName.Text)
                        builder.Append("  收获人姓名：" + focusedTrade.receiver_name + "->" + txtBuyerName.Text);
                    if (focusedTrade.receiver_mobile != txtMobile.Text)
                        builder.Append("  收货人电话：" + focusedTrade.receiver_mobile + "->" + txtMobile.Text);
                    if (focusedTrade.receiver_zip != txtZip.Text)
                        builder.Append("  收货人邮编：" + txtZip.Text + "->" + focusedTrade.receiver_zip);
                    if (focusedTrade.receiver_phone != txtPhone.Text)
                        builder.Append("  收货人座机：" + focusedTrade.receiver_phone + "->" + txtPhone.Text);
                    if (focusedTrade.LogisticCompanyCode != cmbShippingCompany.EditValue.ToString())
                        builder.Append("  收货人物流公司：" +
                              LogisticCompanyService.GetLogisticCompany(p => p.code == focusedTrade.LogisticCompanyCode).FirstOrDefault().name +
                            "->" + cmbShippingCompany.Text);
                    if (focusedTrade.receiver_address != txtReceiverAddress.Text)
                        builder.Append("  收货人地址：" + focusedTrade.receiver_address + "->" + txtReceiverAddress.Text);
                    if (focusedTrade.receiver_state != cmbReceiverState.SelectedItem.ToString())
                        builder.Append("  收货人省：" + focusedTrade.receiver_state + "->" + cmbReceiverState.SelectedItem.ToString());
                    if (focusedTrade.receiver_city != cmbReceiverCity.SelectedItem.ToString())
                        builder.Append("  收货人市：" + focusedTrade.receiver_city + "->" + cmbReceiverCity.SelectedItem.ToString());
                    if (focusedTrade.receiver_district != cmbReceiverDistrict.SelectedItem.ToString())
                        builder.Append("  收货人地区：" + focusedTrade.receiver_district + "->" + cmbReceiverDistrict.SelectedItem.ToString());
                    if ((focusedTrade.buyer_memo == null ? string.Empty : focusedTrade.buyer_memo) != memoBuyerMemo.Text)
                        builder.Append("  收货人备注：" + focusedTrade.buyer_memo + "->" + memoBuyerMemo.Text);
                    if ((focusedTrade.buyer_message == null ? string.Empty : focusedTrade.buyer_message) != memoBuyerMessage.Text)
                        builder.Append("  收货人信息：" + focusedTrade.buyer_message + "->" + memoBuyerMessage.Text);
                }
                catch (System.Exception ex)
                {

                }

                #endregion

                #region 从界面读取修改信息
                focusedTrade.receiver_name = txtBuyerName.Text;
                focusedTrade.receiver_mobile = txtMobile.Text;
                focusedTrade.receiver_zip = txtZip.Text;
                focusedTrade.receiver_phone = txtPhone.Text;
                focusedTrade.LogisticCompanyCode = cmbShippingCompany.EditValue.ToString();
                focusedTrade.TemplateCode = cmbShippingTemplate.EditValue.ToString();
                focusedTrade.receiver_address = txtReceiverAddress.Text;
                focusedTrade.receiver_state = cmbReceiverState.SelectedItem.ToString();
                focusedTrade.receiver_city = cmbReceiverCity.SelectedItem.ToString();
                focusedTrade.receiver_district = cmbReceiverDistrict.SelectedItem.ToString();
                focusedTrade.buyer_memo = memoBuyerMemo.Text;
                focusedTrade.buyer_message = memoBuyerMessage.Text;
                focusedTrade.HasInvoice = false;
                #endregion
               
                WaitDialogForm wdf = new WaitDialogForm(Constants.OPERATE_TBDB_DATA);
                try
                { 
                    //将修改数据更新到淘宝
                    UpdateTaobaoTradeAddressMessage(focusedTrade);
                    //更新修改数据到数据库
                    TradeService.UpdateTrade(focusedTrade);
                    //创建一条流水信息到数据库
                    Alading.Utils.SystemHelper.CreateFlowMessage(focusedTrade.CustomTid, "收货信息修改", builder.ToString(), "订单管理");
                    wdf.Close();
                    XtraMessageBox.Show("修改收货信息成功！");
                }
                catch(Exception ex)
                {
                    wdf.Close();
                    XtraMessageBox.Show("修改收货保存到淘宝失败，修改无效！原因:"+ex.Message);
                }

                //将更新信息同步到界面
                _currentTradeRow["receiver_name"] = txtBuyerName.Text;
                //时间由系统自动生成，需将本地时间戳与数据库最新同步
                _currentTradeRow["TradeTimeStamp"] = TradeService.GetTrade(focusedTrade.CustomTid).TradeTimeStamp;
            }
            else
            {
                tabsTradeDetail.SelectedTabPage = detailTabFlowMsg;
            }
            #endregion

        }

        /// 拆分订单
        private void btnSplit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region 数据声明
            /*用于判断是否有交易勾选*/
            int flag = 0;
            /*用于记录被选择拆分的交易*/
            int rowRecord = 0;
            /*将界面上数据取下处理*/
            DataRow row = null;
            #endregion

            #region /*一次只能拆分一笔交易*/
            for (int rowHandle = 0; rowHandle < GetCurrentTradeGV().RowCount; rowHandle++)
            {
                row = GetCurrentTradeGV().GetDataRow(rowHandle);
                if (row == null)
                    return;
                if (Convert.ToBoolean(row["IsSelected"].ToString()) == true)
                    flag++;
            }
            if (flag >= 2)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("一次只能拆分1笔交易，您勾选的交易量大于1，请检查！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (flag == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("没有勾选所要拆分的交易，请检查！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            #region /*找到所要拆分的交易*/
            for (int rowHandle = 0; rowHandle < GetCurrentTradeGV().RowCount; rowHandle++)
            {
                row = GetCurrentTradeGV().GetDataRow(rowHandle);
                if (row == null)
                {
                    return;
                }
                if (Convert.ToBoolean(row["IsSelected"]) == true)
                {
                    if (Convert.ToBoolean(row["IsSplited"].ToString()))
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("您选的交易已拆，不能再拆分，请检查！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    #region /*拆分处理*/
                    // 拆分的不是合并单
                    if (row != null && !Convert.ToBoolean(row["IsCombined"].ToString()))
                    {
                        TradeSplit tradeSplit = new TradeSplit(row["CustomTid"].ToString());
                        tradeSplit.ShowDialog();

                        //初始化界面
                        InitSelectTab();
                    }
                    else if (row != null && Convert.ToBoolean(row["IsCombined"].ToString()))/*拆分合并单*/
                    {
                        //找到子交易
                        if (View_TradeStockService.Update_TradeOrder(row["CustomTid"].ToString()) == ReturnType.Success)
                        {
                            InitSelectTab();
                            XtraMessageBox.Show("拆分成功!", "提示");
                        }
                    }
                    #endregion
                }
            }
            #endregion
        }

        /// 添加赠品时间响应处理
        private void barAddGifts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_currentTradeRow!=null)
            {
                new GiftGiven(_currentTradeRow["CustomTid"].ToString()).ShowDialog();
                InitSelectTab();
            }
            else
            {
                XtraMessageBox.Show("当前无选中交易！");
            }
        }

        /// 合并订单
        private void btnCombineTrade_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region 变量定义
            List<int> rowHandleList = new List<int>();
            /*用于记录父子交易关系的tid*/
            List<string> tidList = new List<string>();
            List<string> CustomTidList = new List<string>();
            /*用于记录所选的交易*/
            List<Alading.Entity.Trade> tradeList = new List<Alading.Entity.Trade>();
            DataRow rowData = null;
            #endregion

            #region 合并准备
            #region /*记录被打勾的项*/
            for (int row = 0; row < gvTradeWaitConfirm.RowCount; row++)
            {
                rowData = gvTradeWaitConfirm.GetDataRow(row);

                if (rowData == null)
                {
                    return;
                }//if

                if (Convert.ToBoolean(rowData["IsSelected"].ToString()))
                {
                    /*添加*/
                    rowHandleList.Add(row);
                }//if

                rowData = null;
            }//for
            /*没有勾选交易不能合并*/
            if (rowHandleList.Count <= 0)
            {
                XtraMessageBox.Show("没有勾选需要合并的交易，请检查！", "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }//if

            /*一个交易不能合并*/
            if (rowHandleList.Count == 1)
            {
                XtraMessageBox.Show("只勾选了一个交易，不能合并，请检查！", "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }//if
            #endregion

            #region  /*判断被选中交易的合法性*/
            rowData = gvTradeWaitConfirm.GetDataRow(rowHandleList[0]);
            string buyer_nick = rowData["buyer_nick"].ToString();
            string LastReceiverName = rowData["receiver_name"].ToString();
            string LastReceiverZip = rowData["receiver_zip"].ToString();
            /*用于标识是否是合并 并且提交打印的交易*/
            bool ConbinePrintFlg = false;
            /*用于标识是否是合并还原为一个交易的交易*/
            bool ConbineTradeFlg = false;
            int SplitFflag = 0;

            foreach (int record in rowHandleList)
            {
                rowData = gvTradeWaitConfirm.GetDataRow(record);

                if (string.IsNullOrEmpty(rowData["RealPostFee"].ToString()))
                {
                    XtraMessageBox.Show("选中的交易中有交易实付邮费没有填写，不能合并，请检查！",
                                                                                           "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Convert.ToBoolean(rowData["IsCombined"].ToString()))
                {
                    XtraMessageBox.Show("选中的交易中有合并交易，不能在合并，请检查！",
                                                                                           "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                /*判断买家昵称是否一致*/
                if (rowData["buyer_nick"].ToString() != buyer_nick)
                {
                    XtraMessageBox.Show("选中的交易中，买家昵称不一致，不能合并，请检查！",
                                                                                           "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }//if

                /*判断收货人姓名是否一致*/
                if (rowData["receiver_name"].ToString() != LastReceiverName)
                {
                    XtraMessageBox.Show("选中的交易中，收货人姓名不一致，不能合并，请检查！",
                                                                                            "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }//if

                /*所选交易邮编一致性检验*/
                if (rowData["receiver_zip"].ToString() != LastReceiverZip)
                {
                    XtraMessageBox.Show("选中的交易中，收货人邮编不一致，不能合并，请检查！",
                                                                                            "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }//if
                tradeList.Add(TradeService.GetTrade(q => q.CustomTid == rowData["CustomTid"].ToString()).FirstOrDefault());
                CustomTidList.Add(rowData["CustomTid"].ToString());
                rowData = null;
            }/*foreach (int record in rowHandleList)*/

            /*检验拆分交易与非拆分交易合并的合法性*/
            foreach (Alading.Entity.Trade tradeObj in tradeList)
            {
                if (tradeObj.IsSplited == true)
                {
                    SplitFflag++;
                }
            }// for

            if (SplitFflag >= tradeList.Count)
            {
                /*勾选的  属于能够还原成一个交易的 交易*/
                ConbineTradeFlg = true;
            }
            else if (SplitFflag <= 0)
            {
                /*勾选的 属于不同的交易 能够合并成一个交易的交易*/
                ConbinePrintFlg = true;
            }
            else
            {
                XtraMessageBox.Show("选中的交易中有 拆分过的交易 和 没有拆分过的交易，混合交易不能合并，请检查！",
                                                                                        "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            #endregion

            #region  合并
            /*为还原成一个交易*/
            if (ConbineTradeFlg)
            {
                string tid = tradeList.First().tid;

                foreach (Alading.Entity.Trade tradeObj in tradeList)
                {
                    /*判断能否合并还原成一个交易*/
                    if (tid != tradeObj.tid)
                    {
                        XtraMessageBox.Show("收货人为：" + tradeObj.receiver_name.ToString()
                                                                                              + "的交易的交易id与其他所选交易不一致，不能合并，请检查！",
                                                                                            "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                /*合并操作*/
                CombineOrder data_present = new CombineOrder(CustomTidList, combineStyle.ConbineTradeFlg);
                data_present.ShowDialog();
            }

            /*不同交易合并成一个交易，并且提交打印*/
            if (ConbinePrintFlg)
            {
                /*合并操作*/
                CombineOrder data_present = new CombineOrder(CustomTidList, combineStyle.ConbinePrintFlg);
                data_present.ShowDialog();
            }
            InitSelectTab();
            #endregion
        }

        /// 添加赠品时间响应处理,WaitTradeConfirm界面专用
        private void giftAddOrDelete_Click(object sender, EventArgs e)
        {
            //开启订单界面
            new GiftGiven(_currentTradeRow["CustomTid"].ToString()).ShowDialog();
            InitSelectTab();//刷新界面  
        }

        /// 全选
        private void repositoryItemCheckEditSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            int rowCount = GetCurrentTradeGV().RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = GetCurrentTradeGV().GetDataRow(i);
                row["IsSelected"] = ((DevExpress.XtraEditors.CheckEdit)(sender)).Checked;
            }
        }

        private void gvTradeWaitConfirm_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            /*修改邮费*/
            GridView senderIn = sender as GridView;
            DataRow row = senderIn.GetDataRow(e.RowHandle);
            if (e.Column == RealPostFee)
            {
                Alading.Entity.Trade trade = TradeService.GetTrade(row["CustomTid"].ToString());
                trade.RealPostFee = float.Parse(row["RealPostFee"].ToString());
                TradeService.UpdateTrade(trade);
            }
        }
        #endregion
  
        #region 待确认及待打印之间的状态转换
        /// 待确认转化为待打印
        private void CommitWaitPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridView currentView = gvTradeWaitConfirm;//取得当前展示交易的GridView
            int totalRowCount = currentView.RowCount;//循环获取需要提交的Trade
            DataRow currentRow = null;
            

            WaitDialogForm waitFrm = new WaitDialogForm(Alading.Taobao.Constants.OPERATE_DB_DATA);

            for (int runner = 0; runner < totalRowCount; runner++)
            {
                currentRow = currentView.GetDataRow(runner);
                if (Boolean.Parse(currentRow["IsSelected"].ToString()))
                {
                    if (string.IsNullOrEmpty(currentRow["RealPostFee"].ToString()))
                    {
                        XtraMessageBox.Show("收货人为:" + currentRow["receiver_name"] + "的交易实际邮费没有填写,不能提交，请核查!", "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    string customTid = currentRow["CustomTid"].ToString();
                    string lockedUser=currentRow["LockedUserName"].ToString();
                    string currentUser = SystemHelper.User.nick;
                    if (currentRow["TradeIsLackProduct"].ToString() == LackProductOrNot.Normal)
                    {
                        #region  判断当前的操作人权限提交
                        if (lockedUser == UNLOCKED.VALUE || lockedUser == currentUser)
                        {
                               #region   调用存储过程提交打印，保持事务原子性
                            int returnValue = TradeService.SummitCommonTrade(customTid, LocalTradeStatus.HasNotSummit, LocalTradeStatus.SummitNotPrint,currentUser);
                            switch (returnValue)
                            {
                                case SummitReturnType.Success:
                                    SystemHelper.CreateFlowMessage(customTid, "提交打印成功", "交易提交打印成功", "订单管理");
                                    break;
                                case SummitReturnType.StatusChanged:
                                    SystemHelper.CreateFlowMessage(customTid, "提交打印失败", "交易状态已经改变", "订单管理");
                                    break;
                                case SummitReturnType.GoodsLacked:
                                    SystemHelper.CreateFlowMessage(customTid, "提交打印失败", "交易此时处于缺货状态", "订单管理");
                                    break;

                                case SummitReturnType.TemplateNeeded:
                                    SystemHelper.CreateFlowMessage(customTid, "提交打印失败", "交易还没有选择物流模板", "订单管理");
                                    break;
                            }

                            #endregion
                        }
                        else
                        {
                            SystemHelper.CreateFlowMessage(customTid, "提交权限不足", currentUser + " 提交订单，权限不足！", "订单管理");
                        }
#endregion
                    }
                }
            }

            waitFrm.Close();
            InitSelectTab();
        }

        /// 待打印转化为待确认
        private void BtnBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridView currentView = gvTradeWaitPrint;//取得当前展示交易的GridView
            int totalRowCount = currentView.RowCount;//循环获取需要提交的Trade
            DataRow currentRow = null;

            WaitDialogForm waitFrm = new WaitDialogForm(Alading.Taobao.Constants.OPERATE_DB_DATA);

            for (int runner = 0; runner < totalRowCount; runner++)
            {
                currentRow = currentView.GetDataRow(runner);
                
                if (Convert.ToBoolean(currentRow["IsSelected"].ToString()))
                {
                    string curCustomTid = currentRow["CustomTid"].ToString();

                    int returnValue = TradeService.RebackCommonTrade(curCustomTid, LocalTradeStatus.SummitNotPrint, LocalTradeStatus.HasNotSummit);
                        switch (returnValue)
                        {
                            case SummitReturnType.Success:
                                SystemHelper.CreateFlowMessage(curCustomTid, "取消打印成功", "交易取消打印成功", "订单管理");
                                break;
                            case SummitReturnType.StatusChanged:
                                SystemHelper.CreateFlowMessage(curCustomTid, "取消打印失败", "交易状态已经改变", "订单管理");
                                break;
                        }
                    }
                
            }

            waitFrm.Close();
            InitSelectTab();
        }

        ///// 合并单从待打印转到待确认
        //private void WaitPrintToWaitConfirmCombined(Alading.Entity.Trade _trade)
        //{
        //    string customTid = _trade.CustomTid;
        //    foreach (Alading.Entity.Trade trade in TradeService.GetTrade(p => p.CombineCode == customTid))
        //    {
        //        WaitPrintToWaitConfirm(trade);
        //    }
        //    TradeService.RemoveTrade(customTid);
        //    TradeOrderService.RemoveTradeOrder(p => p.CustomTid == customTid);
        //}

        ///// 普通单从待打印转到待确认
        //private void WaitPrintToWaitConfirm(Alading.Entity.Trade trade)
        //{
        //    List<View_TradeStock> orders = View_TradeStockService.GetView_TradeStock(p => p.CustomTid == trade.CustomTid);
        //    try
        //    {
        //        foreach (View_TradeStock order in orders)
        //        {
        //            StockProduct updateProduct = StockProductService.GetStockProduct(p => p.OuterID == order.OuterID && p.SkuProps_Str == order.SkuProps_Str).FirstOrDefault();
        //             if (updateProduct != null)
        //            {
        //                 if (order.num != 0)
        //                 {
        //                     updateProduct.OccupiedQuantity -= order.num;
        //                     StockProductService.UpdateStockProduct(updateProduct);
        //                 }
        //            }
        //        }
        //        trade.LocalStatus = LocalTradeStatus.HasNotSummit;
        //        trade.status = TradeEnum.WAIT_SELLER_SEND_GOODS;
        //        trade.CombineCode = string.Empty;
        //        TradeService.UpdateTrade(trade);
        //    }
        //    catch (System.Exception ex)
        //    {

        //    }
        //}
        #endregion 
        
        #region 待打印块
        private void gvTradeWaitPrint_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadTradeDetail();
        }

        /// <summary>
        /// 合并订单信息显示 子交易信息
        /// </summary>
        /// <param name="ViewTradeList"></param>
        private void LoadTradeDetail()
        {
            List<IGrouping<string, View_TradeStock>> trade_stock_group = new List<IGrouping<string, View_TradeStock>>();
            DataRow row = _currentTradeRow;
            /*如果有数据*/
            if (gvTradeWaitPrint.RowCount >= 0 && _currentTradeRow!=null)
            {
                if (Convert.ToBoolean(row["IsCombined"].ToString()))
                {
                    /*当点击合并项的时候才加载合并订单信息选项卡*/
                    detailCombineTradeMessage.PageVisible = true;
                    tabsTradeDetail.SelectedTabPage = detailCombineTradeMessage;
                    List<View_TradeStock> TradeStockList = View_TradeStockService.GetView_TradeStock(q => q.CombineCode == row["CombineCode"].ToString());
                    /*通过customtid  归类*/
                    trade_stock_group = TradeStockList.GroupBy(q => q.CustomTid).ToList();
                }
                else
                    detailCombineTradeMessage.PageVisible = false;
            }


            DataSet dataSet = new DataSet();
            //从XML文件读出暂存数据库表格框架 包含TradeList表和OrderList表 
            MemoryStream stream = new MemoryStream(Resources.TradeStockSchema);
            try
            {
                dataSet.ReadXmlSchema(stream);
            }
            finally
            {
               stream.Close();
            }
            #region 初始化数据
            foreach (IGrouping<string, View_TradeStock> tradeCur in trade_stock_group)
            {
                /* trade表初始化*/
                DataRow tradeRow = dataSet.Tables["TradeList"].NewRow();
                InitTradeRow(tradeRow, tradeCur);
                dataSet.Tables["TradeList"].Rows.Add(tradeRow);

                int productAccount = tradeCur.Where(o => o.OrderType == emumOrderType.SellGoods).Count(); //用于消除赠品的影响
                foreach (View_TradeStock orderCurrent in tradeCur)
                {
                    /* order表初始化  */
                    DataRow orderRow = dataSet.Tables["OrderList"].NewRow();
                    InitOrderRow(orderRow, orderCurrent);
                    dataSet.Tables["OrderList"].Rows.Add(orderRow);
                }
                // 数据绑定相关连操作
                gcCombineTrade.DataSource = dataSet.Tables["TradeList"];
                gcCombineTrade.ForceInitialize();//强制初始化
                gvParentCombineTrade.BestFitColumns();
                gcCombineTrade.LevelTree.Nodes.Add(Alading.Taobao.Constants.TRADE_ORDER_RELATION, gvChildCombineOrder);//建立联级绑定
            #endregion
            }
        }

        /// <summary>
        /// 初始化dataset中 table["TradeList"]的行
        /// </summary>
        /// <param name="tradeRow"></param>
        /// <param name="tradeCurList"></param>
        private void InitTradeRow(DataRow tradeRow, IGrouping<string, View_TradeStock> tradeCurList)
        {
            //View_TradeStock tradeCur = tradeCurList.FirstOrDefault();
            //tradeRow["CustomTid"] = tradeCur.CustomTid;
            //tradeRow["type"] = tradeCur.type;
            //tradeRow["tid"] = tradeCur.tid;
            //tradeRow["LastReceiverName"] = tradeCur.LastReceiverName;
            //tradeRow["created"] = tradeCur.created.ToString("yyyy-MM-dd");
            //tradeRow["buyer_nick"] = tradeCur.buyer_nick;
            //tradeRow["seller_nick"] = tradeCur.seller_nick;
            //tradeRow["buyer_email"] = tradeCur.buyer_email;
            //tradeRow["LastReceiverAddress"] = tradeCur.LastReceiverAddress;
            //tradeRow["LastReceiverState"] = tradeCur.LastReceiverState;
            //tradeRow["LastReceiverCity"] = tradeCur.LastReceiverCity;
            //tradeRow["LastReceiverDistrict"] = tradeCur.LastReceiverDistrict;
            //tradeRow["LastReceiverZip"] = tradeCur.LastReceiverZip;
            //tradeRow["post_fee"] = tradeCur.post_fee;
            //tradeRow["tradeTotalFee"] = tradeCur.tradeTotalFee;
            //tradeRow["EShopName"] = ShopService.GetShop(p => p.nick == tradeCur.seller_nick).FirstOrDefault().title ?? string.Empty;
            //tradeRow["EShopType"] = ShopService.GetShop(p => p.nick == tradeCur.seller_nick).FirstOrDefault().ShopTypeName ?? string.Empty;
            //tradeRow["LockTradeTime"] = tradeCur.LockTradeTime != null ? tradeCur.LockTradeTime.Value.ToString("yyyy-MM-dd") : string.Empty;
            //tradeRow["AlipayNo"] = tradeCur.alipay_no;
            //tradeRow["post_fee"] = tradeCur.post_fee;
            //2010.04.01
        }

        /// <summary>
        /// 将一条View_TradeStock的信息赋值到TradeRow
        /// </summary>
        /// <param name="orderRow"></param>
        /// <param name="orderCur"></param>
        /// <param name="orderAccount"></param>
        private void InitOrderRow(DataRow orderRow, View_TradeStock orderCur)
        {
            orderRow["tid"] = orderCur.tid;
            orderRow["iid"] = orderCur.iid;
            orderRow["OuterID"] = orderCur.OuterID;
            orderRow["TradeOrderCode"] = orderCur.TradeOrderCode;
            orderRow["oid"] = orderCur.oid;
            orderRow["CustomTid"] = orderCur.CustomTid;
            orderRow["ItemName"] = orderCur.ItemName;
            orderRow["sku_properties_name"] = orderCur.sku_properties_name;
            orderRow["LeftQuantity"] = orderCur.SkuQuantity - orderCur.OccupiedQuantity;
            orderRow["num"] = orderCur.num;
            orderRow["price"] = orderCur.price;
            orderRow["payment"] = orderCur.payment;
            orderRow["OrderType"] = orderCur.OrderType;
            //查询货物库存量
        }
        
        #endregion        
    }
}
        
