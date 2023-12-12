using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overbrugging
{
    [Serializable]
    public class NamenFunties
    {
        public NamenFunties() { }
        public string Index { get; set; }
        public string PersoneelNummer { get; set; }
        public string Naam { get; set; }
        public string Team { get; set; }
        public bool Funtie { get; set; }
        public bool IVW { get; set; }
    }
}
