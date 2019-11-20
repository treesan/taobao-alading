using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Alading.Forms.Email
{
    public partial class CatEditor : DevExpress.XtraEditors.XtraForm
    {
        public CatEditor()
        {
            InitializeComponent();
        }

        public string CatName
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        private void txtName_EditValueChanged(object sender, EventArgs e)
        {
            btOK.Enabled = !string.IsNullOrEmpty(txtName.Text);
        }
    }
}