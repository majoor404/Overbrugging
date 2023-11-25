using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Overbrugging
{
    public class GradientPanel : Panel
    {
        public Color colorTop {  get; set; }
        public Color colorBottom { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush liniear = new LinearGradientBrush(this.ClientRectangle, this.colorTop, this.colorBottom, 90F);
            Graphics g = e.Graphics;
            g.FillRectangle(liniear, this.ClientRectangle);
            base.OnPaint(e);
        }
    }
}
