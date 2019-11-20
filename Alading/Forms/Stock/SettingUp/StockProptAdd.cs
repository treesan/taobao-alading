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
using Alading.Core.Enum;
using System.Linq;
using Alading.Taobao;
using DevExpress.Utils;

namespace Alading.Forms.Stock.SettingUp
{
    public partial class StockPropAdd : DevExpress.XtraEditors.XtraForm
    {
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
        /// 是否增加子属性,true为增加子属性，false为增加普通属性
        /// </summary>
        bool sonFlag = false;

        /// <summary>
        /// 父属性名称
        /// </summary>
        string fPropName = string.Empty;

        /// <summary>
        /// 父属性pid
        /// </summary>
        string fPropPid = string.Empty;

        /// <summary>
        /// 父属性值名称
        /// </summary>
        string fValueName = string.Empty;

        /// <summary>
        /// 父属性值vid
        /// </summary>
        string fVid = string.Empty;

        /// <summary>
        /// 修改的pid
        /// </summary>
        string pid = string.Empty;

        /// <summary>
        /// 修改的属性名
        /// </summary>
        string pName = string.Empty;

        /// <summary>
        /// 添加用
        /// </summary>
        /// <param name="stockCid"></param>
        /// <param name="stockCatName"></param>
        /// <param name="fPropName"></param>
        /// <param name="fPropPid"></param>
        /// <param name="fValueName"></param>
        /// <param name="fVid"></param>
        public StockPropAdd(string stockCid, string stockCatName, string fPropName, string fPropPid, string fValueName, string fVid,bool sonFlag)
        {
            InitializeComponent();
            this.stockCid = stockCid;
            this.stockCatName = stockCatName;
            this.flag = true;
            this.fPropName = fPropName;
            this.fPropPid = fPropPid;
            this.fValueName = fValueName;
            this.fVid = fVid;
            this.sonFlag = sonFlag;
            textEditCatCode.Text = stockCid;
            textEditCatName.Text = stockCatName;
            textEditFPid.Text = fPropPid;
            textEditFPropName.Text = fPropName;
            textEditFValueName.Text = fValueName;
            textEditFVid.Text = fVid;
            textEditPropName.Focus();
        }

        /// <summary>
        /// 修改用
        /// </summary>
        /// <param name="stockCid"></param>
        /// <param name="stockCatName"></param>
        /// <param name="fPropName"></param>
        /// <param name="fPropPid"></param>
        /// <param name="fValueName"></param>
        /// <param name="fVid"></param>
        /// <param name="pid"></param>
        /// <param name="pName"></param>
        public StockPropAdd(string stockCid, string stockCatName, string fPropName, string fPropPid, string fValueName, string fVid,string pid,string pName)
        {
            InitializeComponent();
            this.Text = "修改属性";
            this.stockCid = stockCid;
            this.stockCatName = stockCatName;
            this.flag = false;
            this.fPropName = fPropName;
            this.fPropPid = fPropPid;
            this.fValueName = fValueName;
            this.fVid = fVid;
            this.pid = pid;
            this.pName = pName;
            textEditCatCode.Text = stockCid;
            textEditCatName.Text = stockCatName;
            textEditFPid.Text = fPropPid;
            textEditFPropName.Text = fPropName;
            textEditFValueName.Text = fValueName;
            textEditFVid.Text = fVid;
            textEditPropPid.Text = pid;
            textEditPropName.Text = pName;
            textEditPropPid.Properties.ReadOnly = true;
            textEditPropName.Focus();
            btnAddAndNew.Visible = false;
            btnAddStockProp.Size = new Size(60, 23);
            btnCancel.Size = new Size(60, 23);
            btnAddStockProp.Location = new Point(220, 220);
            btnCancel.Location = new Point(320, 220);
        }

        /// <summary>
        /// 添加
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
                    this.DialogResult = DialogResult.OK;
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
                    List<StockProp> list = StockPropService.GetStockProp(s => s.StockPid == pid);
                    if (list.Count > 0)
                    {
                        StockProp sp = list.First();
                        sp.Name = textEditPropName.Text;
                        if (StockPropService.UpdateStockProp(sp) == ReturnType.Success)
                        {
                            waitForm.Close();
                            XtraMessageBox.Show("修改成功！", Constants.SYSTEM_PROMPT);
                            this.Close();
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            waitForm.Close();
                            XtraMessageBox.Show("修改失败！", Constants.SYSTEM_PROMPT);
                        }
                    }
                    else
                    {
                        waitForm.Close();
                        XtraMessageBox.Show("找不到该属性信息，修改失败！", Constants.SYSTEM_PROMPT);
                    }
                }
                catch (Exception ex)
                {
                    waitForm.Close();
                    XtraMessageBox.Show("修改失败！", Constants.SYSTEM_PROMPT);
                }
            }
        }

        /// <summary>
        /// 添加StockProp
        /// </summary>
        /// <returns></returns>
        bool AddNewStockProp()
        {
            if (textEditPropName.Text == null || string.IsNullOrEmpty(textEditPropName.Text.Trim()))
            {
                XtraMessageBox.Show("请输入属性名称！", Constants.SYSTEM_PROMPT);
                textEditPropName.Focus();
                return false;
            }
            if (textEditCatCode.Text == null || string.IsNullOrEmpty(textEditCatCode.Text.Trim()))
            {
                XtraMessageBox.Show("请输入属性编码！", Constants.SYSTEM_PROMPT);
                textEditCatCode.Focus();
                return false;
            }
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                string stockPid = textEditPropPid.Text;
                if (StockPropService.GetStockProp(s => s.StockPid == stockPid).Count > 0)
                {
                    waitForm.Close();
                    XtraMessageBox.Show("属性编码与数据库中已有编码重复，请重输！", Constants.SYSTEM_PROMPT);
                    textEditCatCode.Focus();
                    return false;
                }
                StockProp sp = new StockProp();
                sp.Name = textEditPropName.Text;
                sp.StockPid = stockPid;
                sp.ParentPid = fPropPid;
                sp.ParentVid = fVid;
                sp.PropValues = string.Empty;
                sp.Status = string.Empty;//
                sp.StockCid = stockCid;
                if (StockPropService.AddStockProp(sp) == ReturnType.Success)
                {
                    if (sonFlag)
                    {
                        List<StockPropValue> list = StockPropValueService.GetStockPropValue(s => s.StockPid == fPropPid && s.StockVid == fVid);
                        if (list.Count > 0)
                        {
                            StockPropValue spv = list.First();
                            spv.IsParent = sonFlag;
                            StockPropValueService.UpdateStockPropValue(spv);
                        }
                    }
                    waitForm.Close();
                    XtraMessageBox.Show("添加属性成功！", Constants.SYSTEM_PROMPT);
                    return true;
                }
                else
                {
                    waitForm.Close();
                    XtraMessageBox.Show("添加属性失败！", Constants.SYSTEM_PROMPT);
                    return false;
                }
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show("添加属性失败！", Constants.SYSTEM_PROMPT);
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
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 添加并新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAndNew_Click(object sender, EventArgs e)
        {
            if (AddNewStockProp())
            {
                textEditPropPid.Text = string.Empty;
                textEditPropName.Text = string.Empty;
                textEditPropName.Focus();
            }
            else
            {

            }
        }
    }
}