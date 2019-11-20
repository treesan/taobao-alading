using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Alading.Taobao.Entity;
using Alading.Taobao.API.Common;
using Alading.Taobao.Entity.Extend;

namespace Alading.Taobao.API
{
    public partial class TopService
    {
        /// <summary>
        /// taobao.user.get 获取单个用户信息
        /// </summary>
        /// <param name="nick"></param>
        /// <param name="alipay_no"></param>
        /// <returns></returns>
        public static UserRsp UserGet(string session, string nick, string alipay_no)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.user.get");
                paramsTable.Add("fields", "user_id,nick,sex,buyer_credit,seller_credit,location,created,last_visit,birthday,type,has_more_pic,item_img_num,item_img_size,prop_img_num,prop_img_size,auto_repost,promoted_type,status,alipay_bind,consumer_protection,alipay_account,alipay_no");
                paramsTable.Add("nick", nick);          
                paramsTable.Add("alipay_no", alipay_no);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<UserRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// 获取多个用户信息
        /// </summary>
        /// <param name="userinput"></param>
        /// <returns></returns>
        public static UserRsp UsersGet(string session, string nicks)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.users.get");
                paramsTable.Add("fields", "user_id,nick,sex,buyer_credit,location.city,location.state,location.zip,location.address,location.country,location.district,created,seller_credit,last_visit");
                paramsTable.Add("nicks", nicks);
                return TopUtils.DeserializeObject<UserRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Public));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
