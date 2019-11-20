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
        /// 获取当前会话用户的所有商品列表
        /// </summary>
        public static ItemRsp ItemsAllGet(string session,ItemReq itemreq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.items.all.get");
                paramsTable.Add("fields", "approve_status,iid,num_iid,title,nick,type,cid,pic_url,num,props,valid_thru,list_time,price,has_discount,has_invoice,has_warranty,has_showcase,modified,delist_time,postage_id,seller_cids,outer_id");
                paramsTable.Add("q", itemreq.Q);
                paramsTable.Add("cid", itemreq.Cid);
                paramsTable.Add("seller_cids", itemreq.SellerCids);
                paramsTable.Add("page_no", itemreq.PageNo);
                paramsTable.Add("page_size", itemreq.PageSize);
                paramsTable.Add("order_by", itemreq.OrderBy);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;	
            }
           
        }

        /// <summary>
        /// 获取当前会话用户的所有商品列表,
        /// page_size最大值：200；默认值：40
        /// </summary>
        public static ItemRsp ItemsAllGet(string session,string fields, int pageNo, int pageSize)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.items.all.get");
                paramsTable.Add("fields", fields);
                paramsTable.Add("page_no", pageNo);
                paramsTable.Add("page_size", pageSize);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }


        /// <summary>
        /// 获取当前会话用户的所有商品列表,
        /// page_size最大值：200；默认值：40
        /// </summary>
        public static ItemRsp ItemsAllGet(string session, int pageNo, int pageSize)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.items.all.get");
                paramsTable.Add("fields", "approve_status,iid,num_iid,title,nick,type,cid,pic_url,num,props,valid_thru,list_time,price,has_discount,has_invoice,has_warranty,has_showcase,modified,delist_time,postage_id,seller_cids,outer_id");
                paramsTable.Add("page_no", pageNo);
                paramsTable.Add("page_size", pageSize);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 搜索商品信息
        /// </summary>
        public static ItemRsp ItemsGet(ItemReq itemreq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.items.get");
                paramsTable.Add("fields", "iid,title,nick,pic_url,cid,price,type,delist_time,post_fee,score,volume,location.city,location.state");
                paramsTable.Add("q", itemreq.Q);
                paramsTable.Add("nicks", itemreq.Nick);
                paramsTable.Add("cid", itemreq.Cid);
                paramsTable.Add("props", itemreq.Props);
                paramsTable.Add("product_id", itemreq.ProductId);
                paramsTable.Add("start_price", itemreq.StartPrice);
                paramsTable.Add("end_price", itemreq.EndPrice);
                paramsTable.Add("post_free", itemreq.PostFree);
                paramsTable.Add("ww_status", itemreq.WwStatus);
                paramsTable.Add("page_no", itemreq.PageNo);
                paramsTable.Add("page_size", itemreq.PageSize);
                paramsTable.Add("order_by", itemreq.OrderBy);
                paramsTable.Add("location.state", itemreq.Location.State);
                paramsTable.Add("location.city", itemreq.Location.City);
                paramsTable.Add("is_3D", itemreq.Is3D);
                paramsTable.Add("start_score", itemreq.StartScore);
                paramsTable.Add("end_score", itemreq.EndScore);
                paramsTable.Add("start_volume", itemreq.StartVolume);
                paramsTable.Add("end_volume", itemreq.EndVolume);
                paramsTable.Add("one_station", itemreq.OneStation);
                paramsTable.Add("is_cod", itemreq.IsCod);
                paramsTable.Add("is_mall", itemreq.IsMall);
                paramsTable.Add("is_prepay", itemreq.IsPrepay);
                paramsTable.Add("genuine_security", itemreq.GenuineSecurity);
                paramsTable.Add("promoted_service", itemreq.PromotedService);
                paramsTable.Add("stuff_status", itemreq.StuffStatus);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Public));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
          
        }
        /// <summary>
        /// 搜索商品全部信息
        /// </summary>
        public static ItemRsp ItemsSearch(ItemReq itemreq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.items.search");
                paramsTable.Add("fields", "iid,title,nick,pic_url,cid,price,type,delist_time,post_fee,score,volume,location.city,location.state");
                paramsTable.Add("q", itemreq.Q);
                paramsTable.Add("nicks", itemreq.Nick);
                paramsTable.Add("cid", itemreq.Cid);
                paramsTable.Add("props", itemreq.Props);
                paramsTable.Add("product_id", itemreq.ProductId);
                paramsTable.Add("start_price", itemreq.StartPrice);
                paramsTable.Add("end_price", itemreq.EndPrice);
                paramsTable.Add("post_free", itemreq.PostFree);
                paramsTable.Add("ww_status", itemreq.WwStatus);
                paramsTable.Add("page_no", itemreq.PageNo);
                paramsTable.Add("page_size", itemreq.PageSize);
                paramsTable.Add("order_by", itemreq.OrderBy);
                paramsTable.Add("location.state", itemreq.Location.State);
                paramsTable.Add("location.city", itemreq.Location.City);
                paramsTable.Add("is_3D", itemreq.Is3D);
                paramsTable.Add("start_score", itemreq.StartScore);
                paramsTable.Add("end_score", itemreq.EndScore);
                paramsTable.Add("start_volume", itemreq.StartVolume);
                paramsTable.Add("end_volume", itemreq.EndVolume);
                paramsTable.Add("one_station", itemreq.OneStation);
                paramsTable.Add("is_cod", itemreq.IsCod);
                paramsTable.Add("is_mall", itemreq.IsMall);
                paramsTable.Add("is_prepay", itemreq.IsPrepay);
                paramsTable.Add("genuine_security", itemreq.GenuineSecurity);
                paramsTable.Add("promoted_service", itemreq.PromotedService);
                paramsTable.Add("stuff_status", itemreq.StuffStatus);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Public));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// 获取当前会话用户出售中的商品列表
        /// </summary>
        public static ItemRsp ItemsOnsaleGet(string session,ItemReq itemreq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.items.onsale.get");
                paramsTable.Add("fields", "approve_status,iid,num_iid,title,nick,type,cid,pic_url,num,props,valid_thru,list_time,price,has_discount,has_invoice,has_warranty,has_showcase,modified,delist_time,postage_id,seller_cids,outer_id");
                paramsTable.Add("q", itemreq.Q);
                paramsTable.Add("cid", itemreq.Cid);
                paramsTable.Add("page_no", itemreq.PageNo);
                paramsTable.Add("page_size", itemreq.PageSize);
                paramsTable.Add("seller_cids", itemreq.SellerCids);
                paramsTable.Add("has_discount", itemreq.HasDiscount);
                paramsTable.Add("has_showcase", itemreq.HasShowcase);
                paramsTable.Add("order_by", itemreq.OrderBy);
                paramsTable.Add("is_taobao", itemreq.IsTaobao);
                paramsTable.Add("is_ex", itemreq.IsExternal);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// 获取当前会话用户出售中的商品列表
        /// </summary>
        public static ItemRsp ItemsOnsaleGet(string session, int pageNo, int pageSize)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.items.onsale.get");
                paramsTable.Add("fields", "approve_status,iid,num_iid,title,nick,type,cid,pic_url,num,props,valid_thru,list_time,price,has_discount,has_invoice,has_warranty,has_showcase,modified,delist_time,postage_id,seller_cids,outer_id");
                paramsTable.Add("page_no", pageNo);
                paramsTable.Add("page_size", pageSize);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// 获取当前会话用户在自定义类目sellercids的出售中的商品列表，返回字段fields自定
        /// </summary>
        /// <param name="fields"></param>
        public static ItemRsp ItemsOnsaleGet(string session,string fields, string sellercids)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.items.onsale.get");
                paramsTable.Add("fields", fields);
                paramsTable.Add("seller_cids", sellercids);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 得到当前会话用户库存中的商品列表
        /// </summary>
        public static ItemRsp ItemsInventoryGet(string session,ItemReq itemreq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.items.inventory.get");
                paramsTable.Add("fields", "approve_status,iid,num_iid,title,nick,type,cid,pic_url,num,props,valid_thru,list_time,price,has_discount,has_invoice,has_warranty,has_showcase,modified,delist_time,postage_id,seller_cids,outer_id");
                paramsTable.Add("q", itemreq.Q);
                paramsTable.Add("cid", itemreq.Cid);
                paramsTable.Add("page_no", itemreq.PageNo);
                paramsTable.Add("page_size", itemreq.PageSize);
                paramsTable.Add("order_by", itemreq.OrderBy);
                paramsTable.Add("banner", itemreq.Banner);
                paramsTable.Add("seller_cids", itemreq.SellerCids);
                paramsTable.Add("has_discount", itemreq.HasDiscount);
                paramsTable.Add("is_taobao", itemreq.IsTaobao);
                paramsTable.Add("has_showcase", itemreq.HasShowcase);
                paramsTable.Add("is_ex", itemreq.IsExternal);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
          
        }

        /// <summary>
        /// 得到当前会话用户库存中的商品列表
        /// </summary>
        public static ItemRsp ItemsInventoryGet(string session, string banner, int pageNo, int pageSize)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.items.inventory.get");
                paramsTable.Add("fields", "approve_status,iid,num_iid,title,nick,type,cid,pic_url,num,props,valid_thru,list_time,price,has_discount,has_invoice,has_warranty,has_showcase,modified,delist_time,postage_id,seller_cids,outer_id");
                paramsTable.Add("page_no", pageNo);
                paramsTable.Add("page_size", pageSize);
                paramsTable.Add("banner", banner);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 得到单个商品信息 （未登录只能取到公开数据，登录可以取到全部数据）,
        /// 但我们必须选择登陆获取,即APIInvokeType.Private
        /// nick必填，iid与num_iid的两者选一
        /// </summary>
        public static ItemRsp ItemGet(string session,string nick, string iid, string num_iid)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.item.get");
                paramsTable.Add("fields", "iid,detail_url,num_iid,title,nick,type,cid,seller_cids,props,input_pids,input_str,desc,pic_url,num,valid_thru,list_time,delist_time,stuff_status,location,price,post_fee,express_fee,ems_fee,has_discount,freight_payer,has_invoice,has_warranty,has_showcase,modified,approve_status,postage_id,product_id,auction_point,property_alias,item_img,prop_img,sku,outer_id,is_virtual,is_taobao,is_ex,video,is_3D,score,volume,one_station");
                paramsTable.Add("nick", nick);
                paramsTable.Add("iid", iid);
                paramsTable.Add("num_iid", num_iid);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// 宝贝删除
        /// </summary>
        public static ItemRsp ItemDelete(string session,string iid)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.item.delete");
                paramsTable.Add("iid", iid);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 更新单个商品信息,iid必填
        /// </summary>
        public static ItemRsp ItemUpdate(string session,ItemReq itemreq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.item.update");
                paramsTable.Add("iid", itemreq.Iid);
                paramsTable.Add("props", itemreq.Props);
                paramsTable.Add("cid", itemreq.Cid);
                paramsTable.Add("approve_status", itemreq.ApproveStatus);
                paramsTable.Add("num", itemreq.Num);
                paramsTable.Add("price", itemreq.Price);
                paramsTable.Add("title", itemreq.Title);
                paramsTable.Add("desc", itemreq.Desc);
                paramsTable.Add("location.state", itemreq.Location.State);
                paramsTable.Add("location.city", itemreq.Location.City);
                paramsTable.Add("freight_payer", itemreq.FreightPayer);
                paramsTable.Add("valid_thru", itemreq.ValidThru);
                paramsTable.Add("has_invoice", itemreq.HasInvoice);
                paramsTable.Add("has_warranty", itemreq.HasWarranty);
                paramsTable.Add("auto_repost", itemreq.AutoRepost);
                paramsTable.Add("has_showcase", itemreq.HasShowcase);
                paramsTable.Add("has_discount", itemreq.HasDiscount);
                paramsTable.Add("post_fee", itemreq.PostFee);
                paramsTable.Add("express_fee", itemreq.ExpressFee);
                paramsTable.Add("ems_fee", itemreq.EmsFee);
                paramsTable.Add("list_time", itemreq.ListTime);
                paramsTable.Add("increment", itemreq.Increment);
                paramsTable.Add("image", itemreq.Image);
                paramsTable.Add("stuff_status", itemreq.StuffStatus);
                paramsTable.Add("property_alias", itemreq.PropAlias);
                paramsTable.Add("input_pids", itemreq.InputPids);
                paramsTable.Add("input_str", itemreq.InputStrs);
                paramsTable.Add("sku_quantities", itemreq.SkuQuantities);
                paramsTable.Add("sku_prices", itemreq.Skuprices);
                paramsTable.Add("sku_properties", itemreq.SkuProperties);
                paramsTable.Add("sku_outer_ids", itemreq.SkuOuterIds);
                paramsTable.Add("seller_cids", itemreq.SellerCids);
                paramsTable.Add("postage_id", itemreq.PostageId);
                paramsTable.Add("lang", itemreq.Lang);
                paramsTable.Add("outer_id", itemreq.OuterId);
                paramsTable.Add("product_id", itemreq.ProductId);
                paramsTable.Add("pic_path", itemreq.PicUrl);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加一个商品
        /// </summary>
        public static ItemRsp ItemAdd(string session,ItemReq itemreq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.item.add");
                paramsTable.Add("num", itemreq.Num);
                paramsTable.Add("price", itemreq.Price);
                paramsTable.Add("type", itemreq.Type);
                paramsTable.Add("stuff_status", itemreq.StuffStatus);
                paramsTable.Add("title", itemreq.Title);
                paramsTable.Add("desc", itemreq.Desc);
                if (itemreq.Location!=null)
                {
                    paramsTable.Add("location.state", itemreq.Location.State);
                    paramsTable.Add("location.city", itemreq.Location.City);
                }
                paramsTable.Add("approve_status", itemreq.ApproveStatus);
                paramsTable.Add("cid", itemreq.Cid);
                paramsTable.Add("props", itemreq.Props);
                paramsTable.Add("freight_payer", itemreq.FreightPayer);
                paramsTable.Add("valid_thru", itemreq.ValidThru);
                paramsTable.Add("has_invoice", itemreq.HasInvoice);
                paramsTable.Add("has_warranty", itemreq.HasWarranty);
                paramsTable.Add("auto_repost", itemreq.AutoRepost);
                paramsTable.Add("has_showcase", itemreq.HasShowcase);
                paramsTable.Add("seller_cids", itemreq.SellerCids);
                paramsTable.Add("has_discount", itemreq.HasDiscount);
                paramsTable.Add("post_fee", itemreq.PostFee);
                paramsTable.Add("express_fee", itemreq.ExpressFee);
                paramsTable.Add("ems_fee", itemreq.EmsFee);
                paramsTable.Add("list_time", itemreq.ListTime);
                paramsTable.Add("increment", itemreq.Increment);
                paramsTable.Add("image", itemreq.Image);
                paramsTable.Add("postage_id", itemreq.PostageId);
                paramsTable.Add("auction_point", itemreq.AuctionPoint);
                paramsTable.Add("property_alias", itemreq.PropAlias);
                paramsTable.Add("input_pids", itemreq.InputPids);
                paramsTable.Add("input_str", itemreq.InputStrs);
                paramsTable.Add("sku_properties", itemreq.SkuProperties);
                paramsTable.Add("sku_quantities", itemreq.SkuQuantities);
                paramsTable.Add("sku_prices", itemreq.Skuprices);
                paramsTable.Add("sku_outer_ids", itemreq.SkuOuterIds);
                paramsTable.Add("lang", itemreq.Lang);
                paramsTable.Add("outer_id", itemreq.OuterId);
                paramsTable.Add("product_id", itemreq.ProductId);
                paramsTable.Add("pic_path", itemreq.PicUrl);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;	
            }
            
        }

        /// <summary>
        /// 添加商品图片,
        /// 如果更新图片需要设置itemimg_id(id)，且该itemimg_id(id)的图片记录需要属于传入的iid对应的商品，
        /// 如果新增图片则不用设置。
        /// image：商品图片内容(更新图片其他信息时可以不传)。类型:JPG,GIF;大小不超过:1M 。
        /// position：图片序号，产品里的图片展示顺序，数据越小越靠前。要求是正整数 。
        /// </summary>
        public static ItemRsp ItemImgUpload(string session, string iid, string id, Nullable<int> position, Byte[] picBytes, bool is_major)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.item.img.upload");
                paramsTable.Add("iid", iid);
                paramsTable.Add("id", id);
                paramsTable.Add("position", position);
                paramsTable.Add("is_major", is_major);
                paramsTable.Add("session", session);
                paramsTable.PictureBytes = picBytes;
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 删除商品图片
        /// </summary>
        public static ItemRsp ItemImgDelete(string session,int id, string iid)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.item.img.delete");
                paramsTable.Add("id", id);
                paramsTable.Add("iid", iid);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// 删除属性图片
        /// </summary>
        public static ItemRsp ItemPropimgDelete(string session,int id, string iid)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.item.propimg.delete");
                paramsTable.Add("id", id);
                paramsTable.Add("iid", iid);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 添加或修改属性图片
        /// </summary>
        public static ItemRsp ItemPropimgUpload(string session,string iid, string properties, string image, int id, int position)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.item.propimg.upload");
                paramsTable.Add("iid", iid);
                paramsTable.Add("properties", properties);
                paramsTable.Add("image", image);
                paramsTable.Add("id", id);
                paramsTable.Add("position", position);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
          
        }
        /// <summary>
        /// 添加SKU
        /// </summary>
        public static ItemRsp ItemSkuAdd(string session,string iid, string properties, string quantity, string price, string outer_id, string lang)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.item.sku.add");
                paramsTable.Add("iid", iid);
                paramsTable.Add("properties", properties);
                paramsTable.Add("quantity", quantity);
                paramsTable.Add("price", price);
                paramsTable.Add("outer_id", outer_id);
                paramsTable.Add("lang", lang);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// 获取SKU
        /// </summary>
        public static ItemRsp ItemSkuGet(string session,string sku_id, string nick)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.item.sku.get");
                paramsTable.Add("fields", "sku_id,iid,properties,quantity,price,outer_id,created,modified,status");
                paramsTable.Add("sku_id", sku_id);
                paramsTable.Add("nick", nick);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 更新SKU信息
        /// </summary>
        public static ItemRsp ItemSkuUpate(string session,string iid, string properties, int quantity, string price, string outer_id, string lang)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.item.sku.update");
                paramsTable.Add("iid", iid);
                paramsTable.Add("properties", properties);
                paramsTable.Add("quantity", quantity);
                paramsTable.Add("price", price);
                paramsTable.Add("outer_id", outer_id);
                paramsTable.Add("lang", lang);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// 根据卖家昵称和商品ID列表获取SKU信息
        /// </summary>
        public static ItemRsp ItemSkusGet(string session, string sku_id, string num_iid)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.item.skus.get");
                paramsTable.Add("fields", "sku_id,iid,properties,quantity,price,outer_id,created,modified,status");
                paramsTable.Add("sku_id", sku_id);
                paramsTable.Add("num_iid ", num_iid);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// 商品下架
        /// </summary>
        public static ItemRsp ItemUpdateDelisting(string session,string iid)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.update.delisting");
                paramsTable.Add("iid", iid);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 一口价商品上架
        /// </summary>
        public static ItemRsp ItemUpdateListing(string session,string iid, int num)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.update.listing");
                paramsTable.Add("iid", iid);
                paramsTable.Add("num", num);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// 一口价商品上架
        /// </summary>
        public static ItemRsp ItemUpdateListing(string session, string iid)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.update.listing");
                paramsTable.Add("iid", iid);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 获取卖家的运费模板
        /// </summary>
        public static ItemRsp PostagesGet(string session)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.postages.get");
                paramsTable.Add("fields", "postage_id,name,memo,created,modified,post_price,post_increase,express_price,express_increase,ems_price,ems_increase,postage_mode.id,postage_mode.type,postage_mode.dests,postage_mode.price,postage_mode.increase");
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;	
            }
           
        }
        /// <summary>
        /// 获取单个运费模板
        /// </summary>
        public static ItemRsp PostageGet(string session, string postage_id, string nick)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.postage.get");
                paramsTable.Add("fields", "postage_id,name,memo,created,modified,post_price,post_increase,express_price,express_increase,ems_price,ems_increase,postage_mode.id,postage_mode.type,postage_mode.dests,postage_mode.price,postage_mode.increase");
                paramsTable.Add("postage_id", postage_id);
                paramsTable.Add("nick", nick);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 添加邮费模板
        /// </summary>
        public static ItemRsp PostageAdd(string session, string name, string memo, string post_price, string post_increase, string express_price, string express_increase, string ems_price, string ems_increase, string postage_mode_types, string postage_mode_dests, string postage_mode_prices, string postage_mode_increases)
        {
            try
            {
           TopDictionary paramsTable = new TopDictionary();
            paramsTable.Add("method", "taobao.postage.add");
            paramsTable.Add("name", name);
            paramsTable.Add("memo", memo);
            paramsTable.Add("post_price", post_price);
            paramsTable.Add("post_increase", post_increase);
            paramsTable.Add("express_price", express_price);
            paramsTable.Add("express_increase", express_increase);
            paramsTable.Add("ems_price", ems_price);
            paramsTable.Add("ems _increase", ems_increase);
            paramsTable.Add("postage_mode_types", postage_mode_types);
            paramsTable.Add("postage_mode_dests", postage_mode_dests);
            paramsTable.Add("postage_mode_prices", postage_mode_prices);
            paramsTable.Add("postage_mode_increases", postage_mode_increases);
            paramsTable.Add("session", session);
            return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 修改邮费模板
        /// </summary>
        public static ItemRsp PostageUpdate(string session, string name, string memo, string postage_id, string post_price, string post_increase, string express_price, string express_increase, string ems_price, string ems_increase, string Postage_mode_ids, string postage_mode_types, string postage_mode_dests, string postage_mode_prices, string postage_mode_increases, string postage_mode_optTypes)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.postage.update");
                paramsTable.Add("name", name);
                paramsTable.Add("memo", memo);
                paramsTable.Add("postage_id", postage_id);
                paramsTable.Add("post_price", post_price);
                paramsTable.Add("post_increase", post_increase);
                paramsTable.Add("express_price", express_price);
                paramsTable.Add("express_increase", express_increase);
                paramsTable.Add("ems_price", ems_price);
                paramsTable.Add("ems_increase", ems_increase);
                paramsTable.Add("postage_mode_ids", Postage_mode_ids);
                paramsTable.Add("postage_mode_types", postage_mode_types);
                paramsTable.Add("postage_mode_dests", postage_mode_dests);
                paramsTable.Add("postage_mode_prices", postage_mode_prices);
                paramsTable.Add("postage_mode_increases", postage_mode_increases);
                paramsTable.Add("postage_mode_optTypes", postage_mode_optTypes);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;	
            }
            
        }
        /// <summary>
        /// 删除单个运费模板
        /// </summary>
        public static ItemRsp PostageDelete(string session,string postage_id)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.postage.delete");
                paramsTable.Add("postage_id", postage_id);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
          
        }
        /// <summary>
        /// 根据外部ID取商品
        /// </summary>
        public static ItemRsp ItemsCustomGet(string session,string outer_id)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.items.custom.get");
                paramsTable.Add("fields", "iid,detail_url,num_iid,title,nick,type,cid,seller_cids,props,input_pids,input_str,desc,pic_url,num,valid_thru,list_time,delist_time,stuff_status,location,price,post_fee,express_fee,ems_fee,has_discount,freight_payer,has_invoice,has_warranty,has_showcase,modified,approve_status,postage_id,auction_point,property_alias,item_imgs,prop_imgs,skus,outer_id,is_virtual,is_taobao,is_ex,videos,is_3D,score,volume,one_station");
                paramsTable.Add("outer_id", outer_id);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// 根据外部ID取商品SKU
        /// </summary>
        public static ItemRsp SkusCustomGet(string session,string outer_id)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.skus.custom.get");
                paramsTable.Add("fields", "sku_id,iid,properties,quantity,price,outer_id,created,modified,status");
                paramsTable.Add("outer_id", outer_id);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 取消橱窗推荐一个商品
        /// </summary>
        public static ItemRsp ItemRecommendDelete(string session,string iid)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.item.recommend.delete");
                paramsTable.Add("iid", iid);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 橱窗推荐一个商品
        /// </summary>
        public static ItemRsp ItemRecommendAdd(string session,string iid)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.item.recommend.add");
                paramsTable.Add("iid", iid);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ItemRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }

        }
    
}
   