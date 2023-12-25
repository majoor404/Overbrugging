using System;
using System.ComponentModel;

namespace Overbrugging
{
    [Serializable]
    public class Data
    {
        //public static string heleText(string _Soort)
        //{
        //    if (_Soort == "0")
        //        return "Overb";
        //    if (_Soort == "1")
        //        return "TIW";
        //    if (_Soort == "2")
        //        return "MOC";
        //    return _Soort;
        //}
        public Data() { }

        //invoer
        public int RegNr { get; set; }
        public string DatumInv { get; set; }
        public string SapNr { get; set; }
        public string MocNr { get; set; }

        public string Sectie { get; set; }
        public string Installatie { get; set; }
        public string InstallatieDeel { get; set; }

        public string Naam1 { get; set; }
        public string Naam2 { get; set; }
        public string Ploeg { get; set; }

        public string Reden { get; set; }
        public string Uitvoering { get; set; }

        // ivwv
        public string WerkVerg { get; set; }
        public string WerkVergNr { get; set; }

        public string DatumWv { get; set; }
        public string NaamWV { get; set; }

        public string UitersteDatum { get; set; }
        public string DatumVerloopWV { get; set; }
        public string Soort { get; set; } 
        public string BijzonderhedenWV { get; set; }

        //verwijderen
        public string Naamverw { get; set; }
        public string DatumVerw { get; set; }
        public string BijzonderhedenVerw { get; set; }

        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
        public string Reserve6 { get; set; }
        public bool Kleur { get; set; }
        public DateTime DatumTemp { get; set; }
    }
}
