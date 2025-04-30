using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class Detail : Form
    {
        public bool LockMode = false;
        public Detail()
        {
            InitializeComponent();
        }

        private void Detail_Shown(object sender, EventArgs e)
        {
            Point loc = Location;
            loc.X += 50;
            Location = loc;

            TextBoxRegNr.Text = MainForm.Main.TempData.RegNr == 0 ? "" : MainForm.Main.TempData.RegNr.ToString();
            RefreshForm();      // type appart, ook gebruik bij knopje drukken.
            _ = TextBoxSapNr.Focus();

            // afhankelijk van rechten zet eea actief            
            // als reg nummer bekend is, maak save WV en verwijder actief.
            ButSaveWV.Enabled = !string.IsNullOrEmpty(TextBoxRegNr.Text) && MainForm.Main.rechten == 2;
            ButSaveVerw.Enabled = !string.IsNullOrEmpty(TextBoxRegNr.Text) && MainForm.Main.rechten == 2;
            ButtonHeropen.Visible = ButSaveVerw.Enabled && (DatumVerw.TB.Text != " --/--/----");

            // als geen iv/wv dan niks invullen bij wv.
            PanelWV.Enabled = MainForm.Main.rechten == 2;
            
            //als persoon in lijst, mag die wel afsluiten
            PanelVerwijderen.Enabled = MainForm.Main.rechten > 0;

            // LockMode
            ButVoerUit.Enabled = !LockMode;
            ButSaveWV.Enabled = !LockMode;
            ButSaveVerw.Enabled = !LockMode;
            ButtonHeropen.Enabled = !LockMode;
            BijlageToevoegen.Enabled = !LockMode;

            // rechten 0
            if (MainForm.Main.rechten == 0)
            {
                ButVoerUit.Enabled = false;
                ButSaveWV.Enabled = false;
                ButSaveVerw.Enabled = false;
                ButtonHeropen.Enabled = false;
                BijlageToevoegen.Enabled = false;
            }
           
            TextBoxRede.TextChanged += InvoerVeranderenTerwijlGoedGekeurd;
            TextBoxOplossing.TextChanged += InvoerVeranderenTerwijlGoedGekeurd;

            Bijlage.Visible = MainForm.bijlage.BijlageAanwezig(TextBoxRegNr.Text);

            BTActueelEnabled();
        }

        private void RefreshForm()
        {
            RedeMOCLabel.Visible = true;
            HelpTextRedeGeenMOC.Visible = true;
            if (MainForm.Main.TempData.Soort == "TIW")
            {
                LabelType.Text = "Tijdelijke Instalatie Wijziging";
                RedeMOCLabel.Text = MainForm.Main.TempData.Reserve1;
            }
            if (MainForm.Main.TempData.Soort == "MOC")
            {
                LabelType.Text = "Management Of Change";
                RedeMOCLabel.Visible = false;
                HelpTextRedeGeenMOC.Visible = false;
            }
            if (MainForm.Main.TempData.Soort == "OVERB")
            {
                LabelType.Text = "Overbruging";
                RedeMOCLabel.Text = MainForm.Main.TempData.Reserve1;
            }
            BTSoort.Text = MainForm.Main.TempData.Soort;
            BTActueelEnabled();
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
            {
                ButtonIVWVDatumNu_Click(this, null);
            }
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
            BTActueelEnabled();
        }

        private void ButtonIVWVDatumNu_Click(object sender, EventArgs e)
        {
            DateTime nu = DateTime.Now;
            DatumWv.TB.Text = nu.ToString("dd-MM-yyyy");
            // verloop op + 1 week en dan op woensdag
            nu = nu.AddDays(7);
            while (nu.DayOfWeek != DayOfWeek.Wednesday)
            {
                nu = nu.AddDays(1);
            }

            DatumVerloopTIW.TB.Text = nu.ToString("dd-MM-yyyy");

            ComboBoxIVWV.Text = MainForm.Main.LabelUser.Text;
        }

        private void ButtonIVWVDatumVerw_Click(object sender, EventArgs e)
        {
            DatumVerw.TB.Text = MainForm.Main.MajoorString(DateTime.Now);
            ComboBoxNaamVerw.Text = MainForm.Main.LabelUser.Text;
        }

        private void ButVoerUit_Click(object sender, EventArgs e)
        {
            // als wel datum iv maar geen naam
            if ((DatumWv.TB.Text != " --/--/----") && (ComboBoxIVWV.Text == ""))
            {
                _ = MessageBox.Show("Datum en Naam IV/WV niet ingevuld");
                return;
            }

            if ((DatumVerw.TB.Text != " --/--/----") && (ComboBoxNaamVerw.Text == ""))
            {
                _ = MessageBox.Show("Datum en Naam IV/WV niet ingevuld");
                return;
            }

            if (ComboBoxNaam1.Text == "" && ComboBoxNaam2.Text == "")
            {
                _ = MessageBox.Show("Geen namen ingevuld Aangemaakt");
                return;
            }

            if (ComboBoxSectie.Text == "")
            {
                _ = MessageBox.Show("Geen Sectie gekozen");
                return;
            }

            if (ComboSectieDeel.Text == "")
            {
                _ = MessageBox.Show("Geen Instalatie gekozen");
                return;
            }

            if (BTSoort.Text == "")
            {
                KeuzeType KS = new KeuzeType();
                DialogResult ret = KS.ShowDialog();
                if (ret == DialogResult.OK)
                {
                    // TIW
                    BTSoort.Text = "TIW";
                    MainForm.Main.TempData.Soort = "TIW";
                }
                if (ret == DialogResult.Yes)
                {
                    //OVERB
                    BTSoort.Text = "OVERB";
                    MainForm.Main.TempData.Soort = "OVERB";
                }
                if (ret == DialogResult.Abort)
                {
                    //MOC
                    BTSoort.Text = "MOC";
                    MainForm.Main.TempData.Soort = "MOC";
                }
            }

            if (BTSoort.Text == "TIW" || BTSoort.Text == "OVERB")
            {
                if (TextBoxMocNr.Text != "")
                {
                    _ = MessageBox.Show("Er is een MOC nummer ingevuld, ik maak er dus een MOC van.");
                    BTSoort.Text = "MOC";
                    MainForm.Main.TempData.Soort = "MOC";
                }
            }

            int index;
            MainForm.Main.LaadData_lijst();
            if (string.IsNullOrEmpty(TextBoxRegNr.Text))
            {
                index = MainForm.Main.LastIndex;
                index++;
                MainForm.Main.Log.LogRegel($"Nieuw record {index} door {MainForm.Main.LabelUser.Text}");
            }
            else
            {
                index = int.Parse(TextBoxRegNr.Text);
                MainForm.Main.Log.LogRegel($"Wijzig record {index} door {MainForm.Main.LabelUser.Text}");
            }

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
            MainForm.Main.TempData.Soort = BTSoort.Text;
            // onderste panel
            MainForm.Main.TempData.DatumVerw = DatumVerw.Datum;
            MainForm.Main.TempData.Naamverw = ComboBoxNaamVerw.Text;
            MainForm.Main.TempData.BijzonderhedenVerw = TextBoxBijzVerw.Text;
            // als record al bestaat, delete
            try
            {
                Data temp = MainForm.Main.LijstData.First(a => a.RegNr == MainForm.Main.TempData.RegNr);
                _ = MainForm.Main.LijstData.Remove(temp);
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
            {
                DatumVerw.TB.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void InvoerVeranderenTerwijlGoedGekeurd(object sender, EventArgs e)
        {
            // als data veranderd, maar is al goedgekeurd en niet afgesloten,
            // verwijder de VW data
            if (ComboBoxIVWV.Text != "" && TextBoxRegNr.Text != "" && ComboBoxNaamVerw.Text == "")
            {
                _ = ComboBoxIVWV.Items.Add("");
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

        

        private void ButPrintUitvoering_Click(object sender, EventArgs e)
        {
            UitvoeringPrintForm UVP = new UitvoeringPrintForm();
            UVP.Uitvoering.Text = TextBoxOplossing.Text;
            UVP.Uitvoering.SelectionStart = 0;
            Point LO = Location;
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
            Timer timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0)
            {
                return;
            }

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

        private void ButVoerUit_Click_1(object sender, EventArgs e)
        {
            MainForm.Main.Log.LogRegel($"Invoer aangemaakt knop ingedrukt. Door {MainForm.Main.LabelUser.Text}");
            ButVoerUit_Click(this, null);
        }

        private void ButSaveWV_Click(object sender, EventArgs e)
        {
            if (DatumWv.Datum == "")
            {
                _ = MessageBox.Show("Nog niet afgetekend door IV/WV");
            }
            else
            {
                MainForm.Main.Log.LogRegel($"Save knop IV/WV ingedrukt. Door {MainForm.Main.LabelUser.Text}");
                ButVoerUit_Click(this, null);
            }
        }

        private void ButSaveVerw_Click(object sender, EventArgs e)
        {
            if (DatumWv.Datum == "")
            {
                _ = MessageBox.Show("Nog niet afgetekend door IV/WV");
                return;
            }

            if (DatumVerw.Datum == "")
            {
                _ = MessageBox.Show("Geen datum ingevuld waarneer verwijderd");
                return;
            }

            if (ComboBoxNaamVerw.Text == "")
            {
                _ = MessageBox.Show("Geen Naam ingevuld wie verwijderd");
                return;
            }

            MainForm.Main.Log.LogRegel($"Save knop Verwijder ingedrukt. Door {MainForm.Main.LabelUser.Text}");
            ButVoerUit_Click(this, null);

        }

        private void ComboBoxNaam1_TextChanged(object sender, EventArgs e)
        {
            if (ComboBoxNaam1.Text == "")
            {
                TextBoxPersnr1.Text = string.Empty;
            }
        }

        private void ComboBoxNaam2_TextChanged(object sender, EventArgs e)
        {
            if (ComboBoxNaam2.Text == "")
            {
                TextBoxPersnr2.Text = string.Empty;
            }
        }

        private void ComboBoxIVWV_TextChanged(object sender, EventArgs e)
        {
            if (ComboBoxIVWV.Text == "")
            {
                TextBoxPersNrIVWV.Text = string.Empty;
            }
        }

        private void ButtonHeropen_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Heropen deze afgesloten TIW/Overb?\nAlle velden van werkverandwoordelijke en verwijderd\nworden leeg geveegd in dit record", "Vraagje", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                ComboBoxIVWV.Text = "";
                DatumWv.TB.Text = " --/--/----";
                DatumVerloopTIW.TB.Text = " --/--/----";
                ComboBoxNaamVerw.Text = "";
                DatumVerw.TB.Text = " --/--/----";

                TextBoxBijzIVWV.Text = "";
                TextBoxBijzVerw.Text = "";

                MainForm.Main.Log.LogRegel($"Afgesloten Record {TextBoxRegNr.Text} weer geopend door {MainForm.Main.LabelUser.Text}");
                ButVoerUit_Click(this, null);
            }
        }

        private void BijlageToevoegen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxRegNr.Text))
            {
                _ = MessageBox.Show("Bijlage kan pas gemaakt worden als regnr bekend is.");
                return;
            }
            Bijlage.Visible = MainForm.Main.BijlageFormOpenenMetJuisteRegnr(TextBoxRegNr.Text);
            Wait(300);
        }

        private void Bijlage_Click(object sender, EventArgs e)
        {
            _ = MainForm.Main.BijlageFormOpenenMetJuisteRegnr(TextBoxRegNr.Text, false);
        }

        private void TextBoxBijzIVWV_KeyPress(object sender, KeyPressEventArgs e)
        {
            // als text van wv veranderd, en naam is anders dan orginele wv, deze aanpassen
            if (MainForm.Main.LabelUser.Text != ComboBoxIVWV.Text)
            {
                ComboBoxIVWV.Text = MainForm.Main.LabelUser.Text;
            }
        }

        private void BTSoort_Click(object sender, EventArgs e)
        {
            KeuzeType KS = new KeuzeType();
            DialogResult retKeuzeForm = KS.ShowDialog();

            if (retKeuzeForm == DialogResult.Cancel)
            {
                return;
            }

            if (retKeuzeForm == DialogResult.OK)
            {
                // TIW
                BTSoort.Text = "TIW";
                MainForm.Main.TempData.Soort = "TIW";
            }
            if (retKeuzeForm == DialogResult.Yes)
            {
                //OVERB
                BTSoort.Text = "OVERB";
                MainForm.Main.TempData.Soort = "OVERB";
            }
            if (retKeuzeForm == DialogResult.Abort)
            {
                //MOC
                BTSoort.Text = "MOC";
                MainForm.Main.TempData.Soort = "MOC";
            }

            RedeMOCLabel.Text = KS.UitgekozenGeenMocRegel;
            MainForm.Main.TempData.Reserve1 = RedeMOCLabel.Text;

            RefreshForm();
        }

        private void BTActuele_Click(object sender, EventArgs e)
        {
            Actuele act = new Actuele();
            if (ComboBoxSectie.Text == "")
                return;
            if(ComboSectieDeel.Text == "")
                return; 

            act.Sectie = ComboBoxSectie.Text;
            act.Installatie = ComboSectieDeel.Text;

            act.ShowDialog();
        }

        private void BTActueelEnabled()
        {
            BTActuele.Enabled = ComboBoxSectie.Text != "" && ComboSectieDeel.Text != "";
        }

        private void ComboSectieDeel_SelectedIndexChanged(object sender, EventArgs e)
        {
            BTActueelEnabled();
        }
    }
}
