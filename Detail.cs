using System;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class Detail : Form
    {
        public Detail()
        {
            InitializeComponent();
        }

        private void ComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ComboBoxType.Text == "TIW")
                LabelType.Text = "Tijdelijke Instalatie Wijzeging";
            if(ComboBoxType.Text == "MOC")
                LabelType.Text = "Managment Of Change";
            if (ComboBoxType.Text == "Overb")
                LabelType.Text = "Overbruging";
        }
    }
}
