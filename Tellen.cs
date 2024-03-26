using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Overbrugging
{
    public class Tellen
    {
        public class Tel
        {
            public Tel() { }
            public string SectieNaam { get; set; }
            public int OVERB { get; set; }
            public int TIW { get; set; }
            public int MOC { get; set; }
            public int OVERBVerl { get; set; }
            public int TIWVerl { get; set; }
            public int MOCVerl { get; set; }
        }

        public static List<Tel> TelLijst = new List<Tel>();

        public void OpTelData(Data a)
        {
            foreach (Tel tel in TelLijst)
            {
                if (tel.SectieNaam == a.Sectie)
                {
                    DateTime uiterstedatum = MainForm.Main.GetDateTime(a.UitersteDatum);
                    if (a.Soort == "OVERB")
                    {
                        tel.OVERB++;
                    }

                    if (a.Soort == "TIW")
                    {
                        tel.TIW++;
                    }

                    if (a.Soort == "MOC")
                    {
                        tel.MOC++;
                    }

                    if (uiterstedatum < DateTime.Now)
                    {
                        if (a.Soort == "OVERB")
                        {
                            tel.OVERBVerl++;
                        }

                        if (a.Soort == "TIW")
                        {
                            tel.TIWVerl++;
                        }

                        if (a.Soort == "MOC")
                        {
                            tel.MOCVerl++;
                        }
                    }
                }
            }
        }
        public void VulSectiesVoorTelling()
        {
            foreach (Secties sec in MainForm.Main.SectieLijst)
            {
                Tel tel = new Tel
                {
                    SectieNaam = sec.Naam
                };
                TelLijst.Add(tel);
            }
        }
        public void SaveOverbrugXml(string filenaam)
        {
            try
            {
                string xmlTekst = ToXML(TelLijst);
                File.WriteAllText(filenaam, xmlTekst);
            }
            catch { }
        }
        public static string ToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
        public void MaakOudeIniFile(string filename)
        {
            // maak een oude ini file
            // moet dus nieuwe sectie naam vertalen naar oude plek/naam in ini file.

            string[] NieuweIniFIle = new string[56]; 

            for (int i = 0;i<NieuweIniFIle.Length;i++)
            {
                NieuweIniFIle[i] = "0";
            }

            foreach (Tel tel in TelLijst)
            {
                int index = -1;
                if(tel.SectieNaam == "RST")
                    index = 0;
                if (tel.SectieNaam == "CON")
                    index = 1;
                if (tel.SectieNaam == "PBI")
                    index = 2;
                if (tel.SectieNaam == "PVK")
                    index = 3;
                if (tel.SectieNaam == "CGM")
                    index = 4;
                if (tel.SectieNaam == "SKV")
                    index = 5;
                if (tel.SectieNaam == "ALG")
                    index = 6;
                if (tel.SectieNaam == "PG&A - PA")
                {
                    index = 7;
                    
                    //// samenvoegen oude naam met nieuwe naam
                    //// AOV en PGA&A - PG
                    //Tel telPG = TelLijst.First(a => a.SectieNaam == "PGA&A - PG");
                    //tel.OVERB += telPG.OVERB;
                    //tel.OVERBVerl += telPG.OVERBVerl;
                    //tel.TIW += telPG.TIW;
                    //tel.TIWVerl += telPG.TIWVerl;
                    //tel.MOC += telPG.MOC;
                    //tel.MOCVerl += telPG.MOCVerl;
                }

                if (index > -1)
                {
                    NieuweIniFIle[index] = tel.OVERB.ToString();            // overbrugging
                    NieuweIniFIle[index + 8] = "0";                         // overbrugging met werkvergunning
                    NieuweIniFIle[index + 16] = tel.OVERBVerl.ToString();   // overbrugging verlopen.
                    NieuweIniFIle[index + 24] = tel.TIW.ToString();         // tiw
                    NieuweIniFIle[index + 32] = tel.TIWVerl.ToString();     // tiw verlopen
                    NieuweIniFIle[index + 40] = tel.MOC.ToString();         // moc
                    NieuweIniFIle[index + 48] = tel.MOCVerl.ToString();     // moc verlopen
                }

            }

            File.WriteAllLines(filename, NieuweIniFIle);
        }
    }
}
