using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Business;
using Alading.Entity;
using System.Linq;
using Alading.Core.Enum;
using Alading.Taobao;
using DevExpress.Utils;

namespace Alading.Forms.Stock.SettingUp
{
    public partial class StockPropValueAdd : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 新增用
        /// </summary>
        /// <param name="stockCid"></param>
        /// <param name="stockCatName"></param>
        /// <param name="PropName"></param>
        /// <param name="PropPid"></param>
        public StockPropValueAdd(string stockCid, string stockCatName, string PropName, string PropPid)
        {
            InitializeComponent();
            this.stockCatName = stockCatName;
            this.stockCid = stockCid;
            this.PropName = PropName;
            this.PropPid = PropPid;
            textEditCatCode.Text = stockCid;
            textEditCatName.Text = stockCatName;
            textEditPid.Text = PropPid;
            textEditPropName.Text = PropName;
        }

        /// <summary>
        /// 修改用
        /// </summary>
        /// <param name="stockCid"></param>
        /// <param name="stockCatName"></param>
        /// <param name="PropName"></param>
        /// <param name="PropPid"></param>
        public StockPropValueAdd(string stockCid, string stockCatName, string PropName, string PropPid,string ValueName,string Vid)
        {
            InitializeComponent();
            this.Text = "修改属性值";
            this.stockCatName = stockCatName;
            this.stockCid = stockCid;
            this.PropName = PropName;
            this.PropPid = PropPid;
            this.Vid = Vid;
            this.ValueName = ValueName;
            textEditCatCode.Text = stockCid;
            textEditCatName.Text = stockCatName;
            textEditPid.Text = PropPid;
            textEditPropName.Text = PropName;
            textEditValueName.Text = ValueName;
            textEditVid.Text = Vid;
            textEditVid.Properties.ReadOnly = true;
            this.flag = false;

            btnAddAndNew.Visible = false;
            btnAddStockProp.Size = new Size(60, 23);
            btnCancel.Size = new Size(60, 23);
            btnAddStockProp.Location = new Point(220, 220);
            btnCancel.Location = new Point(320, 220);
        }

        /// <summary>
        /// 属性类目编码
        /// </summary>
        string stockCid = string.Empty;

        /// <summary>
        /// 属性类目名称
        /// </summary>
        string stockCatName = string.Empty;

        /// <summary>
        /// true为添加，false为修改
        /// </summary>
        bool flag = true;

        /// <summary>
        /// 属性名称
        /// </summary>
        string PropName = string.Empty;

        /// <summary>
        /// 属性pid
        /// </summary>
        string PropPid = string.Empty;

        /// <summary>
        /// 属性VID
        /// </summary>
        string Vid = string.Empty;

        /// <summary>
        /// 属性值名称
        /// </summary>
        string ValueName = string.Empty;

        /// <summary>
        /// 增加并新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAndNew_Click(object sender, EventArgs e)
        {
            if (AddNewStockProp())
            {
                textEditVid.Text = string.Empty;
                textEditValueName.Text = string.Empty;
            }
            else
            {

            }
        }

        /// <summary>
        /// 增加并关闭,或是修改保存关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddStockProp_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                if (AddNewStockProp())
                {
                    this.Close();
                }
                else
                {

                }
            }
            else
            {
                WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                waitForm.Show();
                try
                {
                    List<StockPropValue> list = StockPropValueService.GetStockPropValue(s => s.StockPid == PropPid && s.StockVid == Vid);
                    if (list.Count > 0)
                    {
                        StockPropValue spv = list.First();
                        spv.Name = textEditValueName.Text;
                        StockPropValueService.UpdateStockPropValue(spv);
                        waitForm.Close();
                        XtraMessageBox.Show("修改属性值失败！", Constants.SYSTEM_PROMPT);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    waitForm.Close();
                    XtraMessageBox.Show("修改属性值失败！", Constants.SYSTEM_PROMPT);
                }
            }
        }

        /// <summary>
        /// 增加新的PropValue
        /// </summary>
        /// <returns></returns>
        bool AddNewStockProp()
        {
            if (textEditValueName.Text == null || string.IsNullOrEmpty(textEditValueName.Text.Trim()))
            {
                XtraMessageBox.Show("请输入属性值名！", Constants.SYSTEM_PROMPT);
                textEditValueName.Focus();
                return false;
            }
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                string stockVid = textEditVid.Text;
                if (StockPropService.GetStockProp(s => s.StockPid == stockVid).Count > 0)
                {
                    waitForm.Close();
                    XtraMessageBox.Show("属性值编码与数据库中已有属性值编码相同，请重输！", Constants.SYSTEM_PROMPT);
                    textEditVid.Focus();
                    return false;
                }
                StockPropValue spv = new StockPropValue();
                spv.IsParent = false;
                spv.Name = textEditValueName.Text;
                spv.StockPid = PropPid;
                spv.PropName = PropName;
                spv.SortOrder = 0;
                spv.Status = string.Empty;
                spv.StockCid = stockCid;//
                spv.StockVid = stockVid;
                if (StockPropValueService.AddStockPropValue(spv) == ReturnType.Success)
                {
                    waitForm.Close();
                    XtraMessageBox.Show("添加属性值成功！", Constants.SYSTEM_PROMPT);
                    return true;
                }
                else
                {
                    waitForm.Close();
                    XtraMessageBox.Show("添加属性值失败！", Constants.SYSTEM_PROMPT);
                    return false;
                }
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show("添加属性值失败！", Constants.SYSTEM_PROMPT);
                return false;
            }
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}