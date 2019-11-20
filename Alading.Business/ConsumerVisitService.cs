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
    public static class ConsumerVisitService
    {
        public static ReturnType AddConsumerVisitSqlBulkCopy(DataTable dataTable)
        {
            return DataProviderClass.Instance().AddConsumerVisitSqlBulkCopy(dataTable);
        }

        public static ReturnType AddConsumerVisit(ConsumerVisit consumervisit)
        {
            return DataProviderClass.Instance().AddConsumerVisit(consumervisit);
        }

        public static ReturnType AddConsumerVisit(List<ConsumerVisit> consumervisitList)
        {
            return DataProviderClass.Instance().AddConsumerVisit(consumervisitList);
        }

        public static ReturnType RemoveAllConsumerVisit()
        {
            return DataProviderClass.Instance().RemoveAllConsumerVisit();
        }

        public static ReturnType RemoveConsumerVisit(Func<ConsumerVisit, bool> func)
        {
            return DataProviderClass.Instance().RemoveConsumerVisit(func);
        }

        public static ReturnType RemoveConsumerVisit(string consumervisitCode)
        {
            return DataProviderClass.Instance().RemoveConsumerVisit(consumervisitCode);
        }

        /*
        public static ReturnType RemoveConsumerVisit(int consumervisitID)
        {
            return DataProviderClass.Instance().RemoveConsumerVisit(consumervisitID);
        }
        */

        public static ReturnType RemoveConsumerVisit(List<string> consumervisitCodeList)
        {
            return DataProviderClass.Instance().RemoveConsumerVisit(consumervisitCodeList);
        }

        /*
        public static ReturnType RemoveConsumerVisit(List<int> consumervisitIDList)
        {
            return DataProviderClass.Instance().RemoveConsumerVisit(consumervisitIDList);
        }
        */

        public static ReturnType UpdateConsumerVisit(ConsumerVisit consumervisit)
        {
            return DataProviderClass.Instance().UpdateConsumerVisit(consumervisit);
        }

        public static ReturnType UpdateConsumerVisit(string consumervisitCode, ConsumerVisit consumervisit)
        {
            return DataProviderClass.Instance().UpdateConsumerVisit(consumervisitCode, consumervisit);
        }

        /*
        public static ReturnType UpdateConsumerVisit(int consumervisitID, ConsumerVisit consumervisit)
        {
            return DataProviderClass.Instance().UpdateConsumerVisit(consumervisitID, consumervisit);
        }
        */

        public static List<ConsumerVisit> GetAllConsumerVisit()
        {
            return DataProviderClass.Instance().GetAllConsumerVisit();
        }

        public static List<ConsumerVisit> GetConsumerVisit(Func<ConsumerVisit, bool> func)
        {
            return DataProviderClass.Instance().GetConsumerVisit(func);
        }

        public static ConsumerVisit GetConsumerVisit(string consumervisitCode)
        {
            return DataProviderClass.Instance().GetConsumerVisit(consumervisitCode);
        }

        /*
        public static ConsumerVisit GetConsumerVisit(int consumervisitID)
        {
            return DataProviderClass.Instance().GetConsumerVisit(consumervisitID);
        }
        */

        public static List<ConsumerVisit> GetConsumerVisit(List<string> consumervisitCodeList)
        {
            return DataProviderClass.Instance().GetConsumerVisit(consumervisitCodeList);
        }

        /*
        public static List<ConsumerVisit> GetConsumerVisit(List<int> consumervisitIDList)
        {
            return DataProviderClass.Instance().GetConsumerVisit(consumervisitIDList);
        }
        */

        public static List<ConsumerVisit> GetConsumerVisit(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetConsumerVisit(pageIndex, pageSize, out rowCount);
        }

        public static List<ConsumerVisit> GetConsumerVisit(Func<ConsumerVisit, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetConsumerVisit(func, pageIndex, pageSize, out rowCount);
        }
    }
}
