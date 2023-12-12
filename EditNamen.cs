using System;
using System.Data;
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
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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
            }catch { }
            // toevoegen
            NamenFunties item = new NamenFunties();
            item.Naam = TextBoxNaam.Text;
            item.PersoneelNummer = TextBoxPersNr.Text;
            item.Team = TextBoxTeam.Text;
            item.IVWV = CheckBoxIvWv.Checked == true ? true : false;
            MainForm.Main.NamenLijst.Add(item);
            // sorteer
            MainForm.Main.SorteerNaamOpPersoneelNummer();
            // save
            MainForm.Main.SaveDataNamen_lijst();
            // refresh
            dataGridView1.Refresh();
        }
    }
}
