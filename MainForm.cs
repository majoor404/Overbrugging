using Melding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Xml.Serialization;
using Label = System.Windows.Forms.Label;

namespace Overbrugging
{
    public partial class MainForm : Form
    {
        public List<NamenFunties> NamenLijst { get; set; } = new List<NamenFunties>();
        public List<Secties> SectieLijst = new List<Secties>();
        public List<InstallatieOnderdeel> InstallatieLijst = new List<InstallatieOnderdeel>();
        public List<OverBrugRecord> LijstOverbrugingen = new List<OverBrugRecord> { };
        public List<Data> LijstData = new List<Data>();
        public Data TempData = new Data();
        public int LastIndex = 0;
        public static string datapath = AppDomain.CurrentDomain.BaseDirectory + "Data\\";
        public static string bijlagepath = AppDomain.CurrentDomain.BaseDirectory + "Bijlage\\";
        public static Bijlage bijlage = new Bijlage();
        public List<string> instellingen = new List<string>();
        public DateTime verloopdatum = DateTime.Now.AddDays(1);
        public AutoCompleteStringCollection mycolIVWV = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection mycol = new AutoCompleteStringCollection();
        public string inlognaam = "";
        public int rechten = 0;

        public Lock Geblokkerd = new Lock();

        public Logging Log = new Logging();

        public int NietAfgetekendWv = 0;
        public int VerlopenData = 0;
        private DataGridViewColumn oldColumn = null; // voor sorteer
        private SortOrder SortRichting = SortOrder.None;

        private readonly System.Drawing.Font SmallFont = new System.Drawing.Font("Microsoft Sans Serif", 8);
        public bool Scalling = false;  // als main scherm verkleind is.
        public float ScreenScalingFactor = 1;
        private readonly int LogicalScreenHeight;
        private readonly int LogicalScreenWeight;
        private bool SchermIsKleinGemaakt = false;

        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,
            HORZRES = 8,
            DESKTOPHORZRES = 118,
        }

        public static MainForm Main;

        public MainForm()
        {
            InitializeComponent();
            Main = this;
            LaadInstelingen();
            PanelShrink.Visible = false;
            try
            {
                // test monitor grote
                Graphics g = Graphics.FromHwnd(IntPtr.Zero);
                IntPtr desktop = g.GetHdc();
                LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
                int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);
                LogicalScreenWeight = GetDeviceCaps(desktop, (int)DeviceCap.HORZRES);

                ScreenScalingFactor = PhysicalScreenHeight / (float)LogicalScreenHeight;

                if (LogicalScreenWeight < 1850 || LogicalScreenHeight < 1040 || ScreenScalingFactor != 1) // 1920 * 1080 bij 1 scaling
                {
                    Scalling = true;
                    //PanelShrink.Visible = true;
                    //PanelShrink.Location = panel1.Location;
                    //panel1.Size = PanelShrink.Size;
                }
            }
            catch { }

