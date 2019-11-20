using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;
using System.Data;

namespace Alading.Core.Interface
{
    public interface IConsumerVisit
    {
        ReturnType AddConsumerVisitSqlBulkCopy(DataTable dataTable);

        ReturnType AddConsumerVisit(ConsumerVisit consumervisit);

        ReturnType AddConsumerVisit(List<ConsumerVisit> consumervisitList);

        ReturnType RemoveAllConsumerVisit();

        ReturnType RemoveConsumerVisit(Func<ConsumerVisit, bool> func);

        ReturnType RemoveConsumerVisit(string consumervisitCode);

        ReturnType RemoveConsumerVisit(List<string> consumervisitCodeList);

        ReturnType UpdateConsumerVisit(ConsumerVisit consumervisit);

        ReturnType UpdateConsumerVisit(string consumervisitCode, ConsumerVisit consumervisit);

        List<ConsumerVisit> GetAllConsumerVisit();

        List<ConsumerVisit> GetConsumerVisit(Func<ConsumerVisit, bool> func);

        List<ConsumerVisit> GetConsumerVisit(List<string> consumervisitCodeList);

        List<ConsumerVisit> GetConsumerVisit(int pageIndex, int pageSize, out int rowCount);

        List<ConsumerVisit> GetConsumerVisit(Func<ConsumerVisit, bool> func, int pageIndex, int pageSize, out int rowCount);
    }
}
