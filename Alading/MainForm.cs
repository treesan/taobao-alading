using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Alading.Forms.Stock;
using DevExpress.XtraEditors;
using System.Configuration;
using DevExpress.Utils;
using Alading.Forms.Item;
using Alading.Forms.Trade;
using Alading.Forms.Init;
using Alading.Taobao;
using Alading.Forms.Stock.InOut;
using Alading.Forms.Stock.Product;
using Alading.Forms.Stock.SettingUp;
using Alading.Forms.Consumer;
using Alading.Forms.Rate.Rate;
using Alading.Forms.Trade.Forms;
using Alading.Forms.PurchaseManager;
using Alading.Utils;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API;
using Alading.Business;
using System.Linq;
using System.Runtime.InteropServices;
using Alading.Forms.ReplenishmentOrder;
using Alading.Forms.Account;
using Alading.Forms.Supplier;
using Alading.Forms.Stock.Assemble;
using Alading.Forms;
using DevExpress.Skins;

namespace Alading
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region 全局变量
        private System.Timers.Timer autoListTimer = new System.Timers.Timer();
        private System.Timers.Timer autoRecommedTimer = new System.Timers.Timer();
        private System.Timers.Timer autoGetOrderTimer = new System.Timers.Timer();
        #endregion

        /// <summary>
        /// 是否设置系统初始化对话框
        /// </summary>
        public bool ShowInitSystemDialog { get; set; }
        public string Account { get; set; }

        #region 初始化
        public MainForm()
        {
            InitializeComponent();

            string systemName = ConfigurationManager.AppSettings["SystemName"] + ConfigurationManager.AppSettings["CurrentVersion"];
            /*设置软件名称*/
            this.Text = systemName;
            this.notifyIcon.Text = systemName;

            #region 桌面窗体
            Desktop frm = new Desktop();
            frm.MdiParent = this;
            frm.Text = "业务导航";
            frm.Show();
            #endregion
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            #region  版本发布 功能隐藏  Version2.0  2010-04-16-22：25
            //ribbonPageGroup3                     //经营管理/店铺
            //barBtnShop.Enabled = false;//店铺 
            //btnCon.Enabled = false; //宝贝
            //ribbonPageGroup3                      //经营管理/销售
            barBtnConsumer.Enabled = false;//客户
            barBtnOrder.Enabled = false;//订单
            //ribbonPageGroup7                    //经营管理/打印
            barBtnPrint.Enabled = false;   //打印
            //ribbonPageGroup1                    //经营管理/售后
            barBtnDelivery.Enabled = false;   //发货
            barBtnShip.Enabled = false;   //跟单
            barBtnRate.Enabled = false;   //评价
            //ribbonPageGroup4                     //经营管理/财务
            barBtnAccounts.Enabled = false;   //流水账 
            //ribbonPageGroup4                     //经营管理/报表
            barSubItem4.Enabled = false;   //统计 
            barSubItem5.Enabled = false;   //分析
            //ribbonPageGroup10                    //经营管理/系统
            barButtonItem11.Enabled = false;   //插件
            //iPaintStyle.Enabled = false;   //风格
            //barSubItem6.Enabled = false;   //帮助
            //barButtonItem52.Enabled = false;   //账号

            //ribbonPageGroup5                      //库存管理/采购
            barBtnSupplier.Enabled = false;   //供应商
            barBtnItemReplenish.Enabled = false;   //补货单 
            barButtonItem9.Enabled = false;   //采购单
            //ribbonPageGroup5                      //库存管理/库存
            //barBtnInitShop.Enabled = false;   //初始化 
            //barBtnStockProduct.Enabled = false;   //商店管理
            //barBtnProductList.Enabled = false;   //组合商品 
            //barBtnStockDetail.Enabled = false;   //出入库单
            //barBtnAllocation.Enabled = false;   //库存调拨
            //barSubItem2.Enabled = false;   //盘点
            //barSubItemStockSet.Enabled = false;   //库存设置

            //ribbonPageGroup13                     //系统管理/系统
            //barBtnInitSystem.Enabled = false;   //初始化
            barSubItem8.Enabled = false;   //数据库
            barButtonItem37.Enabled = false;   //配置
            barButtonItem53.Enabled = false;   //日志
            //ribbonPageGroup12                     //系统管理/员工
            barBtnStaff.Enabled = false;   //员工 
            barBtnPermission.Enabled = false;   //权限 
            barBtnAchievement.Enabled = false;   //业绩考核 
            //ribbonPageGroup6                     //系统管理/打印模板
            barBtnTemplate.Enabled = false;   //预览
            barBtnDesign.Enabled = false;   //设计
            //ribbonPageGroup9                      //系统管理/邮件
            barBtnEmailManage.Enabled = false;   //邮件
            barBtnEmailTemp.Enabled = false;   //邮箱 
            //ribbonPageGroup2                      //系统管理/报表

            #endregion

            #region 注册Timer事件
            //this.autoListTimer.Interval = double.Parse(ConfigurationManager.AppSettings["TimerListInterval"]);//设置间隔时间
            this.autoListTimer.Interval = SystemHelper.THREAD_LIST_INTERVAL;
            this.autoListTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
            this.autoListTimer.Elapsed += new System.Timers.ElapsedEventHandler(autoListTimer_Elapsed);

            //this.autoRecommedTimer.Interval = double.Parse(ConfigurationManager.AppSettings["TimerRecommendInterval"]);
            this.autoRecommedTimer.Interval = SystemHelper.THREAD_RECOMMEND_INTERVAL;
            this.autoRecommedTimer.AutoReset = true;
            this.autoRecommedTimer.Elapsed += new System.Timers.ElapsedEventHandler(autoRecommedTimer_Elapsed);

            this.autoGetOrderTimer.Interval = double.Parse(ConfigurationManager.AppSettings["TimerGetOrder"]);
            this.autoGetOrderTimer.AutoReset = true;
            this.autoGetOrderTimer.Elapsed += new System.Timers.ElapsedEventHandler(autoGetOrderTimer_Elapsed);
            #endregion

            #region 显示帐号名称
            barStaticItemUser.Caption += Account;
            #endregion

            #region 显示系统初始化对话框
            if (ShowInitSystemDialog)
            {
                try
                {
                    barBtnInitSystem_ItemClick(this, null);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion

            #region  风格
            foreach (DevExpress.Skins.SkinContainer skin in SkinManager.Default.Skins)
            {
                BarCheckItem item = ribbon.Items.CreateCheckItem(skin.SkinName, false);
                item.Tag = skin.SkinName;
                item.ItemClick += new ItemClickEventHandler(item_ItemClick);
                iPaintStyle.ItemLinks.Add(item);
            }
            #endregion
        }

        void item_ItemClick(object sender, ItemClickEventArgs e)
        {
            defaultLookAndFeel.LookAndFeel.SkinName = e.Item.Tag.ToString();
        }
        private void iPaintStyle_Popup(object sender, System.EventArgs e)
        {
            foreach (BarItemLink link in iPaintStyle.ItemLinks)
                ((BarCheckItem)link.Item).Checked = link.Item.Caption == defaultLookAndFeel.LookAndFeel.ActiveSkinName;
        }
        #endregion

        #region 功能选择
        private bool SelectedTab(string text)
        {
            foreach (System.Windows.Forms.Form frm in this.MdiChildren)
            {
                if (frm.Text == text)
                {
                    frm.Activate();
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region 初始化店铺
        private void barBtnInitShop_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("初始化店铺"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    InitShopStock frm = new InitShopStock();
                    frm.MdiParent = this;
                    frm.Text = "初始化店铺";
                    frm.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //InitShop frm = new InitShop();
            //frm.ShowDialog();
        }
        #endregion

        #region 宝贝管理
        private void btnUpdateItems_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Alading.Forms.Item.ItemsDown downForm = new Alading.Forms.Item.ItemsDown();
                downForm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barBtnItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("宝贝管理"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    ItemManage frm = new ItemManage();
                    frm.MdiParent = this;
                    frm.Text = "宝贝管理";
                    frm.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAverageList_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("均匀上架"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    ItemAverageList frm = new ItemAverageList();
                    frm.MdiParent = this;
                    frm.Text = "均匀上架";
                    frm.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAuto_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemAutoSet frm = new ItemAutoSet(this.autoListTimer, this.autoRecommedTimer);
            frm.Show();
        }

        void autoListTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!backWorkerList.IsBusy)
            {
                this.backWorkerList.DoWork += new DoWorkEventHandler(backWorkerList_DoWork);
                this.backWorkerList.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backWorkerList_RunWorkerCompleted);
                this.backWorkerList.RunWorkerAsync();
            }
        }

        void backWorkerList_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Taobao.Entity.Item> itemlist = DownNeedAutoListItems();
            foreach (Taobao.Entity.Item item in itemlist)
            {
                try
                {
                    if (!this.autoListTimer.Enabled)
                    {
                        return;
                    }
                    string session = SystemHelper.GetSessionKey(item.Nick);
                    TopService.ItemUpdateListing(session, item.Iid);
                    if (item.HasShowcase)
                    {
                        TopService.ItemRecommendDelete(session, item.Iid);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        void backWorkerList_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        void autoRecommedTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!backWorkerRecommend.IsBusy)
            {
                this.backWorkerRecommend.DoWork += new DoWorkEventHandler(backWorkerRecommend_DoWork);
                this.backWorkerRecommend.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backWorkerRecommend_RunWorkerCompleted);
                this.backWorkerRecommend.RunWorkerAsync();
            }
        }

        void backWorkerRecommend_DoWork(object sender, DoWorkEventArgs e)
        {
            ShopRsp shoprsp = new ShopRsp();
            foreach (Alading.Entity.Shop shop in SystemHelper.ShopList)
            {
                try
                {
                    if (!this.autoRecommedTimer.Enabled)
                    {
                        return;
                    }
                    string session = SystemHelper.GetSessionKey(shop.nick);
                    shoprsp = TopService.ShopRemainShowCaseGet(session);
                    int remainShowCase = 0;
                    if (shoprsp.Shop != null)
                    {
                        remainShowCase = shoprsp.Shop.RemainShowcase;
                    }
                    List<Taobao.Entity.Item> itemlist = DownOnsaleItems(shop.nick);
                    for (int i = 0; i < remainShowCase && i < itemlist.Count; i++)
                    {
                        if (!this.autoRecommedTimer.Enabled)
                        {
                            return;
                        }
                        string iid = itemlist[i].Iid;
                        if (!string.IsNullOrEmpty(iid))
                        {
                            ItemRsp itemrsp = TopService.ItemRecommendAdd(session, iid);
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        void backWorkerRecommend_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        /// <summary>
        /// 下载所有店铺中需要上架的宝贝,只能返回部分重要字段
        /// </summary>
        private List<Taobao.Entity.Item> DownNeedAutoListItems()
        {
            List<Taobao.Entity.Item> itemlist = new List<Taobao.Entity.Item>();
            foreach (Alading.Entity.Shop shop in SystemHelper.ShopList)
            {
                try
                {
                    //所有仓库中的宝贝
                    List<Taobao.Entity.Item> forShelfItems = DownInventoryItems(shop.nick, "for_shelved");
                    //所有我下架的宝贝
                    List<Taobao.Entity.Item> offShelfItems = DownInventoryItems(shop.nick, "off_shelved");
                    //从库存列表中删除我下架的
                    foreach (Taobao.Entity.Item item in offShelfItems)
                    {
                        forShelfItems.RemoveAll(i => i.Iid == item.Iid);
                    }
                    //添加入自动上架列表
                    itemlist.AddRange(forShelfItems);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return itemlist;
        }

        private List<Taobao.Entity.Item> DownInventoryItems(string nick, string banner)
        {
            try
            {
                List<Taobao.Entity.Item> itemlist = new List<Taobao.Entity.Item>();
                int pageSize = 200;
                int pageNo = 1;
                ItemRsp myrsp = new ItemRsp();
                string session = SystemHelper.GetSessionKey(nick);
                myrsp = TopService.ItemsInventoryGet(session, banner, 1, pageSize);
                if (myrsp.TotalResults == 0)
                {
                    return itemlist;
                }
                if (myrsp.Items != null && myrsp.Items.Item != null)
                {
                    itemlist.AddRange(myrsp.Items.Item);
                }
                #region 计算分页
                if (myrsp.TotalResults % pageSize == 0)
                {
                    pageNo = myrsp.TotalResults / pageSize;
                }
                else
                {
                    pageNo = myrsp.TotalResults / pageSize + 1;
                }
                if (pageNo >= 2)
                {
                    for (int i = 2; i <= pageNo; i++)
                    {
                        try
                        {
                            myrsp = TopService.ItemsInventoryGet(session, banner, 1, pageSize);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        if (myrsp.Items != null && myrsp.Items.Item != null)
                        {
                            itemlist.AddRange(myrsp.Items.Item);
                        }
                    }//for
                }//if
                #endregion

                return itemlist;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<Taobao.Entity.Item> DownOnsaleItems(string nick)
        {
            try
            {
                List<Taobao.Entity.Item> itemlist = new List<Taobao.Entity.Item>();
                int pageSize = 200;
                int pageNo = 1;
                ItemRsp myrsp = new ItemRsp();
                string session = SystemHelper.GetSessionKey(nick);
                myrsp = TopService.ItemsOnsaleGet(session, 1, pageSize);
                if (myrsp.TotalResults == 0)
                {
                    return itemlist;
                }
                if (myrsp.Items != null && myrsp.Items.Item != null)
                {
                    itemlist.AddRange(myrsp.Items.Item);
                }
                #region 计算分页
                if (myrsp.TotalResults % pageSize == 0)
                {
                    pageNo = myrsp.TotalResults / pageSize;
                }
                else
                {
                    pageNo = myrsp.TotalResults / pageSize + 1;
                }
                if (pageNo >= 2)
                {
                    for (int i = 2; i <= pageNo; i++)
                    {
                        try
                        {
                            myrsp = TopService.ItemsOnsaleGet(session, 1, pageSize);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        if (myrsp.Items != null && myrsp.Items.Item != null)
                        {
                            itemlist.AddRange(myrsp.Items.Item);
                        }
                    }//for
                }//if
                #endregion

                return itemlist.OrderBy(i => i.DelistTime).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 订单管理
        private void barBtnOrder_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("网店订单"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    TaobaoOrder frm = new TaobaoOrder();
                    frm.MdiParent = this;
                    frm.Text = "网店订单";
                    frm.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void WebSailOrder_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                TaobaoOrder taobaoFrm = new TaobaoOrder();
                taobaoFrm.MdiParent = this;
                taobaoFrm.Text = "网店管理";
                taobaoFrm.Show();
                waitFrm.Close();
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 自动获取订单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void autoGetOrderTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

        }
        #endregion

        #region 下载淘宝订单
        private void barBtnItemGetOrder_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string custom_col = "TRADE_NO_CREATE_PAY,WAIT_BUYER_PAY,WAIT_SELLER_SEND_GOODS,WAIT_BUYER_CONFIRM_GOODS,TRADE_BUYER_SIGNED,TRADE_FINISHED,TRADE_CLOSED,TRADE_CLOSED_BY_TAOBAO,ALL_WAIT_PAY,ALL_CLOSED";
                TaobaoOrderGet frm = new TaobaoOrderGet(custom_col);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 初始化系统
        private void barBtnInitSystem_ItemClick(object sender, ItemClickEventArgs e)
        {
            InitSystem frm = new InitSystem();
            frm.ShowDialog();
        }
        #endregion

        #region 出入库单
        private void barBtnStockDetail_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("出入库单"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    StockInOut frm = new StockInOut();
                    frm.MdiParent = this;
                    frm.Text = "出入库单";
                    frm.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 采购入库
        private void barBtnPurchaseIn_ItemClick(object sender, ItemClickEventArgs e)
        {
            PurchaseIn frm = new PurchaseIn();
            frm.ShowDialog();
        }
        #endregion

        #region 生产入库
        private void barBtnProduceIn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProduceInForm frm = new ProduceInForm();
            frm.ShowDialog();
        }
        #endregion

        #region 销售退货入库
        private void barBtnSelledReturnIn_ItemClick(object sender, ItemClickEventArgs e)
        {
            SelledReturnIn frm = new SelledReturnIn();
            frm.ShowDialog();
        }
        #endregion

        #region 其他入库
        private void barBtnOtherIn_ItemClick(object sender, ItemClickEventArgs e)
        {
            OtherIn frm = new OtherIn();
            frm.ShowDialog();
        }
        #endregion

        #region 销售出库
        private void barBtnSoldOut_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProductSoldOut frm = new ProductSoldOut();
            frm.ShowDialog();
        }
        #endregion

        #region 采购退货出库
        private void barBtnPurReturnOut_ItemClick(object sender, ItemClickEventArgs e)
        {
            PurchaseReturnOut frm = new PurchaseReturnOut();
            frm.ShowDialog();
        }
        #endregion

        #region 其它出库
        private void barBtnOtherOut_ItemClick(object sender, ItemClickEventArgs e)
        {
            OtherOut frm = new OtherOut();
            frm.ShowDialog();
        }
        #endregion

        #region 商品管理
        private void barBtnStockProduct_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("商品管理"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    StockProduct frm = new StockProduct();
                    frm.MdiParent = this;
                    frm.Text = "商品管理";
                    frm.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 添加商品
        private void barBtnAddProduct_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                ProductAdd frm = new ProductAdd();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 库存设置
        
        /// <summary>
        /// 商品类目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonStockCat_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("商品类目"))
                {                    
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    StockSet stockSet = new StockSet(1);//打开商品类目
                    stockSet.MdiParent = this;
                    stockSet.Text = "商品类目";
                    stockSet.Show();
                    waitFrm.Close();
                }
                //else
                //{
                //    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                //    stockSet.LoadForm(1);//打开商品类目 
                //    waitFrm.Close();
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 计量单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonStockUnit_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("计量单位"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    StockSet stockSet = new StockSet(2);//打开计量单位
                    stockSet.MdiParent = this;
                    stockSet.Text = "计量单位";
                    stockSet.Show();
                    waitFrm.Close();
                }
                //else
                //{
                //    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                //    stockSet.LoadForm(2);//打开计量单位                      
                //    waitFrm.Close();
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 仓库管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonStockHouse_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("仓库管理"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    StockSet stockSet = new StockSet(3);//打开仓库管理
                    stockSet.MdiParent = this;
                    stockSet.Text = "仓库管理";
                    stockSet.Show();
                    waitFrm.Close();
                }
                //else
                //{
                //    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                //    stockSet.LoadForm(3);//打开仓库管理                  
                //    waitFrm.Close();
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 供应商
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnSupplier_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("供应商"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    SupplierManager supplier = new SupplierManager();
                    supplier.MdiParent = this;
                    supplier.Text = "供应商";
                    supplier.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        #endregion

        #region 采购单
        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("客户订单"))
                {
                    WaitDialogForm wdForm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    PurchaseOrderList purOrder = new PurchaseOrderList();
                    purOrder.MdiParent = this;
                    purOrder.Text = "客户订单";
                    purOrder.Show();

                    //Alading.Forms.PurchaseManager.test ts = new Alading.Forms.PurchaseManager.test();
                    //ts.MdiParent = this;
                    //ts.Text = "Test";
                    //ts.Show();
                    wdForm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        #endregion

        #region 客户管理
        private void barBtnConsumer_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("客户管理"))
                {
                    string str = string.Empty;
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    ConsumerManage frm = new ConsumerManage();
                    frm.MdiParent = this;
                    frm.Text = "客户管理";
                    frm.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        /// <summary>
        /// 同步淘宝客户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConsumerGet_ItemClick(object sender, ItemClickEventArgs e)
        {
            ConsumerGet frm = new ConsumerGet();
            frm.ShowDialog();
        }
        #endregion

        private void barButtonDelivery_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("发货管理"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    Delivery delivery = new Delivery();
                    delivery.MdiParent = this;
                    delivery.Text = "发货管理";
                    delivery.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barBtnShip_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("快递跟单"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    //Alading.Forms.Ship.ShipManage delivery = new Alading.Forms.Ship.ShipManage();
                    //delivery.MdiParent = this;
                    //delivery.Text = "快递跟单";
                    //delivery.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region 评价管理
        private void barBtnRate_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("评价管理"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    Rate rate = new Rate();
                    rate.MdiParent = this;
                    rate.Text = "评价管理";
                    rate.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        private void barBtnAllocation_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("库存调拨"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);

                    Alading.Forms.Stock.Allocation.StockAllocation stockAllocation = new Alading.Forms.Stock.Allocation.StockAllocation();
                    stockAllocation.MdiParent = this;
                    stockAllocation.Text = "库存调拨";
                    stockAllocation.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region 组合商品
        private void barBtnProductList_ItemClick(object sender, ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                if (SelectedTab("组合商品"))
                {                   
                    Alading.Forms.Stock.Assemble.StockAssemble delivery = new Alading.Forms.Stock.Assemble.StockAssemble();
                    delivery.MdiParent = this;
                    delivery.Text = "组合商品";
                    delivery.Show();                    
                }
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 添加组合商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnAddAssemble_ItemClick(object sender, ItemClickEventArgs e)
        {
            AssembleAdd assembleAdd = new AssembleAdd();
            assembleAdd.ShowDialog();
        }

        #endregion

        #region 店铺管理
        private void barBtnShopManage_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("店铺管理"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);

                    Alading.Forms.Shop.ShopManage shop = new Alading.Forms.Shop.ShopManage();
                    shop.MdiParent = this;
                    shop.Text = "店铺管理";
                    shop.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 同步店铺信息
        private void barButtonItemSyncShop_ItemClick(object sender, ItemClickEventArgs e)
        {
            Alading.Forms.Shop.ShopsDown frm = new Alading.Forms.Shop.ShopsDown();
            frm.ShowDialog();
        }
        #endregion

        #region 系统管理的店铺管理
        private void barBtnShop_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("系统店铺管理"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);

                    Alading.Forms.Shop.Shop shop = new Alading.Forms.Shop.Shop();
                    shop.MdiParent = this;
                    shop.Text = "店铺管理";
                    shop.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        private void barBtnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("打印管理"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);

                    Alading.Forms.Print.PrintManage shop = new Alading.Forms.Print.PrintManage();
                    shop.MdiParent = this;
                    shop.Text = "打印管理";
                    shop.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        #region 打印模板
        private void barBtnTemplate_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("打印模板"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);

                    Alading.Forms.Print.PrintTemplate printTemplate = new Alading.Forms.Print.PrintTemplate();
                    //printTemplate.EditTemplate.ItemClick += new ItemClickEventHandler(EditTemplate_ItemClick);
                    printTemplate.OnEditTemplate += new Alading.Forms.Print.DoEditTemplateHandler(printTemplate_OnEditTemplate);
                    printTemplate.OnNewTemplate += new Alading.Forms.Print.DoEditTemplateHandler(printTemplate_OnNewTemplate);
                    printTemplate.MdiParent = this;
                    printTemplate.Text = "打印模板";
                    printTemplate.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 模板新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void printTemplate_OnNewTemplate(object sender, Alading.Forms.Print.EditTemplateEventArgs e)
        {
            try
            {
                if (SelectedTab("模板设计"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);

                    Alading.Forms.Print.TemplateDesign templateDesign = new Alading.Forms.Print.TemplateDesign(e.companyName);
                    templateDesign.MdiParent = this;
                    templateDesign.Text = "模板设计";
                    templateDesign.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 模板编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void printTemplate_OnEditTemplate(object sender, Alading.Forms.Print.EditTemplateEventArgs e)
        {
            try
            {
                if (SelectedTab("模板编辑"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);

                    Alading.Forms.Print.TemplateDesign templateDesign = new Alading.Forms.Print.TemplateDesign(e.template);
                    templateDesign.MdiParent = this;
                    templateDesign.Text = "模板编辑";
                    templateDesign.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //void EditTemplate_ItemClick(object sender, ItemClickEventArgs e)
        //{

        //}
        #endregion

        #region 双击关闭TabbedMdiForm
        // We need to use unmanaged code
        [DllImport("user32.dll")]
        // GetCursorPos() makes everything possible
        static extern bool GetCursorPos(ref Point lpPoint);

        private DateTime m_LastClick = System.DateTime.Now;
        private void xtraTabbedMdiManager_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DateTime dt = DateTime.Now;
                TimeSpan span = dt.Subtract(m_LastClick);
                if (span.TotalMilliseconds < 300)  //如果两次点击的时间间隔小于300毫秒，则认为是双击
                {
                    if (this.MdiChildren.Length > 1)
                    {
                        //if (this.ActiveMdiChild != softwareForm)
                        //{
                        //if (DialogResult.OK== XtraMessageBox.Show("关闭当前页？","系统提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question))
                        //{

                        this.ActiveMdiChild.Close();

                        //}
                        //}
                    }
                    m_LastClick = dt.AddMinutes(-1);
                }
                else
                    m_LastClick = dt;
            }
            //else if (e.Button == MouseButtons.Right)
            //{
            //    //弹出右键菜单
            //    if (this.ActiveMdiChild != softwareForm)
            //    {
            //        Point pt = new Point();
            //        GetCursorPos(ref pt);
            //        this.popupMenuTab.ShowPopup(pt);
            //    }
            //}
        }
        #endregion

        #region 盘点
        private Alading.Forms.Stock.Check.StockCheck stockCheck;
        /// <summary>
        /// 库存盘点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnStockCheck_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("盘点管理"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);

                    stockCheck = new Alading.Forms.Stock.Check.StockCheck(true);
                    stockCheck.Dock = DockStyle.Fill;
                    stockCheck.MdiParent = this;
                    stockCheck.Text = "盘点管理";
                    stockCheck.Show();
                    waitFrm.Close();
                }
                else
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    stockCheck.LoadForm(true);
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 盘点查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barStockCheckSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("盘点管理"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    stockCheck = new Alading.Forms.Stock.Check.StockCheck(false);
                    stockCheck.Dock = DockStyle.Fill;
                    stockCheck.MdiParent = this;
                    stockCheck.Text = "盘点管理";
                    stockCheck.Show();
                    waitFrm.Close();
                }
                else
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    stockCheck.LoadForm(false);
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        /// <summary>
        /// 补货单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnItemReplenish_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("补货单"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    ReplenishmentOrder replenish = new ReplenishmentOrder();
                    replenish.MdiParent = this;
                    replenish.Text = "补货单";
                    replenish.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 流水账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnAccounts_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("流水账"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    AccountsManager account = new AccountsManager();
                    account.MdiParent = this;
                    account.Text = "流水账";
                    account.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barBtnDesign_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("模板设计"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);

                    Alading.Forms.Print.TemplateDesign shop = new Alading.Forms.Print.TemplateDesign();
                    shop.MdiParent = this;
                    shop.Text = "模板设计";
                    shop.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barBtnEmailTemp_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("邮件模板"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    Alading.Forms.Email.EmailTemplateManage manage = new Alading.Forms.Email.EmailTemplateManage();
                    manage.MdiParent = this;
                    manage.Text = "邮件模板";
                    manage.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barBtnStaff_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("员工管理"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    Alading.Forms.StaffManager.StaffManager manage = new Alading.Forms.StaffManager.StaffManager();
                    manage.MdiParent = this;
                    manage.Text = "员工管理";
                    manage.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barBtnPermission_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("权限管理"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    Alading.Forms.StaffManager.PermissionManager manage = new Alading.Forms.StaffManager.PermissionManager();
                    manage.MdiParent = this;
                    manage.Text = "权限管理";
                    manage.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barBtnEmailManage_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedTab("邮件管理"))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    Alading.Forms.Email.EmailManage manage = new Alading.Forms.Email.EmailManage();
                    manage.MdiParent = this;
                    manage.Text = "邮件管理";
                    manage.Show();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barBtnAchievement_ItemClick(object sender, ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                if (SelectedTab("业绩考核"))
                {                    
                    Alading.Forms.Staff.AchievementAssess manage = new Alading.Forms.Staff.AchievementAssess();
                    manage.MdiParent = this;
                    manage.Text = "业绩考核";
                    manage.Show();                    
                }
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }        

        private void btnDownProps_ItemClick(object sender, ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                Alading.Forms.PropValue.DownloadPropValue downForm = new Alading.Forms.PropValue.DownloadPropValue();
                waitFrm.Close();
                downForm.ShowDialog();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region  系统帮助选项
        private void barHelperFiles_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.aldsoft.cc/HelpList.aspx");
        }

        private void barOnlineHelp_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.aldsoft.cc/HelpList.aspx");
        }

        private void barButtonItem42_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.aldsoft.cc/Product.aspx");
        }

        private void barHomePage_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.aldsoft.cc/Default.aspx");
        }

        private void barAbortAlading_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.aldsoft.cc/Product.aspx");
        }
        #endregion

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem36_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("是否要退出系统?", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) ==DialogResult.OK )
            {
                this.Dispose();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (XtraMessageBox.Show("是否要退出系统?", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (this.notifyIcon != null)
                {
                    this.notifyIcon.Dispose();
                }
                this.Dispose();
            }
            else
            {
                e.Cancel = true;
            }

        }
       
    }
}