using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;
using Alading.Core.Enum;
using DevExpress.XtraGrid;
using Alading.Utils;
using Alading.Core.Helper;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API;
using DevExpress.Utils;
using Alading.Taobao;
using System.Web;


namespace Alading.Forms.Init
{
    public partial class InitSystem : DevExpress.XtraEditors.XtraForm
    {
        public InitSystem()
        {
            InitializeComponent();
        }

        #region field

        private Alading.Entity.Shop selectedShop = null;
        private List<Alading.Entity.Shop> shopList = SystemHelper.ShopList; //new List<Alading.Entity.Shop>();

        private StockHouse selectedHouse = null;
        private List<Alading.Entity.StockHouse> houseList = new List<Alading.Entity.StockHouse>();

        private User selectedUser = null;
        private List<Alading.Entity.User> userList = new List<Alading.Entity.User>();

        private Alading.Entity.Supplier selectedSupplier = null;
        private List<Alading.Entity.Supplier> supplierList = new List<Alading.Entity.Supplier>();

        private List<Alading.Entity.Role> roleList = new List<Alading.Entity.Role>();

        int selectedShopType = 0;

        #endregion

        #region init_method

        private void SetTextBoxEmpty(params TextEdit[] controls)
        {
            foreach (var item in controls)
            {
                item.Text = string.Empty;
            }
        }

        private void InitShopInformation()
        {
            SetTextBoxEmpty(
                txShopNick, txShopTitle, txCompName, txShopPassword,
                txShopOwner, txCompName, txCompZip,
                txShopAddr, txShopMobile, txShopTel,
                txShopDbIP, txShopDbName, txShopDbPass,
                txShopDbPort, txShopDbTP, txShopDbUser);

            txShopNick.Focus();
        }

        private void InitHouseInformation()
        {
            SetTextBoxEmpty(txHouseName, txHouseAddr, txHouseContact, txHouseTel);

            txHouseName.Focus();
        }

        private void InitEmployeeInformation()
        {
            SetTextBoxEmpty(txEmpAccount, txEmpAddr, txEmpName, txEmpPass, txEmpTel);

            txEmpAccount.Focus();
        }

        private void InitSupplierInformation()
        {
            SetTextBoxEmpty(
                txSupAccount, txSupAddr, txSupBank,
                txSupName, txSupUser, txSupZip, txSupTaobao);

            txSupName.Focus();
        }

        private void InitUserRoleList()
        {
            roleList = GetRoles();

            for (int i = 0; i < roleList.Count; i++)
            {
                DevExpress.XtraEditors.Controls.CheckedListBoxItem item
                    = new DevExpress.XtraEditors.Controls.CheckedListBoxItem(roleList[i].RoleName, false);

                if (roleList[i].RoleType == 1) item.Enabled = false;

                cblEmpRoleList.Items.Add(item);
            }
        }

        private void InitShopList()
        {
            cblShopList.Items.Clear();
            foreach (var i in shopList)
            {
                DevExpress.XtraEditors.Controls.CheckedListBoxItem item
                    = new DevExpress.XtraEditors.Controls.CheckedListBoxItem(i.title, false);
                cblShopList.Items.Add(item);
            }
        }

        private void InitHouseList()
        {
            cblHouseList.Items.Clear();
            foreach (var i in houseList)
            {
                DevExpress.XtraEditors.Controls.CheckedListBoxItem item
                    = new DevExpress.XtraEditors.Controls.CheckedListBoxItem(i.HouseName, false);
                cblHouseList.Items.Add(item);
            }
        }

        #endregion

        #region data_binding

        private void BindData<DataType, ControlType>(DataType data, IList<DataType> collection, ControlType control)
        {
            Type control_t = typeof(ControlType);

            var prop_info = control_t.GetProperty("DataSource", typeof(object));

            if (prop_info != null)
            {
                prop_info.SetValue(control, null, null);

                collection.Add(data);

                prop_info.SetValue(control, collection, null);
            }
        }

        private void RebindData<ControlType>(object collection, ControlType control)
        {
            Type control_t = typeof(ControlType);

            var prop_info = control_t.GetProperty("DataSource", typeof(object));

            if (prop_info != null)
            {
                prop_info.SetValue(control, null, null);
                prop_info.SetValue(control, collection, null);
            }
        }

        private void BindShopList(CheckedListBoxControl control, List<Alading.Entity.Shop> data)
        {
            control.Items.Clear();
            foreach (var item in data)
            {
                control.Items.Add(item, false);
            }
        }

        private void SetCheckedListBoxControl(CheckedListBoxControl control, CheckState state)
        {
            foreach (var i in control.Items)
            {
                DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
                    = i as DevExpress.XtraEditors.Controls.CheckedListBoxItem;
                control1.CheckState = state;
            }
        }

        #endregion

        #region read_data

        private List<Alading.Entity.Shop> GetShops()
        {
            return SystemHelper.ShopList;
        }

        private List<Alading.Entity.StockHouse> GetStockHouses()
        {
            return Alading.Business.StockHouseService.GetAllStockHouse();
        }

        private List<Alading.Entity.User> GetUsers()
        {
            return Alading.Business.UserService.GetAllUser();
        }

        private List<Alading.Entity.Supplier> GetSuppliers()
        {
            return Alading.Business.SupplierService.GetAllSupplier();
        }

        private List<Alading.Entity.Role> GetRoles()
        {
            return Alading.Business.RoleService.GetAllRole();
        }

