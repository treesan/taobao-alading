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
     
        public ReturnType AddSupplier(Supplier supplier)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToSupplier(supplier);
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
                
        public ReturnType AddSupplier(List<Supplier> supplierList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (Supplier supplier in supplierList)
                    {
                        alading.AddToSupplier(supplier);
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
       
        public ReturnType RemoveAllSupplier()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Supplier> list = alading.Supplier.ToList();
                    foreach (Supplier supplier in list)
                    {
                        alading.DeleteObject(supplier);
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
       
        public ReturnType RemoveSupplier(Func<Supplier, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Supplier> list = alading.Supplier.Where(func).ToList();
                    foreach (Supplier supplier in list)
                    {
                        alading.DeleteObject(supplier);
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

        public List<Supplier> GetSupplier(List<string> supplierCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.Supplier.Where(BuildWhereInExpression<Supplier, int>(v => v.SupplierID, supplierIDList));*/
                    var result = alading.Supplier.Where(BuildWhereInExpression<Supplier, string>(v => v.SupplierCode, supplierCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveSupplier(List<string> supplierCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Supplier.Where(BuildWhereInExpression<Supplier, int>(v => v.SupplierID, supplierIDList));*/
                    var result = alading.Supplier.Where(BuildWhereInExpression<Supplier, string>(v => v.SupplierCode, supplierCodeList));
                    foreach (Supplier s in result)
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

    
        public ReturnType RemoveSupplier(string supplierCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<Supplier> list = alading.Supplier.Where(p => p.SupplierID == supplierID).ToList();*/
                    List<Supplier> list = alading.Supplier.Where(p => p.SupplierCode == supplierCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        Supplier sy = list.First();
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
      
        public ReturnType UpdateSupplier(Supplier supplier)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*Supplier result = alading.Supplier.Where(p => p.SupplierID == supplier.SupplierID).FirstOrDefault();*/
                    Supplier result = alading.Supplier.Where(p => p.SupplierCode == supplier.SupplierCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("Supplier", supplier);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.SupplierCode = supplier.SupplierCode;
                    
                        result.SupplierName = supplier.SupplierName;
                    
                        result.SupplierAddress = supplier.SupplierAddress;
                    
                        result.SupplierPhone = supplier.SupplierPhone;
                    
                        result.SupplierUrl = supplier.SupplierUrl;
                    
                        result.SupplierQQ = supplier.SupplierQQ;
                    
                        result.SupplierWangWang = supplier.SupplierWangWang;
                    
                        result.SupplierRemark = supplier.SupplierRemark;
			
                    */
                    #endregion  
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
       
        public ReturnType UpdateSupplier(string supplierCode, Supplier supplier)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Supplier.Where(p => p.SupplierID == supplierID).ToList();*/
                    var result = alading.Supplier.Where(p => p.SupplierCode == supplierCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    Supplier ob = result.First();
                    ob.SupplierCode = supplier.SupplierCode;
                    ob.SupplierName = supplier.SupplierName;
                    ob.SupplierAddress = supplier.SupplierAddress;
                    ob.SupplierPhone = supplier.SupplierPhone;
                    ob.SupplierUrl = supplier.SupplierUrl;
                    ob.SupplierQQ = supplier.SupplierQQ;
                    ob.SupplierWangWang = supplier.SupplierWangWang;
                    ob.SupplierRemark = supplier.SupplierRemark;
                    
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
     
        public List<Supplier> GetAllSupplier()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Supplier> list = alading.Supplier.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<Supplier> GetSupplier(Func<Supplier, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Supplier> list = alading.Supplier.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public Supplier GetSupplier(string supplierCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<Supplier> list = alading.Supplier.Where(p => p.SupplierID == supplierID).ToList();*/
                    List<Supplier> list = alading.Supplier.Where(p => p.SupplierCode == supplierCode).ToList();
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
        
        public List<Supplier> GetSupplier(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.Supplier orderby u.SupplierID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.Supplier.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Supplier> GetSupplier(Func<Supplier, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<Supplier> list = alading.Supplier.Where(func).OrderByDescending(a=>a.SupplierID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public Supplier GetSupplierN(string supplierName)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<Supplier> list = alading.Supplier.Where(p => p.SupplierID == supplierID).ToList();*/
                    List<Supplier> list = alading.Supplier.Where(p => p.SupplierName == supplierName).ToList();
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


    }
}

