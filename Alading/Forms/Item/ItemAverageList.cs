using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using Alading.Business;
using DevExpress.XtraTreeList;
using Alading.Utils;
using DevExpress.Utils;
using Alading.Taobao;
using System.Collections;
using DevExpress.XtraScheduler;
using System.Linq;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API;

namespace Alading.Forms.Item
{
    public partial class ItemAverageList : DevExpress.XtraEditors.XtraForm
    {
        int itemNum;
        /// <summary>
        /// 商品Cid
        /// </summary>
        string sellercid = string.Empty;
        /// <summary>
        /// 买家昵称
        /// </summary>
        string nick = string.Empty;

        /// <summary>
        /// 全局变量，用来记录加入上架列表的宝贝
        /// </summary>
        SortedList<string, Dictionary<string, string>> g_AutoListDic = new SortedList<string, Dictionary<string, string>>();

        public ItemAverageList()
        {
            InitializeComponent();
        }

        private void ItemAuto_Load(object sender, EventArgs e)
        {
            try
            {
                List<Alading.Entity.Shop> listShop = ShopService.GetAllShop();
                foreach (Alading.Entity.Shop shop in listShop)
                {
                    this.checkedCmbNick.Properties.Items.Add(shop.nick);//加入搜索复选框

                    TreeListNode shopNode = treeListShop.AppendNode(new object[] { shop.nick }, null);

                    TreeListNode saleNode = treeListShop.AppendNode(new object[] { "仓库中的宝贝" }, shopNode);
                    saleNode.Tag = shop.nick;
                    AddNodes(shop.nick, saleNode, treeListShop);
                }
                barEditItemSelect.EditValue = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Alading.Entity.Shop shop in SystemHelper.ShopList)
                {
                    UIHelper.DownSellerCatsList(shop.nick);
                }
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void treeListShop_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                this.barEditItemSelect.EditValue = false;
                TreeListNode focusedNode = treeListShop.FocusedNode;
                if (focusedNode == null)
                {
                    waitFrm.Close();
                    return;
                }
                if (focusedNode.Tag == null)
                {
                    waitFrm.Close();
                    return;
                }
                if (treeListShop.FocusedNode.HasChildren)
                {
                    if (treeListShop.FocusedNode.GetDisplayText(0) == "仓库中的宝贝")
                    {
                        nick = focusedNode.Tag.ToString();
                        sellercid = string.Empty;
                    }
                    else
                    {
                        waitFrm.Close();
                        this.gridControlItem.DataSource = null;
                        return;
                    }
                }
                else
                {
                    string[] arr = focusedNode.Tag.ToString().Split(',');
                    if (arr.Length == 2)
                    {
                        sellercid = arr[0];
                        nick = arr[1];
                    }
                }
                //加载第一页数据，并返回查询商品总量
                ShowItemInGridView(sellercid, nick, "instock",out itemNum);
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void repositoryItemCheckSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (((DevExpress.XtraEditors.CheckEdit)(sender)).Checked == true)
            {
                for (int i = 0; i < gridViewItem.RowCount; i++)
                {
                    gridViewItem.SetRowCellValue(i, "IsSelected", true);
                }
            }
            else
            {
                for (int i = 0; i < gridViewItem.RowCount; i++)
                {
                    gridViewItem.SetRowCellValue(i, "IsSelected", false);
                }
            }
        }

