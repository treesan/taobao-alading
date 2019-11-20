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
        public List<View_ItemPropValue> GetView_ItemPropValueList(string cid, string parentPid, string parentVid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ItemPropValue> list = alading.SP_GetView_ItemPropValue(cid, parentPid, parentVid).ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<View_ItemPropValue> GetView_ItemPropValue(List<string> cidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.View_ItemPropValue.Where(BuildWhereInExpression<View_ItemPropValue, int>(v => v.View_ItemPropValueID, view_itempropvalueIDList));*/
                    var result = alading.View_ItemPropValue.Where(BuildWhereInExpression<View_ItemPropValue, string>(v => v.cid, cidList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }        
     
        public List<View_ItemPropValue> GetAllView_ItemPropValue()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
                    List<View_ItemPropValue> list = alading.View_ItemPropValue.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<View_ItemPropValue> GetView_ItemPropValue(Func<View_ItemPropValue, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ItemPropValue> list = alading.View_ItemPropValue.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_ItemPropValue> GetView_ItemPropValue(string cid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ItemPropValue> list = alading.View_ItemPropValue.Where(vv=>vv.cid==cid).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public View_ItemPropValue GetView_ItemPropValue(string cid, string pid, string vid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ItemPropValue> list = alading.View_ItemPropValue.Where(p => p.cid == cid && p.vid == vid && p.pid == pid).ToList();
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
        
        public List<View_ItemPropValue> GetView_ItemPropValue(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    var ob = (from u in alading.View_ItemPropValue orderby u.ItemPropValueID descending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    rowCount = alading.View_ItemPropValue.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<View_ItemPropValue> GetView_ItemPropValue(Func<View_ItemPropValue, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<View_ItemPropValue> list = alading.View_ItemPropValue.Where(func).OrderByDescending(a=>a.ItemPropValueID);
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

