using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;

namespace Alading.Forms.Email
{
    public partial class EmailSender : DevExpress.XtraEditors.XtraForm
    {
        private static object lockObject = new object();

        public SmtpConfiguration configuration;
        private BackgroundWorker dispatcher = new BackgroundWorker();

        List<BackgroundWorker> threadList = new List<BackgroundWorker>();
        List<SendEmailTask> taskList = new List<SendEmailTask>();

        AutoResetEvent autoReset = new AutoResetEvent(false);

        Alading.Entity.Task currentTask = null;

        int totalThreadCount = 0;
        int doneThreadCount = 0;

        public EmailSender()
        {
            InitializeComponent();

            configuration = SmtpConfigurationProvider.Configuration;

            dispatcher.WorkerSupportsCancellation = true;
            dispatcher.DoWork += new DoWorkEventHandler(dispatcher_DoWork);
            dispatcher.RunWorkerCompleted += new RunWorkerCompletedEventHandler(dispatcher_RunWorkerCompleted);
        }

        private void AddThreadCount()
        {
            lock (lockObject)
            {
                totalThreadCount++;
                if (doneThreadCount == totalThreadCount)
                {
                    autoReset.Set();
                }
            }
        }

        private void dispatcher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // do complete work...
            int done = 0;
            foreach (var i in taskList)
            {
                done += i.SuccessCount;
            }

            currentTask.EndTime = DateTime.Now;
            currentTask.Done = done;

            Alading.Business.TaskService.UpdateTask(currentTask);

            dispatcher.RunWorkerAsync();
        }

        private void dispatcher_DoWork(object sender, DoWorkEventArgs e)
        {
            threadList.Clear();
            taskList.Clear();

            BackgroundWorker dispatcher = sender as BackgroundWorker;

            #region Get a task

            var query = Alading.Business.TaskService.GetTask(c => c.Type == "SendEmail" && c.EndTime == null);
            if (query == null || query.Count == 0)
            {
                e.Result = 0;
                return;
            }
            currentTask = query[0]; 

            #endregion

            #region Get email for sending

            var list = Alading.Business.ConsumerVisitService.GetConsumerVisit(c => c.TaskCode == currentTask.TaskCode && c.Status == "待发送");
            if (list == null || list.Count == 0)
            {
                e.Result = 0;
                return;
            } 

            #endregion

            #region Dispatch task

            if (list.Count < 10)
            {
                SendEmailTask task1 = new SendEmailTask();
                task1.Configuration = configuration;
                task1.TaskCode = currentTask.TaskCode;
                task1.TaskList = list;
                taskList.Add(task1);

                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerSupportsCancellation = true;
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                threadList.Add(worker);
            }
            else
            {
                int size = 10;

                if (list.Count > configuration.ThreadCount * 10)
                {
                    size = list.Count / configuration.ThreadCount;
                }

                int count = list.Count / size;
                int skip = 0;

                for (int i = 0; i < count; i++)
                {
                    SendEmailTask task1 = new SendEmailTask();
                    task1.Configuration = configuration;
                    task1.TaskCode = currentTask.TaskCode;
                    task1.TaskList = list.Skip(skip).Take(size).ToList();
                    taskList.Add(task1);

                    BackgroundWorker worker = new BackgroundWorker();
                    worker.WorkerSupportsCancellation = true;
                    worker.DoWork += new System.ComponentModel.DoWorkEventHandler(worker_DoWork);
                    worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                    threadList.Add(worker);

                    skip += size;
                }

                var left = list.Skip(skip).Take(size).ToList();
                taskList[0].TaskList.AddRange(left);
            }

            doneThreadCount = 0;
            totalThreadCount = 0;

            #endregion

            for (int i = 0; i < threadList.Count; i++)
            {
                threadList[i].RunWorkerAsync(taskList[i]);                
            }

            autoReset.WaitOne();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            SendEmailTask task = e.Argument as SendEmailTask;

            task.DoAllWork();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AddThreadCount();
        }

        private void EmailSender_Load(object sender, EventArgs e)
        {
            dispatcher.RunWorkerAsync();
        }
    }
}