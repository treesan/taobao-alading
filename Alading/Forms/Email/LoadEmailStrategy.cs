using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alading.Forms.Email
{
    public abstract class LoadEmailStrategy
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

        public abstract List<Alading.Entity.ConsumerVisit> LoadEmail();
        public abstract List<Alading.Entity.ConsumerVisit> LoadAllEmail();
    }

    public class SearchAll : LoadEmailStrategy
    {
        /// <summary>
        /// 加载所有邮件
        /// </summary>
        /// <returns></returns>
        public override List<Alading.Entity.ConsumerVisit> LoadEmail()
        {
            return Alading.Business.ConsumerVisitService.GetConsumerVisit(PageIndex, PageSize, out row_count);
        }


        /// <summary>
        /// 加载所有邮件，不分页
        /// </summary>
        /// <returns></returns>
        public override List<Alading.Entity.ConsumerVisit> LoadAllEmail()
        {
            return Alading.Business.ConsumerVisitService.GetAllConsumerVisit();
        }
    }

    public class LoadTypeEmail : LoadEmailStrategy
    {
        public string State { get; set; }
        public override List<Alading.Entity.ConsumerVisit> LoadEmail()
        {
            return Alading.Business.ConsumerVisitService.GetConsumerVisit((cc => cc.Status == State), PageIndex, PageSize, out row_count);

        }


        public override List<Alading.Entity.ConsumerVisit> LoadAllEmail()
        {
            return Alading.Business.ConsumerVisitService.GetConsumerVisit((cc => cc.Status == State));
        }
    }
}
