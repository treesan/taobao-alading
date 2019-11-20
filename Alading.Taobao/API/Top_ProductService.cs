using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Taobao.API.Common;
using Alading.Taobao.Entity;
using Alading.Taobao.Entity.Extend;

namespace Alading.Taobao.API
{
    public partial class TopService
    {
        /// <summary>
        /// 获取一个产品的信息
        /// </summary>
        /// <param name="product_id"></param>
        /// <param name="cid"></param>
        /// <param name="props"></param>
        public static ProductRsp ProductGet(string session, string product_id, string cid, string props)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.product.get");
                paramsTable.Add("fields", "product_id,outer_id,tsc,cid,cat_name,props,props_str,name,binds,binds_str,sale_props,sale_props_str,price,desc,pic_url,product_imgs,product_prop_imgs,created,modified");
                paramsTable.Add("product_id", product_id);
                paramsTable.Add("cid", cid);
                paramsTable.Add("props", props);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ProductRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));   
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
                         
        }
        /// <summary>
        /// 获取一个产品的信息
        /// </summary>
        /// <param name="product_id"></param>
        /// <param name="cid"></param>
        /// <param name="props"></param>
        public static ProductRsp ProductGet(string cid, string props)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.product.get");
                paramsTable.Add("fields", "product_id,outer_id,tsc,cid,cat_name,props,props_str,name,binds,binds_str,sale_props,sale_props_str,price,desc,pic_url,product_imgs,product_prop_imgs,created,modified");
                paramsTable.Add("cid", cid);
                paramsTable.Add("props", props);
                return TopUtils.DeserializeObject<ProductRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 搜索产品信息
        /// </summary>
        /// <param name="q"></param>
        /// <param name="cid"></param>
        /// <param name="props"></param>
        /// <param name="page_size"></param>
        /// <param name="page_no"></param>
        public static ProductRsp ProductSearch(string session,string q, string cid, string props, int page_size, int page_no)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.products.search");
                paramsTable.Add("fields", "product_id,name,pic_url,cid,props,price,tsc");
                paramsTable.Add("cid", cid);
                paramsTable.Add("page_size", page_size);
                paramsTable.Add("Page_no", page_no);
                paramsTable.Add("q", q);
                paramsTable.Add("props", props);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ProductRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 上传一个产品，不包括产品子图和属性图片
        /// name,price,image 必需
        /// </summary>
        public static ProductRsp ProductAdd(string session,ProductReq productadd)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.product.add");
                paramsTable.Add("cid", productadd.cid);
                paramsTable.Add("image", productadd.Image);
                paramsTable.Add("outer_id", productadd.OuterId);
                paramsTable.Add("props", productadd.Props);
                paramsTable.Add("binds", productadd.Binds);
                paramsTable.Add("sale_props", productadd.SaleProps);
                paramsTable.Add("customer_props", productadd.CustomerProps);
                paramsTable.Add("name", productadd.Name);
                paramsTable.Add("price", productadd.Price);
                paramsTable.Add("desc", productadd.Description);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ProductRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
          
        }
        /// <summary>
        /// 上传单张产品非主图，如果需要传多张，可调多次
        /// product_id,image 必需
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product_id"></param>
        /// <param name="image"></param>
        /// <param name="position"></param>
        /// <param name="is_major"></param>
        public static ProductRsp ProductImgUpload(string session,ProductReq productImgUpload)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.product.img.upload");
                paramsTable.Add("id", productImgUpload.id);
                paramsTable.Add("image", productImgUpload.Image);
                paramsTable.Add("product_id", productImgUpload.Id);
                paramsTable.Add("position", productImgUpload.Position);
                paramsTable.Add("is_major", productImgUpload.IsMajor);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ProductRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
                        
        }
       /// <summary>
        /// 上传单张属性图片，如果需要传多张，可调多次
        /// product_id,props 必需
       /// </summary>
       /// <param name="id"></param>
       /// <param name="product_id"></param>
       /// <param name="props"></param>
       /// <param name="image"></param>
       /// <param name="position"></param>
        public static ProductRsp ProductPropimgUpload(string session,ProductReq proPropImgload)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.product.propimg.upload");
                paramsTable.Add("id", proPropImgload.id);
                paramsTable.Add("image", proPropImgload.Image);
                paramsTable.Add("product_id", proPropImgload.Id);
                paramsTable.Add("position", proPropImgload.Position);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ProductRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
                        
        }
        /// <summary>
        /// 修改一个产品，不包括图片的修改
        /// product_id必需
        /// </summary>
        /// <param name="product_id"></param>
        /// <param name="outer_id"></param>
        /// <param name="binds"></param>
        /// <param name="?"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="desc"></param>
        /// <param name="image"></param>
        public static ProductRsp ProductUpdate(string session,ProductReq Proupdate)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.product.update");
                paramsTable.Add("product_id", Proupdate.Id);
                paramsTable.Add("image", Proupdate.Image);
                paramsTable.Add("outer_id", Proupdate.OuterId);
                paramsTable.Add("binds", Proupdate.Binds);
                paramsTable.Add("sale_props", Proupdate.SaleProps);
                paramsTable.Add("name", Proupdate.Name);
                paramsTable.Add("price", Proupdate.Price);
                paramsTable.Add("desc", Proupdate.Description);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ProductRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;	
            }
            
        }
        /// <summary>
        /// 获取产品列表
        /// 
        /// </summary>
        /// <param name="nick"></param>
        /// <param name="page_size"></param>
        /// <param name="page_no"></param>
        /// <returns></returns>
        public static ProductRsp ProductsGet(string session,string nick, int page_size, int page_no)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.products.get");
                paramsTable.Add("fields", "product_id,outer_id,tsc,cid,cat_name,props,props_str,name,binds,binds_str,sale_props,sale_props_str,price,desc,pic_url,product_imgs,product_prop_imgs,created,modified");
                paramsTable.Add("nick", nick);
                paramsTable.Add("page_size", page_size);
                paramsTable.Add("page_no", page_no);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ProductRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 删除产品非主图
        /// id ,product_id 必需
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product_id"></param>
        /// <returns></returns>
        public static ProductRsp ProductImgDelete(string session,int id, int product_id)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "Taobao.product.img.delete");
                paramsTable.Add("id", id);
                paramsTable.Add("product_id", product_id);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ProductRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }

        public static ProductRsp ProductPropimgDelete(string session,int id, int product_id)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "Taobao.product.propimg.delete");
                paramsTable.Add("id", id);
                paramsTable.Add("product_id", product_id);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ProductRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }

    }
}
