using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Taobao.API;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.Entity;
using Alading.Taobao;
using Alading.Business;
using Newtonsoft.Json;
using System.Linq;
using Alading.Utils;
using Alading.Core.Enum;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.Utils;

namespace Alading.Forms.PropValue
{
    public partial class DownloadPropValue : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 属性或属性值没有下载成功的类目列表
        /// </summary>
        private List<string> ciderrorlist = null;

        /// <summary>
        /// 下载线程是否被取消
        /// </summary>
        private bool isCancel = false;

        /// <summary>
        /// 线程总数
        /// </summary>
        private int threadnum = 0;

        /// <summary>
        /// 线程完成个数
        /// </summary>
        private int threadCompleteNum = 0;

        /// <summary>
        /// 完成类目个数
        /// </summary>
        private int cidCompleteNum = 0;

        /// <summary>
        /// 淘宝所有类目
        /// </summary>
        private List<Alading.Entity.ItemCat> itemcatlist = new List<Alading.Entity.ItemCat>();

        public DownloadPropValue()
        {
            InitializeComponent();
            itemcatlist = ItemCatService.GetAllItemCat();
        }

        /// <summary>
        /// 加载界面时，加载数据库中数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadPropValue_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.btnStart.Enabled = false;
            isCancel = false;
            threadCompleteNum = 0;
            cidCompleteNum = 0;
            //ciderrorlist可以不用重新赋值

            listBoxCtrl.Items.Insert(0, DateTime.Now.ToString(Constants.DATE_TIME_FORMAT) + "正在计算需下载个数……");
            Application.DoEvents();
            List<string> totalcidlist = GetCheckedCids(treeListItemCat.Nodes);
            if (totalcidlist.Count == 0)
            {
                listBoxCtrl.Items.Insert(0, "没有需要下载的类目！");
                XtraMessageBox.Show("没有需要下载的类目！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            listBoxCtrl.Items.Insert(0, string.Format("需要下载类目总数{0}个", totalcidlist.Count));
            //进度条最大值
            this.progressBarTotal.Properties.Maximum = totalcidlist.Count;
            //线程数
            threadnum = (int)spinEditThreadCount.Value;
            int size = 0;
            if (totalcidlist.Count < threadnum)
            {
                threadnum = totalcidlist.Count;
                size = 1;
            }
            else
            {
                size = totalcidlist.Count % threadnum == 0 ? totalcidlist.Count / threadnum : totalcidlist.Count / threadnum + 1;
            }
            for (int i = 0; i < threadnum; i++)
            {
                List<string> cidlist = totalcidlist.Skip(i * size).Take(size).ToList();
                BackgroundWorker batchworker = new BackgroundWorker();
                batchworker.WorkerReportsProgress = true;
                batchworker.WorkerSupportsCancellation = true;
                batchworker.DoWork += new DoWorkEventHandler(batchworker_DoWork);
                batchworker.ProgressChanged += new ProgressChangedEventHandler(batchworker_ProgressChanged);
                batchworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(batchworker_RunWorkerCompleted);
                listBoxCtrl.Items.Insert(0, string.Format("{0}线程{1}开始下载……", DateTime.Now.ToString(Constants.DATE_TIME_FORMAT), i + 1));
                MyCidTask cidtask = new MyCidTask();
                cidtask.cidlist = cidlist;
                cidtask.threadId = i + 1;
                batchworker.RunWorkerAsync(cidtask);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isCancel = true;
        }

        /// <summary>
        /// 下载失败的类目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRetry_Click(object sender, EventArgs e)
        {
            if (ciderrorlist != null && ciderrorlist.Count > 0)
            {
                RetryError();
            }
        }

        private void RetryError()
        {
            //进度条最大值
            this.progressBarTotal.Properties.Maximum = ciderrorlist.Count;

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(backWorker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(backWorker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backWorker_RunWorkerCompleted);
            listBoxCtrl.Items.Insert(0, string.Format("需要下载类目总数{0}个", ciderrorlist.Count));
            listBoxCtrl.Items.Insert(0, DateTime.Now.ToString(Constants.DATE_TIME_FORMAT) + "开始下载……");
            List<string> newCidList = new List<string>(ciderrorlist);
            //避免死循环
            ciderrorlist.Clear();
            worker.RunWorkerAsync(newCidList);
        }

        #region 单线程下载
        private void backWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            List<string> cidlist = (List<string>)e.Argument;
            for (int i = 0; i < cidlist.Count; i++)
            {
                if (isCancel)
                {
                    e.Cancel = true;
                    break;
                }
                string cid = cidlist[i];
                try
                {
                    UIHelper.DownPropsAndValues(cid, worker);
                }
                catch (Exception ex)
                {
                    if (ciderrorlist == null)
                    {
                        ciderrorlist = new List<string>();
                    }
                    ciderrorlist.Add(cid);
                    worker.ReportProgress(i + 1, string.Format("类目cid={0}异常信息：{1}", cid, ex.Message));
                    continue;
                }

                //更新ItemCat中此cid下的属性已完全下载
                ItemCatService.UpdateItemCatPropTag(cid, true);

                worker.ReportProgress(i + 1, cid);
            }
        }

        private void backWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null && e.UserState.ToString().Contains("异常"))
            {
                listBoxCtrl.Items.Insert(0, string.Format("{0}第{1}种类目没有下载成功，{2}", DateTime.Now.ToString(Constants.DATE_TIME_FORMAT), e.ProgressPercentage, e.UserState));
            }
            else if (e.UserState != null && !e.UserState.ToString().Contains("异常"))
            {
                this.progressBarTotal.Position = e.ProgressPercentage;
                listBoxCtrl.Items.Insert(0, string.Format("{0}已完成下载第{1}种类目，cid为{2}", DateTime.Now.ToString(Constants.DATE_TIME_FORMAT), e.ProgressPercentage, e.UserState));
            }
            else if (e.UserState == null)
            {
                this.progressBarCurrent.Position = e.ProgressPercentage;
                listBoxCtrl.Items.Insert(0, string.Format("{0}当前类目完成进度为{1}%", DateTime.Now.ToString(Constants.DATE_TIME_FORMAT), e.ProgressPercentage));
            }
        }

