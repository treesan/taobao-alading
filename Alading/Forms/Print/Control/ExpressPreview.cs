using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Alading.Forms.Print.Control
{
    [ToolboxItem(true)]
    public partial class ExpressPreview : DevExpress.XtraEditors.XtraUserControl
    {
        public ExpressPreview()
        {
            InitializeComponent();
        }

        #region 公开控件
        public DevExpress.XtraPrinting.PrintingSystem PrintingSystem
        {
            get {
                return this.printingSystem;
            }
        }

        public DevExpress.XtraPrinting.Control.PrintControl PrintControl
        {
            get
            {
                return this.printControl;
            }
        }
        #endregion
    }    
}
