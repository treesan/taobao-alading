using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Linq;
using System.Configuration;
using Alading.WebService;
using System.Threading;

namespace Alading.Updater
{
    public partial class MainForm : Form
    {
        /*定义全局变量*/
        private Alading.WebService.Version version;

        string rootUpdatePath = string.Empty;

        private List<FileUpdate> fileUpdateList;

        /// <summary>
        /// 更新URL地址
        /// </summary>
        private string urlAddres = string.Empty;
        /// <summary>
        /// 更新信息描述
        /// </summary>
        private string description = string.Empty;

        delegate void DelegateUpdateProgressBar(ProgressBarControl barname, int percentage);
        delegate void DelegateShowDetail(string info);
        private DelegateUpdateProgressBar DlgUpdateCurrentBar;
        private DelegateShowDetail DlgShowDetail;

        public MainForm()
        {
            InitializeComponent();
            rootUpdatePath = Application.StartupPath + "\\UpdateData\\";
            if (!Directory.Exists(rootUpdatePath))
            {
                Directory.CreateDirectory(rootUpdatePath);
            }
            ConfigClass config = new ConfigClass();
            config.SetConfigName(@"\Alading.exe.config");
            string currentVersionCode = config.ReadConfig("CurrentVersion");
            string versionType = config.ReadConfig("VersionType");
            Alading.WebService.Version version = ServiceHelper.GetNewVersion(versionType);
            if (version != null && version.VersionCode != currentVersionCode)
            {
                List<FileUpdate> list = ServiceHelper.GetFileUpdateList(version.VersionCode);
                this.fileUpdateList = list;
                this.version = version;
            }
            else
            {
                this.fileUpdateList = new List<FileUpdate>();
                this.version = new Alading.WebService.Version();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            /*kill主程序拷贝文件*/
            KillProcesses("Alading");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            richTextBoxDetail.AppendText("正在检查更新，请稍后......\n");
            try
            {
                if (version == null)
                {
                    XtraMessageBox.Show("当前版本已经是最新版本，不需要更新。");
                    return;
                }
                if (fileUpdateList != null)
                {
                    List<string> filelist = new List<string>();
                    BackgroundWorker DownFilework = new BackgroundWorker();
                    DownFilework.WorkerReportsProgress = true;
                    DownFilework.WorkerSupportsCancellation = true;
                    DownFilework.DoWork += new DoWorkEventHandler(DownFilework_DoWork);
                    DownFilework.ProgressChanged += new ProgressChangedEventHandler(DownFilework_ProgressChanged);
                    DownFilework.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DownFilework_RunWorkerCompleted);
                    DownFilework.RunWorkerAsync();
                }

            }
            catch (System.Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        void DownFilework_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;           
            int n = fileUpdateList.Count;
            if (n == 0)
            {
                BeginInvoke(new Action(() => { this.richTextBoxDetail.Text += "没有可以更新的文件！"; }));
                //DlgShowDetail = new DelegateShowDetail(ShowDetail);
                //DlgShowDetail.BeginInvoke(string.Format("没有可以更新的文件！"), null, null);
            }
            for (int i = 0; i < n; i++)
            {
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                int percentage = (int)((float)(i + 1) * 100 / (float)n);
                FileUpdate fileUpdate = fileUpdateList[i];
                worker.ReportProgress(percentage, string.Format("正在下载:{0}\n", fileUpdate.FileName));
                DownSaveFile(fileUpdate);                
                worker.ReportProgress(percentage, string.Format("已下载完成:{0}\n", fileUpdate.FileName));
                description +="\n" +fileUpdate.FileName;
                //BeginInvoke(new Action(() => { this.richTextBoxDetail.Text += string.Format("已下载完成:{0}\n", fileUpdate.FileName); }));
                //DlgShowDetail = new DelegateShowDetail(ShowDetail);
                //DlgShowDetail.BeginInvoke(string.Format("已下载完成:{0}\n", fileUpdate.FileName), null, null);
            }
        }

        void DownFilework_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBarTotal.Position = e.ProgressPercentage;
            this.richTextBoxDetail.Text += e.UserState.ToString();
        }

        void DownFilework_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                return;
            }
            else if (e.Error != null)
            {
                XtraMessageBox.Show("网络异常，请重新升级!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.richTextBoxDetail.Text += string.Format("本次更新：{0}\n正在更新文件，请稍后......\n", description);
               
                    //DlgShowDetail.BeginInvoke(string.Format("本次更新：{0}\n正在更新文件，请稍后......\n", description), null, null);
                    ////XtraMessageBox.Show("系统升级成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MoveDirectoryFile();
                    this.richTextBoxDetail.Text += "\n升级成功完成，正在启动主程序......";
                    //DlgShowDetail.BeginInvoke("\n升级成功完成，正在启动主程序......", null, null);

                    /*修改配置文件*/
                    ConfigClass config = new ConfigClass();
                    config.SetConfigName(@"\Alading.exe.config");
                    config.SaveConfig("CurrentVersion", version.VersionCode);//保存当前版本号
                    this.Hide();                   
                    foreach (FileUpdate fileUpdate in fileUpdateList)
                    {
                        if (fileUpdate.RunAfterUpdate)
                        {
                            Process p = Process.Start(Application.StartupPath + @fileUpdate.FilePath + fileUpdate.FileName);
                            p.WaitForExit();
                        }
                    }                   
                    /*启动主进程*/
                    Thread.Sleep(2000);
                    Process.Start(Application.StartupPath + "\\Alading.exe");
                    this.Close();
            }
        }

