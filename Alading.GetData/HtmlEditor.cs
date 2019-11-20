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
            this.fileNewItem1.Glyph.Save("c:\\fileNew.bmp");
            this.fileOpenItem1.Glyph.Save("c:\\fileOpen.bmp");
            this.fileSaveAsItem1.Glyph.Save("c:\\fileSaveAs.bmp");
            this.fileSaveItem1.Glyph.Save("c:\\fileSave.bmp");
            this.undoItem1.Glyph.Save("c:\\undo.bmp");
            this.redoItem1.Glyph.Save("c:\\redo.bmp");
            this.cutItem1.Glyph.Save("c:\\cut.bmp");
            this.copyItem1.Glyph.Save("c:\\copy.bmp");
            this.pasteItem1.Glyph.Save("c:\\paste.bmp");
            this.changeFontColorItem1.Glyph.Save("c:\\fontColor.bmp");
            this.toggleFontBoldItem1.Glyph.Save("c:\\fontBold.bmp");
            this.toggleFontItalicItem1.Glyph.Save("c:\\fontItalic.bmp");
            this.toggleFontUnderlineItem1.Glyph.Save("c:\\fontUnderline.bmp");
            this.insertPictureItem1.Glyph.Save("c:\\insertPicture.bmp");

            this.toggleFontBoldItem1.Glyph.Save("c:\\toggleFontBold.bmp");
        } 
    }
}
