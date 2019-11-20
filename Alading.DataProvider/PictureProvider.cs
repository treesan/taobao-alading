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
        public ReturnType AddPicture(Picture picture)
         {
            try
            {
                using(AladingEntities alading=new AladingEntities(AppSettings.GetConnectionString()))
                {
                    alading.AddToPicture(picture);
                    if (alading.SaveChanges()==1)
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
        public ReturnType AddPicture(List<Picture> pictures)
        {
            try
            {
                using (AladingEntities alading=new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach(Picture picture in pictures)
                    {
                        alading.AddToPicture(picture);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;                    
                   
                }
            }
            catch (SqlException sex)
            {
            	return ReturnType.ConnFailed;
            }
            catch (Exception sex)
            {
                return ReturnType .OthersError;
            }
        }
        public ReturnType RemoveAllPicture()
        {
            try
            {
                using (AladingEntities alading =new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Picture> list = alading.Picture.ToList();
                    foreach(Picture picture in list)
                    {
                        alading.DeleteObject(picture);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException ex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }
            
        }
        public ReturnType RemovePicture(Func<Picture, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Picture> list = alading.Picture.Where(func).ToList();
                    foreach (Picture picture in list)
                    {
                        alading.DeleteObject(picture);
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

        public ReturnType RemovePicture(string pictureCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<StockItem> list = alading.StockItem.Where(p => p.StockItemID == stockitemID).ToList();*/
                    List<Picture> list = alading.Picture.Where(p => p.PictureCode == pictureCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        Picture sy = list.First();
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

        public ReturnType RemovePicture(List<string> pictureCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockItem.Where(BuildWhereInExpression<StockItem, int>(v => v.StockItemID, stockitemIDList));*/
                    var result = alading.Picture.Where(BuildWhereInExpression<Picture, string>(v => v.PictureCode, pictureCodeList));
                    foreach (Picture s in result)
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

        public ReturnType UpdatePicture(Picture picture)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Picture result = alading.Picture.Where(p => p.PictureCode == picture.PictureCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                   
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("Picture", picture);
                   

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

        public ReturnType UpdatePicture(string outerID, Picture picture)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Picture result = alading.Picture.FirstOrDefault(p => p.OuterID == outerID);
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    result.PictureContent = picture.PictureContent; 
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

        public List<Picture> GetAllPicture()
        {
            try
            {
                using(AladingEntities alading=new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.Picture.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Picture> GetPicture(Func<Picture, bool> func)
        {
            try
            {
                using(AladingEntities alading =new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var result = alading.Picture.Where(func);
                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public Picture GetPicture(string pictureCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<StockItem> list = alading.StockItem.Where(p => p.StockItemID == stockitemID).ToList();*/
                    List<Picture> list = alading.Picture.Where(p =>p.PictureCode == pictureCode).ToList();
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

        public List<Picture> GetPicture(List<string> pictureCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockItem.Where(BuildWhereInExpression<StockItem, int>(v => v.StockItemID, stockitemIDList));*/
                    var result = alading.Picture.Where(BuildWhereInExpression<Picture, string>(v => v.PictureCode, pictureCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<Picture> GetPicture(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var ob = (from u in alading.Picture orderby u.PictureID descending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    rowCount = alading.Picture.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Picture> GetPicture(Func<Picture, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<Picture> list = alading.Picture.Where(func).OrderByDescending(a => a.PictureID);
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
