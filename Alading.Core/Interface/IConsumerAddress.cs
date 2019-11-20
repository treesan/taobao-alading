using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IConsumerAddress
    {
        ReturnType AddConsumerAddress(ConsumerAddress consumeraddress);

        ReturnType AddConsumerAddress(List<ConsumerAddress> consumeraddressList);

        ReturnType RemoveAllConsumerAddress();

        ReturnType RemoveConsumerAddress(Func<ConsumerAddress, bool> func);

        ReturnType RemoveConsumerAddress(int consumeraddressID);

        ReturnType RemoveConsumerAddress(List<int> consumeraddressIDList);

        ReturnType UpdateConsumerAddress(ConsumerAddress consumeraddress);

        ReturnType UpdateConsumerAddress(int consumeraddressID, ConsumerAddress consumeraddress);

        List<ConsumerAddress> GetAllConsumerAddress();

        List<ConsumerAddress> GetConsumerAddress(Func<ConsumerAddress, bool> func);

        List<ConsumerAddress> GetConsumerAddress(List<int> consumeraddressIDList);

        List<ConsumerAddress> GetConsumerAddress(int pageIndex, int pageSize, out int rowCount);

        List<ConsumerAddress> GetConsumerAddress(Func<ConsumerAddress, bool> func, int pageIndex, int pageSize, out int rowCount);
    }
}
