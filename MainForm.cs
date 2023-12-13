using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class MainForm : Form
    {
        public List<OudeOverbrugging> OudeLijst = new List<OudeOverbrugging>();
        public List<NamenFunties> NamenLijst { get; set; } = new List<NamenFunties>();
        public List<Secties> SectieLijst = new List<Secties>();
        public List<InstallatieOnderdeel> InstallatieLijst = new List<InstallatieOnderdeel>();
        public List<OverBrugRecord> LijstOverbrugingen = new List<OverBrugRecord> { };
        public List<Data> LijstData = new List<Data>();

        public static MainForm Main;

        public MainForm()
        {
            InitializeComponent();
            Main = this;
        }

        private void ButImport_Click(object sender, EventArgs e)
        {
            _ = MessageBox.Show("Inlezen Overb1.mdb.csv");
            try
            {
                string[] csvTekst = File.ReadAllLines($"Overb1.mdb.csv");
                string[] items;
                OudeLijst.Clear();


                for (int i = 0; csvTekst.Count() > i; i++)
                {
                    OudeOverbrugging ov = new OudeOverbrugging();
                    items = csvTekst[i].Split(';');
                    int count = 0;
                    ov.RegNr = items[count++];
                    ov.DatumInv = items[count++];
                    ov.Sectie = items[count++];
                    ov.Installatie = items[count++];
                    ov.InstallatieDeel = items[count++];
                    ov.NaamKKD1uit = items[count++];
                    ov.NaamKKD2uit = items[count++];
                    ov.Ploeg = items[count++];
                    ov.Reden = items[count++];
                    ov.Uitvoering = items[count++];
                    ov.EnigeOverb = items[count++];
                    ov.WerkVerg = items[count++];
                    ov.WerkVergNr = items[count++];
                    ov.WerkVergOk = items[count++];
                    ov.SrsNr = items[count++];
                    ov.DatumWv = items[count++];
                    ov.NaamWV = items[count++];
                    ov.DatumVerw = items[count++];
                    ov.NaamKKDverw = items[count++];
                    ov.UitersteDatum = items[count++];
                    ov.Veld1 = items[count++];
                    ov.PrintDatum = items[count++];
                    ov.BijzonderhedenWV = items[count++];
                    ov.BijzonderhedenVerw = items[count++];
                    ov.OvernemenIWV = items[count++];
                    ov.DatumVerloopWV = items[count++];
                    ov.TIW = items[count++];
                    ov.TIWOB = items[count++];
                    ov.InGebruik = items[count++];
                    ov.PersWijzig = items[count++];
                    ov.BewerkTijd = items[count++];
                    ov.Soort = items[count++];
                    ov.MocRsNr = items[count++];
                    ov.Reserve = items[count++];

                    OudeLijst.Add(ov);
                }

                _ = MessageBox.Show("Inlezen Namen.mdb.csv");

                string[] namenTekst = File.ReadAllLines($"Namen.mdb.csv");
                NamenLijst.Clear();


                for (int i = 0; namenTekst.Count() > i; i++)
                {
                    NamenFunties nf = new NamenFunties();
                    items = namenTekst[i].Split(';');
                    int count = 0;
                    nf.Index = items[count++];
                    nf.PersoneelNummer = items[count++];
                    nf.Naam = items[count++];
                    nf.Team = items[count++];
                    count++;    // funtie niet meer gebruiken
                    nf.IVWV = items[count++] == "True";

                    NamenLijst.Add(nf);
                }

                _ = MessageBox.Show("Save namen lijst");
                SaveDataNamen_lijst();

                _ = MessageBox.Show("Inlezen SectieTabel.mdb.csv");

                string[] SectieTekst = File.ReadAllLines($"SectieTabel.mdb.csv");
                SectieLijst.Clear();

                for (int i = 0; SectieTekst.Count() > i; i++)
                {
                    Secties sc = new Secties();
                    items = SectieTekst[i].Split(';');
                    int count = 0;
                    sc.Index = items[count++];
                    sc.Naam = items[count++];
                    SectieLijst.Add(sc);
                }

                _ = MessageBox.Show("Save secties lijst");
                SaveDataSecties_lijst();

                _ = MessageBox.Show("Inlezen InstallatieTabel.mdb.csv");

                string[] InstallatieTekst = File.ReadAllLines($"InstallatieTabel.mdb.csv");
                InstallatieLijst.Clear();

                for (int i = 0; InstallatieTekst.Count() > i; i++)
                {
                    InstallatieOnderdeel iso = new InstallatieOnderdeel();
                    items = InstallatieTekst[i].Split(';');
                    int count = 0;
                    iso.Index = items[count++];
                    iso.Instal = items[count++];
                    iso.Sectie = ZoekSectie(items[count++]);
                    InstallatieLijst.Add(iso);
                }

                _ = MessageBox.Show("Save Installatie lijst");
                SaveDataInstallaties_lijst();


                _ = MessageBox.Show("Dan nu samen voegen tot nieuwe opslag class.");

                LijstData.Clear();
                foreach (OudeOverbrugging o in OudeLijst)
                {
                    Data a = new Data
                    {
                        RegNr = int.Parse(o.RegNr),
                        DatumInv = VerwijderTijd(o.DatumInv),
                        SapNr = o.SrsNr,
                        MocNr = o.MocRsNr
                    };

                    labelAantal.Text = a.RegNr.ToString();
                    labelAantal.Refresh();

                    a.Sectie = ZoekSectie(o.Sectie);
                    a.Installatie = ZoekInstallatie(o.Installatie);
                    a.InstallatieDeel = o.InstallatieDeel;

                    a.Naam1 = ZoekNaam(o.NaamKKD1uit);
                    a.Naam2 = ZoekNaam(o.NaamKKD2uit);
                    a.Ploeg = o.Ploeg;

                    a.Reden = o.Reden;
                    a.Uitvoering = o.Uitvoering;

                    //// ivwv
                    a.WerkVerg = o.WerkVerg;
                    a.WerkVergNr = o.WerkVergNr;

                    a.DatumWv = VerwijderTijd(o.DatumWv);
                    a.NaamWV = ZoekNaam(o.NaamWV);

                    a.UitersteDatum = VerwijderTijd(o.UitersteDatum);
                    a.DatumVerloopWV = VerwijderTijd(o.DatumVerloopWV);

                    a.Soort = "OVERB"; // default
                    if (o.TIWOB == "Tijdelijke Installatie Wijziging")
                    {
                        a.Soort = "TIW";
                    }

                    if (o.TIWOB == "Management Of Change")
                    {
                        a.Soort = "MOC";
                    }

                    a.BijzonderhedenWV = o.BijzonderhedenWV;

                    ////verwijderen
                    a.Naamverw = ZoekNaam(o.NaamKKDverw);
                    a.DatumVerw = VerwijderTijd(o.DatumVerw);
                    a.BijzonderhedenVerw = o.BijzonderhedenVerw;

                    a.Reserve1 = "";
                    a.Reserve2 = "";
                    a.Reserve3 = "";
                    a.Reserve4 = "";
                    a.Reserve5 = "";
                    a.Reserve6 = "";
                    a.Reserve7 = "";
                    a.Reserve8 = "";

                    LijstData.Add(a);
                }
                _ = MessageBox.Show("Save overbrug lijst");
                SaveData_lijst();
            }

            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);

            }
        }

        private string VerwijderTijd(string Datum) // is van format "19-11-2023 00:00:00" of "9-4-2001 00:00:00"
        {
            if (string.IsNullOrEmpty(Datum))
            {
                return "";
            }
            // verwijder tijd
            int pos = Datum.IndexOf(" ");
            return Datum.Substring(0, pos);
        }

        //private DateTime NaarDateTime(string Datum) // is van format "19-11-2023 00:00:00" of "9-4-2001 00:00:00"
        //{
        //    if (string.IsNullOrEmpty(Datum))
        //    {
        //        return DateTime.Now;
        //    }
        //    // verwijder tijd
        //    int pos = Datum.IndexOf(" ");
        //    if (pos > -1)
        //    {
        //        Datum = Datum.Substring(0, pos);
        //    }

        //    string[] temp = Datum.Split('-');

        //    int Dag = int.Parse(temp[0]);
        //    int Maand = int.Parse(temp[1]);
        //    int Jaar = int.Parse(temp[2]);

        //    DateTime ret = new DateTime(Jaar, Maand, Dag);
        //    return ret;
        //}

        //private bool MaakBool(string vraag)
        //{
        //    if (string.IsNullOrEmpty(vraag))
        //        return false;
        //    if (vraag == "Ja")
        //        return true;
        //    return false;
        //}

        private string ZoekSectie(string zoek)
        {
            if (string.IsNullOrEmpty(zoek))
            {
                return "";
            }

            try
            {
                Secties Q = SectieLijst.First(a => a.Index == zoek);
                return Q.Naam;
            }
            catch
            {
                return "";
            }
        }

        private Data ZoekDataRecord(int nr)
        {
            Data Q = new Data();
            try
            {
                Q = LijstData.First(a => a.RegNr == nr);
                return Q;
            }
            catch
            {
                return Q;
            }
        }

        private string ZoekNaam(string zoek)
        {
            if (string.IsNullOrEmpty(zoek))
            {
                return "";
            }

            try
            {
                NamenFunties Q = NamenLijst.First(a => a.Index == zoek);
                return Q.Naam;
            }
            catch
            {
                return "";
            }
        }

        private string ZoekPersnr(string zoek)
        {
            if (string.IsNullOrEmpty(zoek))
            {
                return "";
            }

            try
            {
                NamenFunties Q = NamenLijst.First(a => a.Naam == zoek);
                return Q.PersoneelNummer;
            }
            catch
            {
                return "";
            }
        }

        private string ZoekInstallatie(string zoek)
        {
            if (string.IsNullOrEmpty(zoek))
            {
                return "";
            }

            try
            {
                InstallatieOnderdeel Q = InstallatieLijst.First(a => a.Index == zoek);
                return Q.Instal;
            }
            catch
            {
                return "";
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            // laad secties
            LaadSecties_lijst();
            // laad installaties
            LaadInstallaties_lijst();
            // zet filter dropdown
            if (comboBoxSectie.Items.Count == 0)
            {
                comboBoxSectie.Items.Clear();
                _ = comboBoxSectie.Items.Add("ALLE");
                foreach (Secties item in SectieLijst)
                {
                    _ = comboBoxSectie.Items.Add(item.Naam);
                }
                comboBoxSectie.SelectedIndex = 0;
            }

            comboBoxSoortFilter.SelectedIndex = 0;
            comboBoxStatus.SelectedIndex = 0;

            dataGridView1.Columns.Clear();

            dataGridView1.AutoGenerateColumns = false;

            _ = dataGridView1.Columns.Add("Nr", "Nr");
            dataGridView1.Columns[0].Width = 50;
            _ = dataGridView1.Columns.Add("Datum", "Datum inv.");
            dataGridView1.Columns[1].Width = 90;
            _ = dataGridView1.Columns.Add("Soort", "Soort");
            dataGridView1.Columns[2].Width = 60;
            _ = dataGridView1.Columns.Add("Sectie", "Sectie");
            dataGridView1.Columns[3].Width = 60;

            _ = dataGridView1.Columns.Add("Installatie", "Installatie");
            dataGridView1.Columns[4].Width = 110;
            _ = dataGridView1.Columns.Add("InstalatieDeel", "Instalatie Deel");
            dataGridView1.Columns[5].Width = 180;
            _ = dataGridView1.Columns.Add("Rede", "Rede");
            dataGridView1.Columns[6].Width = 340;

            _ = dataGridView1.Columns.Add("DatumVerl", "Verloopt");
            dataGridView1.Columns[7].Width = 90;

            ButRefresh_Click(this, null);

            // zet update roetine op filters actief
            comboBoxSectie.SelectedIndexChanged += ButRefresh_Click;
            comboBoxSoortFilter.SelectedIndexChanged += ButRefresh_Click;
            comboBoxStatus.SelectedIndexChanged += ButRefresh_Click;
        }

        private void VulGrid()
        {
            StopRedraw.SuspendDrawing(panelMain);
            //dataGridView1.SuspendLayout();

            dataGridView1.DataSource = LijstData;

            dataGridView1.Columns["Nr"].DataPropertyName = "Regnr";
            dataGridView1.Columns["Datum"].DataPropertyName = "DatumInv";
            dataGridView1.Columns["Soort"].DataPropertyName = "Soort";
            dataGridView1.Columns["Sectie"].DataPropertyName = "Sectie";
            dataGridView1.Columns["Installatie"].DataPropertyName = "Installatie";
            dataGridView1.Columns["InstalatieDeel"].DataPropertyName = "InstallatieDeel";
            dataGridView1.Columns["Rede"].DataPropertyName = "Reden";
            dataGridView1.Columns["DatumVerl"].DataPropertyName = "UitersteDatum";


            //dataGridView1.ResumeLayout();
            StopRedraw.ResumeDrawing(panelMain);

            labelAantal.Text = LijstData.Count().ToString();
        }

        private void ButRefresh_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            // laad alle overbrugingen
            LaadData_lijst();

            // filter op status
            LijstData = comboBoxStatus.Text == "Niet Verwijderd"
                ? LijstData.Where(x => x.DatumVerw == string.Empty).ToList()
                : LijstData.Where(x => x.DatumVerw != string.Empty).ToList();

            // filter op sectie
            if (comboBoxSectie.Text != "" && comboBoxSectie.Text != "ALLE")
            {
                LijstData = LijstData.Where(x => x.Sectie == comboBoxSectie.Text).ToList();
            }

            // Filter op Soort
            if (comboBoxSoortFilter.Text != "ALLE")
            {
                LijstData = LijstData.Where(x => x.Soort == comboBoxSoortFilter.Text).ToList();
            }

            // sorteer
            LijstData = LijstData.OrderByDescending(o => o.RegNr).ToList();

            VulGrid();
        }

        public void SaveData_lijst()
        {
            try
            {
                using (Stream stream = File.Open("overbrug.bin", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, LijstData);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();
                }
            }
            catch
            {
                GC.Collect();
            }
        }

        public void SaveDataNamen_lijst()
        {
            try
            {
                using (Stream stream = File.Open("namen.bin", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, NamenLijst);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();
                }
            }
            catch
            {
                GC.Collect();
            }
        }

        public void SaveDataSecties_lijst()
        {
            try
            {
                using (Stream stream = File.Open("sectie.bin", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, SectieLijst);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();
                }
            }
            catch
            {
                GC.Collect();
            }
        }

        public void SaveDataInstallaties_lijst()
        {
            try
            {
                using (Stream stream = File.Open("install.bin", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, InstallatieLijst);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();
                }
            }
            catch
            {
                GC.Collect();
            }
        }

        public void LaadData_lijst()
        {
            try
            {
                using (Stream stream = File.Open("overbrug.bin", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    LijstData.Clear();
                    BinaryFormatter bin = new BinaryFormatter();
                    LijstData = (List<Data>)bin.Deserialize(stream);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();
                }
            }
            catch
            {
                GC.Collect();
            }
        }

        public void LaadSecties_lijst()
        {
            try
            {
                using (Stream stream = File.Open("sectie.bin", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    LijstData.Clear();
                    BinaryFormatter bin = new BinaryFormatter();
                    SectieLijst = (List<Secties>)bin.Deserialize(stream);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();
                }
            }
            catch
            {
                GC.Collect();
            }
        }

        public void LaadNamen_lijst()
        {
            try
            {
                using (Stream stream = File.Open("namen.bin", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    NamenLijst.Clear();
                    BinaryFormatter bin = new BinaryFormatter();
                    NamenLijst = (List<NamenFunties>)bin.Deserialize(stream);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();
                }
            }
            catch
            {
                GC.Collect();
            }
        }

        public void LaadInstallaties_lijst()
        {
            try
            {
                using (Stream stream = File.Open("install.bin", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    InstallatieLijst.Clear();
                    BinaryFormatter bin = new BinaryFormatter();
                    InstallatieLijst = (List<InstallatieOnderdeel>)bin.Deserialize(stream);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();
                }
            }
            catch
            {
                GC.Collect();
            }
        }

        private void ButSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            DialogResult ret = settings.ShowDialog();
            if (ret == DialogResult.OK)
            {
                EditNamen ed = new EditNamen();
                _ = ed.ShowDialog();
            }
            if (ret == DialogResult.Cancel)
            {

            }
            if (ret == DialogResult.Abort)
            {
                ButImport_Click(this, null);
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = System.Diagnostics.Process.Start("https://github.com/majoor404/Overbrugging");
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // zoek record
                int regNr = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                Data Q = ZoekDataRecord(regNr);
                // maak formulier
                Detail dt = new Detail();
                dt.TextBoxRegNr.Text = Q.RegNr.ToString();
                dt.TextBoxRegNr.Enabled = false;

                // vullen dropdown items
                LaadNamen_lijst();
                dt.ComboBoxSectie.Items.Clear();
                for (int i = 0; i < MainForm.Main.SectieLijst.Count; i++)
                {
                    _ = dt.ComboBoxSectie.Items.Add(MainForm.Main.SectieLijst[i].Naam);
                }
                List<InstallatieOnderdeel> InstallatieLijstFilter = new List<InstallatieOnderdeel>();
                InstallatieLijstFilter = InstallatieLijst.Where(x => x.Sectie == dt.ComboBoxSectie.Text).ToList();
                dt.ComboSectieDeel.Items.Clear();
                for (int i = 0; i < InstallatieLijstFilter.Count; i++)
                {
                    _ = dt.ComboSectieDeel.Items.Add(InstallatieLijstFilter[i].Instal);
                }
                dt.ComboBoxNaam1.Items.Clear();
                dt.ComboBoxNaam2.Items.Clear();
                dt.ComboBoxNaamVerw.Items.Clear();
                for (int i = 0; i < NamenLijst.Count; i++)
                {
                    _ = dt.ComboBoxNaam1.Items.Add(NamenLijst[i].Naam);
                    _ = dt.ComboBoxNaam2.Items.Add(NamenLijst[i].Naam);
                    _ = dt.ComboBoxNaamVerw.Items.Add(NamenLijst[i].Naam);
                }
                List<NamenFunties> IVWVFilter = new List<NamenFunties>();
                IVWVFilter = NamenLijst.Where(x => x.IVWV == true).ToList();
                for (int i = 0; i < IVWVFilter.Count; i++)
                {
                    dt.ComboBoxIVWV.Items.Add(IVWVFilter[i].Naam);
                }

                // bovenste panel vullen met data
                dt.DatumInv.Datum = Q.DatumInv;
                dt.TextBoxSapNr.Text = Q.SapNr;
                dt.TextBoxMocNr.Text = Q.MocNr;
                dt.ComboBoxSectie.Text = Q.Sectie;
                dt.ComboSectieDeel.Text = Q.Installatie;
                dt.TextBoxInstDeel.Text = Q.InstallatieDeel;
                dt.ComboBoxNaam1.Text = Q.Naam1;
                dt.ComboBoxNaam2.Text = Q.Naam2;
                dt.TextBoxPersnr1.Text = ZoekPersnr(Q.Naam1);
                dt.TextBoxPersnr2.Text = ZoekPersnr(Q.Naam2);
                dt.TextBoxRede.Text = Q.Reden;
                dt.TextBoxOplossing.Text = Q.Uitvoering;
                // middelste panel
                dt.DatumWv.Datum = Q.DatumWv;
                dt.ComboBoxIVWV.Text = Q.NaamWV;
                dt.DatumVerloopTIW.Datum = Q.UitersteDatum;
                dt.TextBoxPersNrIVWV.Text = ZoekPersnr(Q.NaamWV);
                dt.TextBoxBijzIVWV.Text = Q.BijzonderhedenWV;
                dt.ComboBoxType.Text = Q.Soort;
                // onderste panel
                dt.DatumVerw.Datum = Q.DatumVerw;
                dt.ComboBoxNaamVerw.Text = Q.Naamverw;
                dt.TextBoxPersNrVerw.Text = ZoekPersnr(Q.Naamverw);
                dt.TextBoxBijzVerw.Text = Q.BijzonderhedenVerw;
                // open dialog
                _ = dt.ShowDialog();
            }
            catch
            {
                _ = MessageBox.Show($"e = {e.RowIndex}");
            }
        }

        public void SorteerNaamOpPersoneelNummer()
        {
            NamenLijst.Sort(Sorteer_Namen);
        }

        public int Sorteer_Namen(NamenFunties a, NamenFunties b)
        {
            return a.PersoneelNummer.CompareTo(b.PersoneelNummer);
        }
        
    }
}