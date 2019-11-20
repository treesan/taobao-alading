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
        /// 查询appstore应用订购关系
        /// 此接口用于查询在appstore上订购应用中订购关系和使用期限查询 
        /// 如果参数lease_id非空，则判断nick和lease_id的订购关系 
        /// 如果参数lease_id为空，则判断nick和app_key对应应用的lease_id的订购关系 
        /// </summary>
        /// <param name="nick"></param>
        /// <param name="lease_id"></param>
        /// <returns></returns>
        public static UserSubscribeRsp SubscribeGet(string nick, string lease_id)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.appstore.subscribe.get");

                paramsTable.Add("nick", nick);
                paramsTable.Add("lease_id", lease_id);

                return TopUtils.DeserializeObject<UserSubscribeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Public));
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