        private void backWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnRetry.Enabled = true;
            if (e.Cancelled)
            {
                listBoxCtrl.Items.Insert(0, string.Format("{0}下载被取消", DateTime.Now.ToString(Constants.DATE_TIME_FORMAT)));
            }
            else if (e.Error != null)
            {
                listBoxCtrl.Items.Insert(0, string.Format("{0}下载出错", DateTime.Now.ToString(Constants.DATE_TIME_FORMAT)));
            }
            else
            {
                listBoxCtrl.Items.Insert(0, string.Format("{0}下载完成", DateTime.Now.ToString(Constants.DATE_TIME_FORMAT)));
                XtraMessageBox.Show("下载完成", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //另起线程下载错误的cid属性
            if (ciderrorlist != null && ciderrorlist.Count > 0)
            {
                DialogResult result = XtraMessageBox.Show(string.Format("本次有{0}个类目没有下载成功，是否继续下载？", ciderrorlist.Count), Constants.SYSTEM_PROMPT, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    RetryError();
                }
            }
        }
        #endregion

        #region 多线程下载
        void batchworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            threadCompleteNum++;
            if (threadCompleteNum == threadnum)
            {
                this.btnStart.Enabled = true;
            }
            if (e.Cancelled)
            {
                listBoxCtrl.Items.Insert(0, string.Format("{0}下载被取消", DateTime.Now.ToString(Constants.DATE_TIME_FORMAT)));
            }
            else if (e.Error != null)
            {
                listBoxCtrl.Items.Insert(0, string.Format("{0}下载出错", DateTime.Now.ToString(Constants.DATE_TIME_FORMAT)));
            }
            else
            {
                listBoxCtrl.Items.Insert(0, string.Format("{0}下载完成,线程号为{1}", DateTime.Now.ToString(Constants.DATE_TIME_FORMAT), e.Result ?? string.Empty));
                if (threadCompleteNum == threadnum)
                {
                    XtraMessageBox.Show("下载完成", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //另起线程下载错误的cid属性
            if (threadCompleteNum == threadnum && ciderrorlist != null && ciderrorlist.Count > 0)
            {
                this.btnRetry.Enabled = true;
                DialogResult result = XtraMessageBox.Show(string.Format("本次有{0}个类目没有下载成功，是否继续下载？", ciderrorlist.Count), "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //不必在此将threadCompleteNum 赋值0;
                    RetryError();
                }
            }
        }

        void batchworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null && !e.UserState.ToString().Contains("异常信息"))
            {
                listBoxCtrl.Items.Insert(0, string.Format("{0}已完成下载第{1}个类目，线程号为{2}", DateTime.Now.ToString(Constants.DATE_TIME_FORMAT), e.ProgressPercentage, e.UserState));
                this.progressBarTotal.Position = e.ProgressPercentage;
            }
            else if (e.UserState != null && e.UserState.ToString().Contains("异常信息"))
            {
                listBoxCtrl.Items.Insert(0, string.Format("{0}cid={1}的类目异常,{2}", DateTime.Now.ToString(Constants.DATE_TIME_FORMAT), e.ProgressPercentage, e.UserState));
            }
            else if (e.UserState==null)
            {
                this.progressBarCurrent.Position = e.ProgressPercentage;
            }
        }

        void batchworker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            MyCidTask cidtask = (MyCidTask)e.Argument;
            List<string> threadcidlist = cidtask.cidlist;

            #region 多类目下载一次存储方式，不采用
            ////分times次下载
            //int times = 100;
            //int size = 0;
            //if (threadcidlist.Count >= times)
            //{
            //    size = threadcidlist.Count % times == 0 ? threadcidlist.Count / times : threadcidlist.Count / times + 1;
            //}
            //else
            //{
            //    size = 1;
            //    times = threadcidlist.Count;
            //} 
            #endregion

            int times = threadcidlist.Count;
            //int temp = 0;//作用是避免进度值propgress没有改变时得重复报告
            for (int i = 0; i < times; i++)
            {
                cidCompleteNum++;
                if (isCancel)
                {
                    e.Cancel = true;
                    break;
                }

                #region 多类目下载一次存储方式，不采用
                //List<string> cidlist = threadcidlist.Skip(i * size).Take(size).ToList();

                //List<string> valueWhereInCids = ItemPropValueService.GetPropValueWhereInCids(cidlist);
                //List<string> valueCidList = cidlist.Except(valueWhereInCids).ToList();
                //DownItemPropvalue(valueCidList, worker);

                //List<string> propWhereInCids = ItemPropService.GetPropWhereInCids(cidlist);
                //List<string> propCidList = cidlist.Except(propWhereInCids).ToList();
                //DownItemProp(propCidList, worker);

                //UpdatePropValueIsParent(cidlist, worker);
                //ReturnType type = ItemCatService.UpdateItemCatPropTag(cidlist); 
                #endregion

                string cid = threadcidlist[i];
                try
                {
                    UIHelper.DownPropsAndValues(cid, worker);
                }
                catch (Exception ex)
                {
                    if (ciderrorlist == null)
                    {
                        ciderrorlist = new List<string>();
                    }
                    ciderrorlist.Add(cid);
                    worker.ReportProgress(int.Parse(cid), string.Format("异常信息{0}", ex.Message));
                    continue;
                }
                //更新ItemCat中此cid下的属性已完全下载
                ItemCatService.UpdateItemCatPropTag(cid, true);

                /*进度报告*/
                //int percentage = (int)((float)(i + 1) * 100 / (float)times);
                //if (percentage > temp)
                //{
                //    worker.ReportProgress(percentage, cidtask.threadId);
                //}
                //temp = percentage;
                worker.ReportProgress(cidCompleteNum, cidtask.threadId);
            }
            e.Result = cidtask.threadId;
        }

