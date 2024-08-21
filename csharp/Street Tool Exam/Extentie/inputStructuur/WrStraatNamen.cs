using System;
using System.Collections.Generic;
using System.Text;

namespace Extentie.inputStructuur
{
    public class WrStraatNamen
    {
        public WrStraatNamen(int straatId, string straatNaam)
        {
            this.straatId = straatId;
            this.straatNaam = straatNaam;
        }

        public int straatId { get; set; }
        public string straatNaam { get; set; }
    }
}
