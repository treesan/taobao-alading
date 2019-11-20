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
     
        public ReturnType AddPostage(Postage postage)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Postage oldPostage = alading.Postage.FirstOrDefault(p => p.postage_id == postage.postage_id);
                    if (oldPostage == null)
                    {
                        alading.AddToPostage(postage);
                    }
                    else
                    {
                        oldPostage.name = postage.name;
                        oldPostage.memo = postage.memo;
                        oldPostage.post_price = postage.post_price;
                        oldPostage.post_increase = postage.post_increase;
                        oldPostage.express_price = postage.express_price;
                        oldPostage.express_increase = postage.express_increase;
                        oldPostage.ems_price = postage.ems_price;
                        oldPostage.ems_increase = postage.ems_increase;
                        oldPostage.modified = postage.modified;
                        oldPostage.postage_modes = postage.postage_modes;
                        oldPostage.created = postage.created;
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

        public ReturnType AddPAndPM(List<Postage> postageList,List<PostageMode> postageModeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    #region Postage
                    foreach (Postage postage in postageList)
                    {
                        Postage oldPostage = alading.Postage.FirstOrDefault(p => p.postage_id == postage.postage_id);
                        if (oldPostage == null)
                        {
                            alading.AddToPostage(postage);
                        }
                        else
                        {
                            oldPostage.name = postage.name;
                            oldPostage.memo = postage.memo;
                            oldPostage.post_price = postage.post_price;
                            oldPostage.post_increase = postage.post_increase;
                            oldPostage.express_price = postage.express_price;
                            oldPostage.express_increase = postage.express_increase;
                            oldPostage.ems_price = postage.ems_price;
                            oldPostage.ems_increase = postage.ems_increase;
                            oldPostage.modified = postage.modified;
                            oldPostage.postage_modes = postage.postage_modes;
                            oldPostage.created = postage.created;
                        }
                    }

                    #endregion

                    #region PostageMode
                    foreach (PostageMode postagemode in postageModeList)
                    {
                        PostageMode oldPostageMode = alading.PostageMode.FirstOrDefault(p => p.id == postagemode.id);
                        if (oldPostageMode == null)
                        {
                            alading.AddToPostageMode(postagemode);
                        }
                        else
                        {
                            oldPostageMode.postage_id = postagemode.postage_id;
                            oldPostageMode.id = postagemode.id;
                            oldPostageMode.dests = postagemode.dests;
                            oldPostageMode.increase = postagemode.increase;
                            oldPostageMode.type = postagemode.type;
                            oldPostageMode.price = postagemode.price;
                        }
                    }
                    #endregion

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
                
        public ReturnType AddPostage(List<Postage> postageList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (Postage postage in postageList)
                    {
                        Postage oldPostage = alading.Postage.FirstOrDefault(p => p.postage_id == postage.postage_id);
                        if (oldPostage == null)
                        {
                            alading.AddToPostage(postage);
                        }
                        else
                        {
                            oldPostage.name = postage.name;
                            oldPostage.memo = postage.memo;
                            oldPostage.post_price = postage.post_price;
                            oldPostage.post_increase = postage.post_increase;
                            oldPostage.express_price = postage.express_price;
                            oldPostage.express_increase = postage.express_increase;
                            oldPostage.ems_price = postage.ems_price;
                            oldPostage.ems_increase = postage.ems_increase;
                            oldPostage.modified = postage.modified;
                            oldPostage.postage_modes = postage.postage_modes;
                            oldPostage.created = postage.created;
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
       
        public ReturnType RemoveAllPostage()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Postage> list = alading.Postage.ToList();
                    foreach (Postage postage in list)
                    {
                        alading.DeleteObject(postage);
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
       
        public ReturnType RemovePostage(Func<Postage, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Postage> list = alading.Postage.Where(func).ToList();
                    foreach (Postage postage in list)
                    {
                        alading.DeleteObject(postage);
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

        public List<Postage> GetPostage(List<string> postageCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.Postage.Where(BuildWhereInExpression<Postage, int>(v => v.PostageID, postageIDList));*/
                    var result = alading.Postage.Where(BuildWhereInExpression<Postage, string>(v => v.postage_id, postageCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemovePostage(List<string> postageCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Postage.Where(BuildWhereInExpression<Postage, int>(v => v.PostageID, postageIDList));*/
                    var result = alading.Postage.Where(BuildWhereInExpression<Postage, string>(v => v.postage_id, postageCodeList));
                    foreach (Postage s in result)
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


        public ReturnType RemovePostage(string postage_id)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<Postage> list = alading.Postage.Where(p => p.PostageID == postageID).ToList();*/
                    List<Postage> list = alading.Postage.Where(p => p.postage_id == postage_id).ToList();

                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        Postage sy = list.First();
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

        public ReturnType RemovePtAndPtM(string postage_id)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<Postage> list = alading.Postage.Where(p => p.PostageID == postageID).ToList();*/
                    List<Postage> postageList = alading.Postage.Where(p => p.postage_id == postage_id).ToList();
                    List<PostageMode> postageModeList = alading.PostageMode.Where(v => v.postage_id ==postage_id).ToList();
                    if (postageList.Count == 0 || postageModeList.Count==0)
                    {
                        return ReturnType.NotExisted;
                    }
                    else
                    {
                        Postage sy = postageList.First();
                        alading.DeleteObject(sy);
                        alading.DeleteObject(postageModeList);
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

      
        public ReturnType UpdatePostage(Postage postage)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*Postage result = alading.Postage.Where(p => p.PostageID == postage.PostageID).FirstOrDefault();*/
                    Postage result = alading.Postage.Where(p => p.postage_id == postage.postage_id).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("Postage", postage);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.name = postage.name;
                    
                        result.memo = postage.memo;
                    
                        result.created = postage.created;
                    
                        result.modified = postage.modified;
                    
                        result.post_price = postage.post_price;
                    
                        result.post_increase = postage.post_increase;
                    
                        result.express_price = postage.express_price;
                    
                        result.express_increase = postage.express_increase;
                    
                        result.ems_price = postage.ems_price;
                    
                        result.ems_increase = postage.ems_increase;
                    
                        result.postage_modes = postage.postage_modes;
			
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
       
        public ReturnType UpdatePostage(string postageCode, Postage postage)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Postage.Where(p => p.PostageID == postageID).ToList();*/
                    var result = alading.Postage.Where(p => p.postage_id == postageCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    Postage ob = result.First();
                    //ob.name = postage.name;
                    ob.memo = postage.memo;
                    ob.created = postage.created;
                    ob.modified = postage.modified;
                    ob.post_price = postage.post_price;
                    ob.post_increase = postage.post_increase;
                    ob.express_price = postage.express_price;
                    ob.express_increase = postage.express_increase;
                    ob.ems_price = postage.ems_price;
                    ob.ems_increase = postage.ems_increase;
                    ob.postage_modes = postage.postage_modes;
                    
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
     
        public List<Postage> GetAllPostage()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Postage> list = alading.Postage.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public Postage GetPostage(string postage_id)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<Postage> list = alading.Postage.Where(p => p.PostageID == postageID).ToList();*/
                    List<Postage> list = alading.Postage.Where(p => p.postage_id == postage_id).ToList();
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
      
        public List<Postage> GetPostage(Func<Postage, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Postage> list = alading.Postage.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        /*
        public Postage GetPostage(string postageCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //List<Postage> list = alading.Postage.Where(p => p.PostageID == postageID).ToList();
                    List<Postage> list = alading.Postage.Where(p => p.postage_id == postageCode).ToList();
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
        */
        public List<Postage> GetPostage(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.Postage orderby u.PostageID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.Postage.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Postage> GetPostage(Func<Postage, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<Postage> list = alading.Postage.Where(func).OrderByDescending(a=>a.PostageID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public Postage GetPostageByName(string name)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<Postage> list = alading.Postage.Where(p => p.PostageID == postageID).ToList();*/
                    List<Postage> list = alading.Postage.Where(p => p.name == name).ToList();
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