            bijlage.bijlagepath = bijlagepath;
        }

        private void ScaleMainVenster()
        {
            if (SchermIsKleinGemaakt)
            {
                return;
            }

            SchermIsKleinGemaakt = true;

            Text = "Overbrug gescaled window.";

            PanelShrink.Visible = true;
            PanelShrink.Location = panel1.Location;
            panel1.Size = PanelShrink.Size;

            // buttons op panel menu
            foreach (System.Windows.Forms.Button button in panelMenu.Controls.OfType<System.Windows.Forms.Button>())
            {
                ShrinkButton(button);
            }

            ShrinkButton(ButRefresh);

            // locatie panel 2
            ShrinkPanel(panel2);

            // combo box in panel 2
            foreach (System.Windows.Forms.ComboBox button in panel2.Controls.OfType<System.Windows.Forms.ComboBox>())
            {
                ShrinkComboBox(button);
            }

            // label in panel 2
            foreach (Label label in panel2.Controls.OfType<Label>())
            {
                ShrinkLabel(label);
            }

            foreach (System.Windows.Forms.Button button in groupBox1.Controls.OfType<System.Windows.Forms.Button>())
            {
                ShrinkButton(button);
            }

            ShrinkGroupBox(groupBox1);

            ShrinkPanel(panelMenu);

            dataGridView1.DefaultCellStyle.Font = SmallFont;
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.DefaultCellStyle.Font = SmallFont;
            }

            Size nieuw = Size;
            nieuw.Height -= 250;
            Size = nieuw;
        }

        private void ShrinkGroupBox(System.Windows.Forms.GroupBox panel2)
        {
            panel2.Font = SmallFont;
            Point LocNieuw = panel2.Location;
            LocNieuw.Y = (int)(panel2.Location.Y / ScreenScalingFactor);
            LocNieuw.X = (int)(panel2.Location.X / ScreenScalingFactor);
            panel2.Location = LocNieuw;

            Size SizeNew = panel2.Size;
            SizeNew.Width = (int)(SizeNew.Width / ScreenScalingFactor);
            SizeNew.Height = (int)(SizeNew.Height / ScreenScalingFactor);
            panel2.Size = SizeNew;
        }

        private void ShrinkPanel(System.Windows.Forms.Panel panel2)
        {
            Point LocNieuw = panel2.Location;
            LocNieuw.Y = (int)(panel2.Location.Y / ScreenScalingFactor);
            LocNieuw.X = (int)(panel2.Location.X / ScreenScalingFactor);
            panel2.Location = LocNieuw;

            Size SizeNew = panel2.Size;
            SizeNew.Width = (int)(SizeNew.Width / ScreenScalingFactor);
            SizeNew.Height = (int)(SizeNew.Height / ScreenScalingFactor);
            panel2.Size = SizeNew;
        }

        private void ShrinkLabel(Label label)
        {
            label.Font = SmallFont;
            Point LocNieuw = label.Location;
            LocNieuw.Y = (int)(label.Location.Y / ScreenScalingFactor);
            LocNieuw.X = (int)(label.Location.X / ScreenScalingFactor);
            label.Location = LocNieuw;
            label.AutoSize = false;
        }
        private void ShrinkButton(System.Windows.Forms.Button button)
        {
            button.Width = (int)(button.Width / ScreenScalingFactor);
            button.Height = (int)(button.Height / ScreenScalingFactor);

            Point LocNieuw = button.Location;
            LocNieuw.Y = (int)(button.Location.Y / ScreenScalingFactor);
            LocNieuw.X = (int)(button.Location.X / ScreenScalingFactor);
            button.Location = LocNieuw;

            button.Font = SmallFont;
        }

        private void ShrinkComboBox(System.Windows.Forms.ComboBox button)
        {
            button.Font = SmallFont;
            button.Width = (int)(button.Width / ScreenScalingFactor);
            //button.Height = (int)(button.Height / ScreenScalingFactor);
            Point LocNieuw = button.Location;
            LocNieuw.Y = (int)(button.Location.Y / ScreenScalingFactor);
            LocNieuw.X = (int)(button.Location.X / ScreenScalingFactor);
            button.Location = LocNieuw;
        }

        private bool ZoekIV(string zoek)
        {
            if (string.IsNullOrEmpty(zoek))
            {
                return false;
            }

            try
            {
                NamenFunties Q = NamenLijst.First(a => a.PersoneelNummer == zoek);
                return Q.IVWV;
            }
            catch
            {
                return false;
            }
        }
        public Data ZoekDataRecord(int nr)
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

        //private string ZoekNaam(string zoek)
        //{
        //    if (string.IsNullOrEmpty(zoek))
        //    {
        //        return "";
        //    }

        //    try
        //    {
        //        NamenFunties Q = NamenLijst.First(a => a.Index == zoek);
        //        return Q.Naam;
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}

        //private string ZoekInstallatie(string zoek)
        //{
        //    if (string.IsNullOrEmpty(zoek))
        //    {
        //        return "";
        //    }

        //    try
        //    {
        //        InstallatieOnderdeel Q = InstallatieLijst.First(a => a.Index == zoek);
        //        return Q.Instal;
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (Scalling)
            {
                ScaleMainVenster();
            }

            FormMelding md = new FormMelding(FormMelding.Type.Info, "Overbruging 2.0", "R.Majoor");
            md.Show();

            // laad namen
            LaadNamen_lijst();
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
                    if (!string.IsNullOrEmpty(item.Naam))
                    {
                        _ = comboBoxSectie.Items.Add(item.Naam);
                    }
                }
            }

            comboBoxSectie.SelectedIndex = 0;
            comboBoxSoortFilter.SelectedIndex = 0;
            comboBoxStatus.SelectedIndex = 0;

            dataGridView1.Columns.Clear();

            dataGridView1.AutoGenerateColumns = false;

            _ = dataGridView1.Columns.Add("Nr", "Nr");
            dataGridView1.Columns[0].Width = 60;
            _ = dataGridView1.Columns.Add("Datum", "Datum inv.");
            dataGridView1.Columns[1].Width = 100;
            _ = dataGridView1.Columns.Add("Soort", "Soort");
            dataGridView1.Columns[2].Width = 70;
            _ = dataGridView1.Columns.Add("Sectie", "Sectie");
            dataGridView1.Columns[3].Width = 70;

            _ = dataGridView1.Columns.Add("Installatie", "Installatie");
            dataGridView1.Columns[4].Width = 110;
            _ = dataGridView1.Columns.Add("InstalatieDeel", "Instalatie Deel");
            dataGridView1.Columns[5].Width = 175;
            _ = dataGridView1.Columns.Add("Rede", "Rede");
            dataGridView1.Columns[6].Width = 340;

            _ = dataGridView1.Columns.Add("DatumVerl", "Verloopt");
            dataGridView1.Columns[7].Width = 100;

            _ = dataGridView1.Columns.Add("DatumTemp", "DatumTemp");
            dataGridView1.Columns[8].Width = 100;
            _ = dataGridView1.Columns.Add("Kleur", "Kleur");
            dataGridView1.Columns[8].Width = 100;

            dataGridView1.RowHeadersVisible = false;

            if (Scalling)
            {
                dataGridView1.DefaultCellStyle.Font = SmallFont;
                foreach (DataGridViewColumn c in dataGridView1.Columns)
                {
                    c.DefaultCellStyle.Font = SmallFont;
                }
            }

            ButRefresh_Click(this, null);

            // zet update roetine op filters actief
            comboBoxSectie.SelectedIndexChanged += ButRefresh_Click;
            comboBoxSoortFilter.SelectedIndexChanged += ButRefresh_Click;
            comboBoxStatus.SelectedIndexChanged += ButRefresh_Click;

            inlognaam = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];

            if (inlognaam == "ronal")
            {
                inlognaam = "a590588";
            }

            // is bv dan a590588

            if (inlognaam.Length == 7)
            {
                inlognaam = inlognaam.ToLower();
                if (inlognaam[0] == 'a')
                {
                    inlognaam = inlognaam.Substring(1);
                }
            }

            IsIVer.Checked = ZoekIV(inlognaam);
            IsIVer.Text = inlognaam;

            // in oude database stond ik als niet IV. Anders kan ik dit niet aanpassen.
            if (inlognaam == "590588" && !IsIVer.Checked)
            {
                IsIVer.Checked = true;
                _ = MessageBox.Show("Zet IV aan in database ronald.");
            }

            Log.Locatie = AppDomain.CurrentDomain.BaseDirectory + "Data\\Log.txt";
            Log.MaxRegels = 5000;

            try
            {
                NamenFunties Q = NamenLijst.First(a => a.PersoneelNummer == inlognaam);
                LabelUser.Text = Q.Naam;
                rechten = 1;
            }
            catch
            {
                LabelUser.Text = "Gebruiker niet in lijst";
                LabelUser.ForeColor = Color.Teal;
            }

            if (IsIVer.Checked)
            {
                rechten = 2;
            }

            if (inlognaam == "590588")
            {
                PanelDebug.Visible = true;
                RechtenDebug.Value = rechten;
            }
        }

        private void VulGrid()
        {
            if (LijstData.Count > 0)
            {
                dataGridView1.DataSource = LijstData;
            }

            if (dataGridView1.Columns["Nr"].DataPropertyName == "")
            {
                dataGridView1.Columns["Nr"].DataPropertyName = "Regnr";
                dataGridView1.Columns["Datum"].DataPropertyName = "DatumInv";
                dataGridView1.Columns["Soort"].DataPropertyName = "Soort";
                dataGridView1.Columns["Sectie"].DataPropertyName = "Sectie";
                dataGridView1.Columns["Installatie"].DataPropertyName = "Installatie";
                dataGridView1.Columns["InstalatieDeel"].DataPropertyName = "InstallatieDeel";
                dataGridView1.Columns["Rede"].DataPropertyName = "Reden";
                dataGridView1.Columns["DatumVerl"].DataPropertyName = "UitersteDatum";
                dataGridView1.Columns["DatumTemp"].DataPropertyName = "DatumTemp";
                dataGridView1.Columns["Kleur"].DataPropertyName = "Kleur";

            }

            dataGridView1.Columns[8].Visible = false;   // DatumTemp
            dataGridView1.Columns[9].Visible = false;   // Kleur

            labelAantal.Text = LijstData.Count().ToString();

            int index = 0;
            if (labelAantal.Text != "0")
            {
                index = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            }

            KleurAfwijkingen();

            VulPreview(index);
        }

        private void ButRefresh_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            Wait(300);
            dataGridView1.Rows.Clear();

            // laad alle overbrugingen
            LaadData_lijst();

            TelData();

            LabelNietAfgetekendWV.Text = NietAfgetekendWv.ToString();
            LabelDatumVerlopen.Text = VerlopenData.ToString();

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

            VulGrid();
        }

        private void TelData()
        {
            NietAfgetekendWv = 0;
            VerlopenData = 0;
            _ = DateTime.Now;
            foreach (Data a in LijstData)
            {
                BepaalOfKleur(a);
            }
        }

        private void BepaalOfKleur(Data a)
        {
            a.DatumTemp = GetDateTime(a.UitersteDatum);
            a.Kleur = false;

            if (a.DatumWv == string.Empty) // aantal niet afgetekend Wv
            {
                NietAfgetekendWv++;
                a.Kleur = true;
            }

            if (a.DatumTemp.Date < verloopdatum.Date && a.DatumVerw == "")    // datum verlopen
            {
                VerlopenData++;
                a.Kleur = true;
            }
        }

        private void WachtRapportDataSave()
        {
            try
            {
                LaadInstelingen();
                // opslag plek is string 2.

                LaadData_lijst();

                Tellen tellen = new Tellen();
                tellen.VulSectiesVoorTelling();
                foreach (Data a in LijstData)
                {
                    if (a.DatumVerw == "")
                    {
                        tellen.OpTelData(a);
                    }
                }

                tellen.MaakOudeIniFile(instellingen[1] + "\\Overbrug.ini");
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
            }
        }

        public void SaveData_lijst()
        {
            try
            {
                using (Stream stream = File.Open($"{datapath}overbrug.bin", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, LijstData);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();

                    // Melding
                    //FormMelding md = new FormMelding(FormMelding.Type.Save, "Overbruging 2.0", "Save Data Lijst.");
                    //md.Show();
                }
            }
            catch
            {
                GC.Collect();
            }
            //Wait(1000);
            Backup($"{datapath}overbrug.bin");
        }

        public void SaveDataNamen_lijst()
        {
            try
            {
                using (Stream stream = File.Open($"{datapath}namen.bin", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, NamenLijst);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();

                    // Melding
                    //FormMelding md = new FormMelding(FormMelding.Type.Save, "Overbruging 2.0", "Save Namen Lijst.");
                    //md.Show();
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
                using (Stream stream = File.Open($"{datapath}sectie.bin", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, SectieLijst);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();

                    // Melding
                    //FormMelding md = new FormMelding(FormMelding.Type.Save, "Overbruging 2.0", "Save Sectie's Lijst.");
                    //md.Show();
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
                using (Stream stream = File.Open($"{datapath}install.bin", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, InstallatieLijst);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();

                    // Melding
                    //FormMelding md = new FormMelding(FormMelding.Type.Save, "Overbruging 2.0", "Save Instalatie's Lijst.");
                    //md.Show();
                }
            }
            catch
            {
                GC.Collect();
            }
        }

        private void LaadInstelingen()
        {
            string inst = $"{datapath}progdata.ini";
            try
            {
                instellingen = File.ReadAllLines(inst).ToList();
            }
            catch
            {
                _ = MessageBox.Show($"{inst} niet aanwezig of niet te laden");
            }
        }
        public void LaadData_lijst()
        {
            try
            {
                LaadData(5);
            }
            catch{ }
            GC.Collect();
            LastIndex = GetLaatsteRecord();
        }

        private void LaadData(int aantal)
        {
            try
            {
                using (Stream stream = File.Open($"{datapath}overbrug.bin", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    LijstData.Clear();
                    BinaryFormatter bin = new BinaryFormatter();
                    LijstData = (List<Data>)bin.Deserialize(stream);
                    bin = null; // destroy voor volgende keer
                }
            }
            catch
            {
                aantal--;
                if (aantal == 0)
                {
                    _ = MessageBox.Show("Data lijst niet kunnen laden, exit");
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    Thread.Sleep(1000);
                    LaadData(aantal);
                }
            }
        }

        private int GetLaatsteRecord()
        {
            LijstData = LijstData.OrderByDescending(o => o.RegNr).ToList();
            return LijstData[0].RegNr;
        }

        public void LaadSecties_lijst()
        {
            try
            {
                using (Stream stream = File.Open($"{datapath}sectie.bin", FileMode.Open, FileAccess.Read, FileShare.None))
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
                using (Stream stream = File.Open($"{datapath}namen.bin", FileMode.Open, FileAccess.Read, FileShare.None))
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

            NamenLijst = NamenLijst.OrderBy(o => o.Naam).ToList();
        }

        public void LaadInstallaties_lijst()
        {
            try
            {
                using (Stream stream = File.Open($"{datapath}install.bin", FileMode.Open, FileAccess.Read, FileShare.None))
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

            settings.buttonNaam.Enabled = IsIVer.Checked;
            settings.buttonSecties.Enabled = IsIVer.Checked;
            settings.buttonImport.Enabled = inlognaam == "590588";

            DialogResult ret = settings.ShowDialog();
            if (ret == DialogResult.OK)
            {
                EditNamen ed = new EditNamen();
                _ = ed.ShowDialog();
            }
            if (ret == DialogResult.Abort)
            {
                EditSecties editSecties = new EditSecties();
                _ = editSecties.ShowDialog();
            }

            if (ret == DialogResult.Ignore)
            {
                // log view
                LogView LV = new LogView();
                LV.TB.Text = File.ReadAllText(Log.Locatie);
                _ = LV.ShowDialog();
            }

            if (ret == DialogResult.Yes)
            {
                // was export, nu administratie na invullen wachtwoord
                Prompt po = new Prompt();
                string ww = po.ShowDialog("Wachtwoord", "Wachtwoord voor administratie");

                if (ww != DateTime.Now.ToString("ddMM"))
                    return;

                Administratie ad = new Administratie(Main);
                ad.ShowDialog();
            }

            ButRefresh_Click(this, null);
        }

        public void Export()
        {
            // laad
            LaadData_lijst();

            saveFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // zet om naar xml
                ToXML(LijstData, saveFileDialog1.FileName);
            }
        }

        private void ToXML<T>(T obj, string filenaam)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, obj);

                string DataAlsXml = stringWriter.ToString();
                File.WriteAllText(filenaam, DataAlsXml);
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = System.Diagnostics.Process.Start("https://github.com/majoor404/Overbrugging");
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            KillTijdLabel.Text = "29";

            try
            {
                // zoek record
                int regNr = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                TempData = ZoekDataRecord(regNr);

                // maak formulier
                DetailSmall dts = new DetailSmall();
                Detail dt = new Detail();

                bool isgelockt = Geblokkerd.IsLock(GeselRegNr.Text);

                if (isgelockt)
                {
                    dt.LockMode = true;
                    dts.LockMode = true;
                }

                if (LabelUser.Text == "Gebruiker niet in lijst")
                {
                    dt.LockMode = true;
                    dts.LockMode = true;
                }

                // als een IV of WV blokeer
                if (IsIVer.Checked && !isgelockt)
                {
                    Geblokkerd.SetLock(LabelUser.Text, GeselRegNr.Text);
                }

                if (!Scalling)
                {
                    dt.TextBoxRegNr.Text = TempData.RegNr.ToString();
                    dt.TextBoxRegNr.Enabled = false;

                    VulDatailForm(dt, TempData);
                    dt.Bijlage.Visible = bijlage.BijlageAanwezig(dt.TextBoxRegNr.Text);
                    _ = dt.ShowDialog();

                }
                else
                {
                    dts.TextBoxRegNr.Text = TempData.RegNr.ToString();
                    dts.TextBoxRegNr.Enabled = false;

                    VulDatailFormSmall(dts, TempData);
                    dts.Bijlage.Visible = bijlage.BijlageAanwezig(dt.TextBoxRegNr.Text);

                    _ = dts.ShowDialog();
                }

                if (IsIVer.Checked && !isgelockt) // alleen als ik zelf gelockt hebt
                {
                    Geblokkerd.FreeLock(GeselRegNr.Text);
                }

                //refresh
                ButRefresh_Click(this, null);
            }
            catch
            {
                _ = MessageBox.Show($"e = {e.RowIndex}");
            }
        }

        public void SorteerNaamOpNaam()
        {
            NamenLijst.Sort(Sorteer_Namen);
        }

        public int Sorteer_Namen(NamenFunties a, NamenFunties b)
        {
            return a.Naam.CompareTo(b.Naam);
        }

        private void DataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // zoek record
                int regNr = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                VulPreview(regNr);
            }
            catch { }
        }

        private void VulPreview(int index)
        {
            PictureBijlage.Visible = PictureBijlageSmall.Visible = false;
            try
            {
                Data Q = ZoekDataRecord(index);
                TB1.Text = Q.Reden;
                TB2.Text = Q.Uitvoering;
                GeselRegNr.Text = index.ToString();
                TBDINV.Text = Q.DatumInv;
                TBSAP.Text = Q.SapNr;
                TBMOC.Text = Q.MocNr;
                TBSEC.Text = Q.Sectie;
                TBINST.Text = Q.Installatie;
                TBINSTD.Text = Q.InstallatieDeel;
                TBN1.Text = Q.Naam1;
                TBN2.Text = Q.Naam2;

                string HulpTekst = $"Geen MOC : {Q.Reserve1}";
                if (Q.Reserve1 == "")
                {
                    HulpTekst = "";
                }

                if (Q.Soort == "TIW")
                {
                    LabelType.Text = "Tijdelijke Instalatie Wijziging";
                    toolTip1.SetToolTip(LabelType, HulpTekst);
                    toolTip1.SetToolTip(LabelTypeS, HulpTekst);
                }
                if (Q.Soort == "MOC")
                {
                    LabelType.Text = "Management Of Change";
                    toolTip1.SetToolTip(LabelType, "");
                    toolTip1.SetToolTip(LabelTypeS, "");
                }
                if (Q.Soort == "OVERB")
                {
                    LabelType.Text = "Overbruging";
                    toolTip1.SetToolTip(LabelType, HulpTekst);
                    toolTip1.SetToolTip(LabelTypeS, HulpTekst);
                }
                TBDWV.Text = Q.DatumWv;
                TBDWVV.Text = Q.UitersteDatum;
                TBNWV.Text = Q.NaamWV;
                TextBoxBijzIVWV.Text = Q.BijzonderhedenWV;

                TBNVerw.Text = Q.Naamverw;
                TBDVerw.Text = Q.DatumVerw;
                TBTVerw.Text = Q.BijzonderhedenVerw;

                PictureBijlage.Visible = bijlage.BijlageAanwezig(index.ToString());

                if (Scalling)
                {
                    TB1S.Text = Q.Reden;
                    TB2S.Text = Q.Uitvoering;
                    GeselRegNrS.Text = index.ToString();
                    TBDINVS.Text = Q.DatumInv;
                    TBSAPS.Text = Q.SapNr;
                    TBMOCS.Text = Q.MocNr;
                    TBSECS.Text = Q.Sectie;
                    TBINSTS.Text = Q.Installatie;
                    TBINSTDS.Text = Q.InstallatieDeel;
                    TBN1S.Text = Q.Naam1;
                    TBN2S.Text = Q.Naam2;

                    if (Q.Soort == "TIW")
                    {
                        LabelTypeS.Text = "Tijdelijke Instalatie Wijziging";
                    }
                    if (Q.Soort == "MOC")
                    {
                        LabelTypeS.Text = "Management Of Change";
                    }
                    if (Q.Soort == "OVERB")
                    {
                        LabelTypeS.Text = "Overbruging";
                    }
                    TBDWVS.Text = Q.DatumWv;
                    TBDWVVS.Text = Q.UitersteDatum;
                    TBNWVS.Text = Q.NaamWV;
                    TextBoxBijzIVWVS.Text = Q.BijzonderhedenWV;

                    TBNVerwS.Text = Q.Naamverw;
                    TBDVerwS.Text = Q.DatumVerw;
                    TBTVerwS.Text = Q.BijzonderhedenVerw;

                    PictureBijlageSmall.Visible = bijlage.BijlageAanwezig(index.ToString());
                }
            }
            catch
            {
                TB1.Clear();
                TB2.Clear();
                GeselRegNr.Text = string.Empty;
                TBDINV.Text = string.Empty;
                TBSAP.Text = string.Empty;
                TBMOC.Text = string.Empty;
                TBSEC.Text = string.Empty;
                TBINST.Text = string.Empty;
                TBINSTD.Text = string.Empty;
                TBN1.Text = string.Empty;
                TBN2.Text = string.Empty;
                LabelType.Text = string.Empty;
                TBDWV.Text = string.Empty;
                TBDWVV.Text = string.Empty;
                TBNWV.Text = string.Empty;
                TextBoxBijzIVWV.Text = string.Empty;
                TBNVerw.Text = string.Empty;
                TBDVerw.Text = string.Empty;
                TBTVerw.Text = string.Empty;
            }
        }

        private void ButtonNieuw_Click(object sender, EventArgs e)
        {
            if (rechten < 1)
            {
                _ = MessageBox.Show("Geen rechten om nieuwe overbruging te maken.");
                return;
            }

            KeuzeType KS = new KeuzeType();
            DialogResult retKeuzeForm = KS.ShowDialog();

            // maak formulier
            DetailSmall dts = new DetailSmall();
            Detail dt = new Detail();

            TempData = new Data
            {
                Reserve1 = KS.UitgekozenGeenMocRegel
            };

            if (retKeuzeForm == DialogResult.OK)
            {
                // TIW
                TempData.Soort = "TIW";
            }
            if (retKeuzeForm == DialogResult.Yes)
            {
                //OVERB
                TempData.Soort = "OVERB";
            }
            if (retKeuzeForm == DialogResult.Abort)
            {
                //MOC
                TempData.Soort = "MOC";
            }
            if (retKeuzeForm == DialogResult.Cancel)
            {
                return;
            }

            LaadNamen_lijst();

            if (!Scalling)
            {
                VulDropDownItems(dt);
                AutoAanvulNamen(dt);
                TempData.DatumInv = dt.DatumInv.Datum = MajoorString(DateTime.Now);
                _ = dt.ShowDialog();
            }
            else
            {
                VulDropDownItemsSmall(dts);
                AutoAanvulNamenSmall(dts);
                TempData.DatumInv = dts.DatumInv.Datum = MajoorString(DateTime.Now);
                _ = dts.ShowDialog();
            }
            //refresh
            ButRefresh_Click(this, null);
        }

        public string MajoorString(DateTime date)
        {
            // zet datum om in formaat dd-mm-jjjj
            return date.ToString("dd-MM-yyyy");
        }

        public void VulDropDownItems(Detail dt)
        {
            VulSectiesDropDown(dt);
            VulSectiesOnderdeelDropDown(dt);
            VulNamenPersoneel(dt);
            VulNamenIVWVPersoneel(dt);
        }

        public void VulDropDownItemsSmall(DetailSmall dt)
        {
            VulSectiesDropDownSmall(dt);
            VulSectiesOnderdeelDropDownSmall(dt);
            VulNamenPersoneelSmall(dt);
            VulNamenIVWVPersoneelSmall(dt);
        }

        private void VulNamenIVWVPersoneel(Detail dt)
        {
            mycolIVWV.Clear();
            _ = mycolIVWV.Add("");
            dt.ComboBoxIVWV.Items.Clear();
            List<NamenFunties> IVWVFilter = new List<NamenFunties>();
            IVWVFilter = NamenLijst.Where(x => x.IVWV == true).ToList();
            IVWVFilter = IVWVFilter.OrderBy(o => o.Naam).ToList();
            for (int i = 0; i < IVWVFilter.Count; i++)
            {
                _ = dt.ComboBoxIVWV.Items.Add(IVWVFilter[i].Naam);
                _ = mycolIVWV.Add(IVWVFilter[i].Naam);
            }
        }

        private void VulNamenIVWVPersoneelSmall(DetailSmall dt)
        {
            mycolIVWV.Clear();
            _ = mycolIVWV.Add("");
            dt.ComboBoxIVWV.Items.Clear();
            List<NamenFunties> IVWVFilter = new List<NamenFunties>();
            IVWVFilter = NamenLijst.Where(x => x.IVWV == true).ToList();
            IVWVFilter = IVWVFilter.OrderBy(o => o.Naam).ToList();
            for (int i = 0; i < IVWVFilter.Count; i++)
            {
                _ = dt.ComboBoxIVWV.Items.Add(IVWVFilter[i].Naam);
                _ = mycolIVWV.Add(IVWVFilter[i].Naam);
            }
        }

        private void VulNamenPersoneel(Detail dt)
        {
            mycol.Clear();
            _ = mycol.Add(" ");
            dt.ComboBoxNaam1.Items.Clear();
            dt.ComboBoxNaam2.Items.Clear();
            dt.ComboBoxNaamVerw.Items.Clear();
            for (int i = 0; i < NamenLijst.Count; i++)
            {
                _ = dt.ComboBoxNaam1.Items.Add(NamenLijst[i].Naam);
                _ = dt.ComboBoxNaam2.Items.Add(NamenLijst[i].Naam);
                _ = dt.ComboBoxNaamVerw.Items.Add(NamenLijst[i].Naam);
                _ = mycol.Add(NamenLijst[i].Naam);
            }
        }

        private void VulNamenPersoneelSmall(DetailSmall dt)
        {
            mycol.Clear();
            _ = mycol.Add(" ");
            dt.ComboBoxNaam1.Items.Clear();
            dt.ComboBoxNaam2.Items.Clear();
            dt.ComboBoxNaamVerw.Items.Clear();
            for (int i = 0; i < NamenLijst.Count; i++)
            {
                _ = dt.ComboBoxNaam1.Items.Add(NamenLijst[i].Naam);
                _ = dt.ComboBoxNaam2.Items.Add(NamenLijst[i].Naam);
                _ = dt.ComboBoxNaamVerw.Items.Add(NamenLijst[i].Naam);
                _ = mycol.Add(NamenLijst[i].Naam);
            }
        }

        public void VulSectiesOnderdeelDropDown(Detail dt)
        {
            List<InstallatieOnderdeel> InstallatieLijstFilter = new List<InstallatieOnderdeel>();
            InstallatieLijstFilter = InstallatieLijst.Where(x => x.Sectie == dt.ComboBoxSectie.Text).ToList();
            dt.ComboSectieDeel.Items.Clear();
            for (int i = 0; i < InstallatieLijstFilter.Count; i++)
            {
                _ = dt.ComboSectieDeel.Items.Add(InstallatieLijstFilter[i].Instal);
            }
        }

        public void VulSectiesOnderdeelDropDownSmall(DetailSmall dt)
        {
            List<InstallatieOnderdeel> InstallatieLijstFilter = new List<InstallatieOnderdeel>();
            InstallatieLijstFilter = InstallatieLijst.Where(x => x.Sectie == dt.ComboBoxSectie.Text).ToList();
            dt.ComboSectieDeel.Items.Clear();
            for (int i = 0; i < InstallatieLijstFilter.Count; i++)
            {
                _ = dt.ComboSectieDeel.Items.Add(InstallatieLijstFilter[i].Instal);
            }
        }

        private void VulSectiesDropDown(Detail dt)
        {
            dt.ComboBoxSectie.Items.Clear();
            for (int i = 0; i < MainForm.Main.SectieLijst.Count; i++)
            {
                _ = dt.ComboBoxSectie.Items.Add(MainForm.Main.SectieLijst[i].Naam);
            }
        }

        private void VulSectiesDropDownSmall(DetailSmall dt)
        {
            dt.ComboBoxSectie.Items.Clear();
            for (int i = 0; i < MainForm.Main.SectieLijst.Count; i++)
            {
                _ = dt.ComboBoxSectie.Items.Add(MainForm.Main.SectieLijst[i].Naam);
            }
        }

        private void ButtonWijzig_Click(object sender, EventArgs e)
        {
            if (GeselRegNr.Text == "0000")
            {
                return;
            }

            KillTijdLabel.Text = "29";

            try
            {
                // zoek record
                int regNr = int.Parse(GeselRegNr.Text);
                TempData = ZoekDataRecord(regNr);

                // maak formulier
                DetailSmall dts = new DetailSmall();
                Detail dt = new Detail();

                bool isgelockt = Geblokkerd.IsLock(GeselRegNr.Text);

                if (isgelockt)
                {
                    dt.LockMode = true;
                    dts.LockMode = true;
                }

                // als een IV of WV blokeer
                if (IsIVer.Checked && !isgelockt)
                {
                    Geblokkerd.SetLock(LabelUser.Text, GeselRegNr.Text);
                }

                if (!Scalling)
                {
                    dt.TextBoxRegNr.Text = TempData.RegNr.ToString();
                    dt.TextBoxRegNr.Enabled = false;

                    VulDatailForm(dt, TempData);
                    dt.Bijlage.Visible = bijlage.BijlageAanwezig(dt.TextBoxRegNr.Text);

                    _ = dt.ShowDialog();
                }
                else
                {
                    dts.TextBoxRegNr.Text = TempData.RegNr.ToString();
                    dts.TextBoxRegNr.Enabled = false;

                    VulDatailFormSmall(dts, TempData);
                    dts.Bijlage.Visible = bijlage.BijlageAanwezig(dt.TextBoxRegNr.Text);

                    _ = dts.ShowDialog();
                }

                if (IsIVer.Checked && !isgelockt)  // alleen als ik zelf gelockt hebt
                {
                    Geblokkerd.FreeLock(GeselRegNr.Text);
                }

                //refresh
                ButRefresh_Click(this, null);
            }
            catch
            {
                _ = MessageBox.Show($"Kon detail van record {GeselRegNr.Text} niet laden");
            }
        }

        private void VulDatailForm(Detail dt, Data Q)
        {
            try
            {
                LaadNamen_lijst();

                // vullen dropdown items
                VulDropDownItems(dt);

                dt.ComboBoxSectie.Text = Q.Sectie; // daarvoor heb ik wel sectie nodig.

                // bovenste panel vullen met data
                dt.DatumInv.Datum = Q.DatumInv;
                dt.TextBoxSapNr.Text = Q.SapNr;
                dt.TextBoxMocNr.Text = Q.MocNr;
                dt.ComboSectieDeel.Text = Q.Installatie;
                dt.TextBoxInstDeel.Text = Q.InstallatieDeel;
                dt.ComboBoxNaam1.Text = Q.Naam1;
                dt.ComboBoxNaam2.Text = Q.Naam2;
                dt.TextBoxRede.Text = Q.Reden;
                dt.TextBoxOplossing.Text = Q.Uitvoering;
                // middelste panel
                dt.DatumWv.Datum = Q.DatumWv;
                dt.ComboBoxIVWV.Text = Q.NaamWV;
                dt.DatumVerloopTIW.Datum = Q.UitersteDatum;
                dt.TextBoxBijzIVWV.Text = Q.BijzonderhedenWV;
                dt.BTSoort.Text = Q.Soort;
                // onderste panel
                dt.DatumVerw.Datum = Q.DatumVerw;
                dt.ComboBoxNaamVerw.Text = Q.Naamverw;
                dt.TextBoxBijzVerw.Text = Q.BijzonderhedenVerw;

                AutoAanvulNamen(dt);
            }
            catch
            {
                _ = MessageBox.Show($"Error VulDetailForm");
            }
        }

        private void VulDatailFormSmall(DetailSmall dt, Data Q)
        {
            try
            {
                LaadNamen_lijst();

                // vullen dropdown items
                VulDropDownItemsSmall(dt);
                dt.ComboBoxSectie.Text = Q.Sectie; // daarvoor heb ik wel sectie nodig.

                // bovenste panel vullen met data
                dt.DatumInv.Datum = Q.DatumInv;
                dt.TextBoxSapNr.Text = Q.SapNr;
                dt.TextBoxMocNr.Text = Q.MocNr;
                dt.ComboSectieDeel.Text = Q.Installatie;
                dt.TextBoxInstDeel.Text = Q.InstallatieDeel;
                dt.ComboBoxNaam1.Text = Q.Naam1;
                dt.ComboBoxNaam2.Text = Q.Naam2;
                dt.TextBoxRede.Text = Q.Reden;
                dt.TextBoxOplossing.Text = Q.Uitvoering;
                // middelste panel
                dt.DatumWv.Datum = Q.DatumWv;
                dt.ComboBoxIVWV.Text = Q.NaamWV;
                dt.DatumVerloopTIW.Datum = Q.UitersteDatum;
                dt.TextBoxBijzIVWV.Text = Q.BijzonderhedenWV;
                dt.BTSoort.Text = Q.Soort;
                // onderste panel
                dt.DatumVerw.Datum = Q.DatumVerw;
                dt.ComboBoxNaamVerw.Text = Q.Naamverw;
                dt.TextBoxBijzVerw.Text = Q.BijzonderhedenVerw;

                AutoAanvulNamenSmall(dt);
            }
            catch
            {
                _ = MessageBox.Show($"Error VulDetailForm");
            }
        }

        private void AutoAanvulNamen(Detail dt)
        {
            dt.ComboBoxIVWV.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dt.ComboBoxIVWV.AutoCompleteSource = AutoCompleteSource.CustomSource;
            dt.ComboBoxIVWV.AutoCompleteCustomSource = mycolIVWV;
            dt.ComboBoxNaamVerw.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dt.ComboBoxNaamVerw.AutoCompleteSource = AutoCompleteSource.CustomSource;
            dt.ComboBoxNaamVerw.AutoCompleteCustomSource = mycolIVWV;

            dt.ComboBoxNaam1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dt.ComboBoxNaam1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            dt.ComboBoxNaam1.AutoCompleteCustomSource = mycol;
            dt.ComboBoxNaam2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dt.ComboBoxNaam2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            dt.ComboBoxNaam2.AutoCompleteCustomSource = mycol;
        }

        private void AutoAanvulNamenSmall(DetailSmall dt)
        {
            dt.ComboBoxIVWV.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dt.ComboBoxIVWV.AutoCompleteSource = AutoCompleteSource.CustomSource;
            dt.ComboBoxIVWV.AutoCompleteCustomSource = mycolIVWV;
            dt.ComboBoxNaamVerw.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dt.ComboBoxNaamVerw.AutoCompleteSource = AutoCompleteSource.CustomSource;
            dt.ComboBoxNaamVerw.AutoCompleteCustomSource = mycolIVWV;

            dt.ComboBoxNaam1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dt.ComboBoxNaam1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            dt.ComboBoxNaam1.AutoCompleteCustomSource = mycol;
            dt.ComboBoxNaam2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dt.ComboBoxNaam2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            dt.ComboBoxNaam2.AutoCompleteCustomSource = mycol;
        }

        private void Backup(string file)
        {
            DebugMes($"Backup {file}");
            string nieuw_naam = "";
            try
            {
                if (File.Exists(file))
                {
                    DebugMes($"File {file} bestaat");

                    string backuppath = AppDomain.CurrentDomain.BaseDirectory + @"Backup\";
                    DirectoryInfo di = new DirectoryInfo(backuppath);
                    bool bestaat = di.Exists;

                    DebugMes($"backup path =  {backuppath}");
                    DebugMes($"deze backup path bestaat ? {bestaat}");

                    if (!bestaat)
                    {
                        DebugMes($"Backup dir maken");
                        _ = Directory.CreateDirectory(backuppath);
                        DebugMes("Backup dir aangemaakt!");
                    }

                    string s = DateTime.Now.ToString("MM-dd-yyyy HH-mm-ss");

                    nieuw_naam = backuppath + @"overbrug_" + s + ".bin";

                    DebugMes($"niewe naam {nieuw_naam}");
                    //File.Copy(file, nieuw_naam, true);  // overwrite oude file
                    
                    // dit ging fout als orgineel nog gelockt was.
                    CopyWithRetry(file, nieuw_naam);

                    List<FileInfo> files = new DirectoryInfo("Backup").EnumerateFiles("*overbrug_*")
                                    .OrderByDescending(f => f.CreationTime)
                                    .Skip(25)
                                    .ToList();

                    files.ForEach(f => f.Delete());
                }
                else
                {
                    _ = MessageBox.Show($"Backup ging fout\n{file} niet aanwezig");
                }
            }
            catch(Exception ex) 
            {
                _ = MessageBox.Show($"Save backup ging fout\n{nieuw_naam}\n{ex}");
            }
        }

        public void Wait(int milliseconds)
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0)
            {
                return;
            }

            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void ButZoek_Click(object sender, EventArgs e)
        {
            Zoek zk = new Zoek();
            _ = zk.ShowDialog();

            LaadData_lijst();
            dataGridView1.DataSource = null;

            List<Data> temp = new List<Data>();
            foreach (Data a in LijstData)
            {
                if (zk.CBNaam.Checked)
                {
                    if (a.Naam1.ToLower().Contains(zk.TBZoek.Text.ToLower()))
                    {
                        temp.Add(a);
                        continue;
                    }
                }
                if (zk.CBReden.Checked)
                {
                    if (a.Reden.ToLower().Contains(zk.TBZoek.Text.ToLower()))
                    {
                        temp.Add(a);
                        continue;
                    }
                }
                if (zk.CBOplossing.Checked)
                {
                    if (a.Uitvoering.ToLower().Contains(zk.TBZoek.Text.ToLower()))
                    {
                        temp.Add(a);
                        continue;
                    }
                }
                if (zk.CBSap.Checked)
                {
                    if (a.SapNr.ToLower().Contains(zk.TBZoek.Text.ToLower()))
                    {
                        temp.Add(a);
                        continue;
                    }
                }
                if (zk.CBMoc.Checked)
                {
                    if (a.MocNr.ToLower().Contains(zk.TBZoek.Text.ToLower()))
                    {
                        temp.Add(a);
                        continue;
                    }
                }
                if (zk.CBIVWV.Checked)
                {
                    if (a.NaamWV.ToLower().Contains(zk.TBZoek.Text.ToLower()))
                    {
                        temp.Add(a);
                        continue;
                    }
                }
                if (zk.CBNum.Checked)
                {
                    bool ok = int.TryParse(zk.TBZoek.Text, out int reg);
                    if (ok && a.RegNr == reg)
                    {
                        temp.Add(a);
                        continue;
                    }
                }
                if (zk.CBInst.Checked)
                {
                    if (a.Installatie.ToLower().Contains(zk.TBZoek.Text.ToLower()))
                    {
                        temp.Add(a);
                        continue;
                    }
                }
            }

            foreach (Data a in temp)
            {
                BepaalOfKleur(a);
            }

            LijstData = temp;
            VulGrid();
        }
        private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            KillTijdLabel.Text = "29";

            DataGridViewColumn newColumn = dataGridView1.Columns[e.ColumnIndex];
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn)
                {
                    if (SortRichting == SortOrder.Ascending)
                    {
                        SortRichting = SortOrder.Descending;
                        direction = ListSortDirection.Descending;
                    }
                    else
                    {
                        SortRichting = SortOrder.Ascending;
                        direction = ListSortDirection.Ascending;
                    }
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    SortRichting = SortOrder.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
                SortRichting = SortOrder.Ascending;

            }

            oldColumn = newColumn;

            //// Sort the selected column.
            if (Sort(newColumn, direction))
            {

                Wait(300);

                if (LijstData.Count > 0)
                {
                    dataGridView1.DataSource = LijstData;
                }

                newColumn.HeaderCell.SortGlyphDirection = SortRichting == SortOrder.Ascending ? SortOrder.Ascending : SortOrder.Descending;

                KleurAfwijkingen();
            }
        }

        private void KleurAfwijkingen()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[9].Value.ToString() == "True")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(243, 221, 228); // Azure;
                }
            }
        }

        private bool Sort(DataGridViewColumn newColumn, ListSortDirection richting)
        {
            try
            {
                if (newColumn.DataPropertyName == "Regnr")
                {
                    LijstData = richting == ListSortDirection.Ascending
                        ? LijstData.OrderBy(o => o.RegNr).ToList()
                        : LijstData.OrderByDescending(o => o.RegNr).ToList();
                }

                if (newColumn.DataPropertyName == "DatumInv")
                {
                    foreach (Data q in LijstData)
                    {
                        q.DatumTemp = GetDateTime(q.DatumInv);
                    }

                    LijstData = richting == ListSortDirection.Ascending
                        ? LijstData.OrderBy(o => o.DatumTemp).ToList()
                        : LijstData.OrderByDescending(o => o.DatumTemp).ToList();
                }

                if (newColumn.DataPropertyName == "Soort")
                {
                    LijstData = richting == ListSortDirection.Ascending
                        ? LijstData.OrderBy(o => o.Soort).ToList()
                        : LijstData.OrderByDescending(o => o.Soort).ToList();
                }

                if (newColumn.DataPropertyName == "Sectie")
                {
                    LijstData = richting == ListSortDirection.Ascending
                        ? LijstData.OrderBy(o => o.Sectie).ToList()
                        : LijstData.OrderByDescending(o => o.Sectie).ToList();
                }

                if (newColumn.DataPropertyName == "Installatie")
                {
                    LijstData = richting == ListSortDirection.Ascending
                        ? LijstData.OrderBy(o => o.Installatie).ToList()
                        : LijstData.OrderByDescending(o => o.Installatie).ToList();
                }

                if (newColumn.DataPropertyName == "InstallatieDeel")
                {
                    LijstData = richting == ListSortDirection.Ascending
                        ? LijstData.OrderBy(o => o.InstallatieDeel).ToList()
                        : LijstData.OrderByDescending(o => o.InstallatieDeel).ToList();
                }

                if (newColumn.DataPropertyName == "Reden")
                {
                    LijstData = richting == ListSortDirection.Ascending
                        ? LijstData.OrderBy(o => o.Reden).ToList()
                        : LijstData.OrderByDescending(o => o.Reden).ToList();
                }
                if (newColumn.DataPropertyName == "UitersteDatum")
                {
                    foreach (Data q in LijstData)
                    {
                        q.DatumTemp = GetDateTime(q.UitersteDatum);
                    }

                    LijstData = richting == ListSortDirection.Ascending
                        ? LijstData.OrderBy(o => o.DatumTemp).ToList()
                        : LijstData.OrderByDescending(o => o.DatumTemp).ToList();
                }
                return true;
            }
            catch { return false; }
        }

        public DateTime GetDateTime(string datum) // 21-12-2023 09-11-2023 20-01-2023
        {
            if (string.IsNullOrEmpty(datum))
            {
                return DateTime.Now;
            }
            if (datum.Length != 10)
            {
                _ = MessageBox.Show($"Datum string is fout, niet 10 char\n{datum}");
                return DateTime.Now;
            }
            int jaar = int.Parse(datum.Substring(6, 4));
            int maand = int.Parse(datum.Substring(3, 2));
            int dag = int.Parse(datum.Substring(0, 2));

            if (dag == 31 && maand == 4)
            {
                dag = 30;
            }

            DateTime ret = new DateTime(jaar, maand, dag);
            return ret;
        }

        private void IVWVVraag_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = MessageBox.Show("Door inlognaam windows (personeel nr)\nKrijg u rechten voor invoer\nAls u WV of IV bent kunt u ook overnemen.");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // save data voor wachtrapport
            WachtRapportDataSave();
        }

        private void BTMocNodig_Click(object sender, EventArgs e)
        {
            LaadInstelingen();
            // moc nodig link op plek 3
            try
            {
                string mocnodig = instellingen[2];
                _ = Process.Start(mocnodig);
            }
            catch
            {
                _ = MessageBox.Show("Link MOC nodig niet aanwezig!");
            }
        }

        private void BTMOCAanmaken_Click(object sender, EventArgs e)
        {
            LaadInstelingen();
            // moc nodig link op plek 4
            try
            {
                string mocaanmaken = instellingen[3];
                _ = Process.Start(mocaanmaken);
            }
            catch
            {
                _ = MessageBox.Show("Link MOC aanmaken niet aanwezig!");
            }
        }

        private void KillTimer_Tick(object sender, EventArgs e)
        {
            int tijd = int.Parse(KillTijdLabel.Text);

            tijd--;

            KillTijdLabel.Text = tijd.ToString();

            if (tijd < 0 || File.Exists("kill.ini"))
            {
                Process.GetCurrentProcess().Kill();
            }
        }

        private void LabelDatumVerlopen_Click(object sender, EventArgs e)
        {
            LaadData_lijst();
            dataGridView1.DataSource = null;
            List<Data> temp = new List<Data>();

            foreach (Data q in LijstData)
            {
                q.DatumTemp = GetDateTime(q.UitersteDatum);
                if (q.DatumTemp.Date < DateTime.Now.AddDays(1).Date && q.DatumVerw == string.Empty)
                {
                    temp.Add(q);
                    q.Kleur = true;
                }
            }

            LijstData = temp;
            VulGrid();
        }

        private void LabelNietAfgetekendWV_Click(object sender, EventArgs e)
        {
            LaadData_lijst();
            dataGridView1.DataSource = null;
            List<Data> temp = new List<Data>();

            foreach (Data q in LijstData)
            {
                if (q.DatumWv == string.Empty)
                {
                    temp.Add(q);
                    q.Kleur = true;
                }
            }

            LijstData = temp;
            VulGrid();
        }

        public bool PersoneelNummerInLijst(string persNr)
        {
            try
            {
                NamenFunties Q = NamenLijst.First(a => a.PersoneelNummer == persNr);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool BijlageFormOpenenMetJuisteRegnr(string ID, bool large = true)
        {
            bijlage.ID = ID;
            bijlage.Width = !large ? 414 : 627;
            _ = bijlage.ShowDialog();

            DebugMes($"Bijlage form geopend met ID {ID}");
            return bijlage.BijlageAanwezig(ID);
        }

        private void PictureBijlageSmall_Click(object sender, EventArgs e)
        {
            _ = MainForm.Main.BijlageFormOpenenMetJuisteRegnr(GeselRegNrS.Text, false);
        }

        private void PictureBijlage_Click(object sender, EventArgs e)
        {
            _ = MainForm.Main.BijlageFormOpenenMetJuisteRegnr(GeselRegNr.Text, false);
        }

        private void RechtenDebug_ValueChanged(object sender, EventArgs e)
        {
            rechten = (int)RechtenDebug.Value;
        }

        private void CBSmall_CheckedChanged(object sender, EventArgs e)
        {
            Scalling = true;
            ScreenScalingFactor = 1.4f;
            ScaleMainVenster();
            CBSmall.Enabled = false;
        }

        private void DebugCB_CheckedChanged(object sender, EventArgs e)
        {
            DebugMes("Debug mode aan, om wat problemen te melden.");
        }
        public static void DebugMes(string mes)
        {
            if (!Main.DebugCB.Checked)
            {
                return;
            }
            _ = MessageBox.Show(mes);
        }

        public static void CopyWithRetry(string sourceFile, string destinationFile, int maxRetries = 10, int delayMs = 500)
        {
            int attempts = 0;
            while (true)
            {
                try
                {
                    File.Copy(sourceFile, destinationFile, true);
                    DebugMes($"Backup gemaakt van \n{sourceFile} naar \n{destinationFile}");
                    return; // Success
                }
                catch (IOException ex)
                {
                    attempts++;
                    if (attempts >= maxRetries)
                    {
                        throw new IOException($"Failed to copy file after {maxRetries} attempts: {ex.Message}", ex);
                    }

                    Thread.Sleep(delayMs);
                }
            }
        }

        public class Prompt
        {
            public string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
                TextBox textBox = new TextBox() { Left = 50, Top = 40, Width = 400 };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 80, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }
    }
}