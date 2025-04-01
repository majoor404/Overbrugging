using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class Actuele : Form
    {
        public string Sectie { get; set; }
        public string Installatie { get; set; }

        public List<Data> LijstOverbrugingenGefilterd = new List<Data>();
        public Actuele()
        {
            InitializeComponent();
        }

        private void Actuele_Shown(object sender, EventArgs e)
        {
            LBSectie.Text = Sectie;
            LBInstallatie.Text = Installatie;

            // laad alle overbrugingen
            LaadDataGefilterd_lijst();
            // die nog niet afgesloten zijn
            LijstOverbrugingenGefilterd = LijstOverbrugingenGefilterd.Where(x => x.DatumVerw == string.Empty).ToList();
            // filter op sectie
            LijstOverbrugingenGefilterd = LijstOverbrugingenGefilterd.Where(x => x.Sectie == Sectie).ToList();
            // filter op installatie
            LijstOverbrugingenGefilterd = LijstOverbrugingenGefilterd.Where(x => x.Installatie == Installatie).ToList();

            MaakGrid();
            VulGrid();
        }

        private void MaakGrid()
        {
            dataGridView.Columns.Clear();

            dataGridView.AutoGenerateColumns = false;

            _ = dataGridView.Columns.Add("Nr", "Nr");
            dataGridView.Columns[0].Width = 60;
            _ = dataGridView.Columns.Add("Datum", "Datum inv.");
            dataGridView.Columns[1].Width = 100;
            _ = dataGridView.Columns.Add("Soort", "Soort");
            dataGridView.Columns[2].Width = 70;
            _ = dataGridView.Columns.Add("Sectie", "Sectie");
            dataGridView.Columns[3].Width = 70;

            _ = dataGridView.Columns.Add("Installatie", "Installatie");
            dataGridView.Columns[4].Width = 110;
            _ = dataGridView.Columns.Add("InstalatieDeel", "Instalatie Deel");
            dataGridView.Columns[5].Width = 175;
            _ = dataGridView.Columns.Add("Rede", "Rede");
            dataGridView.Columns[6].Width = 380;

            _ = dataGridView.Columns.Add("DatumVerl", "Verloopt");
            dataGridView.Columns[7].Width = 100;

            dataGridView.RowHeadersVisible = false;
        }

        private void VulGrid()
        {
            if(LijstOverbrugingenGefilterd.Count == 0)
            {
                MessageBox.Show("Geen overbruggingen gevonden in deze installatie", "Geen data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                return;
            }
            
            dataGridView.DataSource = LijstOverbrugingenGefilterd;

            if (dataGridView.Columns["Nr"].DataPropertyName == "")
            {
                dataGridView.Columns["Nr"].DataPropertyName = "Regnr";
                dataGridView.Columns["Datum"].DataPropertyName = "DatumInv";
                dataGridView.Columns["Soort"].DataPropertyName = "Soort";
                dataGridView.Columns["Sectie"].DataPropertyName = "Sectie";
                dataGridView.Columns["Installatie"].DataPropertyName = "Installatie";
                dataGridView.Columns["InstalatieDeel"].DataPropertyName = "InstallatieDeel";
                dataGridView.Columns["Rede"].DataPropertyName = "Reden";
                dataGridView.Columns["DatumVerl"].DataPropertyName = "UitersteDatum";
            }

            int index = int.Parse(dataGridView.SelectedCells[0].Value.ToString());
            VulPreview(index);
        }

        private void LaadDataGefilterd_lijst()
        {
            try
            {
                using (Stream stream = File.Open($"{AppDomain.CurrentDomain.BaseDirectory}Data\\overbrug.bin", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    LijstOverbrugingenGefilterd.Clear();
                    BinaryFormatter bin = new BinaryFormatter();
                    LijstOverbrugingenGefilterd = (List<Data>)bin.Deserialize(stream);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();
                }
            }
            catch
            {
                GC.Collect();
            }
        }

        private void dataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // zoek record
                int regNr = int.Parse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                VulPreview(regNr);
            }
            catch { }
        }

        private void VulPreview(int regNr)
        {
            Data data = LijstOverbrugingenGefilterd.FirstOrDefault(x => x.RegNr == regNr);
            if (data != null)
            {
                TBHoe.Text = data.Uitvoering;
                TBRede.Text = data.Reden;
            }
        }

        //public DataTable ConvertListToDataTable<T>(List<T> list)
        //{
        //    DataTable table = new DataTable(typeof(T).Name);
        //    PropertyInfo[] properties = typeof(T).GetProperties();

        //    foreach (PropertyInfo prop in properties)
        //    {
        //        _ = table.Columns.Add(prop.Name, prop.PropertyType);
        //    }

        //    foreach (T item in list)
        //    {
        //        DataRow row = table.NewRow();
        //        foreach (PropertyInfo prop in properties)
        //        {
        //            row[prop.Name] = prop.GetValue(item);
        //        }
        //        table.Rows.Add(row);
        //    }
        //    return table;
        //}
    }
}
