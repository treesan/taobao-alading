using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Alading.Business;
using Alading.Entity;
using Alading.Core.Enum;
using DevExpress.XtraEditors;
using Alading.Taobao.Entity;
using Alading.Utils;
using System.IO;
using Alading.Properties;
using DevExpress.Utils;

namespace Alading.Forms.Trade.Forms
{
    public partial class ModifyOrder : DevExpress.XtraEditors.XtraForm
    {
        private View_TradeStock _tradeStock = null;
        private byte[] _orderTimeStamp = null;

        public ModifyOrder(View_TradeStock tradeStock , byte[] orderTimeStamp)
        {
            InitializeComponent();
            WaitDialogForm wdf = new WaitDialogForm(Alading.Taobao.Constants.OPERATE_DB_DATA);
            _orderTimeStamp = orderTimeStamp;
            _tradeStock = tradeStock; 
            InitCmbDetails();
            InitForm();
            wdf.Close();
            
        }

        /// 界面初始化
        private void InitForm()
        {
            txtIid.Text = _tradeStock.iid;
            txtTitle.Text = _tradeStock.title;
            txtPrice.Text = _tradeStock.price.ToString();
            txtStockNum.Text =(_tradeStock.SkuQuantity - _tradeStock.OccupiedQuantity).ToString();
            txtNum.Text = _tradeStock.num.ToString();
            txtTotalFee.Text = _tradeStock.total_fee.ToString();
            txtPayment.Text = _tradeStock.payment.ToString();
            txtMode.Text = _tradeStock.StockUnitName;
            cmbProperties.EditValue = _tradeStock.sku_properties_name;
        }

        //创建下拉列biao
        private void InitCmbDetails()
        {
            DataSet dataSet = new DataSet();

            //从XML文件读出暂存数据库表格框架 包含TradeList表和OrderList表 
            MemoryStream stream = new MemoryStream(Resources.ModifyOrderSchema);
            try
            {
                dataSet.ReadXmlSchema(stream);
            }
            finally
            {
                stream.Close();
            }

            if (_tradeStock.ItemType != "组合商品")
            {
                List<StockProduct> sipList =
                    StockProductService.GetStockProduct(p => p.OuterID == _tradeStock.outer_id);
                foreach (StockProduct sip in sipList)
                {
                    DataRow sipRow = dataSet.Tables["ProductList"].NewRow();
                    double conversion = _tradeStock.Conversion.Value;
                    sipRow["SkuProps_Str"] = sip.SkuProps_Str;
                    sipRow["LeftQuantity"] = Math.Floor((sip.SkuQuantity - sip.OccupiedQuantity) / conversion);
                    sipRow["lackProductOrNot"] =
                        sip.SkuQuantity - sip.OccupiedQuantity - _tradeStock.num * _tradeStock.Conversion >= 0 ? LackProductOrNot.Normal : LackProductOrNot.Lack;
                    dataSet.Tables["ProductList"].Rows.Add(sipRow);
                }
            }
            else
            {
                List<AssembleItem> sipList =
                 AssembleItemService.GetAssembleItem(p => p.OuterID == _tradeStock.outer_id);
                foreach (AssembleItem sip in sipList)
                {
                    List<View_AssembleProduct> detailAssemble = AssembleItemService.GetView_AssembleProduct(p => p.AssembleOuterID == _tradeStock.outer_id && p.AssembleProps_Str == sip.SkuProps_Str);
                    DataRow sipRow = dataSet.Tables["ProductList"].NewRow();
                    sipRow["SkuProps_Str"] = sip.SkuProps_Str;
                    sipRow["LeftQuantity"] = detailAssemble.Min(p => (p.SkuQuantity - p.OccupiedQuantity )/ p.Count);
                    sipRow["lackProductOrNot"] =
                        detailAssemble.Min(p => p.SkuQuantity - p.OccupiedQuantity - p.Count * _tradeStock.num) >= 0 ? LackProductOrNot.Normal : LackProductOrNot.Lack;
                    dataSet.Tables["ProductList"].Rows.Add(sipRow);
                }
            }
            cmbProperties.Properties.DataSource = dataSet.Tables["ProductList"];
        }