        #region 添加节点方法
        /// <summary>
        /// 向treeList中加载出售中的商品的类目
        /// </summary>
        /// <param name="shopNick">店铺昵称</param>
        /// <param name="rootNode">父节点</param>
        /// <param name="treeListShop">被加载的控件treeListShop</param>
        private void AddNodes(string shopNick, TreeListNode rootNode, TreeList treeListShop)
        {
            List<Alading.Entity.SellerCat> listSellerCat = SellerCatService.GetSellerCatOrdered(shopNick);
            if (listSellerCat != null)
            {
                foreach (Alading.Entity.SellerCat sellerCat in listSellerCat)
                {
                    if (sellerCat.parent_cid == "0")
                    {
                        TreeListNode childNode = treeListShop.AppendNode(new object[] { sellerCat.name }, rootNode);
                        childNode.Tag = sellerCat.cid + "," + shopNick;
                        AppendNodes(shopNick, sellerCat.cid, childNode, treeListShop, listSellerCat);
                    }
                }
            }
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="sellerCatCid">父节点Cid</param>
        /// <param name="node1">父节点</param>
        /// <param name="treeListShop">被加载的控件treeListShop</param>
        /// <param name="listSellerCat">所有类目</param>
        private void AppendNodes(string shopNick, string sellerCatCid, TreeListNode rootNode, TreeList treeListShop, List<Alading.Entity.SellerCat> listSellerCat)
        {
            foreach (Alading.Entity.SellerCat sellerCat in listSellerCat)
            {
                if (sellerCat.parent_cid == sellerCatCid)
                {
                    TreeListNode childNode = treeListShop.AppendNode(new object[] { sellerCat.name }, rootNode);
                    childNode.Tag = sellerCat.cid + "," + shopNick;
                    AppendNodes(shopNick, sellerCat.cid, childNode, treeListShop, listSellerCat);
                }
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        ///  展示仓库中的item
        /// </summary>
        private void ShowItemInGridView(string sellercid, string nick, string approvestatus, out int itemNum)
        {
            List<Alading.Entity.Item> itemlist = ItemService.GetItem(sellercid, nick, approvestatus, out itemNum);
            this.gridControlItem.DataSource = itemlist;
            this.barStaticItemNumValue.Caption = itemNum.ToString();
            PageChange();
            gridViewItem.BestFitColumns();
        }

        /// <summary>
        /// 焦点行改变触发 展示已经上架了的宝贝,打钩
        /// </summary>
        private void PageChange()
        {
            Dictionary<string, string> autoListDic = g_AutoListDic.FirstOrDefault(i=>i.Key==nick).Value;
            if (autoListDic == null)
                return;
              
            for (int i = 0; i < gridViewItem.RowCount; i++)
            {
                Alading.Entity.Item item = gridViewItem.GetRow(i) as Alading.Entity.Item;
                if (item != null && autoListDic.ContainsKey(item.iid))
                {
                    //加入上架
                    gridViewItem.SetRowCellValue(i, "IsInAutoPlan", true);
                    gridViewItem.SetRowCellValue(i, "list_time", autoListDic[item.iid]);                   
                }
                else
                {
                    //移出上架
                    gridViewItem.SetRowCellValue(i, "IsInAutoPlan", false);
                    gridViewItem.SetRowCellValue(i, "list_time", string.Empty);
                }
            }
        }

        /// <summary>
        /// 获取选中行
        /// </summary>
        /// <returns></returns>
        private List<int> GetCheckedRows()
        {
            List<int> checkedRowsList = new List<int>();
            for (int i = 0; i < gridViewItem.RowCount; i++)
            {
                Alading.Entity.Item item = gridViewItem.GetRow(i) as Alading.Entity.Item;
                if (item != null)
                {
                    if (item.IsSelected == true)
                    {
                        checkedRowsList.Add(i);
                    }
                }
            }
            return checkedRowsList;
        }

        /// <summary>
        /// 获取选中的时间间隔
        /// </summary>
        /// <returns></returns>
        private List<int> GetCheckedHours()
        {
            List<int> checkedHours = new List<int>();
            for (int i = 0; i < checkedListBoxControlTime.Items.Count; i++)
            {
                if (checkedListBoxControlTime.Items[i].CheckState == CheckState.Checked)
                {
                    checkedHours.Add(i);
                }
            }
            return checkedHours;
        }

        /// <summary>
        /// 设置均匀上架时间
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <param name="checkedHours"></param>
        /// <returns></returns>
        private List<string> TimeSet(int dayOfWeek,List<int> checkedHours,Dictionary<string, string> autoListDic)
        {
            DevExpress.XtraScheduler.WeekDays weekDays = weekDaysCheckEditPlan.WeekDays;
            List<int> weekdayToNowList = new List<int>();
            if (weekDays == 0)
            {
                XtraMessageBox.Show("请选择日期");
                return new List<string>();
            }
            else
            {
                #region 日期
                if (weekDays.ToString().Contains("EveryDay"))
                {
                    weekdayToNowList.Add(1);
                    weekdayToNowList.Add(2);
                    weekdayToNowList.Add(3);
                    weekdayToNowList.Add(4);
                    weekdayToNowList.Add(5);
                    weekdayToNowList.Add(6);
                    weekdayToNowList.Add(7);
                }
                else
                {

                    if (weekDays.ToString().Contains("WorkDays"))
                    {

                        if (dayOfWeek > 1)
                            weekdayToNowList.Add((8 - dayOfWeek) % 7);
                        else
                            weekdayToNowList.Add(0);


                        if (dayOfWeek > 2)
                            weekdayToNowList.Add((9 - dayOfWeek) % 7);
                        else
                            weekdayToNowList.Add(2 - dayOfWeek);


                        if (dayOfWeek > 3)
                            weekdayToNowList.Add((10 - dayOfWeek) % 7);
                        else
                            weekdayToNowList.Add(3 - dayOfWeek);

                        if (dayOfWeek > 4)
                            weekdayToNowList.Add((11 - dayOfWeek) % 7);
                        else
                            weekdayToNowList.Add(4 - dayOfWeek);

                        if (dayOfWeek > 5)
                            weekdayToNowList.Add((12 - dayOfWeek) % 7);
                        else
                            weekdayToNowList.Add(5 - dayOfWeek);

                    }
                    else
                    {
                        if (weekDays.ToString().Contains("Monday"))
                        {
                            if (dayOfWeek > 1)
                                weekdayToNowList.Add((8 - dayOfWeek) % 7);
                            else
                                weekdayToNowList.Add(0);
                        }
                        if (weekDays.ToString().Contains("Tuesday"))
                        {
                            if (dayOfWeek > 2)
                                weekdayToNowList.Add((9 - dayOfWeek) % 7);
                            else
                                weekdayToNowList.Add(2 - dayOfWeek);
                        }
                        if (weekDays.ToString().Contains("Wednesday"))
                        {
                            if (dayOfWeek > 3)
                                weekdayToNowList.Add((10 - dayOfWeek) % 7);
                            else
                                weekdayToNowList.Add(3 - dayOfWeek);
                        }
                        if (weekDays.ToString().Contains("Thursday"))
                        {
                            if (dayOfWeek > 4)
                                weekdayToNowList.Add((11 - dayOfWeek) % 7);
                            else
                                weekdayToNowList.Add(4 - dayOfWeek);
                        }
                        if (weekDays.ToString().Contains("Friday"))
                        {
                            if (dayOfWeek > 5)
                                weekdayToNowList.Add((12 - dayOfWeek) % 7);
                            else
                                weekdayToNowList.Add(5 - dayOfWeek);
                        }
                    }

                    if (weekDays.ToString().Contains("WeekendDays"))
                    {
                        if (dayOfWeek > 6)
                            weekdayToNowList.Add((13 - dayOfWeek) % 7);
                        else
                            weekdayToNowList.Add(6 - dayOfWeek);
                        if (dayOfWeek > 7)
                            weekdayToNowList.Add((14 - dayOfWeek) % 7);
                        else
                            weekdayToNowList.Add(7 - dayOfWeek);
                    }
                    else
                    {
                        if (weekDays.ToString().Contains("Saturday"))
                        {
                            if (dayOfWeek > 6)
                                weekdayToNowList.Add((13 - dayOfWeek) % 7);
                            else
                                weekdayToNowList.Add(6 - dayOfWeek);
                        }
                        if (weekDays.ToString().Contains("Sunday"))
                        {
                            if (dayOfWeek > 7)
                                weekdayToNowList.Add((14 - dayOfWeek) % 7);
                            else
                                weekdayToNowList.Add(7 - dayOfWeek);
                        }
                    }

                }
                #endregion
            }
            
            int numPerMinute = 0;//每分钟上架几个  
            int MinutePerOnce = 0;//几分钟上架一个
            int totalMinute = 0;//总分钟数
            //去除今天
            if (weekdayToNowList.Contains(0))
            {
                weekdayToNowList.Remove(0);
                weekdayToNowList.Add(7);
            }      

            totalMinute = weekdayToNowList.Count * (checkedHours.Count) * 60;
            if (totalMinute >= autoListDic.Count)
            {
                if (autoListDic.Count > 0)
                {
                    MinutePerOnce = totalMinute / autoListDic.Count;//几分钟上架一个
                }
                
            }
            else
            {
                if (totalMinute > 0)
                {
                    if (autoListDic.Count % totalMinute == 0)
                    {
                        numPerMinute = autoListDic.Count / totalMinute;//每分钟上架几个 
                    }
                    else
                    {
                        numPerMinute = autoListDic.Count / totalMinute + 1;
                    }
                }

            }
            List<string> dateTimeStrList = new List<string>();//安排时间
            weekdayToNowList.Sort();
            //几分钟上架一个
            if (MinutePerOnce > 0)
            {
                #region 几分钟上架一个
                foreach (int weekday in weekdayToNowList)
                {
                    int hour = 1;
                    if (MinutePerOnce >= 60)
                    {
                        hour = MinutePerOnce / 60;
                    }
                    //第一个从整点开始
                    string dateFirst = string.Format("{0} {1}:0:0", DateTime.Now.AddDays(weekday).ToString("yyyy-MM-dd"), checkedHours[0]);
                    dateTimeStrList.Add(dateFirst);
                    int _minute = MinutePerOnce % 60;
                    //int count = 0;
                    int preMin = MinutePerOnce;//前一个小时剩下的分钟数
                    int minuteToHour = 0;//由分钟数加到了小时
                    for (int i = 0; i < checkedHours.Count; i += hour+minuteToHour)
                    {
                        _minute = preMin % 60;
                       // _minute += (MinutePerOnce % 60) * count;
                        if (_minute > 60)
                        {
                            minuteToHour = _minute / 60;
                            _minute %= 60;
                        }
                        int _second = 0;
                        while (_minute < 60)
                        {
                            if (i + minuteToHour < checkedHours.Count)
                            {
                                string date = string.Empty;
                                if (i + minuteToHour < checkedHours.Count)
                                {
                                    date = string.Format("{0} {1}:{2}:{3}", DateTime.Now.AddDays(weekday).ToString("yyyy-MM-dd"), checkedHours[i + minuteToHour], _minute, _second);

                                    dateTimeStrList.Add(date);
                                }
                                _minute += MinutePerOnce;
                            }
                            else
                                break;
                        }
                        preMin = _minute % 60;
                        //count++;
                    }
                }
                #endregion
            }
            else
            {
                //每分钟上架几个
                foreach (int weekday in weekdayToNowList)
                {
                    foreach (int hour in checkedHours)
                    {
                        int _minute = 0;
                        int _second = 0;
                        while (_minute < 60)
                        {
                            string date = string.Format("{0} {1}:{2}:{3}", DateTime.Now.AddDays(weekday).ToString("yyyy-MM-dd"), hour, _minute, _second);
                            //每分钟上架几个
                            for (int i = 0; i < numPerMinute; i++)
                            {
                                dateTimeStrList.Add(date);
                            }
                            _minute += 1;
                        }

                    }
                }

            }
            return dateTimeStrList;
        }

        /// <summary>
        /// 加入上架计划
        /// </summary>
        private void AddToPlan()
        {
            if (nick == string.Empty)
                return;
            Dictionary<string, string> autoListDic = g_AutoListDic[nick];
            List<int> checkedRowsList = GetCheckedRows();           
            List<int> checkedHours = GetCheckedHours();
            if (checkedHours == null || checkedHours.Count == 0)
            {
                XtraMessageBox.Show("请设置上架时间段");
                return;
            }           
            int dayOfWeek = 1;//表示星期一
            switch (DateTime.Now.DayOfWeek.ToString())
            {
                case "Monday": dayOfWeek = 1; break;
                case "Tuesday": dayOfWeek = 2; break;
                case "Wednesday": dayOfWeek = 3; break;
                case "Thursday": dayOfWeek = 4; break;
                case "Friday": dayOfWeek = 5; break;
                case "Saturday": dayOfWeek = 6; break;
                case "Sunday": dayOfWeek = 7; break;
            }
            /*在这里设定上架时间*/
            //选择日期均匀上架
            List<string> dateTimeStrList = new List<string>();
            if (radioGroupSet.SelectedIndex == 1)
            {
                dateTimeStrList = TimeSet(dayOfWeek, checkedHours, autoListDic);

            }// if (radioGroupSet.SelectedIndex == 1)
            else
            {
                #region//系统默认设置上架
                int num = int.Parse(spinEditNum.Text);//每次几个
                int MinutePerOnce = int.Parse(spinEditTime.Text);//每几分钟一次   
                int minuteToHour = 0;//由分钟数加到了小时
                bool alreadyFull = false;
                int addDay = 0;
                while (alreadyFull == false)
                {
                    addDay++;
                    for (int i = 0; i < checkedHours.Count; i++)
                    {
                        int minute = 0;
                        int count = 0;
                        minute = MinutePerOnce;                        
                        minute *= count;
                        if (MinutePerOnce > 60)
                        {
                            minuteToHour = minuteToHour / 60;
                        }
                        while (minute < 60)
                        {
                            string date = string.Empty;
                            if (i + minuteToHour < checkedHours.Count)
                            {
                                //从第二天开始
                                date = string.Format("{0} {1}:{2}:0", DateTime.Now.AddDays(addDay).ToString("yyyy-MM-dd"), checkedHours[i + minuteToHour], minute);
                                //每次上架几个
                                for (int j = 0; j < num; j++)
                                {
                                    dateTimeStrList.Add(date);
                                }
                            }
                            minute += MinutePerOnce;
                        }
                        if (dateTimeStrList.Count >= autoListDic.Count)
                        {
                            alreadyFull = true;
                            break;
                        }
                        count++;
                    }
                }               
                #endregion
            }

            //int dCount = 0;//dateTimeStrList的下标
            List<string> keyStr = autoListDic.Keys.ToList();
            if (keyStr.Count <= dateTimeStrList.Count)
            {
                for (int i=0;i<keyStr.Count;i++)
                {
                    autoListDic[keyStr[i]] = dateTimeStrList[i];
                    //dCount++;
                }
            }
            else
            {
                int times = 1;//当安排的时间不够时重复设置一些              
                if (keyStr.Count % dateTimeStrList.Count == 0)
                    times = keyStr.Count / dateTimeStrList.Count;
                else
                    times = keyStr.Count / dateTimeStrList.Count + 1;
                for (int key = 0; key < keyStr.Count; key++)
                {
                    for (int i = 0; i < times && key <dateTimeStrList.Count; i++)
                    {
                        autoListDic[keyStr[key]] = dateTimeStrList[key];
                    }
                }
            }
                  
            if (autoListDic.Count <= dateTimeStrList.Count)
            {       
                for (int i = 0; i < gridViewItem.RowCount; i++)
                {
                    Alading.Entity.Item item = gridViewItem.GetRow(i) as Alading.Entity.Item;
                    if (item != null && autoListDic.ContainsKey(item.iid))
                    {
                        //加入上架
                        gridViewItem.SetRowCellValue(i, "IsInAutoPlan", true);
                        gridViewItem.SetRowCellValue(i, "list_time", autoListDic[item.iid]);                    
                    }
                    else
                    {
                        //移出上架
                        gridViewItem.SetRowCellValue(i, "IsInAutoPlan", false);
                        gridViewItem.SetRowCellValue(i, "list_time", string.Empty);
                    }
                }
            }         
        }

        #endregion

        #region 宝贝计划操作
        /// <summary>
        /// 加入上架计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInPlan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (nick == string.Empty)
                return;
            List<int> checkedRowsList = GetCheckedRows();
            if (!g_AutoListDic.ContainsKey(nick))
            {
                g_AutoListDic.Add(nick, new Dictionary<string, string>());
            }
            foreach (int row in checkedRowsList)
            {                
                Alading.Entity.Item item = gridViewItem.GetRow(row) as Alading.Entity.Item;
                if (item != null && !g_AutoListDic[nick].ContainsKey(item.iid))
                {
                    g_AutoListDic[nick].Add(item.iid, string.Empty);//添加到上架计划列表
                }
            }
            if (g_AutoListDic[nick] ==null || g_AutoListDic[nick].Count == 0)
            {
                XtraMessageBox.Show("请选择需要加入上架计划的宝贝");
                return;
            }
            AddToPlan();          
        }
        /// <summary>
        /// 移出上架计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutPlan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (nick == string.Empty)
            {
                return;
            }
            else if (!g_AutoListDic.ContainsKey(nick))
            {
                return;
            }
            List<int> checkedRowsList = GetCheckedRows();
            if (checkedRowsList == null || checkedRowsList.Count == 0)
            {
                XtraMessageBox.Show("请选择需要移出上架计划的宝贝");
                return;
            }

            foreach (int row in checkedRowsList)
            {
                Alading.Entity.Item item = gridViewItem.GetRow(row) as Alading.Entity.Item;
                if (item != null && g_AutoListDic[nick].ContainsKey(item.iid))
                {
                    g_AutoListDic[nick].Remove(item.iid);//从上架计划列表中删除
                }
            }
            /*重新安排时间*/
            AddToPlan();
        }
        /// <summary>
        /// 执行上架计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdatePlan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count=0;
            foreach (string nick in g_AutoListDic.Keys)
            {
                count += g_AutoListDic[nick].Count;
            }
            if (count==0)
            {
                XtraMessageBox.Show("当前上架计划列表中没有宝贝！");
                return;
            }
            else
            {
                UpdateItem frm = new UpdateItem(this.g_AutoListDic);
                frm.ShowDialog();
            }      
        }

