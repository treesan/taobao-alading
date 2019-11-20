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
    public partial class TxtMsgToConsumer : DevExpress.XtraEditors.XtraForm
    {
        public string Receiver { get; set; }
        public string ReceiverPhone { get; set; }

        public TxtMsgToConsumer()
        {
            InitializeComponent();
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            //TODO: 发送短信
        }

        private void TxtMsgToConsumer_Load(object sender, EventArgs e)
        {
            txtNickName.Text = string.Format("{0}[{1}];", Receiver, ReceiverPhone);
        }

        private void meTxtMsg_EditValueChanged(object sender, EventArgs e)
        {
            sbSend.Enabled = !string.IsNullOrEmpty(meTxtMsg.Text);
        }
    }
}