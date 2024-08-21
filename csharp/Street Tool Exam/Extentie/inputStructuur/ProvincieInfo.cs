using System;
using System.Collections.Generic;
using System.Text;

namespace Extentie.inputStructuur
{
    public class ProvincieInfo
    {
        public ProvincieInfo(int gemeenteId, int provincieId, string provincieNaam)
        {
            this.gemeenteId = gemeenteId;
            this.provincieId = provincieId;
            this.provincieNaam = provincieNaam;
        }

        public int gemeenteId { get; set; }
        public int provincieId { get; set; }
        public string provincieNaam { get; set; }
    }
}
