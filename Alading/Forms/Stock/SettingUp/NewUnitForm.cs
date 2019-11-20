using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;
using Alading.Entity;
using Alading.Business;
using Alading.Core.Enum;
using System.Linq;
using DevExpress.Utils;
using Alading.Taobao;

namespace Alading.Forms.Stock.SettingUp
{
    public partial class NewUnitForm : DevExpress.XtraEditors.XtraForm
    {
        //List<StockUnitGroup> groupList=new List<StockUnitGroup>();
        public NewUnitForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 计量单位组编码
        /// </summary>
        string stockgroupUnitCode;
        public NewUnitForm(string stockGroupUnitCode,string baseUnitName)
        {
            InitializeComponent();
            this.stockgroupUnitCode = stockGroupUnitCode;
            labelBaseUnit.Text = baseUnitName;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxGroup.Text))
            {
                XtraMessageBox.Show("请选择计量单位组！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textEditUnitName.Text))
            {
                XtraMessageBox.Show("请输入计量单位名称！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                textEditUnitName.Focus();
                return;
            }
            //验证比例为1-5位小数的正实数
            if (string.IsNullOrEmpty(textEditNum.Text) || textEditNum.Text=="0")
            {
                XtraMessageBox.Show("请输入正确的比例关系！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                textEditNum.Focus();
                return;
            }

            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                Alading.Entity.StockUnit stockUnit = new Alading.Entity.StockUnit();
                if (string.IsNullOrEmpty(textEditCode.Text) || string.IsNullOrEmpty(textEditCode.Text.Trim()))
                {
                    stockUnit.StockUnitCode = System.Guid.NewGuid().ToString();
                }
                else
                {
                    stockUnit.StockUnitCode = textEditCode.Text;
                }
                stockUnit.StockUnitName = textEditUnitName.Text;
                stockUnit.StockUnitSource = "手动新增";
                stockUnit.Conversion = float.Parse(textEditNum.Text);
                stockUnit.IsBaseUnit = false;
                stockUnit.StockUnitGroupCode = stockgroupUnitCode;
                stockUnit.Remark = memoExEditRemark.Text;
                if (StockUnitService.AddStockUnit(stockUnit) == ReturnType.Success)
                {
                    waitForm.Close();
                    XtraMessageBox.Show("计量单位添加成功！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    waitForm.Close();
                    XtraMessageBox.Show("计量单位添加失败！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButtonBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void NewUnitForm_Load(object sender, EventArgs e)
        {
            InitGroupUnit();
        }

        private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGroup.SelectedIndex != -1)
            {
                SortedList<int, SortedList<string, string>> tag = comboBoxGroup.Tag as SortedList<int, SortedList<string, string>>;
                if (tag != null && tag.Count > comboBoxGroup.SelectedIndex)
                {
                    if (tag[comboBoxGroup.SelectedIndex].Keys.Count > 0)
                    {
                        labelBaseUnit.Text = tag[comboBoxGroup.SelectedIndex].Values[0];
                        stockgroupUnitCode = tag[comboBoxGroup.SelectedIndex].Keys[0];
                    }
                }
            }
        }

        private void comboBoxGroup_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                GroupUnitForm unit = new GroupUnitForm();
                unit.ShowDialog();
                InitGroupUnit();
            }
        }

        private void InitGroupUnit()
        {
            comboBoxGroup.Properties.Items.Clear();
            List<StockUnitGroup> groupList = StockUnitGroupService.GetAllStockUnitGroup();
            if (groupList == null)
                return;
            int index = 0;
            SortedList<int, SortedList<string, string>> tag = new SortedList<int, SortedList<string, string>>();
            foreach (StockUnitGroup group in groupList)
            {
                comboBoxGroup.Properties.Items.Add(group.StockUnitGroupName);
                SortedList<string, string> value = new SortedList<string, string>();
                value.Add(group.StockUnitGroupCode,group.BaseUnit);
                tag.Add(index, value);
                index++;
            }
            comboBoxGroup.Tag = tag;
        }
    }
}