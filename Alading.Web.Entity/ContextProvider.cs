using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alading.Web.Entity
{
    public class ContextProvider
    {
        //private static readonly string ConnectionString
            //= "metadata=res://*/AladingWebModel.csdl|res://*/AladingWebModel.ssdl|res://*/AladingWebModel.msl;provider=System.Data.SqlClient;provider connection string=\"Data Source=MSTC;Initial Catalog=AladingWeb;Persist Security Info=True;User ID=sa;Password=123456;MultipleActiveResultSets=True\"";

        public static AladingWebEntities DataContext(string connectionString)
        {
            return new AladingWebEntities(connectionString);
        }
    }
}
