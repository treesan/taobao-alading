using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Business;
using DevExpress.XtraTreeList.Nodes;
using Alading.Core.Enum;
using Alading.Utils;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API;
using Newtonsoft.Json;
using DevExpress.Utils;
using Alading.Taobao;
using System.Text.RegularExpressions;


namespace Alading.Forms.Item
{
    public partial class PostageModelForm : DevExpress.XtraEditors.XtraForm
    {
        public PostageModelForm()
        {
            InitializeComponent();
        }
       

        public PostageModelForm(string nick)
        {
            InitializeComponent();
            sellerNick = nick;
        }
        /// <summary>
        /// 当前卖家
        /// </summary>
        string sellerNick = string.Empty;
        /// <summary>
        /// 运费记录
        /// </summary>
        List<Post> listPost = new List<Post>();
        /// <summary>
        /// 被删除的运费记录
        /// </summary>
        List<Post> listDPost = new List<Post>();
        
        private void PostageModelForm_Load(object sender, EventArgs e)
        {
            try
            {
                //加载地域信息
                LoadArea();
                //加载运费模板
                LoadPostage();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        # region 编辑运费模板
        private void barButtonItemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                xtraTabPageHandle.PageVisible = true;
                xtraTabPageHandle.Text = "编辑运费模板";
                xtraTabControlPost.SelectedTabPage = xtraTabPageHandle;

                //在gridControlNew地区一列加载地区列表
                List<Alading.Entity.Area> listArea = AreaService.GetArea(p => p.parent_id == "1");
                if (listArea == null)
                {
                    return;
                }
                foreach (Alading.Entity.Area area in listArea)
                {
                    repositoryItemCheckedComboBoxEditArea.Items.Add((object)area.name);
                }

                TreeListNode focusedNode = treeListPostage.FocusedNode;
                if (focusedNode != null && focusedNode.Tag != null)
                {
                    int postage_id = int.Parse(focusedNode.Tag.ToString());
                    Alading.Entity.Postage postage = PostageService.GetPostage(postage_id.ToString());
                    //加载被编辑运费模板信息
                    ShowFocusedPostage(postage);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///  加载被编辑运费模板信息
        /// </summary>
        /// <param name="focusedNode"></param>
        private void ShowFocusedPostage(Alading.Entity.Postage postage)
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                textEditPostageName.Text = postage.name;
                memoEditPostage.Text = postage.memo;
                //加载所有运费记录
                gridControlNew.DataSource = LoadPostageModels(postage);
                gridViewNew.BestFitColumns();
                waitForm.Close();
            }
            catch (Exception ex)
            {
                waitForm.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 当焦点行为默认运费记录时，“运送到”一列为“默认全国”时不能被编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewNew_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Post focusedPost = gridViewNew.GetFocusedRow() as Post;
            if (focusedPost != null)
            {
                if (focusedPost.dests == "默认全国")
                {
                    gridColumnDests.OptionsColumn.AllowEdit = false;
                }
                else
                {
                    gridColumnDests.OptionsColumn.AllowEdit = true;
                }
            }
        }

        #endregion

        # region 新增运费模板
        private void barButtonItemNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //显示新增运费模板页面
            listPost.Clear();
            xtraTabPageHandle.PageVisible = true;
            xtraTabPageHandle.Text = "新增运费模板";
            xtraTabControlPost.SelectedTabPage = xtraTabPageHandle;

            gridControlNew.DataSource = null;
            textEditPostageName.Text = string.Empty;
            memoEditPostage.Text = string.Empty;
        }

        /// <summary>
        /// 运费方式改变 清空数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxEditPostageType_SelectedValueChanged(object sender, EventArgs e)
        {
            textEditDefaultMoney.Text = string.Empty;
            textEditMoneyMore.Text = string.Empty;
            checkedComboBoxEditArea.Text = string.Empty;
            textEditDestMoney.Text = string.Empty;
            textEditDestMoneyMore.Text = string.Empty;
            checkEditConform.Checked = false;
        }

        /// <summary>
        /// 添加运费记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonMore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(checkedComboBoxEditArea.Text) || string.IsNullOrEmpty(textEditDestMoney.Text) || string.IsNullOrEmpty(textEditDestMoneyMore.Text))
            {
                XtraMessageBox.Show("请填写完整信息！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (int.Parse(textEditDestMoney.Text) < int.Parse(textEditDestMoneyMore.Text))
            {
                XtraMessageBox.Show("宝贝的加收运费必须为小于等于宝贝运费的数值！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Post post = new Post();
            post.dests = checkedComboBoxEditArea.Text;
            post.price = textEditDestMoney.Text;
            post.increase = textEditDestMoneyMore.Text;
            post.type = comboBoxEditPostageType.Text;
            post.postage_id = string.Empty;
            post.id = string.Empty;
            post.flag = "postageMode";
            post.postCode = System.Guid.NewGuid().ToString();
            post.isSelected = false;

            //在添加指定地区运费模板时，先判断该类型的默认运费是否已经添加
            bool HasPostage = false;
            foreach (Post ppost in listPost)
            {
                //默认运费已经添加
                if (ppost.type == post.type && ppost.dests == "默认全国")
                {
                    post.action = "new";
                    listPost.Add(post);
                    HasPostage = true;
                    break;
                }
            }
            //默认运费不存在
            if (!HasPostage)
            {
                XtraMessageBox.Show("在添加指定地区运费前须先添加默认运费！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                checkEditConform.Checked = false;
                return;
            }


            //展示新添加记录
            ShowPostageModel();
            //清空记录
            checkedComboBoxEditArea.Text = string.Empty;
            textEditDestMoney.Text = string.Empty;
            textEditDestMoneyMore.Text = string.Empty;
        }

        /// <summary>
        /// 添加默认运费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEditMoneyMore_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textEditDefaultMoney.Text))
            {
                XtraMessageBox.Show("请填写默认运费！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(textEditMoneyMore.Text))
            {
                XtraMessageBox.Show("请设置多一件宝贝时增加的运费！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                textEditMoneyMore.Focus();
                return;
            }
            if (int.Parse(textEditDefaultMoney.Text) >= 999.99)
            {
                XtraMessageBox.Show("运费必须为有效数值，且不得大于999.99元!", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (int.Parse(textEditDefaultMoney.Text) < int.Parse(textEditMoneyMore.Text) )
            {
                XtraMessageBox.Show("宝贝的加收运费必须为小于等于宝贝运费的数值!", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Post post = new Post();
            post.dests = "默认全国";
            post.id = string.Empty;
            post.type = comboBoxEditPostageType.Text;
            post.price = textEditDefaultMoney.Text;
            post.increase = textEditMoneyMore.Text;
            post.postage_id = string.Empty;
            post.flag = "postage";
            post.postCode = System.Guid.NewGuid().ToString();
            post.isSelected = false;
            post.action = "new";
            bool isExit = false;
            foreach (Post ppost in listPost)
            {
                if (ppost.type == post.type && post.dests == "默认全国")
                {
                    XtraMessageBox.Show(post.type+"的默认运费已经存在！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isExit = true;
                }
            }
            if (!isExit)
            {
                listPost.Add(post);
            }
            //展示新添加记录
            ShowPostageModel();
        }

        /// <summary>
        /// 添加运费记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEditConform_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditConform.Checked == true)
            {
                checkedComboBoxEditArea.Enabled = true;
                textEditDestMoney.Enabled = true;
                textEditDestMoneyMore.Enabled = true;
                simpleButtonMore.Enabled = true;
            }
            else
            {
                checkedComboBoxEditArea.Enabled = false;
                textEditDestMoney.Enabled = false;
                textEditDestMoneyMore.Enabled = false;
                simpleButtonMore.Enabled = false;
            }
        }

        #endregion

        #region 下载运费模板
        private void barButtonItemDownLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string session = SystemHelper.GetSessionKey(sellerNick);
                ItemRsp myRsp = TopService.PostagesGet(session);
                if (myRsp != null && myRsp.Postages != null)
                {
                    List<Alading.Entity.Postage> listPostage = new List<Alading.Entity.Postage>();
                    List<Alading.Entity.PostageMode> listPostageModel = new List<Alading.Entity.PostageMode>();
                    foreach (Alading.Taobao.Entity.Postage postagee in myRsp.Postages.Postage)
                    {
                        if (postagee != null)
                        {
                            Alading.Entity.Postage postAge = TaobaoPostageToAladingPostage(postagee);
                            foreach (Alading.Taobao.Entity.PostageMode postageMode in postagee.PostageModes.PostageMode)
                            {
                                if (postageMode != null)
                                {
                                    Alading.Entity.PostageMode postageModel = TaobaoPMToAladingPM(postageMode, postAge.postage_id);
                                    listPostageModel.Add(postageModel);
                                }
                            }
                            listPostage.Add(postAge);
                        }
                    }
                    //该处同时向两张表中添加数据，否则同时成功,用事务！
                    ReturnType PostageResult = PostageService.AddPAndPM(listPostage, listPostageModel);
                    if (PostageResult == ReturnType.Success)
                    {
                        XtraMessageBox.Show("本地数据库更新成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("本地数据库更新失败！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 删除运费模板
        /// <summary>
        /// 删除此模板 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = XtraMessageBox.Show("您确定要删除当前焦点行的运费模板吗？", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DialogResult.Cancel == DialogResult)
            {
                return;
            }
           WaitDialogForm waitForm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
           TreeListNode focusedNode = treeListPostage.FocusedNode;
           if (focusedNode != null && focusedNode.Tag != null)
           {
               string name = focusedNode.GetDisplayText(0);
               string postage_id = focusedNode.Tag.ToString();
               try
               {
                   string session = SystemHelper.GetSessionKey(sellerNick);
                   ItemRsp myRsp = TopService.PostageDelete(session, postage_id);
                   //删除本次邮费模板与子摸版
                   if (PostageService.RemovePtAndPtM(postage_id) == ReturnType.OthersError)
                   {
                       waitForm.Close();
                       XtraMessageBox.Show("该运费模板在本地删除失败！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                   }
                   else
                   {
                       waitForm.Close();
                       XtraMessageBox.Show("名称为" + name + "的运费模板删除成功！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                   }
               }
               catch (Exception ex)
               {
                   waitForm.Close();
                   if (ex.Message == Constants.POST_DELETED)
                   {
                       XtraMessageBox.Show("该运费模板已在淘宝上删除！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                       try
                       {
                           //删除本次邮费模板与子摸版
                           if (PostageService.RemovePtAndPtM(postage_id) == ReturnType.OthersError)
                           {
                               XtraMessageBox.Show("该运费模板在本地删除失败！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           }
                           else
                           {
                               XtraMessageBox.Show("该运费模板在本地删除成功！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           }
                       }
                       catch (Exception exx)
                       {
                           XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error); 
                       }

                   }
                   else
                   {
                       XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                   }
               }
           }

        }
        #endregion

        # region treeList操作
        private void barButtonItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //加载运费模板
                LoadPostage();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 选中此模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemChosen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode focusedNode = treeListPostage.FocusedNode;
            if (focusedNode != null)
            {
                this.Tag = focusedNode.GetDisplayText(0);
            }
            this.Close();
        }

        /// <summary>
        /// 模板焦点行改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListPostage_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                TreeListNode focusedNode = treeListPostage.FocusedNode;
                if (focusedNode != null && focusedNode.Tag != null)
                {
                    groupControlName.Text = focusedNode.GetDisplayText(0);
                    string postage_id = focusedNode.Tag.ToString();
                    Alading.Entity.Postage postage = PostageService.GetPostage(postage_id);
                    if (postage != null)
                    {
                        labelControlTime.Text = "最后编辑时间:";
                        labelControlTime.Text += postage.modified;
                    }
                    memoEditPostageH.Text = postage.memo;
                    //加载运费模板记录
                    gridControlHistory.DataSource = LoadPostageModels(postage);
                    gridViewPost.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 对gridviewNew操作

        # region 删除
        /// <summary>
        /// 删除运费模板记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemDeleteModel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (xtraTabPageHandle.Text == "新增运费模板")
                {
                    //新建运费模板时，删除误加的记录
                    DeletePInNew();
                }
                else
                {
                    //编辑运费模板时，保存被删除记录
                    DeletePInEdit();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 编辑运费模板时，保存被删除记录
        /// </summary>
        private void DeletePInEdit()
        {
            try
            {
                Post focusedPost = gridViewNew.GetFocusedRow() as Post;
                if (focusedPost != null)
                {
                    //当删除指定地区运费记录
                    if (focusedPost.isSelected == true && focusedPost.dests != "默认全国")
                    {
                        focusedPost.action = "delete";
                        listDPost.Add(focusedPost);
                        listPost.Remove(focusedPost);
                    }
                    else if (focusedPost.isSelected == true)
                    {
                        XtraMessageBox.Show("默认运费记录不能删除！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ShowPostageModel();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 新建运费模板时，删除误加的记录
        /// </summary>
        private void DeletePInNew()
        {
            try
            {
                Post focusedPost = gridViewNew.GetFocusedRow() as Post;
                if (focusedPost != null)
                {
                    //当删除指定地区运费记录
                    if (focusedPost.isSelected == true && focusedPost.dests != "默认全国")
                    {
                        listPost.Remove(focusedPost);
                    }
                    else if (focusedPost.isSelected == true)
                    {
                        //删除默认运费记录，同时也删除该类型的制定地区运费记录
                        List<Post> listpost = new List<Post>();
                        foreach (Post post in listPost)
                        {
                            if (post.type != focusedPost.type)
                            {
                                listpost.Add(post);
                            }
                        }
                        listPost = listpost;
                    }
                    ShowPostageModel();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 保存
        /// <summary>
        /// 模板保存并上传到淘宝
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (xtraTabPageHandle.Text == "新增运费模板")
                {
                    //新建Postage
                    NewPostage();
                }
                else
                {
                    //更新Postage
                    UpdatePostage();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 新建Postage
        /// </summary>
        private void NewPostage()
        {
            if (string.IsNullOrEmpty(textEditPostageName.Text))
            {
                XtraMessageBox.Show("请填写模板名称！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                textEditPostageName.Focus();
                return;
            }
            try
            {
                APIParameter apiParameter = BuildAPIParameterForNew();
                ItemRsp myRsp = TopService.PostageAdd(apiParameter.session, apiParameter.name, apiParameter.memo, apiParameter.post_price, apiParameter.post_increase, apiParameter.express_price, apiParameter.express_increase, 
                                                      apiParameter.ems_price, apiParameter.ems_increase,apiParameter.postage_mode_types, apiParameter.postage_mode_dests, apiParameter.postage_mode_prices, apiParameter.postage_mode_increases);
                if (myRsp != null && myRsp.Postage != null)
                {
                    string postageId = myRsp.Postage.Id;
                    bool isStored = GetPostage(postageId);
                    if (isStored)
                    {
                        XtraMessageBox.Show("模板新建成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("模板新建成功，但未成功保存到本地！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
                else
                {
                    XtraMessageBox.Show("模板新建失败！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    xtraTabControlPost.SelectedTabPage = xtraTabPageHistory;
                    xtraTabPageHandle.PageVisible = false;
                    return;
                }
                xtraTabControlPost.SelectedTabPage = xtraTabPageHistory;
                xtraTabPageHandle.PageVisible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新Postage
        /// </summary>
        private void UpdatePostage()
        {
            try
            {
                APIParameter apiParameter = BuildAPIParameterForEdit();
                ItemRsp myRsp = TopService.PostageUpdate(apiParameter.session, apiParameter.name, apiParameter.memo, apiParameter.postage_id, apiParameter.post_price, apiParameter.post_increase, apiParameter.express_price, apiParameter.express_increase, apiParameter.ems_price, apiParameter.ems_increase,
                                                         apiParameter.postage_mode_ids, apiParameter.postage_mode_types, apiParameter.postage_mode_dests, apiParameter.postage_mode_prices, apiParameter.postage_mode_increases, apiParameter.postage_mode_optTypes);
                if (myRsp != null && myRsp.Postage != null)
                {
                    string postageID = myRsp.Postage.Id;
                    //删除本地运费模板
                    ReturnType isDeleted = PostageService.RemovePostage(postageID);
                    if (isDeleted == ReturnType.Success)
                    {
                        XtraMessageBox.Show("运费模板已在淘宝上成功修改，但原始模板在本地修改失败！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        xtraTabControlPost.SelectedTabPage = xtraTabPageHistory;
                        xtraTabPageHandle.PageVisible = false;
                        return;
                    }
                    //从淘宝上下载刚修改的Postage并存储到数据库
                    bool isGot = GetPostage(postageID);
                    if (isGot == false)
                    {
                        XtraMessageBox.Show("运费模板已在淘宝上成功修改，但原始模板在更新到本地数据库时失败！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        xtraTabControlPost.SelectedTabPage = xtraTabPageHistory;
                        xtraTabPageHandle.PageVisible = false;
                        return;
                    }
                    XtraMessageBox.Show("运费模板更新成功！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                xtraTabControlPost.SelectedTabPage = xtraTabPageHistory;
                xtraTabPageHandle.PageVisible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        # region 公共方法
        /// <summary>
        /// 运费结构
        /// </summary>
        public class Post
        {
            public Post()
            {
                this.isSelected = false;
                this.action = "update";
            }
            public string flag
            {
                get;
                set;
            }
            public string postCode
            {
                get;
                set;
            }
            public string postage_id
            {
                get;
                set;
            }
            public string id
            {
                get;
                set;
            }
            public string type
            {
                get;
                set;
            }
            public string dests
            {
                get;
                set;
            }
            public string price
            {
                get;
                set;
            }
            public string increase
            {
                get;
                set;
            }
            public bool isSelected
            {
                get;
                set;
            }
            public string action
            {
                get;
                set;
            }
        }
        //淘宝API参数类
        public class APIParameter
        {
            public APIParameter()
            {
                session = string.Empty;
                name = string.Empty;
                memo = string.Empty;
                post_price = string.Empty;
                post_increase = string.Empty;
                express_price = string.Empty;
                express_increase = string.Empty;
                ems_price = string.Empty;
                ems_increase = string.Empty;
                postage_mode_types = string.Empty;
                postage_mode_dests = string.Empty;
                postage_mode_prices = string.Empty;
                postage_mode_increases = string.Empty;
                postage_mode_ids = string.Empty;
                postage_mode_optTypes = string.Empty;
                postage_id = string.Empty;
            }
            public string session
            {
                set;
                get;
            }
            public string name
            {
                set;
                get;
            }
            public string memo
            {
                set;
                get;
            }
            public string post_price
            {
                set;
                get;
            }
            public string post_increase
            {
                set;
                get;
            }
            public string express_price
            {
                set;
                get;
            }
            public string express_increase
            {
                set;
                get;
            }
            public string ems_price
            {
                set;
                get;
            }
            public string ems_increase
            {
                set;
                get;
            }
            public string postage_mode_types
            {
                set;
                get;
            }
            public string postage_mode_dests
            {
                set;
                get;
            }
            public string postage_mode_prices
            {
                set;
                get;
            }
            public string postage_mode_increases
            {
                set;
                get;
            }
            public string postage_mode_ids
            {
                set;
                get;
            }
            public string postage_mode_optTypes
            {
                set;
                get;
            }
            public string postage_id
            {
                set;
                get;
            }
        }

        /// <summary>
        /// 加载运费模板
        /// </summary>
        /// <param name="postage">运费模板</param>
        /// <returns>所有运费记录</returns>
        private List<Post> LoadPostageModels(Alading.Entity.Postage postage)
        {
            try
            {
                listPost.Clear();
                #region 加载默认运费
                //post默认运费
                if (!string.IsNullOrEmpty(postage.post_price))
                {
                    Post post = new Post();
                    post.postage_id = postage.postage_id;
                    post.dests = "默认全国";
                    post.flag = "postage";
                    post.price = postage.post_price;
                    post.increase = postage.post_increase;
                    post.postCode = System.Guid.NewGuid().ToString();
                    post.id = string.Empty;
                    post.type = "post";
                    listPost.Add(post);
                }
                //express默认运费
                if (!string.IsNullOrEmpty(postage.express_price))
                {
                    Post express = new Post();
                    express.postage_id = postage.postage_id;
                    express.dests = "默认全国";
                    express.flag = "postage";
                    express.price = postage.express_price;
                    express.increase = postage.express_increase;
                    express.postCode = System.Guid.NewGuid().ToString();
                    express.id = string.Empty;
                    express.type = "express";
                    listPost.Add(express);
                }
                //ems默认运费
                if (!string.IsNullOrEmpty(postage.ems_price))
                {
                    Post ems = new Post();
                    ems.postage_id = postage.postage_id;
                    ems.dests = "默认全国";
                    ems.flag = "postage";
                    ems.price = postage.ems_price;
                    ems.increase = postage.ems_increase;
                    ems.postCode = System.Guid.NewGuid().ToString();
                    ems.id = string.Empty;
                    ems.type = "ems";
                    listPost.Add(ems);
                }
                #endregion

                #region 加载指定地区运费
                List<Alading.Entity.PostageMode> listPostageMode = PostageModeService.GetPostageMode(postage.postage_id);
                if (listPostageMode != null)
                {
                    foreach (Alading.Entity.PostageMode postageMode in listPostageMode)
                    {
                        Post ppost = new Post();
                        ppost.flag = "postageMode";
                        ppost.id = postageMode.id.ToString();
                        ppost.increase = postageMode.increase;
                        ppost.price = postageMode.price;
                        ppost.type = postageMode.type;
                        ppost.postCode = System.Guid.NewGuid().ToString();

                        //根据地区ID获取地域名称
                        string[] areaIdArray = postageMode.dests.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        List<Alading.Entity.Area> listArea = GetDests(areaIdArray);
                        foreach (Alading.Entity.Area area in listArea)
                        {
                            ppost.dests += area.name + ",";
                        }
                        listPost.Add(ppost);
                    }
                }
                #endregion
                return listPost;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 构造API参数
        /// </summary>
        /// <returns></returns>
        private APIParameter BuildAPIParameterForNew()
        {
            try
            {
                APIParameter apiParameter = new APIParameter();
                string postage_mode_prices_post = string.Empty;
                string postage_mode_prices_ems = string.Empty;
                string postage_mode_prices_express = string.Empty;
                string postage_mode_increases_post = string.Empty;
                string postage_mode_increases_ems = string.Empty;
                string postage_mode_increases_express = string.Empty;
                string postage_mode_dests_post = string.Empty;
                string postage_mode_dests_ems = string.Empty;
                string postage_mode_dests_express = string.Empty;
                string postage_mode_type_post = string.Empty;
                string postage_mode_type_ems = string.Empty;
                string postage_mode_type_express = string.Empty;

                List<Alading.Entity.Area> listArea = AreaService.GetArea(p => p.parent_id == "1");
                apiParameter.name = textEditPostageName.Text;
                apiParameter.memo = memoEditPostage.Text;
                foreach (Post post in listPost)
                {
                    if (post.type == "post")
                    {
                        if (post.flag == "postageMode")
                        {
                            postage_mode_prices_post += post.price + ";";
                            postage_mode_increases_post += post.increase + ";";
                            postage_mode_dests_post += post.dests + ";";
                            postage_mode_type_post += "post" + ";";
                            continue;
                        }
                        if (post.flag == "postage")
                        {
                            apiParameter.post_price = post.price;
                            apiParameter.post_increase = post.increase;
                            continue;
                        }
                    }
                    if (post.type == "express")
                    {
                        if (post.flag == "postageMode")
                        {
                            postage_mode_prices_express += post.price + ";";
                            postage_mode_increases_express += post.increase + ";";
                            postage_mode_dests_express += post.dests + ";";
                            postage_mode_type_express += "express" + ";";
                            continue;
                        }
                        if (post.flag == "postage")
                        {
                            apiParameter.express_price = post.price;
                            apiParameter.express_increase = post.increase;
                            continue;
                        }
                    }
                    if (post.type == "ems")
                    {
                        if (post.flag == "postageMode")
                        {
                            postage_mode_prices_ems += post.price + ";";
                            postage_mode_increases_ems += post.increase + ";";
                            postage_mode_dests_ems += post.dests + ";";
                            postage_mode_type_ems += "ems" + ";";
                            continue;
                        }
                        if (post.flag == "postage")
                        {
                            apiParameter.ems_price = post.price;
                            apiParameter.ems_increase = post.increase;
                            continue;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(postage_mode_type_post))
                {
                    apiParameter.postage_mode_prices += postage_mode_prices_post;
                    apiParameter.postage_mode_increases += postage_mode_increases_post;
                    apiParameter.postage_mode_dests += GetProvinceCode(listArea, postage_mode_dests_post);
                }
                if (!string.IsNullOrEmpty(postage_mode_type_express))
                {
                    apiParameter.postage_mode_prices += postage_mode_prices_express;
                    apiParameter.postage_mode_increases += postage_mode_increases_express;
                    apiParameter.postage_mode_dests += GetProvinceCode(listArea, postage_mode_dests_express);
                }
                if (!string.IsNullOrEmpty(postage_mode_type_ems))
                {
                    apiParameter.postage_mode_prices += postage_mode_prices_ems;
                    apiParameter.postage_mode_increases += postage_mode_increases_ems;
                    apiParameter.postage_mode_dests += GetProvinceCode(listArea, postage_mode_dests_ems);
                }
                apiParameter.postage_mode_types = postage_mode_type_post + postage_mode_type_express + postage_mode_type_ems;
                if (!string.IsNullOrEmpty(apiParameter.postage_mode_prices))
                {
                    apiParameter.postage_mode_prices = apiParameter.postage_mode_prices.Substring(0, apiParameter.postage_mode_prices.Length - 1);
                }
                if (!string.IsNullOrEmpty(apiParameter.postage_mode_increases))
                {
                    apiParameter.postage_mode_increases = apiParameter.postage_mode_increases.Substring(0, apiParameter.postage_mode_increases.Length - 1);
                }
                if (!string.IsNullOrEmpty(apiParameter.postage_mode_dests))
                {
                    apiParameter.postage_mode_dests = apiParameter.postage_mode_dests.Substring(0, apiParameter.postage_mode_dests.Length - 1);
                }
                if (!string.IsNullOrEmpty(apiParameter.postage_mode_types))
                {
                    apiParameter.postage_mode_types = apiParameter.postage_mode_types.Substring(0, apiParameter.postage_mode_types.Length - 1);
                }
                apiParameter.session = SystemHelper.GetSessionKey(sellerNick);

                return apiParameter;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private APIParameter BuildAPIParameterForEdit()
        {
            try
            {
                APIParameter apiParameter = new APIParameter();
                apiParameter.name = textEditPostageName.Text;
                apiParameter.memo = memoEditPostage.Text;
                //获取被编辑运费模板id
                TreeListNode focuedNode = treeListPostage.FocusedNode;
                if (focuedNode != null && focuedNode.Tag != null)
                {
                    apiParameter.postage_id = focuedNode.Tag.ToString();
                }
                #region 获取所有运费记录
                //获取新增及被编辑后的运费记录
                List<Post> listpost = new List<Post>();
                for (int i = 0; i < gridViewNew.RowCount; i++)
                {
                    Post post = gridViewNew.GetRow(i) as Post;
                    listpost.Add(post);
                }
                listPost = listpost;
                //获取被删运费记录
                foreach (Post post in listDPost)
                {
                    listPost.Add(post);
                }
                #endregion
                List<Alading.Entity.Area> listArea = AreaService.GetArea(p => p.parent_id == "1");
                string postage_mode_ids_post = string.Empty;
                string postage_mode_types_post = string.Empty;
                string postage_mode_dests_post = string.Empty;
                string postage_mode_prices_post = string.Empty;
                string postage_mode_increases_post = string.Empty;
                string postage_mode_optTypes_post = string.Empty;

                string postage_mode_ids_express = string.Empty;
                string postage_mode_types_express = string.Empty;
                string postage_mode_dests_express = string.Empty;
                string postage_mode_prices_express = string.Empty;
                string postage_mode_increases_express = string.Empty;
                string postage_mode_optTypes_express = string.Empty;

                string postage_mode_ids_ems = string.Empty;
                string postage_mode_types_ems = string.Empty;
                string postage_mode_dests_ems = string.Empty;
                string postage_mode_prices_ems = string.Empty;
                string postage_mode_increases_ems = string.Empty;
                string postage_mode_optTypes_ems = string.Empty;

                foreach (Post post in listPost)
                {
                    #region post
                    if (post.type == "post")
                    {
                        if (post.flag == "postageMode")
                        {
                            postage_mode_types_post += post.type + ";";
                            postage_mode_prices_post += post.price + ";";
                            postage_mode_increases_post += post.increase + ";";
                            postage_mode_dests_post += GetProvinceCode(listArea, post.dests);
                            if (post.action == "delete")
                            {
                                postage_mode_optTypes_post += "delete" + ";";
                                postage_mode_ids_post += post.id + ";";
                            }
                            else if (post.action == "update")
                            {
                                postage_mode_optTypes_post += "update" + ";";
                                postage_mode_ids_post += post.id + ";";
                            }
                            else if (post.action == "new")
                            {
                                postage_mode_optTypes_post += "new" + ";";
                                postage_mode_ids_post += ";";
                            }
                            continue;
                        }
                        if (post.flag == "postage")
                        {
                            apiParameter.post_price = post.price;
                            apiParameter.post_increase = post.increase;
                            continue;
                        }
                    }
                    #endregion
                    #region express
                    if (post.type == "express")
                    {
                        if (post.flag == "postageMode")
                        {
                            postage_mode_types_express += post.type + ";";
                            postage_mode_prices_express += post.price + ";";
                            postage_mode_increases_express += post.increase + ";";
                            postage_mode_dests_express += GetProvinceCode(listArea, post.dests);
                            if (post.action == "delete")
                            {
                                postage_mode_optTypes_express += "delete" + ";";
                                postage_mode_ids_express += post.id + ";";
                            }
                            else if (post.action == "update")
                            {
                                postage_mode_optTypes_express += "update" + ";";
                                postage_mode_ids_express += post.id + ";";
                            }
                            else if (post.action == "new")
                            {
                                postage_mode_optTypes_express += "new" + ";";
                                postage_mode_ids_express += ";";
                            }
                            continue;
                        }
                        if (post.flag == "postage")
                        {
                            apiParameter.express_price = post.price;
                            apiParameter.express_increase = post.increase;
                            continue;
                        }
                    }
                    #endregion
                    #region ems
                    if (post.type == "ems")
                    {
                        if (post.flag == "postageMode")
                        {
                            postage_mode_types_ems += post.type + ";";
                            postage_mode_prices_ems += post.price + ";";
                            postage_mode_increases_ems += post.increase + ";";
                            postage_mode_dests_ems += GetProvinceCode(listArea, post.dests);
                            if (post.action == "delete")
                            {
                                postage_mode_optTypes_ems += "delete" + ";";
                                postage_mode_ids_ems += post.id + ";";
                            }
                            else if (post.action == "update")
                            {
                                postage_mode_optTypes_ems += "update" + ";";
                                postage_mode_ids_ems += post.id + ";";
                            }
                            else if (post.action == "new")
                            {
                                postage_mode_optTypes_ems += "new" + ";";
                                postage_mode_ids_ems += ";";
                            }
                            continue;
                        }
                        if (post.flag == "postage")
                        {
                            apiParameter.ems_price = post.price;
                            apiParameter.ems_increase = post.increase;
                            continue;
                        }
                    }
                    #endregion
                }
                apiParameter.postage_mode_ids = postage_mode_ids_post + postage_mode_ids_express + postage_mode_ids_ems;
                apiParameter.postage_mode_ids = apiParameter.postage_mode_ids.Substring(0, apiParameter.postage_mode_ids.Length - 1);
                apiParameter.postage_mode_dests = postage_mode_dests_post + postage_mode_dests_express + postage_mode_dests_ems;
                apiParameter.postage_mode_dests = apiParameter.postage_mode_dests.Substring(0, apiParameter.postage_mode_dests.Length - 1);
                apiParameter.postage_mode_types = postage_mode_types_post + postage_mode_types_express + postage_mode_types_ems;
                apiParameter.postage_mode_types = apiParameter.postage_mode_types.Substring(0, apiParameter.postage_mode_types.Length - 1);
                apiParameter.postage_mode_prices = postage_mode_prices_post + postage_mode_prices_express + postage_mode_prices_ems;
                apiParameter.postage_mode_prices = apiParameter.postage_mode_prices.Substring(0, apiParameter.postage_mode_prices.Length - 1);
                apiParameter.postage_mode_increases = postage_mode_increases_post + postage_mode_increases_express + postage_mode_increases_ems;
                apiParameter.postage_mode_increases = apiParameter.postage_mode_increases.Substring(0, apiParameter.postage_mode_increases.Length - 1);
                apiParameter.postage_mode_optTypes = postage_mode_optTypes_post + postage_mode_optTypes_express + postage_mode_optTypes_ems;
                apiParameter.postage_mode_optTypes = apiParameter.postage_mode_optTypes.Substring(0, apiParameter.postage_mode_optTypes.Length - 1);
                apiParameter.session = SystemHelper.GetSessionKey(sellerNick);
                return apiParameter;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        
        }

        /// <summary>
        /// 加载地域信息
        /// </summary>
        private void LoadArea()
        {
            try
            {
                List<Alading.Entity.Area> listArea = AreaService.GetArea(p => p.parent_id == "1");
                foreach (Alading.Entity.Area area in listArea)
                {
                    checkedComboBoxEditArea.Properties.Items.Add(area.name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 加载运费模板
        /// </summary>
        private void LoadPostage()
        {
            try
            {
                treeListPostage.ClearNodes();
                List<Alading.Entity.Postage> listPostage = PostageService.GetAllPostage();
                if (listPostage != null)
                {
                    foreach (Alading.Entity.Postage postage in listPostage)
                    {
                        treeListPostage.AppendNode(new object[] { postage.name }, null, postage.postage_id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据地区ID获取地域名称
        /// </summary>
        /// <param name="areaIdArray">地域ID</param>
        /// <returns>返回地域列表</returns>
        private List<Alading.Entity.Area> GetDests(string[] areaIdArray)
        {
            try
            {
                List<Alading.Entity.Area> listArea = new List<Alading.Entity.Area>();
                List<string> listAreaId = new List<string>();
                if (areaIdArray.Length != 0)
                {
                    foreach (string area in areaIdArray)
                    {
                        if (!string.IsNullOrEmpty(area))
                        {
                            listAreaId.Add(area);
                        }
                    }
                    listArea = AreaService.GetArea(listAreaId);
                }
                return listArea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 展示新添加记录
        /// </summary>
        /// <param name="post"></param>
        private void ShowPostageModel()
        {
            gridControlNew.DataSource = null;
            gridControlNew.DataSource = listPost;
            gridViewNew.BestFitColumns();
        }

        /// <summary>
        /// 设置焦点行复选框状态为选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewNew_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "isSelected")
            {
                gridViewNew.SetFocusedRowCellValue("isSelected", e.Value);
            }
        }

        /// <summary>
        /// 获取单个运费模板
        /// </summary>
        /// <param name="postage_id">运费模板ID</param>
        private bool GetPostage(string postage_id)
        {
            try
            {
                bool isStored = false;
                string session = SystemHelper.GetSessionKey(sellerNick);
                ItemRsp myRsp = TopService.PostageGet(session, postage_id, sellerNick);
                if (myRsp != null && myRsp.Postage != null)
                {
                    Alading.Taobao.Entity.Postage postage = myRsp.Postage;
                    //将淘宝Postage转化为Alading的Postage
                    Alading.Entity.Postage postAge = TaobaoPostageToAladingPostage(postage);
                    List<Alading.Entity.PostageMode> listPostageModel = new List<Alading.Entity.PostageMode>();
                    foreach (Alading.Taobao.Entity.PostageMode postageMode in postage.PostageModes.PostageMode)
                    {
                        if (postageMode != null)
                        {
                            //将淘宝PostageMode转化为Alading的PostageMode
                            Alading.Entity.PostageMode postageModel = TaobaoPMToAladingPM(postageMode, postage.Id);
                            listPostageModel.Add(postageModel);
                        }
                    }
                    //向数据库同时添加运费模板与子模板，若已存在则更新
                    List<Alading.Entity.Postage> listPostage = new List<Alading.Entity.Postage>();
                    listPostage.Add(postAge);
                    ReturnType isBuilt = PostageService.AddPAndPM(listPostage, listPostageModel);
                    if (isBuilt == ReturnType.Success)
                    {
                        isStored = true;
                    }
                }
                return isStored;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取制定省的代码
        /// </summary>
        /// <param name="listArea">所有省</param>
        /// <param name="areaStr">制定省列表</param>
        /// <returns></returns>
        private string GetProvinceCode(List<Alading.Entity.Area> listArea, string areaStr)
        {
            try
            {
                string[] areaArray = areaStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string areasCodes = string.Empty;

                foreach (string area in areaArray)
                {
                    if (!string.IsNullOrEmpty(area.Trim()))
                    {
                        string[] areaarray = area.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        string areaCode = string.Empty;
                        foreach (string aarea in areaarray)
                        {
                            if (!string.IsNullOrEmpty(aarea))
                            {
                                foreach (Alading.Entity.Area aaarea in listArea)
                                {
                                    if (aaarea.name == aarea.Trim())
                                    {
                                        areaCode += aaarea.id + ",";
                                    }
                                }
                            }
                        }
                        areasCodes += areaCode.Substring(0, areaCode.Length - 1) + ";";
                    }
                }
                return areasCodes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将淘宝postageMode转化为Alading的postageMode
        /// </summary>
        /// <param name="postageMode"></param>
        /// <param name="postageId"></param>
        /// <returns></returns>
        private Alading.Entity.PostageMode TaobaoPMToAladingPM(Alading.Taobao.Entity.PostageMode postageMode, string postageId)
        {
            Alading.Entity.PostageMode postageModel = new Alading.Entity.PostageMode();
            postageModel.postage_id = postageId;
            postageModel.id = int.Parse(postageMode.Id);
            postageModel.dests = postageMode.Dests;
            postageModel.increase = postageMode.Increase;
            postageModel.type = postageMode.Type;
            postageModel.price = postageMode.Price;
            return postageModel;
        }

        /// <summary>
        /// 将淘宝Postage转化为Alading的Postage
        /// </summary>
        /// <param name="postage">淘宝Entity</param>
        /// <returns>Alading的Entity</returns>
        private Alading.Entity.Postage TaobaoPostageToAladingPostage(Alading.Taobao.Entity.Postage postage)
        {
            Alading.Entity.Postage postAge = new Alading.Entity.Postage();
            postAge.created = postage.Created;
            postAge.memo = postage.Memo ?? string.Empty;
            postAge.modified = postage.Modified ?? string.Empty;
            postAge.name = postage.Name;
            postAge.postage_id = postage.Id;
            postAge.ems_increase = postage.EmsIncrease ?? string.Empty;
            postAge.ems_price = postage.EmsPrice ?? string.Empty;
            postAge.express_increase = postage.ExpressIncrease ?? string.Empty;
            postAge.express_price = postage.ExpressPrice ?? string.Empty;
            postAge.post_increase = postage.PostIncrease ?? string.Empty;
            postAge.post_price = postage.PostPrice ?? string.Empty;
            postAge.postage_modes = JsonConvert.SerializeObject(postage.PostageModes) ?? string.Empty;
            return postAge;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraTabPageHandle.PageVisible = false;
            gridControlNew.DataSource = null;
            textEditPostageName.Text = string.Empty;
            memoEditPostage.Text = string.Empty;
            textEditDefaultMoney.Text = string.Empty;
            textEditMoneyMore.Text = string.Empty;
            checkedComboBoxEditArea.Text = string.Empty;
            textEditDestMoney.Text = string.Empty;
            textEditDestMoneyMore.Text = string.Empty;
        }
        # endregion       

    }
}