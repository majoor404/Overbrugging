using System;
using System.Reflection;
using System.Text.RegularExpressions;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Overbrugging
{
    //[Serializable]
    //[XmlRoot(ElementName = "QueryOverbrug")]
    public class OudeOverbrugging
    {
        public OudeOverbrugging()
        {
            //RegNr = DatumInv = Sectie = Installatie = InstallatieDeel = "";
            //NaamKKD1uit = NaamKKD2uit = Ploeg = Reden = "";
            //Uitvoering = EnigeOverb = WerkVerg = WerkVergNr = "";
            //WerkVergOk = SrsNr = DatumWv = NaamWV = NaamKKDverw = "";
            //UitersteDatum = Veld1 = BijzonderhedenWV = OvernemenIWV = "";
            //TIW = TIWOB = InGebruik = PersWijzig = BewerkTijd = Soort = MocRsNr = "";
        }

        public string RegNr;
        public string DatumInv;
        public string Sectie;
        public string Installatie;
        public string InstallatieDeel;
        public string NaamKKD1uit;
        public string NaamKKD2uit;
        public string Ploeg;
        public string Reden;
        public string Uitvoering;
        public string EnigeOverb;
        public string WerkVerg;
        public string WerkVergNr;
        public string WerkVergOk;
        public string SrsNr;
        public string DatumWv;
        public string NaamWV;
        public string DatumVerw;
        public string NaamKKDverw;
        public string UitersteDatum;
        public string Veld1;
        public string PrintDatum;
        public string BijzonderhedenWV;
        public string BijzonderhedenVerw;
        public string OvernemenIWV;
        public string DatumVerloopWV;
        public string TIW;
        public string TIWOB;
        public string InGebruik;
        public string PersWijzig;
        public string BewerkTijd;
        public string Soort;
        public string MocRsNr;
        public string Reserve;
    }
}
