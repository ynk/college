using System;
using System.Collections.Generic;
using System.Text;

namespace Extentie.Structuur
{
    public class Graaf
    {


        public int GraafId { get; private set; }
        public Dictionary<Knoop, List<Segment>> KnoopSegmenten { get; set; }

        public Graaf(int graafId)
        {
            GraafId = graafId;
  
        }

    }
}
