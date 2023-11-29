using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Overbrugging
{
    public partial class MainForm : Form
    {
        private bool Drag;
        private int MouseX;
        private int MouseY;
        
        public static List<OudeOverbrugging> OudeLijst = new List<OudeOverbrugging>();
        public static List<NamenFunties> NamenLijst = new List<NamenFunties>();
        public static List<Secties> SectieLijst = new List<Secties>();
        public static List<InstallatieOnderdeel> InstallatieLijst = new List<InstallatieOnderdeel>();
        public static List<OverBrugRecord> LijstOverbrugingen = new List<OverBrugRecord> { };
        public static List<Data> LijstData = new List<Data>();

        public MainForm()
        {
            InitializeComponent();
        }


        private static T FromXML<T>(string xml)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute
            {
                ElementName = "ArrayOfOverbrugje",
                IsNullable = true
            };

            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T), xRoot);
                return (T)serializer.Deserialize(stringReader);
            }
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
                    if (items[count++] == "True") { nf.Funtie = true; } else { nf.Funtie = false; };
                    if (items[count++] == "True") { nf.IVW = true; } else { nf.IVW = false; };

                    NamenLijst.Add(nf);
                }

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


                _ = MessageBox.Show("Dan nu samen voegen tot nieuwe opslag class.");

                foreach (OudeOverbrugging o in OudeLijst)
                {
                    Data a = new Data();
                    a.RegNr = o.RegNr;
                    a.DatumInv = NaarDateTime(o.DatumInv);
                    a.SapNr = o.SrsNr;
                    a.MocNr = o.MocRsNr;

                    labelAantal.Text = a.RegNr;
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
                    a.WerkVerg = MaakBool(o.WerkVerg);
                    a.WerkVergNr = o.WerkVergNr;

                    a.DatumWv = NaarDateTime(o.DatumWv);
                    a.NaamWV = ZoekNaam(o.NaamWV); 

                    a.UitersteDatum = NaarDateTime(o.UitersteDatum);
                    a.DatumVerloopWV = NaarDateTime(o.DatumVerloopWV);

                    a.Soort = 0; // default
                    if (o.TIWOB == "Tijdelijke Installatie Wijziging")
                        a.Soort = 1;
                    if (o.TIWOB == "Management Of Change")
                        a.Soort = 2;

                    a.BijzonderhedenWV = o.BijzonderhedenWV;

                    ////verwijderen
                    a.Naamverw = ZoekNaam(o.NaamKKDverw);
                    a.DatumVerw = NaarDateTime(o.DatumVerw);
                    a.BijzonderhedenVerw = o.BijzonderhedenVerw;

                    a.ReserveS1 = "";
                    a.ReserveS2 = "";
                    a.ReserveI1 = 0;
                    a.ReserveI2 = 0;
                    a.ReserveD1 = DateTime.Now;
                    a.ReserveD2 = DateTime.Now;
                    a.ReserveB1 = false;
                    a.ReserveB2 = false;

                    LijstData.Add(a);
                }

            }

            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);

            }
        }

        private DateTime NaarDateTime(string Datum) // is van format "19-11-2023 00:00:00" of "9-4-2001 00:00:00"
        {
            if(string.IsNullOrEmpty(Datum))
                return DateTime.Now;
            // verwijder tijd
            int pos = Datum.IndexOf(" ");
            Datum = Datum.Substring(0, pos);

            string []temp = Datum.Split('-');

            int Dag = int.Parse(temp[0]);
            int Maand = int.Parse(temp[1]);
            int Jaar = int.Parse(temp[2]);

            DateTime ret = new DateTime(Jaar, Maand, Dag);
            return ret;
        }

        private bool MaakBool(string vraag)
        {
            if (string.IsNullOrEmpty(vraag))
                return false;
            if (vraag == "Ja")
                return true;
            return false;
        }
        private String ZoekSectie(string zoek)
        {
            if (string.IsNullOrEmpty(zoek))
                return "";
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

        private String ZoekNaam(string zoek)
        {
            if (string.IsNullOrEmpty(zoek))
                return "";
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

        private string ZoekInstallatie(string zoek)
        {
            if (string.IsNullOrEmpty(zoek))
                return "";
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
        //private void panelTop_MouseDown(object sender, MouseEventArgs e)
        //{
        //    Drag = true;
        //    MouseX = Cursor.Position.X - this.Left;
        //    MouseY = Cursor.Position.Y - this.Top;
        //}

        //private void panelTop_MouseUp(object sender, MouseEventArgs e)
        //{
        //    Drag = false;
        //}

        //private void panelTop_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if(Drag)
        //    {
        //        this.Top = Cursor.Position.Y - MouseY;
        //        this.Left = Cursor.Position.X - MouseX;
        //    }
        //}

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            comboBoxSoortFilter.SelectedIndex = 0;
        }

        private void dataBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
