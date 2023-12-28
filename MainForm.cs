﻿using Melding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class MainForm : Form
    {
        public List<OudeOverbrugging> OudeLijst = new List<OudeOverbrugging>();
        public List<NamenFunties> NamenLijst { get; set; } = new List<NamenFunties>();
        public List<Secties> SectieLijst = new List<Secties>();
        public List<InstallatieOnderdeel> InstallatieLijst = new List<InstallatieOnderdeel>();
        public List<OverBrugRecord> LijstOverbrugingen = new List<OverBrugRecord> { };
        public List<Data> LijstData = new List<Data>();
        public Data TempData = new Data();
        public int LastIndex = 0;
        public static string datapath = AppDomain.CurrentDomain.BaseDirectory + "Data\\";
        public List<string> instellingen = new List<string>();
        public DateTime verloopdatum = DateTime.Now.AddDays(1);

        public int NietAfgetekendWv = 0;
        public int VerlopenData = 0;

        DataGridViewColumn oldColumn = null; // voor sorteer
        SortOrder SortRichting = SortOrder.None;
        
        public int[,] teldata = new int[7, 8]; // voor wachtrapport 7 rijen (soort) van 8 kolomen (secties)

        enum SectieNaam
        {
            SecRst,SecCon,SecPbi,SecPvk,SecCgm,SecSkv,SecAov,SecAlg
        }
        enum Rij
        {
            RowOverb,RowMetWerkv,RowTiw,RowMoc,RowOverbVerl,RowTiwVerl,RowMocVerl
        }

        public static MainForm Main;

        public MainForm()
        {
            InitializeComponent();
            Main = this;
            LaadInstelingen();
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

        private void ButImport_Click(object sender, EventArgs e)
        {
            _ = MessageBox.Show("Inlezen Overb1.mdb.csv");
            try
            {
                string[] csvTekst = File.ReadAllLines($"Overb1.mdb.csv");
                string[] items;
                OudeLijst.Clear();


                for (int i = 0; csvTekst.Count() > i; i++)
                {
                    OudeOverbrugging ov = new OudeOverbrugging();
                    items = csvTekst[i].Split(';');
                    int count = 0;
                    ov.RegNr = items[count++];
                    ov.DatumInv = items[count++];
                    ov.Sectie = items[count++];
                    ov.Installatie = items[count++];
                    ov.InstallatieDeel = items[count++];
                    ov.NaamKKD1uit = items[count++];
                    ov.NaamKKD2uit = items[count++];
                    ov.Ploeg = items[count++];
                    ov.Reden = items[count++];
                    ov.Uitvoering = items[count++];
                    ov.EnigeOverb = items[count++];
                    ov.WerkVerg = items[count++];
                    ov.WerkVergNr = items[count++];
                    ov.WerkVergOk = items[count++];
                    ov.SrsNr = items[count++];
                    ov.DatumWv = items[count++];
                    ov.NaamWV = items[count++];
                    ov.DatumVerw = items[count++];
                    ov.NaamKKDverw = items[count++];
                    ov.UitersteDatum = items[count++];
                    ov.Veld1 = items[count++];
                    ov.PrintDatum = items[count++];
                    ov.BijzonderhedenWV = items[count++];
                    ov.BijzonderhedenVerw = items[count++];
                    ov.OvernemenIWV = items[count++];
                    ov.DatumVerloopWV = items[count++];
                    ov.TIW = items[count++];
                    ov.TIWOB = items[count++];
                    ov.InGebruik = items[count++];
                    ov.PersWijzig = items[count++];
                    ov.BewerkTijd = items[count++];
                    ov.Soort = items[count++];
                    ov.MocRsNr = items[count++];
                    ov.Reserve = items[count++];

                    OudeLijst.Add(ov);
                }

                //_ = MessageBox.Show("Inlezen Namen.mdb.csv");

                //string[] namenTekst = File.ReadAllLines($"Namen.mdb.csv");
                //NamenLijst.Clear();


                //for (int i = 0; namenTekst.Count() > i; i++)
                //{
                //    NamenFunties nf = new NamenFunties();
                //    items = namenTekst[i].Split(';');
                //    int count = 0;
                //    nf.Index = items[count++];
                //    nf.PersoneelNummer = items[count++];
                //    nf.Naam = items[count++];
                //    nf.Team = items[count++];
                //    count++;    // funtie niet meer gebruiken
                //    nf.IVWV = items[count++] == "True";

                //    NamenLijst.Add(nf);
                //}

                //_ = MessageBox.Show("Save namen lijst");
                //SaveDataNamen_lijst();

                //_ = MessageBox.Show("Inlezen SectieTabel.mdb.csv");

                //string[] SectieTekst = File.ReadAllLines($"SectieTabel.mdb.csv");
                //SectieLijst.Clear();

                //for (int i = 0; SectieTekst.Count() > i; i++)
                //{
                //    Secties sc = new Secties();
                //    items = SectieTekst[i].Split(';');
                //    int count = 0;
                //    sc.Index = items[count++];
                //    sc.Naam = items[count++];
                //    SectieLijst.Add(sc);
                //}

                //_ = MessageBox.Show("Save secties lijst");
                //SaveDataSecties_lijst();

                //_ = MessageBox.Show("Inlezen InstallatieTabel.mdb.csv");

                //string[] InstallatieTekst = File.ReadAllLines($"InstallatieTabel.mdb.csv");
                //InstallatieLijst.Clear();

                //for (int i = 0; InstallatieTekst.Count() > i; i++)
                //{
                //    InstallatieOnderdeel iso = new InstallatieOnderdeel();
                //    items = InstallatieTekst[i].Split(';');
                //    int count = 0;
                //    iso.Index = items[count++];
                //    iso.Instal = items[count++];
                //    iso.Sectie = ZoekSectie(items[count++]);
                //    InstallatieLijst.Add(iso);
                //}

                //_ = MessageBox.Show("Save Installatie lijst");
                //SaveDataInstallaties_lijst();


                _ = MessageBox.Show("Dan nu samen voegen tot nieuwe opslag class.");

                LijstData.Clear();
                
                foreach (OudeOverbrugging o in OudeLijst)
                {
                    Data a = new Data
                    {
                        RegNr = int.Parse(o.RegNr),
                        DatumInv = VerwijderTijd(o.DatumInv),
                        SapNr = o.SrsNr,
                        MocNr = o.MocRsNr
                    };
                    
                    labelAantal.Text = a.RegNr.ToString();
                    labelAantal.Refresh();

                    a.Sectie = ZoekSectie(o.Sectie);
                    a.Installatie = ZoekInstallatie(o.Installatie);
                    a.InstallatieDeel = o.InstallatieDeel;

                    a.Naam1 = ZoekNaam(o.NaamKKD1uit);
                    a.Naam2 = ZoekNaam(o.NaamKKD2uit);
                    a.Ploeg = o.Ploeg;

                    a.Reden = o.Reden;
                    a.Uitvoering = o.Uitvoering;

                    //// ivwv
                    a.WerkVerg = o.WerkVerg;
                    a.WerkVergNr = o.WerkVergNr;

                    a.DatumWv = VerwijderTijd(o.DatumWv);
                    a.NaamWV = ZoekNaam(o.NaamWV);

                    a.UitersteDatum = VerwijderTijd(o.UitersteDatum);
                    a.DatumVerloopWV = VerwijderTijd(o.DatumVerloopWV);

                    a.Soort = "OVERB"; // default
                    if (o.TIWOB == "Tijdelijke Installatie Wijziging")
                    {
                        a.Soort = "TIW";
                    }

                    if (o.TIWOB == "Management Of Change")
                    {
                        a.Soort = "MOC";
                    }

                    a.BijzonderhedenWV = o.BijzonderhedenWV;

                    ////verwijderen
                    a.Naamverw = ZoekNaam(o.NaamKKDverw);
                    a.DatumVerw = VerwijderTijd(o.DatumVerw);
                    a.BijzonderhedenVerw = o.BijzonderhedenVerw;

                    a.Reserve1 = "";
                    a.Reserve2 = "";
                    a.Reserve3 = "";
                    a.Reserve4 = "";
                    a.Reserve5 = "";
                    a.Reserve6 = "";
                    a.Kleur = false;
                    a.DatumTemp = DateTime.Now;

                    LijstData.Add(a);
                }
                _ = MessageBox.Show("Save overbrug lijst");
                SaveData_lijst();
            }

            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);

            }
        }

        private string VerwijderTijd(string Datum) // is van format "19-11-2023 00:00:00" of "9-4-2001 00:00:00"
        {
            if (string.IsNullOrEmpty(Datum))
            {
                return "";
            }
            // verwijder tijd
            int pos = Datum.IndexOf(" ");

            if (pos > 0)
            {
                Datum = Datum.Substring(0, pos);

                string[] temp = Datum.Split('-');

                int Dag = int.Parse(temp[0]);
                int Maand = int.Parse(temp[1]);
                int Jaar = int.Parse(temp[2]);

                DateTime ret = new DateTime(Jaar, Maand, Dag);

                var dat = ret.ToString("dd-MM-yyyy");

                return dat;
            }
            return "";
        }

        private string ZoekSectie(string zoek)
        {
            if (string.IsNullOrEmpty(zoek))
            {
                return "";
            }

            try
            {
                Secties Q = SectieLijst.First(a => a.Index == zoek);
                return Q.Naam;
            }
            catch
            {
                return "";
            }
        }

        private bool ZoekIV(string zoek)
        {
            if (string.IsNullOrEmpty(zoek))
            {
                return false;
            }

            try
            {
                NamenFunties Q = NamenLijst.First(a => a.PersoneelNummer == zoek);
                return Q.IVWV;
            }
            catch
            {
                return false;
            }
        }
        public Data ZoekDataRecord(int nr)
        {
            Data Q = new Data();
            try
            {
                Q = LijstData.First(a => a.RegNr == nr);
                return Q;
            }
            catch
            {
                return Q;
            }
        }

        private string ZoekNaam(string zoek)
        {
            if (string.IsNullOrEmpty(zoek))
            {
                return "";
            }

            try
            {
                NamenFunties Q = NamenLijst.First(a => a.Index == zoek);
                return Q.Naam;
            }
            catch
            {
                return "";
            }
        }

        private string ZoekInstallatie(string zoek)
        {
            if (string.IsNullOrEmpty(zoek))
            {
                return "";
            }

            try
            {
                InstallatieOnderdeel Q = InstallatieLijst.First(a => a.Index == zoek);
                return Q.Instal;
            }
            catch
            {
                return "";
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            FormMelding md = new FormMelding(FormMelding.Type.Info, "Overbruging 2.0", "R.Majoor");
            md.Show();

            // laad namen
            LaadNamen_lijst();
            // laad secties
            LaadSecties_lijst();
            // laad installaties
            LaadInstallaties_lijst();
            // zet filter dropdown
            if (comboBoxSectie.Items.Count == 0)
            {
                comboBoxSectie.Items.Clear();
                _ = comboBoxSectie.Items.Add("ALLE");
                foreach (Secties item in SectieLijst)
                {
                    if (!string.IsNullOrEmpty(item.Naam))
                        _ = comboBoxSectie.Items.Add(item.Naam);
                }
                comboBoxSectie.SelectedIndex = 0;
            }

            comboBoxSoortFilter.SelectedIndex = 0;
            comboBoxStatus.SelectedIndex = 0;

            dataGridView1.Columns.Clear();

            dataGridView1.AutoGenerateColumns = false;

            _ = dataGridView1.Columns.Add("Nr", "Nr");
            dataGridView1.Columns[0].Width = 60;
            _ = dataGridView1.Columns.Add("Datum", "Datum inv.");
            dataGridView1.Columns[1].Width = 100;
            _ = dataGridView1.Columns.Add("Soort", "Soort");
            dataGridView1.Columns[2].Width = 70;
            _ = dataGridView1.Columns.Add("Sectie", "Sectie");
            dataGridView1.Columns[3].Width = 70;

            _ = dataGridView1.Columns.Add("Installatie", "Installatie");
            dataGridView1.Columns[4].Width = 110;
            _ = dataGridView1.Columns.Add("InstalatieDeel", "Instalatie Deel");
            dataGridView1.Columns[5].Width = 175;
            _ = dataGridView1.Columns.Add("Rede", "Rede");
            dataGridView1.Columns[6].Width = 340;

            _ = dataGridView1.Columns.Add("DatumVerl", "Verloopt");
            dataGridView1.Columns[7].Width = 100;

            _ = dataGridView1.Columns.Add("DatumTemp", "DatumTemp");
            dataGridView1.Columns[8].Width = 100;
            _ = dataGridView1.Columns.Add("Kleur", "Kleur");
            dataGridView1.Columns[8].Width = 100;

            dataGridView1.RowHeadersVisible = false;

            ButRefresh_Click(this, null);

            // zet update roetine op filters actief
            comboBoxSectie.SelectedIndexChanged += ButRefresh_Click;
            comboBoxSoortFilter.SelectedIndexChanged += ButRefresh_Click;
            comboBoxStatus.SelectedIndexChanged += ButRefresh_Click;

            var inlognaam = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];

            if (inlognaam == "ronal")
                inlognaam = "a590588";
            
            // is bv dan a590588

            if (inlognaam.Length == 7)
            {
                inlognaam = inlognaam.ToLower();
                if (inlognaam[0] == 'a')
                {
                    inlognaam = inlognaam.Substring(1);
                }
            }
            
            IsIVer.Checked = ZoekIV(inlognaam);
            IsIVer.Text = inlognaam;
            
            try
            {
                NamenFunties Q = NamenLijst.First(a => a.PersoneelNummer == inlognaam);
                LabelUser.Text = Q.Naam;
            }
            catch
            {
                LabelUser.Text  = "Gebruiker niet in lijst";
                LabelUser.ForeColor = Color.Teal;
                 
            }
        }

        private void VulGrid()
        {
            if (LijstData.Count > 0)
                dataGridView1.DataSource = LijstData;

            if (dataGridView1.Columns["Nr"].DataPropertyName == "")
            {
                dataGridView1.Columns["Nr"].DataPropertyName = "Regnr";
                dataGridView1.Columns["Datum"].DataPropertyName = "DatumInv";
                dataGridView1.Columns["Soort"].DataPropertyName = "Soort";
                dataGridView1.Columns["Sectie"].DataPropertyName = "Sectie";
                dataGridView1.Columns["Installatie"].DataPropertyName = "Installatie";
                dataGridView1.Columns["InstalatieDeel"].DataPropertyName = "InstallatieDeel";
                dataGridView1.Columns["Rede"].DataPropertyName = "Reden";
                dataGridView1.Columns["DatumVerl"].DataPropertyName = "UitersteDatum";
                dataGridView1.Columns["DatumTemp"].DataPropertyName = "DatumTemp";
                dataGridView1.Columns["Kleur"].DataPropertyName = "Kleur";

            }

            dataGridView1.Columns[8].Visible = false;   // DatumTemp
            dataGridView1.Columns[9].Visible = false;   // Kleur

            labelAantal.Text = LijstData.Count().ToString();

            int index = 0;
            if (labelAantal.Text != "0")
                index = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());

            KleurAfwijkingen();

            VulPreview(index);
        }

        private void ButRefresh_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            Wait(500);
            dataGridView1.Rows.Clear();

            // laad alle overbrugingen
            LaadData_lijst();

            TelData();
            
            LabelNietAfgetekendWV.Text = NietAfgetekendWv.ToString();
            LabelDatumVerlopen.Text = VerlopenData.ToString();

            // filter op status
            LijstData = comboBoxStatus.Text == "Niet Verwijderd"
                ? LijstData.Where(x => x.DatumVerw == string.Empty).ToList()
                : LijstData.Where(x => x.DatumVerw != string.Empty).ToList();

            // filter op sectie
            if (comboBoxSectie.Text != "" && comboBoxSectie.Text != "ALLE")
            {
                LijstData = LijstData.Where(x => x.Sectie == comboBoxSectie.Text).ToList();
            }

            // Filter op Soort
            if (comboBoxSoortFilter.Text != "ALLE")
            {
                LijstData = LijstData.Where(x => x.Soort == comboBoxSoortFilter.Text).ToList();
            }

            // sorteer (ziet nu in laaddata)
            //LijstData = LijstData.OrderByDescending(o => o.RegNr).ToList();

            VulGrid();
        }

        private void TelData()
        {
            /*
              lijstTemp->LoadFromFile(dir + "overbrug.ini");
            // overbrugingen zonder werkvergunning
            ZoekSelVervangString("%#RST", lijstTemp->Strings[0]);
            ZoekSelVervangString("%#CON", lijstTemp->Strings[1]);
            ZoekSelVervangString("%#PBI", lijstTemp->Strings[2]);
            ZoekSelVervangString("%#PVK", lijstTemp->Strings[3]);
            ZoekSelVervangString("%#CGM", lijstTemp->Strings[4]);
            ZoekSelVervangString("%#SKV", lijstTemp->Strings[5]);
            ZoekSelVervangString("%#ALG",  lijstTemp->Strings[6]); // algemeen.
            ZoekSelVervangString("%#AOV", lijstTemp->Strings[7]);
            // overbrugingen met werkvergunning
            ZoekSelVervangString("%#RSTW", lijstTemp->Strings[8]);
            ZoekSelVervangString("%#CONW", lijstTemp->Strings[9]);
            ZoekSelVervangString("%#PBIW", lijstTemp->Strings[10]);
            ZoekSelVervangString("%#PVKW", lijstTemp->Strings[11]);
            ZoekSelVervangString("%#CGMW", lijstTemp->Strings[12]);
            ZoekSelVervangString("%#SKVW", lijstTemp->Strings[13]);
            ZoekSelVervangString("%#ALGW",  lijstTemp->Strings[14]); // algemeen.
            ZoekSelVervangString("%#AOVW", lijstTemp->Strings[15]);
            // overbrugingen verlopen
            ZoekSelVervangString("%#RSTV", lijstTemp->Strings[16]);
            ZoekSelVervangString("%#CONV", lijstTemp->Strings[17]);
            ZoekSelVervangString("%#PBIV", lijstTemp->Strings[18]);
            ZoekSelVervangString("%#PVKV", lijstTemp->Strings[19]);
            ZoekSelVervangString("%#CGMV", lijstTemp->Strings[20]);
            ZoekSelVervangString("%#SKVV", lijstTemp->Strings[21]);
            ZoekSelVervangString("%#ALGV",  lijstTemp->Strings[22]); // algemeen.
            ZoekSelVervangString("%#AOVV", lijstTemp->Strings[23]);
            // tiw
            ZoekSelVervangString("%#RSTT", lijstTemp->Strings[24]);
            ZoekSelVervangString("%#CONT", lijstTemp->Strings[25]);
            ZoekSelVervangString("%#PBIT", lijstTemp->Strings[26]);
            ZoekSelVervangString("%#PVKT", lijstTemp->Strings[27]);
            ZoekSelVervangString("%#CGMT", lijstTemp->Strings[28]);
            ZoekSelVervangString("%#SKVT", lijstTemp->Strings[29]);
            ZoekSelVervangString("%#ALGT",  lijstTemp->Strings[30]); // algemeen.
            ZoekSelVervangString("%#AOVT", lijstTemp->Strings[31]);
            // tiw verlopen
            ZoekSelVervangString("%#RSTTV", lijstTemp->Strings[32]);
            ZoekSelVervangString("%#CONTV", lijstTemp->Strings[33]);
            ZoekSelVervangString("%#PBITV", lijstTemp->Strings[34]);
            ZoekSelVervangString("%#PVKTV", lijstTemp->Strings[35]);
            ZoekSelVervangString("%#CGMTV", lijstTemp->Strings[36]);
            ZoekSelVervangString("%#SKVTV", lijstTemp->Strings[37]);
            ZoekSelVervangString("%#ALGTV",  lijstTemp->Strings[38]); // algemeen.
            ZoekSelVervangString("%#AOVTV", lijstTemp->Strings[39]);
            // MOC staat voor Management Of Change
            ZoekSelVervangString("%#RSTMOC", lijstTemp->Strings[40]);
            ZoekSelVervangString("%#CONMOC", lijstTemp->Strings[41]);
            ZoekSelVervangString("%#PBIMOC", lijstTemp->Strings[42]);
            ZoekSelVervangString("%#PVKMOC", lijstTemp->Strings[43]);
            ZoekSelVervangString("%#CGMMOC", lijstTemp->Strings[44]);
            ZoekSelVervangString("%#SKVMOC", lijstTemp->Strings[45]);
            ZoekSelVervangString("%#ALGMOC",  lijstTemp->Strings[46]); // algemeen.
            ZoekSelVervangString("%#AOVMOC", lijstTemp->Strings[47]);
            // MOC staat voor Management Of Change verlopen.
            ZoekSelVervangString("%#RSTMOCV", lijstTemp->Strings[48]);
            ZoekSelVervangString("%#CONMOCV", lijstTemp->Strings[49]);
            ZoekSelVervangString("%#PBIMOCV", lijstTemp->Strings[50]);
            ZoekSelVervangString("%#PVKMOCV", lijstTemp->Strings[51]);
            ZoekSelVervangString("%#CGMMOCV", lijstTemp->Strings[52]);
            ZoekSelVervangString("%#SKVMOCV", lijstTemp->Strings[53]);
            ZoekSelVervangString("%#ALGMOCV",  lijstTemp->Strings[54]); // algemeen.
            ZoekSelVervangString("%#AOVMOCV", lijstTemp->Strings[55]); 
             */


            NietAfgetekendWv = 0;
            VerlopenData = 0;
            DateTime nu = DateTime.Now;
            foreach (Data a in LijstData)
            {
                a.DatumTemp = GetDateTime(a.UitersteDatum);
                a.Kleur = false;

                if (a.DatumWv == string.Empty) // aantal niet afgetekend Wv
                {
                    NietAfgetekendWv++;
                    a.Kleur = true;
                }

                if (a.DatumTemp < verloopdatum && a.DatumVerw == "")    // datum verlopen
                {
                    VerlopenData++;
                    a.Kleur = true;
                }
                if(a.Sectie == "RST" && a.DatumVerw == "")
                {
                    if (a.Soort == "TIW")
                    {
                        teldata[(int)Rij.RowTiw, (int)SectieNaam.SecRst]++;
                        //if(a.UitersteDatum > verloopdatum)
                        //    teldata[(int)Rij.RowOverbVerl, (int)SectieNaam.SecRst]++;
                    }
                    if (a.Soort == "MOC")
                        teldata[(int)Rij.RowMoc,(int)SectieNaam.SecRst]++;
                    if (a.Soort == "OVERB")
                        teldata[(int)Rij.RowOverb,(int)SectieNaam.SecRst]++;
                }
                if (a.Sectie == "CON" && a.DatumVerw == "")
                {
                    if (a.Soort == "TIW")
                        teldata[(int)Rij.RowTiw, (int)SectieNaam.SecCon]++;
                    if (a.Soort == "MOC")
                        teldata[(int)Rij.RowMoc, (int)SectieNaam.SecCon]++;
                    if (a.Soort == "OVERB")
                        teldata[(int)Rij.RowOverb, (int)SectieNaam.SecCon]++;
                }
                if (a.Sectie == "PBI" && a.DatumVerw == "")
                {
                    if (a.Soort == "TIW")
                        teldata[(int)Rij.RowTiw, (int)SectieNaam.SecPbi]++;
                    if (a.Soort == "MOC")
                        teldata[(int)Rij.RowMoc, (int)SectieNaam.SecPbi]++;
                    if (a.Soort == "OVERB")
                        teldata[(int)Rij.RowOverb, (int)SectieNaam.SecPbi]++;
                }
                if (a.Sectie == "PVK" && a.DatumVerw == "")
                {
                    if (a.Soort == "TIW")
                        teldata[(int)Rij.RowTiw, (int)SectieNaam.SecPvk]++;
                    if (a.Soort == "MOC")
                        teldata[(int)Rij.RowMoc, (int)SectieNaam.SecPvk]++;
                    if (a.Soort == "OVERB")
                        teldata[(int)Rij.RowOverb, (int)SectieNaam.SecPvk]++;
                }
                if (a.Sectie == "CGM" && a.DatumVerw == "")
                {
                    if (a.Soort == "TIW")
                        teldata[(int)Rij.RowTiw, (int)SectieNaam.SecCgm]++;
                    if (a.Soort == "MOC")
                        teldata[(int)Rij.RowMoc, (int)SectieNaam.SecCgm]++;
                    if (a.Soort == "OVERB")
                        teldata[(int)Rij.RowOverb, (int)SectieNaam.SecCgm]++;
                }
                if (a.Sectie == "SKV" && a.DatumVerw == "")
                {
                    if (a.Soort == "TIW")
                        teldata[(int)Rij.RowTiw, (int)SectieNaam.SecSkv]++;
                    if (a.Soort == "MOC")
                        teldata[(int)Rij.RowMoc, (int)SectieNaam.SecSkv]++;
                    if (a.Soort == "OVERB")
                        teldata[(int)Rij.RowOverb, (int)SectieNaam.SecSkv]++;
                }
                if (a.Sectie == "AOV" && a.DatumVerw == "")
                {
                    if (a.Soort == "TIW")
                        teldata[(int)Rij.RowTiw, (int)SectieNaam.SecAov]++;
                    if (a.Soort == "MOC")
                        teldata[(int)Rij.RowMoc, (int)SectieNaam.SecAov]++;
                    if (a.Soort == "OVERB")
                        teldata[(int)Rij.RowOverb, (int)SectieNaam.SecAov]++;
                }
                if (a.Sectie == "ALG" && a.DatumVerw == "")
                {
                    if (a.Soort == "TIW")
                        teldata[(int)Rij.RowTiw, (int)SectieNaam.SecAlg]++;
                    if (a.Soort == "MOC")
                        teldata[(int)Rij.RowMoc, (int)SectieNaam.SecAlg]++;
                    if (a.Soort == "OVERB")
                        teldata[(int)Rij.RowOverb, (int)SectieNaam.SecAlg]++;
                }
            }
        }

        public void SaveData_lijst()
        {
            try
            {
                using (Stream stream = File.Open($"{datapath}overbrug.bin", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, LijstData);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();

                    // Melding
                    FormMelding md = new FormMelding(FormMelding.Type.Save, "Overbruging 2.0", "Data");
                    md.Show();
                }
            }
            catch
            {
                GC.Collect();
            }
            Backup($"{datapath}overbrug.bin");
        }

        public void SaveDataNamen_lijst()
        {
            try
            {
                using (Stream stream = File.Open($"{datapath}namen.bin", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, NamenLijst);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();

                    // Melding
                    FormMelding md = new FormMelding(FormMelding.Type.Save, "Overbruging 2.0", "Namen");
                    md.Show();
                }
            }
            catch
            {
                GC.Collect();
            }
        }

        public void SaveDataSecties_lijst()
        {
            try
            {
                using (Stream stream = File.Open($"{datapath}sectie.bin", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, SectieLijst);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();

                    // Melding
                    FormMelding md = new FormMelding(FormMelding.Type.Save, "Overbruging 2.0", "Sectie's");
                    md.Show();
                }
            }
            catch
            {
                GC.Collect();
            }
        }

        public void SaveDataInstallaties_lijst()
        {
            try
            {
                using (Stream stream = File.Open($"{datapath}install.bin", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, InstallatieLijst);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();

                    // Melding
                    FormMelding md = new FormMelding(FormMelding.Type.Save, "Overbruging 2.0", "Instalatie's");
                    md.Show();
                }
            }
            catch
            {
                GC.Collect();
            }
        }

        public void SaveInstelingen()
        {
            string inst = $"{datapath}progdata.ini";
            try
            {
                File.WriteAllLines(inst, instellingen);
            }
            catch (IOException)
            {
                MessageBox.Show("instelingen file save Error()");
            }
        }

        private void LaadInstelingen()
        {
            string inst = $"{datapath}progdata.ini";
            try
            {
                instellingen = File.ReadAllLines(inst).ToList();
            }
            catch
            {
                MessageBox.Show($"{inst} niet aanwezig, exit");
                Close();
            }
        }
        public void LaadData_lijst()
        {
            try
            {
                using (Stream stream = File.Open($"{datapath}overbrug.bin", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    LijstData.Clear();
                    BinaryFormatter bin = new BinaryFormatter();
                    LijstData = (List<Data>)bin.Deserialize(stream);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();
                }
            }
            catch
            {
                GC.Collect();
            }

            LastIndex = GetLaatsteRecord();
        }

        private int GetLaatsteRecord()
        {
            LijstData = LijstData.OrderByDescending(o => o.RegNr).ToList();
            return LijstData[0].RegNr;
        }

        public void LaadSecties_lijst()
        {
            try
            {
                using (Stream stream = File.Open($"{datapath}sectie.bin", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    LijstData.Clear();
                    BinaryFormatter bin = new BinaryFormatter();
                    SectieLijst = (List<Secties>)bin.Deserialize(stream);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();
                }
            }
            catch
            {
                GC.Collect();
            }
        }

        public void LaadNamen_lijst()
        {
            try
            {
                using (Stream stream = File.Open($"{datapath}namen.bin", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    NamenLijst.Clear();
                    BinaryFormatter bin = new BinaryFormatter();
                    NamenLijst = (List<NamenFunties>)bin.Deserialize(stream);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();
                }
            }
            catch
            {
                GC.Collect();
            }

            NamenLijst = NamenLijst.OrderBy(o => o.PersoneelNummer).ToList();
        }

        public void LaadInstallaties_lijst()
        {
            try
            {
                using (Stream stream = File.Open($"{datapath}install.bin", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    InstallatieLijst.Clear();
                    BinaryFormatter bin = new BinaryFormatter();
                    InstallatieLijst = (List<InstallatieOnderdeel>)bin.Deserialize(stream);
                    bin = null; // destroy voor volgende keer
                    GC.Collect();
                }
            }
            catch
            {
                GC.Collect();
            }
        }

        private void ButSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            DialogResult ret = settings.ShowDialog();
            if (ret == DialogResult.OK)
            {
                EditNamen ed = new EditNamen();
                _ = ed.ShowDialog();
            }
            if (ret == DialogResult.Abort)
            {
                EditSecties editSecties = new EditSecties();
                _ = editSecties.ShowDialog();
            }
            if (ret == DialogResult.Retry)
            {
                ButImport_Click(this, null);
            }
            ButRefresh_Click(this, null);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = System.Diagnostics.Process.Start("https://github.com/majoor404/Overbrugging");
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if(!IsIVer.Checked)
            //{
            //    MessageBox.Show("Geen rechten");
            //    return;
            //}
            try
            {
                // zoek record
                int regNr = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                TempData = ZoekDataRecord(regNr);

                // maak formulier
                Detail dt = new Detail();
                //dt.TextBoxRegNr.Text = TempData.RegNr.ToString();
                //dt.TextBoxRegNr.Enabled = false;

                // vullen dropdown items
                dt.ComboBoxSectie.Text = TempData.Sectie; // daarvoor heb ik wel sectie nodig.
                VulDropDownItems(dt);

                VulDatailForm(dt, TempData);

                // open dialog
                _ = dt.ShowDialog();

                //refresh
                ButRefresh_Click(this, null);
            }
            catch
            {
                _ = MessageBox.Show($"e = {e.RowIndex}");
            }
        }

        public void SorteerNaamOpNaam()
        {
            NamenLijst.Sort(Sorteer_Namen);
        }

        public int Sorteer_Namen(NamenFunties a, NamenFunties b)
        {
            return a.Naam.CompareTo(b.Naam);
        }

        private void DataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // zoek record
                int regNr = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                VulPreview(regNr);
            }
            catch { }
        }

        private void VulPreview(int index)
        {
            try
            {
                Data Q = ZoekDataRecord(index);
                TB1.Text = Q.Reden;
                TB2.Text = Q.Uitvoering;
                GeselRegNr.Text = index.ToString();
                TBDINV.Text = Q.DatumInv;
                TBSAP.Text = Q.SapNr;
                TBMOC.Text = Q.MocNr;
                TBSEC.Text = Q.Sectie;
                TBINST.Text = Q.Installatie;
                TBINSTD.Text = Q.InstallatieDeel;
                TBN1.Text = Q.Naam1;
                TBN2.Text = Q.Naam2;

                if (Q.Soort == "TIW")
                {
                    LabelType.Text = "Tijdelijke Instalatie Wijziging";
                }
                if (Q.Soort == "MOC")
                {
                    LabelType.Text = "Management Of Change";
                }
                if (Q.Soort == "OVERB")
                {
                    LabelType.Text = "Overbruging";
                }
                TBDWV.Text = Q.DatumWv;
                TBDWVV.Text = Q.UitersteDatum;
                TBNWV.Text = Q.NaamWV;
                TextBoxBijzIVWV.Text = Q.BijzonderhedenWV;
            }
            catch
            {
                TB1.Clear();
                TB2.Clear();
                GeselRegNr.Text = string.Empty;
                TBDINV.Text = string.Empty;
                TBSAP.Text = string.Empty;
                TBMOC.Text = string.Empty;
                TBSEC.Text = string.Empty;
                TBINST.Text = string.Empty;
                TBINSTD.Text = string.Empty;
                TBN1.Text = string.Empty;
                TBN2.Text = string.Empty;
                LabelType.Text = string.Empty;
                TBDWV.Text = string.Empty;
                TBDWVV.Text = string.Empty;
                TBNWV.Text = string.Empty;
                TextBoxBijzIVWV.Text = string.Empty;
            }
        }

        private void ButtonNieuw_Click(object sender, EventArgs e)
        {
            KeuzeType KS = new KeuzeType();
            DialogResult retKeuzeForm = KS.ShowDialog();

            Detail dt = new Detail();

            TempData = new Data();

            if (retKeuzeForm == DialogResult.OK)
            {
                // TIW
                TempData.Soort = "TIW";
            }
            if (retKeuzeForm == DialogResult.Yes)
            {
                //OVERB
                TempData.Soort = "OVERB";
            }
            if (retKeuzeForm == DialogResult.Abort)
            {
                //MOC
                TempData.Soort = "MOC";
            }
            if (retKeuzeForm == DialogResult.Cancel)
            {
                return;
            }

            LaadNamen_lijst();
            VulDropDownItems(dt);

            TempData.DatumInv = dt.DatumInv.Datum = DateTime.Now.ToShortDateString();

            // open dialog
            _ = dt.ShowDialog();

            //refresh
            ButRefresh_Click(this, null);
        }

        public void VulDropDownItems(Detail dt)
        {
            VulSectiesDropDown(dt);
            VulSectiesOnderdeelDropDown(dt);
            VulNamenPersoneel(dt);
            VulNamenIVWVPersoneel(dt);
        }  // vullen dropdown items

        private void VulNamenIVWVPersoneel(Detail dt)
        {
            List<NamenFunties> IVWVFilter = new List<NamenFunties>();
            IVWVFilter = NamenLijst.Where(x => x.IVWV == true).ToList();
            for (int i = 0; i < IVWVFilter.Count; i++)
            {
                _ = dt.ComboBoxIVWV.Items.Add(IVWVFilter[i].Naam);
            }
        }

        private void VulNamenPersoneel(Detail dt)
        {
            dt.ComboBoxNaam1.Items.Clear();
            dt.ComboBoxNaam2.Items.Clear();
            dt.ComboBoxNaamVerw.Items.Clear();
            for (int i = 0; i < NamenLijst.Count; i++)
            {
                _ = dt.ComboBoxNaam1.Items.Add(NamenLijst[i].Naam);
                _ = dt.ComboBoxNaam2.Items.Add(NamenLijst[i].Naam);
                _ = dt.ComboBoxNaamVerw.Items.Add(NamenLijst[i].Naam);
            }
        }

        public void VulSectiesOnderdeelDropDown(Detail dt)
        {
            List<InstallatieOnderdeel> InstallatieLijstFilter = new List<InstallatieOnderdeel>();
            InstallatieLijstFilter = InstallatieLijst.Where(x => x.Sectie == dt.ComboBoxSectie.Text).ToList();
            dt.ComboSectieDeel.Items.Clear();
            for (int i = 0; i < InstallatieLijstFilter.Count; i++)
            {
                _ = dt.ComboSectieDeel.Items.Add(InstallatieLijstFilter[i].Instal);
            }
        }

        private void VulSectiesDropDown(Detail dt)
        {
            dt.ComboBoxSectie.Items.Clear();
            for (int i = 0; i < MainForm.Main.SectieLijst.Count; i++)
            {
                _ = dt.ComboBoxSectie.Items.Add(MainForm.Main.SectieLijst[i].Naam);
            }
        }

        private void ButtonWijzig_Click(object sender, EventArgs e)
        {
            if (GeselRegNr.Text == "0000")
            {
                return;
            }

            try
            {
                // zoek record
                int regNr = int.Parse(GeselRegNr.Text);
                Data Q = ZoekDataRecord(regNr);

                // maak formulier
                Detail dt = new Detail();
                dt.TextBoxRegNr.Text = Q.RegNr.ToString();
                dt.TextBoxRegNr.Enabled = false;

                // vullen dropdown items
                dt.ComboBoxSectie.Text = Q.Sectie; // daarvoor heb ik wel sectie nodig.
                VulDropDownItems(dt);

                VulDatailForm(dt, Q);

                // open dialog
                _ = dt.ShowDialog();

                //refresh
                ButRefresh_Click(this, null);
            }
            catch
            {
                _ = MessageBox.Show($"Kon detail van record {GeselRegNr.Text} niet laden");
            }


        }

        private void VulDatailForm(Detail dt, Data Q)
        {
            try
            {
                LaadNamen_lijst();

                // vullen dropdown items
                dt.ComboBoxSectie.Text = Q.Sectie; // daarvoor heb ik wel sectie nodig.
                VulDropDownItems(dt);

                // bovenste panel vullen met data
                dt.DatumInv.Datum = Q.DatumInv;
                dt.TextBoxSapNr.Text = Q.SapNr;
                dt.TextBoxMocNr.Text = Q.MocNr;
                dt.ComboSectieDeel.Text = Q.Installatie;
                dt.TextBoxInstDeel.Text = Q.InstallatieDeel;
                dt.ComboBoxNaam1.Text = Q.Naam1;
                dt.ComboBoxNaam2.Text = Q.Naam2;
                dt.TextBoxRede.Text = Q.Reden;
                dt.TextBoxOplossing.Text = Q.Uitvoering;
                // middelste panel
                dt.DatumWv.Datum = Q.DatumWv;
                dt.ComboBoxIVWV.Text = Q.NaamWV;
                dt.DatumVerloopTIW.Datum = Q.UitersteDatum;
                dt.TextBoxBijzIVWV.Text = Q.BijzonderhedenWV;
                dt.ButtonType.Text = Q.Soort;
                // onderste panel
                dt.DatumVerw.Datum = Q.DatumVerw;
                dt.ComboBoxNaamVerw.Text = Q.Naamverw;
                dt.TextBoxBijzVerw.Text = Q.BijzonderhedenVerw;
            }
            catch
            {
                _ = MessageBox.Show($"Error VulDetailForm");
            }
        }

        private void Backup(string file)
        {
            string nieuw_naam = "";
            try
            {
                if (File.Exists(file))
                {

                    if (!Directory.Exists("Backup"))
                    {
                        _ = Directory.CreateDirectory("Backup");
                    }

                    string s = DateTime.Now.ToString("MM-dd-yyyy HH-mm-ss");

                    nieuw_naam = Directory.GetCurrentDirectory() + @"\Backup\overbrug_" + s + ".bin";
                    File.Copy(file, nieuw_naam, true);  // overwrite oude file

                    List<FileInfo> files = new DirectoryInfo("Backup").EnumerateFiles("*overbrug_*")
                                    .OrderByDescending(f => f.CreationTime)
                                    .Skip(10)
                                    .ToList();

                    files.ForEach(f => f.Delete());
                }
            }
            catch
            {
                _ = MessageBox.Show($"Save backup ging fout\n{nieuw_naam}");
            }
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

        private void ButZoek_Click(object sender, EventArgs e)
        {
            Zoek zk = new Zoek();
            zk.ShowDialog();

            LaadData_lijst();
            dataGridView1.DataSource = null;

            List<Data> temp = new List<Data>();
            foreach (Data a in LijstData)
            {
                if (zk.CBNaam.Checked)
                {
                    if (a.Naam1.ToLower().Contains(zk.TBZoek.Text.ToLower()))
                    {
                        temp.Add(a);
                        continue;
                    }
                }
                if (zk.CBReden.Checked)
                {
                    if (a.Reden.ToLower().Contains(zk.TBZoek.Text.ToLower()))
                    {
                        temp.Add(a);
                        continue;
                    }
                }
                if (zk.CBOplossing.Checked)
                {
                    if (a.Uitvoering.ToLower().Contains(zk.TBZoek.Text.ToLower()))
                    {
                        temp.Add(a);
                        continue;
                    }
                }
                if (zk.CBSap.Checked)
                {
                    if (a.SapNr.ToLower().Contains(zk.TBZoek.Text.ToLower()))
                    {
                        temp.Add(a);
                        continue;
                    }
                }
                if (zk.CBMoc.Checked)
                {
                    if (a.MocNr.ToLower().Contains(zk.TBZoek.Text.ToLower()))
                    {
                        temp.Add(a);
                        continue;
                    }
                }
                if (zk.CBIVWV.Checked)
                {
                    if (a.NaamWV.ToLower().Contains(zk.TBZoek.Text.ToLower()))
                    {
                        temp.Add(a);
                        continue;
                    }
                }
                if (zk.CBNum.Checked)
                {
                    bool ok = int.TryParse(zk.TBZoek.Text, out int reg);
                    if (ok && a.RegNr == reg)
                    {
                        temp.Add(a);
                        continue;
                    }
                }
                if (zk.CBInst.Checked)
                {
                    if (a.Installatie.ToLower().Contains(zk.TBZoek.Text.ToLower()))
                    {
                        temp.Add(a);
                        continue;
                    }
                }
            }
            LijstData = temp;
            //wait(500);
            VulGrid();
        }
        private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dataGridView1.Columns[e.ColumnIndex];
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn)
                {
                    if (SortRichting == SortOrder.Ascending)
                    {
                        SortRichting = SortOrder.Descending;
                        direction = ListSortDirection.Descending;
                    }
                    else
                    {
                        SortRichting = SortOrder.Ascending;
                        direction = ListSortDirection.Ascending;
                    }
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    SortRichting = SortOrder.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
                SortRichting = SortOrder.Ascending;

            }

            oldColumn = newColumn;

            //// Sort the selected column.
            Sort(newColumn, direction);

            if (LijstData.Count > 0)
                dataGridView1.DataSource = LijstData;

            if (SortRichting == SortOrder.Ascending)
                newColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            else
                newColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending;

            KleurAfwijkingen();

            Wait(300);
        }

        private void KleurAfwijkingen()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                if (row.Cells[9].Value.ToString() == "True")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(243, 221, 228); // Azure;
                }
        }

        private void Sort(DataGridViewColumn newColumn, ListSortDirection richting)
        {
            if (newColumn.DataPropertyName == "Regnr")
            {
                if (richting == ListSortDirection.Ascending)
                {
                    LijstData = LijstData.OrderBy(o => o.RegNr).ToList();
                }
                else
                {
                    LijstData = LijstData.OrderByDescending(o => o.RegNr).ToList();
                }
            }

            if (newColumn.DataPropertyName == "DatumInv")
            {
                foreach (Data q in LijstData)
                {
                    q.DatumTemp = GetDateTime(q.DatumInv);
                }

                if (richting == ListSortDirection.Ascending)
                    LijstData = LijstData.OrderBy(o => o.DatumTemp).ToList();
                else
                    LijstData = LijstData.OrderByDescending(o => o.DatumTemp).ToList();
            }

            if (newColumn.DataPropertyName == "Soort")
            {
                if (richting == ListSortDirection.Ascending)
                {
                    LijstData = LijstData.OrderBy(o => o.Soort).ToList();
                }
                else
                {
                    LijstData = LijstData.OrderByDescending(o => o.Soort).ToList();
                }
            }

            if (newColumn.DataPropertyName == "Sectie")
            {
                if (richting == ListSortDirection.Ascending)
                {
                    LijstData = LijstData.OrderBy(o => o.Sectie).ToList();
                }
                else
                {
                    LijstData = LijstData.OrderByDescending(o => o.Sectie).ToList();
                }
            }

            if (newColumn.DataPropertyName == "Installatie")
            {
                if (richting == ListSortDirection.Ascending)
                {
                    LijstData = LijstData.OrderBy(o => o.Installatie).ToList();
                }
                else
                {
                    LijstData = LijstData.OrderByDescending(o => o.Installatie).ToList();
                }
            }

            if (newColumn.DataPropertyName == "InstallatieDeel")
            {
                if (richting == ListSortDirection.Ascending)
                {
                    LijstData = LijstData.OrderBy(o => o.InstallatieDeel).ToList();
                }
                else
                {
                    LijstData = LijstData.OrderByDescending(o => o.InstallatieDeel).ToList();
                }
            }

            if (newColumn.DataPropertyName == "Reden")
            {
                if (richting == ListSortDirection.Ascending)
                {
                    LijstData = LijstData.OrderBy(o => o.Reden).ToList();
                }
                else
                {
                    LijstData = LijstData.OrderByDescending(o => o.Reden).ToList();

                }
            }
            if (newColumn.DataPropertyName == "UitersteDatum")
            {
                foreach (Data q in LijstData)
                {
                    q.DatumTemp = GetDateTime(q.UitersteDatum);
                }

                if (richting == ListSortDirection.Ascending)
                    LijstData = LijstData.OrderBy(o => o.DatumTemp).ToList();
                else
                    LijstData = LijstData.OrderByDescending(o => o.DatumTemp).ToList();
            }
        }

        private DateTime GetDateTime(string  datum) // 21-12-2023 09-11-2023 20-01-2023
        {
            if(string.IsNullOrEmpty(datum))
            {
                return DateTime.Now;
            }
            int jaar = int.Parse(datum.Substring(6,4));
            int maand = int.Parse(datum.Substring(3, 2));
            int dag = int.Parse(datum.Substring(0, 2));
            DateTime ret = new DateTime(jaar,maand,dag);
            return ret;
        }

        private void IVWVVraag_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Door inlognaam windows (personeel nr)\nKrijg u rechten voor invoer\nOf als u WV of IV bent verwijderen.");
        }
    }
}