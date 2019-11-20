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
using Alading.Core.Enum;
using Alading.Business;
using System.Collections;
using Alading.Utils;
using DevExpress.Utils;
using DevExpress.XtraTreeList;

namespace Alading.Forms.Stock.Control
{
    [ToolboxItem(false)]
    public partial class ProductSoldOut : DevExpress.XtraEditors.XtraUserControl
    {
        public ProductSoldOut()
        {
            InitializeComponent();
        }

        #region 全局变量
        InOutHelper inoutHelper;

        DataTable dTable;

        List<string> skuOuterIDList;
        #endregion

        #region 事件
        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductSoldOut_Load(object sender, EventArgs e)
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
                inoutHelper.AddTaxColumns(gcProSoldOut, dTable);
                gvProSoldOut.BestFitColumns();
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
        private void gvProSoldOut_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gvProSoldOut.RowCount == 0)
                {
                    Clear();
                }
                inoutHelper.LoadLayoutAndProps(repositoryItemComboBoxStockLayout, gvProSoldOut
                    , categoryKeyProps, categorySaleProps, categoryNotKeyProps, categoryInputProps);
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
        private void barBtnSoldOutSelectPro_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int oldRowCount = gvProSoldOut.RowCount;
                //用于存放从 选择商品 选择的StockProduct
                DataTable table = new DataTable();
                ProductSelected ps = new ProductSelected(table, null);
                ps.ShowDialog();

