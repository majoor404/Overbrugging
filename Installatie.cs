using System;

namespace Overbrugging
{
    [Serializable]
    public class InstallatieOnderdeel
    {
        public InstallatieOnderdeel() { }
        public string Index { get; set; }
        public string Instal { get; set; }
        public string Sectie { get; set; }
    }
}