        private void radioGroupSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroupSet.SelectedIndex == 0)
            {
                weekDaysCheckEditPlan.Enabled = false;
                spinEditNum.Enabled = true;
                spinEditTime.Enabled = true;
            }
            else
            {
                weekDaysCheckEditPlan.Enabled = true;
                spinEditNum.Enabled = false;
                spinEditTime.Enabled = false;
            }
        }

        private void gridViewItem_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsSelected")
            {                
                gridViewItem.SetFocusedRowCellValue("IsSelected", e.Value);
            }           
        }


        private void btnImmedUpList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<Alading.Entity.Item> itemlist = gridViewItem.DataSource as List<Alading.Entity.Item>;
            if (itemlist!=null)
            {
                List<Alading.Entity.Item> selectedItemList = itemlist.Where(i => i.IsSelected == true).ToList();
                List<Alading.Entity.Item> notSelectedItemList = itemlist.Where(i => i.IsSelected == false).ToList();
                if (selectedItemList.Count==0)
                {
                    XtraMessageBox.Show(Constants.NOT_SELECT_ITEM);
                    return;
                }
                SortedList<string, List<string>> sortItemList = new SortedList<string, List<string>>();
                selectedItemList.ToLookup<Alading.Entity.Item, string>(c => c.nick).ToList().ForEach(a => sortItemList.Add(a.Key,a.Select(i=>i.iid).ToList()));
                if (sortItemList.Count == 0 || sortItemList.First().Value == null || sortItemList.First().Value.Count == 0)
                {
                    XtraMessageBox.Show(Constants.NOT_SELECT_ITEM);
                }
                else
                {
                    ItemBatchUpdate batchUpdate = new ItemBatchUpdate(sortItemList);
                    batchUpdate.ShowDialog(this);
                    if(batchUpdate.iidlist.Count>0)
                    {
                        ItemService.UpdateItemsStatus(batchUpdate.iidlist, "onsale");
                        notSelectedItemList.AddRange(itemlist.Where(a => !batchUpdate.iidlist.Contains(a.iid)).ToList());
                        this.gridControlItem.DataSource = notSelectedItemList;
                    }
                    
                }
            }
        }

        #endregion

    }
    
}