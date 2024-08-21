using System;
using System.Collections.Generic;
using System.Text;
using Extentie.Structuur;

namespace Extentie.dbStructuur
{
    public class DataModel
    {


        public Dictionary<int, Straat> StraatenDictionary { get; set; }
        public Dictionary<int, Gemeente> GemeenteDictionary { get; set; }
        public Dictionary<int, Provincie> ProvinciesDictionary { get; set; }
        public Dictionary<int, Segment> SegmentenDictionary { get; set; }
        public Dictionary<int, Graaf> GraafsDictionary { get; set; }
        public Dictionary<int, Knoop> KnoopenDictionary { get; set; }

        public DataModel()
        {
            StraatenDictionary = new Dictionary<int, Straat>();
            GemeenteDictionary = new Dictionary<int, Gemeente>();
            ProvinciesDictionary = new Dictionary<int, Provincie>();
            SegmentenDictionary = new Dictionary<int, Segment>();
            GraafsDictionary = new Dictionary<int, Graaf>();
            KnoopenDictionary = new Dictionary<int, Knoop>();
        }

    }
}