                //传递数据
                inoutHelper.SetColumnsValue(dTable, table, skuOuterIDList);
                inoutHelper.DisPlayTax(oldRowCount, gvProSoldOut, gcTaxCode, gcTax);
                //gcProSoldOut.DataSource = dTable.DefaultView;
                gvProSoldOut.BestFitColumns();
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
        private void barBtnProDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int rowHandle = gvProSoldOut.FocusedRowHandle;
                if (rowHandle < 0)
                {
                    XtraMessageBox.Show("没有可删除的数据", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (DialogResult.Yes == XtraMessageBox.Show("您确定要从列表中移除该商品吗？", Constants.SYSTEM_PROMPT
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    DataRow row = gvProSoldOut.GetFocusedDataRow();
                    string skuOuterID = row[gcSkuOuterID.FieldName].ToString();
                    if (skuOuterIDList.Contains(skuOuterID))
                    {
                        skuOuterIDList.Remove(skuOuterID);
                    }
                    gvProSoldOut.DeleteRow(rowHandle);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 选择仓库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemComboBoxStockHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBoxEdit combo = sender as ComboBoxEdit;
                gvProSoldOut.BeginUpdate();
                DataRow row = gvProSoldOut.GetFocusedDataRow();
                row[gcStockHouse.FieldName] = combo.Properties.Items[combo.SelectedIndex];
                Hashtable table = repositoryItemComboBoxStockHouse.Tag as Hashtable;
                row["HouseCode"] = table[combo.SelectedIndex];
                gvProSoldOut.BestFitColumns();
                gvProSoldOut.EndUpdate();

                //加载库位
                inoutHelper.LoadLayout(repositoryItemComboBoxStockLayout, table[combo.SelectedIndex].ToString());
            }
            catch (Exception ex)
            {
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
                DataRow row = gvProSoldOut.GetFocusedDataRow();
                row[gcStockLayout.FieldName] = combo.Properties.Items[combo.SelectedIndex];
                row["LayoutCode"] = table[combo.SelectedIndex];
                gvProSoldOut.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 根据条形码获取商品
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
                    bool isGet = inoutHelper.GetProByBarCode(bEditBarCode.EditValue.ToString(), gvProSoldOut, dTable);
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
        /// 计算税率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProSoldOut_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                DataRow row = gvProSoldOut.GetDataRow(e.RowHandle);
                string taxCode = row[gcTaxCode.FieldName] != null ? row[gcTaxCode.FieldName].ToString() : string.Empty;
                if (e.Column.Name == "gcTotalMoney")
                {
                    int num = row[gcCount.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalCount.FieldName].ToString()) ? int.Parse(row[gcTotalCount.FieldName].ToString()) : 1;
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
                        taxFee = inoutHelper.CaculateTaxFee(taxCode, tax, taxFee, totalFee);
                        feeNotContainsTax = totalFee - taxFee;
                        price = totalFee / num;
                    }

                    gvProSoldOut.BeginUpdate();
                    row[gcCount.FieldName] = num;
                    row[gcTotalMoney.FieldName] = totalFee;
                    row[gcTaxFee.FieldName] = taxFee;
                    row[gcPrice.FieldName] = price;
                    row[gcFeeNotContainsTax.FieldName] = feeNotContainsTax;
                    gvProSoldOut.EndUpdate();
                    //textEditTotalFee.Text = GetNeedToPay().ToString();
                }
                else if (e.Column.Name == "gcTax")
                {
                    int num = row[gcTotalCount.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalCount.FieldName].ToString()) ? int.Parse(row[gcTotalCount.FieldName].ToString()) : 1;
                    float totalFee = row[gcTotalMoney.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalMoney.FieldName].ToString()) ? int.Parse(row[gcTotalMoney.FieldName].ToString()) : 0;
                    float taxFee = row[gcTaxFee.FieldName] != null && !string.IsNullOrEmpty(row[gcTaxFee.FieldName].ToString()) ? float.Parse(row[gcTaxFee.FieldName].ToString()) : 0;
                    float tax = e.Value != null && e.Value.ToString() != string.Empty ? float.Parse(e.Value.ToString()) : 0;
                    float price = row[gcPrice.FieldName] != null && !string.IsNullOrEmpty(row[gcPrice.FieldName].ToString()) ? float.Parse(row[gcPrice.FieldName].ToString()) : 0;
                    float feeNotContainsTax = row[gcFeeNotContainsTax.FieldName] != null && !string.IsNullOrEmpty(row[gcFeeNotContainsTax.FieldName].ToString()) ? float.Parse(row[gcFeeNotContainsTax.FieldName].ToString()) : 0;

                    tax = tax / 100;
                    taxFee = inoutHelper.CaculateTaxFee(taxCode, tax, taxFee, totalFee);
                    feeNotContainsTax = totalFee - taxFee;

                    gvProSoldOut.BeginUpdate();
                    row[gcTax.FieldName] = tax;
                    row[gcCount.FieldName] = num;
                    row[gcTotalMoney.FieldName] = totalFee;
                    row[gcTaxFee.FieldName] = taxFee;
                    row[gcPrice.FieldName] = price;
                    row[gcFeeNotContainsTax.FieldName] = feeNotContainsTax;
                    gvProSoldOut.EndUpdate();
                    //textEditTotalFee.Text = GetNeedToPay().ToString();
                }
                else if (e.Column.Name == "gcCount")
                {
                    int num = e.Value != null && e.Value.ToString() != string.Empty ? int.Parse(e.Value.ToString()) : 1;
                    float totalFee = row[gcTotalMoney.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalMoney.FieldName].ToString()) ? int.Parse(row[gcTotalMoney.FieldName].ToString()) : 0;
                    float taxFee = row[gcTaxFee.FieldName] != null && !string.IsNullOrEmpty(row[gcTaxFee.FieldName].ToString()) ? float.Parse(row[gcTaxFee.FieldName].ToString()) : 0;
                    float tax = row[gcTax.FieldName] != null && !string.IsNullOrEmpty(row[gcTax.FieldName].ToString()) ? float.Parse(row[gcTax.FieldName].ToString()) : 0;
                    float price = row[gcPrice.FieldName] != null && !string.IsNullOrEmpty(row[gcPrice.FieldName].ToString()) ? float.Parse(row[gcPrice.FieldName].ToString()) : 0;
                    float feeNotContainsTax = row[gcFeeNotContainsTax.FieldName] != null && !string.IsNullOrEmpty(row[gcFeeNotContainsTax.FieldName].ToString()) ? float.Parse(row[gcFeeNotContainsTax.FieldName].ToString()) : 0;

                    totalFee = price * num;
                    taxFee = inoutHelper.CaculateTaxFee(taxCode, tax, taxFee, totalFee);
                    feeNotContainsTax = totalFee - taxFee;

