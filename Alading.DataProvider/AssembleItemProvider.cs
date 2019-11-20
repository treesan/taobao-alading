using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core;
namespace Alading.DataProvider
{
    public partial class DataProviderClass
    {
        public List<View_AssembleProduct> GetView_AssembleProduct(Func<View_AssembleProduct, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_AssembleProduct> list = alading.View_AssembleProduct.Where(func).ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

    }
}