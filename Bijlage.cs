using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                // er kunnen dus file opgeslagen worden met zelfde naam
                // dus verander filenaam in een GUID tijdens copy naar bijlage
                // zet in inifile de orginele naam met daaronder de GUID van file

                string ext = Path.GetExtension(open.FileName);
                Guid NieuweNaam = Guid.NewGuid();
                string NaamEnPath = $"{bijlagepath}{NieuweNaam}.{ext}";
                File.Copy(open.FileName, NaamEnPath);

                ListBox.Items.Clear();
                string file = $"{bijlagepath}{ID}.ini";

                List<string> lijst = new List<string>();

                if (File.Exists(file))
                {
                    lijst = File.ReadAllLines(file).ToList();
                }

                lijst.Add(Path.GetFileName(open.FileName));
                lijst.Add(NaamEnPath);

                // save

                try
                {
                    File.WriteAllLines(file, lijst);
                }

                catch (IOException)
                {
                    MessageBox.Show("info file save Error()");
                }
                Bijlage_Shown(this, null);
            }
        }
    }
}