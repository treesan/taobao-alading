using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Alading.Forms.Consumer
{
    public partial class ConsumerManage : DevExpress.XtraEditors.XtraForm
    {
        private int pageSize = 20;

        private List<Alading.Entity.Area> provinceList = new List<Alading.Entity.Area>();
        private List<Alading.Entity.Area> cityList = new List<Alading.Entity.Area>();
        private List<Alading.Entity.Area> countyList = new List<Alading.Entity.Area>();

        private int selectedProvinceIndex = 0;
        private int selectedCityIndex = 0;
        private int selectedCountyIndex = 0;
        private int selectedSource = 0;
        private int selectedGrade = 0;

        private Alading.Entity.Consumer selectedConsumer = null;
        private List<Alading.Entity.Consumer> consumerList = new List<Alading.Entity.Consumer>();
        private LoadConsumerStrategy loadStrategy = null;

        private Alading.Entity.ConsumerAddress selectedConsumerAddress = null;
        private List<Alading.Entity.ConsumerAddress> consumerAddressList = new List<Alading.Entity.ConsumerAddress>();
        private List<Alading.Entity.Trade> consumerTradeList = new List<Alading.Entity.Trade>();
        private List<Alading.Entity.ConsumerVisit> consumerVisitList = new List<Alading.Entity.ConsumerVisit>();
        private string sortType = "nick";

        private SelectedConsumerList selectedList = new SelectedConsumerList();

        public ConsumerManage()
        {
            InitializeComponent();
        }

        private LoadConsumerStrategy LoadStrategy
        {
            get { return loadStrategy; }
            set
            {
                if (value != null)
                {
                    loadStrategy = value;
                    loadStrategy.PageIndex = 1;
                    loadStrategy.PageSize = pageSize;
                    LoadConsumer();
                }
            }
        }

        private void LoadAllConsumer()
        {
            LoadStrategy = new LoadAllConsumer();
        }

        private void LoadConsumer()
        {
            if (loadStrategy != null)
            {
                loadStrategy.PageSize = pageSize;
                gcConsumerGrid.DataSource = null;
                consumerList = loadStrategy.LoadConsumers();
                gcConsumerGrid.DataSource = consumerList;

                btPrevPage.Enabled = loadStrategy.PageIndex > 1;
                btNextPage.Enabled = loadStrategy.PageIndex < loadStrategy.PageCount;
            }
        }

        private void ConsumerManage_Load(object sender, EventArgs e)
        {
            LoadAllConsumer();
            provinceList = Alading.Business.AreaService.GetAreas("1");
            SetControlItems(cbProvince, provinceList);
        }

        private void btNewConsumer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //TODO: (WXC) 添加客户
            ConsumerAdd consumerAdd = new ConsumerAdd();
            if (DialogResult.OK == consumerAdd.ShowDialog())
            {
                Alading.Entity.Consumer consumer = consumerAdd.GetNewConsumer();
                Alading.Core.Enum.ReturnType result = Alading.Business.ConsumerService.AddConsumer(consumer);
                if (result == Alading.Core.Enum.ReturnType.Success)
                {
                    LoadStrategy = new LoadAllConsumer();
                    LoadAllConsumer();
                }
                //do when insert data is failed
                else
                {
                    XtraMessageBox.Show("保存数据失败！");
                }
            }
        }

        private void btImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //TODO: (WXC) 导入Excel文件
        }

        private void btRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //刷新数据
            LoadConsumer();
        }

        private void btSendText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //TODO: (WXC) 给选定的用户发送短信
            if (selectedConsumer != null)
            {
                TxtMsgToConsumer txtMsgToConsumer = new TxtMsgToConsumer();
                if (!string.IsNullOrEmpty(selectedConsumer.mobilephone))
                {
                    txtMsgToConsumer.Receiver = selectedConsumer.nick;
                    txtMsgToConsumer.ReceiverPhone = selectedConsumer.mobilephone;
                    txtMsgToConsumer.ShowDialog();
                }
                else
                {
                    XtraMessageBox.Show("未指定所选客户的手机号码");
                }
            }
        }        

        private void btSave_Click(object sender, EventArgs e)
        {
            //TODO: (WXC) 保存对客户的更改
            if (selectedConsumer != null)
            {
                selectedConsumer.nick = txConsumerName.Text;
                selectedConsumer.buyer_name = txBuyer.Text;
                selectedConsumer.location_address = txAddr.Text;
                selectedConsumer.buyer_zip = txZipcode.Text;
                selectedConsumer.alipay = txZhifubao.Text;
                selectedConsumer.phone = txTel.Text;
                selectedConsumer.mobilephone = txMobile.Text;
                selectedConsumer.email = txEmail.Text;
                selectedConsumer.score = Convert.ToInt32(txCredit.Text);

                selectedConsumer.location_state = selectedProvinceIndex >= 0 ? provinceList[selectedProvinceIndex].name : string.Empty;
                selectedConsumer.location_city = selectedCityIndex >= 0 ? cityList[selectedCityIndex].name : string.Empty;
                selectedConsumer.location_district = selectedCountyIndex >= 0 ? countyList[selectedCountyIndex].name : string.Empty;

                selectedConsumer.source = cbSource.SelectedIndex;

                Alading.Core.Enum.ReturnType result = Alading.Business.ConsumerService.UpdateConsumer(selectedConsumer);
                if (result == Alading.Core.Enum.ReturnType.Success)
                {
                    //刷新表格中的数据
                    gcConsumerGrid.DataSource = null;
                    gcConsumerGrid.DataSource = consumerList;
                }
                else
                {
                    XtraMessageBox.Show("更新数据失败！");
                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            //取消对客户的更改
            FillConsumerInfo();
        }

        private void FillConsumerInfo()
        {
            if (selectedConsumer != null)
            {
                txConsumerName.Text = selectedConsumer.nick;
                txBuyer.Text = selectedConsumer.buyer_name;
                txAddr.Text = selectedConsumer.location_address;
                txZipcode.Text = selectedConsumer.buyer_zip;
                txZhifubao.Text = selectedConsumer.alipay;
                txTel.Text = selectedConsumer.phone;
                txMobile.Text = selectedConsumer.mobilephone;
                txEmail.Text = selectedConsumer.email;
                txCredit.Text = selectedConsumer.score.ToString();

                cbProvince.SelectedIndex = provinceList.FindIndex(c => c.name == selectedConsumer.location_state);
                cbCity.SelectedIndex = cityList.FindIndex(c => c.name == selectedConsumer.location_city);
                cbZone.SelectedIndex = countyList.FindIndex(c => c.name == selectedConsumer.location_district);
                cbSource.SelectedIndex = selectedConsumer.source ?? 0;
            }
        }

        private void SetControlItems(ComboBoxEdit control, List<Alading.Entity.Area> data)
        {
            control.Properties.Items.Clear();

            foreach (var i in data)
            {
                control.Properties.Items.Add(i.name);
            }

            control.SelectedIndex = 0;
        }

        #region 加载客户事件

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //根据关键字搜索客户
            LoadStrategy = new SearchConsumer()
            {
                Keyword = txSearchKey.Text,
                SortType = sortType,
            };
        }

        private void btAllConsumer_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看所有用户
            LoadAllConsumer();
        }

        private void btFqToday_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看今天内进行了交易的客户
            LoadStrategy = new LoadTradedConsumer()
            {
                After = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0),
                Before = DateTime.Now
            };
        }

        private void btFqWeek_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看一周内进行了交易的客户
            LoadStrategy = new LoadTradedConsumer()
            {
                After = DateTime.Now.AddDays(-7),
                Before = DateTime.Now,
            };
        }

        private void btFqMonth_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看一个月内进行了交易的客户
            LoadStrategy = new LoadTradedConsumer()
            {
                After = DateTime.Now.AddMonths(-1),
                Before = DateTime.Now,
            };
        }

        private void btFqSeason_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看三个月内进行了交易的客户
            LoadStrategy = new LoadTradedConsumer()
            {
                After = DateTime.Now.AddMonths(-3),
                Before = DateTime.Now,
            };
        }

        private void btClNormal_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看普通客户
            LoadStrategy = new LoadTypedConsumer() { Type = 0 };
        }

        private void btClImportant_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看重要客户
            LoadStrategy = new LoadTypedConsumer() { Type = 1 };
        }

        private void btClVIP_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看VIP客户
            LoadStrategy = new LoadTypedConsumer() { Type = 2 };
        }

        private void btClDealer_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看经销商
            LoadStrategy = new LoadTypedConsumer() { Type = 3 };
        }

        private void btSrTaobaoB_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看淘宝商城店的客户
            LoadStrategy = new LoadSourcedConsumer() { Source = 0 };
        }

        private void btSrTaobaoC_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看淘宝普通店的客户
            LoadStrategy = new LoadSourcedConsumer() { Source = 1 };
        }

        private void btSrShopEx_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看ShopEx的客户
            LoadStrategy = new LoadSourcedConsumer() { Source = 2 };
        }

        private void btSrEcShop_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看EcShop的客户
            LoadStrategy = new LoadSourcedConsumer() { Source = 3 };
        }

        private void btSrPaipai_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看拍拍的客户
            LoadStrategy = new LoadSourcedConsumer() { Source = 4 };
        }

        private void btSrYiqu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看易趣第客户
            LoadStrategy = new LoadSourcedConsumer() { Source = 5 };
        }

        private void btSrYoua_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看有啊的客户
            LoadStrategy = new LoadSourcedConsumer() { Source = 6 };
        }

        private void btSrReal_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看实体店的客户
            LoadStrategy = new LoadSourcedConsumer() { Source = 7 };
        }

        private void btFbToday_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看今天内回访过的客户
            LoadStrategy = new LoadVisitedConsumer()
            {
                Aftre = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0),
                Before = DateTime.Now,
            };
        }

        private void btFbWeek_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看一周内回访过的客户
            LoadStrategy = new LoadVisitedConsumer()
            {
                Aftre = DateTime.Now.AddDays(-7),
                Before = DateTime.Now,
            };
        }

        private void btFbMonth_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看一个月内回访过的客户
            LoadStrategy = new LoadVisitedConsumer()
            {
                Aftre = DateTime.Now.AddMonths(-1),
                Before = DateTime.Now,
            };
        }

        private void btFbSeason_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看三个月内回访过的客户
            LoadStrategy = new LoadVisitedConsumer()
            {
                Aftre = DateTime.Now.AddMonths(-3),
                Before = DateTime.Now,
            };
        }

        #endregion

        private void btFirstPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //首页客户列表
            if (loadStrategy != null)
            {
                loadStrategy.PageIndex = 1;
                loadStrategy.PageSize = pageSize;
                LoadConsumer();
            }
        }

        private void btPrevPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //上一页客户
            if (loadStrategy != null && loadStrategy.PageIndex > 1)
            {
                loadStrategy.PageIndex--;
                LoadConsumer();
            }
        }

        private void btNextPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //下一页客户列表
            if (loadStrategy != null && loadStrategy.PageIndex < loadStrategy.PageCount)
            {
                loadStrategy.PageIndex++;
                LoadConsumer();
            }
        }

        private void btLastPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //末页客户列表
            if (loadStrategy != null && loadStrategy.PageCount > 1)
            {
                loadStrategy.PageIndex = loadStrategy.PageCount;
                loadStrategy.PageSize = pageSize;
                LoadConsumer();
            }
        }

        private void gvConsumerView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //选中客户
            int[] rows = gvConsumerView.GetSelectedRows();

            if (rows.Length > 0)
            {
                selectedConsumer = consumerList[rows[0]];

                btSave.Enabled = true;
                btSendText.Enabled = true;
                btSendMail.Enabled = true;

                //加载用户基本信息
                FillConsumerInfo();

                //导入客户地址信息
                consumerAddressList = Alading.Business.ConsumerAddressService.GetConsumerAddress(c => c.buyer_nick == selectedConsumer.nick);
                gcAddrGrid.DataSource = null;
                gcAddrGrid.DataSource = consumerAddressList;

                //导入客户交易记录
                consumerTradeList = Alading.Business.TradeService.GetTrade(c => c.buyer_nick == selectedConsumer.nick);
                gcTradGrid.DataSource = null;
                gcTradGrid.DataSource = consumerTradeList;

                //导入客户回访记录
                consumerVisitList = Alading.Business.ConsumerVisitService.GetConsumerVisit(c => c.ConsumerNick == selectedConsumer.nick);
                gcVisitGrid.DataSource = null;
                gcVisitGrid.DataSource = consumerVisitList;
            }
            else
            {
                btSave.Enabled = false;
                btSendText.Enabled = false;
                btSendMail.Enabled = false;
            }
        }

        private void cbOrderType_SelectedValueChanged(object sender, EventArgs e)
        {
            string selected = cbOrderType.SelectedText;
            sortType = "nick";
            switch (selected)
            {
                case "收货人姓名": sortType = "buyer_name"; break;
            }
        }

        private void txSearchKey_EditValueChanged(object sender, EventArgs e)
        {
            btnSearch.Enabled = (!string.IsNullOrEmpty(txSearchKey.Text));
        }

        private void txRequired_EditValueChanged(object sender, EventArgs e)
        {
            btSave.Enabled = (selectedConsumer != null) && (!string.IsNullOrEmpty(txConsumerName.Text)) && (!string.IsNullOrEmpty(txBuyer.Text));
        }

        private void cbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProvince.SelectedIndex >= 0)
            {
                selectedProvinceIndex = cbProvince.SelectedIndex;
                Alading.Entity.Area selectedArea = provinceList[selectedProvinceIndex];
                cityList = Alading.Business.AreaService.GetAreas(selectedArea.id);
                SetControlItems(cbCity, cityList);
            }
        }

        private void cbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCity.SelectedIndex >= 0)
            {
                selectedCityIndex = cbCity.SelectedIndex;
                Alading.Entity.Area selectedArea = cityList[selectedCityIndex];
                countyList = Alading.Business.AreaService.GetAreas(selectedArea.id);
                SetControlItems(cbZone, countyList);
            }
        }

        private void btExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //TODO: (WXC) 导出Excel文件
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = saveFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                gcConsumerGrid.ExportToExcelOld(saveFileDialog.FileName);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ceAllConsumer_CheckedChanged(object sender, EventArgs e)
        {
            //点击全选
            bool value = ((DevExpress.XtraEditors.CheckEdit)(sender)).Checked;
            for (int i = 0; i < gvConsumerView.RowCount; i++)
            {
                gvConsumerView.SetRowCellValue(i, "checked", value);
                if (value)
                {
                    selectedList.AddItem(consumerList[i]);
                }
                else
                {
                    selectedList.RemoveItem(consumerList[i]);
                }
            }        
        }

        private void gvConsumerView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bool value = (bool)e.Value;
            if (value)
            {
                selectedList.AddItem(consumerList[e.RowHandle]);
            }
            else
            {
                selectedList.RemoveItem(consumerList[e.RowHandle]);
            }
        }

        private void gcConsumerGrid_DataSourceChanged(object sender, EventArgs e)
        {
            if (consumerList != null)
            {
                for (int i = 0; i < consumerList.Count; i++)
                {
                    if (selectedList.ContainItem(consumerList[i]))
                    {
                        gvConsumerView.SetRowCellValue(i, "checked", true);
                    }
                }
            }
        }

        /// <summary>
        /// 新建地址事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btNewAddr_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedConsumer != null)
            {
                ConsumerAddressAdd dialog = new ConsumerAddressAdd();
                dialog.Text = "新建地址";
                dialog.ConsumerNick = selectedConsumer.nick;
                if (DialogResult.OK == dialog.ShowDialog())
                {
                    Alading.Entity.ConsumerAddress addr = new Alading.Entity.ConsumerAddress();
                    addr.buyer_nick = selectedConsumer.nick;
                    addr.location_country = dialog.ConsumerCountry;
                    addr.location_state = dialog.ConsumerProvince;
                    addr.location_city = dialog.ConsumerCity;
                    addr.location_district = dialog.ConsumerCounty;
                    addr.location_address = dialog.ConsumerAddress;
                    addr.location_zip = dialog.ConsumerZip;

                    Alading.Core.Enum.ReturnType result 
                        = Alading.Business.ConsumerAddressService.AddConsumerAddress(addr);

                    if (result == Alading.Core.Enum.ReturnType.Success)
                    {
                        consumerAddressList.Add(addr);
                        gcAddrGrid.DataSource = null;
                        gcAddrGrid.DataSource = consumerAddressList;
                    }
                    else
                    {
                        XtraMessageBox.Show("保存数据失败！");
                    }
                }
            }
        }

        /// <summary>
        /// 编辑地址事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btEditAddr_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedConsumer != null && selectedConsumerAddress != null)
            {
                ConsumerAddressAdd dialog = new ConsumerAddressAdd();
                dialog.Text = "编辑地址";
                dialog.ConsumerNick = selectedConsumer.nick;
                dialog.ConsumerProvince = selectedConsumerAddress.location_state;
                dialog.ConsumerCity = selectedConsumerAddress.location_city;
                dialog.ConsumerCounty = selectedConsumerAddress.location_district;
                dialog.ConsumerAddress = selectedConsumerAddress.location_address;
                dialog.ConsumerZip = selectedConsumerAddress.location_zip;

                if (DialogResult.OK == dialog.ShowDialog())
                {
                    selectedConsumerAddress.location_country = dialog.ConsumerCountry;
                    selectedConsumerAddress.location_state = dialog.ConsumerProvince;
                    selectedConsumerAddress.location_city = dialog.ConsumerCity;
                    selectedConsumerAddress.location_district = dialog.ConsumerCounty;
                    selectedConsumerAddress.location_address = dialog.ConsumerAddress;
                    selectedConsumerAddress.location_zip = dialog.ConsumerZip;

                    Alading.Core.Enum.ReturnType result
                        = Alading.Business.ConsumerAddressService.UpdateConsumerAddress(selectedConsumerAddress);
                    if (result == Alading.Core.Enum.ReturnType.Success)
                    {
                        gcAddrGrid.DataSource = null;
                        gcAddrGrid.DataSource = consumerAddressList;
                    }
                    else
                    {
                        XtraMessageBox.Show("保存数据失败！");
                    }
                }
            }
        }

        /// <summary>
        /// 删除地址事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDelAddr_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedConsumer != null && selectedConsumerAddress != null)
            {
                Alading.Core.Enum.ReturnType result
                        = Alading.Business.ConsumerAddressService.RemoveConsumerAddress(selectedConsumerAddress.ConsumerAddressID);
                if (result == Alading.Core.Enum.ReturnType.Success)
                {
                    consumerAddressList.Remove(selectedConsumerAddress);
                    gcAddrGrid.DataSource = null;
                    gcAddrGrid.DataSource = consumerAddressList;
                }
                else
                {
                    XtraMessageBox.Show("保存数据失败！");
                }
            }
        }

        /// <summary>
        /// Selecte consumer address row event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvAddrView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int[] rows = gvAddrView.GetSelectedRows();

            if (rows.Length > 0)
            {
                selectedConsumerAddress = consumerAddressList[rows[0]];
            }
        }

        /// <summary>
        /// Send email to selected consumer event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedConsumer != null)
            {
                if (!string.IsNullOrEmpty(selectedConsumer.email))
                {
                    List<Alading.Entity.Consumer> list1 = new List<Alading.Entity.Consumer>();
                    list1.Add(selectedConsumer);
                    EmailToConsumer emailToConsumer = new EmailToConsumer();
                    emailToConsumer.Receivers = list1;
                    emailToConsumer.ShowDialog();
                }
                else
                {
                    XtraMessageBox.Show("未指定所选客户的邮箱地址");
                }
            }
        }

        /// <summary>
        /// Send email to selected consumers event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSendSelectedConsumers_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedList != null && selectedList.Items.Count > 0)
            {
                var query = (from s1 in selectedList.Items
                             where s1.email != null && s1.email != string.Empty
                             select s1).ToList();

                if (query.Count < selectedList.Items.Count)
                {
                    XtraMessageBox.Show("一个或多个用户未指定邮箱地址");
                }
                if (query.Count > 0)
                {
                    List<Alading.Entity.Consumer> list1 = new List<Alading.Entity.Consumer>();
                    list1.AddRange(query);
                    EmailToConsumer emailToConsumer = new EmailToConsumer();
                    emailToConsumer.Receivers = list1;
                    emailToConsumer.ShowDialog();
                }
            }
            else
            {
                XtraMessageBox.Show("未指定要发送邮件的客户");
            }
        }

        /// <summary>
        /// Send email to all consumers event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSendToAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var list1 = loadStrategy.LoadAllConsumers();

            if (list1 != null && list1.Count > 0)
            {
                var query = (from s1 in list1
                             where s1.email != null && s1.email != string.Empty
                             select s1).ToList();

                if (query.Count < list1.Count)
                {
                    XtraMessageBox.Show("一个或多个用户未指定邮箱地址");
                }
                if (query.Count > 0)
                {
                    List<Alading.Entity.Consumer> list2 = new List<Alading.Entity.Consumer>();
                    list2.AddRange(query);
                    try
                    {
                        EmailToConsumer emailToConsumer = new EmailToConsumer();
                        emailToConsumer.Receivers = list2;
                        emailToConsumer.ShowDialog();
                    }
                    catch (Exception)
                    {                        
                        //throw;
                    }                    
                }
            }
            else
            {
                XtraMessageBox.Show("未指定要发送邮件的客户");
            }
        }
    }
}