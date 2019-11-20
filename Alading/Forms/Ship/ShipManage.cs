using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;
using System.Linq;
using Alading.Business;
using Alading.Core.Enum;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using Alading.Taobao;
using Alading.Core.Enum;
//using httpget;
using DevExpress.Utils;
using System.Xml;
using System.Xml.XPath;
using Sgml;
using System.IO;
using Express_Query;

namespace Alading.Forms.Ship
{
    public partial class ShipManage : DevExpress.XtraEditors.XtraForm
    {
        private DataSet _currentDataSet;
        public ShipManage()
        {   
            InitializeComponent();
        }

        private void gridControlAll_Load(object sender, EventArgs e)
        {
            //List<Alading.Entity.Trade> newOrderList = TradeService.GetTrade(c => c.status == TradeStatus.WAIT_BUYER_CONFIRM_GOODS);
            List<Alading.Entity.Trade> newOrderList = TradeService.GetAllTrade();
            //LogisticCompanyService.GetLogisticCompany(p=>p.code==focusedTrade.LogisticCompanyCode).FirstOrDefault().name
            //Alading.Entity.Trade tt = new Alading.Entity.Trade();
            //tt.ConsignStatus
            gridControlAll.DataSource = newOrderList;
            gridView1.BestFitColumns();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void simpleQuery_Click(object sender, EventArgs e)
        {

            string shipNum = "";
            if (textEdit1.Text != "")
            {
                WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                shipNum = textEdit1.Text.ToString();
                //ShunfengQuery sn = new ShunfengQuery("023044268555");//(shipNum);023044268555
                //ResultInfo back = sn.query();
                ResultInfo back = ExpressQuery.Query("ShengTong", "368009597493");//("yuantong", "2221570574");//("ShunFeng", "023044268555");//("ShengTong","368009597493");
                addGridviewList(back);
                waitFrm.Close();
                //YuantongEmail email = new YuantongEmail("2221570574");
                //ResultInfo yuant = email.query();
            }
            else
                XtraMessageBox.Show("请输入物流单号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //YuantongEmail email = new YuantongEmail("2221570574");
            //ResultInfo yuant = email.query();
            
        }
        private void addGridviewList(ResultInfo result)
        {
            DataTable table = new DataTable();
            table.Columns.Add("listTime");
            table.Columns.Add("listRecord");
            if (result.trackList.Count == 0)
            {
                //DataRow row = table.NewRow();
                //row[0] = DateTime.Now.TimeOfDay.ToString();
                //row[1] = "输入单号有误或暂时无记录！";
                //table.Rows.Add(row);
                XtraMessageBox.Show("输入物流单号有误或无记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            }
            else
            {
                for (int i = 0; i < result.trackList.Count; i++)
                {
                    DataRow row = table.NewRow();
                    //gridViewList.SetRowCellValue(i, "listTime", result.trackList[i].ToString());//listRecord
                    trackNode temp = (trackNode)result.trackList[i];
                    row[0] = temp.trackTime;
                    row[1] = temp.trackStatus;
                    if (!string.IsNullOrEmpty(temp.trackTime) && !string.IsNullOrEmpty(temp.trackStatus))
                    {
                        table.Rows.Add(row);
                    }
                }
            }
                gridControlList.DataSource = table.DefaultView;
                gridControlList.Refresh();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string shipNum = textEdit2.Text;//http://115.238.55.94:8081/result.asp?wen=368009597493
            string webUrl="http://115.238.55.94:8081/result.asp?wen="+shipNum;
            webBrowser1.Navigate(webUrl);

            webBrowser1.Refresh();
        }
    }
}