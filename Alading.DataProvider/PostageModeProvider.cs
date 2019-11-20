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
     
        public ReturnType AddPostageMode(PostageMode postagemode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
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
                
        public ReturnType AddPostageMode(List<PostageMode> postagemodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (PostageMode postagemode in postagemodeList)
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
       
        public ReturnType RemoveAllPostageMode()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<PostageMode> list = alading.PostageMode.ToList();
                    foreach (PostageMode postagemode in list)
                    {
                        alading.DeleteObject(postagemode);
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
       
        public ReturnType RemovePostageMode(Func<PostageMode, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<PostageMode> list = alading.PostageMode.Where(func).ToList();
                    foreach (PostageMode postagemode in list)
                    {
                        alading.DeleteObject(postagemode);
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

        public List<PostageMode> GetPostageMode(List<string> postageidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.PostageMode.Where(BuildWhereInExpression<PostageMode, int>(v => v.PostageModeID, postagemodeIDList));*/
                    var result = alading.PostageMode.Where(BuildWhereInExpression<PostageMode, string>(v =>v.postage_id, postageidList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemovePostageMode(List<string> postageidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.PostageMode.Where(BuildWhereInExpression<PostageMode, int>(v => v.PostageModeID, postagemodeIDList));*/
                    var result = alading.PostageMode.Where(BuildWhereInExpression<PostageMode, string>(v => v.postage_id, postageidList));
                    foreach (PostageMode s in result)
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

    
        public ReturnType RemovePostageMode(string postageid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<PostageMode> list = alading.PostageMode.Where(p => p.PostageModeID == postagemodeID).ToList();*/
                    List<PostageMode> list = alading.PostageMode.Where(p => p.postage_id == postageid).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                    else
                    {
                        foreach (PostageMode postagemode in list)
                        {
                            alading.DeleteObject(postagemode);
                        }
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
      
        public ReturnType UpdatePostageMode(PostageMode postagemode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*PostageMode result = alading.PostageMode.Where(p => p.PostageModeID == postagemode.PostageModeID).FirstOrDefault();*/
                    PostageMode result = alading.PostageMode.Where(p => p.postage_id == postagemode.postage_id).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("PostageMode", postagemode);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.postage_id = postagemode.postage_id;
                    
                        result.id = postagemode.id;
                    
                        result.type = postagemode.type;
                    
                        result.dests = postagemode.dests;
                    
                        result.price = postagemode.price;
                    
                        result.increase = postagemode.increase;
			
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
       
        public ReturnType UpdatePostageMode(string postageid, PostageMode postagemode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.PostageMode.Where(p => p.PostageModeID == postagemodeID).ToList();*/
                    var result = alading.PostageMode.Where(p => p.postage_id == postageid).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    PostageMode ob = result.First();
                    ob.postage_id = postagemode.postage_id;
                    ob.id = postagemode.id;
                    ob.type = postagemode.type;
                    ob.dests = postagemode.dests;
                    ob.price = postagemode.price;
                    ob.increase = postagemode.increase;
                    
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
     
        public List<PostageMode> GetAllPostageMode()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<PostageMode> list = alading.PostageMode.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<PostageMode> GetPostageMode(Func<PostageMode, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<PostageMode> list = alading.PostageMode.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public List<PostageMode> GetPostageMode(string postageid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<PostageMode> list = alading.PostageMode.Where(p => p.PostageModeID == postagemodeID).ToList();*/
                    List<PostageMode> list = alading.PostageMode.Where(p => p.postage_id == postageid).ToList();
                    if (list.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return list;
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<PostageMode> GetPostageMode(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.PostageMode orderby u.PostageModeID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.PostageMode.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<PostageMode> GetPostageMode(Func<PostageMode, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<PostageMode> list = alading.PostageMode.Where(func).OrderByDescending(a=>a.PostageModeID);
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

