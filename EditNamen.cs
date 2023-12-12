using System;
using System.Data;
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

            //dataGridView1.AutoGenerateColumns = false;
            //dataGridView1.Columns.Clear();


            //_ = dataGridView1.Columns.Add("Naam", "Naam");
            //dataGridView1.Columns[0].Width = 200;
            //dataGridView1.Columns["Naam"].DataPropertyName = "Naam";

            //_ = dataGridView1.Columns.Add("PersoneelNummer", "PersoneelNummer");
            //dataGridView1.Columns[1].Width = 200;
            //dataGridView1.Columns["PersoneelNummer"].DataPropertyName = "PersoneelNummer";

            //_ = dataGridView1.Columns.Add("Team", "Team");
            //dataGridView1.Columns[2].Width = 200;
            //dataGridView1.Columns["Team"].DataPropertyName = "Team";

            //_ = dataGridView1.Columns.Add("Funtie", "Funtie");
            //dataGridView1.Columns[3].ValueType = typeof(bool);
            //dataGridView1.Columns[3].Width = 70;
            //dataGridView1.Columns["Funtie"].DataPropertyName = "Funtie";

            //_ = dataGridView1.Columns.Add("IVWV", "IVWV");
            //dataGridView1.Columns[4].Width = 70;
            //dataGridView1.Columns[4].ValueType = typeof(bool);
            //dataGridView1.Columns["IVWV"].DataPropertyName = "IVW";

            dataGridView1.Columns[0].Visible = false;



        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBoxNaam.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TextBoxPersNr.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TextBoxTeam.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
    }
}