        //private List<Alading.Entity.Role> GetUserRoles(User user)
        //{
        //    var query1 = Alading.Business.UserRoleService.GetUserRole(c => c.UserCode == user.UserCode);
        //    var list1 = new List<Alading.Entity.Role>();
        //    foreach (var item in query1)
        //    {

        //    }
        //    return list1;
        //}

        #endregion

        #region write_data

        #region write_shop

        private ReturnType AddShop(Alading.Entity.Shop data)
        {
            // add shop to database
            return Alading.Business.ShopService.AddShop(data);
        }

        private ReturnType UpdateShop(Alading.Entity.Shop data)
        {
            // update shop in database
            return Alading.Business.ShopService.UpdateShop(data);
        }

        private ReturnType DeleteShop(Alading.Entity.Shop data)
        {
            // delete shop from database
            return Alading.Business.ShopService.RemoveShop(data.sid);
        }

        #endregion

        #region write_stockhouse

        private ReturnType AddHouse(StockHouse data)
        {
            // add stock house to database
            return Alading.Business.StockHouseService.AddStockHouse(data);
        }

        private ReturnType UpdateHouse(StockHouse data)
        {
            // update stock house in database
            return Alading.Business.StockHouseService.UpdateStockHouse(data);
        }

        private ReturnType DeleteHouse(StockHouse data)
        {
            // delete stock house from database
            return Alading.Business.StockHouseService.RemoveStockHouse(data.StockHouseCode);
        }

        #endregion

        #region write_user

        private ReturnType AddEmployee(User data)
        {
            // add user to database
            return Alading.Business.UserService.AddUser(data);
        }

        private ReturnType UpdateEmployee(User data)
        {
            // update user in database
            return Alading.Business.UserService.UpdateUser(data);
        }

        private ReturnType DeleteEmployee(User data)
        {
            // delete user from database
            return Alading.Business.UserService.RemoveUser(data.UserCode);
        }

        private void SaveEmployeeInformation(User user, List<UserRole> roles, List<UserShop> shops, List<UserStockHouse> houses)
        {
            // save user detail information to database

            Alading.Business.UserService.UpdateUser(user);

            // update user roles
            Alading.Business.UserRoleService.RemoveUserRole(c => c.UserCode == user.UserCode);
            Alading.Business.UserRoleService.AddUserRole(roles);

            // update user shop
            Alading.Business.UserShopService.RemoveUserShop(c => c.UserCode == user.UserCode);
            Alading.Business.UserShopService.AddUserShop(shops);

            // update user stock house
            Alading.Business.UserStockHouseService.RemoveUserStockHouse(c => c.UserCode == user.UserCode);
            Alading.Business.UserStockHouseService.AddUserStockHouse(houses);
        }

        private ReturnType SaveEmployeeInformation(User user, List<Role> roles, List<Alading.Entity.Shop> shops, List<StockHouse> houses)
        {
            return Alading.Business.UserService.UpdateUser(user, roles, shops, houses);
        }

        #endregion

        #region write_supplier

        private ReturnType AddSupplier(Alading.Entity.Supplier data)
        {
            // add user to database
            return Alading.Business.SupplierService.AddSupplier(data);
        }

        private ReturnType UpdateSupplier(Alading.Entity.Supplier data)
        {
            // update user in database
            return Alading.Business.SupplierService.UpdateSupplier(data);
        }

        private ReturnType DeleteSupplier(Alading.Entity.Supplier data)
        {
            // delete user from database
            return Alading.Business.SupplierService.RemoveSupplier(data.SupplierCode);
        }

        #endregion

        #endregion

        #region fill_data

        private void FillShop(Alading.Entity.Shop shop)
        {
            int? port = null;

            if (!string.IsNullOrEmpty(txShopDbPort.Text))
            {
                port = Convert.ToInt32(txShopDbPort.Text);
            }

            shop.type = cbShopType.Text;
            shop.nick = txShopNick.Text;
            shop.title = txShopTitle.Text;
            shop.seller_name = txShopOwner.Text;
            shop.seller_company = txCompName.Text;
            shop.seller_zip = txCompZip.Text;
            shop.seller_mobile = txShopMobile.Text;
            shop.seller_tel = txShopTel.Text;
            shop.seller_address = txShopAddr.Text;
            shop.ShopTypeName = cbShopType.SelectedText;
            shop.ShopType = selectedShopType + 1;
            shop.password = txShopPassword.Text;
            shop.ShopTypeName = cbShopType.Text;
            shop.type = cbShopType.Text;

            shop.db_ip = txShopDbIP.Text;
            shop.db_name = txShopDbName.Text;
            shop.db_user = txShopDbUser.Text;
            shop.db_password = txShopDbPass.Text;
            shop.db_port = port;
            shop.db_prefix = txShopDbTP.Text;
        }

        private void FillHouse(StockHouse house)
        {
            house.HouseName = txHouseName.Text;
            house.HouseAddress = txHouseAddr.Text;
            house.HouseContact = txHouseContact.Text;
            house.HouseTel = txHouseTel.Text;
        }

        private void FillEmployee(User user)
        {
            user.account = txEmpAccount.Text;
            user.password = txEmpPass.Text;
            user.nick = txEmpName.Text;
            user.addr = txEmpAddr.Text;
            user.tel = txEmpTel.Text;
            //user.taobao_account = txEmpTaobaoAccount.Text;
            //user.taobao_password = txEmpTaobaoPassword.Text;
        }

