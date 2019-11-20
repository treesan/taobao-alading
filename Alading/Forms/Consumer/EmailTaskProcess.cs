using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Alading.Forms.Consumer
{
    public partial class EmailTaskProcess : DevExpress.XtraEditors.XtraForm
    {
        private BackgroundWorker worker;

        public EmailTaskBuilder TaskBuilder { get; set; }

        public EmailTaskProcess()
        {
            InitializeComponent();

            TaskBuilder = new EmailTaskBuilder();

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = false;
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
        }

        /// <summary>
        /// worker do work event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker wk = sender as BackgroundWorker;
            EmailTaskBuilder tb = e.Argument as EmailTaskBuilder;

            bool start = tb.InitializeTaskBuilder();
            if (!start)
            {
                e.Result = 0; // failed at initializing task
                return;
            }

            wk.ReportProgress(0, string.Format("{0}% ({1}/{2})", tb.Percentage, tb.Current, tb.Total));

            while (tb.HasNext)
            {
                tb.StepNext();
                wk.ReportProgress(tb.Percentage, string.Format("{0}% ({1}/{2})", tb.Percentage, tb.Current, tb.Total));
            }

            bool end = tb.Flush();
            if (!end)
            {
                e.Result = -1; // failed at ending task
                tb.RemoveTask();
                return;
            }

            wk.ReportProgress(100, string.Format("{0}% ({1}/{2})", tb.Percentage, tb.Current, tb.Total));
            e.Result = 1; // success
        }

        /// <summary>
        /// worker progress changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Position = e.ProgressPercentage;
            lbStatus.Text = e.UserState as string;
        }

        /// <summary>
        /// worker work completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int result = (int)e.Result;

            switch (result)
            {
                case 0: // failed at initializing task
                    XtraMessageBox.Show("初始化任务失败，请稍候尝试！");
                    DialogResult = DialogResult.Abort;
                    break;

                case -1: // failed at ending task
                    XtraMessageBox.Show("创建任务失败，请稍候尝试！");                    
                    DialogResult = DialogResult.Abort;
                    break;
                default: // success
                    DialogResult = DialogResult.OK;
                    break;
            }

            this.Close();
        }

        private void EmailTaskProcess_Load(object sender, EventArgs e)
        {
            if (TaskBuilder != null)
            {
                System.Threading.Thread.Sleep(1000);
                worker.RunWorkerAsync(TaskBuilder);
            }
        }
    }
}