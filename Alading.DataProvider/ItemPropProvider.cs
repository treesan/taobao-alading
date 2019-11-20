using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Core.Interface;
using Alading.Entity;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Data.Objects;
using Alading.Core.Enum;
using System.Linq.Expressions;
using System.Data.EntityClient;
using System.Data;
using Alading.Core;
namespace Alading.DataProvider
{
   
    public partial class DataProviderClass : IAlading
    {
        public ReturnType AddItemPropSqlBulkCopy(DataTable dataTable)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //获得连接字符串
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;

                    SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connectionString);
                    //sqlBulkCopy.DestinationTableName = alading.ItemProp.CommandText;
                    sqlBulkCopy.DestinationTableName = "A22";
                    sqlBulkCopy.BatchSize = dataTable.Rows.Count;
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    if (dataTable != null && dataTable.Rows.Count != 0)
                    {
                        sqlBulkCopy.ColumnMappings.Add("cid", "A22P1");
                        sqlBulkCopy.ColumnMappings.Add("child_template", "A22P15");
                        sqlBulkCopy.ColumnMappings.Add("is_allow_alias", "A22P18");
                        sqlBulkCopy.ColumnMappings.Add("is_color_prop", "A22P8");
                        sqlBulkCopy.ColumnMappings.Add("is_enum_prop", "A22P9");
                        sqlBulkCopy.ColumnMappings.Add("is_input_prop", "A22P10");
                        sqlBulkCopy.ColumnMappings.Add("is_item_prop", "A22P11");
                        sqlBulkCopy.ColumnMappings.Add("is_key_prop", "A22P6");
                        sqlBulkCopy.ColumnMappings.Add("is_sale_prop", "A22P7");
                        sqlBulkCopy.ColumnMappings.Add("multi", "A22P13");
                        sqlBulkCopy.ColumnMappings.Add("must", "A22P12");
                        sqlBulkCopy.ColumnMappings.Add("name", "A22P5");
                        sqlBulkCopy.ColumnMappings.Add("parent_pid", "A22P3");
                        sqlBulkCopy.ColumnMappings.Add("parent_vid", "A22P4");
                        sqlBulkCopy.ColumnMappings.Add("pid", "A22P2");
                        sqlBulkCopy.ColumnMappings.Add("prop_values", "A22P14");
                        sqlBulkCopy.ColumnMappings.Add("status", "A22P16");
                        sqlBulkCopy.ColumnMappings.Add("sort_order", "A22P17");
                        sqlBulkCopy.WriteToServer(dataTable);
                    }
                    return ReturnType.Success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType AddItemProp(ItemProp itemprop)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Alading.Entity.ItemProp DBitemprop = alading.ItemProp.Where(p => p.cid == itemprop.cid && p.pid == itemprop.pid).FirstOrDefault();
                    if (DBitemprop==null)
                    {
                        alading.AddToItemProp(itemprop);
                        if (alading.SaveChanges() == 1)
                        {
                            return ReturnType.Success;
                        }
                        else
                            return ReturnType.SaveFailed;
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
                
        public ReturnType AddItemProp(List<ItemProp> itempropList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (ItemProp itemprop in itempropList)
                    {
                        Alading.Entity.ItemProp DBitemprop = alading.ItemProp.Where(p => p.cid == itemprop.cid && p.pid == itemprop.pid).FirstOrDefault();
                        if (DBitemprop == null)
                        {
                            alading.AddToItemProp(itemprop);
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
       
        public ReturnType RemoveAllItemProp()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemProp> list = alading.ItemProp.ToList();
                    foreach (ItemProp itemprop in list)
                    {
                        alading.DeleteObject(itemprop);
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
       
        public ReturnType RemoveItemProp(Func<ItemProp, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemProp> list = alading.ItemProp.Where(func).ToList();
                    foreach (ItemProp itemprop in list)
                    {
                        alading.DeleteObject(itemprop);
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

        public List<ItemProp> GetItemProp(List<string> itempropCodeList)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.ItemProp.Where(BuildWhereInExpression<ItemProp, int>(v => v.ItemPropID, itempropIDList));*/
            //        var result = alading.ItemProp.Where(BuildWhereInExpression<ItemProp, string>(v => v.ItemPropCode, itempropCodeList));

            //        return result.ToList();
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    return null;
            //}
        }

        public ReturnType RemoveItemProp(List<string> itempropCodeList)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.ItemProp.Where(BuildWhereInExpression<ItemProp, int>(v => v.ItemPropID, itempropIDList));*/
            //        var result = alading.ItemProp.Where(BuildWhereInExpression<ItemProp, string>(v => v.ItemPropCode, itempropCodeList));
            //        foreach (ItemProp s in result)
            //        {
            //            alading.DeleteObject(s);
            //        }
            //        alading.SaveChanges();
            //        return ReturnType.Success;
            //    }
            //}
            //catch (SqlException sex)
            //{
            //    return ReturnType.ConnFailed;
            //}
            //catch (System.Exception ex)
            //{
            //    return ReturnType.OthersError;
            //}
        }


        public ReturnType RemoveItemProp(string cid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<ItemProp> list = alading.ItemProp.Where(p => p.ItemPropID == itempropID).ToList();*/
                    List<ItemProp> list = alading.ItemProp.Where(p => p.cid == cid).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        ItemProp sy = list.First();
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
      
        public ReturnType UpdateItemProp(ItemProp itemprop)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*ItemProp result = alading.ItemProp.Where(p => p.ItemPropID == itemprop.ItemPropID).FirstOrDefault();*/
            //        ItemProp result = alading.ItemProp.Where(p => p.ItemPropCode == itemprop.ItemPropCode).FirstOrDefault();
            //        if (result == null)
            //        {
            //            return ReturnType.NotExisted;
            //        }
            //        #region   Using Attach() Function Update,Default USE;          
            //        alading.Attach(result);
            //        alading.ApplyPropertyChanges("ItemProp", itemprop);
            //        #endregion
                    
            //        #region    Using All Items Replace To Update ,Default UnUse
            //        /*		
                    
            //            result.cid = itemprop.cid;
                    
            //            result.pid = itemprop.pid;
                    
            //            result.parent_pid = itemprop.parent_pid;
                    
            //            result.parent_vid = itemprop.parent_vid;
                    
            //            result.name = itemprop.name;
                    
            //            result.is_key_prop = itemprop.is_key_prop;
                    
            //            result.is_sale_prop = itemprop.is_sale_prop;
                    
            //            result.is_color_prop = itemprop.is_color_prop;
                    
            //            result.is_enum_prop = itemprop.is_enum_prop;
                    
            //            result.is_input_prop = itemprop.is_input_prop;
                    
            //            result.is_item_prop = itemprop.is_item_prop;
                    
            //            result.must = itemprop.must;
                    
            //            result.multi = itemprop.multi;
                    
            //            result.prop_values = itemprop.prop_values;
                    
            //            result.child_template = itemprop.child_template;
                    
            //            result.status = itemprop.status;
                    
            //            result.sort_order = itemprop.sort_order;
                    
            //            result.is_allow_alias = itemprop.is_allow_alias;
			
            //        */
            //        #endregion  
            //        if (alading.SaveChanges() == 1)
            //        {
            //            return ReturnType.Success;
            //        }
            //    }
            //}
            //catch (SqlException sex)
            //{
            //    return ReturnType.ConnFailed;
            //}
            //catch (Exception ex)
            //{
            //    return ReturnType.OthersError;
            //}
        }
       
        public ReturnType UpdateItemProp(string itempropCode, ItemProp itemprop)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.ItemProp.Where(p => p.ItemPropID == itempropID).ToList();*/
            //        var result = alading.ItemProp.Where(p => p.ItemPropCode == itempropCode).ToList();
            //        if (result.Count == 0)
            //        {
            //            return ReturnType.NotExisted;
            //        }
                  
            //        ItemProp ob = result.First();
            //        ob.cid = itemprop.cid;
            //        ob.pid = itemprop.pid;
            //        ob.parent_pid = itemprop.parent_pid;
            //        ob.parent_vid = itemprop.parent_vid;
            //        ob.name = itemprop.name;
            //        ob.is_key_prop = itemprop.is_key_prop;
            //        ob.is_sale_prop = itemprop.is_sale_prop;
            //        ob.is_color_prop = itemprop.is_color_prop;
            //        ob.is_enum_prop = itemprop.is_enum_prop;
            //        ob.is_input_prop = itemprop.is_input_prop;
            //        ob.is_item_prop = itemprop.is_item_prop;
            //        ob.must = itemprop.must;
            //        ob.multi = itemprop.multi;
            //        ob.prop_values = itemprop.prop_values;
            //        ob.child_template = itemprop.child_template;
            //        ob.status = itemprop.status;
            //        ob.sort_order = itemprop.sort_order;
            //        ob.is_allow_alias = itemprop.is_allow_alias;
                    
            //        if (alading.SaveChanges() == 1)
            //        {
            //            return ReturnType.Success;
            //        }  
            //        else
            //        {
            //            return ReturnType.OthersError;
            //        }
            //    }
            //}
            //catch (SqlException sex)
            //{
            //    return ReturnType.ConnFailed;
            //}
            //catch (System.Exception ex)
            //{
            //    return ReturnType.OthersError;
            //}
        }
     
        public List<ItemProp> GetAllItemProp()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemProp> list = alading.ItemProp.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<ItemProp> GetItemProp(Func<ItemProp, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemProp> list = alading.ItemProp.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ItemProp GetItemProp(string cid,string pid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
                    ItemProp itemProp = alading.ItemProp.FirstOrDefault(i => i.cid == cid && i.pid == pid);
                    return itemProp;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ItemProp GetItemProp(string itempropCode)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*List<ItemProp> list = alading.ItemProp.Where(p => p.ItemPropID == itempropID).ToList();*/
            //        List<ItemProp> list = alading.ItemProp.Where(p => p.ItemPropCode == itempropCode).ToList();
            //        if (list.Count == 0)
            //        {
            //            return null;
            //        }
            //        else
            //        {
            //            return list.First();
            //        }
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
        }
        
        public List<ItemProp> GetItemProp(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.ItemProp orderby u.ItemPropID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.ItemProp.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<ItemProp> GetItemProp(Func<ItemProp, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<ItemProp> list = alading.ItemProp.Where(func).OrderByDescending(a=>a.ItemPropID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetKeyPropPid(string cid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.ItemProp.Where(p =>( p.cid == cid && p.is_key_prop == true )||(p.cid==cid&& p.is_input_prop == true)).Select(p => p.pid).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public bool IsPropExistedCid(string cid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Alading.Entity.ItemProp propvalue = alading.ItemProp.Where(p => p.cid == cid).FirstOrDefault();
                    if (propvalue == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetPropWhereInCids(List<string> cidlist)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<string> whereInCidList = alading.ItemProp.Where(BuildWhereInExpression<ItemProp, string>(v => v.cid, cidlist)).Select(v => v.cid).Distinct().ToList();
                    return whereInCidList;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

