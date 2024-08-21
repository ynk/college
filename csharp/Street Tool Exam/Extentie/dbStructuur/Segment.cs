using System;
using System.Collections.Generic;
using System.Text;

namespace Extentie.Structuur
{
    public class Segment
    {
        public int SegmentId { get; private set; }
        public Knoop BeginKnoop { get; set; }
        public Knoop EindKnoop { get; set; }
        public List<Punt> Punten { get; set; }

        public Segment(int segmentId, Knoop beginKnoop, Knoop eindKnoop, List<Punt> punten)
        {
            SegmentId = segmentId;
            BeginKnoop = beginKnoop;
            EindKnoop = eindKnoop;
            Punten = punten;
        }
    }
}
