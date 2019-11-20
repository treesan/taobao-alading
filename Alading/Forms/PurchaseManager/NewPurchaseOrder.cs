using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Forms.Stock.Control;
using Alading.Entity;
using Alading.Business;
using Alading.Core.Enum;
using System.Text.RegularExpressions;

namespace Alading.Forms.PurchaseManager
{
    public partial class NewPurchaseOrder : DevExpress.XtraEditors.XtraForm
    {
        public DataTable dTable = new DataTable();
        public List<PurchaseProduct> purchaseProductList = new List<PurchaseProduct>();

        public NewPurchaseOrder()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            PuchaseDate.EditValue = System.DateTime.Now;
            dateEdit2.EditValue = System.DateTime.Now;
            textEdit2.EditValue = 0.00;
            textEdit3.EditValue = 0.00;

            // 对DataTable的初始化             产品>商品
            
            dTable.Columns.Add("IsDelete", System.Type.GetType("System.Boolean"));
            dTable.Columns.Add("Line",typeof(string));           
            dTable.Columns.Add("SkuOuterID", typeof(string));
            dTable.Columns.Add("ItemName", typeof(string));
            dTable.Columns.Add("ProductSpecification", typeof(string));
            dTable.Columns.Add("PurchaseNum",typeof(int));
            dTable.Columns.Add("ProductPrice",typeof(double));
            dTable.Columns.Add("DiscountRate",typeof(double));
            dTable.Columns.Add("PurchaseSum", typeof(double)); //采购金额
            dTable.Columns.Add("Remarks", typeof(string));
            DataRow row = dTable.NewRow();
           
            gridControl1.DataSource = dTable;

            this.SupplierName.Properties.NullText = "请选择...";
            List<Alading.Entity.Supplier> supplierList = new List<Alading.Entity.Supplier>();
            supplierList = SupplierService.GetAllSupplier();
            foreach (var supplier in supplierList)
            {
                SupplierName.Properties.Items.Add(supplier.SupplierName.ToString());
            }
            
        }

        /// <summary>
        ///  单击 “保存”触发的数据库操作事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            List<string> purchaseorderCodeList = new List<string>();
            decimal ToalFee = new decimal();
            /*********生成采购订单***********/
            Guid purchaseOrderCode = Guid.NewGuid(); 
            Alading.Entity.PurchaseOrder newPurchaseOrder = new Alading.Entity.PurchaseOrder();
            newPurchaseOrder.PurchaserCode = "AA" + purchaseOrderCode.ToString();//采购人编号
            newPurchaseOrder.PurchaseOrderCode = purchaseOrderCode.ToString();
            newPurchaseOrder.SupplierCode = SupplierService.GetSupplierByName(SupplierName.Text).SupplierCode;//供货商编号
            newPurchaseOrder.SupplierName = SupplierName.Text;//供货商姓名
            newPurchaseOrder.OrderStatus = 3;//订单状态  表示为执行
            newPurchaseOrder.PurchaserName = purchaserName.Text;//采购人姓名
            newPurchaseOrder.OrderTime = PuchaseDate.DateTime;//下订单时间
         
            //purchaseorderCodeList.Add(newPurchaseOrder.PurchaseOrderCode);
            /************生成采购单中的数据************/

            int selNum = 0; //
            int flag = 0; //判断该行是否被选上

