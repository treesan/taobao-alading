using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Alading.Business;
using DevExpress.XtraEditors;
using Alading.Taobao;
using System.IO;
using System.Security.Cryptography;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Reflection;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API;
using Alading.Core.Enum;
using System.Web;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Net;
using System.Drawing;
using Alading.Properties;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.Configuration;
using Alading.Core.Helper;

namespace Alading.Utils
{
    /// <summary>
    /// 当前登录用户存储相关信息。
    /// </summary>
    public static class SystemHelper
    {
        /// <summary>
        /// 自动上架线程时间间隔
        /// </summary>
        public static double THREAD_LIST_INTERVAL = 600000;

        /// <summary>
        /// 自动推荐线程时间间隔
        /// </summary>
        public static double THREAD_RECOMMEND_INTERVAL = 600000;


        static ICacheManager cache = CacheFactory.GetCacheManager();

        /// <summary>
        /// 用户昵称
        /// </summary>
        public static string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 当前登录用户对象
        /// </summary>
        public static User User
        {
            get
            {
                if (cache["User"] != null)
                {
                    return cache["User"] as User;
                }
                else
                {
                    User user = UserService.GetUser(u => u.nick == Name).FirstOrDefault();

                    //如果获得的对象为空，则说明连接数据库失败了。则需要退出系统。
                    if (user == null)
                    {
                        user = new User();
                        user.nick = "默认用户";
                        //XtraMessageBox.Show("获取用户信息失败，系统必须强制关闭。");
                    }
                    else
                    {
                        cache.Add("User", user, CacheItemPriority.Normal, null, new AbsoluteTime(DateTime.Now.AddMinutes(20)));
                    }

                    return user;
                }
            }
            set
            {
                //20分钟后失效
                cache.Add("User", value, CacheItemPriority.Normal, null, new AbsoluteTime(DateTime.Now.AddMinutes(20)));
            }
        }

        /// <summary>
        /// 当前登录用户的角色
        /// </summary>
        public static List<UserRole> UserRoleList
        {
            get
            {
                if (cache["UserRoleList"] != null)
                {
                    return cache["UserRoleList"] as List<UserRole>;
                }
                else
                {
                    List<UserRole> roles = UserRoleService.GetUserRole(u => u.UserCode == Name).ToList();

                    //如果获得的对象为空，则说明连接数据库失败了。则需要退出系统。
                    if (roles == null)
                    {
                        XtraMessageBox.Show("获取用户角色信息失败，系统必须强制关闭。");
                    }
                    else
                    {
                        cache.Add("UserRoleList", roles, CacheItemPriority.Normal, null, new AbsoluteTime(DateTime.Now.AddMinutes(20)));
                    }

                    return roles;
                }
            }
            set
            {
                //20分钟后失效
                cache.Add("UserRoleList", value, CacheItemPriority.Normal, null, new AbsoluteTime(DateTime.Now.AddMinutes(20)));
            }
        }


        /// <summary>
        /// 用户管理的店铺
        /// </summary>
        public static List<Shop> ShopList
        {
            get
            {
                if (cache["ShopList"] != null)
                {
                    return cache["ShopList"] as List<Shop>;
                }
                else
                {
                    //List<Shop> shops = new List<Shop>(); 
                    //ShopService.GetShop(UserRoleList.Select(r => r.Sid).ToList()); 代码有错
                    List<Shop> shops  = ShopService.GetAllShop();
                    //如果获得的对象为空，则说明连接数据库失败了。则需要退出系统。
                    if (shops == null)
                    {
                        XtraMessageBox.Show("获取用户管理店铺信息失败，系统必须强制关闭。");
                    }
                    else
                    {
                        cache.Add("ShopList", shops, CacheItemPriority.Normal, null, new AbsoluteTime(DateTime.Now.AddMinutes(20)));
                    }

                    return shops;
                }
            }
            set
            {
                //20分钟后失效
                cache.Add("ShopList", value, CacheItemPriority.Normal, null, new AbsoluteTime(DateTime.Now.AddMinutes(20)));
            }
        }

