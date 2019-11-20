using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alading.Forms.Init
{
    public class EntityState
    {
        private Dictionary<string, string> stateDic
            = new Dictionary<string, string>();

        private Dictionary<int, string> keyDic
            = new Dictionary<int, string>();

        public Dictionary<string, string> StateValues { get { return stateDic; } }

        public bool State { get { return stateDic.Count == 0; } }

        public void AddMessage(string property, string errorMessage)
        {
            if (stateDic.ContainsKey(property))
            {
                stateDic[property] = errorMessage;
            }
            else
            {
                keyDic.Add(stateDic.Count, property);
                stateDic.Add(property, errorMessage);                
            }
        }

        public string GetMessage(int index)
        {
            return stateDic[keyDic[index]];
        }
    }
}
