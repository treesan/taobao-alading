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
    public partial class EmailToConsumer : DevExpress.XtraEditors.XtraForm
    {
        public bool SendEmailState { get; set; }

        private List<Alading.Entity.Consumer> sendtolist = new List<Alading.Entity.Consumer>();

        private List<Alading.Entity.EmailTemplate> templateList = new List<Alading.Entity.EmailTemplate>();
        private List<Alading.Entity.EmailTemplateCat> templateCatList = new List<Alading.Entity.EmailTemplateCat>();
        private List<Alading.Entity.Shop> shopList = new List<Alading.Entity.Shop>();

        private int selectedCatIndex = -1;
        private int selectedTempIndex = -1;
        private int selectedShopIndex = -1;

        private Alading.Entity.EmailTemplateCat selectedCat = null;
        private Alading.Entity.EmailTemplate selectedTemp = null;
        private Alading.Entity.Shop selectedShop = null;

        // Constructor
        public EmailToConsumer()
        {
            InitializeComponent();

            dateEdit1.DateTime = DateTime.Now;
            timeEdit1.Time = DateTime.Now;

            InitTempCatList();
            InitShopList();
        }

        public List<Alading.Entity.Consumer> Receivers
        {
            get { return sendtolist; }
            set
            {
                sendtolist = value;
                SetReceiverList();
            }
        }

        #region initialize

        /// <summary>
        /// initialize template category
        /// </summary>
        private void InitTempCatList()
        {
            templateCatList = Alading.Business.EmailTemplateCatService.GetAllEmailTemplateCat();

            cbTempCat.Properties.Items.Clear();
            cbTempCat.Properties.Items.Add("<不使用>");
            cbTempCat.Properties.Items.Add("所有模板分类");
            foreach (var i in templateCatList)
            {
                cbTempCat.Properties.Items.Add(i.Name);
            }
            cbTempCat.SelectedIndex = 0;
        }

        /// <summary>
        /// initialize template
        /// </summary>
        private void InitTempList()
        {
            cbTemp.Properties.Items.Clear();

            // get template list
            if (selectedCatIndex == 0)
            {
                templateList = null;
            }
            else if (selectedCatIndex == 1)
            {
                templateList = Alading.Business.EmailTemplateService.GetAllEmailTemplate();
            }
            else if (selectedCat != null)
            {
                templateList = Alading.Business.EmailTemplateService.GetEmailTemplate(c => c.Type == selectedCat.Code);
            }

            // bind data
            if (templateList != null && templateList.Count > 0)
            {
                foreach (var i in templateList)
                {
                    cbTemp.Properties.Items.Add(i.Name);
                }
                cbTemp.SelectedIndex = 0;
            }
            else
            {
                cbTemp.SelectedIndex = -1;
                txtTitle.Text = string.Empty;
                htxContent.BodyHtml = string.Empty;
            }
        }

        /// <summary>
        /// initialize shop
        /// </summary>
        private void InitShopList()
        {
            cbShopList.Properties.Items.Clear();
            shopList = Alading.Business.ShopService.GetAllShop();

            cbShopList.Properties.Items.Add("<不使用>");
            foreach (var i in shopList)
            {
                cbShopList.Properties.Items.Add(i.title);
            }
            cbShopList.SelectedIndex = 0;
        } 

        #endregion

        //public string SenderAddress { get; set; }
        //public string SmtpServerAddress { get; set; }
        //public string SmtpServerAccount { get; set; }
        //public string SmtpServerPassword { get; set; }        

        private void SetReceiverList()
        {
            if (sendtolist != null)
            {
                StringBuilder builder = new StringBuilder();
                foreach (var i in sendtolist)
                {
                    if (!string.IsNullOrEmpty(i.email))
                    {
                        builder.AppendLine(string.Format("{0}<{1}>; ", i.nick, i.email));
                    }
                }
                txtReceivers.Text = builder.ToString();
            }
        }

        private void sbSend_Click(object sender, EventArgs e)
        {
            //SendEmailState = SendEmail.Send(
            //    SenderAddress, ReceiverAddress, 
            //    txtTitle.Text, htmlEditorMsg.PrintingRichEditControl.HtmlText, 
            //    SmtpServerAddress, SmtpServerAccount, SmtpServerPassword);

            //if (SendEmailState) XtraMessageBox.Show("邮件发送成功！");
            //else XtraMessageBox.Show("邮件发送失败！");

            DateTime day = dateEdit1.DateTime;
            DateTime time = timeEdit1.Time;

            EmailTaskBuilder taskbuilder = new EmailTaskBuilder();
            taskbuilder.Receivers = this.Receivers;
            taskbuilder.TaskStartTime = new DateTime(day.Year, day.Month, day.Day, time.Hour, time.Minute, time.Second);
            taskbuilder.EmailSubject = txtTitle.Text;
            taskbuilder.EmailContentTemplate = htxContent.BodyHtml;
            taskbuilder.Shop = selectedShop;

            EmailTaskProcess process = new EmailTaskProcess();
            process.TaskBuilder = taskbuilder;
            if (DialogResult.OK == process.ShowDialog())
            {
                XtraMessageBox.Show("所有邮件已成功添加到邮件发送队列，等待稍候发送！");
            }
        }

        private void requiredText_EditValueChanged(object sender, EventArgs e)
        {
            sbSend.Enabled = (!string.IsNullOrEmpty(txtTitle.Text)) && (!string.IsNullOrEmpty(htxContent.BodyHtml));
        }

        /// <summary>
        /// selected template category changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTempCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCatIndex = cbTempCat.SelectedIndex;

            if (selectedCatIndex > 1)
            {
                selectedCat = templateCatList[selectedCatIndex - 2];
            }
            else
            {
                selectedCat = null;
            }

            InitTempList();
        }

        /// <summary>
        /// selected template changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTemp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTemp.SelectedIndex >= 0)
            {
                selectedTempIndex = cbTemp.SelectedIndex;
                selectedTemp = templateList[selectedTempIndex];

                txtTitle.Text = selectedTemp.Title;
                htxContent.BodyHtml = selectedTemp.Content;
            }
        }

        /// <summary>
        /// selected shop changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbShopList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbShopList.SelectedIndex > 0)
            {
                selectedShopIndex = cbShopList.SelectedIndex;
                if (selectedShopIndex > 0)
                {
                    selectedShop = shopList[selectedShopIndex - 1];
                }
                else
                {
                    selectedShop = null;
                }
            }
        }

        private void btSetting_Click(object sender, EventArgs e)
        {
            Alading.Forms.Email.SmtpSetting dialog = new Alading.Forms.Email.SmtpSetting();
            dialog.SmtpConfiguration = Alading.Forms.Email.SmtpConfigurationProvider.Configuration;
            if (DialogResult.OK == dialog.ShowDialog())
            {
                Alading.Forms.Email.SmtpConfigurationProvider.Configuration = dialog.SmtpConfiguration;
                Alading.Forms.Email.SmtpConfigurationProvider.Save();
            }
        }

        private void EmailToConsumer_FormClosing(object sender, FormClosingEventArgs e)
        {
            //必须在这里Dispose Html控件，因为里面有个timer还在继续工作，不处理会抛出异常
            //试图吊销一个未注册的拖放目标 (异常来自 HRESULT:0x80040100 (DRAGDROP_E_NOTREGISTERED))
            htxContent.Dispose();
        }
    }
}