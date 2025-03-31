using System;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class DatumPicker : UserControl
    {
        public string Datum
        {
            get
            {
                if (TB.Text == " --/--/----")
                    return "";
                return TB.Text;
            }

            set
            {
                if (value == "")
                {
                    TB.Text = " --/--/----";
                }
                else
                {
                    TB.Text = value;
                }
            }
        }

        public DatumPicker()
        {
            InitializeComponent();
        }

        private void DT_ValueChanged(object sender, EventArgs e)
        {
            TB.Text = DT.Value.ToString("dd-MM-yyyy");
        }

        private void TB_Leave(object sender, EventArgs e)
        {
            if (TB.Text != " --/--/----")
            {
                try
                {
                    DT.Value = DateTime.Parse(TB.Text);
                }
                catch
                {
                    MessageBox.Show("Ongeldige datum");
                    TB.Text = " --/--/----";
                }
            }
        }

        private void TB_MouseLeave(object sender, EventArgs e)
        {
            TB_Leave(this, null);
        }
    }
}
