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
    public partial class EditNamen : Form
    {
        public EditNamen()
        {
            InitializeComponent();
        }

        private void EditNamen_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            _ = dataGridView1.Columns.Add("Naam", "Naam");
            dataGridView1.Columns[0].Width = 200;
            _ = dataGridView1.Columns.Add("PersoneelNummer", "PersoneelNummer");
            dataGridView1.Columns[1].Width = 200;
            _ = dataGridView1.Columns.Add("Team", "Team");
            dataGridView1.Columns[2].Width = 200;
            _ = dataGridView1.Columns.Add("Funtie", "Funtie");
            dataGridView1.Columns[3].Width = 70;
            _ = dataGridView1.Columns.Add("IVWV", "IVWV");
            dataGridView1.Columns[3].Width = 70;

            //dataGridView1.DataSource = MainForm.NamenLijst;

            dataGridView1.Columns["Naam"].DataPropertyName = "Naam";
            dataGridView1.Columns["Naam"].ValueType = typeof(string);
            dataGridView1.Columns["PersoneelNummer"].DataPropertyName = "PersoneelNummer";
            dataGridView1.Columns["Naam"].ValueType = typeof(string);
            dataGridView1.Columns["Team"].DataPropertyName = "Team";
            dataGridView1.Columns["Naam"].ValueType = typeof(string);
            dataGridView1.Columns["Funtie"].DataPropertyName = "Funtie";
            dataGridView1.Columns["Naam"].ValueType = typeof(bool);
            dataGridView1.Columns["IVWV"].DataPropertyName = "IVW";
            dataGridView1.Columns["Naam"].ValueType = typeof(bool);



        }
    }
}
