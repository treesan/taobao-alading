using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alading.Web.Entity;

namespace Alading.Web.Bussiness
{
    public class UpdateService
    {
        public static Alading.Web.Entity.Version GetNewVersion(string versionType)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                var result = context.Version.Where(c=>c.VersionType==versionType).OrderBy(c=>c.VersionID).ToList();
                if (result.Count() > 0)
                {
                    return result[result.Count()-1];
                }
                else
                {
                    return null;
                }
            }
        }

        public static List<Alading.Web.Entity.FileUpdate> GetFileUpdateList(string versionCode)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                var result = context.FileUpdate.Where(c=>c.VersionCode==versionCode).ToList();                
                return result;                
            }
        }
    }
}