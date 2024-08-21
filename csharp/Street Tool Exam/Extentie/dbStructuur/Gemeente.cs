using System;
using System.Collections.Generic;
using System.Text;

namespace Extentie.Structuur
{
    public class Gemeente
    {
        public int GemeenteId { get; private set; }
        public Provincie Provincie { get; set; }
        public string GemeenteNaam { get; set; }

        public Gemeente(int gemeenteId, Provincie provincie, String gemeenteNaam)
        {
            GemeenteId = gemeenteId;
            Provincie = provincie;
            GemeenteNaam = gemeenteNaam;
        }
    }
}