        #region 移动文件夹下所有文件
        public void MoveDirectoryFile()
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(rootUpdatePath);
                FileInfo[] files = d.GetFiles();
                foreach (FileUpdate fileUpdate in fileUpdateList)
                {
                    /*根据别名找到目标文件*/
                    FileInfo info = files.FirstOrDefault(c => c.Name == fileUpdate.FileNameAlias);
                    if (info != null)
                    {
                        string desfilePath = Application.StartupPath + @fileUpdate.FilePath;
                        string desfile = desfilePath + fileUpdate.FileName;
                        if (!Directory.Exists(desfilePath))
                        {
                            Directory.CreateDirectory(desfilePath);
                        }

                        if (File.Exists(desfile))
                        {
                            File.Delete(desfile);
                        }
                        info.MoveTo(desfile);
                    }
                }
            }
            catch
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("更新文件出错!\n请手动将" + Application.StartupPath + "\\UpdateData目录下文件\n拷贝到" + Application.StartupPath, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processName"></param>
        private void KillProcesses(string processName)
        {
            try
            {
                Process[] pArray = Process.GetProcessesByName(processName);
                foreach (Process process in pArray)
                {
                    process.Kill();
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 根据文件URL下载到本地文件夹
        /// </summary>
        private void DownSaveFile(FileUpdate fileUpdate)
        {
            try
            {
                DlgUpdateCurrentBar = new DelegateUpdateProgressBar(UpdateCurrentBarPosition);

                /*保证相对路径最后一个字符都加上\！！*/
                /*如果是根目录，则其相对路径存一个字符：\*/
                /*相对路径首字符必须是\，否则路径拼接要出错。*/
                /*例子：*/
                /*Name:Mix.Core.WinForm.dll    NameAlias:Mix.Core.WinForm.dll    filePath:\ItemPics\    srcFilePath:http://mixtrade.wisecode.com.cn/UpdateData/.*/
                string srcFilePath = fileUpdate.SrcFilePath + fileUpdate.FileName;
                //string destFile = fileUpdate.FilePath + fileUpdate.FileName;

                string filePath = string.Empty;
                if (!Directory.Exists(Application.StartupPath + "\\UpdateData\\"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\UpdateData\\");
                }
                /*下载的文件直接使用别名，如果不需要别名，fileUpdate.FileName必须等于fileUpdate.FileNameAlias*/
                filePath = Application.StartupPath + "\\UpdateData\\" + fileUpdate.FileNameAlias;

                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(srcFilePath);
                webRequest.Method = "GET";
                System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
                System.IO.Stream stream = webResponse.GetResponseStream();
                long length = webResponse.ContentLength;
                List<byte> list = new List<byte>();
                int c = 0;
                int percentage = 0;
                while (true)
                {
                    int data = stream.ReadByte();
                    if (data == -1)
                        break;
                    else
                    {
                        byte b = (byte)data;
                        list.Add(b);
                        c++;
                        int temp = percentage;
                        percentage = (int)((float)c * 100 / (float)length);
                        if (percentage != temp)
                        {
                            DlgUpdateCurrentBar.BeginInvoke(progressBarCurrent, percentage, null, null);
                        }
                    }
                }
                byte[] bb = list.ToArray();
                System.IO.File.WriteAllBytes(filePath, bb);
                stream.Close();//别忘了关闭             
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void UpdateCurrentBarPosition(ProgressBarControl bar, int percentage)
        {
            if (progressBarCurrent.InvokeRequired)
            {
                progressBarCurrent.Invoke(DlgUpdateCurrentBar, bar, percentage);
            }
            else
            {
                progressBarCurrent.Position = percentage;
            }
        }

        private void ShowDetail(string info)
        {
            if (this.richTextBoxDetail.InvokeRequired)
            {
                this.richTextBoxDetail.Invoke(DlgShowDetail, info);
            }
            else
            {
                this.richTextBoxDetail.Text += info;
            }
        }

        /*
         <?xml version="1.0"?>
<AutoUpdater>
  <URLAddres>http://mixtrade.wisecode.com.cn/UpdateData/</URLAddres>
  <Version>1.0.1818.42821</Version>
  <Description>修正一些</Description>
  <FileCount>3</FileCount>
  <UpdateFileList>
    <UpdateFile FileName = "dsadsa.dll" DestFolder=""/>
    <UpdateFile FileName = "dddd.eeeee" DestFolder=""/>
  </UpdateFileList>
</AutoUpdater>

         */
    }

    public class ConfigClass
    {
        public string strFileName;
        public string configName;
        public string configValue;
        public ConfigClass()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public string ReadConfig(string configKey)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {               
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att != null)
                {
                    if (att.Value == "" + configKey + "")
                    {
                        //对目标元素中的第二个属性赋值
                        att = nodes[i].Attributes["value"];
                        configValue = att.Value;
                        break;
                    }
                }
            }
            return configValue;
        }

        //得到程序的config文件的名称以及其所在的全路径
        public void SetConfigName(string strConfigName)
        {
            configName = strConfigName;
            //获得配置文件的全路径
            GetFullPath();
        }

        public void GetFullPath()
        {
            //获得配置文件的全路径
            strFileName = Application.StartupPath + configName;
        }

        public void SaveConfig(string configKey, string configValue)
        {
            //System.Configuration.ConfigurationManager.AppSettings.Set(configKey, configValue);
            XmlDocument doc = new XmlDocument();
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att != null)
                {
                    if (att.Value == "" + configKey + "")
                    {
                        //对目标元素中的第二个属性赋值
                        att = nodes[i].Attributes["value"];
                        att.Value = configValue;
                        break;
                    }
                }
            }
            //保存上面的修改
            doc.Save(strFileName);
        }
    }
}
