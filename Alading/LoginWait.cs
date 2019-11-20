using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.WebService;
using Alading.Core.Enum;
using Alading.Entity;
using System.Linq;
using System.Diagnostics;
using Alading.Core.Helper;
using System.IO;
using Alading.Taobao;

namespace Alading
{
    public partial class LoginWait : DevExpress.XtraEditors.XtraForm
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public bool IsLogin { get; private set; }
        public List<Alading.Entity.User> LocalUserList { get; private set; }
        public List<Alading.Entity.Shop> LocalShopList { get; private set; }
        private List<Alading.Entity.User> NetUserList = null;
        private List<Alading.Entity.Shop> NetShopList = null;
        public Alading.Entity.User mainUser { get; private set; }

        private bool firstrun = false;
        private bool approve = false;
        public bool FirstRun { get { return firstrun; } }
        public bool IsMain { get; private set; }

        private Timer waitTimer;
        private BackgroundWorker backWorker;

        public LoginWait()
        {
            InitializeComponent();
            //取最新的版本，与当前的版本号
            ConfigHelper config = new ConfigHelper();
            config.SetConfigName(@"\Alading.exe.config");
            string currentVersionCode = config.ReadConfig("CurrentVersion");
            string versionType = config.ReadConfig("VersionType");
            Alading.WebService.Version version = ServiceHelper.GetNewVersion(versionType);
            if (version != null && version.VersionCode!=null && version.VersionCode != currentVersionCode)
            {
                DialogResult result = XtraMessageBox.Show("检测到阿拉丁有最新版本，是否现在更新？", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    string updatePath = Application.StartupPath + "\\Alading.Updater.exe";
                    if (File.Exists(updatePath))
                    {
                        Process.Start(updatePath);
                    }
                }
            }
            waitTimer = new Timer();
            waitTimer.Interval = 600000;
            waitTimer.Tick += new EventHandler(waitTimer_Tick);

            backWorker = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = false };
            backWorker.DoWork += new DoWorkEventHandler(backWorker_DoWork);
            backWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backWorker_RunWorkerCompleted);
            backWorker.ProgressChanged += new ProgressChangedEventHandler(backWorker_ProgressChanged);

            LocalUserList = new List<Alading.Entity.User>();
            LocalShopList = new List<Alading.Entity.Shop>();
            NetShopList = new List<Shop>();
            NetUserList = new List<User>();
            mainUser = new Alading.Entity.User();
        }

        void backWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                labelControl1.Text = e.UserState as string;
            }));
        }

        void backWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        void backWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                #region 服务器验证失败
                if (!ServiceHelper.CheckUser(Account, Password, out firstrun, out approve))
                {
                    throw new Exception("服务器认证失败，请确认账号和密码正确！");
                } 
                #endregion

                #region 账号未通过官方验证
                if (!approve)
                {
                    throw new Exception("此帐号还未通过审批，暂时无法使用！请联系我们！");
                } 
                #endregion

                IsMain = !Account.Contains(":");

                var users = Alading.Business.UserService.GetAllUser();

                if (users.Count == 0)
                {
                    if (IsMain)
                    {
                        // 主号初次运行
                        backWorker.ReportProgress(1, "正在初始化用户信息");

                        bool r1 = InitMainUser(Account, Password);
                        if (r1 == false) throw new Exception("初始化用户信息失败！");

                        LocalShopList = Alading.Business.ShopService.GetAllShop();
                        LocalUserList = Alading.Business.UserService.GetUser(p => p.account.Contains(Account + ":"));

                        backWorker.ReportProgress(1, "正在下载店铺信息");
                        NetShopList = ServiceHelper.GetShop(Account, Password);

                        bool r2 = SycShop(Account, Password);
                        if (r2 == false) backWorker.ReportProgress(0, "下载店铺信息失败");

                        backWorker.ReportProgress(1, "正在下载员工信息");
                        NetUserList = ServiceHelper.GetUser(Account, Password);

                        bool r3 = SycUser(Account, Password);
                        if (r3 == false) backWorker.ReportProgress(0, "下载员工信息失败");

                        // 设置 first run
                        firstrun = true;
                        IsLogin = true;
                        approve = true;
                        return;
                    }
                    else
                    {
                        // 本地无员工，无法登陆
                        throw new Exception("系统未初始化，暂时无法登陆！");
                    }
                }

                bool isLocal = users.FirstOrDefault(c => c.account == Account) != null;

                if (!isLocal)
                {
                    throw new Exception("此账号不属于本系统，无法登陆！");
                }

                if (IsMain)
                {
                    // 主号登陆
                    backWorker.ReportProgress(1, "正在初始化用户信息");

                    //bool r1 = InitMainUser(Account, Password);
                    //if (r1 == false) throw new Exception("初始化用户信息失败！");

                    LocalShopList = Alading.Business.ShopService.GetAllShop();
                    LocalUserList = Alading.Business.UserService.GetUser(p => p.account.Contains(Account + ":"));

                    backWorker.ReportProgress(1, "正在下载店铺信息");
                    NetShopList = ServiceHelper.GetShop(Account, Password);

                    bool r2 = SycShop(Account, Password);
                    if (r2 == false) backWorker.ReportProgress(0, "下载店铺信息失败");

                    backWorker.ReportProgress(1, "正在下载员工信息");
                    NetUserList = ServiceHelper.GetUser(Account, Password);

                    bool r3 = SycUser(Account, Password);
                    if (r3 == false) backWorker.ReportProgress(0, "下载员工信息失败");
                }
                else
                {
                    // 子号登陆
                }

                firstrun = false;
                IsLogin = true;
                approve = true;
            }
            catch (Exception ex1)
            {
                IsLogin = false;
                approve = false;
                firstrun = false;
                backWorker.ReportProgress(0, ex1.Message);
                XtraMessageBox.Show(ex1.Message, Alading.Taobao.Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            #region 用户检测 已过时
            //try
            //{               
            //    if (ServiceHelper.CheckUser(Account, Password, out firstrun, out approve))
            //    {
            //        if (!approve)
            //        {
            //            XtraMessageBox.Show("此帐号还未通过审批，暂时无法使用！请联系我们！");
            //            IsLogin = false;
            //            return;
            //        }

            //        #region 主号初始化
            //        if (!Account.Contains(":"))  //条件成立为主号
            //        {
            //            IsMain = true;                        
            //            List<User> cu = Alading.Business.UserService.GetAllUser();

            //            if (firstrun && cu.Count == 0) //把主号，员工，店铺全部写到本地数据库中！
            //            {
            //                backWorker.ReportProgress(1, "正在初始化用户信息");
            //                MainAccountFirstRun(Account, Password);
            //                IsLogin = true;
            //                ServiceHelper.UpdateFirstRun(Account, Password);
            //                return;
            //            }
            //            else if (firstrun && Account != "sa")
            //            {
            //                IsLogin = false;
            //                XtraMessageBox.Show("本机已注册主号！", Alading.Taobao.Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                return;
            //            }
            //            else
            //            {
            //                mainUser = Alading.Business.UserService.GetUser(p => p.account == Account).FirstOrDefault();
            //                if (mainUser == null)
            //                {
            //                    IsLogin = false;
            //                    XtraMessageBox.Show("本机已注册主号！", Alading.Taobao.Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    return;
            //                }
            //                LocalShopList = Alading.Business.ShopService.GetAllShop();
            //                LocalUserList = Alading.Business.UserService.GetUser(p => p.account.Contains(Account + ":"));
            //                backWorker.ReportProgress(1, "正在下载店铺信息");
            //                NetShopList = ServiceHelper.GetShop(Account, Password);
            //                backWorker.ReportProgress(1, "正在下载员工信息");
            //                NetUserList = ServiceHelper.GetUser(Account, Password);

            //                if (LocalShopList.Count == 0)  //本地无店铺信息
            //                {
            //                    backWorker.ReportProgress(1, "正在把店铺写到本地数据库");
            //                    AddShop(Account, Password);
            //                }
            //                else
            //                {
            //                    backWorker.ReportProgress(1, "正在验证帐号与店铺签名");
            //                    if (CheckShopSign(Account, Password))  //验证店铺与帐号签名
            //                    {
            //                        backWorker.ReportProgress(1, "正在同步店铺信息");
            //                        SycShop(Account, Password);   //同步店铺信息
            //                    }
            //                    else
            //                    {
            //                        XtraMessageBox.Show("此帐号不能在本机登录！", Alading.Taobao.Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                        IsLogin = false;
            //                        return;
            //                    }
            //                }

            //                CheckLocalUser(Account, Password);  //同步员工信息                            
            //            }
            //        } 
            //        #endregion
            //        else  //子号
            //        {
            //            List<User> tu = Alading.Business.UserService.GetAllUser();
            //            if (tu.Count == 0)  //当主号没有初始化系统时，子号不能登录
            //            {
            //                XtraMessageBox.Show("主号还没有初始化系统！");
            //                IsLogin = false;
            //                return;
            //            }
            //            else
            //            {
            //                backWorker.ReportProgress(1, "正在验证员工帐号");
            //                //User mainUser = Alading.Business.UserService.GetUser(p=>p.account == Account.Substring(0,Account.IndexOf(":"))).FirstOrDefault();
            //                User u = Alading.Business.UserService.GetUser(p => p.account == Account && p.password == HashProvider.GetHash(Password)).FirstOrDefault();
            //                if (u != null)
            //                {
            //                    backWorker.ReportProgress(1, "正在登录员工帐号");
            //                    IsLogin = true;
            //                    return;
            //                }
            //                else  //子号不属于系统主号不能登录
            //                {
            //                    XtraMessageBox.Show("此帐号不属于本主号！","系统提示");
            //                    IsLogin = false;
            //                    return;
            //                }
            //            }                        
            //        }
            //    }
            //    else
            //    {
            //        //密码错误
            //        IsLogin = false;
            //        XtraMessageBox.Show("登陆密码错误！", "系统提示");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //网络中断
            //    IsLogin = false;
            //    //XtraMessageBox.Show(ex.Message, Alading.Taobao.Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    backWorker.ReportProgress(1, ex.Message);
            //}
            #endregion
        }

        void waitTimer_Tick(object sender, EventArgs e)
        {
            this.IsLogin = false;
            this.Close();
        }

        private void LoginWait_Load(object sender, EventArgs e)
        {
            waitTimer.Start();
            backWorker.RunWorkerAsync();
        }

        #region 过时
        //private void MainAccountFirstRun(string account, string pwd)
        //{
        //    try
        //    {
        //        User mainUser = ServiceHelper.GetMainUser(account, pwd);
        //        if (mainUser != null)
        //        {
        //            Alading.Business.UserService.AddUser(mainUser);
        //        }

        //        AddUser(account, pwd);

        //        AddShop(account, pwd);

        //        //IsLogin = true;
        //    }
        //    catch (Exception)
        //    {
        //        IsLogin = false;
        //    }
        //} 
        //private void AddUser(string account, string pwd)  //添加员工
        //{
        //    LocalUserList = ServiceHelper.GetUser(account, pwd);
        //    if (LocalUserList.Count > 0)
        //    {
        //        Alading.Business.UserService.AddUser(LocalUserList);
        //    }
        //}

        //private void AddShop(string account, string pwd)
        //{
        //    LocalShopList = ServiceHelper.GetShop(account, pwd);
        //    if (LocalShopList.Count > 0)
        //    {
        //        Alading.Business.ShopService.AddShop(LocalShopList);
        //    }
        //}

        //private bool CheckShopSign(string account, string pwd)
        //{
        //    int count = 0;
        //    foreach (Shop s in LocalShopList)
        //    {
        //        if (ServiceHelper.CheckShopSign(account, pwd, mainUser.UserCode, s.nick))
        //        {
        //            //只要有一个
        //            count++;
        //        }
        //        if (count > 0)
        //            return true;
        //    }

        //    return false;
        //}

        //private void SycShop(string account, string pwd)
        //{
        //    foreach (Shop s in NetShopList)
        //    {
        //        if (LocalShopList.Find(p => p.nick == s.nick) == null)
        //        {
        //            Alading.Business.ShopService.AddShop(s);
        //        }
        //    }

        //    foreach (Shop s in LocalShopList)
        //    {
        //        if (NetShopList.Find(p => p.nick == s.nick) == null)
        //        {
        //            Alading.Business.ShopService.RemoveShop(p => p.nick == s.nick);
        //        }
        //    }
        //}

        //private void SycUser(string account, string pwd)
        //{
        //    foreach (User u in NetUserList)   //网上有，本地无，添加
        //    {
        //        if (LocalUserList.Find(p => p.UserCode == u.UserCode) == null)
        //        {
        //            Alading.Business.UserService.AddUser(u);
        //        }
        //    }

        //    foreach (User u in LocalUserList)   //本地有，网上无，删除！
        //    {
        //        if (NetUserList.Find(p => p.UserCode == u.UserCode) == null)
        //        {
        //            Alading.Business.UserService.RemoveUser(u.UserCode);
        //        }
        //    }
        //}

        //private void CheckLocalUser(string account, string pwd)
        //{
        //    if (LocalUserList.Count == 0)  //本地没有员工
        //    {
        //        backWorker.ReportProgress(1, "正在把员工信息写到本地数据库");
        //        AddUser(account, pwd);
        //    }
        //    else  //同步员工信息
        //    {
        //        backWorker.ReportProgress(1, "正在同步员工信息");
        //        SycUser(account, pwd);
        //    }
        //    IsLogin = true;
        //}
        #endregion

        private bool InitMainUser(string account, string password)
        {
            try
            {
                User user = ServiceHelper.GetMainUser(account, password);
                if (user == null) return false;              
                ReturnType result = Alading.Business.UserService.AddUser(user);

                Alading.Business.UserRoleService.AddUserRole(
                    new UserRole
                    {
                        UserCode = user.UserCode,
                        UserRoleCode = Guid.NewGuid().ToString(),
                        RoleCode = "1",
                        RoleType = 1,
                    });

                if (result != ReturnType.Success) return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool SycShop(string account, string pwd)
        {
            try
            {
                foreach (Shop s in NetShopList)
                {
                    if (LocalShopList.Find(p => p.nick == s.nick) == null)
                    {
                        Alading.Business.ShopService.AddShop(s);
                    }
                }

                foreach (Shop s in LocalShopList)
                {
                    if (NetShopList.Find(p => p.nick == s.nick) == null)
                    {
                        Alading.Business.ShopService.RemoveShop(p => p.nick == s.nick);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool SycUser(string account, string pwd)
        {
            try
            {
                foreach (User u in NetUserList)   //网上有，本地无，添加
                {
                    if (LocalUserList.Find(p => p.UserCode == u.UserCode) == null)
                    {
                        Alading.Business.UserService.AddUser(u);
                    }
                }

                foreach (User u in LocalUserList)   //本地有，网上无，删除！
                {
                    if (NetUserList.Find(p => p.UserCode == u.UserCode) == null)
                    {
                        Alading.Business.UserService.RemoveUser(u.UserCode);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}