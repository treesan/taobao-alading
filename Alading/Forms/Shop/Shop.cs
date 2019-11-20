using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Core.Enum;
using Alading.Taobao;
using DevExpress.Utils;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API;
using Alading.Utils;
using Alading.Business;
using System.Web;
using DevExpress.XtraGauges.Win.Base;

namespace Alading.Forms.Shop
{
    public partial class Shop : DevExpress.XtraEditors.XtraForm
    {
        public Shop()
        {
            InitializeComponent();
        }

        #region 全局变量
        //用于判断点击的类型,默认为全部店铺
        ShopType shopShowType = ShopType.AllShop;

        bool isQuery = false;
        string keyWord = string.Empty;
        #endregion

        #region 点击控件触发事件
        /// <summary>
        /// 加载界面同时加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shop_Load(object sender, EventArgs e)
        {
            try
            {
                shopShowType = ShopType.AllShop;
                groupCtrlShop.Text = "所有店铺列表";

                LoadLocation(null, cBoxState);
                LoadShop(shopShowType);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region 点击NavBarItem
        /// <summary>
        /// 点击所有店铺
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarAllShop_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                /*用于判断点击的店铺类目*/
                shopShowType = ShopType.AllShop;
                groupCtrlShop.Text = "所有店铺列表";

                LoadShop(shopShowType);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击淘宝商城店（B店）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarTBBShop_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                /*用于判断点击的店铺类目*/
                shopShowType = ShopType.TaobaoBShop;
                groupCtrlShop.Text = "淘宝商城店（B店）店铺列表";

                LoadShop(shopShowType);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击淘宝商城店（C店）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarTBCShop_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                /*用于判断点击的店铺类目*/
                shopShowType = ShopType.TaobaoCShop;
                groupCtrlShop.Text = "淘宝商城店（C店）店铺列表";

                LoadShop(shopShowType);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击ShopEx店铺
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarShopEx_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                /*用于判断点击的店铺类目*/
                shopShowType = ShopType.ShopEx;
                groupCtrlShop.Text = "ShopEx 店铺列表";

                LoadShop(shopShowType);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击EcShop店铺
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarEcShop_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                /*用于判断点击的店铺类目*/
                shopShowType = ShopType.EcShop;
                groupCtrlShop.Text = "EcShop 店铺列表";

                LoadShop(shopShowType);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击拍拍店铺
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarPaipai_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                /*用于判断点击的店铺类目*/
                shopShowType = ShopType.Paipai;
                groupCtrlShop.Text = "拍拍 店铺列表";

                LoadShop(shopShowType);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击易趣店铺
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarEbay_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                /*用于判断点击的店铺类目*/
                shopShowType = ShopType.Ebay;
                groupCtrlShop.Text = "易趣 店铺列表";

                LoadShop(shopShowType);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击有啊店铺
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarYoua_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                /*用于判断点击的店铺类目*/
                shopShowType = ShopType.Youa;
                groupCtrlShop.Text = "有啊 店铺列表";

                LoadShop(shopShowType);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击其他店铺
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarOtherShop_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                /*用于判断点击的店铺类目*/
                shopShowType = ShopType.Other;
                groupCtrlShop.Text = "其他 店铺列表";

                LoadShop(shopShowType);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        /// <summary>
        /// 点击GridView焦点行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewShop_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                Alading.Entity.Shop shop = gridViewShop.GetFocusedRow() as Alading.Entity.Shop;
                if (!string.IsNullOrEmpty(shop.nick))
                {
                    barBtnDownload.Enabled = true;
                }
                else
                {
                    barBtnDownload.Enabled = false;
                }
                ClearTextEdit();
                ShowShopDetail(shop);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击查询按钮，进行混淆查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBtn_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitFrm = null;
            try
            {
                if (string.IsNullOrEmpty(tEKeyWord.Text))
                {
                    XtraMessageBox.Show("请先输入关键字", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    //是查询
                    isQuery = true;
                    keyWord = tEKeyWord.Text.Trim().ToUpper();
                    //根据关键字，进行查询操作
                    QueryCheck();
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                //判断是否是查询
                if (!isQuery)
                {
                    LoadShop(shopShowType);
                }
                else
                {
                    QueryCheck();
                }
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击更新店铺
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnDownload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = null;
            try
            {
                Alading.Entity.Shop focusShop = gridViewShop.GetFocusedRow() as Alading.Entity.Shop;
                if (focusShop == null)//未选中
                {
                    XtraMessageBox.Show("请先选中更新的店铺", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                ShopRsp shopRsp = TopService.ShopGet(focusShop.nick);
                string session = SystemHelper.GetSessionKey(focusShop.nick);
                UserRsp userRsp = TopService.UserGet(session, focusShop.nick, string.Empty);

                Alading.Entity.Shop shop = new Alading.Entity.Shop();

                if (shopRsp == null || userRsp == null)//从淘宝网未获取到数据
                {
                    waitFrm.Close();
                    XtraMessageBox.Show("未获取到数据", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ShopCopyData((int)shopShowType, shop, shopRsp.Shop, userRsp.User);

                Alading.Entity.Shop oldShop = ShopService.GetShopByNick(shop.nick);

                if (oldShop == null)//不存在时添加
                {
                    ShopService.AddShop(shop);
                }
                else//存在时更新
                {
                    ShopService.UpdateShop(shop);
                }

                LoadShop(shopShowType);//重新刷新界面
                waitFrm.Close();
                XtraMessageBox.Show("更新成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                waitFrm.Close();
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
                Alading.Entity.Shop focusShop = gridViewShop.GetFocusedRow() as Alading.Entity.Shop;
                if (focusShop == null)
                {
                    XtraMessageBox.Show("请先选中要删除的店铺", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (XtraMessageBox.Show("是否彻底从本地删除", Constants.SYSTEM_PROMPT, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    == DialogResult.Yes)
                {
                    ShopService.RemoveShop(c => c.sid == focusShop.sid);
                    LoadShop(shopShowType);
                }
                else//不从本地彻底删除
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 保存店铺按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveShopBtn_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitFrm = null;
            try
            {
                waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);

                Alading.Entity.Shop shop = SaveShopDetail();
                if (shop.sid == null)
                {
                    shop.sid = string.Empty;
                }
                if (shop.created == null)
                {
                    shop.created = System.DateTime.Now;
                }
                if (shop.modified == null)
                {
                    shop.modified = System.DateTime.Now;
                }

                ShopService.UpdateShop(shop);
                //判断是否是查询
                if (!isQuery)
                {
                    LoadShop(shopShowType);
                }
                else
                {
                    QueryCheck();
                }
                waitFrm.Close();
                XtraMessageBox.Show("保存成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region 点击加载地址
        /// <summary>
        /// 点击选择省份后，加载City的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cBoxState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cBoxCity.Properties.Items.Clear();
                cBoxDistrict.Properties.Items.Clear();
                cBoxCity.Tag = string.Empty;
                cBoxDistrict.Tag = string.Empty;
                cBoxCity.Text = string.Empty;
                cBoxDistrict.Text = string.Empty;

                LoadLocation(cBoxState, cBoxCity);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 点击选择City后，加载区县的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cBoxDistrict.Properties.Items.Clear();
                cBoxDistrict.Text = string.Empty;
                cBoxDistrict.Tag = string.Empty;

                LoadLocation(cBoxCity, cBoxDistrict);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #endregion

        #region 方法
        /// <summary>
        /// 根据情况，展示不同店铺信息
        /// </summary>
        /// <param name="isClearGridView">是否需要清空GridView</param>
        /// <param name="shopType">展示店铺类型</param>
        public void LoadShop(ShopType shopType)
        {
            isQuery = false;
            gridCtrlShop.DataSource = null;
            ClearTextEdit();

            List<Alading.Entity.Shop> shopList = new List<Alading.Entity.Shop>();
            barBtnDownload.Enabled = true;
            barBtnDelete.Enabled = true;
            if (shopType == ShopType.AllShop)//加载全部店铺
            {
                shopList = ShopService.GetAllShop();
            }
            else
            {
                shopList = ShopService.GetShop(c => c.ShopType == (int)shopType);
            }

            //加载数据并显示到界面
            if (shopList != null && shopList.Count != 0)
            {
                ShowShopDetail(shopList);
                //LoadShopGridView(shopList);
            }
            else
            {
                barBtnDownload.Enabled = false;
                barBtnDelete.Enabled = false;
            }
        }

        /// <summary>
        /// 展示店铺详细信息
        /// </summary>
        public void ShowShopDetail(List<Alading.Entity.Shop> shopList)
        {
            gridCtrlShop.DataSource = shopList;
            gridViewShop.BestFitColumns();

            //触发第一行
            if (gridViewShop.FocusedRowHandle == 0)
            {
                Alading.Entity.Shop shop = gridViewShop.GetFocusedRow() as Alading.Entity.Shop;
                ShowShopDetail(shop);
            }
        }

        /// <summary>
        /// 加载State City District的方法
        /// </summary>
        public void LoadLocation(ComboBoxEdit parentComboBoxEdit, ComboBoxEdit comboBoxEdit)
        {
            if (parentComboBoxEdit == null)//加载Location_State的信息
            {
                /*当不存在数据时才进行加载*/
                if (comboBoxEdit.Properties.Items == null || comboBoxEdit.Properties.Items.Count == 0)
                {
                    List<Alading.Entity.Area> areaList = AreaService.GetArea(c => c.parent_id == "1");
                    foreach (Alading.Entity.Area area in areaList)
                    {
                        cBoxState.Tag += area.id + ";";
                        cBoxState.Properties.Items.Add(area.name);
                    }
                }
            }
            else//加载Location_City 或者 Location_District的信息
            {
                int index = parentComboBoxEdit.SelectedIndex;
                if (index == -1 || parentComboBoxEdit.Tag == null)
                {
                    return;
                }
                string[] parentIDStr = parentComboBoxEdit.Tag.ToString().Split(';');
                string parentID = parentIDStr[index];
                List<Alading.Entity.Area> areaList = AreaService.GetArea(c => c.parent_id == parentID);
                foreach (Alading.Entity.Area area in areaList)
                {
                    comboBoxEdit.Tag += area.id + ";";
                    comboBoxEdit.Properties.Items.Add(area.name);
                }
            }
        }

        /// <summary>
        /// 显示店铺详细信息
        /// </summary>
        /// <param name="shop"></param>
        public void ShowShopDetail(Alading.Entity.Shop shop)
        {
            tENick.Text = shop.nick;
            tETitle.Text = shop.title;
            tESellerName.Text = shop.seller_name;
            tESellerZip.Text = shop.seller_zip;
            tETelephone.Text = shop.seller_tel;
            tEPhone.Text = shop.seller_mobile;
            cBoxState.Text = shop.seller_state;
            cBoxCity.Text = shop.seller_city;
            cBoxDistrict.Text = shop.seller_district;
            tEAddress.Text = shop.seller_address;
            labelSellerLevel.Text = shop.seller_level == null ? string.Empty : shop.seller_level.ToString();
            labelSellerScore.Text = shop.seller_score == null ? string.Empty : shop.seller_score.ToString();
            labelSellerTotalNum.Text = shop.seller_total_num == null ? string.Empty : shop.seller_total_num.ToString();
            labelSellerGoodNum.Text = shop.seller_good_num == null ? string.Empty : shop.seller_good_num.ToString();
            if (!string.IsNullOrEmpty(shop.desc))
            {
                editorDesc.BodyHtml = HttpUtility.HtmlDecode(shop.desc);
            }
            if (!string.IsNullOrEmpty(shop.bulletin))
            {
                editorBulletin.BodyHtml = HttpUtility.HtmlDecode(shop.bulletin);
            }
            if (shop.seller_level != null)
            {
                ((ILinearGauge)rating.Gauges[0]).Scales[0].Value = (float)shop.seller_level * 5 - 1;
            }
            else
            {
                ((ILinearGauge)rating.Gauges[0]).Scales[0].Value = 0;
            }
        }

        /// <summary>
        /// 传递店铺详细信息
        /// </summary>
        /// <param name="shop"></param>
        public Alading.Entity.Shop SaveShopDetail()
        {
            Alading.Entity.Shop focusShop = gridViewShop.GetFocusedRow() as Alading.Entity.Shop;

            if (focusShop != null)
            {
                focusShop.nick = tENick.Text;
                focusShop.title = tETitle.Text;
                focusShop.seller_name = tESellerName.Text;
                focusShop.seller_zip = tESellerZip.Text;
                focusShop.seller_tel = tETelephone.Text;
                focusShop.seller_mobile = tEPhone.Text;
                focusShop.seller_state = cBoxState.Text;
                focusShop.seller_city = cBoxCity.Text;
                focusShop.seller_district = cBoxDistrict.Text;
                focusShop.seller_address = tEAddress.Text;
            }
            else
            {
                focusShop = new Alading.Entity.Shop();
            }
            return focusShop;
        }

        /// <summary>
        /// 赋值给Shop表
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="TbShop"></param>
        private void ShopCopyData(int shopType, Entity.Shop shop, Taobao.Entity.Shop TbShop, Taobao.Entity.User TbUser)
        {
            shop.sid = TbShop.Sid;//店铺编号
            shop.cid = TbShop.Cid;//店铺类目编号
            shop.nick = TbShop.SellerNick;//店主淘宝会员号
            shop.title = TbShop.Title;//店铺名称

            shop.desc = TbShop.Description == null ? string.Empty : HttpUtility.HtmlEncode(TbShop.Description);//店铺描述
            shop.bulletin = TbShop.Bulletin == null ? string.Empty : HttpUtility.HtmlEncode(TbShop.Bulletin);//店铺公告

            shop.pic_path = TbShop.PicPath;//店标地址
            if (!string.IsNullOrEmpty(TbShop.Created))
            {
                shop.created = DateTime.Parse(TbShop.Created);//创建时间
            }
            if (!string.IsNullOrEmpty(TbShop.Modified))
            {
                shop.modified = DateTime.Parse(TbShop.Modified);//修改时间
            }

            shop.sex = TbUser.Sex;

            /*卖家信用*/
            shop.seller_level = TbUser.SellerCredit.Level;
            shop.seller_score = TbUser.SellerCredit.Score;
            shop.seller_total_num = TbUser.SellerCredit.TotalNum;
            shop.seller_good_num = TbUser.SellerCredit.GoodNum;

            /*用户当前居住地公开信息*/
            shop.seller_zip = TbUser.Location.Zip;
            shop.seller_state = TbUser.Location.State;
            shop.seller_address = TbUser.Location.Address;
            shop.seller_city = TbUser.Location.City;
            shop.seller_state = TbUser.Location.State;
            shop.seller_country = TbUser.Location.Country;
            shop.seller_district = TbUser.Location.District;

            shop.user_created = DateTime.Parse(TbUser.Created);
            shop.last_visit = Convert.ToDateTime(TbUser.LastVisit);
            shop.birthday = Convert.ToDateTime(TbUser.Birthday);
            shop.type = TbUser.Type;//用户类型。可选值:B(B商家),C(C商家) 
            if (TbUser.Type == "B")
            {
                shop.ShopType = (int)ShopType.TaobaoBShop;
                shop.ShopTypeName = "淘宝商城店（B店）";
            }
            else if (TbUser.Type == "C")
            {
                shop.ShopType = (int)ShopType.TaobaoCShop;
                shop.ShopTypeName = "淘宝普通店（C店）";
            }
            else
            {
                shop.ShopType = (int)ShopType.Other;
                shop.ShopTypeName = "其它";
            }
            shop.has_more_pic = TbUser.HasMorePic;
            shop.item_img_num = TbUser.ItemImgNum;
            shop.item_img_size = TbUser.ItemImgSize;
            shop.prop_img_num = TbUser.PropImgNum;
            shop.prop_img_size = TbUser.PropImgSize;
            shop.auto_repost = TbUser.AutoRepost;
            shop.promoted_type = TbUser.PromotedType;
            shop.status = TbUser.Status;//状态值:normal(正常),inactive(未激活),delete(删除),reeze(冻结),supervise(监管)
            shop.alipay_bind = TbUser.AlipayAccount;
            shop.consumer_protection = TbUser.ConsumerProtection;
            shop.alipay_account = TbUser.AlipayAccount;
            shop.alipay_no = TbUser.AlipayNo;
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        public void QueryCheck()
        {
            gridCtrlShop.DataSource = null;
            ClearTextEdit();
            Func<Alading.Entity.Shop, bool> func = null;
            if (shopShowType == ShopType.AllShop)//加载全部店铺
            {
                func = new Func<Alading.Entity.Shop, bool>(c => c.title.ToUpper().Contains(keyWord)
                    || c.nick.ToUpper().Contains(keyWord));
            }
            else
            {
                func = new Func<Alading.Entity.Shop, bool>(c => c.title.ToUpper().Contains(keyWord)
                    || c.nick.ToUpper().Contains(keyWord) && c.ShopType == (int)shopShowType);
            }

            List<Alading.Entity.Shop> shopList = ShopService.GetShop(func);

            if (shopList == null || shopList.Count == 0)
            {
                return;
            }

            ShowShopDetail(shopList);
        }

        /// <summary>
        /// 清空TextEdit里面的内容
        /// </summary>
        public void ClearTextEdit()
        {
            tENick.Text = string.Empty;
            tETitle.Text = string.Empty;
            tESellerName.Text = string.Empty;
            tESellerZip.Text = string.Empty;
            tETelephone.Text = string.Empty;
            tEPhone.Text = string.Empty;
            cBoxState.Text = string.Empty;
            cBoxCity.Text = string.Empty;
            cBoxDistrict.Text = string.Empty;
            tEAddress.Text = string.Empty;
            labelSellerLevel.Text = string.Empty;
            labelSellerScore.Text = string.Empty;
            labelSellerTotalNum.Text = string.Empty;
            labelSellerGoodNum.Text = string.Empty;
            editorDesc.BodyHtml = string.Empty;
            editorBulletin.BodyHtml = string.Empty;
            ((ILinearGauge)rating.Gauges[0]).Scales[0].Value = (float)0;
        }
        #endregion
    }
}