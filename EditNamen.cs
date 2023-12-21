using System;
using System.Linq;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class EditNamen : Form
    {
        public EditNamen()
        {
            InitializeComponent();
        }

        private void EditNamen_Shown(object sender, EventArgs e)
        {
            MainForm.Main.LaadNamen_lijst();
            dataGridView1.DataSource = MainForm.Main.NamenLijst;

            dataGridView1.Columns[0].Visible = false;   // index

            dataGridView1.RowHeadersVisible = false;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBoxNaam.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TextBoxPersNr.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TextBoxTeam.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            CheckBoxIvWv.Checked = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() == "True";
        }

        private void ButSave_Click(object sender, EventArgs e)
        {
            // check of personeel nummer ingevuld is
            if (TextBoxPersNr.Text.Length != 6)
            {
                MessageBox.Show("Personeel nummer geen lengte 6");
                return;
            }
            // zoek personeel nummer record
            // en delete deze
            try
            {
                NamenFunties Record = MainForm.Main.NamenLijst.First(a => a.PersoneelNummer == TextBoxPersNr.Text);
                MainForm.Main.NamenLijst.Remove(Record);
            }
            catch { }
            // toevoegen
            NamenFunties item = new NamenFunties
            {
                Naam = TextBoxNaam.Text,
                PersoneelNummer = TextBoxPersNr.Text,
                Team = TextBoxTeam.Text,
                IVWV = CheckBoxIvWv.Checked == true
            };
            MainForm.Main.NamenLijst.Add(item);
            // sorteer
            MainForm.Main.SorteerNaamOpNaam();
            // save
            MainForm.Main.SaveDataNamen_lijst();
            // refresh
            dataGridView1.Refresh();
        }

        private void ButNew_Click(object sender, EventArgs e)
        {
            // veeg alle namen leeg
            TextBoxNaam.Text = TextBoxPersNr.Text = TextBoxTeam.Text = "";
            CheckBoxIvWv.Checked = false;
            // melding
            MessageBox.Show("Voeg gegevens toe en druk op save.");
        }

        private void ButDel_Click(object sender, EventArgs e)
        {
            // zoek personeel nummer record
            // en delete deze
            try
            {
                NamenFunties Record = MainForm.Main.NamenLijst.First(a => a.PersoneelNummer == TextBoxPersNr.Text);
                // vraag
                DialogResult dialogResult = MessageBox.Show($"Verwijder {Record.Naam}", "Vraagje", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    MainForm.Main.NamenLijst.Remove(Record);
                    // sorteer
                    MainForm.Main.SorteerNaamOpNaam();
                    // save
                    MainForm.Main.SaveDataNamen_lijst();
                    //
                    MainForm.Main.wait(500);
                    // refresh
                    dataGridView1.Refresh();
                }
            }
            catch
            {
                MessageBox.Show($"Personeel nr {TextBoxPersNr.Text} niet gevonden in lijst!");
            }
        }
    }
}