        #endregion

        #region 多类目批量下载方法
        /// <summary>
        ///批量 先下载类目cid下的所有属性值，不能返回is_parent字段
        /// </summary>
        private void DownItemPropvalue(List<string> cidlist, BackgroundWorker worker)
        {
            DataTable table = new DataTable();
            table.Columns.Add("cid", typeof(string));
            table.Columns.Add("is_parent", typeof(bool));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("name_alias", typeof(string));
            table.Columns.Add("pid", typeof(string));
            table.Columns.Add("prop_name", typeof(string));
            table.Columns.Add("vid", typeof(string));
            table.Columns.Add("status", typeof(string));
            table.Columns.Add("sort_order", typeof(Int32));
            for (int c = 0; c < cidlist.Count; c++)
            {
                string cid = cidlist[c];
                if (ItemPropValueService.IsExistedCid(cid))
                {
                    continue;
                }
                ItemCatRsp myrsp = new ItemCatRsp();
                try
                {
                    myrsp = TopService.ItemPropValuesGet(cid, null, "2005-01-01 00:00:00");
                }
                catch (Exception ex)
                {
                    worker.ReportProgress(int.Parse(cid), string.Format("异常信息{0}", ex.Message));
                    //加入错误列表
                    if (ciderrorlist == null)
                    {
                        ciderrorlist = new List<string>();
                    }
                    ciderrorlist.Add(cid);

                    //从需要更新列表中删除
                    cidlist.Remove(cid);
                    continue;
                }
                if (myrsp.PropValues != null && myrsp.PropValues.PropValue != null)
                {
                    Alading.Taobao.Entity.PropValue[] pvalueArr = myrsp.PropValues.PropValue;
                    for (int i = 0; i < pvalueArr.Length; i++)
                    {
                        DataRow row = table.NewRow();
                        row["cid"] = pvalueArr[i].Cid;
                        row["is_parent"] = pvalueArr[i].IsParent;//API不能返回此值
                        row["name"] = pvalueArr[i].Name ?? string.Empty;
                        row["name_alias"] = pvalueArr[i].NameAlias ?? string.Empty;
                        row["pid"] = pvalueArr[i].Pid;
                        row["prop_name"] = pvalueArr[i].PropName ?? string.Empty;
                        row["vid"] = pvalueArr[i].Vid;
                        row["status"] = pvalueArr[i].Status ?? string.Empty;
                        row["sort_order"] = pvalueArr[i].SortOrder;
                        table.Rows.Add(row);
                    }
                }
            }
            if (table.Rows.Count > 0)
            {
                ItemPropValueService.AddItemPropValueSqlBulkCopy(table);
            }
        }

