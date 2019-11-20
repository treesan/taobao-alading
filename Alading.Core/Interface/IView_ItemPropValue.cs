using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IView_ItemPropValue
    {       
        List<View_ItemPropValue> GetAllView_ItemPropValue();

        List<View_ItemPropValue> GetView_ItemPropValueList(string cid, string parentPid, string parentVid);

        View_ItemPropValue GetView_ItemPropValue(string cid, string pid, string vid);
      
        List<View_ItemPropValue> GetView_ItemPropValue(Func<View_ItemPropValue, bool> func);

        List<View_ItemPropValue> GetView_ItemPropValue(string cid);

        List<View_ItemPropValue> GetView_ItemPropValue(List<string> cidList);
       
        List<View_ItemPropValue> GetView_ItemPropValue(int pageIndex, int pageSize, out int rowCount);
        
        List<View_ItemPropValue> GetView_ItemPropValue(Func<View_ItemPropValue, bool> func, int pageIndex, int pageSize, out int rowCount);
        
        /*        
        ReturnType RemoveView_ItemPropValue(int view_itempropvalueID);
        
        ReturnType RemoveView_ItemPropValue(List<int> view_itempropvalueIDList);
        
        ReturnType UpdateView_ItemPropValue(int view_itempropvalueID,View_ItemPropValue view_itempropvalue);
        
        List<View_ItemPropValue> GetView_ItemPropValue(List<int> view_itempropvalueIDList);
        */
    }
}
