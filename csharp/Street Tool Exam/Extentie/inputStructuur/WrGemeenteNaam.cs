using System;
using System.Collections.Generic;
using System.Text;

namespace Extentie.inputStructuur
{
    public class WrGemeenteNaam
    {
        public int gemeenteNaamId { get; set; }
        public int gemeenteId { get; set; }
        public string taalCodeGemeenteNaam { get; set; } // un used eigelijk
        public string gemeenteNaam { get; set; }

        public WrGemeenteNaam(int gemeenteNaamId, int gemeenteId, string taalCodeGemeenteNaam, string gemeenteNaam)
        {
            this.gemeenteNaamId = gemeenteNaamId;
            this.gemeenteId = gemeenteId;
            this.taalCodeGemeenteNaam = taalCodeGemeenteNaam;
            this.gemeenteNaam = gemeenteNaam;
        }


    }
}
