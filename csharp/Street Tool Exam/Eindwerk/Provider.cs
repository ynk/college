using System;
using System.Collections.Generic;
using System.Linq;
using Extentie.dbStructuur;
using Extentie.inputStructuur;
using Extentie.Structuur;

namespace Tool1
{
    public class Provider
    {
        private static int _graafId = 1;
      
        public static DataModel Read(SourceHelper sourceData)
        {
            DataModel resultaat = new DataModel();
            Console.WriteLine("[Read] Parsing: Provincie");
            foreach (var cur_prov in sourceData.ProvincieInfoList)
            {
                if (!resultaat.ProvinciesDictionary.ContainsKey(cur_prov.provincieId)) 
                {
                    var provincie = new Provincie(cur_prov.provincieId, cur_prov.provincieNaam);
                    resultaat.ProvinciesDictionary.Add(cur_prov.provincieId, provincie);
                }

            }
            Console.WriteLine($"[Read] Parsing Provincie Done! {resultaat.ProvinciesDictionary.Count}");
            Console.WriteLine("[Read] Parsing: Gemeenten");

            foreach (var cur_gem in sourceData.WrGemeenteNaamlList)
            {
                if (!resultaat.GemeenteDictionary.ContainsKey(cur_gem.gemeenteId))
                {
                    var sourc = sourceData.ProvincieInfoList.Where(s => s.gemeenteId == cur_gem.gemeenteId)
                        .FirstOrDefault();
                    if (sourc == null)
                    {
                        continue;
                    }

                    var _provintie = resultaat.ProvinciesDictionary[sourc.provincieId];
                    var nieuwe_gemeente = new Gemeente(cur_gem.gemeenteId, _provintie, cur_gem.gemeenteNaam);
                    resultaat.GemeenteDictionary.Add(nieuwe_gemeente.GemeenteId, nieuwe_gemeente);
                }
            }
            Console.WriteLine($"[Read] Parsing Gemeenten Done! {resultaat.GemeenteDictionary.Count}");
            // straaten opvullen
            vulStraatDataIn(sourceData, resultaat);
            return resultaat;

        }

        public static int nextGraafID()
        {
            // random generator,+1
            return _graafId++;

        }

        public static void vulStraatDataIn(SourceHelper source, DataModel resultaat)
        {
            Console.Write ("[Read]  vulStraatDataIn ");
            foreach (var wrData in source.wrDataList)
            {
                var straatRechts = StraatHelper(source, resultaat, wrData.rechtsStraatNaamId);
                var straatLinks = StraatHelper(source, resultaat, wrData.linksStraatNaamId);
                
                if (straatLinks != null)
                {

                    vulStraatGraafin(resultaat, straatLinks, wrData);

                }

                if (straatRechts != null)
                {

                    vulStraatGraafin(resultaat, straatRechts, wrData);

                }
            }
            Console.WriteLine(" | Done");

        }

        public static void vulStraatGraafin(DataModel resultaat, Straat straat, WrData wrData)
        {

            
            var puntenList = new List<Punt>();

            var punten = wrData.geo.Replace("LINESTRING (", String.Empty).Replace(")", String.Empty).Split(", ");

            foreach (var s in punten)
            {
                

                  var punt = s.Replace(".",",").Split(" "); // punt koma probleem ik heb geen fix :(
                //var punt = s.Split(" ");
                  
                decimal x = decimal.Parse(punt[0]);
                decimal y = decimal.Parse(punt[1]);

                var current_punt = new Punt(x, y);
                puntenList.Add(current_punt);
            }
            wegSegmentHandler(resultaat, straat, wrData, puntenList);

        }

        public static void wegSegmentHandler(DataModel resultaat, Straat straat, WrData wrData, List<Punt> puntenList)
        {
            var graaf = straat.Graaf;
            var beginKnoopID = wrData.beginWegknoopID;
            var eindKnoopID = wrData.eindWegknoopID;
            var wegSegId = (wrData.wegsegmentID);
            Knoop beginKnoop;
            Knoop eindKnoop;
            Segment wegSegment;

            if (resultaat.KnoopenDictionary.ContainsKey(beginKnoopID))
            {
                beginKnoop = resultaat.KnoopenDictionary[beginKnoopID];
            }
            else
            {
                beginKnoop = new Knoop(beginKnoopID, puntenList[0]);
                resultaat.KnoopenDictionary.Add(beginKnoopID, beginKnoop);
            }

            
            if (resultaat.KnoopenDictionary.ContainsKey(eindKnoopID))
            {
                eindKnoop = resultaat.KnoopenDictionary[eindKnoopID];
            }
            else
            {
                eindKnoop = new Knoop(eindKnoopID, puntenList[puntenList.Count-1]); //end of index fix
                resultaat.KnoopenDictionary.Add(eindKnoopID, eindKnoop); // EINDKNOOP FIX

            }
            if (resultaat.SegmentenDictionary.ContainsKey(wegSegId))
            {
                wegSegment = resultaat.SegmentenDictionary[wegSegId];
            }
            else
            {
                wegSegment = new Segment(wegSegId, beginKnoop, eindKnoop, puntenList);
                resultaat.SegmentenDictionary.Add(wegSegId, wegSegment);
            }

            if (!graaf.KnoopSegmenten.ContainsKey(beginKnoop))
            {
                graaf.KnoopSegmenten.Add(beginKnoop,new List<Segment>());
            }

            var segList = graaf.KnoopSegmenten[beginKnoop];
            if(!segList.Any(s => s.SegmentId == wegSegment.SegmentId))
            {
                segList.Add(wegSegment);
            }

        }
        public static Straat StraatHelper(SourceHelper source, DataModel resultaat, int straatNaamId)
        {
            if (resultaat.StraatenDictionary.ContainsKey(straatNaamId))
            {
                return resultaat.StraatenDictionary[straatNaamId]; // ok
            }
            if (!source.WrGeementeIdDictionary.ContainsKey(straatNaamId))
            {
                return null;
            }
            var gemeenteId = source.WrGeementeIdDictionary[straatNaamId].gemeenteId;
            if (!resultaat.GemeenteDictionary.ContainsKey(gemeenteId))
            {
                return null;
            }
            var straat = new Straat(straatNaamId, source.wrStraatNamenLijst[straatNaamId].straatNaam, new Graaf(nextGraafID()), resultaat.GemeenteDictionary[gemeenteId]);

            straat.Graaf.KnoopSegmenten = new Dictionary<Knoop, List<Segment>>();
            resultaat.GraafsDictionary.Add(straat.Graaf.GraafId,straat.Graaf);
            resultaat.StraatenDictionary.Add(straatNaamId, straat);
            return straat;
        }


    }
}