                    gvProSoldOut.BeginUpdate();
                    row[gcCount.FieldName] = num;
                    row[gcTotalMoney.FieldName] = totalFee;
                    row[gcTaxFee.FieldName] = taxFee;
                    row[gcPrice.FieldName] = price;
                    row[gcFeeNotContainsTax.FieldName] = feeNotContainsTax;
                    gvProSoldOut.EndUpdate();
                }
                else if (e.Column.Name == "gcPrice")
                {
                    int num = row[gcTotalCount.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalCount.FieldName].ToString()) ? int.Parse(row[gcTotalCount.FieldName].ToString()) : 1;
                    float totalFee = row[gcTotalMoney.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalMoney.FieldName].ToString()) ? int.Parse(row[gcTotalMoney.FieldName].ToString()) : 0;
                    float taxFee = row[gcTaxFee.FieldName] != null && !string.IsNullOrEmpty(row[gcTaxFee.FieldName].ToString()) ? float.Parse(row[gcTaxFee.FieldName].ToString()) : 0;
                    float tax = row[gcTax.FieldName] != null && !string.IsNullOrEmpty(row[gcTax.FieldName].ToString()) ? float.Parse(row[gcTax.FieldName].ToString()) : 0;
                    float price = e.Value != null && e.Value.ToString() != string.Empty ? float.Parse(e.Value.ToString()) : 0; ;
                    float feeNotContainsTax = row[gcFeeNotContainsTax.FieldName] != null && !string.IsNullOrEmpty(row[gcFeeNotContainsTax.FieldName].ToString()) ? float.Parse(row[gcFeeNotContainsTax.FieldName].ToString()) : 0;

                    totalFee = price * num;
                    taxFee = inoutHelper.CaculateTaxFee(taxCode, tax, taxFee, totalFee);
                    feeNotContainsTax = totalFee - taxFee;

                    gvProSoldOut.BeginUpdate();
                    row[gcCount.FieldName] = num;
                    row[gcTotalMoney.FieldName] = totalFee;
                    row[gcTaxFee.FieldName] = taxFee;
                    row[gcPrice.FieldName] = price;
                    row[gcFeeNotContainsTax.FieldName] = feeNotContainsTax;
                    gvProSoldOut.EndUpdate();
                }
                else if (e.Column.Name == "gcFeeNotContainsTax")
                {
                    int num = row[gcCount.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalCount.FieldName].ToString()) ? int.Parse(row[gcTotalCount.FieldName].ToString()) : 1;
                    float totalFee = row[gcTotalMoney.FieldName] != null && !string.IsNullOrEmpty(row[gcTotalMoney.FieldName].ToString()) ? int.Parse(row[gcTotalMoney.FieldName].ToString()) : 0;
                    float taxFee = row[gcTaxFee.FieldName] != null && !string.IsNullOrEmpty(row[gcTaxFee.FieldName].ToString()) ? float.Parse(row[gcTaxFee.FieldName].ToString()) : 0;
                    float tax = row[gcTax.FieldName] != null && !string.IsNullOrEmpty(row[gcTax.FieldName].ToString()) ? float.Parse(row[gcTax.FieldName].ToString()) : 0;
                    float price = row[gcPrice.FieldName] != null && !string.IsNullOrEmpty(row[gcPrice.FieldName].ToString()) ? float.Parse(row[gcPrice.FieldName].ToString()) : 0;
                    float feeNotContainsTax = e.Value != null && e.Value.ToString() != string.Empty ? float.Parse(e.Value.ToString()) : 0; ;

                    /*如果税率为0，则含税金额=不含税金额*/
                    if (tax == 0)
                    {
                        totalFee = feeNotContainsTax;
                    }
                    else
                    {
                        Tax t = InOutHelper.alltaxList.Find(c => c.TaxCode == taxCode);
                        if (t != null)
                        {
                            if (t.Formula == 1)
                            {
                                totalFee = feeNotContainsTax * (1 + tax);
                            }
                            else if (t.Formula == 2)
                            {
                                totalFee = feeNotContainsTax / (1 - tax);
                            }
                        }
                        else
                        {
                            totalFee = feeNotContainsTax / (1 - tax);
                        }

                    }
                    /*税额=含税金额-不含税金额*/
                    taxFee = totalFee - feeNotContainsTax;
                    /*单价=含税金额/数量*/
                    price = totalFee / num;

                    gvProSoldOut.BeginUpdate();
                    row[gcTotalCount.FieldName] = num;
                    row[gcTotalMoney.FieldName] = totalFee;
                    row[gcTaxFee.FieldName] = taxFee;
                    row[gcPrice.FieldName] = price;
                    row[gcFeeNotContainsTax.FieldName] = feeNotContainsTax;
                    gvProSoldOut.EndUpdate();
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
            inoutData.InOutType = (int)InOutType.SaleOut;
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
            if (!string.IsNullOrEmpty(pceFreightCompany.Text))
            {
                inoutData.FreightCompany = pceFreightCompany.Text;
                inoutData.FreightCode = pceFreightCompany.Tag.ToString();
            }
            else
            {
                inoutData.FreightCompany = string.Empty;
                inoutData.FreightCode = string.Empty;
            }
            #endregion

            return inoutHelper.Save(gvProSoldOut, inoutData);
        }

        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            gcProSoldOut.DataSource = null;

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

        #region 销售单
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupContainerProduceIn_Properties_Popup(object sender, EventArgs e)
        {
            try
            {
                inoutHelper.ClickPopup(barEditPageNo, barBtnProSoldOutFirstPage, barBtnProSoldOutForwardPage, barBtnProSoldOutNextPage, barBtnProSoldOutLastPage
                    , barBtnProSoldOutSkipPage, gcProSoldOut, gvProSoldOut, (int)InOutType.SaleOut);
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
        private void barBtnProSoldOutFirstPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadFirstPage(barEditPageNo, barBtnProSoldOutFirstPage, barBtnProSoldOutForwardPage, barBtnProSoldOutNextPage, barBtnProSoldOutLastPage
                    , barBtnProSoldOutSkipPage, gcProSoldOut, gvProSoldOut, (int)InOutType.SaleOut);
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
        private void barBtnProSoldOutForwardPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadForwardPage(barEditPageNo, barBtnProSoldOutFirstPage, barBtnProSoldOutForwardPage, barBtnProSoldOutNextPage, barBtnProSoldOutLastPage
                    , barBtnProSoldOutSkipPage, gcProSoldOut, gvProSoldOut, (int)InOutType.SaleOut);
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
        private void barBtnProSoldOutNextPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadNextPage(barEditPageNo, barBtnProSoldOutFirstPage, barBtnProSoldOutForwardPage, barBtnProSoldOutNextPage, barBtnProSoldOutLastPage
                    , barBtnProSoldOutSkipPage, gcProSoldOut, gvProSoldOut, (int)InOutType.SaleOut);
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
        private void barBtnProSoldOutLastPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadLastPage(barEditPageNo, barBtnProSoldOutFirstPage, barBtnProSoldOutForwardPage, barBtnProSoldOutNextPage, barBtnProSoldOutLastPage
                    , barBtnProSoldOutSkipPage, gcProSoldOut, gvProSoldOut, (int)InOutType.SaleOut);
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
        private void barBtnProSoldOutSkipPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadSkipPage(barEditPageNo, barBtnProSoldOutFirstPage, barBtnProSoldOutForwardPage, barBtnProSoldOutNextPage, barBtnProSoldOutLastPage
                    , barBtnProSoldOutSkipPage, gcProSoldOut, gvProSoldOut, (int)InOutType.SaleOut);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 票据类型
        private void repositoryItemRadioSoldOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RadioGroup radioGroup = (RadioGroup)sender;
                inoutHelper.RadioGroupSelectIndexChange(radioGroup, gcTax, gcTaxFee, gcTotalMoney, gcFeeNotContainsTax, "销售金额");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 到账期限
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

        private void dateEditPayTerm_TextChanged(object sender, EventArgs e)
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

        #region 货运公司
        private void pceFreightCompany_Popup(object sender, EventArgs e)
        {
            try
            {
                inoutHelper.GetLogisticCompany(tlFreightCompany);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tlFreightCompany_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                TreeListHitInfo hitInfo = tlFreightCompany.CalcHitInfo(new Point(e.X, e.Y));
                /*如果单击到单元格内*/
                if (hitInfo.HitInfoType == HitInfoType.Cell)
                {
                    this.pceFreightCompany.Text = hitInfo.Node.GetDisplayText(0);
                    this.pceFreightCompany.Tag = hitInfo.Node.Tag;
                    this.pceFreightCompany.ClosePopup();
                }
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

        #region 客户
        private void pceConsumer_Popup(object sender, EventArgs e)
        {
            try
            {
                inoutHelper.GetConsumer(tlConsumer);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tlConsumer_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                TreeListHitInfo hitInfo = tlConsumer.CalcHitInfo(new Point(e.X, e.Y));
                /*如果单击到单元格内*/
                if (hitInfo.HitInfoType == HitInfoType.Cell)
                {
                    this.pceConsumer.Text = hitInfo.Node.GetDisplayText(0);
                    this.pceConsumer.Tag = hitInfo.Node.Tag;
                    this.pceConsumer.ClosePopup();
                }
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
            textEditNeedToPay.EditValue = inoutHelper.CalcTaxTotalFee(gvProSoldOut);
            //合计金额
            textEditTotalFee.EditValue = inoutHelper.CalcTaxTotalFee(gvProSoldOut);
            //税额
            textEditAmountTax.EditValue = inoutHelper.CalcAmountTax(gvProSoldOut);
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
                        bool isGet = inoutHelper.GetProByBarCode(textEdit.Text, gvProSoldOut, dTable);
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
