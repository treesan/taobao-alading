using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alading.Forms.Consumer
{
    public abstract class LoadConsumerStrategy
    {
        protected int row_count = 0;

        public int PageIndex { get; set; } //注意PageIndex从1开始
        public int PageSize { get; set; }
        public int RowCount { get { return row_count; } }

        public int PageCount
        {
            get
            {
                int size = 1;
                if (PageSize != 0) size = PageSize;
                return Math.Max((RowCount + size - 1) / size, 1);
            }
        }

        public abstract List<Alading.Entity.Consumer> LoadConsumers();
        public abstract List<Alading.Entity.Consumer> LoadAllConsumers();
    }

    public class SearchConsumer : LoadConsumerStrategy
    {
        public string Keyword { get; set; }
        public string SortType { get; set; }

        /// <summary>
        /// 根据关键字查询用户
        /// </summary>
        /// <returns></returns>
        public override List<Alading.Entity.Consumer> LoadConsumers()
        {
            return Alading.Business.ConsumerService.GetConsumer(
                (c => c.nick.Contains(Keyword) ||
                    c.buyer_name.Contains(Keyword) ||
                    c.location_country.Contains(Keyword) ||
                    c.location_state.Contains(Keyword) ||
                    c.location_city.Contains(Keyword) ||
                    c.location_district.Contains(Keyword) ||
                    c.location_address.Contains(Keyword)),
                SortType, PageIndex, PageSize, out row_count);
        }

        /// <summary>
        /// 根据关键字查询用户，不分页
        /// </summary>
        /// <returns></returns>
        public override List<Alading.Entity.Consumer> LoadAllConsumers()
        {
            return Alading.Business.ConsumerService.GetConsumer(
                c => c.nick.Contains(Keyword) ||
                   c.buyer_name.Contains(Keyword) ||
                    c.location_country.Contains(Keyword) ||
                    c.location_state.Contains(Keyword) ||
                    c.location_city.Contains(Keyword) ||
                    c.location_district.Contains(Keyword) ||
                    c.location_address.Contains(Keyword));
        }
    }

    public class LoadAllConsumer : LoadConsumerStrategy
    {
        /// <summary>
        /// 加载所有客户
        /// </summary>
        /// <returns></returns>
        public override List<Alading.Entity.Consumer> LoadConsumers()
        {
            return Alading.Business.ConsumerService.GetConsumer(PageIndex, PageSize, out row_count);
        }

        /// <summary>
        /// 加载所有客户，不分页
        /// </summary>
        /// <returns></returns>
        public override List<Alading.Entity.Consumer> LoadAllConsumers()
        {
            return Alading.Business.ConsumerService.GetAllConsumer();
        }
    }

    public class LoadTradedConsumer : LoadConsumerStrategy
    {
        public DateTime After { get; set; }
        public DateTime Before { get; set; }

        /// <summary>
        /// 根据交易时间加载客户
        /// </summary>
        /// <returns></returns>
        public override List<Alading.Entity.Consumer> LoadConsumers()
        {
            return Alading.Business.ConsumerService.GetConsumer((c => c.last_trade >= After && c.last_trade <= Before), PageIndex, PageSize, out row_count);
        }

        /// <summary>
        /// 根据交易时间加载客户，不分页
        /// </summary>
        /// <returns></returns>
        public override List<Alading.Entity.Consumer> LoadAllConsumers()
        {
            return Alading.Business.ConsumerService.GetConsumer(c => c.last_trade >= After && c.last_trade <= Before);
        }
    }    

    public class LoadTypedConsumer : LoadConsumerStrategy
    {
        public int Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override List<Alading.Entity.Consumer> LoadConsumers()
        {
            switch (Type)
            {
                //加载普通客户
                case 1: return Alading.Business.ConsumerService.GetConsumer((c => c.vip == false), PageIndex, PageSize, out row_count);
                
                //加载重要客户
                case 2: return Alading.Business.ConsumerService.GetConsumer((c => c.vip == true), PageIndex, PageSize, out row_count);
                
                //加载经销商客户
                case 3: return Alading.Business.ConsumerService.GetConsumer((c => c.is_dealer == true), PageIndex, PageSize, out row_count);
                
                default: return new List<Alading.Entity.Consumer>();
            }
        }

        /// <summary>
        /// 根据类型加载客户，不分页
        /// </summary>
        /// <returns></returns>
        public override List<Alading.Entity.Consumer> LoadAllConsumers()
        {
            
            switch (Type)
            {
                //加载普通客户
                case 1: return Alading.Business.ConsumerService.GetConsumer(c => c.vip == false);

                //加载重要客户
                case 2: return Alading.Business.ConsumerService.GetConsumer(c => c.vip == true);

                //加载经销商客户
                case 3: return Alading.Business.ConsumerService.GetConsumer(c => c.is_dealer == true);

                default: return new List<Alading.Entity.Consumer>();
            }
        }
    }    

    public class LoadSourcedConsumer : LoadConsumerStrategy
    {
        public int Source { get; set; }

        /// <summary>
        /// 根据来源加载客户
        /// </summary>
        /// <returns></returns>
        public override List<Alading.Entity.Consumer> LoadConsumers()
        {
            return Alading.Business.ConsumerService.GetConsumer((c => c.source == Source), PageIndex, PageSize, out row_count);
        }

        /// <summary>
        /// 根据来源加载客户，不分页
        /// </summary>
        /// <returns></returns>
        public override List<Alading.Entity.Consumer> LoadAllConsumers()
        {
            return Alading.Business.ConsumerService.GetConsumer(c => c.source == Source);
        }
    }

    public class LoadVisitedConsumer : LoadConsumerStrategy
    {
        public DateTime Aftre { get; set; }
        public DateTime Before { get; set; }

        /// <summary>
        /// 根据回访记录加载客户
        /// </summary>
        /// <returns></returns>
        public override List<Alading.Entity.Consumer> LoadConsumers()
        {
            return Alading.Business.ConsumerService.GetConsumer((c => c.last_visit >= Aftre && c.last_visit <= Before), PageIndex, PageSize, out row_count);
        }

        /// <summary>
        /// 根据回访记录加载客户，不分页
        /// </summary>
        /// <returns></returns>
        public override List<Alading.Entity.Consumer> LoadAllConsumers()
        {
            return Alading.Business.ConsumerService.GetConsumer(c => c.last_visit >= Aftre && c.last_visit <= Before);
        }
    }
}
