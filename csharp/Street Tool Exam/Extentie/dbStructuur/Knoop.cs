using System;
using System.Collections.Generic;
using System.Text;

namespace Extentie.Structuur
{
    public class Knoop
    {
        public int KnoopId { get; private set; }
        public Punt Punt { get; set; }

        public Knoop(int knoopId, Punt punt)
        {
            KnoopId = knoopId;
            Punt = punt;
        }
        /*
        public override string ToString()
        {
            string text = string.Join(",", Punt);
            return text;
        }
        */
    }
}
