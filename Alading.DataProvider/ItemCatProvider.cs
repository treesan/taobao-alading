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
using System.Data;
using System.Data.EntityClient;
using Alading.Core;
namespace Alading.DataProvider
{
   
    public partial class DataProviderClass : IAlading
    {
        public ReturnType UpdateItemCatPropTag(List<string> cidlist)
        {
            try
            {   
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    #region 新方法，但没有更新成功
                    /*//获得连接字符串
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;

                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        //打开数据库连接
                        sqlConnection.Open();
                        //新建一个sql命令对象
                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.Connection = sqlConnection;
                        //创建执行脚本
                        sqlCmd.CommandText = "Update ItemCat Set PropTag=0 Where cid=@cid";

                        sqlCmd.Parameters.Add("@cid", SqlDbType.Int);

                        //像存储过程一样预编译
                        sqlCmd.Prepare();

                        foreach (int cid in cidlist)
                        {
                            sqlCmd.Parameters["@cid"].Value = cid;
                            int i = sqlCmd.ExecuteNonQuery();
                        }

                        return ReturnType.Success;
                    }*/
                #endregion

                    var result = alading.ItemCat.Where(BuildWhereInExpression<ItemCat, string>(v => v.cid, cidlist));
                    foreach(ItemCat itemcat in result)
                    {
                        itemcat.PropTag = true;
                    }
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
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

        public bool AddItemCatSqlBulkCopy(DataTable dataTable)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //获得连接字符串
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;
                 
                    SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connectionString);
                    //sqlBulkCopy.DestinationTableName = alading.ItemCat.CommandText;
                    sqlBulkCopy.DestinationTableName = "A23";
                    sqlBulkCopy.BatchSize = dataTable.Rows.Count;

                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    //打开数据库连接
                    sqlConnection.Open();

                    #region 添加之前清空数据库
                    //新建一个sql命令对象
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlConnection;
                    //创建执行脚本
                    sqlCmd.CommandText = "delete A23";
                    //像存储过程一样预编译
                    sqlCmd.Prepare();
                    //执行脚本返回影响行数
                    int i = sqlCmd.ExecuteNonQuery(); 
                    #endregion

                    if (dataTable != null && dataTable.Rows.Count != 0)
                    {
                        sqlBulkCopy.ColumnMappings.Add("cid", "A23P1");
                        sqlBulkCopy.ColumnMappings.Add("parent_cid", "A23P2");
                        sqlBulkCopy.ColumnMappings.Add("name", "A23P3");
                        sqlBulkCopy.ColumnMappings.Add("is_parent", "A23P4");
                        sqlBulkCopy.ColumnMappings.Add("status", "A23P5");
                        sqlBulkCopy.ColumnMappings.Add("sort_order", "A23P6");
                        sqlBulkCopy.ColumnMappings.Add("PropTag", "A23P7");             
                        sqlBulkCopy.WriteToServer(dataTable);
                    }
                    sqlBulkCopy.Close();
                    sqlConnection.Close();
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType AddItemCat(ItemCat itemcat)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToItemCat(itemcat);
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
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
                
        public ReturnType AddItemCat(List<ItemCat> itemcatList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (ItemCat itemcat in itemcatList)
                    {
                        alading.AddToItemCat(itemcat);
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
       
        public ReturnType RemoveAllItemCat()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemCat> list = alading.ItemCat.ToList();
                    foreach (ItemCat itemcat in list)
                    {
                        alading.DeleteObject(itemcat);
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
       
        public ReturnType RemoveItemCat(Func<ItemCat, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemCat> list = alading.ItemCat.Where(func).ToList();
                    foreach (ItemCat itemcat in list)
                    {
                        alading.DeleteObject(itemcat);
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

        public List<ItemCat> GetItemCat(List<string> cidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.ItemCat.Where(BuildWhereInExpression<ItemCat, int>(v => v.ItemCatID, itemcatIDList));*/
                    var result = alading.ItemCat.Where(BuildWhereInExpression<ItemCat, string>(v => v.cid, cidList));
                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public  List<ItemCat> GetAllItemCat(List<string> cidlist)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemCat> listItemCat = alading.ItemCat.Where(BuildWhereInExpression<ItemCat, string>(v => v.cid, cidlist)).Distinct().ToList();
    
                    for (int i = 0; i < listItemCat.Count; i++)
                    {
                        ItemCat itemCat = listItemCat[i];
                        while (itemCat.parent_cid != "0")
                        {
                            ItemCat itemcat = alading.ItemCat.FirstOrDefault(p => p.cid == itemCat.parent_cid);
                            if (listItemCat.Contains(itemcat))
                            {
                                break;
                            }
                            listItemCat.Add(itemcat);
                            itemCat = itemcat;
                        }
                    }
                    return listItemCat.Distinct().OrderBy(v=>v.cid).ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveItemCat(List<string> cidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.ItemCat.Where(BuildWhereInExpression<ItemCat, int>(v => v.ItemCatID, itemcatIDList));*/
                    var result = alading.ItemCat.Where(BuildWhereInExpression<ItemCat, string>(v => v.cid, cidList));
                    foreach (ItemCat s in result)
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


        public ReturnType RemoveItemCat(string cid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<ItemCat> list = alading.ItemCat.Where(p => p.ItemCatID == itemcatID).ToList();*/
                    List<ItemCat> list = alading.ItemCat.Where(p => p.cid == cid).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        ItemCat sy = list.First();
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
      
        public ReturnType UpdateItemCat(ItemCat itemcat)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*ItemCat result = alading.ItemCat.Where(p => p.ItemCatID == itemcat.ItemCatID).FirstOrDefault();*/
                    ItemCat result = alading.ItemCat.Where(p => p.cid == itemcat.cid).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("ItemCat", itemcat);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.cid = itemcat.cid;
                    
                        result.parent_cid = itemcat.parent_cid;
                    
                        result.name = itemcat.name;
                    
                        result.is_parent = itemcat.is_parent;
                    
                        result.status = itemcat.status;
                    
                        result.sort_order = itemcat.sort_order;
			
                    */
                    #endregion  
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }
                    else
                    {
                        return ReturnType.OthersError;
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

        public ReturnType UpdateItemCatPropTag(string cid, bool value)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    ItemCat result = alading.ItemCat.First(p => p.cid == cid);
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                   else
                    {
                        result.PropTag = value;
                    }
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }
                    else
                    {
                        return ReturnType.SaveFailed;
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

        public ReturnType UpdateItemCat(string cid, ItemCat itemcat)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.ItemCat.Where(p => p.ItemCatID == itemcatID).ToList();*/
                    var result = alading.ItemCat.Where(p => p.cid == cid).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    ItemCat ob = result.First();
                    ob.cid = itemcat.cid;
                    ob.parent_cid = itemcat.parent_cid;
                    ob.name = itemcat.name;
                    ob.is_parent = itemcat.is_parent;
                    ob.status = itemcat.status;
                    ob.sort_order = itemcat.sort_order;
                    
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }  
                    else
                    {
                        return ReturnType.OthersError;
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

        public List<string> GetNotDownCids()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var q = from c in alading.ItemCat.Where(c=>c.is_parent==false&&(c.PropTag==null||c.PropTag==false)).OrderBy(i=>i.cid)
                            select c.cid;
                    return q.Distinct().ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemCat> GetAllItemCat()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemCat> list = alading.ItemCat.OrderBy(c=>c.cid).ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<ItemCat> GetItemCat(Func<ItemCat, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemCat> list = alading.ItemCat.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ItemCat GetItemCat(string cid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<ItemCat> list = alading.ItemCat.Where(p => p.ItemCatID == itemcatID).ToList();*/
                    List<ItemCat> list = alading.ItemCat.Where(p => p.cid == cid).ToList();
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

        public List<ItemCat> GetItemCat(string parent_cid,string status)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<ItemCat> list = alading.ItemCat.Where(p => p.ItemCatID == itemcatID).ToList();*/
                    Func<ItemCat, bool> func = new Func<ItemCat, bool>(p => p.parent_cid == parent_cid && p.status == status);
                    List<ItemCat> list = alading.ItemCat.Where(func).OrderBy(i=>i.cid).ToList(); 
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<ItemCat> GetItemCat(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.ItemCat orderby u.ItemCatID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.ItemCat.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<ItemCat> GetItemCat(Func<ItemCat, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<ItemCat> list = alading.ItemCat.Where(func).OrderByDescending(a=>a.ItemCatID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

    }
}

