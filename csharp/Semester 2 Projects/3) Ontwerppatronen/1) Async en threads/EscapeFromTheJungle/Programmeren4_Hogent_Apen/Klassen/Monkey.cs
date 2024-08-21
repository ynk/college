using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Prog4_Hogent_Apen.Klassen
{
    public  class Monkey
    {

        public int id { get; set; }
        public string Name { get; set; }

        public Color Kleur { get; set; }

        public Tree CurrentTree { get; set; }
        
        
        public List<Tree> visitedTrees { get; set; } = new List<Tree>();

    }
}
