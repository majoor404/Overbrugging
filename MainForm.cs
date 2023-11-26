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
                    nf.Index = int.Parse(items[count++]);
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
                    sc.Index = int.Parse(items[count++]);
                    sc.Naam = items[count++];
                    SectieLijst.Add(sc);
                }

                _ = MessageBox.Show("Dan nu samen voegen tot nieuwe opslag class.");

                foreach (OudeOverbrugging o in OudeLijst)
                {
                    Data a = new Data();
                    a.RegNr = o.RegNr;
                    a.DatumInv = NaarDateTime(o.DatumInv);
                    a.SapNr = o.SrsNr;
                    a.MocNr = o.MocRsNr;

                    a.Sectie = ZoekSectie(o.Sectie);
                    //public string Installatie;
                    //public string InstallatieDeel;

                    //public string Naam1;
                    //public string Naam2;
                    //public string Ploeg;

                    //public string Reden;
                    //public string Uitvoering;

                    //// ivwv
                    //public bool WerkVerg;
                    //public string WerkVergNr;

                    //public DateTime DatumWv;
                    //public string NaamWV;

                    //public DateTime UitersteDatum;
                    //public DateTime DatumVerloopWV;

                    //public int Soort;

                    //public string BijzonderhedenWV;

                    ////verwijderen
                    //public string Naamverw;
                    //public DateTime DatumVerw;
                    //public string BijzonderhedenVerw;

                    //public string ReserveS1;
                    //public string ReserveS2;
                    //public int ReserveI1;
                    //public int ReserveI2;
                    //public DateTime ReserveD1;
                    //public DateTime ReserveD2;
                    //public bool ReserveB1;


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

        private String ZoekSectie(string nummer)
        {
            int zoek = int.Parse(nummer);
            foreach (Secties s in SectieLijst)
            {
                if (zoek == s.Index)
                    return s.Naam;
            }
            return "";
        }


        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            Drag = true;
            MouseX = Cursor.Position.X - this.Left;
            MouseY = Cursor.Position.Y - this.Top;
        }

        private void panelTop_MouseUp(object sender, MouseEventArgs e)
        {
            Drag = false;
        }

        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        {
            if(Drag)
            {
                this.Top = Cursor.Position.Y - MouseY;
                this.Left = Cursor.Position.X - MouseX;
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            comboBoxSoortFilter.SelectedIndex = 0;
        }
    }
}
