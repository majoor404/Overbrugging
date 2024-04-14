using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace Overbrugging
{
    public partial class Bijlage : Form
    {
        public string ID;
        public string bijlagepath;
        public Bijlage()
        {
            InitializeComponent();
        }

        private void Bijlage_Shown(object sender, EventArgs e)
        {
            ListBox.Items.Clear();
            string file = $"{bijlagepath}{ID}.ini";
            if(File.Exists(file))
            {
                List<string> lijst = new List<string>();
                lijst = File.ReadAllLines(file).ToList();
                for(int i = 0; i < lijst.Count; i+=2)
                {
                    ListBox.Items.Add(lijst[i]);
                }
            }
        }

        public bool BijlageAanwezig(string ID)
        {
            string file = $"{bijlagepath}{ID}.ini";
            if (File.Exists(file))
                return true;
            return false;
        }

        private void ButVoegToe_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Open File";
             open.Filter = "Files (*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                if (!Directory.Exists(bijlagepath))
                    _ = Directory.CreateDirectory(bijlagepath);

                // er kunnen dus file opgeslagen worden met zelfde naam
                // dus verander filenaam in een GUID tijdens copy naar bijlage
                // zet in inifile de orginele naam met daaronder de GUID van file

                string ext = System.IO.Path.GetExtension(open.FileName);
                Guid NieuweNaam = Guid.NewGuid();
                string NaamEnPath = $"{bijlagepath}{NieuweNaam}";
                File.Copy(open.FileName, NaamEnPath);

                ListBox.Items.Clear();
                string file = $"{bijlagepath}{ID}.ini";

                List<string> lijst = new List<string>();

                if (File.Exists(file))
                {
                    lijst = File.ReadAllLines(file).ToList();
                }

                lijst.Add(System.IO.Path.GetFileName(open.FileName));
                lijst.Add(NaamEnPath);

                // save
                SaveFile(file, lijst);

                Bijlage_Shown(this, null);

                MainForm.Main.Log.LogRegel($"Bijlage toegevoegd RegNr {ID} door {MainForm.Main.LabelUser.Text}");
            }
        }

        private static void SaveFile(string file, List<string> lijst)
        {
            try
            {
                File.WriteAllLines(file, lijst);
            }

            catch (IOException)
            {
                MessageBox.Show("info file save Error()");
            }
        }

        private void ButVerwijder_Click(object sender, EventArgs e)
        {
            if(ListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecteer welk Item");
                return;
            }
            string Guid = GetGuid(ListBox.SelectedItem as string);
            if (Guid != null)
            {
                // delete guid file
                File.Delete(Guid);
                // delete in index file
                DeleteInIndexFile(ListBox.SelectedItem as string);

                MainForm.Main.Log.LogRegel($"Bijlage verwijderd RegNr {ID} door {MainForm.Main.LabelUser.Text}");
            }
        }

        private string GetGuid(string naam)
        {
            string file = $"{bijlagepath}{ID}.ini";
            if (File.Exists(file))
            {
                List<string> lijst = new List<string>();
                lijst = File.ReadAllLines(file).ToList();
                for (int i = 0; i < lijst.Count; i += 2)
                {
                    if (lijst[i] == naam)
                        return lijst[i+1];
                }
            }
            return string.Empty;
        }

        private void DeleteInIndexFile(string naam)
        {
            string file = $"{bijlagepath}{ID}.ini";
            if (File.Exists(file))
            {
                List<string> lijst = new List<string>();
                lijst = File.ReadAllLines(file).ToList();
                for (int i = 0; i < lijst.Count; i += 2)
                {
                    if (lijst[i] == naam)
                    {
                        lijst.RemoveAt(i+1);
                        lijst.RemoveAt(i);
                    }
                    SaveFile(file, lijst);
                    if (lijst.Count == 0)
                    {
                        File.Delete(file);
                    }
                }
            }
            Bijlage_Shown(this, null);
        }

        private void ListBox_DoubleClick(object sender, EventArgs e)
        {
            if (ListBox.SelectedItems.Count == 0)
                return;

            string Guid = GetGuid(ListBox.SelectedItem as string);
            if (Guid != null)
            {
                string newFile = System.IO.Path.GetTempPath() + ListBox.SelectedItem as string;
                // copy guid naar temp en rename met oude naam
                try
                {
                    File.Copy(Guid, newFile , true);

                    Start(newFile);
                }catch (Exception ex) { }
            }
        }

        private static void Start(string fileEnPath)
        {
            string path = string.Empty;
            string file = string.Empty;

            try
            {
                if (Directory.Exists(fileEnPath))
                {
                    path = fileEnPath;
                    file = fileEnPath;
                }

                if (File.Exists(fileEnPath))
                {
                    path = System.IO.Path.GetDirectoryName(fileEnPath);
                    file = System.IO.Path.GetFileName(fileEnPath);
                }

                if (path == string.Empty)
                {
                    _ = MessageBox.Show($"Link {fileEnPath} bestaat niet");
                    return;
                }

                ProcessStartInfo _processStartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = path,
                    FileName = file
                };

                try
                {
                    Process myProcess = Process.Start(_processStartInfo);
                }
                catch { }
            }
            catch (IOException)
            {
            }
        }
    }
}