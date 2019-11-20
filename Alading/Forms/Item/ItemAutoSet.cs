using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Alading.Taobao.Entity.Extend;
using Alading.Utils;
using Alading.Taobao.API;
using Alading.Business;
using System.Configuration;

namespace Alading.Forms.Item
{
    public partial class ItemAutoSet : DevExpress.XtraEditors.XtraForm
    {
        private System.Timers.Timer autoListTimer = new System.Timers.Timer();
        private System.Timers.Timer autoRecommedTimer = new System.Timers.Timer();

        public ItemAutoSet()
        {
            InitializeComponent();
        }

        public ItemAutoSet(System.Timers.Timer timerList, System.Timers.Timer timerRecommend)
        {
            InitializeComponent();
            this.autoListTimer = timerList;
            this.autoRecommedTimer = timerRecommend;
        }

        private void ItemAutoSet_Load(object sender, EventArgs e)
        {
            this.checkButtonList.Checked = this.autoListTimer.Enabled;
            this.checkButtonRecommend.Checked = this.checkButtonRecommend.Checked;
            spinEditList.EditValue = SystemHelper.THREAD_LIST_INTERVAL/60000;
            spinEditRecommend.EditValue = SystemHelper.THREAD_RECOMMEND_INTERVAL/60000;
        }

        private void checkButtonList_CheckedChanged(object sender, EventArgs e)
        {
            if (checkButtonList.Checked == true)
            {
                checkButtonList.ImageIndex = 0;
                checkButtonList.Text = "已开启，点击关闭";
                this.autoListTimer.Start();
            }
            else
            {
                checkButtonList.ImageIndex = 1;
                checkButtonList.Text = "已关闭，点击开启";
                this.autoRecommedTimer.Stop();
            }
        }

        private void checkButtonRecommend_CheckedChanged(object sender, EventArgs e)
        {
            if (checkButtonRecommend.Checked == true)
            {
                checkButtonRecommend.ImageIndex = 0;
                checkButtonRecommend.Text = "已开启，点击关闭";
              
                this.autoRecommedTimer.Start();
            }
            else
            {
                checkButtonRecommend.ImageIndex = 1;
                checkButtonRecommend.Text = "已关闭，点击开启";
                this.autoRecommedTimer.Stop();
            }
        }

        private void spinEditList_EditValueChanged(object sender, EventArgs e)
        {
            this.autoListTimer.Interval = double.Parse(spinEditList.EditValue.ToString()) * 60000;
            SystemHelper.THREAD_LIST_INTERVAL = double.Parse(spinEditList.EditValue.ToString()) * 60000;
        }

        private void spinEditRecommend_EditValueChanged(object sender, EventArgs e)
        {
            this.autoRecommedTimer.Interval = double.Parse(spinEditRecommend.EditValue.ToString()) * 60000;
            SystemHelper.THREAD_RECOMMEND_INTERVAL = double.Parse(spinEditRecommend.EditValue.ToString()) * 60000;
        }
      
    }
}