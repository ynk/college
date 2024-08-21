using System;
using System.Collections.Generic;
using System.Text;
using Extentie.inputStructuur;


namespace Extentie.dbStructuur
{
    public class SourceHelper
    { 
      public List<ProvincieInfo> ProvincieInfoList { get; set; }
      public List<WrData> wrDataList { get; set; }
      public Dictionary<int, WrGemeenteID> WrGeementeIdDictionary { get; set; } 
      public List<WrGemeenteNaam> WrGemeenteNaamlList { get; set; }
      public Dictionary<int,WrStraatNamen> wrStraatNamenLijst { get; set; }

    }
}
