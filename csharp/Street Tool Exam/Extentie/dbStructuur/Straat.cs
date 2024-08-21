using System;
using System.Collections.Generic;
using System.Text;

namespace Extentie.Structuur
{
    public class Straat
    {
        public int StraatId { get; private set; }
        public string StraatNaam { get; set; }
        public Graaf Graaf { get; set; }
        public Gemeente Gemeente { get; set; }
        // StraatID, StraatNaam, Graaf, Gemeente
      //  public int straatLengte { get; set; }
        public Straat(int straatId, string straatNaam, Graaf graaf, Gemeente gemeente)
        {
            StraatId = straatId;
            StraatNaam = straatNaam;
            Graaf = graaf;
            Gemeente = gemeente;
            //lengte = lengte;

        }

    }
}
