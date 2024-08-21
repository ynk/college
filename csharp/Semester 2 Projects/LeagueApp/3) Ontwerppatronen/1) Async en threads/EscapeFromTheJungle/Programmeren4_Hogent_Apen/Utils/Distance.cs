using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Prog4_Hogent_Apen.Klassen;


namespace Prog4_Hogent_Apen.Utils
{
    public class Distance
    {
        public static double getDistance(Tree tree1, Tree tree2)
        {
            return Math.Sqrt(Math.Pow(tree1.X - tree1.Y, 2) + Math.Pow(tree2.X - tree2.Y, 2));
        }

        //Cries in Programming


        public static double getDistanceToBorder(Map map, Tree tree)
        {
            return (new List<double>()
            {
                map.yMax - tree.Y,
                map.xMax - tree.X,
                tree.Y - map.yMin,
                tree.X - map.xMin
            }).Min();

        }

    }
}
