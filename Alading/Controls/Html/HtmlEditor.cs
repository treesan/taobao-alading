using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraRichEdit.Internal;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.Utils.Menu;

namespace Alading.Controls.Html
{
    [ToolboxItem(true)]
    public partial class HtmlEditor : HtmlControl
    {
        public HtmlEditor()
        {
            InitializeComponent();
            IDocumentImportManagerService service = richEditControl.GetService<IDocumentImportManagerService>();
            if (service != null) {
                service.UnregisterImporter(service.GetImporter(DocumentFormat.PlainText));
                service.UnregisterImporter(service.GetImporter(DocumentFormat.Rtf));
                service.UnregisterImporter(service.GetImporter(DocumentFormat.Mht));
                service.UnregisterImporter(service.GetImporter(DocumentFormat.OpenXml));
            }
        }

        public override RichEditControl PrintingRichEditControl { get { return richEditControl; } }

        private void richEditControl_Click(object sender, EventArgs e)
        {

        } 
    }
}
