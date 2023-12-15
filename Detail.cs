using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class Detail : Form
    {
        public Data QN = new Data();
        
        public Detail()
        {
            InitializeComponent();
        }

        private void ButtonType_Click(object sender, EventArgs e)
        {
            KeuzeType KS = new KeuzeType();
            DialogResult ret = KS.ShowDialog();
            if (ret == DialogResult.OK)
            {
                // TIW
                QN.Soort = "TIW";
            }
            if (ret == DialogResult.Cancel)
            {
                //OVERB
                QN.Soort = "OVERB";
            }
            if (ret == DialogResult.Abort)
            {
                //MOC
                QN.Soort = "MOC";
            }
            RefreshForm();
        }

        private void Detail_Shown(object sender, EventArgs e)
        {
            Point loc = this.Location;
            loc.X += 50;
            this.Location = loc;
            // zoek record
            int regNr = int.Parse(TextBoxRegNr.Text);
            QN = MainForm.Main.ZoekDataRecord(regNr);
            RefreshForm();
        }

        private void RefreshForm()
        {
            if (QN.Soort == "TIW")
            {
                LabelType.Text = "Tijdelijke Instalatie Wijzeging";
                labelMOC.Visible = false;
                TextBoxMocNr.Visible = false;
            }
            if (QN.Soort == "MOC")
            {
                LabelType.Text = "Management Of Change";
                labelMOC.Visible = true;
                TextBoxMocNr.Visible = true;
            }
            if (QN.Soort == "OVERB")
            {
                LabelType.Text = "Overbruging";
                labelMOC.Visible = false;
                TextBoxMocNr.Visible = false;
            }
            ButtonType.Text = QN.Soort;
        }

        private void ComboBoxNaam1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxPersnr1.Text = ZoekPersnr(ComboBoxNaam1.Text);
        }

        private void ComboBoxNaam2_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxPersnr2.Text = ZoekPersnr(ComboBoxNaam2.Text);
        }

        private void ComboBoxIVWV_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxPersNrIVWV.Text = ZoekPersnr(ComboBoxIVWV.Text);
        }

        private void ComboBoxNaamVerw_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxPersNrVerw.Text = ZoekPersnr(ComboBoxNaamVerw.Text);
        }

        public string ZoekPersnr(string zoek)
        {
            if (string.IsNullOrEmpty(zoek))
            {
                return "";
            }

            try
            {
                NamenFunties Q = MainForm.Main.NamenLijst.First(a => a.Naam == zoek);
                return Q.PersoneelNummer;
            }
            catch
            {
                return "";
            }
        }
    }
}
