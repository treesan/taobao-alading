using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Alading.WebService
{
    public class ServiceHelper
    {
        public static Alading.Entity.User GetMainUser(string account, string pwd)  //获取主号
        {
            AladingWebService.WebService wsc = new Alading.WebService.AladingWebService.WebService();
            Alading.Entity.User user = new Alading.Entity.User();
            
            try
            {
                var mainUser = wsc.GetUser(account, HashProvider.GetHash(pwd));                
                user.UserCode = mainUser.UserCode;
                user.tel = mainUser.Tel;
                user.account = mainUser.Account;
                user.addr = mainUser.Address;
                user.nick = mainUser.UserName;
                user.password = mainUser.Password;
                
            }
            catch(Exception e)
            {
                throw e;
            }            
            return user;
        }

        public static List<Alading.Entity.User> GetUser(string account, string pwd)  //获取员工
        {
            AladingWebService.WebService wsc = new Alading.WebService.AladingWebService.WebService();
            List<Alading.Entity.User> result = new List<Alading.Entity.User>();
            try
            {
                var x = wsc.GetUsers(account, HashProvider.GetHash(pwd));
                if (x != null)
                {
                    foreach (var y in x)
                    {
                        if (y.Account.Contains(":"))
                        {
                            Alading.Entity.User user = new Alading.Entity.User();
                            user.UserCode = y.UserCode;
                            user.tel = y.Tel;
                            user.account = y.Account;
                            user.addr = y.Address;
                            user.nick = y.UserName;
                            user.password = y.Password;

                            result.Add(user);
                        }
                    }
                }
            }
            catch (Exception e )
            {
                
                throw e;
            }
            return result;
        }

        public static List<Alading.Entity.Shop> GetShop(string account, string pwd)
        {
            AladingWebService.WebService wsc = new Alading.WebService.AladingWebService.WebService();
            List<Alading.Entity.Shop> result = new List<Alading.Entity.Shop>();
            try
            {
                var shopList = wsc.GetShop(account, HashProvider.GetHash(pwd));
                if (shopList != null)
                {
                    foreach (var x in shopList)
                    {
                        Alading.Entity.Shop shop = new Alading.Entity.Shop();

                        shop.sid = string.Empty;
                        shop.nick = x.ShopNick;
                        shop.created = DateTime.Now;
                        shop.modified = DateTime.Now;
                        shop.title = x.ShopNick;
                        //加上shop.shopType,shop.shopTypeName
                        result.Add(shop);
                    }
                }
            }
            catch (Exception e)
            {                
                throw e;
            }
            return result;
        }

        public static bool CheckUser(string account, string pwd, out bool firstRun,out bool approve)
        {
            AladingWebService.WebService wsc = new Alading.WebService.AladingWebService.WebService();
            try
            {
                return wsc.CheckUser(account, HashProvider.GetHash(pwd), out firstRun, out approve);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckShopSign(string account,string pwd,string userCode, string shopNick)
        {
            AladingWebService.WebService wsc = new Alading.WebService.AladingWebService.WebService();
            try
            {
                var shopList = wsc.GetShop(account, HashProvider.GetHash(pwd));
                string localShopSign = HashProvider.GetHash(userCode, shopNick);
                string netShopSign = string.Empty;

                foreach (var x in shopList)
                {
                    if (x.ShopNick == shopNick)
                    {
                        netShopSign = x.Sign;
                    }
                }

                if (localShopSign == netShopSign)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }

        public static void UpdateFirstRun(string account, string pwd)
        {
            try
            {
                AladingWebService.WebService wsc = new Alading.WebService.AladingWebService.WebService();
                wsc.UpdateFirstRun(account, HashProvider.GetHash(pwd));
            }
            catch (Exception e)
            {                
                throw e;
            }
        }

        public static Version GetNewVersion(string versionType)
        {
            try
            {
                AladingWebService.WebService wsc = new Alading.WebService.AladingWebService.WebService();
                var x =  wsc.GetNewVersion(versionType);
                Version v = new Version();
                if (x != null)
                {
                    v.Expand1 = x.Expand1;
                    v.Expand2 = x.Expand2;
                    v.PulishTime = x.PublishTime;
                    v.Remark = x.Remark;
                    v.VersionCode = x.VersionCode;
                    v.VersionID = x.VersionID;
                    v.VersionType = x.VersionType;
                }
                return v;
            }
            catch (Exception e)
            {
                throw e;
            } 
        }

        public static List<FileUpdate> GetFileUpdateList(string versionCode)
        {
            try
            {
                AladingWebService.WebService wsc = new Alading.WebService.AladingWebService.WebService();
                var x = wsc.GetFileUpdateList(versionCode);
                List<FileUpdate> list = new List<FileUpdate>();
                for (int i = 0; i < x.Count(); i++)
                {
                    FileUpdate f = new FileUpdate();
                    var v = x[i];
                    f.Expand1 = v.Expand1;
                    f.Expand2 = v.Expand2;
                    f.FileName = v.FileName;
                    f.FileNameAlias = v.FileNameAlias;
                    f.FilePath = v.FilePath;
                    f.Length = v.Length;
                    f.RunAfterUpdate = v.RunAfterUpdate;
                    f.SrcFilePath = v.SrcFilePath;
                    f.UpdateFileID = v.UpdateFileID;
                    f.VersionCode = v.VersionCode;
                    list.Add(f);
                }
                return list;
            }
            catch (Exception e)
            {
                throw e;
            } 
        }
    }

    public class Version
    {
        public Int64 VersionID
        {
            get;
            set;
        }

        public string VersionCode
        {
            get;
            set;
        }

        public string VersionType
        {
            get;
            set;
        }

        public string Remark
        {
            get;
            set;
        }

        public DateTime PulishTime
        {
            get;
            set;
        }

        public string Expand1
        {
            get;
            set;
        }

        public string Expand2
        {
            get;
            set;
        }
    }

    public class FileUpdate
    {
        public Int64 UpdateFileID
        {
            get;
            set;
        }

        public string VersionCode
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public string FileNameAlias
        {
            get;
            set;
        }

        public string FilePath
        {
            get;
            set;
        }

        public string SrcFilePath
        {
            get;
            set;
        }

        public string Length
        {
            get;
            set;
        }

        public bool RunAfterUpdate
        {
            get;
            set;
        }

        public string Expand1
        {
            get;
            set;
        }

        public string Expand2
        {
            get;
            set;
        }
    }
}