        private void FillSupplier(Alading.Entity.Supplier supplier)
        {
            supplier.SupplierName = txSupName.Text;
            supplier.SupplierContact = txContact.Text;
            supplier.SupplierPhone = txContactTel.Text;
            supplier.SupplierZipcode = txSupZip.Text;
            supplier.SupplierAddress = txSupAddr.Text;
            supplier.SupplierBank = txSupBank.Text;
            supplier.SupplierBankUser = txSupUser.Text;
            supplier.SupplierBankAccount = txSupAccount.Text;
            supplier.SupplierTaobaoAccount = txSupTaobao.Text;
            supplier.SupplierWangWang = txSupTaobao.Text;
        }

        #endregion

        #region entity_validation

        private bool ValidateShop(Alading.Entity.Shop shop, EntityState state)
        {
            if (string.IsNullOrEmpty(shop.nick))
            {
                state.AddMessage("nick", "输入店主昵称");
            }
            if (string.IsNullOrEmpty(shop.password))
            {
                state.AddMessage("password", "输入店铺密码");
            }

            return state.State;
        }

        private bool ValidateStockHouse(Alading.Entity.StockHouse house, EntityState state)
        {
            if (string.IsNullOrEmpty(house.HouseName))
            {
                state.AddMessage("HouseName", "输入仓库名称");
            }
            if (string.IsNullOrEmpty(house.HouseAddress))
            {
                state.AddMessage("HouseAddress", "输入仓库地址");
            }
            return state.State;
        }

        private bool ValidateUser(Alading.Entity.User user, EntityState state)
        {
            if (string.IsNullOrEmpty(user.account))
            {
                state.AddMessage("account", "输入登录帐号");
            }
            if (string.IsNullOrEmpty(user.password))
            {
                state.AddMessage("password", "输入登录密码");
            }
            if (string.IsNullOrEmpty(user.nick))
            {
                state.AddMessage("nick", "输入员工姓名");
            }
            return state.State;
        }

        private bool ValidateSupplier(Alading.Entity.Supplier supplier, EntityState state)
        {
            if (string.IsNullOrEmpty(supplier.SupplierName))
            {
                state.AddMessage("SupplierName", "输入供应商名称");
            }
            if (string.IsNullOrEmpty(supplier.SupplierContact))
            {
                state.AddMessage("SupplierContact", "输入联系人");
            }
            return state.State;
        }

        private static void ValidateFailed(EntityState state)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("保存数据失败，请更正以下错误：");
            for (int i = 0; i < state.StateValues.Count; i++)
            {
                builder.AppendLine(string.Format("{0}. {1}", i + 1, state.GetMessage(i)));
            }
            XtraMessageBox.Show(builder.ToString());
        }

        #endregion

        private void InitSystem_Load(object sender, EventArgs e)
        {
            cbShopType.SelectedIndex = 0;

            shopList = GetShops();
            houseList = GetStockHouses();
            userList = GetUsers();
            supplierList = GetSuppliers();

            gcShopGrid.DataSource = shopList;
            gcHouseGrid.DataSource = houseList;
            gcEmpGrid.DataSource = userList;
            gcSupGrid.DataSource = supplierList;

            initShopControl1.ShopList = shopList;

            InitUserRoleList();
            InitShopList();
            InitHouseList();
        }

        #region init_shop_wizard_event