        /// <summary>
        /// 批量下载类目cid下的所有属性，不能返回prop_values
        /// </summary>
        private void DownItemProp(List<string> cidlist, BackgroundWorker worker)
        {
            DataTable table = new DataTable();
            table.Columns.Add("cid", typeof(string));
            table.Columns.Add("child_template", typeof(string));
            table.Columns.Add("is_allow_alias", typeof(bool));
            table.Columns.Add("is_color_prop", typeof(bool));
            table.Columns.Add("is_enum_prop", typeof(bool));
            table.Columns.Add("is_input_prop", typeof(bool));
            table.Columns.Add("is_item_prop", typeof(bool));
            table.Columns.Add("is_key_prop", typeof(bool));
            table.Columns.Add("is_sale_prop", typeof(bool));
            table.Columns.Add("multi", typeof(bool));
            table.Columns.Add("must", typeof(bool));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("parent_pid", typeof(string));
            table.Columns.Add("parent_vid", typeof(string));
            table.Columns.Add("pid", typeof(string));
            table.Columns.Add("prop_name", typeof(string));
            table.Columns.Add("prop_values", typeof(string));
            table.Columns.Add("status", typeof(string));
            table.Columns.Add("sort_order", typeof(Int32));
            for (int c = 0; c < cidlist.Count; c++)
            {
                string cid = cidlist[c];
                if (ItemPropService.IsPropExistedCid(cid))
                {
                    continue;
                }
                ItemCatRsp rsp = new ItemCatRsp();
                try
                {
                    rsp = TopService.ItemPropsGet(cid);
                }
                catch (Exception ex)
                {
                    worker.ReportProgress(int.Parse(cid), string.Format("异常信息{0}", ex.Message));
                    //加入错误列表
                    if (ciderrorlist == null)
                    {
                        ciderrorlist = new List<string>();
                    }
                    ciderrorlist.Add(cid);

                    //从需要更新列表中删除
                    cidlist.Remove(cid);
                    continue;
                }
                if (rsp.ItemProps != null && rsp.ItemProps.ItemProp != null)
                {

                    ItemProp[] propArr = rsp.ItemProps.ItemProp;
                    for (int i = 0; i < propArr.Length; i++)
                    {
                        #region 数据赋值
                        DataRow row = table.NewRow();
                        row["cid"] = cid;
                        row["child_template"] = propArr[i].ChildPropTemplate ?? string.Empty;
                        row["is_allow_alias"] = propArr[i].IsAllowAlias;
                        row["is_color_prop"] = propArr[i].IsColorProp;
                        row["is_enum_prop"] = propArr[i].IsEnumProp;
                        row["is_input_prop"] = propArr[i].IsInputProp;
                        row["is_item_prop"] = propArr[i].IsItemProp;
                        row["is_key_prop"] = propArr[i].IsKeyProp;
                        row["is_sale_prop"] = propArr[i].IsSaleProp;
                        row["multi"] = propArr[i].IsMulti;
                        row["must"] = propArr[i].IsMust;
                        row["name"] = propArr[i].Name ?? string.Empty;
                        row["parent_pid"] = propArr[i].ParentPid;
                        row["parent_vid"] = propArr[i].ParentVid;
                        row["pid"] = propArr[i].Pid;
                        row["prop_values"] = JsonConvert.SerializeObject(propArr[i].PropValues) ?? string.Empty;
                        row["sort_order"] = propArr[i].SortOrder;
                        row["status"] = propArr[i].Status ?? string.Empty;
                        #endregion
                        table.Rows.Add(row);
                    }//for
                }
            }
            if (table.Rows.Count > 0)
            {
                ItemPropService.AddItemPropSqlBulkCopy(table);
            }
        }

