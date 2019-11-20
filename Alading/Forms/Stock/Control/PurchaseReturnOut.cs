using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Taobao;
using Alading.Entity;
using System.Collections;
using Alading.Business;
using Alading.Utils;
using Alading.Core.Enum;
using DevExpress.Utils;
using System.Linq;
using DevExpress.XtraTreeList;
using System.Globalization;

namespace Alading.Forms.Stock.Control
{
    public partial class PurchaseReturnOut : DevExpress.XtraEditors.XtraUserControl
    {
        public PurchaseReturnOut()
        {            
            InitializeComponent();
        }

        #region 全局变量
        InOutHelper inoutHelper = new InOutHelper();

        DataTable dTable = new DataTable();

        List<string> skuOuterIDList = new List<string>();
        #endregion

        #region 触发事件
        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PurchaseReturnOut_Load(object sender, EventArgs e)
        {
            try
            {
                dTable = new DataTable();
                inoutHelper = new InOutHelper();
                //防止重复加载同一条商品信息
                skuOuterIDList = new List<string>();

                //显示当前入库日期
                dateEditOutTime.Text = DateTime.Now.ToShortDateString();
                //到账日期
                dateEditIncomeTime.Text = DateTime.Now.ToShortDateString();
                //账期
                textEditPayTerm.Text = "0";

                //加载所有仓库
                inoutHelper.LoadAllHouse(repositoryItemComboBoxStockHouse);
                //加载付款方式
                inoutHelper.GetPayType(comboPayType);
                inoutHelper.AddTaxColumns(gcPurchaseReturnOut, dTable);
                gvPurchaseReturnOut.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 选择商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnProSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int oldRowCount = gvPurchaseReturnOut.RowCount;
                //用于存放从 选择商品 选择的StockProduct
                DataTable table = new DataTable();
                ProductSelected ps = new ProductSelected(table, null);
                ps.ShowDialog();

                //传递数据
                inoutHelper.SetColumnsValue(dTable, table, skuOuterIDList);
                /*显示税率*/
                inoutHelper.DisPlayTax(oldRowCount, gvPurchaseReturnOut, gcTaxCode, gcTax);
                //gcProSoldOut.DataSource = dTable.DefaultView;
                gvPurchaseReturnOut.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 删除列表中商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int rowHandle = gvPurchaseReturnOut.FocusedRowHandle;
                if (rowHandle < 0)
                {
                    XtraMessageBox.Show("没有可删除的数据", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (DialogResult.Yes == XtraMessageBox.Show("您确定要从列表中移除该商品吗？", Constants.SYSTEM_PROMPT
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    DataRow row = gvPurchaseReturnOut.GetFocusedDataRow();
                    string skuOuterID = row[gcSkuOuterID.FieldName].ToString();
                    if (skuOuterIDList.Contains(skuOuterID))
                    {
                        skuOuterIDList.Remove(skuOuterID);
                    }
                    gvPurchaseReturnOut.DeleteRow(rowHandle);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 改变列表的焦点行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchaseReturnOut_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gvPurchaseReturnOut.RowCount == 0)
                {
                    Clear();
                }
                inoutHelper.LoadLayoutAndProps(repositoryItemComboBoxStockLayout, gvPurchaseReturnOut
                    , categoryKeyProps, categorySaleProps, categoryNotKeyProps, categoryInputProps);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 改变仓库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemComboBoxStockHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBoxEdit combo = sender as ComboBoxEdit;
                gvPurchaseReturnOut.BeginUpdate();
                DataRow row = gvPurchaseReturnOut.GetFocusedDataRow();
                row[gcStockHouse.FieldName] = combo.Properties.Items[combo.SelectedIndex];
                Hashtable table = repositoryItemComboBoxStockHouse.Tag as Hashtable;
                row[gcStockHouseCode.FieldName] = table[combo.SelectedIndex];
                gvPurchaseReturnOut.BestFitColumns();
                gvPurchaseReturnOut.EndUpdate();

                //加载库位
                inoutHelper.LoadLayout(repositoryItemComboBoxStockLayout, table[combo.SelectedIndex].ToString());
            }
            catch (Exception ex)
            {
                gvPurchaseReturnOut.EndUpdate();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 选择库位
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
                DataRow row = gvPurchaseReturnOut.GetFocusedDataRow();
                row[gcStockLayout.FieldName] = combo.Properties.Items[combo.SelectedIndex];
                row[gcStockLayoutCode.FieldName] = table[combo.SelectedIndex];
                gvPurchaseReturnOut.BestFitColumns();
            }
            catch (Exception ex)
            {
                gvPurchaseReturnOut.EndUpdate();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 用条形码获取商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnGetPro_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bEditBarCode.EditValue != null && !string.IsNullOrEmpty(bEditBarCode.EditValue.ToString()))
            {
                WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                try
                {
                    bool isGet = inoutHelper.GetProByBarCode(bEditBarCode.EditValue.ToString(), gvPurchaseReturnOut, dTable);
                    waitFrm.Close();

                    if (!isGet)
                    {
                        XtraMessageBox.Show("找不到相应的商品", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    waitFrm.Close();
                    XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show("请输入条形码", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 根据选择的是税票还是收据隐藏和显示税率，税额，不含税金额等
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemRadioReturnOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RadioGroup radioGroup = (RadioGroup)sender;
                inoutHelper.RadioGroupSelectIndexChange(radioGroup, gcTax, gcTaxFee, gcTotalMoney, gcFeeNotContainsTax, "退款金额");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 单元格值变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchaseReturnOut_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                DataRow row = gvPurchaseReturnOut.GetDataRow(e.RowHandle);
                string taxCode = row[gcTaxCode.FieldName] != null ? row[gcTaxCode.FieldName].ToString() : string.Empty;
                if (e.Column == gcStockHouse)
                {
                    gvPurchaseReturnOut.BeginUpdate();
                    row[gcStockHouse.FieldName] = e.Value;
                    gvPurchaseReturnOut.EndUpdate();
                }
                else if (e.Column == gcStockLayout)
                {
                    gvPurchaseReturnOut.BeginUpdate();
                    row[gcStockLayout.FieldName] = e.Value;
                    gvPurchaseReturnOut.EndUpdate();
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
                    gvPurchaseReturnOut.BeginUpdate();
                    row[gcTotalCount.FieldName] = num;
                    row[gcTotalMoney.FieldName] = Math.Round(totalFee, 2);
                    row[gcTaxFee.FieldName] = Math.Round(taxFee, 2);
                    row[gcPrice.FieldName] = Math.Round(price, 2);
                    row[gcFeeNotContainsTax.FieldName] = Math.Round(feeNotContainsTax, 2);
                    gvPurchaseReturnOut.EndUpdate();
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

                    gvPurchaseReturnOut.BeginUpdate();
                    row[gcTax.FieldName] = tax;
                    row[gcTotalCount.FieldName] = num;
                    row[gcTotalMoney.FieldName] = Math.Round(totalFee, 2);
                    row[gcTaxFee.FieldName] = Math.Round(taxFee, 2);
                    row[gcPrice.FieldName] = Math.Round(price, 2);
                    row[gcFeeNotContainsTax.FieldName] = Math.Round(feeNotContainsTax, 2);
                    gvPurchaseReturnOut.EndUpdate();
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

                    gvPurchaseReturnOut.BeginUpdate();
                    row[gcTotalCount.FieldName] = num;
                    row[gcTotalMoney.FieldName] = Math.Round(totalFee, 2);
                    row[gcTaxFee.FieldName] = Math.Round(taxFee, 2);
                    row[gcPrice.FieldName] = Math.Round(price, 2);
                    row[gcFeeNotContainsTax.FieldName] = Math.Round(feeNotContainsTax, 2);
                    gvPurchaseReturnOut.EndUpdate();
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

                    gvPurchaseReturnOut.BeginUpdate();
                    row[gcTotalCount.FieldName] = num;
                    row[gcTotalMoney.FieldName] = Math.Round(totalFee, 2);
                    row[gcTaxFee.FieldName] = Math.Round(taxFee, 2);
                    row[gcPrice.FieldName] = Math.Round(price, 2);
                    row[gcFeeNotContainsTax.FieldName] = Math.Round(feeNotContainsTax, 2);
                    gvPurchaseReturnOut.EndUpdate();
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

                    gvPurchaseReturnOut.BeginUpdate();
                    row[gcTotalCount.FieldName] = num;
                    row[gcTotalMoney.FieldName] = Math.Round(totalFee, 2);
                    row[gcTaxFee.FieldName] = Math.Round(taxFee, 2);
                    row[gcPrice.FieldName] = Math.Round(price, 2);
                    row[gcFeeNotContainsTax.FieldName] = Math.Round(feeNotContainsTax, 2);
                    gvPurchaseReturnOut.EndUpdate();
                }

                //展示总金额
                GetTaxAndFee();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 保存
        /// </summary>
        public bool SaveData()
        {
            #region 赋值
            InOutData inoutData = new InOutData();
            inoutData.isIn = false;
            //出入库类型
            inoutData.InOutType = (int)InOutType.PurchaseReturnOut;
            //出入库状态
            inoutData.InOutStatus = (int)InOutStatus.AllSend;

            //出入库单号
            if (!string.IsNullOrEmpty(textEditInOutCode.Text))
            {
                inoutData.InOutCode = textEditInOutCode.Text;
                if (inoutHelper.ExistInOutCode(inoutData.InOutCode))
                {
                    XtraMessageBox.Show("此单号已存在", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textEditInOutCode.Focus();
                    return false;
                }
            }
            else
            {
                inoutData.InOutCode = Guid.NewGuid().ToString();
            }

            //出入库时间
            if (string.IsNullOrEmpty(dateEditOutTime.Text))
            {
                XtraMessageBox.Show("请填写入库日期", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateEditOutTime.Focus();
                return false;
            }
            inoutData.InOutDateTime = DateTime.Parse(dateEditOutTime.Text);

            //业务员及编号
            if (string.IsNullOrEmpty(pceOperator.Text))
            {
                XtraMessageBox.Show("请填写业务员", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pceOperator.Focus();
                return false;
            }
            inoutData.Operator = pceOperator.Text;
            inoutData.OperatorCode = pceOperator.Tag.ToString();

            //付款方式
            inoutData.PayType = comboPayType.SelectedIndex;
            //应付金额
            if (!string.IsNullOrEmpty(textEditNeedToPay.Text))
            {
                inoutData.NeedToPay = double.Parse(textEditNeedToPay.EditValue.ToString());
            }
            else
            {
                inoutData.NeedToPay = 0.0;
            }
            //本次付款金额
            if (!string.IsNullOrEmpty(textEditPayThisTime.Text))
            {
                inoutData.PayThisTime = double.Parse(textEditPayThisTime.EditValue.ToString());
            }
            else
            {
                inoutData.PayThisTime = 0.0;
            }
            //到账日期
            if (!string.IsNullOrEmpty(dateEditIncomeTime.Text))
            {
                inoutData.IncomeTime = DateTime.Parse(dateEditIncomeTime.Text);
            }
            else
            {
                inoutData.IncomeTime = DateTime.MinValue;
            }
            //合计金额
            if (!string.IsNullOrEmpty(textEditTotalFee.Text))
            {
                inoutData.TotalFee = double.Parse(textEditTotalFee.EditValue.ToString());
            }
            else
            {
                inoutData.TotalFee = 0.0;
            }
            //税率
            if (!string.IsNullOrEmpty(textEditTotalFee.Text))
            {
                inoutData.Tax = double.Parse(textEditTotalFee.EditValue.ToString());
            }
            else
            {
                inoutData.Tax = 0.0;
            }
            //税额
            if (!string.IsNullOrEmpty(textEditAmountTax.Text))
            {
                inoutData.AmountTax = double.Parse(textEditAmountTax.EditValue.ToString());
            }
            else
            {
                inoutData.AmountTax = 0.0;
            }
            //折扣金额
            if (!string.IsNullOrEmpty(textEditDiscountFee.Text))
            {
                inoutData.DiscountFee = double.Parse(textEditDiscountFee.EditValue.ToString());
            }
            else
            {
                inoutData.DiscountFee = 0.0;
            }
            //支付期限
            if (!string.IsNullOrEmpty(textEditPayTerm.Text))
            {
                inoutData.PayTerm = int.Parse(textEditPayTerm.Text);
            }

            //快递公司
            inoutData.FreightCompany = string.Empty;
            inoutData.FreightCode = string.Empty;
            #endregion

            return inoutHelper.Save(gvPurchaseReturnOut, inoutData);
        }

        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            gcPurchaseReturnOut.DataSource = null;

            comboPayType.SelectedIndex = 0;
            textEditNeedToPay.Text = string.Empty;
            textEditPayThisTime.Text = string.Empty;
            dateEditIncomeTime.Text = string.Empty;
            textEditTotalFee.Text = string.Empty;
            textEditAmountTax.Text = string.Empty;
            textEditDiscountFee.Text = string.Empty;
            textEditPayTerm.Text = string.Empty;
        }
        #endregion

        #region 购买退货单
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupPurchaseReturnOutBill_Popup(object sender, EventArgs e)
        {
            try
            {
                inoutHelper.ClickPopup(barEditPageNo, barBtnPurReturnOutFirstPage, barBtnPurReturnOutForwardPage, barBtnPurReturnOutNextPage, barBtnPurReturnOutLastPage
                    , barBtnPurReturnOutSkipPage, gcPurchaseReturnOut, gvPurchaseReturnOut, (int)InOutType.ProduceIn);
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
        private void barBtnPurReturnOutFirstPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadFirstPage(barEditPageNo, barBtnPurReturnOutFirstPage, barBtnPurReturnOutForwardPage, barBtnPurReturnOutNextPage, barBtnPurReturnOutLastPage
                    , barBtnPurReturnOutSkipPage, gcPurchaseReturnOut, gvPurchaseReturnOut, (int)InOutType.ProduceIn);
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
        private void barBtnPurReturnOutForwardPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadForwardPage(barEditPageNo, barBtnPurReturnOutFirstPage, barBtnPurReturnOutForwardPage, barBtnPurReturnOutNextPage, barBtnPurReturnOutLastPage
                    , barBtnPurReturnOutSkipPage, gcPurchaseReturnOut, gvPurchaseReturnOut, (int)InOutType.ProduceIn);
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
        private void barBtnPurReturnOutNextPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadNextPage(barEditPageNo, barBtnPurReturnOutFirstPage, barBtnPurReturnOutForwardPage, barBtnPurReturnOutNextPage, barBtnPurReturnOutLastPage
                    , barBtnPurReturnOutSkipPage, gcPurchaseReturnOut, gvPurchaseReturnOut, (int)InOutType.ProduceIn);
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
        private void barBtnPurReturnOutLastPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadLastPage(barEditPageNo, barBtnPurReturnOutFirstPage, barBtnPurReturnOutForwardPage, barBtnPurReturnOutNextPage, barBtnPurReturnOutLastPage
                    , barBtnPurReturnOutSkipPage, gcPurchaseReturnOut, gvPurchaseReturnOut, (int)InOutType.ProduceIn);
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
        private void barBtnPurReturnOutSkipPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadSkipPage(barEditPageNo, barBtnPurReturnOutFirstPage, barBtnPurReturnOutForwardPage, barBtnPurReturnOutNextPage, barBtnPurReturnOutLastPage
                    , barBtnPurReturnOutSkipPage, gcPurchaseReturnOut, gvPurchaseReturnOut, (int)InOutType.ProduceIn);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 业务员
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
                /*如果单击到单元格内*/
                if (hitInfo.HitInfoType == HitInfoType.Cell)
                {
                    this.pceOperator.Text = hitInfo.Node.GetDisplayText(0);
                    this.pceOperator.Tag = hitInfo.Node.Tag;
                    this.pceOperator.ClosePopup();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 到账时间
        /// <summary>
        /// 时间变化导致天数变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateEditIncomeTime_DateTimeChanged(object sender, EventArgs e)
        {
            try
            {
                inoutHelper.CalculateIncomeDays(dateEditIncomeTime, textEditPayTerm);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 天数变化导致时间变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEditPayTerm_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inoutHelper.CalculateInComeTime(dateEditIncomeTime, textEditPayTerm);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 保存金额信息
        public void GetTaxAndFee()
        {
            //应付应收款
            textEditNeedToPay.EditValue = inoutHelper.CalcTaxTotalFee(gvPurchaseReturnOut);
            //合计金额
            textEditTotalFee.EditValue = inoutHelper.CalcTaxTotalFee(gvPurchaseReturnOut);
            //税额
            textEditAmountTax.EditValue = inoutHelper.CalcAmountTax(gvPurchaseReturnOut);
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
            try
            {
                inoutHelper.PayThisTimeChange(textEditTotalFee, textEditNeedToPay, textEditDiscountFee, textEditPayThisTime);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 改变折扣金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEditDiscountFee_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inoutHelper.DisFeeChange(textEditTotalFee, textEditNeedToPay, textEditDiscountFee, textEditPayThisTime);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 响应条形码的回车事件
        private void repositoryItemBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextEdit textEdit = (TextEdit)sender;
                if (!string.IsNullOrEmpty(textEdit.Text))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    try
                    {
                        bool isGet = inoutHelper.GetProByBarCode(textEdit.Text, gvPurchaseReturnOut, dTable);
                        waitFrm.Close();

                        if (!isGet)
                        {
                            XtraMessageBox.Show("找不到相应的商品", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        waitFrm.Close();
                        XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    XtraMessageBox.Show("请输入条形码", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion
    }
}