        /// <summary>
        /// 获取店铺nick的sessionkey,使用条件是必须保证Shop中存有此nick的的password
        /// </summary>
        public static string GetSessionKey(string nick)
        {
            try
            {
                string session = string.Empty;
                TimeSpan timepan = new TimeSpan();
                Alading.Entity.Shop shop = ShopList.FirstOrDefault(s => s.nick == nick);
                if (shop != null)
                {
                    session = shop.SessionKey;
                    DateTime sessiontime = shop.SessionTime ?? DateTime.Now.AddMinutes(-30);
                    timepan = DateTime.Now - sessiontime;
                    if (string.IsNullOrEmpty(session) || timepan.TotalMinutes >= 30)
                    {
                        if (string.IsNullOrEmpty(shop.password))
                        {
                            throw new Exception(Constants.LACK_PSW);
                        }
                        string password = SecurityHelper.TripleDESDecrypt(shop.password);
                        session = SessionHelper.GetBSKey(shop.nick, password, Constants.APP_KEY);
                        shop.SessionKey = session;
                        shop.SessionTime = DateTime.Now;
                        ShopService.UpdateSessionkey(nick, session, DateTime.Now);
                    }
                }
                else
                {
                    throw new Exception(nick + Constants.NOT_EXISTED_SHOP);
                }
                return session;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 加密/解密相关
        /// <summary>
        /// 初始化系统加密Key
        /// </summary>
        public static void InitKey()
        {
            string keyPath = Application.StartupPath + "\\3DES.key";
            if (!File.Exists(keyPath))
            {
                Stream fs = null;
                //分发密钥文件名称:3DESKey.txt
                //using (Stream fileOut = File.OpenRead(Application.StartupPath + "\\3DESKey.txt"))
                using (Stream fileOut = new MemoryStream(Resources.TripleDESKey))
                {
                    ProtectedKey protectedKey = KeyManager.RestoreKey(fileOut, "60AB62BE4BD58039AB83F2D2B0F42C6F4AD95E36A4B17AEB", DataProtectionScope.LocalMachine);
                    try
                    {
                        fs = new FileStream(keyPath, FileMode.Create);
                        KeyManager.Write(fs, protectedKey.EncryptedKey, DataProtectionScope.LocalMachine);
                        fs.Flush();
                    }
                    finally
                    {
                        if (fs != null)
                            fs.Close();
                    }
                }
            }
        }        

        /// <summary>
        /// 产生新的DES密钥对
        /// </summary>
        /// <param name="keyFilePath"></param>
        public static void CreateNewKey(string keyFilePath)
        {
            byte[] key = KeyManager.GenerateSymmetricKey(typeof(DESCryptoServiceProvider));
            byte[] encryptedKey = ProtectedData.Protect(key, null, DataProtectionScope.LocalMachine);
            Stream fs = null;
            try
            {
                fs = new FileStream(keyFilePath, FileMode.Create);
                KeyManager.Write(fs, encryptedKey, DataProtectionScope.LocalMachine);
                fs.Flush();
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        public static void ModifProtectedKeyFilename(string keyFilePath)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.HasFile)
            {
                ConfigurationSection configurationSection = config.Sections["securityCryptographyConfiguration"];
                CryptographySettings cryptographySettings = configurationSection as CryptographySettings;
                NameTypeConfigurationElementCollection<SymmetricProviderData, CustomSymmetricCryptoProviderData> elementCollection = cryptographySettings.SymmetricCryptoProviders;
                SymmetricProviderData symmetricProviderData = elementCollection.Get("DESCryptoServiceProvider");
                symmetricProviderData.ElementInformation.Properties["protectedKeyFilename"].Value = keyFilePath;
                config.Save(ConfigurationSaveMode.Minimal);
            }
        }
        #endregion

        #region  转化数据AutoCopyData
        public static void AutoCopyData(object destObj, object srcObj)
        {
            PropertyInfo[] destProperties = destObj.GetType().GetProperties();
            PropertyInfo[] srcProperties = srcObj.GetType().GetProperties();

            foreach (PropertyInfo pi in srcProperties)
            {
                if (pi.ToString().Contains("ID") || pi.ToString().Contains("System.Data"))
                    continue;
                else
                {
                    string properyName = pi.Name;
                    IEnumerable<PropertyInfo> properyList = destProperties.Where(p => p.Name == properyName);
                    if (properyList.Count() == 1)
                    {
                        object value = srcObj.GetType().GetProperty(properyName).GetValue(srcObj, null);
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                            destObj.GetType().GetProperty(properyName).SetValue(destObj, value, null);
                    }
                }
            }
        }
        #endregion

        #region Stream和文件操作
        /// <summary>
        /// 获取图片的二进制文件
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] GetImageBytes(Image image)
        {
            try
            {
                if (image == null) return null;
                using (Bitmap bitmap = new Bitmap(image))
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        return stream.GetBuffer();
                    }
                }
            }
           catch(Exception ex)
           {
               throw ex;
           }
        }