        /// <summary>
        /// 批量更新类目cid下所有属性值的is_parent字段，条件是必须保证cid下的所有属性及属性值已完全下载
        /// </summary>
        private void UpdatePropValueIsParent(List<string> cidlist, BackgroundWorker worker)
        {
            DataTable table = new DataTable();
            table.Columns.Add("cid", typeof(string));
            table.Columns.Add("is_parent", typeof(bool));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("name_alias", typeof(string));
            table.Columns.Add("pid", typeof(string));
            table.Columns.Add("prop_name", typeof(string));
            table.Columns.Add("vid", typeof(string));
            table.Columns.Add("status", typeof(string));
            table.Columns.Add("sort_order", typeof(Int32));

            List<Taobao.Entity.PropValue> pValueListNeedUpdate = new List<Taobao.Entity.PropValue>();

            /*每执行一次调用一次API，读取两次数据库
             * 这里不能用foreach，因为方法体里面有
             * cidlist.Remove(cid)语句，会造成无法
             * 枚举的异常
             * */
            for (int c = 0; c < cidlist.Count; c++)
            {
                string cid = cidlist[c];
                /*读取存在子属性的关键属性的pid*/
                List<string> keyPropsPidList = ItemPropService.GetKeyPropPid(cid);
                if (keyPropsPidList.Count == 0)
                {
                    return;
                }

                #region 循环获取keyPropsPidList的属性值，每执行一次调用一次API，读取一次数据库
                float n = keyPropsPidList.Count;
                //int temp = 0;//作用是避免进度值propgress没有改变时得重复报告
                for (int i = 0; i < n; i++)
                {
                    ////进度报告
                    //int propgress = (int)((float)(i + 1) / n * 100);
                    //if (propgress > temp)
                    //{
                    //    worker.ReportProgress(propgress, null);
                    //}
                    //temp = propgress;

                    string pid = keyPropsPidList[i];
                    ItemCatRsp rsp = new ItemCatRsp();
                    try
                    {
                        rsp = TopService.ItemPropsGet(cid, pid, null);
                    }
                    catch (Exception ex)
                    {
                        worker.ReportProgress(int.Parse(cid), string.Format("异常信息{0}", ex.Message));
                        //加入错误列表
                        if (ciderrorlist == null)
                        {
                            ciderrorlist = new List<string>();
                        }
                        ciderrorlist.Add(cid);

                        //从需要更新列表中删除
                        cidlist.Remove(cid);
                        continue;
                    }
                    if (rsp.ItemProps == null || rsp.ItemProps.ItemProp == null)
                    {
                        /*说明下载失败*/
                        continue;
                    }

                    #region 加一条“自定义”记录至table里,读取了一次数据库
                    if (!ItemPropValueService.IsExistedPropValueName(cid, pid, "自定义"))
                    {
                        DataRow row = table.NewRow();
                        row["cid"] = cid;
                        row["is_parent"] = false;
                        row["name"] = row["name_alias"] = "自定义";
                        row["pid"] = pid;
                        row["vid"] = 0;
                        row["status"] = "normal";
                        row["sort_order"] = 1000;
                        if (rsp.ItemProps.ItemProp != null && rsp.ItemProps.ItemProp.Length > 0)
                        {
                            row["prop_name"] = rsp.ItemProps.ItemProp[0].Name ?? string.Empty;
                        }
                        else
                        {
                            row["prop_name"] = string.Empty;
                        }
                        table.Rows.Add(row);
                    }
                    #endregion

                    #region 将is_parent为TRUE的属性值加入pValueListNeedUpdate
                    foreach (Taobao.Entity.ItemProp prop in rsp.ItemProps.ItemProp)
                    {
                        Taobao.Entity.PropValues propvalues = prop.PropValues;
                        if (propvalues != null && propvalues.PropValue != null)
                        {
                            List<Alading.Taobao.Entity.PropValue> propValueWhereToList = propvalues.PropValue.Where(p => p.IsParent).ToList();
                            foreach (Taobao.Entity.PropValue pv in propValueWhereToList)
                            {
                                pv.Cid = cid;
                                pv.Pid = pid;
                            }
                            pValueListNeedUpdate.AddRange(propValueWhereToList);
                        }
                    }//foreach
                }//for  
                    #endregion

                #endregion

            }
            //将自定义加入数据库
            if (table.Rows.Count > 0)
            {
                ItemPropValueService.AddItemPropValueSqlBulkCopy(table);
            }

            //更新is_parent
            if (pValueListNeedUpdate.Count > 0)
            {
                ItemPropValueService.UpdateItemPropValueDataParameters(pValueListNeedUpdate);
            }
        }
        #endregion

