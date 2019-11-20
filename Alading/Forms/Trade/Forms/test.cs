using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Alading.Business;
using System.Data.OleDb;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Alading.Entity;
using DevExpress.XtraEditors;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Alading.Core.Enum;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Collections;
using DevExpress.XtraRichEdit.Utils;

namespace Alading.Forms.Trade.Forms
{
    public partial class test : Form
    {

        static ICacheManager cache = CacheFactory.GetCacheManager();
        public test()
        {
            InitializeComponent();
            checkedComboBoxEdit1.EditValueChanged -= new EventHandler(checkedComboBoxEdit1_EditValueChanged);   
            init();
        }

        private void init()
        {
            for (int i = 0; i < 10; i++)
            {
                this.checkedComboBoxEdit1.Properties.Items.Add(i.ToString());
            }

            string str = checkedComboBoxEdit1.Text;
            btnCheck.Enabled = false;
            //List<Alading.Entity.LogisticCompany> list = LogisticCompanyService.GetAllLogisticCompany();
            //lookUpEdit1.Properties.DataSource = list;
            ////list.Where
            //////lookUpEdit1.EditValue = list.First
            //Image a = Image.FromFile("d:\\");
            //List<string> string_list = new List<string> ();
            //for (int i = 0; i < 10;i++ )
            //{
            //    string_list.Add(i.ToString());
            //}
            //int sum = string_list.Sum();

           
            //gridcontrol1.DataSource = TradeService.GetAllTrade();
            //gridView1.BestFitColumns();
        }

        private DataTable ExecleDs(string filenameurl)
        {
            string strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + filenameurl + ";Extended Properties='Excel 8.0; HDR=YES; IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);

            OleDbDataAdapter odda = new OleDbDataAdapter("select * from [Sheet$]", conn);
            DataTable dt = new DataTable();
            odda.Fill(dt);
            return dt;
        }

        private void test_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“aladingDataSet.Trade”中。您可以根据需要移动或移除它。
            //this.tradeTableAdapter.Fill(this.aladingDataSet.Trade);
            this.listBox1.HorizontalScrollbar = true;
        }

        bool icon = true;
        //private void SetIndicator(int val, bool icon)
        //{
        //    spinEdit1.EditValue = val;
        //    this.icon = icon;
        //    gridView1.Invalidate();
        //}

