using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;

namespace Alading.Taobao
{
    public class SessionHelper
    {
        static int timeout = 10000;
        /// <summary>
        /// 测试环境获取sessionkey
        /// </summary>
        /// <param name="username"></param>
        /// <param name="appkey"></param>
        /// <returns></returns>
        public static string TestGetSessionKey(string username, string appkey)
        {
            try
            {
                string sessionkey = null;
                string url = "http://open.taobao.com/isv/authorize.php?appkey={0}";
                url = string.Format(url, appkey);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.CookieContainer = new CookieContainer();
                req.ReadWriteTimeout = timeout;
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8);
                String line = sr.ReadToEnd();
                res.Close();

                string TPL_username = username;
                HttpWebRequest req2 = (HttpWebRequest)WebRequest.Create(url);
                string PostData = string.Format("zhxz=1&nick={0}", TPL_username);
                req2.Method = "POST";
                req2.CookieContainer = req.CookieContainer;
                req2.ReadWriteTimeout = timeout;
                req2.ContentType = "application/x-www-form-urlencoded";
                Stream myRequestStream = req2.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.Default);
                myStreamWriter.Write(PostData);
                //把数据写入HttpWebRequest的Request流 
                myStreamWriter.Close();
                myRequestStream.Close();
                HttpWebResponse res2 = (HttpWebResponse)req2.GetResponse();
                StreamReader sr2 = new StreamReader(res2.GetResponseStream(), System.Text.Encoding.UTF8);
                String line2 = sr2.ReadToEnd();
                res2.Close();

                string authcodePos1 = "autoInput";
                string authcodePos2 = "authButton";
                int substringlength = line2.IndexOf(authcodePos2) - line2.IndexOf(authcodePos1);
                if (substringlength < 0)
                {
                    return sessionkey;
                }
                line2 = line2.Substring(line2.IndexOf(authcodePos1), substringlength);
                substringlength = line2.IndexOf("style") - line2.IndexOf("value");
                if (substringlength < 0)
                {
                    return sessionkey;
                }
                line2 = line2.Substring(line2.IndexOf("value"), substringlength);
                string[] line2arry = line2.Split('"');
                if (line2arry.Length < 2)
                {
                    return sessionkey;
                }
                string authcode = line2arry[1];


