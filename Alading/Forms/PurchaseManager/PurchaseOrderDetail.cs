using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;
using Alading.Business;
using Alading.Taobao;
using System.Linq;
using Alading.Core.Enum;
using System.Text.RegularExpressions;

namespace Alading.Forms.PurchaseManager
{
    public partial class PurchaseOrderDetail : DevExpress.XtraEditors.XtraForm
    {

        public DataTable dTable = new DataTable();

        public string purchaseOrderCode;

        public List<PurchaseProduct> purchaseProductList = new List<PurchaseProduct>();

        public PurchaseOrderDetail()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 将各个不同的status转化为字符串类型进行提示[OrderStatus]
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string StatusToString(int status)
        {
            if ((OrderStatus)status == OrderStatus.NotPerformed)  // 1代表“未执行”
            {
                return "未执行";
            }
            if ((OrderStatus)status == OrderStatus.Performing)
            {
                return "执行中";
            }
            if ((OrderStatus)status == OrderStatus.Performed)
            {
                return "已执行";
            }
            if ((OrderStatus)status == OrderStatus.Invalid)
            {
                return "已作废";
            }
            return "";
        }

        /// <summary>
        ///  根据purOrderCode获取唯一的采购订单进行PurchaseOrderDetail的初始化
        /// </summary>
        /// <param name="purOrderCode"></param>
        public PurchaseOrderDetail(string purOrderCode)
        {
            InitializeComponent();
            Init();
            dTable.Rows.Clear();
            this.purchaseOrderCode = purOrderCode;
            PurchaseOrder nowPurchaseOrder = new PurchaseOrder();
            nowPurchaseOrder = PurchaseOrderService.GetPurchaseOrder(purOrderCode);
            OrderCodeText.Text = nowPurchaseOrder.PurchaseOrderCode.ToString();
            OrderTime.Text = Convert.ToString(nowPurchaseOrder.OrderTime).ToString();
            PurchaserName.Text = nowPurchaseOrder.PurchaserName.ToString();
            SupplierText.Text = nowPurchaseOrder.SupplierName.ToString();
            OrderStatusText.Text = StatusToString(nowPurchaseOrder.OrderStatus);

            List<PurchaseProduct> nowPurchaseProductList = new List<PurchaseProduct>();
            nowPurchaseProductList = PurchaseProductService.GetPurchasePorduct(purOrderCode);
            int i = 1;
            if (nowPurchaseProductList!=null)
            {
                foreach (var purchaseProduct in nowPurchaseProductList)
                {
                    DataRow row = dTable.NewRow();
                    row["Line"] = i.ToString();
                    row["SkuOuterID"] = purchaseProduct.SkuOuterId.ToString();
                    row["ItemName"] = purchaseProduct.ProductName.ToString();
                    row["PurchaseNum"] = Convert.ToDouble(purchaseProduct.PurchaseQuantity);
                    row["ProductPrice"] = Convert.ToDouble(purchaseProduct.PurchasePrice);
                    row["PurchaseSum"] = Convert.ToDouble(purchaseProduct.PurchaseTotalFee);
                    row["Remarks"] = purchaseProduct.PurchaseRemark.ToString();
                    dTable.Rows.Add(row);

                    i++;
                }
            }

            Productgrid.DataSource = dTable;

        }

        public void Init()
        {
            //dTable.Columns.Add("IsDelete", System.Type.GetType("System.Boolean"));
            dTable.Columns.Add("Line", typeof(string));
            dTable.Columns.Add("SkuOuterID", typeof(string));
            dTable.Columns.Add("ItemName", typeof(string));
            dTable.Columns.Add("ProductSpecification", typeof(string));
            dTable.Columns.Add("PurchaseNum", typeof(int));
            dTable.Columns.Add("ProductPrice", typeof(double));
            dTable.Columns.Add("DiscountRate", typeof(double));
            dTable.Columns.Add("PurchaseSum", typeof(double)); //采购金额
            dTable.Columns.Add("Remarks", typeof(string));
            //DataRow row = dTable.NewRow();
            
            Productgrid.DataSource = dTable;
        }

        private void ChangeBtn_Click(object sender, EventArgs e)
        {
            decimal ToalFee = new decimal();
            PurchaseOrder nowPurchaseOrder = PurchaseOrderService.GetPurchaseOrder(this.purchaseOrderCode);
            nowPurchaseOrder.PurchaserName = PurchaserName.Text;

            //更新采购单中的物品

            List<PurchaseProduct> oldPurchaseProductList = PurchaseProductService.GetPurchasePorduct(this.purchaseOrderCode);
            int count_PurchaseProduct=oldPurchaseProductList.Count-1;
            foreach (DataRow row in dTable.Rows)
            {
                if (count_PurchaseProduct >= 0)
                {
                    oldPurchaseProductList[count_PurchaseProduct].PurchasePrice = Convert.ToDecimal(row["ProductPrice"]);
                    oldPurchaseProductList[count_PurchaseProduct].PurchaseQuantity = Convert.ToDouble(row["PurchaseNum"]);
                    decimal PurchaseNum = (decimal)oldPurchaseProductList[count_PurchaseProduct].PurchaseQuantity;
                    oldPurchaseProductList[count_PurchaseProduct].PurchaseTotalFee = oldPurchaseProductList[count_PurchaseProduct].PurchasePrice * PurchaseNum;
                    ToalFee += oldPurchaseProductList[count_PurchaseProduct].PurchaseTotalFee;
                    oldPurchaseProductList[count_PurchaseProduct].PurchaseRemark = row["Remarks"].ToString();
                    count_PurchaseProduct--;
                }

            }
            ReturnType result_pro=PurchaseProductService.UpdatePurchaseProduct(oldPurchaseProductList);

            //更新采购单
            nowPurchaseOrder.TotalFee = ToalFee;
            ReturnType result_pur= PurchaseOrderService.UpdatePurchaseOrder(nowPurchaseOrder);
            if (result_pro == ReturnType.Success && result_pur == ReturnType.Success)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //new Alading.Forms.PurchaseManager.PurchaseOrderList().GetPurchaseOrder(purchaseorderCodeList);
            }
            else
                DevExpress.XtraEditors.XtraMessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                

        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(e.RowHandle);
            if (e.Column == gridProNum)
            {
                gridView1.BeginDataUpdate();
                gridProNum.ToolTip = "";
                Regex regex = new Regex("^[0-9]*[1-9][0-9]*$");
                bool result = regex.IsMatch(Convert.ToString(e.Value).Trim());
                if (result)
                {
                    int num = Convert.ToInt32(e.Value);
                    int price = Convert.ToInt32(row["ProductPrice"]);
                    row["PurchaseNum"] = num;
                    row["PurchaseSum"] = num * price;
                }
                else
                {
                    gridProNum.ToolTip = "商品数量必须为正整数,请重新输入";
                }
                gridView1.EndDataUpdate();
            }
        }


    }
}