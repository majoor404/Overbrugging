using System;
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

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            
            _ = dataGridView1.Columns.Add("Naam", "Naam");
            dataGridView1.Columns[0].Width = 200;
            _ = dataGridView1.Columns.Add("PersoneelNummer", "PersoneelNummer");
            dataGridView1.Columns[1].Width = 200;
            _ = dataGridView1.Columns.Add("Team", "Team");
            dataGridView1.Columns[2].Width = 200;
            _ = dataGridView1.Columns.Add("Funtie", "Funtie");
            dataGridView1.Columns[2].ValueType = typeof(bool);
            dataGridView1.Columns[3].Width = 70;
            _ = dataGridView1.Columns.Add("IVWV", "IVWV");
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[3].ValueType = typeof(bool);

            //_ = dataGridView1.Columns.Add("Naam", "Naam");
            //dataGridView1.Columns[0].Width = 200;
            //_ = dataGridView1.Columns.Add("PersoneelNummer", "PersoneelNummer");
            //dataGridView1.Columns[1].Width = 200;
            //_ = dataGridView1.Columns.Add("Team", "Team");
            //dataGridView1.Columns[2].Width = 200;
            //_ = dataGridView1.Columns.Add("Funtie", "Funtie");
            ////dataGridView1.Columns[2].ValueType = typeof(bool);
            //dataGridView1.Columns[3].Width = 70;
            //_ = dataGridView1.Columns.Add("IVWV", "IVWV");
            //dataGridView1.Columns[3].Width = 70;
            ////dataGridView1.Columns[3].ValueType = typeof(bool);

            dataGridView1.Columns["Naam"].DataPropertyName = "Naam";
            dataGridView1.Columns["PersoneelNummer"].DataPropertyName = "PersoneelNummer";
            dataGridView1.Columns["Team"].DataPropertyName = "Team";
            dataGridView1.Columns["Funtie"].DataPropertyName = "Funtie";
            dataGridView1.Columns["IVWV"].DataPropertyName = "IVW";
        }
    }
}