        /// <summary>
        /// 将 Stream 转成 byte[] 
        /// </summary>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, bytes.Length);            
            return bytes;
        }

        /// <summary> 
        /// 将 byte[] 转成 Stream 
        /// </summary> 
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary> 
        /// 将 Stream 写入文件 
        /// </summary> 
        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[] 
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件 
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary> 
        /// 从文件读取 Stream 
        /// </summary> 
        public static Stream FileToStream(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }
            // 打开文件 
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[] 
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream 
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// 将文件转成Image对象
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Image FileToImage(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }

            try
            {
                // 打开文件 
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                // 读取文件的 byte[] 
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                fileStream.Close();
                // 把 byte[] 转换成 Stream 
                Stream stream = new MemoryStream(bytes);
                Image image = Image.FromStream(stream);
                stream.Close();

                return image;     
            }
            catch (System.Exception ex)
            {
                File.Delete(fileName);
                return null;
            }           
        }

        /// <summary>
        /// 将文件转成Byte[]对象
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static byte[] FileToBytes(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }
            // 打开文件 
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[] 
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();

            return bytes;
        }
        #endregion

        #region 比较时间戳是否相等
        /// <summary>
        /// 比较两个TimeStamp是否相等，byte[]比较
        /// </summary>
        /// <param name="oldTP">待比较TimeStamp</param>
        /// <param name="newTP">待比较TimeStamp</param>
        /// <returns></returns>
        public static bool CompareTimeStamp(byte[] oldTP,byte[] newTP)
        {
           return BitConverter.ToString(oldTP).Equals(BitConverter.ToString(newTP));//转换形式为： 00-00-00-00-12-25-36-98.可以使用
        }
        #endregion

        #region  创建一条操作信息函数
        /// <summary>
        /// 创建一条订单流水操作信息到数据库,操作时间默认为当前时间
        /// </summary>
        /// <param name="trade">交易实体</param>
        /// <param name="title">操作主题</param>
        /// <param name="contents">操作内容</param>
        /// <param name="department">操作部分</param>
        public static void CreateFlowMessage(string customTid, string title, string content, string department)
        {
            Alading.Entity.TradeInfo tradeInfo = new Alading.Entity.TradeInfo();
            tradeInfo.TradeInfoCode = Guid.NewGuid().ToString();
            tradeInfo.CustomTid = customTid;
            tradeInfo.Title = title;
            tradeInfo.AppendUserName =User.nick;
            //tradeInfo.AppendUserName = oprator;
            tradeInfo.AppendTime= DateTime.Now;
            tradeInfo.AppendUserCode = string.Empty;
            tradeInfo.AppendDepartment = department;
            tradeInfo.Content = content;

            TradeInfoService.AddTradeInfo(tradeInfo);
        }

        #endregion      
        
        #region  根据CustomTid来取得子订单 包含合并单
        /*
        public  static List<View_OrderItem> GetChildOrders(string customTid)
        {
            Trade trade = TradeService.GetTrade(customTid);
            List<View_OrderItem> orders = null;
            if(trade.IsCombined==false)
            {
               // orders = View_OrderItemService.GetView_OrderItem(p=>p.CustomTid==customTid);
            }
            else
            {
                foreach (Trade _trade in TradeService.GetTrade(p => p.CombineCode == trade.CustomTid))
                 {
                    foreach(TradeOrder order in  TradeOrderService.GetTradeOrder(p=>p.CustomTid==_trade.CustomTid))
                    {
                        orders.Add(order);
                    }
                 }
            }

            return orders;
        }
*/
        #endregion

        #region 英寸到厘米的转换
        /// <summary> 
        /// 英寸到厘米的转换 
        /// /* = = = = = = = = = = = = = = = = * 
        /// | 换算一下计量单位，将其换算成厘米  |
        /// |    厘米     像素     英寸         |
        /// |     1        38     0.395         |
        /// |   0.026       1      0.01         |
        /// |    2.54      96        1          |
        /// * = = = = = = = = = = = = = = = = */
        /// </summary>
        /// <param name="inch">英寸数</param> 
        /// <returns>厘米数，两位小数</returns> 
        /// 
        public static decimal FromInchToCM(decimal inch)
        {
            return Math.Round((System.Convert.ToDecimal((inch / 100)) * System.Convert.ToDecimal(2.5400)), 2);
        }
        #endregion 英寸到厘米的转换
    }
}
