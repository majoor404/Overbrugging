using System;
using System.Collections.Generic;
using System.Linq;
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
            dataGridViewSecties.Columns[0].Visible = false;   // index
            dataGridViewSecties.RowHeadersVisible = false;

            MainForm.Main.LaadInstallaties_lijst();
            dataGridViewInstal.DataSource = MainForm.Main.InstallatieLijst;
            dataGridViewInstal.Columns[0].Visible = false;   // index
            dataGridViewInstal.RowHeadersVisible = false;

            string sectie = dataGridViewSecties.SelectedRows[0].Cells[1].Value.ToString();
            FiterInstalOpSectie(sectie);
        }

        private void dataGridViewSecties_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string sectie = dataGridViewSecties.Rows[e.RowIndex].Cells[1].Value.ToString();
            FiterInstalOpSectie(sectie);
        }

        private void FiterInstalOpSectie(string sectie)
        {
            // filter op sectie
            List<InstallatieOnderdeel> InstallatieLijstGefiteerd = new List<InstallatieOnderdeel>();

            InstallatieLijstGefiteerd = MainForm.Main.InstallatieLijst.Where(x => x.Sectie == sectie).ToList();
            dataGridViewInstal.DataSource = InstallatieLijstGefiteerd;
        }

        private void dataGridViewInstal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBoxSectie.Text = dataGridViewInstal.Rows[e.RowIndex].Cells[2].Value.ToString();
            TextBoxInstall.Text = dataGridViewInstal.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
