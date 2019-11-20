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
    public partial class ConsumerAddressAdd : DevExpress.XtraEditors.XtraForm
    {
        private int selectedProvinceIndex;
        private int selectedCityIndex;
        private int selectedCountyIndex;
        private int selectedCountryIndex;
        private string selectedCountry;

        private List<Alading.Entity.Area> provinceList = new List<Alading.Entity.Area>();
        private List<Alading.Entity.Area> cityList = new List<Alading.Entity.Area>();
        private List<Alading.Entity.Area> countyList = new List<Alading.Entity.Area>();

        public ConsumerAddressAdd()
        {
            InitializeComponent();

            cbCountry.SelectedIndex = 0;

            provinceList = Alading.Business.AreaService.GetAreas("1");
            SetControlItems(cbProvince, provinceList);
        }

        public string ConsumerCountry
        {
            get { return cbCountry.Properties.Items[selectedCountryIndex].ToString(); }
        }

        public string ConsumerNick
        {
            get { return teNickName.Text; }
            set { teNickName.Text = value; }
        }

        public string ConsumerProvince
        {
            get
            {
                if (selectedProvinceIndex >= 0) { return provinceList[selectedProvinceIndex].name; }
                else { return string.Empty; }
            }
            set
            {
                int index = provinceList.FindIndex(c => c.name == value);
                cbProvince.SelectedIndex = index;
            }
        }

        public string ConsumerCity
        {
            get
            {
                if (selectedCityIndex >= 0) { return cityList[selectedCityIndex].name; }
                else { return string.Empty; }
            }
            set
            {
                int index = cityList.FindIndex(c => c.name == value);
                cbCity.SelectedIndex = index;
            }
        }

        public string ConsumerCounty
        {
            get
            {
                if (selectedCountyIndex >= 0) { return countyList[selectedCountyIndex].name; }
                else { return string.Empty; }
            }
            set
            {
                int index = countyList.FindIndex(c => c.name == value);
                cbCounty.SelectedIndex = index;
            }
        }

        public string ConsumerAddress
        {
            get { return txAddr.Text; }
            set { txAddr.Text = value; }
        }

        public string ConsumerZip
        {
            get { return txZip.Text; }
            set { txZip.Text = value; }
        }
        
        /// <summary>
        /// Change province event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Change city event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Change country event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCountry.SelectedIndex >= 0)
            {
                selectedCountryIndex = cbCountry.SelectedIndex;
            }
        }

        /// <summary>
        /// Fill combobox data
        /// </summary>
        /// <param name="control"></param>
        /// <param name="data"></param>
        private void SetControlItems(ComboBoxEdit control, List<Alading.Entity.Area> data)
        {
            control.Properties.Items.Clear();

            foreach (var i in data)
            {
                control.Properties.Items.Add(i.name);
            }

            control.SelectedIndex = 0;
        }
    }
}