        #region TreeList方法
        /// <summary>
        /// 向treeList中加载出售中的商品的淘宝类目
        /// </summary>
        /// <param name="node1">父节点</param>
        /// <param name="treeListCat">被加载的控件treeListCat</param>
        public void LoadAllItemCat()
        {
            //获取所有Item的Cid
            List<string> listItemCid = ItemService.GetItemCids();
            //获取所有Item的淘宝类目及其父类目
            List<Alading.Entity.ItemCat> listItemCat = ItemCatService.GetAllItemCat(listItemCid);

            TreeListNode rootNode = treeListItemCat.AppendNode(new object[] { "所有类目" }, null);
            rootNode.Tag = "0";
            AddNodes("0", listItemCat, rootNode, treeListItemCat);
            rootNode.ExpandAll();
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="cid">父节点Cid</param>
        /// <param name="listItemCat">所有类目</param>
        /// <param name="node1">父节点</param>
        /// <param name="treeListCat">被加载的控件treeListCat</param>
        private void AddNodes(string cid, List<Alading.Entity.ItemCat> listItemCat, TreeListNode parentNode, TreeList treeList)
        {
            foreach (Alading.Entity.ItemCat itemCat in listItemCat.Where(c => c.parent_cid == cid))
            {
                if (itemCat.parent_cid == cid)
                {
                    TreeListNode childNode = treeList.AppendNode(new object[] { itemCat.name }, parentNode);
                    childNode.Tag = itemCat.cid;
                    AddNodes(itemCat.cid, listItemCat, childNode, treeList);
                }
            }
        }

        /// <summary>
        /// 设置子节点
        /// </summary>
        /// <param name="currNode"></param>
        /// <param name="state"></param>
        private void SetChildNodeCheckedState(TreeListNode currNode, bool state)
        {
            TreeListNodes nodes = currNode.Nodes;
            if (nodes.Count > 0)
            {
                foreach (TreeListNode node in nodes)
                {
                    node.Checked = state;
                    SetChildNodeCheckedState(node, state);
                }
            }
        }

        /// <summary>
        /// 设置父节点
        /// </summary>
        /// <param name="fnode"></param>
        /// <param name="state"></param>
        private void SetParentNodeCheckedState(TreeListNode fnode, bool state)
        {
            if (state == true)
            {
                fnode.Checked = true;
                if (fnode.ParentNode != null)
                {
                    SetParentNodeCheckedState(fnode.ParentNode, true);
                }
            }
            else
            {
                fnode.Checked = false;
                foreach (TreeListNode node in fnode.Nodes)
                {
                    if (node.Checked == true)
                    {
                        fnode.Checked = true;
                        break;
                    }
                }
                if (fnode.ParentNode != null)
                {
                    SetParentNodeCheckedState(fnode.ParentNode, fnode.Checked);
                }
            }
        }

        /// <summary>
        ///  加载ItemCat到TreeList
        /// </summary>
        /// <param name="tlItemCat"></param>
        private void LoadItemCat(TreeList tlItemCat)
        {
            tlItemCat.BeginUnboundLoad();
            List<Alading.Entity.ItemCat> itemCatList = itemcatlist.Where(c => c.parent_cid == "0" && c.status == "normal").ToList();

            foreach (Alading.Entity.ItemCat itemCat in itemCatList)
            {
                TreeListNode node = tlItemCat.AppendNode(new object[] { itemCat.name }, null, new TreeListNodeTag(itemCat.cid.ToString()));
                //设置是否有子节点，有则会显示一个+号
                node.HasChildren = itemCat.is_parent;
            }
            tlItemCat.EndUnboundLoad();
        }

        /// <summary>
        /// 加载当前节点的子类目
        /// </summary>
        /// <param name="focusedNode"></param>
        private void LoadChildNodes(TreeListNode focusedNode)
        {
            TreeListNodeTag tag = focusedNode.Tag as TreeListNodeTag;
            if (tag == null)
            {
                return;
            }
            #region 获得当前节点的子节点
            if (!tag.HasExpanded)
            {
                treeListItemCat.BeginUnboundLoad();
                List<Alading.Entity.ItemCat> itemCatList = itemcatlist.Where(c => c.parent_cid == tag.Cid && c.status == "normal").ToList();

                foreach (Alading.Entity.ItemCat itemCat in itemCatList)
                {
                    TreeListNode node = treeListItemCat.AppendNode(new object[] { itemCat.name }, focusedNode, new TreeListNodeTag(itemCat.cid.ToString()));
                    node.HasChildren = (bool)itemCat.is_parent;
                    node.Checked = focusedNode.Checked;
                }
                treeListItemCat.EndUnboundLoad();
                tag.HasExpanded = true;
            }
            #endregion
        }

        /// <summary>
        /// 获取选中节点的所有最底级的cid
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private List<string> GetCheckedCids(TreeListNodes nodes)
        {
            List<string> cidlist = new List<string>();
            foreach (TreeListNode node in nodes)
            {
                if (node.Checked && node.Tag != null)
                {
                    if (!node.HasChildren)//有子类目
                    {
                        TreeListNodeTag tag = node.Tag as TreeListNodeTag;
                        cidlist.Add(tag.Cid);
                    }
                    else if (node.Nodes.Count == 0)//没有子类目但子节点个数为0
                    {
                        TreeListNodeTag tag = node.Tag as TreeListNodeTag;
                        /*查询cid下的所有最底级子类目*/
                        cidlist.AddRange(GetChildCids(tag.Cid));
                    }
                    else//没有子类目但子节点个数不为0
                    {
                        cidlist.AddRange(GetCheckedCids(node.Nodes));
                    }
                }
            }
            return cidlist;
        }

        /// <summary>
        /// 查询cid下的所有最底级子类目
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        private List<string> GetChildCids(string cid)
        {
            List<string> cidlist = new List<string>();
            List<Alading.Entity.ItemCat> catList = itemcatlist.Where(c => c.parent_cid == cid).ToList();
            if (catList.Count == 0)
            {
                cidlist.Add(cid);
            }
            else
            {
                foreach (Alading.Entity.ItemCat cat in catList)
                {
                    cidlist.AddRange(GetChildCids(cat.cid));
                }
            }
            return cidlist;
        }

        #endregion

        private class MyCidTask
        {
            /// <summary>
            /// 类目列表
            /// </summary>
            public List<string> cidlist;
            /// <summary>
            /// 线程序号
            /// </summary>
            public int threadId;
        }

        private void treeListItemCat_AfterCheckNode(object sender, NodeEventArgs e)
        {
            try
            {
                SetChildNodeCheckedState(e.Node, e.Node.Checked);
                if (e.Node.ParentNode != null)
                {
                    SetParentNodeCheckedState(e.Node.ParentNode, e.Node.Checked);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void popupContainerEditTBCat_Popup(object sender, EventArgs e)
        {
            LoadItemCat(treeListItemCat);
        }

        private void treeListItemCat_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            TreeListNode focusedNode = e.Node;
            LoadChildNodes(focusedNode);
        }

        private void popupContainerEditTBCat_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            string text = string.Empty;
            foreach (TreeListNode node in treeListItemCat.Nodes)
            {
                if (node.Checked)
                {
                    text += node.GetDisplayText(0) + ",";
                }
            }
            popupContainerEditTBCat.Text = text.TrimEnd(',');
        }

        private void btnItemCat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                listBoxCtrl.Items.Insert(0, DateTime.Now.ToString(Constants.DATE_TIME_FORMAT) + " 启动淘宝类目下载");
                ItemCatRsp results = TopService.ItemCatsGet(null, null, "1970-01-01 00:00:00");
                listBoxCtrl.Items.Insert(0, DateTime.Now.ToString(Constants.DATE_TIME_FORMAT) + " 淘宝类目下载完毕");

                if (results != null && results.ItemCats != null && results.ItemCats.ItemCat != null)
                {
                    listBoxCtrl.Items.Insert(0, "ItemCats数量为" + results.ItemCats.ItemCat.Length);
                    Application.DoEvents();
                    //重新置空全局变量
                    itemcatlist.Clear();

                    DataTable table = new DataTable();
                    table.Columns.Add("cid", typeof(string));
                    table.Columns.Add("parent_cid", typeof(string));
                    table.Columns.Add("name", typeof(string));
                    table.Columns.Add("is_parent", typeof(bool));
                    table.Columns.Add("status", typeof(string));
                    table.Columns.Add("sort_order", typeof(Int32));
                    table.Columns.Add("PropTag", typeof(bool));
                    foreach (ItemCat itemCat in results.ItemCats.ItemCat)
                    {
                        Alading.Entity.ItemCat cat = new Alading.Entity.ItemCat();
                        cat.cid = itemCat.Cid;
                        cat.parent_cid = itemCat.ParentCid;
                        cat.name = string.IsNullOrEmpty(itemCat.Name) ? "无" : itemCat.Name;
                        cat.is_parent = itemCat.IsParent;
                        cat.status = itemCat.Status;
                        cat.sort_order = itemCat.SortOrder;
                        cat.PropTag = false;
                        itemcatlist.Add(cat);

                        DataRow row = table.NewRow();
                        row["cid"] = itemCat.Cid;
                        row["parent_cid"] = itemCat.ParentCid;
                        row["name"] = string.IsNullOrEmpty(itemCat.Name) ? "无" : itemCat.Name;
                        row["is_parent"] = itemCat.IsParent;
                        row["status"] = itemCat.Status;
                        row["sort_order"] = itemCat.SortOrder;
                        row["PropTag"] = false;
                        table.Rows.Add(row);
                    }
                    //排序
                    itemcatlist = itemcatlist.OrderBy(c => c.cid).ToList();
                    listBoxCtrl.Items.Insert(0, DateTime.Now.ToString(Constants.DATE_TIME_FORMAT) + " 启动淘宝类目存储");
                    Application.DoEvents();
                    ItemCatService.AddItemCatSqlBulkCopy(table);
                    listBoxCtrl.Items.Insert(0, DateTime.Now.ToString(Constants.DATE_TIME_FORMAT) + " 淘宝类目存储完毕");
                    //重新加载节点
                    treeListItemCat.Nodes.Clear();
                    LoadItemCat(treeListItemCat);
                    waitFrm.Close();
                }
            }
            catch (Exception ex)
            {
                waitFrm.Close();
               XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}