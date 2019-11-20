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
using System.Data.Common;
using Alading.Core;
//using Alading.Entity;

namespace Alading.DataProvider
{
    public partial class DataProviderClass : IAlading
    {
        public List<View_StockItemProduct> GetView_StockItemProduct(List<string> skuOuterIDList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var result = alading.View_StockItemProduct.Where(BuildWhereInExpression<View_StockItemProduct, string>(v => v.SkuOuterID, skuOuterIDList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<View_StockItemProduct> GetView_StockItemProduct(Func<View_StockItemProduct, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_StockItemProduct> list = alading.View_StockItemProduct.Where(func).ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<View_StockItemProduct> GetView_StockItemProduct(Func<View_StockItemProduct, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IEnumerable<View_StockItemProduct> list = alading.View_StockItemProduct.Where(func).OrderBy(i => i.SkuOuterID);
                    int count = list.Count();
                    if (count / pageSize == 0)
                    {
                        rowCount = count / pageSize;
                    }
                    else
                    {
                        rowCount = count / pageSize + 1;
                    }
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(); 
                    //var q2 = from i in list
                    //        select i.SkuOuterID;
                    //var q = from i in list
                    //        select i.OuterID;//i => i.SkuQuantity >= i.LowestNum && i.SkuQuantity <= i.HighestNum

                    //List<string> list3 = alading.View_StockItemProduct.Where(func).OrderBy(i => i.SkuOuterID).Select(i=>i.SkuOuterID).ToList();

                    //List<string> list4 = alading.View_StockItemProduct.Where(i => i.SkuQuantity >= i.LowestNum && i.SkuQuantity <= i.HighestNum).OrderBy(i => i.SkuOuterID).Select(i => i.SkuOuterID).ToList();
                    
                    //IEnumerable<View_StockItemProduct> list2 = alading.View_StockItemProduct.Where(i => i.SkuQuantity >= i.LowestNum && i.SkuQuantity <= i.HighestNum).OrderBy(i => i.SkuOuterID);
                    //var q2f = from i in list2
                    //         select i.SkuOuterID;
                    //var qf = from i in list2
                    //        select i.OuterID;

                    //List<string> outerIDList = q.Distinct().ToList();
                    //rowCount = outerIDList.Count;
                    //List<string> returnList = outerIDList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    //List<View_StockItemProduct> viewList = new List<View_StockItemProduct>();
                   
                                   
                    
                }
            }
            catch (System.Exception ex)
            {
                rowCount = 0;
                return null;
            }
        }

        public List<StockItem> GetView_StockItemProduct(int funcType, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<string> outerIDList = new List<string>();
                    switch (funcType)
                    {
                        case 1: // 正常
                            outerIDList = alading.View_StockItemProduct.Where(i => i.SkuQuantity >= i.LowestNum && i.SkuQuantity <= i.HighestNum).Select(i => i.OuterID).Distinct().ToList();
                            break;
                        case 2: // 缺货
                            outerIDList = alading.View_StockItemProduct.Where(i => i.SkuQuantity <= i.OccupiedQuantity).Select(i => i.OuterID).Distinct().ToList();
                            break;
                        case 3: // 超储
                            outerIDList = alading.View_StockItemProduct.Where(i => i.SkuQuantity > i.HighestNum).Select(i => i.OuterID).Distinct().ToList();
                            break;
                        case 4: // 预警
                            outerIDList = alading.View_StockItemProduct.Where(i => i.SkuQuantity < i.WarningCount).Select(i => i.OuterID).Distinct().ToList();
                            break;
                    }

                    if (outerIDList != null && outerIDList.Count > 0)
                    {
                        List<string> returnList = outerIDList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                        rowCount = outerIDList.Count;
                        List<StockItem> List = alading.StockItem.ToList();                      
                        return List.Where(i => returnList.Contains(i.OuterID)).ToList();
                    }
                    rowCount = 0;
                    return new List<StockItem>();

                }
            }
            catch (System.Exception ex)
            {
                rowCount = 0;
                return new List<StockItem>();
            }
        }

