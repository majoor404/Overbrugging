using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class Administratie : Form
    {
        private readonly MainForm _mainForm;
        private BindingList<Data> _DataRows;

        public Administratie(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm; // Store reference to MainForm
        }

        private void BTClose_Click(object sender, EventArgs e)
        {
            if(LBChange.Visible)
            {
                DialogResult result = MessageBox.Show("Stop zonder dat de wijzigingen zijn opgeslagen?", "Waarschuwing", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            Close();
        }

        private void BTToXML_Click(object sender, EventArgs e)
        {
            if(LBChange.Visible)
            {
                MessageBox.Show("Sla eerst de wijzigingen op!");
                return;
            }
            _mainForm.Export(); // Call Export method from MainForm
        }

        private void Administratie_Shown(object sender, EventArgs e)
        {
            // laad
            _mainForm.LaadData_lijst();

            _DataRows = new BindingList<Data>();

            foreach (Data data in _mainForm.LijstData)
            {
                _DataRows.Add(data);
            }

            DGAdmin.Columns.Clear();

            DGAdmin.DataSource = _DataRows;
        }

        private void BTAovToPG_Click(object sender, EventArgs e)
        {
            foreach (Data data in _mainForm.LijstData)
            {
                if (data.Sectie == "AOV")
                {
                    data.Sectie = "PG&A - PA";
                }
            }
            LBChange.Visible = true;
        }

        private void BTSave_Click(object sender, EventArgs e)
        {
            _mainForm.SaveData_lijst();
            LBChange.Visible = false;
        }
    }
}
