using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Overbrugging
{
    public class Logging
    {
        private List<string> inhoud = new List<string>();

        public string Locatie { get; set; } = string.Empty;

        public int MaxRegels { get; set; } = 5000;

        public Logging() { }

        public void LogRegel(string text)
        {
            // open file
            OpenFile();
            // test max regels
            TestMax();
            // schrijf regel
            RegelToevoeg(text);
            // save file
            SaveFile();
        }

        private void OpenFile()
        {
            try
            {
                inhoud = File.ReadAllLines(Locatie).ToList();
            }
            catch
            {
            }
        }

        private void TestMax()
        {
            if (inhoud.Count > MaxRegels)
            {
                //verwijder 10%
                int verwijder = (int ) ((double)MaxRegels * 10) / 100;
                List<string> temp = new List<string>();
                for (int i = MaxRegels - verwijder; i < inhoud.Count; i++)
                {
                    temp.Add(inhoud[i]);
                }
                inhoud.Clear();
                inhoud = temp;
                RegelToevoeg("Log File ingekort!");
            }
        }

        private void RegelToevoeg(string text)
        {
            string toevoeg = $"{DateTime.Now,-19:dd/MM/yyyy HH:mm:ss} - {Environment.UserName,-20} - {Environment.MachineName,-14} - {text}";
            inhoud.Add(toevoeg);
        }

        private void SaveFile()
        {
            try
            {
                File.WriteAllLines(Locatie, inhoud);
            }
            catch (IOException)
            {
                _ = MessageBox.Show("info file save Error()");
            }
        }
    }
}
