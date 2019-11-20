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
     
        public ReturnType AddShipping(Shipping shipping)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToShipping(shipping);
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
                
        public ReturnType AddShipping(List<Shipping> shippingList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (Shipping shipping in shippingList)
                    {
                        alading.AddToShipping(shipping);
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
       
        public ReturnType RemoveAllShipping()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Shipping> list = alading.Shipping.ToList();
                    foreach (Shipping shipping in list)
                    {
                        alading.DeleteObject(shipping);
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
       
        public ReturnType RemoveShipping(Func<Shipping, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Shipping> list = alading.Shipping.Where(func).ToList();
                    foreach (Shipping shipping in list)
                    {
                        alading.DeleteObject(shipping);
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

        public List<Shipping> GetShipping(List<string> shippingCodeList)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.Shipping.Where(BuildWhereInExpression<Shipping, int>(v => v.ShippingID, shippingIDList));*/
            //        var result = alading.Shipping.Where(BuildWhereInExpression<Shipping, string>(v => v.ShippingCode, shippingCodeList));

            //        return result.ToList();
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    return null;
            //}
        }

        public ReturnType RemoveShipping(List<string> shippingCodeList)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.Shipping.Where(BuildWhereInExpression<Shipping, int>(v => v.ShippingID, shippingIDList));*/
            //        var result = alading.Shipping.Where(BuildWhereInExpression<Shipping, string>(v => v.ShippingCode, shippingCodeList));
            //        foreach (Shipping s in result)
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

    
        public ReturnType RemoveShipping(string shippingCode)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*List<Shipping> list = alading.Shipping.Where(p => p.ShippingID == shippingID).ToList();*/
            //        List<Shipping> list = alading.Shipping.Where(p => p.ShippingCode == shippingCode).ToList();
            //        if (list.Count == 0)
            //        {
            //            return ReturnType.NotExisted;
            //        }

            //        else
            //        {
            //            Shipping sy = list.First();
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
      
        public ReturnType UpdateShipping(Shipping shipping)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*Shipping result = alading.Shipping.Where(p => p.ShippingID == shipping.ShippingID).FirstOrDefault();*/
            //        Shipping result = alading.Shipping.Where(p => p.ShippingCode == shipping.ShippingCode).FirstOrDefault();
            //        if (result == null)
            //        {
            //            return ReturnType.NotExisted;
            //        }
            //        #region   Using Attach() Function Update,Default USE;          
            //        alading.Attach(result);
            //        alading.ApplyPropertyChanges("Shipping", shipping);
            //        #endregion
                    
            //        #region    Using All Items Replace To Update ,Default UnUse
            //        /*		
                    
            //            result.tid = shipping.tid;
                    
            //            result.seller_nick = shipping.seller_nick;
                    
            //            result.buyer_nick = shipping.buyer_nick;
                    
            //            result.delivery_start = shipping.delivery_start;
                    
            //            result.delivery_end = shipping.delivery_end;
                    
            //            result.out_sid = shipping.out_sid;
                    
            //            result.item_title = shipping.item_title;
                    
            //            result.receiver_name = shipping.receiver_name;
                    
            //            result.receiver_phone = shipping.receiver_phone;
                    
            //            result.receiver_mobile = shipping.receiver_mobile;
                    
            //            result.receiver_location_zip = shipping.receiver_location_zip;
                    
            //            result.receiver_location_address = shipping.receiver_location_address;
                    
            //            result.receiver_location_city = shipping.receiver_location_city;
                    
            //            result.receiver_location_state = shipping.receiver_location_state;
                    
            //            result.receiver_location_country = shipping.receiver_location_country;
                    
            //            result.receiver_location_district = shipping.receiver_location_district;
                    
            //            result.status = shipping.status;
                    
            //            result.type = shipping.type;
                    
            //            result.freight_payer = shipping.freight_payer;
                    
            //            result.seller_confirm = shipping.seller_confirm;
                    
            //            result.company_name = shipping.company_name;
			
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
       
        public ReturnType UpdateShipping(string shippingCode, Shipping shipping)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.Shipping.Where(p => p.ShippingID == shippingID).ToList();*/
            //        var result = alading.Shipping.Where(p => p.ShippingCode == shippingCode).ToList();
            //        if (result.Count == 0)
            //        {
            //            return ReturnType.NotExisted;
            //        }
                  
            //        Shipping ob = result.First();
            //        ob.tid = shipping.tid;
            //        ob.seller_nick = shipping.seller_nick;
            //        ob.buyer_nick = shipping.buyer_nick;
            //        ob.delivery_start = shipping.delivery_start;
            //        ob.delivery_end = shipping.delivery_end;
            //        ob.out_sid = shipping.out_sid;
            //        ob.item_title = shipping.item_title;
            //        ob.receiver_name = shipping.receiver_name;
            //        ob.receiver_phone = shipping.receiver_phone;
            //        ob.receiver_mobile = shipping.receiver_mobile;
            //        ob.receiver_location_zip = shipping.receiver_location_zip;
            //        ob.receiver_location_address = shipping.receiver_location_address;
            //        ob.receiver_location_city = shipping.receiver_location_city;
            //        ob.receiver_location_state = shipping.receiver_location_state;
            //        ob.receiver_location_country = shipping.receiver_location_country;
            //        ob.receiver_location_district = shipping.receiver_location_district;
            //        ob.status = shipping.status;
            //        ob.type = shipping.type;
            //        ob.freight_payer = shipping.freight_payer;
            //        ob.seller_confirm = shipping.seller_confirm;
            //        ob.company_name = shipping.company_name;
                    
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
     
        public List<Shipping> GetAllShipping()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Shipping> list = alading.Shipping.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<Shipping> GetShipping(Func<Shipping, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Shipping> list = alading.Shipping.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public Shipping GetShipping(string shippingCode)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*List<Shipping> list = alading.Shipping.Where(p => p.ShippingID == shippingID).ToList();*/
            //        List<Shipping> list = alading.Shipping.Where(p => p.ShippingCode == shippingCode).ToList();
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
        
        public List<Shipping> GetShipping(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.Shipping orderby u.ShippingID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.Shipping.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Shipping> GetShipping(Func<Shipping, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<Shipping> list = alading.Shipping.Where(func).OrderByDescending(a=>a.ShippingID);
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