        private void cbShopType_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedShopType = cbShopType.SelectedIndex;
            gcShopDb.Enabled = cbShopType.SelectedText == "ShopEx";
        }

        private void btAddShop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // initialize shop information
            InitShopInformation();
        }

        private void btSaveShop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // add shop to list and save shop data

            // save data

            Alading.Entity.Shop s1 = new Alading.Entity.Shop();
            FillShop(s1);
            s1.sid = Guid.NewGuid().ToString();

            EntityState state = new EntityState();
            if (ValidateShop(s1, state))
            {
                // encrypt shop password
                s1.password = SecurityHelper.TripleDESEncrypt(s1.password);

                ReturnType result = AddShop(s1);
                if (result == ReturnType.Success)
                {
                    // add to list and grid view
                    BindData<Alading.Entity.Shop, GridControl>(s1, shopList, gcShopGrid);
                    InitShopList();
                    InitShopInformation();
                }
                // other result to do
            }
            else
            {
                ValidateFailed(state);
            }
        }

        private void btUpdateShop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // update shop information
            if (selectedShop != null)
            {
                FillShop(selectedShop);
                EntityState state = new EntityState();
                if (ValidateShop(selectedShop, state))
                {
                    // encrypt shop password
                    selectedShop.password = SecurityHelper.TripleDESEncrypt(selectedShop.password);

                    ReturnType result = UpdateShop(selectedShop);
                    if (result == ReturnType.Success)
                    {
                        // refresh grid view
                        RebindData<GridControl>(shopList, gcShopGrid);
                        InitShopList();
                    }
                    // other result to do
                }
                else
                {
                    ValidateFailed(state);
                }
            }
        }

        private void btDelShop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // delete shop data and remove from list

            if (selectedShop != null)
            {
                ReturnType result = DeleteShop(selectedShop);

                if (result == ReturnType.Success)
                {
                    // remove data from list
                    shopList.Remove(selectedShop);

                    // refresh grid view
                    RebindData<GridControl>(shopList, gcShopGrid);
                    RebindData<CheckedListBoxControl>(shopList, cblShopList);

                    // clear information
                    InitShopInformation();
                }
                // other result to do
            }
        }

        private void btnSycShop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedShop != null)
            {
                FillShop(selectedShop);
                EntityState state = new EntityState();
                if (ValidateShop(selectedShop, state))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.OPERATE_SYC_DATA);
                    // encrypt shop password
                    string cypher = SecurityHelper.TripleDESEncrypt(selectedShop.password);
                    selectedShop.password = cypher;

                    // save shop
                    ReturnType result = UpdateShop(selectedShop);
                    if (result == ReturnType.Success)
                    {
                        try
                        {
                            ShopRsp shopRsp = TopService.ShopGet(selectedShop.nick);
                            string session = SystemHelper.GetSessionKey(selectedShop.nick);
                            UserRsp userRsp = TopService.UserGet(session, selectedShop.nick, string.Empty);
                            if (shopRsp == null || userRsp == null)
                            {
                                throw new Exception();
                            }
                            else
                            {
                                ShopCopyData(selectedShop, shopRsp.Shop, userRsp.User);
                                if (selectedShop.password != cypher) selectedShop.password = cypher;
                                result = UpdateShop(selectedShop);
                            }

                            // refresh grid view
                            RebindData<GridControl>(shopList, gcShopGrid);
                            InitShopList();

                            waitFrm.Close();
                        }
                        catch (Exception ex)
                        {
                            selectedShop.ShopType = (int)Alading.Core.Enum.ShopType.Other;
                            selectedShop.ShopTypeName = "其它";
                            selectedShop.type = "其它";
                            UpdateShop(selectedShop);
                            waitFrm.Close();
                            XtraMessageBox.Show("同步店铺信息失败," + ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("同步店铺信息失败", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    ValidateFailed(state);
                }
            }
        }

        private void gvShopView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                // select data row

                int[] rows = gvShopView.GetSelectedRows();

                if (rows.Length > 0)
                {
                    Alading.Entity.Shop s1 = shopList[rows[0]];

                    if (s1 != null)
                    {
                        // put data in controls
                        cbShopType.SelectedIndex = (s1.ShopType ?? 8) - 1;
                        txShopNick.Text = s1.nick;
                        txShopTitle.Text = s1.title;
                        txShopOwner.Text = s1.seller_name;
                        txCompName.Text = s1.seller_company;
                        txCompZip.Text = s1.seller_zip;
                        txShopMobile.Text = s1.seller_mobile;
                        txShopTel.Text = s1.seller_tel;
                        txShopAddr.Text = s1.seller_address;
                        //txShopPassword.Text = s1.password;
                        //SecurityHelper.TripleDESDecrypt(s1.password);
                        if (string.IsNullOrEmpty(s1.password))
                        {
                            txShopPassword.Text = string.Empty;
                        }
                        else
                        {
                            txShopPassword.Text = SecurityHelper.TripleDESDecrypt(s1.password);
                        }

                        txShopDbIP.Text = s1.db_ip;
                        txShopDbName.Text = s1.db_name;
                        txShopDbUser.Text = s1.db_user;
                        txShopDbPass.Text = s1.db_password;
                        txShopDbPort.Text = s1.db_port.ToString();
                        txShopDbTP.Text = s1.db_prefix;

                        selectedShop = s1;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btn_save_shop_1_Click(object sender, EventArgs e)
        {
            if (selectedShop != null)
            {
                FillShop(selectedShop);
                EntityState state = new EntityState();
                if (ValidateShop(selectedShop, state))
                {
                    // encrypt shop password
                    selectedShop.password = SecurityHelper.TripleDESEncrypt(selectedShop.password);

                    ReturnType result = UpdateShop(selectedShop);
                    if (result == ReturnType.Success)
                    {
                        // refresh grid view
                        RebindData<GridControl>(shopList, gcShopGrid);
                        InitShopList();
                    }
                    // other result to do
                }
                else
                {
                    ValidateFailed(state);
                }
            }
        }

        private void btn_syc_shop_1_Click(object sender, EventArgs e)
        {
            if (selectedShop != null)
            {
                FillShop(selectedShop);
                EntityState state = new EntityState();
                if (ValidateShop(selectedShop, state))
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.OPERATE_SYC_DATA);
                    // encrypt shop password
                    string cypher = SecurityHelper.TripleDESEncrypt(selectedShop.password);
                    selectedShop.password = cypher;

                    // save shop
                    ReturnType result = UpdateShop(selectedShop);
                    if (result == ReturnType.Success)
                    {
                        try
                        {
                            ShopRsp shopRsp = TopService.ShopGet(selectedShop.nick);
                            string session = SystemHelper.GetSessionKey(selectedShop.nick);
                            UserRsp userRsp = TopService.UserGet(session, selectedShop.nick, string.Empty);
                            if (shopRsp == null || userRsp == null)
                            {
                                throw new Exception();
                            }
                            else
                            {
                                ShopCopyData(selectedShop, shopRsp.Shop, userRsp.User);
                                if (selectedShop.password != cypher) selectedShop.password = cypher;
                                result = UpdateShop(selectedShop);
                            }

                            // refresh grid view
                            RebindData<GridControl>(shopList, gcShopGrid);
                            InitShopList();

                            waitFrm.Close();
                        }
                        catch (Exception ex)
                        {
                            selectedShop.ShopType = (int)Alading.Core.Enum.ShopType.Other;
                            selectedShop.ShopTypeName = "其它";
                            selectedShop.type = "其它";
                            UpdateShop(selectedShop);
                            waitFrm.Close();
                            XtraMessageBox.Show("同步店铺信息失败," + ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("同步店铺信息失败", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    ValidateFailed(state);
                }
            }
        }

        #endregion

        #region init_stock_house_wizard_event

        private void btAddHouse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // initialize house information
            InitHouseInformation();
        }

        private void btSaveHouse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // add house to list and save house data

            // save data

            StockHouse s1 = new StockHouse();
            FillHouse(s1);
            s1.StockHouseCode = Guid.NewGuid().ToString();
            s1.HouseRemark = string.Empty;

            EntityState state = new EntityState();
            if (ValidateStockHouse(s1, state))
            {
                ReturnType result = AddHouse(s1);
                if (result == ReturnType.Success)
                {
                    // add to list and grid view
                    BindData<Alading.Entity.StockHouse, GridControl>(s1, houseList, gcHouseGrid);
                    InitHouseList();
                    InitHouseInformation();
                }
                // other result to do
            }
            else
            {
                ValidateFailed(state);
            }
        }

        private void btUpdateHouse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // update stock house information
            if (selectedHouse != null)
            {
                FillHouse(selectedHouse);
                EntityState state = new EntityState();
                if (ValidateStockHouse(selectedHouse, state))
                {
                    ReturnType result = UpdateHouse(selectedHouse);

                    if (result == ReturnType.Success)
                    {
                        // refresh grid view
                        RebindData<GridControl>(houseList, gcHouseGrid);
                        InitHouseList();
                    }
                    // other result to do
                }
                else
                {
                    ValidateFailed(state);
                }
            }
        }

        private void btDelHouse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // delete house from list
            if (selectedHouse != null)
            {
                if (selectedHouse.StockHouseCode == Alading.Taobao.Constants.DEFAULT_STOCKHOUSE_CODE)
                {
                    XtraMessageBox.Show("该仓库为系统默认仓库，不能删除！");
                    return;
                }

                ReturnType result = DeleteHouse(selectedHouse);

                if (result == ReturnType.Success)
                {
                    // remove data from list
                    houseList.Remove(selectedHouse);

                    // refresh grid view                
                    RebindData<GridControl>(houseList, gcHouseGrid);
                    RebindData<CheckedListBoxControl>(houseList, cblHouseList);

                    // clear information
                    InitHouseInformation();
                }
                // other result to do
            }
        }

        private void gvHouseView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            // select a stock house

            int[] rows = gvHouseView.GetSelectedRows();

            if (rows.Length > 0)
            {
                Alading.Entity.StockHouse s1 = houseList[rows[0]];

                if (s1 != null)
                {
                    // put data in controls
                    txHouseName.Text = s1.HouseName;
                    txHouseAddr.Text = s1.HouseAddress;
                    txHouseContact.Text = s1.HouseContact;
                    txHouseTel.Text = s1.HouseTel;

                    selectedHouse = s1;
                }
            }
        }

        #endregion

        #region init_employee_wizard_event

        private void btAddEmp_Click(object sender, EventArgs e)
        {
            // initialize employee information
            InitEmployeeInformation();
        }

        private void btSaveEmp_Click(object sender, EventArgs e)
        {
            // add employee to list and save user data

            // save data

            User u1 = new User();
            FillEmployee(u1);
            u1.UserCode = Guid.NewGuid().ToString();

            EntityState state = new EntityState();
            if (ValidateUser(u1, state))
            {
                u1.password = SecurityHelper.TripleDESEncrypt(u1.password);
                ReturnType result = AddEmployee(u1);
                if (result == ReturnType.Success)
                {
                    // add to list and grid view
                    BindData<User, GridControl>(u1, userList, gcEmpGrid);
                    InitEmployeeInformation();
                }
                // other result to do
            }
            else
            {
                ValidateFailed(state);
            }
        }

        private void btUpdateEmp_Click(object sender, EventArgs e)
        {
            // update employee information
            if (selectedUser != null)
            {
                // update user information
                FillEmployee(selectedUser);

                EntityState state = new EntityState();
                if (ValidateUser(selectedUser, state))
                {
                    selectedUser.password = SecurityHelper.TripleDESEncrypt(selectedUser.password);
                    ReturnType result = UpdateEmployee(selectedUser);
                    if (result == ReturnType.Success)
                    {
                        // refresh grid view
                        RebindData<GridControl>(userList, gcEmpGrid);
                    }
                    // other result to do
                }
                else
                {
                    ValidateFailed(state);
                }
            }
        }

        private void btDelEmp_Click(object sender, EventArgs e)
        {
            // delete employee from list
            if (selectedUser != null)
            {
                ReturnType result = DeleteEmployee(selectedUser);

                if (result == ReturnType.Success)
                {
                    // remove data from list
                    userList.Remove(selectedUser);

                    // refresh grid view                
                    RebindData<GridControl>(userList, gcEmpGrid);

                    // clear information
                    InitEmployeeInformation();
                }
                // other result to do
            }
        }

        private void gvEmpView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //// select a employee from list
            //int[] rows = gvEmpView.GetSelectedRows();

            //if (rows.Length > 0)
            //{
            //    Alading.Entity.User s1 = userList[rows[0]];

            //    if (s1 != null)
            //    {
            //        // put data in controls
            //        txEmpAccount.Text = s1.account;
            //        txEmpPass.Text = s1.password;
            //        txEmpName.Text = s1.nick;
            //        txEmpTel.Text = s1.tel;
            //        txEmpAddr.Text = s1.addr;

            //        selectedUser = s1;

            //        SetCheckedListBoxControl(cblEmpRoleList, CheckState.Unchecked);
            //        SetCheckedListBoxControl(cblShopList, CheckState.Unchecked);
            //        SetCheckedListBoxControl(cblHouseList, CheckState.Unchecked);

            //        // initialize roles
            //        List<UserRole> roles = Alading.Business.UserRoleService.GetUserRole(c => c.UserCode == selectedUser.UserCode);

            //        foreach (var i in roles)
            //        {
            //            int index1 = roleList.FindIndex(c => c.RoleCode == i.RoleCode);

            //            DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
            //                = cblEmpRoleList.Items[index1] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

            //            control1.CheckState = CheckState.Checked;
            //        }

            //        // initialize shops
            //        List<UserShop> shops = Alading.Business.UserShopService.GetUserShop(c => c.UserCode == selectedUser.UserCode);

            //        foreach (var i in shops)
            //        {
            //            int index1 = shopList.FindIndex(c => c.sid == i.ShopId);

            //            DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
            //                = cblShopList.Items[index1] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

            //            control1.CheckState = CheckState.Checked;
            //        }

            //        // initialize stock house
            //        List<UserStockHouse> houses = Alading.Business.UserStockHouseService.GetUserStockHouse(c => c.UserCode == selectedUser.UserCode);

            //        foreach (var i in houses)
            //        {
            //            int index1 = houseList.FindIndex(c => c.StockHouseCode == i.StockHouseCode);

            //            DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
            //                = cblHouseList.Items[index1] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

            //            control1.CheckState = CheckState.Checked;
            //        }
            //    }
            //}
        }

        private void cblEmpRoleList_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            // initialize shop list
            DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
                    = cblEmpRoleList.Items[3] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

            cblShopList.Enabled = ceAllShop.Enabled = (control1.CheckState == CheckState.Checked);

            // initialize stock house list
            DevExpress.XtraEditors.Controls.CheckedListBoxItem control2
                    = cblEmpRoleList.Items[5] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

            cblHouseList.Enabled = ceAllHouse.Enabled = (control2.CheckState == CheckState.Checked);
        }

        private void gvEmpView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0 && userList.Count > 0)
                {
                    Alading.Entity.User s1 = userList[e.FocusedRowHandle];

                    if (s1 != null)
                    {
                        // put data in controls
                        txEmpAccount.Text = s1.account;
                        txEmpPass.Text = s1.password;
                        txEmpName.Text = s1.nick;
                        txEmpTel.Text = s1.tel;
                        txEmpAddr.Text = s1.addr;

                        selectedUser = s1;

                        SetCheckedListBoxControl(cblEmpRoleList, CheckState.Unchecked);
                        SetCheckedListBoxControl(cblShopList, CheckState.Unchecked);
                        SetCheckedListBoxControl(cblHouseList, CheckState.Unchecked);

                        // initialize roles
                        List<UserRole> roles = Alading.Business.UserRoleService.GetUserRole(c => c.UserCode == selectedUser.UserCode);

                        if (cblEmpRoleList.Items.Count > 0 && roles.Count > 0)
                        {
                            foreach (var i in roles)
                            {
                                int index1 = roleList.FindIndex(c => c.RoleCode == i.RoleCode);

                                if (index1 >= 0)
                                {
                                    DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
                                        = cblEmpRoleList.Items[index1] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

                                    control1.CheckState = CheckState.Checked;
                                }
                            }
                        }

                        bool isAdmin = !selectedUser.account.Contains(":");

                        if (isAdmin)
                        {
                            if (cblEmpRoleList.Items.Count > 0)
                            {
                                DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
                                            = cblEmpRoleList.Items[0] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

                                control1.CheckState = CheckState.Checked;
                            }
                        }
                        else
                        {
                            if (cblEmpRoleList.Items.Count > 0)
                            {
                                DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
                                            = cblEmpRoleList.Items[0] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

                                control1.CheckState = CheckState.Unchecked;
                            }
                        }

                        // initialize shops
                        List<UserShop> shops = Alading.Business.UserShopService.GetUserShop(c => c.UserCode == selectedUser.UserCode);

                        if (cblShopList.Items.Count > 0 && shopList.Count > 0)
                        {
                            foreach (var i in shops)
                            {
                                int index1 = shopList.FindIndex(c => c.sid == i.ShopId);

                                if (index1 > 0)
                                {
                                    DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
                                        = cblShopList.Items[index1] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

                                    control1.CheckState = CheckState.Checked;
                                }
                            }
                        }

                        // initialize stock house
                        List<UserStockHouse> houses = Alading.Business.UserStockHouseService.GetUserStockHouse(c => c.UserCode == selectedUser.UserCode);

                        if (cblHouseList.Items.Count > 0 && houseList.Count > 0)
                        {
                            foreach (var i in houses)
                            {
                                int index1 = houseList.FindIndex(c => c.StockHouseCode == i.StockHouseCode);

                                if (index1 >= 0)
                                {
                                    DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
                                        = cblHouseList.Items[index1] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

                                    control1.CheckState = CheckState.Checked;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void ceAllShop_CheckedChanged(object sender, EventArgs e)
        {
            // check or uncheck all shop in list
            SetCheckedListBoxControl(cblShopList, ceAllShop.CheckState);
        }

        private void ceAllHouse_CheckedChanged(object sender, EventArgs e)
        {
            // check or uncheck all stock house in list
            SetCheckedListBoxControl(cblHouseList, ceAllHouse.CheckState);
        }

        private void btSaveUpdate_Click(object sender, EventArgs e)
        {
            DevExpress.Utils.WaitDialogForm waitFrm = new DevExpress.Utils.WaitDialogForm("数据加载,请稍后...");
            waitFrm.Show();

            if (selectedUser != null)
            {
                // save employee information

                //selectedUser.taobao_account = txEmpTaobaoAccount.Text;
                //selectedUser.taobao_password = txEmpTaobaoPassword.Text;

                List<Role> roles = new List<Role>();
                for (int i = 0; i < cblEmpRoleList.Items.Count; i++)
                {
                    DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
                            = cblEmpRoleList.Items[i] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

                    if (control1.CheckState == CheckState.Checked)
                    {
                        roles.Add(roleList[i]);
                    }
                }

                List<Alading.Entity.Shop> shops = new List<Alading.Entity.Shop>();
                if (cblShopList.Enabled)
                {
                    for (int i = 0; i < cblShopList.Items.Count; i++)
                    {
                        DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
                                = cblShopList.Items[i] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

                        if (control1.CheckState == CheckState.Checked)
                        {
                            shops.Add(shopList[i]);
                        }
                    }
                }

                List<StockHouse> houses = new List<StockHouse>();
                if (cblHouseList.Enabled)
                {
                    for (int i = 0; i < cblHouseList.Items.Count; i++)
                    {
                        DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
                                = cblHouseList.Items[i] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

                        if (control1.CheckState == CheckState.Checked)
                        {
                            houses.Add(houseList[i]);
                        }
                    }
                }

                // get selected roles
                //List<UserRole> roles = new List<UserRole>();
                //for (int i = 0; i < cblEmpRoleList.Items.Count; i++)
                //{
                //    DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
                //            = cblHouseList.Items[i] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

                //    if (control1.CheckState == CheckState.Checked)
                //    {
                //        Role r1 = roleList[i];
                //        UserRole r2 = new UserRole()
                //        {
                //            UserCode = selectedUser.UserCode,
                //            RoleCode = r1.RoleCode,
                //            RoleType = r1.RoleType,
                //            UserRoleCode = Guid.NewGuid().ToString(),
                //        };
                //        roles.Add(r2);
                //    }
                //}

                // get selected shops
                //List<UserShop> shops = new List<UserShop>();
                //if (cblShopList.Enabled)
                //{
                //    for (int i = 0; i < cblShopList.Items.Count; i++)
                //    {
                //        DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
                //                = cblShopList.Items[i] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

                //        if (control1.CheckState == CheckState.Checked)
                //        {
                //            Alading.Entity.Shop s1 = shopList[i];
                //            UserShop s2 = new UserShop()
                //            {
                //                UserCode = selectedUser.UserCode,
                //                ShopId = s1.sid,
                //                Enable = true,
                //            };
                //            shops.Add(s2);
                //        }
                //    }
                //}

                // get selected stock house
                //List<UserStockHouse> houses = new List<UserStockHouse>();
                //if (cblHouseList.Enabled)
                //{
                //    for (int i = 0; i < cblHouseList.Items.Count; i++)
                //    {
                //        DevExpress.XtraEditors.Controls.CheckedListBoxItem control1
                //                = cblHouseList.Items[i] as DevExpress.XtraEditors.Controls.CheckedListBoxItem;

                //        if (control1.CheckState == CheckState.Checked)
                //        {
                //            StockHouse s1 = houseList[i];
                //            UserStockHouse s2 = new UserStockHouse()
                //            {
                //                UserCode = selectedUser.UserCode,
                //                StockHouseCode = s1.StockHouseCode,
                //                Enable = true,
                //            };
                //            houses.Add(s2);
                //        }
                //    }
                //}

                ReturnType result = SaveEmployeeInformation(selectedUser, roles, shops, houses);
                if (result != ReturnType.Success)
                {
                    XtraMessageBox.Show("保存数据失败！", "提示");
                    waitFrm.Close();
                }
                else
                {
                    XtraMessageBox.Show("保存数据成功！", "提示");
                    waitFrm.Close();
                }
            }
        }

        #endregion

        #region init_supplier_wizard_event

        private void btAddSup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // initialize supplier information
            InitSupplierInformation();
        }

        private void btSaveSup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // add supplier to list

            // save data

            Alading.Entity.Supplier s1 = new Alading.Entity.Supplier();
            FillSupplier(s1);
            s1.SupplierCode = Guid.NewGuid().ToString();

            EntityState state = new EntityState();
            if (ValidateSupplier(s1, state))
            {
                ReturnType result = AddSupplier(s1);
                if (result == ReturnType.Success)
                {
                    // add to list and grid view
                    BindData<Alading.Entity.Supplier, GridControl>(s1, supplierList, gcSupGrid);
                    InitSupplierInformation();
                }
                // other result to do
            }
            else
            {
                ValidateFailed(state);
            }
        }

        private void btUpdateSup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // update supplier information
            if (selectedSupplier != null)
            {
                FillSupplier(selectedSupplier);

                EntityState state = new EntityState();
                if (ValidateSupplier(selectedSupplier, state))
                {
                    ReturnType result = UpdateSupplier(selectedSupplier);
                    if (result == ReturnType.Success)
                    {
                        // refresh grid view
                        RebindData<GridControl>(supplierList, gcSupGrid);
                    }
                    // other result to do
                }
                else
                {
                    ValidateFailed(state);
                }
            }
        }

        private void btDelSup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // delete supplier from list
            if (selectedSupplier != null)
            {
                ReturnType result = DeleteSupplier(selectedSupplier);

                if (result == ReturnType.Success)
                {
                    // remove data from list
                    supplierList.Remove(selectedSupplier);

                    // refresh grid view                
                    RebindData<GridControl>(supplierList, gcSupGrid);

                    // clear information
                    InitSupplierInformation();
                }
                // other result to do
            }
        }

        private void gvSupView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            // select data row

            int[] rows = gvSupView.GetSelectedRows();

            if (rows.Length > 0)
            {
                Alading.Entity.Supplier s1 = supplierList[rows[0]];

                if (s1 != null)
                {
                    txSupName.Text = s1.SupplierName;
                    txContact.Text = s1.SupplierContact;
                    txContactTel.Text = s1.SupplierPhone;
                    txSupZip.Text = s1.SupplierZipcode;
                    txSupAddr.Text = s1.SupplierAddress;
                    txSupBank.Text = s1.SupplierBank;
                    txSupUser.Text = s1.SupplierBankUser;
                    txSupAccount.Text = s1.SupplierBankAccount;
                    txSupTaobao.Text = s1.SupplierTaobaoAccount;

                    selectedSupplier = s1;
                }
            }
        }

        #endregion

        private void wcShopInit_SelectedPageChanged(object sender, DevExpress.XtraWizard.WizardPageChangedEventArgs e)
        {
            if (e.Page == wizardPage6)
            {
                initShopControl1.Initialize();
            }

            #region 将店铺信息初始化  fxg添加4.17
            /*如果是第一页 要加载第一行数据*/
            if (e.Page == wizardPage1 && gvShopView.RowCount > 0)
            {
                Alading.Entity.Shop s1 = gvShopView.GetRow(0) as Alading.Entity.Shop;
                selectedShop = s1;

                if (s1 != null)
                {
                    // put data in controls
                    cbShopType.SelectedIndex = (s1.ShopType ?? 8) - 1;
                    txShopNick.Text = s1.nick;
                    txShopTitle.Text = s1.title;
                    txShopOwner.Text = s1.seller_name;
                    txCompName.Text = s1.seller_company;
                    txCompZip.Text = s1.seller_zip;
                    txShopMobile.Text = s1.seller_mobile;
                    txShopTel.Text = s1.seller_tel;
                    txShopAddr.Text = s1.seller_address;
                    //txShopPassword.Text = s1.password;
                    //SecurityHelper.TripleDESDecrypt(s1.password);
                    if (string.IsNullOrEmpty(s1.password))
                    {
                        txShopPassword.Text = string.Empty;
                    }
                    else
                    {
                        txShopPassword.Text = SecurityHelper.TripleDESDecrypt(s1.password);
                    }

                    txShopDbIP.Text = s1.db_ip;
                    txShopDbName.Text = s1.db_name;
                    txShopDbUser.Text = s1.db_user;
                    txShopDbPass.Text = s1.db_password;
                    txShopDbPort.Text = s1.db_port.ToString();
                    txShopDbTP.Text = s1.db_prefix;
                }
            }
            #endregion

            #region 仓库信息初始化    fxg添加4.17
            if (e.Page == wizardPage3 && gvHouseView.RowCount > 0)
            {
                Alading.Entity.StockHouse s1 = gvHouseView.GetRow(0) as Alading.Entity.StockHouse;
                selectedHouse = s1;

                if (s1 != null)
                {
                    // put data in controls
                    txHouseName.Text = s1.HouseName;
                    txHouseAddr.Text = s1.HouseAddress;
                    txHouseContact.Text = s1.HouseContact;
                    txHouseTel.Text = s1.HouseTel;
                }
            }
            #endregion

            #region 初始化供应商信息     fxg添加4.17
            if (e.Page == wizardPage4 && gvSupView.RowCount > 0)
            {
                Alading.Entity.Supplier s1 = gvSupView.GetRow(0) as Alading.Entity.Supplier;
                selectedSupplier = s1;

                if (s1 != null)
                {
                    txSupName.Text = s1.SupplierName;
                    txContact.Text = s1.SupplierContact;
                    txContactTel.Text = s1.SupplierPhone;
                    txSupZip.Text = s1.SupplierZipcode;
                    txSupAddr.Text = s1.SupplierAddress;
                    txSupBank.Text = s1.SupplierBank;
                    txSupUser.Text = s1.SupplierBankUser;
                    txSupAccount.Text = s1.SupplierBankAccount;
                    txSupTaobao.Text = s1.SupplierTaobaoAccount;
                }
            #endregion
            }
        }

        private void wcShopInit_CancelClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        private void wcShopInit_FinishClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        private void ShopCopyData(Entity.Shop shop, Taobao.Entity.Shop TbShop, Taobao.Entity.User TbUser)
        {
            shop.sid = TbShop.Sid;//店铺编号
            shop.cid = TbShop.Cid;//店铺类目编号
            shop.nick = TbShop.SellerNick;//店主淘宝会员号
            shop.title = TbShop.Title;//店铺名称
            //shop.password = txShopPassword.Text; // 不能赋值
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

            shop.created = DateTime.Parse(TbUser.Created);
            shop.last_visit = Convert.ToDateTime(TbUser.LastVisit);
            shop.birthday = Convert.ToDateTime(TbUser.Birthday);
            shop.type = TbUser.Type;//用户类型。可选值:B(B商家),C(C商家) 
            if (TbUser.Type == "B")
            {
                shop.ShopType = (int)Alading.Core.Enum.ShopType.TaobaoBShop;
                shop.ShopTypeName = "淘宝商城店（B店）";
            }
            else if (TbUser.Type == "C")
            {
                shop.ShopType = (int)Alading.Core.Enum.ShopType.TaobaoCShop;
                shop.ShopTypeName = "淘宝普通店（C店）";
            }
            else
            {
                shop.ShopType = (int)Alading.Core.Enum.ShopType.Other;
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

        private void wcShopInit_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            if (e.Page == wizardPage1)
            {                
            }
        }

        
    }
}