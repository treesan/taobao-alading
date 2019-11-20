using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IConsumer
    {       
        ReturnType AddConsumer(Consumer consumer);
       
        ReturnType AddConsumer(List<Consumer> consumerList);
        
        ReturnType RemoveAllConsumer();
       
        ReturnType RemoveConsumer(Func<Consumer, bool> func);
              
        ReturnType RemoveConsumer(string consumerNick);
        
        ReturnType RemoveConsumer(List<string> consumerNickList);
       
        ReturnType UpdateConsumer(Consumer consumer);
       
        ReturnType UpdateConsumer(string consumerNick,Consumer consumer);
       
        List<Consumer> GetAllConsumer();
      
        List<Consumer> GetConsumer(Func<Consumer, bool> func);
      
        List<Consumer> GetConsumer(List<string> consumerNickList);
       
        List<Consumer> GetConsumer(int pageIndex, int pageSize, out int rowCount);
        
        List<Consumer> GetConsumer(Func<Consumer, bool> func, int pageIndex, int pageSize, out int rowCount);

        List<Consumer> GetConsumer(Func<Consumer, bool> func, string orderby, int pageIndex, int pageSize, out int rowCount);
       
        /*        
        ReturnType RemoveConsumer(int consumerID);
        
        ReturnType RemoveConsumer(List<int> consumerIDList);
        
        ReturnType UpdateConsumer(int consumerID,Consumer consumer);
        
        List<Consumer> GetConsumer(List<int> consumerIDList);
        */
    }
}
