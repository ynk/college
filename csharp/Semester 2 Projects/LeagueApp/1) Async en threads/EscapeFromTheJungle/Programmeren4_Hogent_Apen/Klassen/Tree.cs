using System;
using System.Collections.Generic;
using System.Text;
using Prog4_Hogent_Apen.Utils;

namespace Prog4_Hogent_Apen.Klassen
{
    public class Tree
    { 

        public int TreeID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int bosId { get; set; }
        public Monkey MonkeyInTree { get; set; }

        public int Radius { get; set; } = 10;
        // boomshine
        public bool Overlaps(Tree curTree)
        {
            double rsum = Radius + curTree.Radius;
            return (rsum >= DistanceTo(curTree));
        }
        public double DistanceTo(Tree curTree)
        {
            double heightBetweenPoints = (Y + Radius / 2) - (curTree.Y + Radius / 2);
            double lenthBetweenPoints = (X + Radius / 2) - (curTree.X + Radius / 2);
             return Math.Sqrt(Math.Pow(lenthBetweenPoints, 2) + Math.Pow(heightBetweenPoints, 2));
        }

    }
}
