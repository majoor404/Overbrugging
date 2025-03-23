using System;
using System.IO;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class KeuzeType : Form
    {
        public string UitgekozenGeenMocRegel { get; private set; }
        public KeuzeType()
        {
            InitializeComponent();
        }

        private void KeuzeType_Shown(object sender, System.EventArgs e)
        {
            LaadJuisteMocFile();
        }

        private void LaadJuisteMocFile()
        {
            if (checkBoxTT.Checked)
            {
                LoadMocRegels("MocTT.txt");
            }
            else
            {
                LoadMocRegels("MocAll.txt");
            }
        }

        private void LoadMocRegels(String MocFile)
        {
            try
            {
                string filePath = $"data\\{MocFile}";
                if (File.Exists(filePath))
                {
                    string fileContent = File.ReadAllText(filePath);
                    textBoxMOC.Text = fileContent;
                }
                else
                {
                    MessageBox.Show($"The file data\\{MocFile} does not exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while reading the file: {ex.Message}");
            }
        }

        private void textBoxMOC_Click(object sender, EventArgs e)
        {
            int index = textBoxMOC.GetCharIndexFromPosition(textBoxMOC.PointToClient(Cursor.Position));
            int line = textBoxMOC.GetLineFromCharIndex(index);
            int start = textBoxMOC.GetFirstCharIndexFromLine(line);
            int length = textBoxMOC.Lines[line].Length;

            textBoxMOC.Select(start, length);

            UitgekozenGeenMocRegel = textBoxMOC.Lines[line];
        }

        private void checkBoxTT_CheckedChanged(object sender, EventArgs e)
        {
            LaadJuisteMocFile();
        }
    }
}
