using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Overbrugging
{
    public partial class EditSecties : Form
    {
        List<InstallatieOnderdeel> InstallatieLijstGefiteerd = new List<InstallatieOnderdeel>();
        public EditSecties()
        {
            InitializeComponent();
        }

        private void EditSecties_Shown(object sender, EventArgs e)
        {
            MainForm.Main.LaadSecties_lijst();
            if(MainForm.Main.SectieLijst.Count > 0)
                dataGridViewSecties.DataSource = MainForm.Main.SectieLijst;
            dataGridViewSecties.Columns[0].Visible = false;   // index
            dataGridViewSecties.RowHeadersVisible = false;

            MainForm.Main.LaadInstallaties_lijst();
            if(MainForm.Main.InstallatieLijst.Count > 0)
                dataGridViewInstal.DataSource = MainForm.Main.InstallatieLijst;
            dataGridViewInstal.Columns[0].Visible = false;   // index
            dataGridViewInstal.RowHeadersVisible = false;

            string sectie = dataGridViewSecties.SelectedRows[0].Cells[1].Value.ToString();
            TextBoxSectie.Text = sectie;
            FiterInstalOpSectie(sectie);
        }

        private void DataGridViewSecties_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string sectie = dataGridViewSecties.Rows[e.RowIndex].Cells[1].Value.ToString();
            TextBoxSectie.Text = sectie;
            FiterInstalOpSectie(sectie);
        }

        private void FiterInstalOpSectie(string sectie)
        {
            // filter op sectie
            //InstallatieLijstGefiteerd = new List<InstallatieOnderdeel>();

            InstallatieLijstGefiteerd = MainForm.Main.InstallatieLijst.Where(x => x.Sectie == sectie).ToList();
            dataGridViewInstal.DataSource = InstallatieLijstGefiteerd;

            if (dataGridViewInstal.SelectedRows.Count > 0)
            {
                TextBoxInstall.Text = dataGridViewInstal.SelectedRows[0].Cells[1].Value.ToString();
            }
            else
            {
                TextBoxInstall.Text = "";
            }
        }

        private void DataGridViewInstal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBoxSectie.Text = dataGridViewInstal.Rows[e.RowIndex].Cells[2].Value.ToString();
            TextBoxInstall.Text = dataGridViewInstal.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void ButNewSec_Click(object sender, EventArgs e)
        {
            TextBoxSectie.Text = "";
            FiterInstalOpSectie("");
            MessageBox.Show("Druk op save na invullen nieuwe sectie");
        }

        private void ButSaveSec_Click(object sender, EventArgs e)
        {
            Secties N = new Secties
            {
                Index = "@@@@",
                Naam = TextBoxSectie.Text
            };

            MainForm.Main.SectieLijst.Add(N);
            MainForm.Main.SaveDataSecties_lijst();

            //MainForm.Main.Wait(500);
            EditSecties_Shown(this, null);
        }

        private void ButDeleteSec_Click(object sender, EventArgs e)
        {
            try
            {
                Secties Record = MainForm.Main.SectieLijst.First(a => a.Naam == TextBoxSectie.Text);
                // onderdelen leeg ?
                if(InstallatieLijstGefiteerd.Count > 0)
                {
                    MessageBox.Show("Verwijder eerst de installaties van deze sectie");
                    return;
                }
                
                // vraag
                DialogResult dialogResult = MessageBox.Show($"Verwijder {Record.Naam}", "Vraagje", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    MainForm.Main.SectieLijst.Remove(Record);
                    // save
                    MainForm.Main.SaveDataSecties_lijst();
                    //MainForm.Main.Wait(500);
                    // refresh
                    EditSecties_Shown(this, null);
                }
            }
            catch
            {
                MessageBox.Show($"Naam {TextBoxSectie.Text} niet gevonden in lijst!");
            }
        }

        private void ButNewInstall_Click(object sender, EventArgs e)
        {
            TextBoxInstall.Text = "";
            MessageBox.Show("Druk op save na invullen nieuwe Instalatie");
        }

        private void ButSaveInstall_Click(object sender, EventArgs e)
        {
            InstallatieOnderdeel N = new InstallatieOnderdeel
            {
                Index = "@@@@",
                Instal = TextBoxInstall.Text,
                Sectie = TextBoxSectie.Text
            };

            MainForm.Main.InstallatieLijst.Add(N);
            MainForm.Main.SaveDataInstallaties_lijst();
            //MainForm.Main.Wait(500);

            EditSecties_Shown(this, null);
        }

        private void ButDelInstall_Click(object sender, EventArgs e)
        {
            try
            {
                InstallatieOnderdeel Record = MainForm.Main.InstallatieLijst.First(a => a.Instal == TextBoxInstall.Text);

                // vraag
                DialogResult dialogResult = MessageBox.Show($"Verwijder {Record.Instal}", "Vraagje", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    MainForm.Main.InstallatieLijst.Remove(Record);
                    // save
                    MainForm.Main.SaveDataInstallaties_lijst();
                    //MainForm.Main.Wait(500);
                    // refresh
                    EditSecties_Shown(this, null);
                }
            }
            catch
            {
                MessageBox.Show($"Naam {TextBoxInstall.Text} niet gevonden in lijst!");
            }
        }
    }
}
