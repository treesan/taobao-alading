using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;
using System.Data;

namespace Alading.Business
{
    public static class AreaService
    {
        /// <summary>
        /// 批量添加Area，添加之前执行清空Area
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static bool AddAreaSqlBulkCopy(DataTable dataTable)
        {
            return DataProviderClass.Instance().AddAreaSqlBulkCopy(dataTable);
        }

        public static ReturnType AddArea(Area area)
        {
            return DataProviderClass.Instance().AddArea(area);
        }

        public static ReturnType AddArea(List<Area> areaList)
        {
            return DataProviderClass.Instance().AddArea(areaList);
        }
    
        public static ReturnType RemoveAllArea()
        {
            return DataProviderClass.Instance().RemoveAllArea();
        }
    
        public static ReturnType RemoveArea(Func<Area, bool> func)
        {
            return DataProviderClass.Instance().RemoveArea(func);
        }

        public static ReturnType RemoveArea(string id)
        {
            return DataProviderClass.Instance().RemoveArea(id);
        }       
        
        /*
        public static ReturnType RemoveArea(int areaID)
        {
            return DataProviderClass.Instance().RemoveArea(areaID);
        }
        */

        public static ReturnType RemoveArea(List<string> idList)
        {
            return DataProviderClass.Instance().RemoveArea(idList);
        }        
        
        /*
        public static ReturnType RemoveArea(List<int> areaIDList)
        {
            return DataProviderClass.Instance().RemoveArea(areaIDList);
        }
        */
    
        public static ReturnType UpdateArea(Area area)
        {
            return DataProviderClass.Instance().UpdateArea(area);
        }

        public static ReturnType UpdateArea(string id, Area area)
        {
            return DataProviderClass.Instance().UpdateArea(id, area);
        }
        
        /*
        public static ReturnType UpdateArea(int areaID, Area area)
        {
            return DataProviderClass.Instance().UpdateArea(areaID, area);
        }
        */

        public static List<Area> GetAllAreaOrdered()
        {
            return DataProviderClass.Instance().GetAllAreaOrdered();
        }
    
        public static List<Area> GetAllArea()
        {
            return DataProviderClass.Instance().GetAllArea();
        }
    
        public static List<Area> GetArea(Func<Area, bool> func)
        {
            return DataProviderClass.Instance().GetArea(func);
        }
    
        public static Area GetArea(string id)
        {
            return DataProviderClass.Instance().GetArea(id);
        }
        
        /*
        public static Area GetArea(int areaID)
        {
            return DataProviderClass.Instance().GetArea(areaID);
        }
        */

        public static List<Area> GetArea(List<string> idList)
        {
            return DataProviderClass.Instance().GetArea(idList);
        }
        
        /*
        public static List<Area> GetArea(List<int> areaIDList)
        {
            return DataProviderClass.Instance().GetArea(areaIDList);
        }
        */
    
        public static List<Area> GetArea(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetArea(pageIndex, pageSize, out rowCount);
        }
        
        public static List<Area> GetArea(Func<Area, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetArea(func, pageIndex, pageSize, out rowCount);
        }

        public static List<Area> GetAreas(string parent_id)
        {
            return DataProviderClass.Instance().GetAreas(parent_id);
        }
    }
}
