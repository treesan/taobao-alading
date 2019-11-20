using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Alading.Web.Entity;
using Alading.Web.Bussiness;

namespace Alading.Web.Service
{
    /// <summary>
    /// WebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {

            return "Hello World";
            //return x;
        }

        /// <summary>
        /// 员工帐号论证
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [WebMethod]
        public bool CheckUser(string account, string pwd,out bool firstRun,out bool approve)
        {
            firstRun = false ;
            approve = false;

            Alading.Web.Entity.User user = Alading.Web.Bussiness.UserService.GetUserByAccount(account);
            if (user != null && user.Password == pwd)
            {
                if (account == "sa")
                {
                    firstRun = true;
                    approve = true;
                }
                else if (user.FirstRun && user.Approve)
                {
                    firstRun = true;
                    //user.FirstRun = false;
                    approve = user.Approve;
                    //Alading.Web.Bussiness.UserService.UpdateUser(user);
                }
                else
                {
                    firstRun = user.FirstRun;
                    approve = user.Approve;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取主号员工
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [WebMethod]
        public User GetUser(string account,string pwd)
        {
            //return null;
            return Alading.Web.Bussiness.UserService.GetUser(p => p.Account == account && p.Password == pwd).FirstOrDefault();
        }

        /// <summary>
        /// 获取子号员工
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [WebMethod]
        public List<User> GetUsers(string account,string pwd)
        {
            if (Alading.Web.Bussiness.UserService.ChecMainkUser(account, pwd))
            {
                return Alading.Web.Bussiness.UserService.GetUsers(account);
            }
            return null;
        }

        /// <summary>
        /// 获取店铺列表
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        [WebMethod]
        public List<Shop> GetShop(string account,string psw)
        {
            if (!account.Contains(":"))
            {
                Alading.Web.Entity.User u = Alading.Web.Bussiness.UserService.GetUser(account, psw);
                if (u != null)
                {
                    return Alading.Web.Bussiness.ShopService.GetShopList(u.UserCode);
                }
            }
            return null;
        }

        [WebMethod]
        public void UpdateFirstRun(string account, string pwd)
        {
            Alading.Web.Bussiness.UserService.UpdateFirstRun(account, pwd);
        }

        /// <summary>
        /// 获取当前最新版本
        /// </summary>
        /// <param name="versionType"></param>
        /// <returns></returns>
        [WebMethod]
        public Alading.Web.Entity.Version GetNewVersion(string versionType)
        {
            return Alading.Web.Bussiness.UpdateService.GetNewVersion(versionType);
        }

        /// <summary>
        /// 获取当前更新文件列表
        /// </summary>
        /// <param name="versionCode"></param>
        /// <returns></returns>
        [WebMethod]
        public List<Alading.Web.Entity.FileUpdate> GetFileUpdateList(string versionCode)
        {
            return Alading.Web.Bussiness.UpdateService.GetFileUpdateList(versionCode);
        }
    }
}