        //保存修改
        private void BtnModify_Click(object sender, EventArgs e)
        {
             DialogResult result = DialogResult.OK;
             if (!Alading.Utils.SystemHelper.CompareTimeStamp(_orderTimeStamp as byte[], _tradeStock.OrderTimeStamp))
             {
                  result=XtraMessageBox.Show("当前订单已经被修改,继续修改(OK)/查看流程信息(Canel)", "订单修改", MessageBoxButtons.OKCancel);
             }
             if (result == DialogResult.OK)
             {
                 string skuProsName = cmbProperties.Text.ToString();//取得选中sku_pros
                 TradeOrder order = TradeOrderService.GetTradeOrder(p => p.TradeOrderCode == _tradeStock.TradeOrderCode).FirstOrDefault();

                 if (order.sku_properties_name != skuProsName)//的的确确修改订单信息才提交笔生成流程信息
                 {
                     //创建一条交易信息
                     string flowMeassge = "商品\"" + _tradeStock.title + "\"销售属性修改:" + order.sku_properties_name+"-->"+skuProsName;
                     SystemHelper.CreateFlowMessage(_tradeStock.CustomTid, "订单信息修改", flowMeassge,"订单管理");
                     order.sku_properties_name = skuProsName;
                    
                     #region  保存修改信息到数据库和并同步到淘宝
                     WaitDialogForm wdf = new WaitDialogForm(Alading.Taobao.Constants.OPERATE_TBDB_DATA);
                     try
                     {
                         UpdateTaobaoOrder();
                         TradeOrderService.UpdateTradeOrder(order);
                         wdf.Close();
                         XtraMessageBox.Show("修改订单信息成功！");
                     }
                     catch (Exception ex)
                     {
                         wdf.Close();
                         XtraMessageBox.Show("将修改信息保存到淘宝失败，修改无效！原因:" + ex.Message);
                     }
                     #endregion
                 }
                 else
                 {
                     result = DialogResult.Ignore;//实际什么都没做，不需要更新数据库时间戳
                 }
             }
           //如果在主界面接受到的结果为Dialog.Canel，则跳转流程信息页面。如果是Dialog.OK则修改界面信息，Dialog.Ignore不做
           this.DialogResult = result;
           this.Close();
        }

        //返回
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }

        //绑定数据
        private void cmbProperties_Enter(object sender, EventArgs e)
        {
            InitCmbDetails();
        }

        //属性改变时相应的处理
        private void cmbProperties_EditValueChanged(object sender, EventArgs e)
        {
            string skuProsName=cmbProperties.EditValue.ToString();
            if (_tradeStock.ItemType == "组合商品")
            {
                List<View_AssembleProduct> detailAssemble = AssembleItemService.GetView_AssembleProduct(p => p.AssembleOuterID == _tradeStock.outer_id &&
                    p.AssembleProps_Str == skuProsName);
                txtStockNum.Text = detailAssemble.Min(p => (p.SkuQuantity - p.OccupiedQuantity) / p.Count).ToString();
            }
            else
            {
                StockProduct product = StockProductService.GetStockProduct(_tradeStock.SkuOuterID);
                txtStockNum.Text = (product.SkuQuantity - product.OccupiedQuantity).ToString();
            }
        }

        //将本地修改数据数据更新到淘宝字段
        private void UpdateTaobaoOrder()
        {
            string sessionKey = Alading.Utils.SystemHelper.GetSessionKey(_tradeStock.seller_nick);
            string upadteOid = _tradeStock.oid;
            string newSkuPros = _tradeStock.SkuProps_Str;
            try
            {
                //调用API函数进行更新
                Alading.Taobao.API.TopService.TradeOrderSkuUpdate(sessionKey, upadteOid, newSkuPros);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
