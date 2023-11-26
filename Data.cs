using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overbrugging
{
    public class Data
    {
        public Data() { }
        
        //invoer
        public string RegNr;
        public DateTime DatumInv;
        public string SapNr;
        public string MocNr;

        public string Sectie;
        public string Installatie;
        public string InstallatieDeel;

        public string Naam1;
        public string Naam2;
        public string Ploeg;

        public string Reden;
        public string Uitvoering;
        
        // ivwv
        public bool WerkVerg;
        public string WerkVergNr;

        public DateTime DatumWv;
        public string NaamWV;

        public DateTime UitersteDatum;
        public DateTime DatumVerloopWV;

        public int Soort;

        public string BijzonderhedenWV;
        
        //verwijderen
        public string Naamverw;
        public DateTime DatumVerw;
        public string BijzonderhedenVerw;

        public string ReserveS1;
        public string ReserveS2;
        public int ReserveI1;
        public int ReserveI2;
        public DateTime ReserveD1;
        public DateTime ReserveD2;
        public bool ReserveB1;
        public bool ReserveB2;
    }
}
