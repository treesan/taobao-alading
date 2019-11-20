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
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.Utils;
using Alading.Properties;
using System.IO;

namespace Alading.Forms.Trade.Forms
{
    public partial class TradeAdd : DevExpress.XtraEditors.XtraForm
    {

        //当前GridView的数据源，Shema从XML读取,GridView与本源随时保持同步
        private DataTable _dTbRecord = new DataTable();

        public TradeAdd()
        {
            InitializeComponent();
            //从XML读取Table框架
            MemoryStream stream = new MemoryStream(Resources.TradeAddSchema);
            try
            {
                _dTbRecord.ReadXmlSchema(stream);
            }
            finally
            {
                stream.Close();
            }
            InitFormValues();
          
        }

        #region 界面初始化 绑定数据 
        /// 初始化界面绑定
        private void InitFormValues()
        {
            dateCreateTrade.DateTime = DateTime.Now;//订单时间
            dateEndTrade.DateTime = DateTime.Now.AddDays(Constants.DEFAULT_END_DAYS);//默认为15天过后

            #region 客户名称
            cmbConsumerName.Properties.DataSource = ConsumerService.GetAllConsumer();
            cmbConsumerName.Properties.NullText = "请选择客户";
            #endregion

            #region 收款方式
            cmbPayWay.Properties.DataSource = CodeService.GetCode(p => p.CodeCategory == Constants.CODE_TRADE_TYPE);
            cmbPayWay.EditValue = Constants.DEFAULT_TRADE_TYPE;
            #endregion

            #region 销售人员
            cmbSeller.Properties.DataSource = UserService.GetAllUser();//待添加条件选择
            cmbSeller.Properties.NullText = "请选择销售人员";
            #endregion

            #region 承担方式
            cmbStandWay.Properties.DataSource = CodeService.GetCode(p => p.CodeCategory == Constants.CODE_POSTFEE_OWNER);
            cmbStandWay.EditValue = Constants.DEFAULT_POSTFEE_OWNER;
            #endregion

            #region 物流信息系列绑定
            try
            {
                cmbShippingType.Properties.DataSource = CodeService.GetCode(p => p.CodeCategory == Constants.CODE_SHIPPING_TYPE);
                cmbShippingType.EditValue = Constants.DEFAULT_SHIPPING_TYPE;

                // 物流公司
                List<LogisticCompany> companySource = LogisticCompanyService.GetLogisticCompany(p => p.shippingType == cmbShippingType.EditValue.ToString());
                cmbShipCompany.Properties.DataSource = companySource;
                cmbShipCompany.EditValue = companySource.FirstOrDefault(p => p.isdefault == true).code;

                //物流模板
                List<LogisticCompanyTemplate> templateSource = LogisticCompanyTemplateService.GetLogisticCompanyTemplate(p => p.LogisticCompanyCode == cmbShipCompany.EditValue.ToString());
                cmbShippingTemplate.Properties.DataSource = templateSource;
                cmbShippingTemplate.EditValue = templateSource.FirstOrDefault().TemplateCode;
            }
            catch(Exception ex)
            {

            }

            #endregion

            #region  所属店铺
            cmbOwnerShop.Properties.DataSource = ShopService.GetAllShop();
            cmbOwnerShop.Properties.NullText = "请选择店铺";
            cmbOwnerShop.Properties.PopupWidth = 400;
            #endregion

            radioHasTicket.EditValue = false;//默认不开票
            txtPostFee.Text = "10";//默认邮费为10元
            txtDiscountRate.Text = "1.0";//默认不打折
            txtDicountOutFee.Text = "0.0";
            txtDiscountFee.Text = "0.0";
        }
        #endregion

        #region  物流信息相关项改变值的时候重新绑定

