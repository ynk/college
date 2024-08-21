using System;
using System.Collections.Generic;
using System.Text;
using Extentie.Structuur;

namespace Extentie.Handlers
{
    public static class StraatMath
    {
        public static decimal getLenghtSegment(this Segment segment)
        {
            decimal result = 0;
            for (int i = 0; i < segment.Punten.Count - 1; i++)
            {
                var x = segment.Punten[i];
                var y = segment.Punten[i + 1];
                result += (decimal)System.Math.Sqrt((double)((x.X - y.X) * (x.X - y.X) + (x.Y - y.Y) * (x.Y - y.Y)));
            }

            return result;
        }
        /*
        public IEnumerable<Straat> getLangste()
        {
            
            

        }
        */

        public static decimal getLengthStraat(this Straat straat)
        {
            decimal result = 0;
            foreach (var knoopSegmentenList in straat.Graaf.KnoopSegmenten.Values)
            {
                foreach (var segment in knoopSegmentenList)
                {
                    result += segment.getLenghtSegment();
                }
            }

            return result;
        }
    }

   

}