        public List<View_StockItemUnit> GetView_StockItemProducts(int funcType, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<string> outerIDList = new List<string>();
                    switch (funcType)
                    {
                        case 1: // 正常
                            outerIDList = alading.View_StockItemProduct.Where(i => i.IsUsingWarn == false || (i.IsUsingWarn == true && i.SkuQuantity >= i.LowestNum && i.SkuQuantity <= i.HighestNum)).Select(i => i.OuterID).Distinct().ToList();
                            break;
                        case 2: // 缺货 i =>i.IsUsingWarn == false || (i.IsUsingWarn == true && i.SkuQuantity <= i.OccupiedQuantity)
                            outerIDList = alading.View_StockItemProduct.Where(i=> i.SkuQuantity <= i.OccupiedQuantity).Select(i => i.OuterID).Distinct().ToList();
                            break;
                        case 3: // 超储 i =>i.IsUsingWarn == false || (i.IsUsingWarn == true && i.SkuQuantity > i.HighestNum)
                            outerIDList = alading.View_StockItemProduct.Where(i =>i.IsUsingWarn == true && i.SkuQuantity > i.HighestNum).Select(i => i.OuterID).Distinct().ToList();
                            break;
                        case 4: // 预警 i =>i.IsUsingWarn == false || (i.IsUsingWarn == true && i.SkuQuantity < i.WarningCount)
                            outerIDList = alading.View_StockItemProduct.Where(i =>i.IsUsingWarn == true && i.SkuQuantity < i.WarningCount).Select(i => i.OuterID).Distinct().ToList();
                            break;
                    }

                    if (outerIDList != null && outerIDList.Count > 0)
                    {
                        List<string> returnList = outerIDList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                        rowCount = outerIDList.Count;
                        List<View_StockItemUnit> List = alading.View_StockItemUnit.ToList();
                        return List.Where(i => returnList.Contains(i.OuterID)).ToList();
                    }
                    rowCount = 0;
                    return new List<View_StockItemUnit>();

                }
            }
            catch (System.Exception ex)
            {
                rowCount = 0;
                return new List<View_StockItemUnit>();
            }
        }

        public List<View_StockItemProduct> GetView_StockItemProduct(string StockCid,int currentIndex,int dataPerPage,ref int allIndex)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    int result = 0;
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;
                    //选取执行存储过程
                    SqlCommand cmd = new SqlCommand("SP_CountView_StockItemProduct",
                        new SqlConnection(connectionString));

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StockCid", StockCid);
                    SqlParameter returnValue =
                        cmd.Parameters.AddWithValue("@Count", result);
                    returnValue.Direction = ParameterDirection.Output;

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    
                    result = (int)returnValue.Value;
                    int mod = result % dataPerPage;
                    if (mod == 0)
                    {
                        allIndex = result / dataPerPage;
                    }
                    else
                    {
                        allIndex = result / dataPerPage+1;
                    }
                    IEnumerable<View_StockItemProduct> list = alading.SP_GetView_StockItemProduct(StockCid,currentIndex,dataPerPage);                   
                    return list.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<View_StockProductHouse> GetView_StockProductHouse(string StockCid, string HouseCode, int currentIndex, int dataPerPage, ref int allIndex)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    int result = 0;
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;
                    //选取执行存储过程
                    SqlCommand cmd = new SqlCommand("SP_CountView_StockProductHouse",
                        new SqlConnection(connectionString));

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StockCid", StockCid);
                    cmd.Parameters.AddWithValue("@HouseCode", HouseCode);
                    SqlParameter returnValue =
                        cmd.Parameters.AddWithValue("@Count", result);
                    returnValue.Direction = ParameterDirection.Output;

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    result = (int)returnValue.Value;
                    int mod = result % dataPerPage;
                    if (mod == 0)
                    {
                        allIndex = result / dataPerPage;
                    }
                    else
                    {
                        allIndex = result / dataPerPage + 1;
                    }
                    IEnumerable<View_StockProductHouse> list = alading.SP_GetView_StockProductHouse(StockCid,HouseCode, currentIndex, dataPerPage);
                    return list.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<View_StockItemProduct> GetView_StockItemProductByOuterId(string outer_id)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_StockItemProduct> list = alading.View_StockItemProduct.Where(v => v.OuterID == outer_id).ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public View_StockItemProduct GetView_StockItemProductBySkuOuterId(string skuOuterID)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_StockItemProduct> list = alading.SP_GetView_StockItemProductBySkuOuterID(skuOuterID).ToList();
                    if (list.Count > 0)
                    {
                        return list.First();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<View_StockItemProduct> GetView_StockItemProductByType(int stockItemType)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_StockItemProduct> list = alading.SP_GetView_StockItemProductByType(stockItemType).ToList();
                    if (list.Count > 0)
                    {
                        return list.ToList();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }



        //public int SummitShippingCode(string stockCid, ref int result)
        //{
        //    try
        //    {
        //        using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
        //        {
        //            string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;
        //            //选取执行存储过程
        //            SqlCommand cmd = new SqlCommand("SP_GetView_StockItemProduct",
        //                new SqlConnection(connectionString));

        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@StockCid", stockCid);
        //            //传入输出参数 获得返回值
        //            SqlParameter returnValue =
        //                cmd.Parameters.AddWithValue("@returnValue", result);
        //            returnValue.Direction = ParameterDirection.Output;

        //            cmd.Connection.Open();
        //            cmd.ExecuteNonQuery();
        //            cmd.Connection.Close();

        //            result = (int)returnValue.Value;
        //            return cmd.Parameters["@ReturnValue"].Value as List<View_StockItemProduct>;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public IEnumerable<View_StockItemProduct> GetAllView_StockItemProduct()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.View_StockItemProduct;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<View_StockItemProduct> GetView_StockItemProductItem(string outer_id)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.SP_GetView_StockItemProductItem(outer_id).ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        
    }
}
