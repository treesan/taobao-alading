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
using System.Diagnostics;
using System.Data.EntityClient;
using System.Data;
using Alading.Taobao.Entity;
using Alading.Core;
namespace Alading.DataProvider
{
   
    public partial class DataProviderClass : IAlading
    {
        public ReturnType UpdateItemPropValueDataParameters(List<PropValue> propValues)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //获得连接字符串
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;

                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        //打开数据库连接
                        sqlConnection.Open();
                        //新建一个sql命令对象
                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.Connection = sqlConnection;
                        //创建执行脚本
                        sqlCmd.CommandText = "Update A21 Set A21P7=1 Where A21P4=@vid and A21P2=@pid and A21P1=@cid";

                        sqlCmd.Parameters.Add("@vid", SqlDbType.VarChar,50);
                        sqlCmd.Parameters.Add("@pid", SqlDbType.VarChar,50);
                        sqlCmd.Parameters.Add("@cid", SqlDbType.VarChar,50);

                        //像存储过程一样预编译
                        sqlCmd.Prepare();

                        foreach (PropValue pv in propValues)
                        {  
                            sqlCmd.Parameters["@vid"].Value = pv.Vid;
                            sqlCmd.Parameters["@pid"].Value = pv.Pid;
                            sqlCmd.Parameters["@cid"].Value = pv.Cid;                            

                            int i = sqlCmd.ExecuteNonQuery();
                        }

                        return ReturnType.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }
        
        public ReturnType AddItemPropValueSqlBulkCopy(DataTable dataTable)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //获得连接字符串
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;
                    