                HttpWebRequest req5 = (HttpWebRequest)WebRequest.Create(string.Format("http://container.api.tbsandbox.com/container?authcode={0}", authcode));
                HttpWebResponse res5 = (HttpWebResponse)req5.GetResponse();
                StreamReader sr5 = new StreamReader(res5.GetResponseStream(), System.Text.Encoding.UTF8);
                String line5 = sr5.ReadToEnd();
                res5.Close();
                string[] sArray = line5.Split('&');
                foreach (string str in sArray)
                {
                    if (str.ToString().StartsWith("top_session"))
                        sessionkey = str.ToString().Substring(str.ToString().IndexOf("=") + 1);
                }
                return sessionkey;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 测试环境 验证用户，登录成功返回1，账号密码错误返回0
        /// </summary>
        public static int TestValidateUser(string username, string password)
        {
            try
            {
                if (password == "taobao1234")
                {
                    return 1;
                }

                else
                {
                    return 0;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///正式环境 用BS方式获取去sessionkey
        /// </summary>
        public static string GetBSKey(string username, string password, string appkey)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    throw new Exception(Constants.LACK_NICK);
                }
                if (string.IsNullOrEmpty(password))
                {
                    throw new Exception(Constants.LACK_PSW);
                }
                Encoding encoding = Encoding.GetEncoding("gb2312");
                string sessionkey = null;

                #region 实现登录并保存下Cookie
                string url = "http://login.taobao.com/member/login.jhtml?login_type=3&redirect_url=null&yparam=&done=&f=";
                HttpWebRequest req1 = (HttpWebRequest)WebRequest.Create(url);
                req1.CookieContainer = new CookieContainer();
                req1.Timeout = timeout;
                HttpWebResponse res = (HttpWebResponse)req1.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
                String line = sr.ReadToEnd();
                res.Close();

                string TPL_username = username;
                string TPL_password = password;
                HttpWebRequest req2 = (HttpWebRequest)WebRequest.Create(url);
                string PostData = string.Format("TPL_username={0}&TPL_password={1}&actionForStable=enable_post_user_action&action=Authenticator&mi_uid=&mcheck=&TPL_redirect_url=null&_oooo_=null&event_submit_do_login=anything&abtest=&pstrong=2&from=&yparam=&done=&loginType=3&tid=XOR_1_000000000000000000000000000000_635847574D7F7E7379000670&support=000001&CtrlVersion=1%2C0%2C0%2C7", TPL_username, TPL_password);
                byte[] postdata = encoding.GetBytes(PostData);
                req2.ContentLength = postdata.Length;
                req2.Method = "POST";//大写
                req2.Timeout = timeout;
                req2.CookieContainer = req1.CookieContainer;
                req2.ContentType = "application/x-www-form-urlencoded";
                req2.Connection = "http://login.taobao.com/member/login.jhtml";
                req2.KeepAlive = true;

                Stream myRequestStream = req2.GetRequestStream();
                //把数据写入HttpWebRequest的Request流 
                myRequestStream.Write(postdata, 0, postdata.Length);
                myRequestStream.Close();
                HttpWebResponse res2 = (HttpWebResponse)req2.GetResponse();
                StreamReader sr2 = new StreamReader(res2.GetResponseStream(), System.Text.Encoding.Default);
                String line2 = sr2.ReadToEnd();
                res2.Close();
                #endregion

                string checkcodeFlag = "请输入校验码后再试";
                if (line2.Contains(checkcodeFlag))
                {
                    System.Diagnostics.Process.Start("http://login.taobao.com/member/login.jhtml");
                    throw new Exception(Constants.Check_CODE_INFO);
                }
                string flag = "我是卖家";
                if (!line2.Contains(flag))
                {
                    throw new Exception(Constants.USER_PSW_ERROR);
                }
               

                #region 获取同意条款界面的签名agreementsign值
                HttpWebRequest req3 = (HttpWebRequest)WebRequest.Create(string.Format("http://container.open.taobao.com/container?appkey={0}", appkey));
                req3.Method = "GET";//大写
                req3.Timeout = timeout;
                req3.CookieContainer = req2.CookieContainer;
                req3.KeepAlive = true;
                HttpWebResponse res3 = (HttpWebResponse)req3.GetResponse();
                StreamReader sr3 = new StreamReader(res3.GetResponseStream(), System.Text.Encoding.UTF8);
                String line3 = sr3.ReadToEnd();
                res3.Close();
                if (line3.Contains("您无法访问该应用") || line3.Contains("错误码：106"))
                {
                    throw new Exception("您无法访问该应用如果您想使用该应用，欢迎到淘宝应用商店订购");
                }
                if (line3.Contains("top_session"))
                {
                    string[] sArray = line3.Split('&');
                    foreach (string str in sArray)
                    {
                        if (str.ToString().StartsWith("top_session"))
                            sessionkey = str.ToString().Substring(str.ToString().IndexOf("=") + 1);
                    }
                    return sessionkey;
                }
                else
                    if (!line3.Contains("agreementsign"))
                    {
                        return sessionkey;
                    }
                string agreementsign = line3.Substring(line3.IndexOf("agreementsign"), 100);
                if (agreementsign.Contains(appkey))
                {
                    agreementsign = agreementsign.Substring(agreementsign.IndexOf(appkey), 50);
                }
                else
                {
                    agreementsign = string.Empty;
                }
                #endregion

                #region 获取sessionkey并返回其值，失败则返回null
                HttpWebRequest req4 = (HttpWebRequest)WebRequest.Create(string.Format("http://container.open.taobao.com/container?appkey={0}", appkey));
                PostData = string.Format("appkey={0}&agreementsign={1}&agreement=true", appkey, agreementsign);
                postdata = encoding.GetBytes(PostData);
                req4.Method = "POST";//大写
                req4.Timeout = timeout;
                req4.ContentType = "application/x-www-form-urlencoded";//必不可少的
                req4.CookieContainer = req3.CookieContainer;//必不可少的
                req4.KeepAlive = true;
                /*把数据写入HttpWebRequest的Request流 */
                myRequestStream = req4.GetRequestStream();
                myRequestStream.Write(postdata, 0, postdata.Length);
                myRequestStream.Close();
                HttpWebResponse res4 = (HttpWebResponse)req4.GetResponse();
                StreamReader sr4 = new StreamReader(res4.GetResponseStream(), System.Text.Encoding.UTF8);
                String line4 = sr4.ReadToEnd();
                res4.Close();
                string[] strArray = line4.Split('&');
                foreach (string str in strArray)
                {
                    if (str.ToString().StartsWith("top_session"))
                        sessionkey = str.ToString().Substring(str.ToString().IndexOf("=") + 1);
                }

                if (string.IsNullOrEmpty(sessionkey))
                {
                    throw new Exception("您没有订购该服务或用户名密码错误!");
                }
                return sessionkey;
                #endregion
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///正式环境 验证用户，登录成功返回1，账号密码错误返回0，需要输入验证码返回2
        /// </summary>
        public static int ValidateUser(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    throw new Exception(Constants.LACK_NICK);
                }
                if (string.IsNullOrEmpty(password))
                {
                    throw new Exception(Constants.LACK_PSW);
                }
                Encoding encoding = Encoding.GetEncoding("gb2312");

                #region 实现登录并保存下Cookie
                string url = "http://login.taobao.com/member/login.jhtml?login_type=3&redirect_url=null&yparam=&done=&f=";
                HttpWebRequest req1 = (HttpWebRequest)WebRequest.Create(url);
                req1.CookieContainer = new CookieContainer();
                req1.Timeout = timeout;
                HttpWebResponse res = (HttpWebResponse)req1.GetResponse();
                res.Close();
                string TPL_username = username;
                string TPL_password = password;
                HttpWebRequest req2 = (HttpWebRequest)WebRequest.Create(url);
                string PostData = string.Format("TPL_username={0}&TPL_password={1}&actionForStable=enable_post_user_action&action=Authenticator&mi_uid=&mcheck=&TPL_redirect_url=null&_oooo_=null&event_submit_do_login=anything&abtest=&pstrong=2&from=&yparam=&done=&loginType=3&tid=XOR_1_000000000000000000000000000000_635847574D7F7E7379000670&support=000001&CtrlVersion=1,0,0,7", TPL_username, TPL_password);
                byte[] postdata = encoding.GetBytes(PostData);
                //byte[] postdata = HttpUtility.UrlDecodeToBytes(PostData);
                req2.ContentLength = postdata.Length;
                req2.Method = "POST";//大写
                req2.Timeout = timeout;
                req2.CookieContainer = req1.CookieContainer;
                req2.ContentType = "application/x-www-form-urlencoded";
                req2.Connection = "http://login.taobao.com/member/login.jhtml";
                req2.KeepAlive = true;
                Stream myRequestStream = req2.GetRequestStream();
                //把数据写入HttpWebRequest的Request流 
                myRequestStream.Write(postdata, 0, postdata.Length);
                myRequestStream.Close();
                HttpWebResponse res2 = (HttpWebResponse)req2.GetResponse();
                StreamReader sr2 = new StreamReader(res2.GetResponseStream(), System.Text.Encoding.Default);
                String line2 = sr2.ReadToEnd();
                res2.Close();
                string checkcodeFlag = "请输入校验码后再试";
                if (line2.Contains(checkcodeFlag))
                {
                    return 2;
                }
                string flag = "我是卖家";
                if (line2.Contains(flag))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

                #endregion
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
