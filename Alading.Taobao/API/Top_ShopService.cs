using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Alading.Taobao.API.Common;
using Alading.Taobao.Entity;
using Alading.Taobao.Entity.Extend;

namespace Alading.Taobao.API
{
    public partial class TopService
    {
        /// <summary>
        /// 获取卖家店铺的基本信息    
        /// fields 和 nick都是必须的 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="nick"></param>
        /// <returns></returns>
        public static ShopRsp ShopGet(string nick)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.shop.get");
                paramsTable.Add("fields", "sid,cid,title,nick,desc,bulletin,pic_path,created,modified");
                paramsTable.Add("nick", nick);
                return TopUtils.DeserializeObject<ShopRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Public));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 更新店铺基本信息
        /// title,bulletion,desc可以选填最少三选一
        /// </summary>
        /// <param name="title"></param>
        /// <param name="bulletin"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static ShopRsp ShopUpdate(string session, string title, string bullentin, string desc)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.shop.update");
                paramsTable.Add("title", title);
                paramsTable.Add("bulletin", bullentin);
                paramsTable.Add("desc", desc);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ShopRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 更新店铺基本信息
        /// title,bulletion,desc选择一个上传
        /// </summary>
        /// <param name="title"></param>
        /// <param name="bulletin"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static ShopRsp ShopUpdate(string session, string name, string value)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.shop.update");
                paramsTable.Add(name, value);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ShopRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 获取前台展示的店铺类目
        /// fields不是必须的
        /// </summary>
        /// <returns></returns>
        public static ShopRsp ShopCatsListGet()
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.shopcats.list.get");
                paramsTable.Add("fields", "cid,parent_cid,name,is_parent");
                return TopUtils.DeserializeObject<ShopRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Public));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 获取前台展示的店铺内卖家自定义商品类目
        /// nick  必须要填
        /// </summary>
        /// <param name="nick"></param>
        /// <returns></returns>
        public static ShopRsp SellerCatsListGet(string nick)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.sellercats.list.get");
                paramsTable.Add("fields", "cid,parent_cid,name,is_parent");
                paramsTable.Add("nick", nick);
                return TopUtils.DeserializeObject<ShopRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Public));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 添加卖家自定义类目
        /// 除name是必填项之外其余三项不是必须项
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pict_url"></param>
        /// <param name="parent_cid"></param>
        /// <param name="sort_order"></param>
        /// <returns></returns>
        public static ShopRsp SellerCatsListAdd(string session, string name, string picurl, int parentcid, int sortorder)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.sellercats.list.add");
                paramsTable.Add("name", name);
                paramsTable.Add("pict_url", picurl);
                paramsTable.Add("parent_cid", parentcid);
                paramsTable.Add("sort_order", sortorder);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ShopRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 更新卖家自定义类目
        /// 除cid是必填项之外其余三项不是必须的
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="name"></param>
        /// <param name="pict_url"></param>
        /// <param name="sort_order"></param>
        /// <returns></returns>
        public static ShopRsp SellerCatsListUpdate(string session, string name, string picurl, string cid, int sortorder)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.sellercats.list.update");
                paramsTable.Add("name", name);
                paramsTable.Add("pict_url", picurl);
                paramsTable.Add("cid", cid);
                paramsTable.Add("sort_order", sortorder);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ShopRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        public static ShopRsp ShopRemainShowCaseGet(string session)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.shop.remainshowcase.get");
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ShopRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }

}
