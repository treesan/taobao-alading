using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alading.Forms.Consumer
{
    public class SelectedConsumerList
    {
        private List<Alading.Entity.Consumer> consumerList
            = new List<Alading.Entity.Consumer>();

        public List<Alading.Entity.Consumer> Items
        {
            get { return consumerList; }
        }

        public void AddItem(Alading.Entity.Consumer item)
        {
            int index = consumerList.FindIndex(c => c.nick == item.nick);
            if (index >= 0)
            {
                consumerList[index] = item;
            }
            else
            {
                consumerList.Add(item);
            }
        }

        public void RemoveItem(Alading.Entity.Consumer item)
        {
            int index = consumerList.FindIndex(c => c.nick == item.nick);
            if (index >= 0)
            {
                consumerList.RemoveAt(index);
            }
        }

        public bool ContainItem(Alading.Entity.Consumer item)
        {
            return consumerList.Find(c => c.nick == item.nick) != null;
        }
    }
}
