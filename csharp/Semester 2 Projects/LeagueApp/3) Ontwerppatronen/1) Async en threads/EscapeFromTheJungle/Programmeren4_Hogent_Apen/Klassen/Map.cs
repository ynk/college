using System;
using System.Collections.Generic;
using System.Text;

namespace Prog4_Hogent_Apen.Klassen
{
    public class Map
    {
        public int id { get; set; }
        public int xMin { get; set; }
        public int xMax { get; set; }

        public int yMin { get; set; }
        public int yMax { get; set; }

       
        public List<Tree> TreeList { get; set; }
        public List<Monkey> MonkeyList { get; set; }

        /*yMin      xMin         xMax
         *---------------------
         *|
         *|
         *|
         *|
         *|
         *|
         *yMax
         *
         */
    }
}
