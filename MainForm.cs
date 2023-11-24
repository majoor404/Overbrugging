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
        public static List<OudeOverbrugging> OudeLijst = new List<OudeOverbrugging>();

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
            MessageBox.Show("Inlezen Overb1.mdb.csv");
            try
            {
                string[] csvTekst = File.ReadAllLines($"Overb1.mdb.csv");
                string[] items;
                OudeLijst.Clear();

                OudeOverbrugging ov = new OudeOverbrugging();
                for (int i = 0; csvTekst.Count() > i; i++)
                {
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
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
