﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Overbrugging
{
    public partial class Detail : Form
    {
        //public Data HuidigeDataInDetail = new Data();
        public Detail()
        {
            InitializeComponent();
        }

        private void Detail_Shown(object sender, EventArgs e)
        {
            Point loc = this.Location;
            loc.X += 50;
            this.Location = loc;

            if (MainForm.Main.TempData.RegNr == 0)
            {
                TextBoxRegNr.Text = "";
            }
            else
            {
                TextBoxRegNr.Text = MainForm.Main.TempData.RegNr.ToString();
            }
            RefreshForm();      // type appart, ook gebruik bij knopje drukken.
            TextBoxSapNr.Focus();

            // als reg nummer bekend is, maak save WV en verwijder actief.
            ButSaveWV.Enabled = !(string.IsNullOrEmpty(TextBoxRegNr.Text)) && (MainForm.Main.IsIVer.Checked);
            ButSaveVerw.Enabled = !(string.IsNullOrEmpty(TextBoxRegNr.Text)) && (MainForm.Main.IsIVer.Checked);

            // als geen iv/wv dan niks invullen bij wv of afsluiten.
            PanelWV.Enabled = MainForm.Main.IsIVer.Checked;
            PanelVerwijderen.Enabled = MainForm.Main.IsIVer.Checked;

            TextBoxRede.TextChanged += InvoerVeranderenTerwijlGoedGekeurd;
            TextBoxOplossing.TextChanged += InvoerVeranderenTerwijlGoedGekeurd;

            CBSoort.SelectedIndexChanged += CBSoort_SelectedIndexChanged;
        }

        private void RefreshForm()
        {
            if (MainForm.Main.TempData.Soort == "TIW")
            {
                LabelType.Text = "Tijdelijke Instalatie Wijziging";
            }
            if (MainForm.Main.TempData.Soort == "MOC")
            {
                LabelType.Text = "Management Of Change";
            }
            if (MainForm.Main.TempData.Soort == "OVERB")
            {
                LabelType.Text = "Overbruging";
            }
            CBSoort.Text = MainForm.Main.TempData.Soort;
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
            if (DatumWv.TB.Text == " --/--/----")
                ButtonIVWVDatumNu_Click(this, null);
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
            DateTime nu = DateTime.Now;
            DatumWv.TB.Text = nu.ToString("dd-MM-yyyy");
            // verloop op + 1 week en dan op woensdag
            nu = nu.AddDays(7);
            while (nu.DayOfWeek != DayOfWeek.Wednesday)
                nu = nu.AddDays(1);
            DatumVerloopTIW.TB.Text = nu.ToString("dd-MM-yyyy");

            ComboBoxIVWV.Text = MainForm.Main.LabelUser.Text;
        }

        private void ButtonIVWVDatumVerw_Click(object sender, EventArgs e)
        {
            DatumVerw.TB.Text = DateTime.Now.ToShortDateString();
            ComboBoxNaamVerw.Text = MainForm.Main.LabelUser.Text;
        }

        private void ButVoerUit_Click(object sender, EventArgs e)
        {
            // als wel datum iv maar geen naam
            if ((DatumWv.TB.Text != " --/--/----") && (ComboBoxIVWV.Text == ""))
            {
                MessageBox.Show("Datum en Naam IV/WV niet ingevuld");
                return;
            }

            if ((DatumVerw.TB.Text != " --/--/----") && (ComboBoxNaamVerw.Text == ""))
            {
                MessageBox.Show("Datum en Naam IV/WV niet ingevuld");
                return;
            }

            if (ComboBoxNaam1.Text == "" && ComboBoxNaam2.Text == "")
            {
                MessageBox.Show("Geen namen ingevuld Aangemaakt");
                return;
            }

            if (ComboBoxSectie.Text == "")
            {
                MessageBox.Show("Geen Sectie gekozen");
                return;
            }

            if (ComboSectieDeel.Text == "")
            {
                MessageBox.Show("Geen Instalatie gekozen");
                return;
            }

            if (CBSoort.Text == "")
            {
                KeuzeType KS = new KeuzeType();
                DialogResult ret = KS.ShowDialog();
                if (ret == DialogResult.OK)
                {
                    // TIW
                    CBSoort.Text = "TIW";
                    MainForm.Main.TempData.Soort = "TIW";
                }
                if (ret == DialogResult.Yes)
                {
                    //OVERB
                    CBSoort.Text = "OVERB";
                    MainForm.Main.TempData.Soort = "OVERB";
                }
                if (ret == DialogResult.Abort)
                {
                    //MOC
                    CBSoort.Text = "MOC";
                    MainForm.Main.TempData.Soort = "MOC";
                }
            }

            if(CBSoort.Text == "TIW" || CBSoort.Text == "OVERB")
            {
                if(TextBoxMocNr.Text != "")
                {
                    MessageBox.Show("Er is een MOC nummer ingevuld, maak er dus een MOC van.");
                    CBSoort.Text = "MOC";
                    MainForm.Main.TempData.Soort = "MOC";
                }
            }

            int index;
            //bool save_nieuwe_index = false;
            if (string.IsNullOrEmpty(TextBoxRegNr.Text))
            {
                index = MainForm.Main.LastIndex;
                index++;
                //save_nieuwe_index = true;
            }
            else
            {
                index = int.Parse(TextBoxRegNr.Text);
            }

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
            MainForm.Main.TempData.RegNr = index;
            // middelste panel
            MainForm.Main.TempData.DatumWv = DatumWv.Datum;
            MainForm.Main.TempData.NaamWV = ComboBoxIVWV.Text;
            MainForm.Main.TempData.UitersteDatum = DatumVerloopTIW.Datum;
            MainForm.Main.TempData.BijzonderhedenWV = TextBoxBijzIVWV.Text;
            MainForm.Main.TempData.Soort = CBSoort.Text;
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

        private void ComboBoxNaamVerw_TextChanged(object sender, EventArgs e)
        {
            if (DatumVerw.TB.Text == " --/--/----")
                DatumVerw.TB.Text = DateTime.Now.ToShortDateString();
        }

        private void InvoerVeranderenTerwijlGoedGekeurd(object sender, EventArgs e)
        {
            // als data veranderd, maar is al goedgekeurd en niet afgesloten,
            // verwijder de VW data
            if (ComboBoxIVWV.Text != "" && TextBoxRegNr.Text != "" && ComboBoxNaamVerw.Text == "")
            {
                ComboBoxIVWV.Items.Add("");
                ComboBoxIVWV.Text = "";
                DatumWv.TB.Text = " --/--/----";
                DatumVerloopTIW.TB.Text = " --/--/----";
            }
        }

        private void ButPrint_Click(object sender, EventArgs e)
        {
            string tempfile = System.IO.Path.GetTempPath() + "overb.jpg";
            if (CaptureForm(tempfile))
            {
                if (File.Exists(tempfile))
                {
                    _ = Process.Start(tempfile);
                }
            }
        }

        private bool CaptureForm(string file)
        {
            try
            {
                Rectangle bounds = Bounds;
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                    }
                    bitmap.Save(file, ImageFormat.Jpeg);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void CBSoort_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainForm.Main.TempData.Soort = CBSoort.Text;
            RefreshForm();
        }

        private void ButPrintUitvoering_Click(object sender, EventArgs e)
        {
            UitvoeringPrintForm UVP = new UitvoeringPrintForm();
            UVP.Uitvoering.Text = TextBoxOplossing.Text;
            UVP.Uitvoering.SelectionStart = 0;
            Point LO = this.Location;
            LO.X += 100;
            LO.Y += 100;
            UVP.Location = LO;
            UVP.Show();
            Wait(500);
            string tempfile = System.IO.Path.GetTempPath() + "overb.jpg";
            if (CaptureForm(tempfile))
            {
                if (File.Exists(tempfile))
                {
                    _ = Process.Start(tempfile);
                }
            }
            UVP.Close();
        }

        public void Wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }

        }
    }
}
