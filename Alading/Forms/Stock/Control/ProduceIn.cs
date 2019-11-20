using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Business;
using Alading.Entity;
using Alading.Utils;
using System.Collections;
using Alading.Core.Enum;
using Alading.Taobao;
using DevExpress.Utils;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;

namespace Alading.Forms.Stock.Control
{
    [ToolboxItem(false)]
    public partial class ProduceIn : DevExpress.XtraEditors.XtraUserControl
    {
        public ProduceIn()
        {
            InitializeComponent();
        }

        #region 全局变量
        DataTable dTable;

        InOutHelper inoutHelper;
        //防止重复加载同一条商品信息
        List<string> skuOuterIDList;
        #endregion

        #region 点击控件触发事件

        /// <summary>
        /// 加载主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProduceIn_Load(object sender, EventArgs e)
        {
            try
            {
                dTable = new DataTable();
                inoutHelper = new InOutHelper();
                //防止重复加载同一条商品信息
                skuOuterIDList = new List<string>();

                //显示当前入库日期
                dateEditInTime.Text = DateTime.Now.ToShortDateString();
                //显示到账时间
                dateEditIncomeTime.Text = DateTime.Now.ToShortDateString();
                textEditPayTerm.Text = "0";

                //加载所有仓库
                inoutHelper.LoadAllHouse(repositoryItemComboBoxStockHouse);
                //加载付款方式
                inoutHelper.GetPayType(comboPayType);

                inoutHelper.AddColumns(gcProduceInProduct, dTable);
                gvProduceInProduct.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 焦点行改变触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProduceInProduct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                inoutHelper.LoadLayoutAndProps(repositoryItemComboBoxStockLayout, gvProduceInProduct
                    , categoryKeyProps, categorySaleProps, categoryNotKeyProps, categoryInputProps);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击选择商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnProductAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //用于存放从 选择商品 选择的StockProduct
                DataTable table = new DataTable();
                ProductSelected ps = new ProductSelected(table, null);
                ps.ShowDialog();

                //传递数据
                inoutHelper.SetColumnsValue(dTable, table, skuOuterIDList);
                //gcProduceInProduct.DataSource = dTable;
                gvProduceInProduct.BestFitColumns();
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
                gvProduceInProduct.BeginUpdate();
                DataRow row = gvProduceInProduct.GetFocusedDataRow();
                row[gcStockHouse.FieldName] = combo.Properties.Items[combo.SelectedIndex];
                Hashtable table = repositoryItemComboBoxStockHouse.Tag as Hashtable;
                row["HouseCode"] = table[combo.SelectedIndex];
                gvProduceInProduct.BestFitColumns();
                gvProduceInProduct.EndUpdate();

                //加载库位
                inoutHelper.LoadLayout(repositoryItemComboBoxStockLayout, table[combo.SelectedIndex].ToString());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 库位选择
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
                DataRow row = gvProduceInProduct.GetFocusedDataRow();
                row[gcStockLayout.FieldName] = combo.Properties.Items[combo.SelectedIndex];
                row["LayoutCode"] = table[combo.SelectedIndex];
                gvProduceInProduct.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int rowHandle = gvProduceInProduct.FocusedRowHandle;
                if (rowHandle < 0)
                {
                    XtraMessageBox.Show("没有可删除的数据", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (DialogResult.Yes == XtraMessageBox.Show("您确定要从列表中移除该商品吗？", Constants.SYSTEM_PROMPT
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    DataRow row = gvProduceInProduct.GetFocusedDataRow();
                    string skuOuterID = row[gcSkuOuterID.FieldName].ToString();
                    if (skuOuterIDList.Contains(skuOuterID))
                    {
                        skuOuterIDList.Remove(skuOuterID);
                    }
                    gvProduceInProduct.DeleteRow(rowHandle);
                }
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
                    bool isGet = inoutHelper.GetProByBarCode(bEditBarCode.EditValue.ToString(), gvProduceInProduct, dTable);
                    waitFrm.Close();

                    if (!isGet)
                    {
                        XtraMessageBox.Show("找不到相应的商品", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
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

        #endregion

        #region 生产单
        /// <summary>
        /// 加载生产单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupProduceInBill_Popup(object sender, EventArgs e)
        {
            try
            {
                inoutHelper.ClickPopup(barEditPageNo, barBtnProduceBillFirstPage, barBtnProduceBillForwardPage, barBtnProduceBillNextPage, barBtnProduceBillLastPage
                    , barBtnProduceBillSkipPage, gcProduceInProduct, gvProduceInProduct, (int)InOutType.ProduceIn);
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
        private void barBtnProduceBillFirstPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadFirstPage(barEditPageNo, barBtnProduceBillFirstPage, barBtnProduceBillForwardPage, barBtnProduceBillNextPage, barBtnProduceBillLastPage
                    , barBtnProduceBillSkipPage, gcProduceInProduct, gvProduceInProduct, (int)InOutType.ProduceIn);
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
        private void barBtnProduceBillForwardPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadForwardPage(barEditPageNo, barBtnProduceBillFirstPage, barBtnProduceBillForwardPage, barBtnProduceBillNextPage, barBtnProduceBillLastPage
                    , barBtnProduceBillSkipPage, gcProduceInProduct, gvProduceInProduct, (int)InOutType.ProduceIn);
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
        private void barBtnProduceBillNextPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadNextPage(barEditPageNo, barBtnProduceBillFirstPage, barBtnProduceBillForwardPage, barBtnProduceBillNextPage, barBtnProduceBillLastPage
                    , barBtnProduceBillSkipPage, gcProduceInProduct, gvProduceInProduct, (int)InOutType.ProduceIn);
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
        private void barBtnProduceBillLastPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadLastPage(barEditPageNo, barBtnProduceBillFirstPage, barBtnProduceBillForwardPage, barBtnProduceBillNextPage, barBtnProduceBillLastPage
                    , barBtnProduceBillSkipPage, gcProduceInProduct, gvProduceInProduct, (int)InOutType.ProduceIn);
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
        private void barBtnProduceBillSkipPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                inoutHelper.LoadLastPage(barEditPageNo, barBtnProduceBillFirstPage, barBtnProduceBillForwardPage, barBtnProduceBillNextPage, barBtnProduceBillLastPage
                    , barBtnProduceBillSkipPage, gcProduceInProduct, gvProduceInProduct, (int)InOutType.ProduceIn);
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
            inoutData.isIn = true;
            //出入库类型
            inoutData.InOutType = (int)InOutType.ProduceIn;
            //出入库状态
            inoutData.InOutStatus = (int)InOutStatus.AllReach;

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
            if (string.IsNullOrEmpty(dateEditInTime.Text))
            {
                XtraMessageBox.Show("请填写入库日期", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateEditInTime.Focus();
                return false;
            }
            inoutData.InOutDateTime = DateTime.Parse(dateEditInTime.Text);

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
                inoutData.PayThisTime=0.0;
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
                inoutData.TotalFee =0.0;
            }
            //税率
            inoutData.Tax = 0.0;
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

            return inoutHelper.Save(gvProduceInProduct, inoutData);
        }

        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            gcProduceInProduct.DataSource = null;

            comboPayType.SelectedIndex=0;
            textEditNeedToPay.Text = string.Empty;
            textEditPayThisTime.Text = string.Empty;
            dateEditIncomeTime.Text = string.Empty;
            textEditTotalFee.Text = string.Empty;
            textEditAmountTax.Text = string.Empty;
            textEditDiscountFee.Text = string.Empty;
            textEditPayTerm.Text= string.Empty;
        }
        #endregion

        #region 业务员相关
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

        #region 计算金额
        private void gvProduceInProduct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                //计算金额
                inoutHelper.CalcTotlaFee(gvProduceInProduct);
                //显示总金额
                textEditTotalFee.EditValue=inoutHelper.CalcTaxTotalFee(gvProduceInProduct);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 计算到账日期
        private void dateEditIncomeTime_DateTimeChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateEditIncomeTime.DateTime < DateTime.Now.Date)
                {
                    dateEditIncomeTime.DateTime = DateTime.Parse(DateTime.Now.ToShortDateString());
                }
                else if (dateEditIncomeTime.DateTime > DateTime.Now.Date.AddDays(999))
                {
                    dateEditIncomeTime.DateTime = DateTime.Parse(DateTime.Now.Date.AddDays(999).ToShortDateString());
                }
                textEditPayTerm.Text = (dateEditIncomeTime.DateTime.Date - DateTime.Now.Date).Days.ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
            
        private void textEditPayTerm_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textEditPayTerm.Text))
                {
                    int day = int.Parse(textEditPayTerm.Text);
                    dateEditIncomeTime.DateTime = DateTime.Parse(DateTime.Now.Date.AddDays(day).ToShortDateString());
                }
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
                        bool isGet = inoutHelper.GetProByBarCode(textEdit.Text, gvProduceInProduct, dTable);
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