        //private void radioGroup1_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    int val = (int)radioGroup1.EditValue;
        //    if (val == 0) SetIndicator(12, true);
        //    else if (val == 1) SetIndicator(50, false);
        //    else SetIndicator(55, true);
        //}
        ////..
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = "Row " + e.RowHandle.ToString();
                gridView1.Invalidate();
                e.Info.ImageIndex = 4;
            }
        }

        private DataTable creatData()
        {
            DataTable table = new DataTable("TradeInfo");
            table.Columns.Add("TradeInfoCode", typeof(string));
            table.Columns.Add("CustomTid", typeof(string));
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("Content", typeof(string));
            table.Columns.Add("AppendUserCode", typeof(string));
            table.Columns.Add("AppendUserName", typeof(string));
            table.Columns.Add("AppendDepartment", typeof(string));
            table.Columns.Add("AppendTime", typeof(string));

            for (int i = 0; i < 10; i++)
            {
                DataRow row = table.NewRow();
                row["TradeInfoCode"] = Guid.NewGuid().ToString();
                row["CustomTid"] = Guid.NewGuid().ToString();
                row["Title"] = Guid.NewGuid().ToString();
                row["Content"] = Guid.NewGuid().ToString();
                row["AppendUserCode"] = Guid.NewGuid().ToString();
                row["AppendUserName"] = Guid.NewGuid().ToString();
                row["AppendDepartment"] = Guid.NewGuid().ToString();
                row["AppendTime"] = System.DateTime.Now;
                table.Rows.Add(row);
            }
            return table;
        }


        private DataTable creatData1()
        {
            DataTable table = new DataTable("TradeInfo");
            table.Columns.Add("TradeInfoCode", typeof(string));
            table.Columns.Add("CustomTid", typeof(string));
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("Content", typeof(string));
            table.Columns.Add("AppendUserCode", typeof(string));
            table.Columns.Add("AppendUserName", typeof(string));
            table.Columns.Add("AppendDepartment", typeof(string));
            table.Columns.Add("AppendTime", typeof(string));

            for (int i = 0; i < 50000; i++)
            {
                DataRow row = table.NewRow();
                row["TradeInfoCode"] = Guid.NewGuid().ToString();
                row["CustomTid"] = Guid.NewGuid().ToString();
                row["Title"] = Guid.NewGuid().ToString();
                row["Content"] = Guid.NewGuid().ToString();
                row["AppendUserCode"] = Guid.NewGuid().ToString();
                row["AppendUserName"] = Guid.NewGuid().ToString();
                row["AppendDepartment"] = Guid.NewGuid().ToString();
                row["AppendTime"] = System.DateTime.Now;
                table.Rows.Add(row);
            }
            return table;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime time = System.DateTime.Now;
            DataTable table = creatData();
            if (TradeInfoService.AddTradeInfoSqlBulkCopy(table) == ReturnType.Success)
            {
                XtraMessageBox.Show("success!");
            }
            DateTime time1 = System.DateTime.Now;
            MessageBox.Show("I am not cache & use time:" + (time1 - time).ToString());
        }

        private void testXtra_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (testXtra.SelectedTabPage == tradeInfor)
            {
                DateTime time1 = System.DateTime.Now;
                List<Alading.Entity.TradeInfo> tradeInfoList = TradeInfoService.GetAllTradeInfo().ToList();
                TradeInfo.DataSource = tradeInfoList;
                DateTime time2 = System.DateTime.Now;
                MessageBox.Show("I am not cache & use time:" + (time2 - time1).ToString());
            }

            if (testXtra.SelectedTabPage == trade2)
            {
                DateTime time1 = System.DateTime.Now;
                List<Alading.Entity.TradeInfo> tradeInfoList = test.TradeInfoList;
                gridControl2.DataSource = tradeInfoList;
                DateTime time2 = System.DateTime.Now;
                MessageBox.Show("I am cache & use time:" + (time2 - time1).ToString());
            }
        }

        public static List<TradeInfo> TradeInfoList
        {
            get
            {
                if (cache["TradeInfoList"] != null)
                {
                    return cache["TradeInfoList"] as List<TradeInfo>;
                }
                else
                {
                    List<TradeInfo> TradeInfoList = TradeInfoService.GetAllTradeInfo().ToList();

                    //如果获得的对象为空，则说明连接数据库失败了。则需要退出系统。
                    if (TradeInfoList == null)
                    {
                        XtraMessageBox.Show("获取用户角色信息失败，系统必须强制关闭。");
                    }
                    else
                    {
                        cache.Add("TradeInfoList", TradeInfoList, CacheItemPriority.Normal, null, new AbsoluteTime(DateTime.Now.AddMinutes(1)));
                    }
                    return TradeInfoList;
                }
            }
            set
            {
                //20分钟后失效
                cache.Add("TradeInfoList", value, CacheItemPriority.Normal, null, new AbsoluteTime(DateTime.Now.AddMinutes(1)));
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //if (TradeInfoService.AddTradeInfoSqlBulkCopy(table) == ReturnType.Success)
            //{
            //    XtraMessageBox.Show("success!");
            //}

            //sw.Stop();

            //XtraMessageBox.Show(sw.Elapsed.ToString());
        }

        private void gridView3_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void importIn_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable("TradeInfo");
            table.Columns.Add("TradeInfoCode", typeof(string));
            table.Columns.Add("CustomTid", typeof(string));
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("Content", typeof(string));
            table.Columns.Add("AppendUserCode", typeof(string));
            table.Columns.Add("AppendUserName", typeof(string));
            table.Columns.Add("AppendDepartment", typeof(string));
            table.Columns.Add("time", typeof(string));
            table = ExecleDs("D:\\a.xls");
            TradeInfoService.AddTradeInfoSqlBulkCopy(table);
            if (TradeInfoService.AddTradeInfoSqlBulkCopy(table) == ReturnType.Success)
            {
                XtraMessageBox.Show("success!");
            }
        }

        private void outport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = saveFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                XlsExportOptions options = new XlsExportOptions();
                gridView3.ExportToXls(saveFileDialog.FileName, options);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string str = "颜色:酒红色;尺码:39";
            string str1 = "颜色:酒红色;尺码:37";
            List<string> str_list1 = StrCom(str);
            List<string> str_list2 = StrCom(str);
            int i;
            int j;
            if (str_list1.Count != str_list2.Count)
            {
                return;
            }
            int count = str_list1.Count;
            for (i = 0; i < count;i++ )
            {
                if (str_list1[i] != str_list2[i])
                {
                    break;
                }
            }
            if (i >= count)
            {
                XtraMessageBox.Show("success!");
            }
        }

        public List<string> StrCom(string str_pro)
        {
            List<string> str_list = new List<string>();
            string[] str1 = null;
            str1 = str_pro.Split(';');
            foreach (string str in str1)
            {
                str_list.Add(str);
            }
            str_list.Sort();
            return str_list;
        }

        private void listBoxControl1_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            //Brush FontBrush = null;
            //ListBox listBox = sender as ListBox;
            ////listBox.DrawMode = DrawMode.OwnerDrawFixed;
            //if (e.Index > -1)
            //{
            //    if (listBox.Items[e.Index].ToString().Contains("你好"))
            //    {
            //        FontBrush = Brushes.Red;
            //    }
            //    else
            //    {
            //        FontBrush = Brushes.Black;
            //    }
            //    e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.f, FontBrush, e.Bounds);
            //}  
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush FontBrush = null;
            ListBox listBox = sender as ListBox;
            //listBox.DrawMode = DrawMode.OwnerDrawFixed;
            if (e.Index > -1)
            {
                if (listBox.Items[e.Index].ToString().Contains("你好"))
                {
                    FontBrush = Brushes.Red;
                }
                else
                {
                    FontBrush = Brushes.Black;
                }
                e.DrawBackground();
                e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, FontBrush, e.Bounds);
                e.DrawFocusRectangle();
            }  
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //string str = "颜色:红色;尺寸:37";
            //string str1 = Resolve(str);

            int? a = 3;
            int? b = 4;
            int? c = null;
            int? d = null;
            int i = c.Value - d.Value;
        }
        private string Resolve(string skuPros)
        {
            List<string> str_list = new List<string>();
            string[] str1 = null;
            StringBuilder strBuilder = new StringBuilder();
            str1 = skuPros.Split(';');
            foreach (string str in str1)
            {
                str_list.Add(str);
            }
            str_list.Sort();
            foreach (string str in str_list)
            {
                strBuilder.Append(str);
                strBuilder.Append(";");
            }
            return strBuilder.ToString().Substring(0, strBuilder.ToString().Length - 1);
        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            checkedComboBoxEdit1.EditValueChanged += new EventHandler(checkedComboBoxEdit1_EditValueChanged);   
            MessageBox.Show("hello");
        }

        private void checkedComboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
           if (checkedComboBoxEdit1.EditValue != string.Empty || checkedComboBoxEdit1.EditValue != "")
           {
               btnCheck.Enabled = true;
           }
           else
           {
               MessageBox.Show("请选择店铺");
               btnCheck.Enabled = false;
           }
        }

        private void checkedComboBoxEdit1_TextChanged(object sender, EventArgs e)
        {
            //btnCheck.Enabled = true;
        }
    }
}
