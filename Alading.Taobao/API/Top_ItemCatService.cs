using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Taobao.API.Common;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.Entity;


namespace Alading.Taobao.API
{
    public partial class TopService
    {
        /// <summary>
        /// 获取标准类目属性值
        /// cid 必需,pvs格式例如(pid1;pid2)或(pid1:vid1;pid2:vid2)或(pid1;pid2:vid2),
        ///  datetime假如传2005-01-01 00:00:00，则取所有的属性和子属性(状态为删除的属性值不返回prop_name)
        /// </summary>
        public static ItemCatRsp ItemPropValuesGet(string cid,string pvs,string datetime)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.itempropvalues.get");
                paramsTable.Add("fields", "cid,pid,prop_name,vid,name,name_alias,is_parent,status,sort_order");
                paramsTable.Add("cid", cid);
                paramsTable.Add("pvs", pvs);
                paramsTable.Add("datetime", datetime);

                return TopUtils.DeserializeObject<ItemCatRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Public));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }

       /// <summary>
        /// 查询B商家被授权品牌列表和类目列表
       /// </summary>
        public static ItemCatRsp ItemCatsAuthorizeGet(string session)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.itemcats.authorize.get");
                paramsTable.Add("fields", "brand.vid,brand.name,item_cat.cid,item_cat.name,item_cat.status,item_cat.sort_order,item_cat.parent_cid,item_cat.is_parent");
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemCatRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// taobao.itemcats.get(获取后台供卖家发布商品的标准商品类目)
        /// </summary>
        /// <param name="parent_cid"></param>
        /// <param name="cids"></param>
        /// <param name="datetime"></param>
        public static ItemCatRsp ItemCatsGet(string parent_cid, string cids, string datetime)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.itemcats.get");
                paramsTable.Add("fields", "cid,parent_cid,name,is_parent,status,sort_order");
                paramsTable.Add("parent_cid", parent_cid);
                paramsTable.Add("cids", cids);
                paramsTable.Add("datetime", datetime);

                return TopUtils.DeserializeObject<ItemCatRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Public)); 
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }


        /// <summary>
        /// taobao.itemprops.get(获取标准商品类目属性)
        /// cid 必需，不能返回propvalues里的is_parent字段
        /// </summary>
        public static ItemCatRsp ItemPropsGet(ItemCatReq itemcatrep)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.itemprops.get");
                paramsTable.Add("fields", "pid,name,status,sort_order,parent_pid,parent_vid,must,multi,is_key_prop,is_sale_prop,is_color_prop,is_item_prop,prop_values,is_allow_alias,is_enum_prop,is_input_prop,child_template");
                paramsTable.Add("cid", itemcatrep.Cid);
                paramsTable.Add("pid", itemcatrep.pid);
                paramsTable.Add("parent_pid", itemcatrep.ParentCid);
                paramsTable.Add("is_key_prop", itemcatrep.is_key_prop);
                paramsTable.Add("is_sale_prop", itemcatrep.is_sale_prop);
                paramsTable.Add("is_color_prop", itemcatrep.is_color_prop);
                paramsTable.Add("is_enum_prop", itemcatrep.is_enum_prop);
                paramsTable.Add("is_input_prop", itemcatrep.is_input_prop);
                paramsTable.Add("is_item_prop", itemcatrep.is_item_prop);
                paramsTable.Add("datetime", itemcatrep.datetime);

                return TopUtils.DeserializeObject<ItemCatRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Public)); 
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// taobao.itemprops.get(获取标准商品类目cid下所有属性),
        /// cid必填,不能返回propvalues里的is_parent字段,
        /// datetime =“2005-01-01 00:00:00”
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static ItemCatRsp ItemPropsGet(string cid)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.itemprops.get");
                paramsTable.Add("fields", "pid,name,status,sort_order,parent_pid,parent_vid,must,multi,is_key_prop,is_sale_prop,is_color_prop,is_item_prop,prop_values,is_allow_alias,is_enum_prop,is_input_prop,child_template");
                paramsTable.Add("cid", cid);
                paramsTable.Add("datetime", "2005-01-01 00:00:00");
                return TopUtils.DeserializeObject<ItemCatRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Public));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// taobao.itemprops.get(获取标准商品类目属性),
        /// 返回pid的指定信息，包括prop_values,
        /// cid必填, 能返回Propvalues里的is_parent字段,
        /// </summary>
        public static ItemCatRsp ItemPropsGet(string cid, string pid, string parent_pid)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.itemprops.get");
                paramsTable.Add("fields", "prop_values,pid,name,status,sort_order,parent_pid,parent_vid,must,multi,is_key_prop,is_sale_prop,is_color_prop,is_item_prop,is_allow_alias,is_enum_prop,is_input_prop,child_template");
                paramsTable.Add("cid", cid);
                paramsTable.Add("pid", pid);
                paramsTable.Add("parent_pid", parent_pid);
                return TopUtils.DeserializeObject<ItemCatRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Public));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

    
    }
}