                    SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connectionString);
                    //sqlBulkCopy.DestinationTableName = alading.ItemPropValue.CommandText;
                    sqlBulkCopy.DestinationTableName = "A21";
                    sqlBulkCopy.BatchSize = dataTable.Rows.Count;
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        if (dataTable != null && dataTable.Rows.Count != 0)
                        {
                            sqlBulkCopy.ColumnMappings.Add("cid", "A21P1");
                            sqlBulkCopy.ColumnMappings.Add("is_parent", "A21P7");
                            sqlBulkCopy.ColumnMappings.Add("name", "A21P5");
                            sqlBulkCopy.ColumnMappings.Add("name_alias", "A21P6");
                            sqlBulkCopy.ColumnMappings.Add("pid", "A21P2");
                            sqlBulkCopy.ColumnMappings.Add("prop_name", "A21P3");
                            sqlBulkCopy.ColumnMappings.Add("vid", "A21P4");
                            sqlBulkCopy.ColumnMappings.Add("status", "A21P8");
                            sqlBulkCopy.ColumnMappings.Add("sort_order", "A21P9");
                            sqlBulkCopy.WriteToServer(dataTable);
                        }
                    }
                    return ReturnType.Success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType AddItemPropValue(ItemPropValue itempropvalue)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Alading.Entity.ItemPropValue DBpropvalue = alading.ItemPropValue.Where(p => p.cid == itempropvalue.cid && p.pid == itempropvalue.pid && p.vid == itempropvalue.vid).FirstOrDefault();
                    if (DBpropvalue==null)
                    {
                        alading.AddToItemPropValue(itempropvalue);
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
                
        public ReturnType AddItemPropValue(List<ItemPropValue> itempropvalueList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (ItemPropValue itempropvalue in itempropvalueList)
                    {
                        Alading.Entity.ItemPropValue dbpropvalue = alading.ItemPropValue.FirstOrDefault(p => p.cid == itempropvalue.cid && p.pid == itempropvalue.pid && p.vid == itempropvalue.vid);
                        if (dbpropvalue == null)
                        {
                            alading.AddToItemPropValue(itempropvalue);
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
       
        public ReturnType RemoveAllItemPropValue()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemPropValue> list = alading.ItemPropValue.ToList();
                    foreach (ItemPropValue itempropvalue in list)
                    {
                        alading.DeleteObject(itempropvalue);
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
       
        public ReturnType RemoveItemPropValue(Func<ItemPropValue, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemPropValue> list = alading.ItemPropValue.Where(func).ToList();
                    foreach (ItemPropValue itempropvalue in list)
                    {
                        alading.DeleteObject(itempropvalue);
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

        public List<ItemPropValue> GetItemPropValue(List<string> itempropvalueCodeList)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.ItemPropValue.Where(BuildWhereInExpression<ItemPropValue, int>(v => v.ItemPropValueID, itempropvalueIDList));*/
            //        var result = alading.ItemPropValue.Where(BuildWhereInExpression<ItemPropValue, string>(v => v.ItemPropValueCode, itempropvalueCodeList));

            //        return result.ToList();
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    return null;
            //}
        }

        public ReturnType RemoveItemPropValue(List<string> itempropvalueCodeList)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.ItemPropValue.Where(BuildWhereInExpression<ItemPropValue, int>(v => v.ItemPropValueID, itempropvalueIDList));*/
            //        var result = alading.ItemPropValue.Where(BuildWhereInExpression<ItemPropValue, string>(v => v.ItemPropValueCode, itempropvalueCodeList));
            //        foreach (ItemPropValue s in result)
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
    
        public ReturnType RemoveItemPropValue(string itempropvalueCode)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*List<ItemPropValue> list = alading.ItemPropValue.Where(p => p.ItemPropValueID == itempropvalueID).ToList();*/
            //        List<ItemPropValue> list = alading.ItemPropValue.Where(p => p.ItemPropValueCode == itempropvalueCode).ToList();
            //        if (list.Count == 0)
            //        {
            //            return ReturnType.NotExisted;
            //        }

            //        else
            //        {
            //            ItemPropValue sy = list.First();
            //            alading.DeleteObject(sy);
            //            alading.SaveChanges();
            //            return ReturnType.Success;
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
      
        public ReturnType UpdateItemPropValue(ItemPropValue itempropvalue)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*ItemPropValue result = alading.ItemPropValue.Where(p => p.ItemPropValueID == itempropvalue.ItemPropValueID).FirstOrDefault();*/
            //        ItemPropValue result = alading.ItemPropValue.Where(p => p.ItemPropValueCode == itempropvalue.ItemPropValueCode).FirstOrDefault();
            //        if (result == null)
            //        {
            //            return ReturnType.NotExisted;
            //        }
            //        #region   Using Attach() Function Update,Default USE;          
            //        alading.Attach(result);
            //        alading.ApplyPropertyChanges("ItemPropValue", itempropvalue);
            //        #endregion
                    
            //        #region    Using All Items Replace To Update ,Default UnUse
            //        /*		
                    
            //            result.cid = itempropvalue.cid;
                    
            //            result.pid = itempropvalue.pid;
                    
            //            result.prop_name = itempropvalue.prop_name;
                    
            //            result.vid = itempropvalue.vid;
                    
            //            result.name = itempropvalue.name;
                    
            //            result.name_alias = itempropvalue.name_alias;
                    
            //            result.is_parent = itempropvalue.is_parent;
                    
            //            result.status = itempropvalue.status;
                    
            //            result.sort_order = itempropvalue.sort_order;
			
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
       
        public ReturnType UpdateItemPropValue(string itempropvalueCode, ItemPropValue itempropvalue)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.ItemPropValue.Where(p => p.ItemPropValueID == itempropvalueID).ToList();*/
            //        var result = alading.ItemPropValue.Where(p => p.ItemPropValueCode == itempropvalueCode).ToList();
            //        if (result.Count == 0)
            //        {
            //            return ReturnType.NotExisted;
            //        }
                  
            //        ItemPropValue ob = result.First();
            //        ob.cid = itempropvalue.cid;
            //        ob.pid = itempropvalue.pid;
            //        ob.prop_name = itempropvalue.prop_name;
            //        ob.vid = itempropvalue.vid;
            //        ob.name = itempropvalue.name;
            //        ob.name_alias = itempropvalue.name_alias;
            //        ob.is_parent = itempropvalue.is_parent;
            //        ob.status = itempropvalue.status;
            //        ob.sort_order = itempropvalue.sort_order;
                    
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

        public ReturnType UpdateItemPropValueIsParent(string cid, string pid, string vid, bool value)
         {
             try
             {
                 using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                 {
                     Alading.Entity.ItemPropValue propvalue = alading.ItemPropValue.FirstOrDefault(p => p.cid == cid && p.pid == pid && p.vid == vid);
                     if (propvalue!=null)
                     {
                         propvalue.is_parent = value;
                         alading.SaveChanges();
                         return ReturnType.Success;
                     }
                     else
                     {
                         return ReturnType.NotExisted;
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
     
        public List<ItemPropValue> GetAllItemPropValue()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemPropValue> list = alading.ItemPropValue.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<ItemPropValue> GetItemPropValue(Func<ItemPropValue, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemPropValue> list = alading.ItemPropValue.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ItemPropValue GetItemPropValue(string cid, string pid, string vid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    ItemPropValue itemPropValue = alading.ItemPropValue.FirstOrDefault(i => i.cid == cid && i.pid == pid && i.vid == vid);
                    return itemPropValue;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public ItemPropValue GetItemPropValue(string itempropvalueCode)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*List<ItemPropValue> list = alading.ItemPropValue.Where(p => p.ItemPropValueID == itempropvalueID).ToList();*/
            //        List<ItemPropValue> list = alading.ItemPropValue.Where(p => p.ItemPropValueCode == itempropvalueCode).ToList();
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
        
        public List<ItemPropValue> GetItemPropValue(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.ItemPropValue orderby u.ItemPropValueID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.ItemPropValue.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<ItemPropValue> GetItemPropValue(Func<ItemPropValue, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<ItemPropValue> list = alading.ItemPropValue.Where(func).OrderByDescending(a=>a.ItemPropValueID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetItemPropvalueDownCids()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var q = from v in alading.ItemPropValue
                            select v.cid;
                    return q.Distinct().ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public bool IsExistedPropValueName(string cid, string pid, string vname)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Alading.Entity.ItemPropValue propvalue = alading.ItemPropValue.Where(p => p.cid == cid && p.pid == pid && p.name == vname).FirstOrDefault();
                    if (propvalue==null)
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

        public bool IsPropValueExistedCid(string cid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Alading.Entity.ItemPropValue propvalue = alading.ItemPropValue.Where(p => p.cid == cid).FirstOrDefault();
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

        public List<string> GetPropValueWhereInCids(List<string> cidlist)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<string> whereInCidList =alading.ItemPropValue.Where(BuildWhereInExpression<ItemPropValue, string>(v => v.cid, cidlist)).Select(v=>v.cid).Distinct().ToList();
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

