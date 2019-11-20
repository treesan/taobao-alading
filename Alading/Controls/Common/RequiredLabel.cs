using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alading.Controls.Common
{
    public partial class RequiredLabel : Label
    {
        public RequiredLabel()
            : base()
        {
            this.AutoSize = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            string required = "[*]";
            SizeF size = e.Graphics.MeasureString(base.Text + required + "：", base.Font);
            System.Drawing.Size ps = GetPreferredSize(this.Size);
            this.Size = new System.Drawing.Size(Convert.ToInt32(size.Width - 8), ps.Height);
            base.OnPaint(e);
            size = e.Graphics.MeasureString(base.Text, base.Font);
            e.Graphics.DrawString(required, base.Font, Brushes.Red, new PointF(size.Width - 8, 0));
            size = e.Graphics.MeasureString(base.Text + required, base.Font);
            e.Graphics.DrawString("：", base.Font, Brushes.Black, new PointF(size.Width - 6, 0));
        }
    }
}
