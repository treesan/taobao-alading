using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
namespace Express_Query
{
   public struct trackNode
    {
        public string trackTime;
        public string trackStatus;
    };
  public  class ResultInfo
    {
        public string key;
        public ArrayList trackList;
        public ResultInfo(string num)
        {
            key = num;
            trackList = new ArrayList();
        }
        public void add(string time,string state)
        {
            trackNode node=new trackNode();
            node.trackTime = time;
            node.trackStatus = state;
            trackList.Add(node);
        }
        
    }
}
