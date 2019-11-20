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
     
        public ReturnType AddProduct(Product product)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToProduct(product);
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
                
        public ReturnType AddProduct(List<Product> productList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (Product product in productList)
                    {
                        alading.AddToProduct(product);
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
       
        public ReturnType RemoveAllProduct()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Product> list = alading.Product.ToList();
                    foreach (Product product in list)
                    {
                        alading.DeleteObject(product);
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
       
        public ReturnType RemoveProduct(Func<Product, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Product> list = alading.Product.Where(func).ToList();
                    foreach (Product product in list)
                    {
                        alading.DeleteObject(product);
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

        public List<Product> GetProduct(List<string> producidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.Product.Where(BuildWhereInExpression<Product, int>(v => v.ProductID, productIDList));*/
                    var result = alading.Product.Where(BuildWhereInExpression<Product, string>(v => v.product_id, producidList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveProduct(List<string> productidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Product.Where(BuildWhereInExpression<Product, int>(v => v.ProductID, productIDList));*/
                    var result = alading.Product.Where(BuildWhereInExpression<Product, string>(v => v.product_id, productidList));
                    foreach (Product s in result)
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


        public ReturnType RemoveProduct(string productid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<Product> list = alading.Product.Where(p => p.ProductID == productID).ToList();*/
                    List<Product> list = alading.Product.Where(p => p.product_id == productid).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        Product sy = list.First();
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
      
        public ReturnType UpdateProduct(Product product)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*Product result = alading.Product.Where(p => p.ProductID == product.ProductID).FirstOrDefault();*/
                    Product result = alading.Product.Where(p => p.product_id == product.product_id).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("Product", product);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.product_id = product.product_id;
                    
                        result.outer_id = product.outer_id;
                    
                        result.tsc = product.tsc;
                    
                        result.cid = product.cid;
                    
                        result.cat_name = product.cat_name;
                    
                        result.props = product.props;
                    
                        result.props_str = product.props_str;
                    
                        result.name = product.name;
                    
                        result.binds = product.binds;
                    
                        result.binds_str = product.binds_str;
                    
                        result.sale_props = product.sale_props;
                    
                        result.sale_props_str = product.sale_props_str;
                    
                        result.price = product.price;
                    
                        result.desc = product.desc;
                    
                        result.pic_url = product.pic_url;
                    
                        result.product_imgs = product.product_imgs;
                    
                        result.product_prop_imgs = product.product_prop_imgs;
                    
                        result.created = product.created;
                    
                        result.modified = product.modified;
			
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

        public ReturnType UpdateProduct(string productid, Product product)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Product.Where(p => p.ProductID == productID).ToList();*/
                    var result = alading.Product.Where(p => p.product_id == productid).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    Product ob = result.First();
                    ob.product_id = product.product_id;
                    ob.outer_id = product.outer_id;
                    ob.tsc = product.tsc;
                    ob.cid = product.cid;
                    ob.cat_name = product.cat_name;
                    ob.props = product.props;
                    ob.props_str = product.props_str;
                    ob.name = product.name;
                    ob.binds = product.binds;
                    ob.binds_str = product.binds_str;
                    ob.sale_props = product.sale_props;
                    ob.sale_props_str = product.sale_props_str;
                    ob.price = product.price;
                    ob.desc = product.desc;
                    ob.pic_url = product.pic_url;
                    ob.product_imgs = product.product_imgs;
                    ob.product_prop_imgs = product.product_prop_imgs;
                    ob.created = product.created;
                    ob.modified = product.modified;
                    
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
     
        public List<Product> GetAllProduct()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Product> list = alading.Product.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<Product> GetProduct(Func<Product, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Product> list = alading.Product.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public Product GetProduct(string productid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<Product> list = alading.Product.Where(p => p.ProductID == productID).ToList();*/
                    List<Product> list = alading.Product.Where(p => p.product_id == productid).ToList();
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
        
        public List<Product> GetProduct(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.Product orderby u.ProductID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.Product.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Product> GetProduct(Func<Product, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<Product> list = alading.Product.Where(func).OrderByDescending(a=>a.ProductID);
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

