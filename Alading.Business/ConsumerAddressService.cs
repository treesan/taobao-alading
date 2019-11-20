using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class ConsumerAddressService
    {
        public static ReturnType AddConsumerAddress(ConsumerAddress consumeraddress)
        {
            return DataProviderClass.Instance().AddConsumerAddress(consumeraddress);
        }

        public static ReturnType AddConsumerAddress(List<ConsumerAddress> consumeraddressList)
        {
            return DataProviderClass.Instance().AddConsumerAddress(consumeraddressList);
        }

        public static ReturnType RemoveAllConsumerAddress()
        {
            return DataProviderClass.Instance().RemoveAllConsumerAddress();
        }

        public static ReturnType RemoveConsumerAddress(Func<ConsumerAddress, bool> func)
        {
            return DataProviderClass.Instance().RemoveConsumerAddress(func);
        }

        //public static ReturnType RemoveConsumerAddress(string consumeraddressCode)
        //{
        //    return DataProviderClass.Instance().RemoveConsumerAddress(consumeraddressCode);
        //}

        public static ReturnType RemoveConsumerAddress(int consumeraddressID)
        {
            return DataProviderClass.Instance().RemoveConsumerAddress(consumeraddressID);
        }

        //public static ReturnType RemoveConsumerAddress(List<string> consumeraddressCodeList)
        //{
        //    return DataProviderClass.Instance().RemoveConsumerAddress(consumeraddressCodeList);
        //}

        public static ReturnType RemoveConsumerAddress(List<int> consumeraddressIDList)
        {
            return DataProviderClass.Instance().RemoveConsumerAddress(consumeraddressIDList);
        }

        public static ReturnType UpdateConsumerAddress(ConsumerAddress consumeraddress)
        {
            return DataProviderClass.Instance().UpdateConsumerAddress(consumeraddress);
        }

        //public static ReturnType UpdateConsumerAddress(string consumeraddressCode, ConsumerAddress consumeraddress)
        //{
        //    return DataProviderClass.Instance().UpdateConsumerAddress(consumeraddressCode, consumeraddress);
        //}

        public static ReturnType UpdateConsumerAddress(int consumeraddressID, ConsumerAddress consumeraddress)
        {
            return DataProviderClass.Instance().UpdateConsumerAddress(consumeraddressID, consumeraddress);
        }

        public static List<ConsumerAddress> GetAllConsumerAddress()
        {
            return DataProviderClass.Instance().GetAllConsumerAddress();
        }

        public static List<ConsumerAddress> GetConsumerAddress(Func<ConsumerAddress, bool> func)
        {
            return DataProviderClass.Instance().GetConsumerAddress(func);
        }

        //public static ConsumerAddress GetConsumerAddress(string consumeraddressCode)
        //{
        //    return DataProviderClass.Instance().GetConsumerAddress(consumeraddressCode);
        //}

        public static ConsumerAddress GetConsumerAddress(int consumeraddressID)
        {
            return DataProviderClass.Instance().GetConsumerAddress(consumeraddressID);
        }

        //public static List<ConsumerAddress> GetConsumerAddress(List<string> consumeraddressCodeList)
        //{
        //    return DataProviderClass.Instance().GetConsumerAddress(consumeraddressCodeList);
        //}

        public static List<ConsumerAddress> GetConsumerAddress(List<int> consumeraddressIDList)
        {
            return DataProviderClass.Instance().GetConsumerAddress(consumeraddressIDList);
        }

        public static List<ConsumerAddress> GetConsumerAddress(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetConsumerAddress(pageIndex, pageSize, out rowCount);
        }

        public static List<ConsumerAddress> GetConsumerAddress(Func<ConsumerAddress, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetConsumerAddress(func, pageIndex, pageSize, out rowCount);
        }
    }
}
