using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alading.Forms.Consumer
{
    public class EmailTaskBuilder
    {
        private System.Data.DataTable databuffer;
        private int currentIndex;
        private bool state;
        private string taskcode = string.Empty;
        private Alading.Entity.Task currentTask = null;
        private bool isInitialized = false;

        public string EmailSubject { get; set; }
        public string EmailContentTemplate { get; set; }
        public DateTime TaskStartTime { get; set; }

        public bool TaskState
        {
            get { return state; }
        }

        public bool HasNext
        {
            get { return (Total > 0) && (Current < Total); }
        }

        public int Current
        {
            get { return currentIndex; }
        }

        public int Total
        {
            get
            {
                if (Receivers != null) return Receivers.Count;
                return 0;
            }
        }

        public int Percentage
        {
            get
            {
                if (Total == 0) return 0;
                return Convert.ToInt32((double)Current / Total);
            }
        }

        public Alading.Entity.Shop Shop { get; set; }

        public List<Alading.Entity.Consumer> Receivers { get; set; }

        public EmailTaskBuilder()
        {
            // initialize data table structrue
            databuffer = new System.Data.DataTable();
            TaskStartTime = DateTime.Now;
            /*
            databuffer.Columns.Add("Id");
            databuffer.Columns.Add("VisitCode");
            databuffer.Columns.Add("ConsumerNick");
            databuffer.Columns.Add("Type");
            databuffer.Columns.Add("Subject");
            databuffer.Columns.Add("Content");
            databuffer.Columns.Add("VisitTime");
            databuffer.Columns.Add("Receiver");
            databuffer.Columns.Add("Status");
            databuffer.Columns.Add("TaskCode");
            */

            databuffer.Columns.Add("A30P0");
            databuffer.Columns.Add("A30P1");
            databuffer.Columns.Add("A30P2");
            databuffer.Columns.Add("A30P3");
            databuffer.Columns.Add("A30P4");
            databuffer.Columns.Add("A30P5");
            databuffer.Columns.Add("A30P6");
            databuffer.Columns.Add("A30P7");
            databuffer.Columns.Add("A30P8");
            databuffer.Columns.Add("A30P9");
        }

        public bool InitializeTaskBuilder()
        {
            state = false;
            currentIndex = 0;

            taskcode = Guid.NewGuid().ToString();

            currentTask = new Alading.Entity.Task
            {
                TaskCode = taskcode,
                Type = "SendEmail",
                Desc = "Send email to consumers",
                StartTime = DateTime.Now,
                EndTime = null,
                Creator = string.Empty,
                Done = 0,
                Total = Total
            };

            isInitialized = Alading.Business.TaskService.AddTask(currentTask) == Alading.Core.Enum.ReturnType.Success;
            return isInitialized;
        }

        public void StepNext()
        {
            if (Receivers.Count > 0 && currentIndex < Receivers.Count)
            {
                Alading.Entity.Consumer currentObject = Receivers[currentIndex];
                BuildEmailRecord(currentObject);
                currentIndex++;
            }
        }

        public bool Flush()
        {
            if (isInitialized)
            {
                return Alading.Business.ConsumerVisitService.AddConsumerVisitSqlBulkCopy(databuffer) == Alading.Core.Enum.ReturnType.Success;
            }
            return false;
        }

        public void RemoveTask()
        {
            Alading.Business.TaskService.RemoveTask(c => c.TaskCode == taskcode);
        }

        private string BuildEmailContent(Alading.Entity.Consumer consumer, Alading.Entity.Shop shop)
        {
            StringBuilder sb = new StringBuilder(EmailContentTemplate);
            string addr = string.Format("{0} {1} {2} {3} {4}",
                consumer.location_country,
                consumer.location_state,
                consumer.location_city,
                consumer.location_district,
                consumer.location_address);

            sb.Replace("@收货人@", consumer.buyer_name);
            sb.Replace("@收货地址@", addr);
            sb.Replace("@邮政编码@", consumer.buyer_zip);
            sb.Replace("@联系电话@", consumer.phone);

            sb.Replace("@客户昵称@", consumer.nick);
            sb.Replace("@客户地址@", addr);
            sb.Replace("@客户邮编@", consumer.buyer_zip);
            sb.Replace("@客户电话@", consumer.phone);
            sb.Replace("@客户手机@", consumer.mobilephone);
            sb.Replace("@客户邮箱@", consumer.email);

            if (shop != null)
            {
                string shopAddr = string.Format("{0} {1} {2} {3} {4}",
                                    shop.seller_country,
                                    shop.seller_state,
                                    shop.seller_city,
                                    shop.seller_district,
                                    shop.seller_address);

                sb.Replace("@店主昵称@", shop.nick);
                sb.Replace("@店主地址@", shopAddr);
                sb.Replace("@店主邮编@", shop.seller_zip);
                sb.Replace("@店主电话@", shop.seller_tel);
                sb.Replace("@店主手机@", shop.seller_mobile);
                sb.Replace("@店铺名称@", shop.title);
            }
            else
            {
                sb.Replace("@店主昵称@", string.Empty);
                sb.Replace("@店主地址@", string.Empty);
                sb.Replace("@店主邮编@", string.Empty);
                sb.Replace("@店主电话@", string.Empty);
                sb.Replace("@店主手机@", string.Empty);
                sb.Replace("@店铺名称@", string.Empty);
            }

            sb.Replace("@系统日期@", DateTime.Today.ToShortDateString());
            sb.Replace("@系统时间@", DateTime.Now.ToLongTimeString());

            return sb.ToString();
        }

        private void BuildEmailRecord(Alading.Entity.Consumer consumer)
        {
            string content = BuildEmailContent(consumer, Shop);

            //databuffer.Rows.Add(
            //    0,                              //Id
            //    Guid.NewGuid().ToString(),      //VisitCode
            //    consumer.nick,                  //ConsumerNick
            //    "邮件",                        //Type
            //    EmailSubject,                   //Subject
            //    content,                        //Content
            //    DateTime.Now,                   //VisitTime
            //    consumer.email,                 //Receiver
            //    "待发送",                      //Status
            //    taskcode                        //TaskCode
            //    );

            System.Data.DataRow row = databuffer.NewRow();

            row["A30P1"] = Guid.NewGuid().ToString();
            row["A30P2"] = consumer.nick;
            row["A30P3"] = "邮件";
            row["A30P4"] = EmailSubject;
            row["A30P5"] = BuildEmailContent(consumer, Shop);
            row["A30P6"] = TaskStartTime;
            row["A30P7"] = consumer.email;
            row["A30P8"] = "待发送";
            row["A30P9"] = taskcode;

            databuffer.Rows.Add(row);

        }
    }
}
