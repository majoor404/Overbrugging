using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

namespace Overbrugging
{
    public class Lock
    {
        public class Loc
        {
            public Loc() { }
            public Loc(string naam, string record, DateTime starttijd)
            {
                Naam = naam;
                Record = record;
                Starttijd = starttijd;
            }

            public string Naam { get; set; }
            public string Record { get; set; }
            public DateTime Starttijd { get; set; }

        }

        public static List<Loc> LockList = new List<Loc>();

        private readonly string datapath = AppDomain.CurrentDomain.BaseDirectory + "Data\\";

        private bool LaadLockFile()
        {
            try
            {
                LockList.Clear();
                string xmlTekst = File.ReadAllText($"{datapath}Lock.xml");
                LockList = FromXML<List<Loc>>(xmlTekst);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void SaveLockFile()
        {
            try
            {
                using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
                {
                    string xmlTekst = ToXML(LockList);
                    File.WriteAllText($"{datapath}Lock.xml", xmlTekst);
                }

            }
            catch { }
        }

        public void SetLock(string Naam, string Record)
        {
            _ = LaadLockFile();
            Loc nieuw = new Loc(Naam, Record, DateTime.Now);
            LockList.Add(nieuw);
            SaveLockFile();
        }

        public void FreeLock(string Record)
        {
            if (LaadLockFile())
            {
                try
                {
                    Loc Rec = LockList.First(a => a.Record == Record);
                    _ = LockList.Remove(Rec);
                    SaveLockFile();
                }
                catch { };
            }
        }

        public bool IsLock(string Record)
        {
            if (LaadLockFile())
            {
                if (LockList.Count == 0)
                {
                    return false;
                }

                try
                {
                    Loc Rec = LockList.First(a => a.Record == Record);

                    // als lock door zelfde gebruiker en zelfde record, gooi ik hem los , zou normaal nooit kunnen ;-)
                    if (Rec.Naam == MainForm.Main.LabelUser.Text)
                    {
                        FreeLock(Record);
                        return false;
                    }

                    // als lock > 30 minuten, gooi ik hem los.
                    DateTime test = DateTime.Now;
                    test = test.AddMinutes(-30);
                    if (Rec.Starttijd < test)
                    {
                        FreeLock(Record);
                        return false;
                    }
                    _ = MessageBox.Show($"Record is gelockt\nDoor {Rec.Naam} gestart op {Rec.Starttijd}\nIk open detail in ViewOnly mode.\n\nKan vrijgegeven door {Rec.Naam}, of automatisch na {Rec.Starttijd.AddMinutes(30)}");
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
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

        public static T FromXML<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }

        public void Opruimen()
        {
            // Opruimen van locks die ouder zijn dan 30 minuten
            if (LaadLockFile())
            {
                if (LockList.Count == 0)
                {
                    return;
                }

                bool changed = false;
                // als lock > 30 minuten, gooi ik hem los.
                DateTime test = DateTime.Now;
                test = test.AddMinutes(-30);

                foreach (Loc Rec in LockList.ToList())
                {
                    // als lock > 30 minuten, gooi ik hem los.
                    if (Rec.Starttijd < test)
                    {
                        _ = LockList.Remove(Rec);
                        changed = true;
                    }
                }
                if(changed)
                {
                    SaveLockFile();
                }
            }
        }
    }
}