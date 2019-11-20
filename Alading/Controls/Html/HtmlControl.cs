using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using System.Drawing.Imaging;
using System.Drawing.Text;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.Skins;
using DevExpress.XtraRichEdit;
using DevExpress.Utils;

namespace Alading.Controls.Html
{
    public class HtmlControl : HtmlControlBase, IDXMenuManager
    {
        private LookAndFeelMenu menu = null;
        IDXMenuManager fMenuManager;

        public HtmlControl() { }

		public virtual RichEditControl PrintingRichEditControl { get { return null; } }

		public LookAndFeelMenu DemoMainMenu {
			get { return menu; }
			set {
				if(menu == value) return;
				this.menu = value;
			}
		}

        void IDXMenuManager.ShowPopupMenu(DXPopupMenu menu, Control control, Point pos) {
            MenuManagerHelper.ShowMenu(menu, LookAndFeel, fMenuManager, control, pos);
        }
		IDXMenuManager IDXMenuManager.Clone(Form newForm) { return this; }
		void IDXMenuManager.DisposeManager() { }

        public IDXMenuManager MenuManager {
            get { return fMenuManager; }
            set { fMenuManager = value; }
        }

		public virtual bool ShowOptions { get { return false; }}		
    }
}
