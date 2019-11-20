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
using System.Data;
using System.Data.EntityClient;
using Alading.Core;

namespace Alading.DataProvider
{
   
    public partial class DataProviderClass : IAlading
    {        
     
        public ReturnType AddArea(Area area)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToArea(area);
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
                
        public ReturnType AddArea(List<Area> areaList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (Area area in areaList)
                    {
                        alading.AddToArea(area);
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
       
        public ReturnType RemoveAllArea()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Area> list = alading.Area.ToList();
                    foreach (Area area in list)
                    {
                        alading.DeleteObject(area);
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
       
        public ReturnType RemoveArea(Func<Area, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Area> list = alading.Area.Where(func).ToList();
                    foreach (Area area in list)
                    {
                        alading.DeleteObject(area);
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

        public List<Area> GetArea(List<string> idList)
        {
            //throw new NotImplementedException();

            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Area.Where(BuildWhereInExpression<Area, int>(v => v.AreaID, areaIDList));*/
                    var result = alading.Area.Where(BuildWhereInExpression<Area, string>(v =>v.id, idList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }            
        }

        public ReturnType RemoveArea(List<string> idList)
        {
            throw new NotImplementedException();
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Area.Where(BuildWhereInExpression<Area, int>(v => v.AreaID, areaIDList));*/
                    var result = alading.Area.Where(BuildWhereInExpression<Area, string>(v => v.id, idList));
                    foreach (Area s in result)
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

    
        public ReturnType RemoveArea(string id)
        {
            throw new NotImplementedException();
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<Area> list = alading.Area.Where(p => p.AreaID == areaID).ToList();*/
                    List<Area> list = alading.Area.Where(p => p.id == id).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        Area sy = list.First();
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
      
        public ReturnType UpdateArea(Area area)
        {
            throw new NotImplementedException();
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*Area result = alading.Area.Where(p => p.AreaID == area.AreaID).FirstOrDefault();*/
                    Area result = alading.Area.Where(p => p.id == area.id).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("Area", area);
                    #endregion

                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.id = area.id;
                    
                        result.type = area.type;
                    
                        result.name = area.name;
                    
                        result.parent_id = area.parent_id;
                    
                        result.zip = area.zip;
			
                    */
                    #endregion
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
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
       
        public ReturnType UpdateArea(string id, Area area)
        {
            throw new NotImplementedException();
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Area.Where(p => p.AreaID == areaID).ToList();*/
                    var result = alading.Area.Where(p => p.id == id).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    Area ob = result.First();

                    ob.id = area.id;

                    ob.type = area.type;

                    ob.name = area.name;

                    ob.parent_id = area.parent_id;

                    ob.zip = area.zip;

                    if (alading.SaveChanges() == 1)
                    {
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

        public List<Area> GetAllAreaOrdered()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Area> list = alading.Area.ToList();
                    var ob = (from u in list orderby u.parent_id ascending select u);
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public List<Area> GetAllArea()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Area> list = alading.Area.OrderBy(p=>p.parent_id).OrderBy(p=>p.id).ToList();

                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<Area> GetArea(Func<Area, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Area> list = alading.Area.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public Area GetArea(string id)
        {
            throw new NotImplementedException();
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<Area> list = alading.Area.Where(p => p.AreaID == areaID).ToList();*/
                    List<Area> list = alading.Area.Where(p => p.id == id).ToList();
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
        
        public List<Area> GetArea(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.Area orderby u.AreaID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.Area.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Area> GetArea(Func<Area, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<Area> list = alading.Area.Where(func).OrderByDescending(a=>a.AreaID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Area> GetAreas(string parent_id)
        {
            using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            {
                var query = from s1 in alading.Area
                            where s1.parent_id == parent_id
                            select s1;

                return query.ToList();
            }
        }

        public bool AddAreaSqlBulkCopy(DataTable dataTable)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //获得连接字符串
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;

                    SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connectionString);
                    //sqlBulkCopy.DestinationTableName = alading.ItemCat.CommandText;
                    sqlBulkCopy.DestinationTableName = "A38";
                    sqlBulkCopy.BatchSize = dataTable.Rows.Count;

                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    //打开数据库连接
                    sqlConnection.Open();

                    #region 添加之前清空数据库
                    //新建一个sql命令对象
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlConnection;
                    //创建执行脚本
                    sqlCmd.CommandText = "delete A38";
                    //像存储过程一样预编译
                    sqlCmd.Prepare();
                    //执行脚本返回影响行数
                    int i = sqlCmd.ExecuteNonQuery();
                    #endregion

                    if (dataTable != null && dataTable.Rows.Count != 0)
                    {
                        sqlBulkCopy.ColumnMappings.Add("id", "A38P1");
                        sqlBulkCopy.ColumnMappings.Add("type", "A38P2");
                        sqlBulkCopy.ColumnMappings.Add("name", "A38P3");
                        sqlBulkCopy.ColumnMappings.Add("parent_id", "A38P4");
                        sqlBulkCopy.ColumnMappings.Add("zip", "A38P5");
                        sqlBulkCopy.WriteToServer(dataTable);
                    }
                    sqlBulkCopy.Close();
                    sqlConnection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

