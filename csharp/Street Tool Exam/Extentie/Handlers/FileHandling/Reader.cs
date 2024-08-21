using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Extentie.inputStructuur;

namespace Extentie.Handlers.FileHandling
{
    public static class Reader
    {

        public static char Seperator = ';'; // Global Seperator



        public static Dictionary<int, WrStraatNamen> getWRstraatnamen()
        {
            Dictionary<int, WrStraatNamen> wrStraatData = new Dictionary<int, WrStraatNamen>();
            var WRstraatnamen_reader =
                new StreamReader(
                    File.OpenRead(@"C:\Users\ynk\source\repos\Eindwerk\csvBestanden\WRstraatnamen.csv")); // Lokaal
            while (!WRstraatnamen_reader.EndOfStream)
            {

                WRstraatnamen_reader.ReadLine();
                WRstraatnamen_reader.ReadLine();
                while (WRstraatnamen_reader.Peek() != -1)
                {
                    string line = WRstraatnamen_reader.ReadLine();
                    var split = line.Split(Seperator);
                    int straatId = int.Parse(split[0]);
                    string straatNaam = split[1].Trim();
                    wrStraatData.Add(straatId, new WrStraatNamen(straatId, straatNaam));
                }

            }

            return wrStraatData;
        }

        public static List<WrGemeenteNaam> WRGemeenteNaam()
        {
            List<WrGemeenteNaam> gemeenteNaamen = new List<WrGemeenteNaam>();
            var getWrGemeentenaam_reader =
                new StreamReader(File.OpenRead(@"C:\Users\ynk\source\repos\Eindwerk\csvBestanden\WRGemeentenaam.csv"));
            while (!getWrGemeentenaam_reader.EndOfStream)
            {
                getWrGemeentenaam_reader.ReadLine();
                while (getWrGemeentenaam_reader.Peek() != -1) // als het null is stopt hij
                {

                    string line = getWrGemeentenaam_reader.ReadLine(); // line text
                    var split = line.Split(Seperator); // split line op ;
                    int gemeenteNaamId = int.Parse(split[0]); // WORDT NIET GEBRUIKT!
                    int gemeenteId = int.Parse(split[1]);
                    string taalCodeGemeenteNaam = split[2];
                    string gemeenteNaam = split[3];

                    if (taalCodeGemeenteNaam == "nl")
                    {
                        gemeenteNaamen.Add(new WrGemeenteNaam(gemeenteNaamId, gemeenteId, taalCodeGemeenteNaam,
                            gemeenteNaam));
                    }
                }
            }
            return gemeenteNaamen;
        }

        public static Dictionary<int, WrGemeenteID> getWRGemeneteNaamPerStraat()
        {
            Dictionary<int, WrGemeenteID> wrGemeenteData = new Dictionary<int, WrGemeenteID>();
            var WRGemeenteID_reader =
                new StreamReader(File.OpenRead(@"C:\Users\ynk\source\repos\Eindwerk\csvBestanden\WRGemeenteID.csv"));

            while (!WRGemeenteID_reader.EndOfStream)
            {
                WRGemeenteID_reader.ReadLine();
                while (WRGemeenteID_reader.Peek() != -1) // als het null is stopt hij
                {
                    string line = WRGemeenteID_reader.ReadLine(); // line text
                    var split = line.Split(Seperator); // split line op ;
                    int straatNaamId = int.Parse(split[0]);
                    int gemeenteId = int.Parse(split[1]);

                    wrGemeenteData.Add(straatNaamId, new WrGemeenteID(straatNaamId, gemeenteId));


                }
            }

            return wrGemeenteData;
        }

        public static List<WrData> getWrData()
        {
            //10LINES: WRdata10_small.csv
            List<WrData> wrDataList = new List<WrData>();
            int a = 0;
            var wrDataReader =
                new StreamReader(
                    File.OpenRead(@"C:\Users\ynk\Downloads\WRdata-master\WRdata-master\WRdata\WRdata.csv"));
            while (!wrDataReader.EndOfStream)
            {

                wrDataReader.ReadLine();
                while (wrDataReader.Peek() != -1) // als het null is stopt hij
                {
                    string line = wrDataReader.ReadLine(); // line text
                    var splitted = line.Split(Seperator);
                    if (splitted[6] == "-9" && splitted[7] == "-9")
                    {
                    }
                    else
                    {
                        // best arrayke van maken om er aan uit te kunnen
                        var wegsegmentID = int.Parse(splitted[0]);
                        var geo = splitted[1];
                        var morfologie = int.Parse(splitted[2]);
                        var status = int.Parse(splitted[3]);
                        var beginWegknoopID = int.Parse(splitted[4]);
                        var eindWegknoopID = int.Parse(splitted[5]);
                        //!!!
                        var linksStraatnaamID = int.Parse(splitted[6]);
                        var rechtsStraatnaamID = int.Parse(splitted[7]);
                        wrDataList.Add(new WrData(wegsegmentID, geo, morfologie, beginWegknoopID, eindWegknoopID, linksStraatnaamID, rechtsStraatnaamID));

                    }
                }
            }
            return wrDataList;
        }






        public static List<ProvincieInfo> getProvinceInfo()
        {
            List<ProvincieInfo> provincieInfos = new List<ProvincieInfo>();
            var ProvincieInfo_reader = new StreamReader(File.OpenRead(@"C:\Users\ynk\source\repos\Eindwerk\csvBestanden\ProvincieInfo.csv"));
            int lenght = File.ReadAllLines(@"C:\Users\ynk\source\repos\Eindwerk\csvBestanden\ProvincieInfo.csv").Count();
          //  int a = 0; // debug

            var provincieIds = getProvincieIDsVlaanderen(); // pakt alle ids van den andere functie

            while (!ProvincieInfo_reader.EndOfStream)
            {
                ProvincieInfo_reader.ReadLine();
                while (ProvincieInfo_reader.Peek() != -1) // als het null is stopt hij
                {
                 
                    string line = ProvincieInfo_reader.ReadLine(); // line text
                    var split = line.Split(Seperator); // split line op ;
                    int gemeenteId = int.Parse(split[0]);
                    int provincieId = int.Parse(split[1]);
                    string taalCodeProvincieNaam = split[2];
                    string provincieNaam = split[3];
                    if (provincieIds.Contains(provincieId))
                    {
                        if (taalCodeProvincieNaam == "nl")
                        {
                            //       Console.WriteLine($"{gemeenteId} {provincieId} {provincieNaam}");
                            provincieInfos.Add(new ProvincieInfo(gemeenteId, provincieId, provincieNaam));
                        }


                    }
                }
            }
            return provincieInfos;
        }

        public static int[] getProvincieIDsVlaanderen()
        {
            var ProvincieIDsVlaanderen_reader =
                new StreamReader(
                    File.OpenRead(@"C:\Users\ynk\source\repos\Eindwerk\csvBestanden\ProvincieIDsVlaanderen.csv"));
            string line = ProvincieIDsVlaanderen_reader.ReadLine(); // We moeten tog maar 1 lijn lezen
            var split = line.Split(',');
            int[] id = new int[split.Length];

            for (int i = 0; i < split.Length; i++)
            {
                id[i] = Convert.ToInt32(split[i]);
            }

            return id;
        }


    }
}
