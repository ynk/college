using System;
using System.Collections.Generic;
using System.Text;

namespace Extentie.Structuur
{
    public class Provincie
    {
        public int ProvincieId { get; private set; }
        public string ProvincieNaam { get; set; }

        public Provincie(int provincieId, string provincieNaam)
        {
            ProvincieId = provincieId; // check
            ProvincieNaam = provincieNaam;
        }
    }
}
