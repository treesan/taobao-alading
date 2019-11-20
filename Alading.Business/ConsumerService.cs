using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class ConsumerService
    {

        public static ReturnType AddConsumer(Consumer consumer)
        {
            return DataProviderClass.Instance().AddConsumer(consumer);
        }

        public static ReturnType AddConsumer(List<Consumer> consumerList)
        {
            return DataProviderClass.Instance().AddConsumer(consumerList);
        }
    
        public static ReturnType RemoveAllConsumer()
        {
            return DataProviderClass.Instance().RemoveAllConsumer();
        }
    
        public static ReturnType RemoveConsumer(Func<Consumer, bool> func)
        {
            return DataProviderClass.Instance().RemoveConsumer(func);
        }
        
        public static ReturnType RemoveConsumer(string consumerCode)
        {
            return DataProviderClass.Instance().RemoveConsumer(consumerCode);
        }       
        
        /*
        public static ReturnType RemoveConsumer(int consumerID)
        {
            return DataProviderClass.Instance().RemoveConsumer(consumerID);
        }
        */
    
        public static ReturnType RemoveConsumer(List<string> consumerCodeList)
        {
            return DataProviderClass.Instance().RemoveConsumer(consumerCodeList);
        }        
        
        /*
        public static ReturnType RemoveConsumer(List<int> consumerIDList)
        {
            return DataProviderClass.Instance().RemoveConsumer(consumerIDList);
        }
        */
    
        public static ReturnType UpdateConsumer(Consumer consumer)
        {
            return DataProviderClass.Instance().UpdateConsumer(consumer);
        }
    
        public static ReturnType UpdateConsumer(string consumerCode, Consumer consumer)
        {
            return DataProviderClass.Instance().UpdateConsumer(consumerCode, consumer);
        }
        
        /*
        public static ReturnType UpdateConsumer(int consumerID, Consumer consumer)
        {
            return DataProviderClass.Instance().UpdateConsumer(consumerID, consumer);
        }
        */
    
        public static List<Consumer> GetAllConsumer()
        {
            return DataProviderClass.Instance().GetAllConsumer();
        }
    
        public static List<Consumer> GetConsumer(Func<Consumer, bool> func)
        {
            return DataProviderClass.Instance().GetConsumer(func);
        }
    
        public static Consumer GetConsumer(string consumerCode)
        {
            return DataProviderClass.Instance().GetConsumer(consumerCode);
        }
        
        /*
        public static Consumer GetConsumer(int consumerID)
        {
            return DataProviderClass.Instance().GetConsumer(consumerID);
        }
        */
    
        public static List<Consumer> GetConsumer(List<string> consumerCodeList)
        {
            return DataProviderClass.Instance().GetConsumer(consumerCodeList);
        }
        
        /*
        public static List<Consumer> GetConsumer(List<int> consumerIDList)
        {
            return DataProviderClass.Instance().GetConsumer(consumerIDList);
        }
        */
    
        public static List<Consumer> GetConsumer(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetConsumer(pageIndex, pageSize, out rowCount);
        }
        
        public static List<Consumer> GetConsumer(Func<Consumer, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetConsumer(func, pageIndex, pageSize, out rowCount);
        }

        public static List<Consumer> GetConsumer(Func<Consumer, bool> func, string orderType, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetConsumer(func, orderType, pageIndex, pageSize, out rowCount);
        }

    }
}
