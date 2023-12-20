using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class Detail : Form
    {
        //public Data HuidigeDataInDetail = new Data();
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
                MainForm.Main.TempData.Soort = "TIW";
            }
            if (ret == DialogResult.Yes)
            {
                //OVERB
                MainForm.Main.TempData.Soort = "OVERB";
            }
            if (ret == DialogResult.Abort)
            {
                //MOC
                MainForm.Main.TempData.Soort = "MOC";
            }
            RefreshForm();
        }

        private void Detail_Shown(object sender, EventArgs e)
        {
            Point loc = this.Location;
            loc.X += 50;
            this.Location = loc;
            TextBoxRegNr.Text = MainForm.Main.TempData.RegNr.ToString();
            RefreshForm();      // type appart, ook gebruik bij knopje drukken.
            TextBoxSapNr.Focus();
        }

        private void RefreshForm()
        {
            if (MainForm.Main.TempData.Soort == "TIW")
            {
                LabelType.Text = "Tijdelijke Instalatie Wijzeging";
                labelMOC.Visible = false;
                TextBoxMocNr.Visible = false;
            }
            if (MainForm.Main.TempData.Soort == "MOC")
            {
                LabelType.Text = "Management Of Change";
                labelMOC.Visible = true;
                TextBoxMocNr.Visible = true;
            }
            if (MainForm.Main.TempData.Soort == "OVERB")
            {
                LabelType.Text = "Overbruging";
                labelMOC.Visible = false;
                TextBoxMocNr.Visible = false;
            }
            ButtonType.Text = MainForm.Main.TempData.Soort;
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

        private void ComboBoxSectie_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainForm.Main.VulSectiesOnderdeelDropDown(this);
        }

        private void ButtonIVWVDatumNu_Click(object sender, EventArgs e)
        {
            DateTime nu  = DateTime.Now;
            DatumWv.TB.Text = nu.ToShortDateString();
            // verloop op + 1 week en dan op woensdag
            nu = nu.AddDays(7);
            while(nu.DayOfWeek !=  DayOfWeek.Wednesday)
                nu = nu.AddDays(1);
            DatumVerloopTIW.TB.Text = nu.ToShortDateString();
        }

        private void ButtonIVWVDatumVerw_Click(object sender, EventArgs e)
        {
            DatumVerw.TB.Text = DateTime.Now.ToShortDateString();
        }

        private void Detail_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void ButVoerUit_Click(object sender, EventArgs e)
        {
            MainForm.Main.LaadData_lijst();
            // save data
            MainForm.Main.TempData.DatumInv = DatumInv.Datum;
            MainForm.Main.TempData.SapNr = TextBoxSapNr.Text;
            MainForm.Main.TempData.MocNr = TextBoxMocNr.Text;
            MainForm.Main.TempData.Sectie = ComboBoxSectie.Text;
            MainForm.Main.TempData.Installatie = ComboSectieDeel.Text;
            MainForm.Main.TempData.InstallatieDeel = TextBoxInstDeel.Text;
            MainForm.Main.TempData.Naam1 = ComboBoxNaam1.Text;
            MainForm.Main.TempData.Naam2 = ComboBoxNaam2.Text;
            MainForm.Main.TempData.Reden = TextBoxRede.Text;
            MainForm.Main.TempData.Uitvoering = TextBoxOplossing.Text;
            MainForm.Main.TempData.RegNr = int.Parse(TextBoxRegNr.Text);
            // middelste panel
            MainForm.Main.TempData.DatumWv = DatumWv.Datum;
            MainForm.Main.TempData.NaamWV = ComboBoxIVWV.Text;
            MainForm.Main.TempData.UitersteDatum = DatumVerloopTIW.Datum;
            MainForm.Main.TempData.BijzonderhedenWV = TextBoxBijzIVWV.Text;
            MainForm.Main.TempData.Soort = ButtonType.Text;
            // onderste panel
            MainForm.Main.TempData.DatumVerw = DatumVerw.Datum;
            MainForm.Main.TempData.Naamverw = ComboBoxNaamVerw.Text;
            MainForm.Main.TempData.BijzonderhedenVerw = TextBoxBijzVerw.Text;
            // als record al bestaat, delete
            try
            {
                Data temp = MainForm.Main.LijstData.First(a => a.RegNr == MainForm.Main.TempData.RegNr);
                MainForm.Main.LijstData.Remove(temp);
            }
            catch { }
            // en toevoegen nieuwe
            MainForm.Main.LijstData.Add(MainForm.Main.TempData);
            MainForm.Main.SaveData_lijst();

            Close();
        }
    }
}
