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
using Alading.Core;
namespace Alading.DataProvider
{
   
    public partial class DataProviderClass : IAlading
    {
        public View_HouseLayout GetViewHouseLayout(string layoutCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.View_HouseLayout.FirstOrDefault(i => i.StockLayoutCode == layoutCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        public ReturnType AddStockLayout(StockLayout stocklayout)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    StockLayout result = alading.StockLayout.FirstOrDefault(i=>i.StockLayoutCode==stocklayout.StockLayoutCode);
                    if (result != null)
                    {
                        return ReturnType.PropertyExisted;
                    }
                    alading.AddToStockLayout(stocklayout);
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
                
        public ReturnType AddStockLayout(List<StockLayout> stocklayoutList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (StockLayout stocklayout in stocklayoutList)
                    {
                        alading.AddToStockLayout(stocklayout);
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
       
        public ReturnType RemoveAllStockLayout()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockLayout> list = alading.StockLayout.ToList();
                    foreach (StockLayout stocklayout in list)
                    {
                        alading.DeleteObject(stocklayout);
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
       
        public ReturnType RemoveStockLayout(Func<StockLayout, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockLayout> list = alading.StockLayout.Where(func).ToList();
                    foreach (StockLayout stocklayout in list)
                    {
                        alading.DeleteObject(stocklayout);
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

        public List<StockLayout> GetStockLayout(List<string> stocklayoutCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.StockLayout.Where(BuildWhereInExpression<StockLayout, int>(v => v.StockLayoutID, stocklayoutIDList));*/
                    var result = alading.StockLayout.Where(BuildWhereInExpression<StockLayout, string>(v => v.StockLayoutCode, stocklayoutCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveStockLayout(List<string> stocklayoutCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockLayout.Where(BuildWhereInExpression<StockLayout, int>(v => v.StockLayoutID, stocklayoutIDList));*/
                    var result = alading.StockLayout.Where(BuildWhereInExpression<StockLayout, string>(v => v.StockLayoutCode, stocklayoutCodeList));
                    foreach (StockLayout s in result)
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

    
        public ReturnType RemoveStockLayout(string stocklayoutCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<StockLayout> list = alading.StockLayout.Where(p => p.StockLayoutID == stocklayoutID).ToList();*/
                    List<StockLayout> list = alading.StockLayout.Where(p => p.StockLayoutCode == stocklayoutCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        StockLayout sy = list.First();
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

        public ReturnType UpdateStockLayout(StockLayout stocklayout)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*StockLayout result = alading.StockLayout.Where(p => p.StockLayoutID == stocklayout.StockLayoutID).FirstOrDefault();*/
                    StockLayout result = alading.StockLayout.FirstOrDefault(p => p.StockLayoutCode == stocklayout.StockLayoutCode);
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }

                    result.LayoutName = stocklayout.LayoutName;

                    result.LayoutRemark = stocklayout.LayoutRemark;

                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }

                    return ReturnType.OthersError;
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
       
        public ReturnType UpdateStockLayout(string stocklayoutCode, StockLayout stocklayout)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.StockLayout.Where(p => p.StockLayoutID == stocklayoutID).ToList();*/
            //        var result = alading.StockLayout.Where(p => p.StockLayoutCode == stocklayoutCode).ToList();
            //        if (result.Count == 0)
            //        {
            //            return ReturnType.NotExisted;
            //        }
                  
            //        StockLayout ob = result.First();
            //        ob.HouseCode = stocklayout.HouseCode;
            //        ob.LayoutName = stocklayout.LayoutName;
            //        ob.LayoutRemark = stocklayout.LayoutRemark;
                    
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
     
        public List<StockLayout> GetAllStockLayout()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockLayout> list = alading.StockLayout.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<StockLayout> GetStockLayout(Func<StockLayout, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockLayout> list = alading.StockLayout.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public StockLayout GetStockLayout(string stocklayoutCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<StockLayout> list = alading.StockLayout.Where(p => p.StockLayoutID == stocklayoutID).ToList();*/
                    List<StockLayout> list = alading.StockLayout.Where(p => p.StockLayoutCode == stocklayoutCode).ToList();
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
        
        public List<StockLayout> GetStockLayout(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.StockLayout orderby u.StockLayoutID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.StockLayout.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<StockLayout> GetStockLayout(Func<StockLayout, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<StockLayout> list = alading.StockLayout.Where(func).OrderByDescending(a=>a.StockLayoutID);
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