         //当物流方式改变时重新绑定物流公司和物流模板
        private void cmbShippingType_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                // 物流公司
                List<LogisticCompany> companySource = LogisticCompanyService.GetLogisticCompany(p => p.shippingType == cmbShippingType.EditValue.ToString());
                cmbShipCompany.Properties.DataSource = companySource;
                cmbShipCompany.EditValue = companySource.FirstOrDefault(p => p.isdefault == true).code;
            }
            catch (Exception ex)
            {

            }
        }

        
        //当物流公司改变时重新绑定物流模板
        private void cmbShipCompany_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //物流模板
                List<LogisticCompanyTemplate> templateSource = LogisticCompanyTemplateService.GetLogisticCompanyTemplate(p => p.LogisticCompanyCode == cmbShipCompany.EditValue.ToString());
                cmbShippingTemplate.Properties.DataSource = templateSource;
                cmbShippingTemplate.EditValue = templateSource.FirstOrDefault().TemplateCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        

        #endregion

        #region 界面验证检验 提交数据时检验

        /// 检验界面数据是否正确
        private bool ValidationHasErrors()
        {
            dxErrorProvider.ClearErrors();//清空记录

            #region 代码绑定用户控件验证
            if (cmbConsumerName.EditValue == null)
            {
                dxErrorProvider.SetError(cmbConsumerName, "请选择客户！");
            }
            if (cmbSeller.EditValue == null || cmbSeller.EditValue.ToString()==string.Empty)
            {
                dxErrorProvider.SetError(cmbSeller, "请选择卖家！");
            }
            if (txtZipCode.EditValue == null || txtZipCode.EditValue.ToString() == string.Empty)
            {
                dxErrorProvider.SetError(txtZipCode, "请输入邮政编码！");
            }
            if (cmbOwnerShop.EditValue == null)
            {
                dxErrorProvider.SetError(cmbOwnerShop, "请选择店铺！");
            }
            if (txtMobileNum.EditValue == null || txtMobileNum.EditValue.ToString() == string.Empty)
            {
                dxErrorProvider.SetError(txtMobileNum, "请输入联系电话！");
            }
            if (txtReceiverName.EditValue == null || txtReceiverName.EditValue.ToString() == string.Empty)
            {
                dxErrorProvider.SetError(txtReceiverName, "请填写收货人！");
            }
            if (txtReceiverAddr.EditValue == null || txtReceiverAddr.EditValue.ToString() == string.Empty)
            {
                dxErrorProvider.SetError(txtReceiverAddr, "请填写收货地址！");
            }
            #endregion

            if (dxErrorProvider.HasErrors)
            {
                XtraMessageBox.Show("请确认收货信息填写正确！");
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region  新加入订单商品和赠品

        /// 添加商品
        private void barAddItems_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Alading.Forms.Trade.Forms.TradeProductSelect productSelect = new Alading.Forms.Trade.Forms.TradeProductSelect();
            productSelect.ShowDialog();
            if (productSelect.DialogResult == DialogResult.OK)
            {
                AppendNewRows(productSelect.SelectedItems);
            }
        }

        //添加订单赠品
        private void barAddGifts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //GiftGiven gifts = new GiftGiven();
            //gifts.Owner = this;//将GiftGiven的父窗口置为本窗口
            //gifts.ShowDialog();
            //AppendNewRows(gifts.SelectedItems);
        }

        ///辅助函数  将选中商品赠品加入队列
        private void AppendNewRows(List<View_StockItemProduct> productsList)
        {
            if (productsList == null)
                return;
            foreach (View_StockItemProduct item in productsList)
            {
                if (this.HasExist(item) == false)//已有队列中不存在才可以加入
                {
                    DataRow itemRow = _dTbRecord.NewRow();
                    itemRow["operateDel"] = "Delete";
                    itemRow["Name"] = item.Name;
                    itemRow["OuterID"] = item.OuterID;
                    itemRow["SkuProps_Str"] = item.SkuProps_Str;
                    itemRow["LeftQuantity"] = item.SkuQuantity - item.OccupiedQuantity;
                    itemRow["num"] = 1;//默认购买量为1
                    itemRow["price"] = item.SkuPrice;
                    itemRow["orderTotalFee"] = item.SkuPrice;
                    if (item.StockItemType == (int)Alading.Core.Enum.StockItemType.GiftGoods)
                    {
                        itemRow["OrderType"] = Alading.Core.Enum.emumOrderType.GiftGoods;
                    }
                    else
                    {
                        itemRow["OrderType"] = Alading.Core.Enum.emumOrderType.SellGoods;
                    }
                    itemRow["oid"] = Guid.NewGuid().ToString();
                    _dTbRecord.Rows.Add(itemRow);
                }
            }

            gcTradeOrders.DataSource = _dTbRecord;
            gcTradeOrders.ForceInitialize();//强制再次初始化
            gvTradeOrder.BestFitColumns();

            //重新计算当前的折扣金额
            txtDiscountFee.Text = (GetOrdersTotalFee() * (1.0 - Double.Parse(txtDiscountRate.EditValue.ToString()))).ToString();
            //重新计算当前的折扣后金额
            txtDicountOutFee.Text = (GetOrdersTotalFee() * Double.Parse(txtDiscountRate.EditValue.ToString())).ToString();
        }

        ///辅助函数  判断之前是否已经加入了本项Sku_Out_Id
        private bool HasExist(View_StockItemProduct item)
        {
            foreach (DataRow row in _dTbRecord.Rows)
            {
                if (row["OuterID"].ToString()==item.OuterID&&row["SkuProps_Str"].ToString()==item.SkuProps_Str)
                {
                    return true;//只要匹配，即true
                }
            }
            return false;//程序执行到此，一定是false
        }
        #endregion

        #region 操作订单数据  GridControl事件响应

        /// 删除本列数据
        private void gvTradeOrder_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView gv = (sender as GridView).GridControl.GetViewAt(e.Location) as GridView;
            if (gv != null)
            {
                GridHitInfo hi = gv.CalcHitInfo(e.Location);
                if (hi.Column != null && hi.InRowCell)
                {
                    DataRow currentMatch = gv.GetDataRow(hi.RowHandle);  //记录当前焦点所在的OrderRow
                    if (hi.Column.ColumnHandle == 0)//如果选中行为修改订单
                    {
                        switch (XtraMessageBox.Show("是否确认删除本行订单！","确认？",MessageBoxButtons.OKCancel))
                        {
                            case DialogResult.OK:
                         #region 去除源Table中的本行记录&&Grid中的本行记录
                                foreach(DataRow row in _dTbRecord.Rows)
                                {
                                  if( row["OuterID"].ToString()==currentMatch["OuterID"].ToString()&&
                                     row["SkuProps_Str"].ToString() == currentMatch["SkuProps_Str"].ToString())
                                  {
                                      _dTbRecord.Rows.Remove(row);
                                      //重新计算当前的折扣金额
                                      txtDiscountFee.Text = (GetOrdersTotalFee() * (1.0 - Double.Parse(txtDiscountRate.Text))).ToString();
                                      //重新计算当前的折扣后金额
                                      txtDicountOutFee.Text = (GetOrdersTotalFee() * Double.Parse(txtDiscountRate.Text)).ToString();
                                      break;
                                  }
                                }
                                gvTradeOrder.DeleteRow(hi.RowHandle);
                        #endregion
                                break;
                            case DialogResult.Cancel:
                                return;
                        }
                    }
                }
            }
        }

        /// 改变商品数量是需要的改变
        private void gvTradeOrder_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView sendView = sender as GridView;
            DataRow matchRow = sendView.GetDataRow(e.RowHandle);
            matchRow["orderTotalFee"] = Double.Parse(matchRow["num"].ToString()) * Double.Parse(matchRow["price"].ToString());
            //  Validates the focused row and saves its values to the data source.
            //  取得当前焦点行，并更新其数据到数据源，即_dTbRecord
            sendView.UpdateCurrentRow();

            //重新计算当前的折扣金额
            txtDiscountFee.Text = (GetOrdersTotalFee() * (1.0 - Double.Parse(txtDiscountRate.EditValue.ToString()))).ToString();
            //重新计算当前的折扣后金额
            txtDicountOutFee.Text = (GetOrdersTotalFee() * Double.Parse(txtDiscountRate.EditValue.ToString())).ToString();
        }

        /// 修改优惠率后的数据绑定
        private void txtDiscountRate_Properties_Leave(object sender, EventArgs e)
        {
            //重新计算当前的折扣金额
            txtDiscountFee.Text = (GetOrdersTotalFee() * (1.0 - Double.Parse(txtDiscountRate.EditValue.ToString()))).ToString();
            //重新计算当前的折扣后金额
            txtDicountOutFee.Text = (GetOrdersTotalFee() * Double.Parse(txtDiscountRate.EditValue.ToString())).ToString();
        }

        #endregion

        #region  下方按钮事件响应

        /// 保存
        private void btnSummit_Click(object sender, EventArgs e)
        {
            if (ValidationHasErrors())//验证填写内容是否正确
            {
                return;
            }
            if (_dTbRecord.Rows.Count == 0)
            {
                XtraMessageBox.Show("请选择订单商品！");
                return;
            }
            WaitDialogForm wdf = new WaitDialogForm(Alading.Taobao.Constants.OPERATE_DB_DATA);
            SummitTradeAndOrders();
            wdf.Close();
            this.Close();
        }

        /// 保存并新建
        private void btnSummitRefresh_Click(object sender, EventArgs e)
        {

            if (ValidationHasErrors())
            {
                return;
            }
            if(_dTbRecord.Rows.Count==0)
            {
                XtraMessageBox.Show("请选择订单商品！");
                return;
            }
            SummitTradeAndOrders();
            this.Refresh();
        }

        /// 取消
        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       #endregion

        #region  提交内容到数据库
        /// 根据界面内容来提交交易，订单数据
        private void SummitTradeAndOrders()
        {
            Alading.Entity.Trade createTrade = new Alading.Entity.Trade();
            TradeCreateData(createTrade);
            foreach (DataRow row in _dTbRecord.Rows)
            {
                 Alading.Entity.TradeOrder createOrder = new  Alading.Entity.TradeOrder();
                 OrderCreateData(createOrder,row,createTrade.CustomTid);
                 TradeOrderService.AddTradeOrder(createOrder);
            }
            TradeService.AddTrade(createTrade);
        }

        #endregion

        #region  界面辅助 计算总费  交易数据Create 订单数据Create
        /// <summary>
        /// 获得当前的所有订单的总费用
        /// </summary>
        /// <returns></returns>
        private double GetOrdersTotalFee()
        {
            double sumFee = double.Parse("0.0");
            foreach (DataRow row in _dTbRecord.Rows)
            {
                //只有销售品才计价
                if (row["OrderType"].ToString() == Alading.Core.Enum.emumOrderType.SellGoods)
                {
                    sumFee += double.Parse(row["orderTotalFee"].ToString());
                }
            }
            return sumFee;
        }

        #region  TradeCreateData
        public void TradeCreateData(Alading.Entity.Trade createTrade)
        {
            createTrade.CustomTid = Guid.NewGuid().ToString();
            createTrade.tid = createTrade.CustomTid;
            createTrade.iid = string.Empty;
            createTrade.ParentCustomTid = "0";
            createTrade.seller_nick = cmbOwnerShop.SelectedText;
            createTrade.buyer_nick = cmbConsumerName.SelectedText ;
            createTrade.title = "本底新建订单";
            createTrade.type =  Alading.Core.Enum.emumOrderType.SellGoods;
            createTrade.created = DateTime.Now;
            createTrade.price = 0.0;
            createTrade.pic_path = string.Empty;
            createTrade.num = 1;
            createTrade.buyer_message = txtMemo.Text;
            createTrade.buyer_rate = false;
            createTrade.buyer_memo = txtMemo.Text;
            createTrade.seller_rate = false;
            createTrade.seller_memo = txtMemo.Text;
            createTrade.shipping_type =cmbShippingType.EditValue.ToString();
            createTrade.alipay_no = string.Empty;
            createTrade.payment =double.Parse(txtDicountOutFee.Text.ToString()) + double.Parse(txtPostFee.Text.ToString());//总费用等于邮费加货物费用
            createTrade.discount_fee = 0.0;//取负值
            createTrade.adjust_fee = 0 - double.Parse(txtDiscountFee.Text.ToString()); //取负值
            createTrade.snapshot_url = string.Empty;
            createTrade.snapshot = string.Empty;
            createTrade.status = TradeEnum.WAIT_SELLER_SEND_GOODS;
            createTrade.pay_time = DateTime.MinValue;
            createTrade.end_time = DateTime.MinValue;
            createTrade.modified = DateTime.MinValue;
            createTrade.buyer_obtain_point_fee = string.Empty;
            createTrade.point_fee = string.Empty;
            createTrade.real_point_fee = string.Empty;
            createTrade.total_fee = GetOrdersTotalFee();
            createTrade.post_fee = double.Parse(txtPostFee.Text);
            createTrade.buyer_alipay_no = string.Empty;
            createTrade.receiver_name = txtReceiverName.Text ?? string.Empty;
            createTrade.receiver_state = string.Empty;
            createTrade.receiver_city = string.Empty;
            createTrade.receiver_district = string.Empty;
            createTrade.receiver_address = txtReceiverAddr.Text;
            createTrade.receiver_zip = txtZipCode.Text;
            createTrade.receiver_mobile = txtMobileNum.Text ;
            createTrade.receiver_phone = txtMobileNum.Text ;
            createTrade.consign_time = DateTime.MinValue;
            createTrade.buyer_email = string.Empty;
            createTrade.commission_fee = string.Empty;
            createTrade.seller_alipay_no = string.Empty;
            createTrade.seller_mobile =  string.Empty;
            createTrade.seller_phone =  string.Empty;
            createTrade.seller_name = cmbSeller.SelectedText ?? string.Empty;
            createTrade.seller_email = string.Empty;
            createTrade.available_confirm_fee = string.Empty;
            createTrade.has_post_fee = true;
            createTrade.received_payment = 0.0;
            createTrade.cod_fee = 0.0;
            createTrade.cod_status = string.Empty;
            createTrade.timeout_action_time = dateEndTrade.DateTime;
            createTrade.is_3D = false;
            createTrade.LastShippingType = cmbShippingType.EditValue.ToString();
            createTrade.LogisticCompanyCode = cmbShipCompany.EditValue.ToString();
            createTrade.TemplateCode = cmbShippingTemplate.EditValue.ToString();//物流公司信息
            createTrade.receiver_name = txtReceiverName.Text;
            createTrade.receiver_state = string.Empty;
            createTrade.receiver_city =string.Empty;
            createTrade.receiver_district = string.Empty;
            createTrade.receiver_address = txtReceiverAddr.Text;
            createTrade.receiver_zip = txtZipCode.Text;
            createTrade.receiver_mobile = txtMobileNum.Text;
            createTrade.receiver_phone = txtMobileNum.Text;
            createTrade.buyer_message = txtMemo.Text;
            createTrade.buyer_memo = txtMemo.Text;
            createTrade.ShippingCode = string.Empty;
            createTrade.LocalStatus = LocalTradeStatus.HasNotSummit;
            createTrade.CombineCode = string.Empty;
            createTrade.BuyerType = 1;
            createTrade.IsCombined = false;
            createTrade.IsSplited = false;
            createTrade.type ="fixed";
            createTrade.HasInvoice = bool.Parse(radioHasTicket.EditValue.ToString());
            createTrade.BuyerType = 1;
            createTrade.SellerType = 1;//TODO 卖家类型B C？
            createTrade.TradeSourceType = TradeSourceType.NEWCREATE;
            createTrade.ParentCustomTid = string.Empty;
            createTrade.CombineCode = string.Empty;
            createTrade.LockedUserCode = string.Empty;
            createTrade.LockedUserName = string.Empty;
            createTrade.LockedTime = DateTime.MinValue;
            createTrade.ConsignStatus = 1;
            createTrade.TradeTimeStamp = new byte[1];
        }
        #endregion

        #region OrderCreateData
        private void OrderCreateData(Alading.Entity.TradeOrder createOrder, DataRow row,string customTid)
        {
            Alading.Entity.Item item = ItemService.GetItem(p => p.outer_id == row["OuterID"].ToString()).FirstOrDefault();
            createOrder.CustomTid = customTid;
            createOrder.iid = string.Empty;
            createOrder.sku_id = item == null ? string.Empty : item.iid;
            createOrder.TradeOrderCode = Guid.NewGuid().ToString();
            createOrder.oid=createOrder.TradeOrderCode;
            createOrder.outer_sku_id = string.Empty;
            createOrder.outer_iid =string.Empty;
            createOrder.sku_properties_name = row["SkuProps_Str"].ToString();
            createOrder.price = double.Parse(row["price"].ToString());
            createOrder.total_fee = double.Parse(row["orderTotalFee"].ToString()); 
            createOrder.discount_fee = 0.0;//淘宝系统优惠价  为 0. 0
            createOrder.adjust_fee = 0 - double.Parse(row["orderTotalFee"].ToString()) * (1 - double.Parse(txtDiscountRate.Text));
            createOrder.payment = double.Parse(row["orderTotalFee"].ToString()) * double.Parse(txtDiscountRate.Text);
            createOrder.item_meal_name =  string.Empty;
            createOrder.num = int.Parse(row["num"].ToString());
            createOrder.title = row["Name"].ToString();
            createOrder.pic_path =  string.Empty;
            createOrder.seller_nick = cmbSeller.SelectedText;
            createOrder.buyer_nick = cmbConsumerName.SelectedText;
            createOrder.created =dateCreateTrade.DateTime;
            createOrder.refund_status = Alading.Core.Enum.RefundStatus.NO_REFUND;
            createOrder.status = TradeEnum.WAIT_SELLER_SEND_GOODS;
            createOrder.snapshot_url = string.Empty;
            createOrder.snapshot = string.Empty;
            createOrder.timeout_action_time = DateTime.MinValue;
            createOrder.OrderType = Alading.Core.Enum.emumOrderType.SellGoods;//系统配置订单类型
            createOrder.type = string.Empty;
            createOrder.seller_type = cmbSeller.EditValue.ToString();
            createOrder.HouseCode = string.Empty;
            createOrder.LayoutCode = string.Empty;

            
        }
        #endregion
       
        #endregion
        
        

    }
}