using System;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class EditSecties : Form
    {
        public EditSecties()
        {
            InitializeComponent();
        }

        private void EditSecties_Shown(object sender, EventArgs e)
        {
            MainForm.Main.LaadSecties_lijst();
            dataGridViewSecties.DataSource = MainForm.Main.SectieLijst;

            MainForm.Main.LaadInstallaties_lijst();
            dataGridViewInstallaties.DataSource = MainForm.Main.InstallatieLijst;
        }
    }
}
