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
    public partial class MacroEditor : DevExpress.XtraEditors.XtraForm
    {
        public MacroEditor()
        {
            InitializeComponent();
        }

        public string MacroText
        {
            get { return txMacro.Text; }
        }

        private void macro_Click(object sender, EventArgs e)
        {
            SimpleButton sb = sender as SimpleButton;
            if (sb != null)
            {
                txMacro.Text = string.Format("@{0}@", sb.Text);
            }
        }

        private void txMacro_EditValueChanged(object sender, EventArgs e)
        {
            btOK.Enabled = !string.IsNullOrEmpty(txMacro.Text);
        }
    }
}