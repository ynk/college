using System;
using System.Collections.Generic;
using System.Text;

namespace Extentie.Structuur
{
    public class Punt
    {
        public decimal X { get; private set; }
        public decimal Y { get; private set; }

        public Punt(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X} {Y}";
        }
    }
}
