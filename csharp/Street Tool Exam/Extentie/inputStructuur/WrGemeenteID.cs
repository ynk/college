using System;
using System.Collections.Generic;
using System.Text;

namespace Extentie.inputStructuur
{
    public class WrGemeenteID
    {
        public WrGemeenteID(int straatNaamId, int gemeenteId)
        {
            this.straatNaamId = straatNaamId;
            this.gemeenteId = gemeenteId;
        }

        public int straatNaamId { get; set; }
        public int gemeenteId { get; set; }
    }
}
