using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IArea
    {       
        ReturnType AddArea(Area area);
       
        ReturnType AddArea(List<Area> areaList);
        
        ReturnType RemoveAllArea();
       
        ReturnType RemoveArea(Func<Area, bool> func);

        ReturnType RemoveArea(string id);
        
        ReturnType RemoveArea(List<string> idList);
       
        ReturnType UpdateArea(Area area);
       
        ReturnType UpdateArea(string id,Area area);
       
        List<Area> GetAllArea();
      
        List<Area> GetArea(Func<Area, bool> func);
      
        List<Area> GetArea(List<string> idList);
       
        List<Area> GetArea(int pageIndex, int pageSize, out int rowCount);
        
        List<Area> GetArea(Func<Area, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveArea(int areaID);
        
        ReturnType RemoveArea(List<int> areaIDList);
        
        ReturnType UpdateArea(int areaID,Area area);
        
        List<Area> GetArea(List<int> areaIDList);
        */

        List<Area> GetAreas(string parent_id);
    }
}
