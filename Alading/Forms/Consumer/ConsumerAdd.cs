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
    public partial class ConsumerAdd : DevExpress.XtraEditors.XtraForm
    {
        private List<Alading.Entity.Area> provinceList = new List<Alading.Entity.Area>();
        private List<Alading.Entity.Area> cityList = new List<Alading.Entity.Area>();
        private List<Alading.Entity.Area> countyList = new List<Alading.Entity.Area>();

        private int selectedProvinceIndex = 0;
        private int selectedCityIndex = 0;
        private int selectedCountyIndex = 0;
        private int selectedSource = 0;
        private int selectedGrade = 0;

        public ConsumerAdd()
        {
            InitializeComponent();
        }

        public Alading.Entity.Consumer GetNewConsumer()
        {
            //TODO: (WXC) 将界面上的用户信息封装成对象
            Alading.Entity.Consumer consumer = new Alading.Entity.Consumer();
            //属性赋值...
            //第一组属性
            consumer.source = selectedSource;
            switch (selectedGrade)
            {
                case 0: consumer.vip = false; consumer.is_dealer = false; break;
                case 1: consumer.vip = true; consumer.is_dealer = false; break;
                case 2: consumer.vip = false; consumer.is_dealer = true; break;
            }
            consumer.nick = txConsumerName.Text;
            consumer.alipay = txZhifubao.Text;
            consumer.score = Convert.ToInt32(txJifen.Text);
            consumer.level = cbLevel.SelectedIndex;
            //第二组属性
            consumer.buyer_name = txBuyer.Text;
            consumer.location_city = cbCity.Text;
            consumer.location_state = cbProvince.Text;
            consumer.buyer_zip = txZipcode.Text;
            consumer.location_district = cbCounty.Text;
            consumer.location_address = txAddr.Text;
            //第三组属性
            consumer.mobilephone = txMobile.Text;
            consumer.email = txEmail.Text;
            consumer.phone = txTel.Text;
            consumer.comments = htDesc.PrintingRichEditControl.Text;
            consumer.buyer_wangwang = txWangwang.Text;
            return consumer;
        }

        private bool ValidateInput()
        {
            return (!string.IsNullOrEmpty(txConsumerName.Text)) && (!string.IsNullOrEmpty(txBuyer.Text));
        }

        private void ConsumerAdd_Load(object sender, EventArgs e)
        {
            //TODO: (WXC) 初始化省列表
            provinceList = Alading.Business.AreaService.GetAreas("1");
            SetControlItems(cbProvince, provinceList);
        }

        private void SetControlItems(ComboBoxEdit control, List<Alading.Entity.Area> data)
        {
            control.Properties.Items.Clear();

            foreach (var i in data)
            {
                control.Properties.Items.Add(i.name);
            }

            control.SelectedIndex = 0;
        }

        private void cbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProvince.SelectedIndex >= 0)
            {
                selectedProvinceIndex = cbProvince.SelectedIndex;
                Alading.Entity.Area selectedArea = provinceList[selectedProvinceIndex];
                cityList = Alading.Business.AreaService.GetAreas(selectedArea.id);
                SetControlItems(cbCity, cityList);                
            }
        }

        private void cbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCity.SelectedIndex >= 0)
            {
                selectedCityIndex = cbCity.SelectedIndex;
                Alading.Entity.Area selectedArea = cityList[selectedCityIndex];
                countyList = Alading.Business.AreaService.GetAreas(selectedArea.id);
                SetControlItems(cbCounty, countyList);
            }
        }

        private void cbSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLevel.SelectedIndex >= 0)
            {
                selectedSource = cbLevel.SelectedIndex;
            }
        }

        private void cbGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGrade.SelectedIndex >= 0)
            {
                selectedGrade = cbGrade.SelectedIndex;
            }
        }

        private void txRequired_EditValueChanged(object sender, EventArgs e)
        {
            btSave.Enabled = ValidateInput();
        }
    }
}