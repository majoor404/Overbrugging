using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class ucMenu : UserControl
    {
        string menuTitel;
        Image icon;
        //Color _BorderColor = Color.Transparent;
        public event EventHandler<EventArgs> OnMenuClick;
        public ucMenu()
        {
            InitializeComponent();
        }

        public string Titel
        {
            get
            {
                return menuTitel;
            }
            set
            {
                menuTitel = value;
                this.Invalidate();
            }
        }

        public Image Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
                this.Invalidate();
            }
        }

        //public Color BorderColor
        //{
        //    get
        //    {
        //        return _BorderColor;
        //    }
        //    set
        //    {
        //        _BorderColor = value;
        //        this.Invalidate();
        //    }
        //}

        private void ucMenu_Paint(object sender, PaintEventArgs e)
        {
            label.Text = menuTitel;
            pictureBox.Image = icon;
        }

        
    }
}