            foreach (DataRow row in dTable.Rows)
            {
                Guid purchaseProductCode = Guid.NewGuid();// 每次循环都产生不同的PurchaseProductCode
                PurchaseProduct newPurchaseProduct = new PurchaseProduct();
                if (Convert.ToBoolean(row["IsDelete"]) == true)
                {
                    // 这个PurchaseOrder是数据库中间的表
                    newPurchaseProduct.PurchaseOrderCode = purchaseOrderCode.ToString();
                    newPurchaseProduct.PurchaseProductCode = purchaseProductCode.ToString();
                    newPurchaseProduct.ProductName = row["ItemName"].ToString();
                    newPurchaseProduct.SkuOuterId = row["SkuOuterID"].ToString();
                    string str=row["PurchaseSum"].ToString();
                    if (string.Empty.Equals(str))
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("你没有输入采购商品的数量", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        flag = 1;   //没输入数字就变成1
                        break;
                    }
                    else 
                    {
                        newPurchaseProduct.PurchasePrice = Convert.ToDecimal(row["ProductPrice"]);
                        newPurchaseProduct.PurchaseQuantity = Convert.ToDouble(row["PurchaseNum"]);
                        decimal PurchaseNum = (decimal)newPurchaseProduct.PurchaseQuantity;
                        newPurchaseProduct.PurchaseTotalFee = newPurchaseProduct.PurchasePrice * PurchaseNum;
                        ToalFee += newPurchaseProduct.PurchaseTotalFee;
                        newPurchaseProduct.ProductType = "aa";
                        newPurchaseProduct.PurchaseRemark = purchaseNotes.Text;

                        purchaseorderCodeList.Add(newPurchaseProduct.PurchaseOrderCode);
                        purchaseProductList.Add(newPurchaseProduct);
                        selNum++;
                        flag = 0;
                    }
                    
                }

            }
            //if (PurchaseProductService.GetPurchaseProductByCode(purchaseOrderCode.ToString()).Count == 0)
            if(selNum==0&&flag==0)   //
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("你没选择任何商品", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if(selNum!=0&&flag==0) 
            {
                ReturnType result_PurchaseOrder = new ReturnType();
                newPurchaseOrder.TotalFee = ToalFee;
                result_PurchaseOrder = PurchaseOrderService.AddPurchaseOrder(newPurchaseOrder);//保存订单
                ReturnType result_PurchaseProduct = new ReturnType();
                result_PurchaseProduct = PurchaseProductService.AddPurchaseProduct(purchaseProductList);//保存采购的商品
                if (result_PurchaseOrder == ReturnType.Success && result_PurchaseProduct == ReturnType.Success)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new Alading.Forms.PurchaseManager.PurchaseOrderList().GetPurchaseOrder(purchaseorderCodeList);
                }

                dTable.Rows.Clear();
                this.Close();
            }
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            dTable.Rows.Clear();
            this.Close();
        }

        /// <summary>
        ///  单击“选择商品”按钮触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)       
        {
            DataTable table = new DataTable();
            ProductSelected ps = new ProductSelected(table, null);
            ps.ShowDialog();
            //下面就是对当前DataTable的赋值
            if (gridControl1.DataSource != null)
            {
                dTable = gridControl1.DataSource as DataTable;
            }
            else {
                init();
            }
            int i = 1;
            foreach (DataRow row in table.Rows)
            {                
                DataRow dRow = dTable.NewRow();
                // 其余信息量是在商家初始化过程中或者添加商品的过程中加载的                
                dRow["IsDelete"] = false;                
                dRow["Line"] = i.ToString();
                dRow["SkuOuterID"] = row["SkuOuterID"];
                dRow["ItemName"] = row["Name"];
                dRow["ProductSpecification"] = row["Specification"];
                dRow["ProductPrice"] = 100.00;    
                dTable.Rows.Add(dRow);

                i++;                
            }
            gridControl1.DataSource = dTable;
        }

        /// <summary>
        ///   采购订单中商品选中操作  处理当单元格发生改变时候触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(e.RowHandle);
            if (e.Column == gvDelete)   // IsDelete单元格被选中
            {
                gridView1.BeginDataUpdate();
                row["IsDelete"] = e.Value;
                gridView1.EndDataUpdate();
            }            

            if (e.Column == gvNum)  // PurchaseNum单元格改变
            {
                gridView1.BeginDataUpdate();
                gvNum.ToolTip = "";
                Regex regex = new Regex("^[0-9]*[1-9][0-9]*$");
                //Regex regex = new Regex("[1-9]\\d*\\.\\d*|0\\.\\d*[1-9]\\d*");
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
                    gvNum.ToolTip = "商品数量必须为正整数,请重新输入";
                    DevExpress.XtraEditors.XtraMessageBox.Show("商品数量必须为正整数,请重新输入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                gridView1.EndDataUpdate();
            }

            if (e.Column == gvRate)  //DiscountRate单元格改变
            {
                gridView1.BeginDataUpdate();
                Regex regex = new Regex("^(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*))$"); //正浮点数  
                //Regex regex = new Regex("[1-9]\d*\.\d*|0\.\d*[1-9]\d*");
                bool result = regex.IsMatch(Convert.ToString(e.Value).Trim());
                if (result)
                {
                    double rate = Convert.ToDouble(e.Value);
                    if (rate <= 100.00 && rate >= 0.00)
                    {
                        row["DiscountRate"] = rate;
                    }
                    else
                    {
                        gvRate.ToolTip = "折扣率的范围是0~100,请重新输入";
                        DevExpress.XtraEditors.XtraMessageBox.Show("折扣率的范围是0~100,请重新输入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    gvRate.ToolTip = "输入的折扣率不合法,请重新输入";
                    DevExpress.XtraEditors.XtraMessageBox.Show("输入的折扣率不合法,请重新输入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                gridView1.EndDataUpdate();

            }
        }

        private void gridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只会对正整数的输入响应
            //if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            //{
            //    e.Handled = true;
            //}
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
     
    }
}