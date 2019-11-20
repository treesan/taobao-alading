using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Alading.Core.Interface;
using Alading.Entity;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Data.Objects;
using Alading.Core.Enum;
using System.Linq.Expressions;
using System.Data;
using System.Diagnostics;
using System.Data.EntityClient;
using Alading.Core;
namespace Alading.DataProvider
{
    
    public partial class DataProviderClass : IAlading
    {
        
        
        public List<View_AssembleProduct> GetViewAssembleProduct(string assemblecode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_AssembleProduct> assembleproductList = alading.View_AssembleProduct.Where(p => p.AssembleCode == assemblecode).ToList();
                    return assembleproductList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<AssembleItem> GetAllAssembleItem()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.AssembleItem.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AssembleItem> GetAllUsedAssembleItem()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.AssembleItem.Where(p => p.IsUsage == true).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AssembleItem> GetAssembleItem(Func<AssembleItem, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.AssembleItem.Where(func).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AssembleItem> SearchAssembleItem(string keyword, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<AssembleItem> itemAssembleList;
                    //if (IsAllAssemble == true)
                    //{
                    itemAssembleList = alading.AssembleItem.Where(i => i.OuterID.ToUpper().Contains(keyword.ToUpper()) || i.Name.ToUpper().Contains(keyword.ToUpper()) || i.SimpleName.ToUpper().Contains(keyword.ToUpper())
                    || i.SkuProps_Str.ToUpper().Contains(keyword.ToUpper()) || i.Model.ToUpper().Contains(keyword.ToUpper()) || i.Specification.ToUpper().Contains(keyword.ToUpper())).OrderByDescending(i => i.AssembleItemID).ToList();
                    //}
                    //else
                    //{
                       // itemAssembleList = alading.AssembleItem.Where(i => i.IsUsage == IsUsage && (i.OuterID.Contains(keyword) || i.Name.Contains(keyword) || i.SimpleName.Contains(keyword)
                       //|| i.SkuProps_Str.Contains(keyword) || i.Model.Contains(keyword) || i.Specification.Contains(keyword))).OrderByDescending(i => i.AssembleItemID).ToList();
                    //}
                    rowCount = itemAssembleList.Count;                    
                    return itemAssembleList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (Exception ex)
            {
                rowCount = 0;
                throw ex;
            }
        }

        public List<AssembleItem> SelectAssembleItem(string keyword)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<AssembleItem> itemAssembleList = alading.AssembleItem.Where(i => i.OuterID.ToUpper().Contains(keyword.ToUpper()) || i.Name.ToUpper().Contains(keyword.ToUpper()) || i.SimpleName.ToUpper().Contains(keyword.ToUpper())
                    || i.SkuProps_Str.ToUpper().Contains(keyword.ToUpper()) || i.Model.ToUpper().Contains(keyword.ToUpper()) || i.Specification.ToUpper().Contains(keyword.ToUpper())).OrderByDescending(i => i.AssembleItemID).ToList();
                    return itemAssembleList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AssembleItem> GetAssembleItem(Func<AssembleItem, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IEnumerable<AssembleItem> itemList = alading.AssembleItem.Where(func);
                    rowCount = itemList.Count();
                    return itemList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (Exception ex)
            {
                rowCount = 0;
                throw ex;
            }
        }

        public List<AssembleItem> GetAllAssembleItem(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IEnumerable<AssembleItem> itemList = alading.AssembleItem;
                    rowCount = itemList.Count();
                    return itemList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Alading.Entity.View_ShopItem> Get_ViewShopItem(string sellerNick, string keyWords)
        {
             try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //DataTable table = new DataTable();
                    //string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;
                    //SqlDataAdapter adapter = new SqlDataAdapter("SP_GetView_ShopItem", connectionString);
                    //adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //adapter.SelectCommand.Parameters.AddWithValue("@sellerNick", sellerNick);
                    //adapter.SelectCommand.Parameters.AddWithValue("@keyWord", keyWords);
                    List<Alading.Entity.View_ShopItem> viewShopItemList = alading.SP_GetView_ShopItem(keyWords, sellerNick).ToList();
                    return viewShopItemList;
                    
                }
            }
             catch (System.Exception ex)
             {
                 return null;
             }
        }

        public ReturnType AddAssembleItem(AssembleItem itemAssemble)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    alading.AddToAssembleItem(itemAssemble);
                    if (alading.SaveChanges() == 1)
                        return ReturnType.Success;
                    else
                        return ReturnType.SaveFailed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType AddAssembleItem(List<AssembleItem> assembleItemList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (AssembleItem assembleItem in assembleItemList)
                    {
                        alading.AddToAssembleItem(assembleItem);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType UpdateAssembleItem(AssembleItem itemAssemble)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    AssembleItem result = alading.AssembleItem.FirstOrDefault(i => i.AssembleCode == itemAssemble.AssembleCode);
                    if (result != null)
                    {
                        result.AssembleDesc = itemAssemble.AssembleDesc;                     
                        result.Modified = itemAssemble.Modified;
                        result.IsUsage = itemAssemble.IsUsage;
                        result.Name = itemAssemble.Name;
                        result.Price = itemAssemble.Price;                     
                        result.SimpleName = itemAssemble.SimpleName;
                    }
                    else
                        return ReturnType.NotExisted;

                    if (alading.SaveChanges() == 1)
                        return ReturnType.Success;
                    else
                        return ReturnType.SaveFailed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType UpdateItem(string iid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Item result = alading.Item.FirstOrDefault(p => p.iid == iid);
                    if (result != null)
                    {
                        result.IsHistory = true;
                    }
                    else
                        return ReturnType.NotExisted;

                    if (alading.SaveChanges() == 1)
                        return ReturnType.Success;
                    else
                        return ReturnType.SaveFailed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 判断StockItem中此OuterID是否存在,AssembleItem中是否存在
        /// </summary>
        /// <param name="outerID"></param>
        /// <param name="skuProps"></param>
        /// <returns></returns>
        public ReturnType IsAssembleStockItemExisted(string outerID, string skuProps)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    StockItem stockItem = alading.StockItem.FirstOrDefault(i => i.OuterID == outerID);
                    if (stockItem != null)
                        return ReturnType.Conflicted;
                    if (!string.IsNullOrEmpty(skuProps))
                    {
                        AssembleItem result = alading.AssembleItem.FirstOrDefault(i => i.OuterID == outerID && i.SkuProps == skuProps);
                        if (result != null)
                            return ReturnType.PropertyExisted;
                    }
                    else
                    {
                        AssembleItem result = alading.AssembleItem.FirstOrDefault(i => i.OuterID == outerID);
                        if (result != null)
                            return ReturnType.OthersError;
                    }
               
                    return ReturnType.NotExisted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType AddAssembleItemDetails(List<AssembleItem> itemList, List<AssembleDetail> detailList)
        {
            System.Data.Common.DbTransaction tran = null;
            using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            {
                try
                {
                    alading.Connection.Open();
                    tran = alading.Connection.BeginTransaction();

                    foreach (AssembleItem item in itemList)
                    {
                        alading.AddToAssembleItem(item);
                    }
                    foreach (AssembleDetail detail in detailList)
                    {
                        alading.AddToAssembleDetail(detail);
                    }

                    alading.SaveChanges();
                    tran.Commit();
                    return ReturnType.Success;
                }

                catch (Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    throw ex;
                }
                finally
                {
                    if (alading != null && alading.Connection.State != System.Data.ConnectionState.Closed)
                    {
                        alading.Connection.Close();
                    }
                }
            }
        }

        public ReturnType UpdateAssembleItemDetails(AssembleItem itemAssemble, List<AssembleDetail> detailList)
        {
            using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            {
                System.Data.Common.DbTransaction tran = null;
                try
                {
                    alading.Connection.Open();
                    tran = alading.Connection.BeginTransaction();

                    var resultItem = alading.AssembleItem.FirstOrDefault(i => i.AssembleCode == itemAssemble.AssembleCode);
                    if (resultItem != null)
                    {
                        #region AssembleItem
                        resultItem.AssembleCode = itemAssemble.AssembleCode;
                        resultItem.AssembleDesc = itemAssemble.AssembleDesc;
                        resultItem.CatName = itemAssemble.CatName;
                        resultItem.Cid = itemAssemble.Cid;
                        resultItem.Created = itemAssemble.Created;
                        resultItem.IsUsage = itemAssemble.IsUsage;
                        resultItem.Model = itemAssemble.Model;
                        resultItem.Name = itemAssemble.Name;
                        resultItem.OuterID = itemAssemble.OuterID;
                        resultItem.PicUrl = itemAssemble.PicUrl;
                        resultItem.Price = itemAssemble.Price;
                        resultItem.Quantity = itemAssemble.Quantity;
                        resultItem.SimpleName = itemAssemble.SimpleName;
                        resultItem.SkuProps = itemAssemble.SkuProps;
                        resultItem.SkuProps_Str = itemAssemble.SkuProps_Str;
                        resultItem.Specification = itemAssemble.Specification;
                        resultItem.TaxName = itemAssemble.TaxName;
                        resultItem.TaxCode = itemAssemble.TaxCode;
                        resultItem.UnitName = itemAssemble.UnitName;
                        resultItem.UnitCode = itemAssemble.UnitCode;
                        resultItem.Props = itemAssemble.Props;
                        #endregion

                        //先删除AssembleDetail
                        IQueryable<AssembleDetail> resultDetailList = alading.AssembleDetail.Where(i => i.AssembleCode == resultItem.AssembleCode);
                        if (resultDetailList != null && resultDetailList.Count() > 0)
                        {
                            foreach (AssembleDetail detail in resultDetailList)
                            {
                                alading.DeleteObject(detail);
                            }
                        }
                    }

                    foreach (AssembleDetail detail in detailList)
                    {
                        alading.AddToAssembleDetail(detail);
                    }

                    alading.SaveChanges();
                    tran.Commit();
                    return ReturnType.Success;

                }
                catch (Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    throw ex;
                }
                finally
                {
                    if (alading != null && alading.Connection.State != System.Data.ConnectionState.Closed)
                    {
                        alading.Connection.Close();
                    }
                }
            }
        }

        public ReturnType UpdateAssembleItemBase(AssembleItem assembleItem)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var resultItem = alading.AssembleItem.FirstOrDefault(i => i.AssembleCode == assembleItem.AssembleCode);
                    if (resultItem != null)
                    {
                        #region AssembleItem
                        if (resultItem.OuterID.ToUpper() != assembleItem.OuterID.ToUpper())
                        {
                            return ReturnType.PropertyExisted;
                        }
                        resultItem.AssembleDesc = assembleItem.AssembleDesc;
                        resultItem.Model = assembleItem.Model;
                        resultItem.Name = assembleItem.Name;
                        resultItem.OuterID = assembleItem.OuterID;
                        resultItem.PicUrl = assembleItem.PicUrl;
                        resultItem.Price = assembleItem.Price;
                        resultItem.Quantity = assembleItem.Quantity;
                        resultItem.SimpleName = assembleItem.SimpleName;
                        resultItem.Specification = assembleItem.Specification;
                        resultItem.TaxName = assembleItem.TaxName;
                        resultItem.TaxCode = assembleItem.TaxCode;
                        resultItem.UnitName = assembleItem.UnitName;
                        resultItem.UnitCode = assembleItem.UnitCode;                        
                        #endregion                        
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType UpdateAssembleItemDetails(string assembleCode, List<AssembleDetail> detailList)
        {
            using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            {
                System.Data.Common.DbTransaction tran = null;
                try
                {
                    alading.Connection.Open();
                    tran = alading.Connection.BeginTransaction();

                    var resultItem = alading.AssembleItem.FirstOrDefault(i => i.AssembleCode == assembleCode);
                    if (resultItem != null)
                    {
                        //先更新AssembleDetail
                        IQueryable<AssembleDetail> resultDetailList = alading.AssembleDetail.Where(i => i.AssembleCode == resultItem.AssembleCode);
                        if (resultDetailList != null && resultDetailList.Count() > 0)
                        {
                            foreach (AssembleDetail detail in resultDetailList)
                            {
                                AssembleDetail src = detailList.FirstOrDefault(i => i.SkuOuterID == detail.SkuOuterID);
                                if (src != null)
                                {
                                    detail.Count = src.Count;
                                }
                                else
                                {
                                    //说明已被删除
                                    alading.DeleteObject(detail);
                                }
                                detailList.Remove(src);
                            }
                        }
                    }
                    foreach (AssembleDetail ass in detailList)
                    {
                        alading.AddToAssembleDetail(ass);
                    }

                    alading.SaveChanges();
                    tran.Commit();
                    return ReturnType.Success;
                }

                catch (Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    throw ex;
                }
                finally
                {
                    if (alading != null && alading.Connection.State != System.Data.ConnectionState.Closed)
                    {
                        alading.Connection.Close();
                    }
                }
            }
        }

        public ReturnType UpdateAssembleItemProps(AssembleItem assembleItem)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var resultItem = alading.AssembleItem.FirstOrDefault(i => i.AssembleCode == assembleItem.AssembleCode);
                    if (resultItem != null)
                    {                    
                        resultItem.Props = assembleItem.Props;
                        resultItem.InputPids = assembleItem.InputPids;
                        resultItem.InputStr = assembleItem.InputStr;
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType UpdateItemsStatus(List<string> iidList, string status)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Item> itemList = alading.Item.Where(BuildWhereInExpression<Item, string>(v => v.iid, iidList)).ToList();
                    foreach (Item item in itemList)
                    {
                        item.approve_status = status;
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType AddItem(Item item)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Alading.Entity.Item ob = alading.Item.FirstOrDefault(i => i.iid == item.iid);
                    if (ob == null)
                    {
                        item.IsSelected = false;
                        alading.AddToItem(item);
                    }
                    else
                    {
                        #region 赋值
                        ob.iid = item.iid;
                        ob.detail_url = item.detail_url;
                        ob.num_iid = item.num_iid;
                        ob.title = item.title;
                        ob.nick = item.nick;
                        ob.type = item.type;
                        ob.seller_cids = item.seller_cids;
                        ob.props = item.props;
                        ob.input_pids = item.input_pids;
                        ob.input_str = item.input_str;
                        ob.desc = item.desc;
                        ob.pic_url = item.pic_url;
                        ob.num = item.num;
                        ob.valid_thru = item.valid_thru;
                        ob.list_time = item.list_time;
                        ob.delist_time = item.delist_time;
                        ob.stuff_status = item.stuff_status;
                        ob.location_zip = item.location_zip;
                        ob.location_address = item.location_address;
                        ob.location_city = item.location_city;
                        ob.location_state = item.location_state;
                        ob.location_country = item.location_country;
                        ob.location_district = item.location_district;
                        ob.price = item.price;
                        ob.post_fee = item.post_fee;
                        ob.express_fee = item.express_fee;
                        ob.ems_fee = item.ems_fee;
                        ob.has_discount = item.has_discount;
                        ob.freight_payer = item.freight_payer;
                        ob.has_invoice = item.has_invoice;
                        ob.has_warranty = item.has_warranty;
                        ob.has_showcase = item.has_showcase;
                        ob.modified = item.modified;
                        ob.increment = item.increment;
                        ob.auto_repost = item.auto_repost;
                        ob.approve_status = item.approve_status;
                        ob.postage_id = item.postage_id;
                        ob.product_id = item.product_id;
                        ob.auction_point = item.auction_point;
                        ob.property_alias = item.property_alias;
                        ob.item_imgs = item.item_imgs;
                        ob.prop_imgs = item.prop_imgs;
                        ob.skus = item.skus;
                        ob.outer_id = item.outer_id;
                        ob.is_virtual = item.is_virtual;
                        ob.is_taobao = item.is_taobao;
                        ob.is_ex = item.is_ex;
                        ob.videos = item.videos;
                        ob.is_3D = item.is_3D;
                        ob.score = item.score;
                        ob.volume = item.volume;
                        ob.one_station = item.one_station;
                        //如果cid发生变化，则需要将item与库存关联解除
                        if (ob.cid != item.cid)
                        {
                            ob.IsAsociate = item.IsAsociate;
                        }
                        ob.cid = item.cid;
                        #endregion
                    }

                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }

        }

        public ReturnType AddItem(Item item,bool update)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Alading.Entity.Item ob = alading.Item.FirstOrDefault(i => i.iid == item.iid);
                    if (ob == null)
                    {
                        item.IsSelected = false;
                        alading.AddToItem(item);
                    }
                    else if (update)
                    {
                        #region 赋值更新
                        ob.iid = item.iid;
                        ob.detail_url = item.detail_url;
                        ob.num_iid = item.num_iid;
                        ob.title = item.title;
                        ob.nick = item.nick;
                        ob.type = item.type;
                        ob.seller_cids = item.seller_cids;
                        ob.props = item.props;
                        ob.input_pids = item.input_pids;
                        ob.input_str = item.input_str;
                        ob.desc = item.desc;
                        ob.pic_url = item.pic_url;
                        ob.num = item.num;
                        ob.valid_thru = item.valid_thru;
                        ob.list_time = item.list_time;
                        ob.delist_time = item.delist_time;
                        ob.stuff_status = item.stuff_status;
                        ob.location_zip = item.location_zip;
                        ob.location_address = item.location_address;
                        ob.location_city = item.location_city;
                        ob.location_state = item.location_state;
                        ob.location_country = item.location_country;
                        ob.location_district = item.location_district;
                        ob.price = item.price;
                        ob.post_fee = item.post_fee;
                        ob.express_fee = item.express_fee;
                        ob.ems_fee = item.ems_fee;
                        ob.has_discount = item.has_discount;
                        ob.freight_payer = item.freight_payer;
                        ob.has_invoice = item.has_invoice;
                        ob.has_warranty = item.has_warranty;
                        ob.has_showcase = item.has_showcase;
                        ob.modified = item.modified;
                        ob.increment = item.increment;
                        ob.auto_repost = item.auto_repost;
                        ob.approve_status = item.approve_status;
                        ob.postage_id = item.postage_id;
                        ob.product_id = item.product_id;
                        ob.auction_point = item.auction_point;
                        ob.property_alias = item.property_alias;
                        ob.item_imgs = item.item_imgs;
                        ob.prop_imgs = item.prop_imgs;
                        ob.skus = item.skus;
                        ob.outer_id = item.outer_id;
                        ob.is_virtual = item.is_virtual;
                        ob.is_taobao = item.is_taobao;
                        ob.is_ex = item.is_ex;
                        ob.videos = item.videos;
                        ob.is_3D = item.is_3D;
                        ob.score = item.score;
                        ob.volume = item.volume;
                        ob.one_station = item.one_station;
                        //如果cid发生变化，则需要将item与库存关联解除
                        if (ob.cid != item.cid)
                        {
                            ob.IsAsociate = item.IsAsociate;
                        }
                        ob.cid = item.cid;
                        #endregion                  
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }

        }
                
        public ReturnType AddItem(List<Item> itemList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (Item item in itemList)
                    {
                        Alading.Entity.Item obItem = alading.Item.FirstOrDefault(i => i.iid == item.iid);
                        if (obItem == null)
                        {
                            item.IsSelected = false;
                            alading.AddToItem(item);
                        }
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }

        }
       
        public ReturnType RemoveAllItem()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Item> list = alading.Item.ToList();
                    foreach (Item item in list)
                    {
                        alading.DeleteObject(item);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;

                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }
       
        public ReturnType RemoveItem(Func<Item, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Item> list = alading.Item.Where(func).ToList();
                    foreach (Item item in list)
                    {
                        alading.DeleteObject(item);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }

            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public List<Item> GetItem(List<string> iidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.Item.Where(BuildWhereInExpression<Item, int>(v => v.ItemID, itemIDList));*/
                    var result = alading.Item.Where(BuildWhereInExpression<Item, string>(v => v.iid, iidList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveItem(List<string> iidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Item.Where(BuildWhereInExpression<Item, int>(v => v.ItemID, itemIDList));*/
                    var result = alading.Item.Where(BuildWhereInExpression<Item, string>(v => v.iid, iidList));
                    foreach (Item s in result)
                    {
                        alading.DeleteObject(s);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public ReturnType RemoveAssembleItem(string assembleCode)
        {
            using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            {
                System.Data.Common.DbTransaction tran = null;
                try
                {
                    alading.Connection.Open();
                    tran = alading.Connection.BeginTransaction();

                    var q = alading.AssembleDetail.Where(i => i.AssembleCode == assembleCode);
                    foreach (AssembleDetail item in q)
                    {
                        alading.DeleteObject(item);
                    }
                    var result = alading.AssembleItem.FirstOrDefault(i => i.AssembleCode == assembleCode);
                    if (result != null)
                        alading.DeleteObject(result);
                    else
                        return ReturnType.NotExisted;

                    alading.SaveChanges();
                    tran.Commit();
                    return ReturnType.Success;

                }
                catch (System.Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    return ReturnType.SaveFailed;
                }
                finally
                {
                    if (alading != null && alading.Connection.State != System.Data.ConnectionState.Closed)
                    {
                        alading.Connection.Close();
                    }
                }
            }
        }

        public ReturnType RemoveItem(string iid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<Item> list = alading.Item.Where(p => p.ItemID == itemID).ToList();*/
                    List<Item> list = alading.Item.Where(p => p.iid == iid).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        Item sy = list.First();
                        alading.DeleteObject(sy);
                        alading.SaveChanges();
                        return ReturnType.Success;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }
      
        public ReturnType UpdateItem(Item item)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*Item result = alading.Item.Where(p => p.ItemID == item.ItemID).FirstOrDefault();*/
                    Item result = alading.Item.Where(p => p.iid == item.iid).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE; 
                  
                    if (item.detail_url!=null)
                    {
                        result.detail_url = item.detail_url;
                    }
                    if (item.num_iid!=null)
                    {
                        result.num_iid = item.num_iid;
                    }
                    if (item.title != null)
                    {
                        result.title = item.title;
                    }
                    if (item.nick != null)
                    {
                        result.nick = item.nick;
                    }
                    if (item.type != null)
                    {
                        result.type = item.type;
                    }
                    if (item.ItemType != null)
                    {
                        result.ItemType = item.ItemType;
                    }
                    if (item.seller_cids != null)
                    {
                        result.seller_cids = item.seller_cids;
                    }
                    if (item.props != null)
                    {
                        result.props = item.props;
                    }
                    if (item.input_pids != null)
                    {
                        result.input_pids = item.input_pids;
                    }
                    if (item.input_str != null)
                    {
                        result.input_str = item.input_str;
                    }
                    if (item.desc != null)
                    {
                        result.desc = item.desc;
                    }
                    if (item.pic_url != null)
                    {
                        result.pic_url = item.pic_url;
                    }
                    if (item.num != null)
                    {
                        result.num = item.num;
                    }
                    if (item.valid_thru != null)
                    {
                        result.valid_thru = item.valid_thru;
                    }
                    if (item.list_time != null)
                    {
                        result.list_time = item.list_time;
                    }
                    if (item.delist_time != null)
                    {
                        result.delist_time = item.delist_time;
                    }
                    if (item.stuff_status != null)
                    {
                        result.stuff_status = item.stuff_status;
                    }
                    if (item.location_zip != null)
                    {
                        result.location_zip = item.location_zip;
                    }
                    if (item.location_address != null)
                    {
                        result.location_address = item.location_address;
                    }
                    if (item.location_city != null)
                    {
                        result.location_city = item.location_city;
                    }
                    if (item.location_state != null)
                    {
                        result.location_state = item.location_state;
                    }
                    if (item.location_country != null)
                    {
                        result.location_country = item.location_country;
                    }
                    if (item.location_district != null)
                    {
                        result.location_district = item.location_district;
                    }
                    if (item.price != null)
                    {
                        result.price = item.price;
                    }
                    if (item.post_fee != null)
                    {
                        result.post_fee = item.post_fee;
                    }
                    if (item.ems_fee != null)
                    {
                        result.ems_fee = item.ems_fee;
                    }
                    if (item.has_discount != null)
                    {
                        result.has_discount = item.has_discount;
                    }
                    if (item.freight_payer != null)
                    {
                        result.freight_payer = item.freight_payer;
                    }
                    if (item.has_invoice != null)
                    {
                        result.has_invoice = item.has_invoice;
                    }
                    if (item.has_warranty != null)
                    {
                        result.has_warranty = item.has_warranty;
                    }
                    if (item.has_showcase != null)
                    {
                        result.has_showcase = item.has_showcase;
                    }
                    if (item.modified != null)
                    {
                        result.modified = item.modified;
                    }
                    if (item.increment != null)
                    {
                        result.increment = item.increment;
                    }
                    if (item.auto_repost != null)
                    {
                        result.auto_repost = item.auto_repost;
                    }
                    if (item.approve_status != null)
                    {
                        result.approve_status = item.approve_status;
                    }
                    if (item.postage_id != null)
                    {
                        result.postage_id = item.postage_id;
                    }
                    if (item.product_id != null)
                    {
                        result.product_id = item.product_id;
                    }
                    if (item.auction_point != null)
                    {
                        result.auction_point = item.auction_point;
                    }
                    if (item.property_alias != null)
                    {
                        result.property_alias = item.property_alias;
                    }
                    if (item.item_imgs != null)
                    {
                        result.item_imgs = item.item_imgs;
                    }
                    if (item.prop_imgs != null)
                    {
                        result.prop_imgs = item.prop_imgs;
                    }
                    if (item.skus != null)
                    {
                        result.skus = item.skus;
                    }
                    if (item.outer_id != null)
                    {
                        result.outer_id = item.outer_id;
                    }
                    if (item.is_virtual != null)
                    {
                        result.is_virtual = item.is_virtual;
                    }
                    if (item.is_taobao != null)
                    {
                        result.is_taobao = item.is_taobao;
                    }
                    if (item.is_ex != null)
                    {
                        result.is_ex = item.is_ex;
                    }
                    if (item.videos != null)
                    {
                        result.videos = item.videos;
                    }

                    if (item.is_3D != null)
                    {
                        result.is_3D = item.is_3D;
                    }
                    if (item.score != null)
                    {
                        result.score = item.score;
                    }
                    if (item.volume != null)
                    {
                        result.volume = item.volume;
                    }

                    if (item.one_station != null)
                    {
                        result.one_station = item.one_station;
                    }
                    if (item.StockProps != null)
                    {
                        result.StockProps = item.StockProps;
                    }
                    if (item.KeyProps != null)
                    {
                        result.KeyProps = item.KeyProps;
                    }
                    if (item.NotKeyProps != null)
                    {
                        result.NotKeyProps = item.NotKeyProps;
                    }
                    if (item.SaleProps != null)
                    {
                        result.SaleProps = item.SaleProps;
                    }
                    if (item.IsAsociate != null)
                    {
                        result.IsAsociate = item.IsAsociate;
                    }
                    if (item.IsUpdate != null)
                    {
                        result.IsUpdate = item.IsUpdate;
                    }
                    if (item.IsHistory != null)
                    {
                        result.IsHistory = item.IsHistory;
                    }
               
                    if (item.cid != null)
                    {
                        result.cid = item.cid;
                    }
                    #endregion

                    alading.SaveChanges();              
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public ReturnType UpdateItem(string iid, Item item)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Item.Where(p => p.ItemID == itemID).ToList();*/
                    var result = alading.Item.Where(p => p.iid == iid).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    Item ob = result.First();
                    ob.iid = item.iid;
                    ob.detail_url = item.detail_url;
                    ob.num_iid = item.num_iid;
                    ob.title = item.title;
                    ob.nick = item.nick;
                    ob.type = item.type;
                    ob.cid = item.cid;
                    ob.seller_cids = item.seller_cids;
                    ob.props = item.props;
                    ob.input_pids = item.input_pids;
                    ob.input_str = item.input_str;
                    ob.desc = item.desc;
                    ob.pic_url = item.pic_url;
                    ob.num = item.num;
                    ob.valid_thru = item.valid_thru;
                    ob.list_time = item.list_time;
                    ob.delist_time = item.delist_time;
                    ob.stuff_status = item.stuff_status;
                    ob.location_zip = item.location_zip;
                    ob.location_address = item.location_address;
                    ob.location_city = item.location_city;
                    ob.location_state = item.location_state;
                    ob.location_country = item.location_country;
                    ob.location_district = item.location_district;
                    ob.price = item.price;
                    ob.post_fee = item.post_fee;
                    ob.express_fee = item.express_fee;
                    ob.ems_fee = item.ems_fee;
                    ob.has_discount = item.has_discount;
                    ob.freight_payer = item.freight_payer;
                    ob.has_invoice = item.has_invoice;
                    ob.has_warranty = item.has_warranty;
                    ob.has_showcase = item.has_showcase;
                    ob.modified = item.modified;
                    ob.increment = item.increment;
                    ob.auto_repost = item.auto_repost;
                    ob.approve_status = item.approve_status;
                    ob.postage_id = item.postage_id;
                    ob.product_id = item.product_id;
                    ob.auction_point = item.auction_point;
                    ob.property_alias = item.property_alias;
                    ob.item_imgs = item.item_imgs;
                    ob.prop_imgs = item.prop_imgs;
                    ob.skus = item.skus;
                    ob.outer_id = item.outer_id;
                    ob.is_virtual = item.is_virtual;
                    ob.is_taobao = item.is_taobao;
                    ob.is_ex = item.is_ex;
                    ob.videos = item.videos;
                    ob.is_3D = item.is_3D;
                    ob.score = item.score;
                    ob.volume = item.volume;
                    ob.one_station = item.one_station;
                    ob.StockProps = item.StockProps;
                    ob.KeyProps = item.KeyProps;
                    ob.NotKeyProps = item.NotKeyProps;
                    ob.SaleProps = item.SaleProps;
                    ob.IsAsociate = item.IsAsociate;
                    ob.IsUpdate = item.IsUpdate;
                    ob.UnitCode = item.UnitCode;
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public ReturnType UpdateItemIsUpdate(string iid, bool isUpdate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var result = alading.Item.Where(p => p.iid == iid).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                    result.First().IsUpdate = isUpdate;
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public ReturnType UpdateItemsListTime(Dictionary<string,string> itemdic)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<string>iidlist=itemdic.Keys.ToList();
                    List<Item>  itemlist= alading.Item.Where(BuildWhereInExpression<Item, string>(v => v.iid, iidlist)).ToList();
                    foreach (Item item in itemlist)
                    {
                        item.list_time = itemdic.SingleOrDefault(i => i.Key == item.iid).Value;
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
           
        }

        public ReturnType UpdateItemsOuterId(Dictionary<string, string> itemdic,bool? isAssociate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<string> iidlist = itemdic.Keys.ToList();
                    List<Item> itemlist = alading.Item.Where(BuildWhereInExpression<Item, string>(v => v.iid, iidlist)).ToList();
                    foreach (Item item in itemlist)
                    {
                        item.outer_id = itemdic.SingleOrDefault(i => i.Key == item.iid).Value;
                        if (isAssociate!=null)
                        {
                            item.IsAsociate = (bool)isAssociate;
                        }
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }

        }

        public ReturnType UpdateItemType(string iid, string outer_id,string itemType)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Item item = alading.Item.FirstOrDefault(p => p.iid == iid);
                    if (item != null)
                    {
                        item.outer_id = outer_id;
                        item.ItemType = itemType;
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType UpdateItemStockUnit(string iid, string stockUnitcode,string ItemType)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Item item = alading.Item.FirstOrDefault(p => p.iid == iid);
                    if (item != null)
                    {
                        item.UnitCode = stockUnitcode;
                        item.ItemType = ItemType;
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType UpdateItemsAssociate(List<string> iidlist, bool isAssociate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Item> itemlist = alading.Item.Where(BuildWhereInExpression<Item, string>(v => v.iid, iidlist)).ToList();
                    foreach (Item item in itemlist)
                    {
                        item.IsAsociate = isAssociate;
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }

        }

        public List<Item> GetItems(string cid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Item> list = alading.Item.Where(p => p.cid == cid).ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Item> GetItems(List<string> listCid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Item> listItem = alading.Item.Where(BuildWhereInExpression<Item, string>(v => v.cid, listCid)).ToList();
                    return listItem;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        public Item GetItem(string iid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<Item> list = alading.Item.Where(p => p.ItemID == itemID).ToList();*/
                    List<Item> list = alading.Item.Where(p => p.iid == iid).ToList();
                    if (list.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return list.First();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Item> GetItem(string cid, bool IsAssociate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Item> list = alading.Item.Where(p => p.cid == cid && p.IsAsociate == IsAssociate).ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Item> GetItem(List<string> listCid, bool isAssociate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Item> result = alading.Item.Where(BuildWhereInExpression<Item, string>(v => v.cid, listCid)).Where(u => u.IsAsociate == isAssociate).ToList();
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetItemCidByNick(string nick)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.Item.Where(c => c.nick == nick).Select(i => i.cid).Distinct().ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetAllItemOuterId()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<string> outeridList = new List<string>();
                    List<Item> listItem = alading.Item.ToList();
                    foreach (Item item in listItem)
                    {
                        outeridList.Add(item.outer_id);
                    }
                    return outeridList.Distinct().ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public List<Item> GetAllItem()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Item> list = alading.Item.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)  
            {
                throw ex;
            }
        }
      
        public List<Item> GetItem(Func<Item, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Item> list = alading.Item.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Item> GetItem(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var ob = (from u in alading.Item orderby u.ItemID descending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    rowCount = alading.Item.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Item> GetItem(string cid, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Item> listItem = alading.Item.Where(p => p.cid == cid).ToList();
                    var ob = (from u in listItem orderby u.ItemID descending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    rowCount = alading.Item.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Item> GetItem(string sellercid, string nick, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Item> itemlist = new List<Item>();
                    if (string.IsNullOrEmpty(sellercid))
                    {
                        itemlist = alading.Item.Where(i => i.nick == nick ).ToList();
                    }
                    else
                    {
                        itemlist = alading.Item.Where(i => i.nick == nick && i.seller_cids.Contains(sellercid)).ToList();
                    }
                    rowCount = itemlist.Count;
                    return itemlist;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Item> GetItem(string sellercid,string nick ,string aprrovestatus,out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Item> itemlist = new List<Item>();
                    if (string.IsNullOrEmpty(sellercid))
                    {
                        itemlist = alading.Item.Where(i => i.nick == nick && i.approve_status == aprrovestatus).ToList();
                    }
                    else
                    {
                        itemlist = alading.Item.Where(i => i.nick == nick && i.approve_status == aprrovestatus && i.seller_cids.Contains(sellercid)).ToList();
                    }
                    rowCount = itemlist.Count;
                    return itemlist;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Item> GetItem(string sellercid, string sellerNick, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Item> itemlist = new List<Item>();
                     if (string.IsNullOrEmpty(sellercid))
                     {
                         itemlist = alading.Item.Where(p => p.nick == sellerNick ).OrderByDescending(a => a.ItemID).ToList();
                     }
                     else
                     {
                         itemlist = alading.Item.Where(p => p.nick == sellerNick && p.seller_cids.Contains(sellercid)).OrderByDescending(a => a.ItemID).ToList();
                     }
                     rowCount = itemlist.Count();
                     return itemlist.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Item> GetItem(string sellercid, string sellerNick,string approvestatus, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Item> itemlist = new List<Item>();
                     if (string.IsNullOrEmpty(sellercid))
                     {
                          itemlist = alading.Item.Where(p => p.approve_status == approvestatus && p.nick == sellerNick ).OrderByDescending(a => a.ItemID).ToList();
                     }
                    else
                     {
                          itemlist = alading.Item.Where(p => p.approve_status == approvestatus && p.nick == sellerNick && p.seller_cids.Contains(sellercid)).OrderByDescending(a => a.ItemID).ToList();
                     }
                     rowCount = itemlist.Count();
                     return itemlist.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Item> GetItem(Func<Item, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<Item> list = alading.Item.Where(func).OrderByDescending(a=>a.ItemID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType IsItemExisted(string iid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Item result = alading.Item.Where(p => p.iid == iid).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    else
                    {
                        return ReturnType.PropertyExisted;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public List<string> GetItemCids()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.Item.Select(i => i.cid).Distinct().ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<AssembleItem> GetAssembleItem(List<string> outerIdList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<AssembleItem> itemAssembleList = alading.AssembleItem.Where(BuildWhereInExpression<AssembleItem, string>(v => v.OuterID, outerIdList)).Where(p => p.IsUsage == true).ToList();
                    return itemAssembleList;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

    }
}

