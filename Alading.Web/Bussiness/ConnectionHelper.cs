using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alading.Web.Bussiness
{
    public class ConnectionHelper
    {
        private static string constr = null;

        public static string ConnectionString
        {
            get
            {
                if (constr == null)
                {
                    constr = System.Configuration.ConfigurationManager.ConnectionStrings["AladingWebEntities"].ConnectionString;
                    if (constr != null) constr = constr.Replace("&quot;", "\"");
                }
                return constr;
            }
        }
    }
}
