using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Extentie.dbStructuur;
using Extentie.Structuur;
using Extentie;
using Extentie.Handlers.FileHandling;
using Tool1;
using Writer = Extentie.Handlers.FileHandling.Writing; // diy naar extenie
using Reader = Extentie.Handlers.FileHandling.Reader;

//using Console = System.Console;

namespace Eindwerk
{
    class Program
    {
        static void Main(string[] args)
        {
            //Inlezen
            var unparseData = getData();

            var parsedData = Provider.Read(unparseData);

            if (!Directory.Exists(@"C:\\output"))
            {
                Console.WriteLine("saving");
                Directory.CreateDirectory(@"c:\\output");
                Save(parsedData);
                
            }
            else
            {
  
                Save(parsedData);
            }

          Rapport.RapportGenereeren(parsedData);
#if DEBUG
#endif

            Console.WriteLine("end of program");
            Console.ReadLine();
        }
        


        public static void Save(DataModel resultaat)
        {
            
            //Writer.writeKnoopenTEMP(resultaat.KnoopenDictionary);
            Writing.writeGraaf(resultaat.GraafsDictionary);
            Writing.writeProvincie(resultaat.ProvinciesDictionary);
            Writing.writeStraaten(resultaat.StraatenDictionary);
            Writing.writeGemeenten(resultaat.GemeenteDictionary);
            Writing.writeGrafen(resultaat.GraafsDictionary);
            Writing.writeKnopen(resultaat.KnoopenDictionary);
            Writing.writeSegmeten(resultaat.SegmentenDictionary);
            Writing.writeGraafKnoop(resultaat.GraafsDictionary);
       
        }

        public static SourceHelper getData()
        { // DIT IS ALLE DATA VOOR WE ER IETS MEE DOEN  
            SourceHelper data = new SourceHelper();
            Console.WriteLine(" ");
            Console.Write("[Reading] Provinces ");
            data.ProvincieInfoList = Reader.getProvinceInfo();
            Console.WriteLine($"           |  Done: {data.ProvincieInfoList.Count} found");
            Console.Write("[Reading] Gemeentes ");
            data.WrGemeenteNaamlList = Reader.WRGemeenteNaam();
            Console.WriteLine($"           |  Done: {data.WrGemeenteNaamlList.Count} found");
            Console.Write("[Reading] Straaten");
            data.wrStraatNamenLijst = Reader.getWRstraatnamen();
            Console.WriteLine($"             |  Done: {data.wrStraatNamenLijst.Count} found");
            Console.Write("[Reading] alleGemeentenMetId");
            data.WrGeementeIdDictionary = Reader.getWRGemeneteNaamPerStraat();
            Console.WriteLine($"   |  Done: {data.WrGeementeIdDictionary.Count} found");
            Console.Write("[Reading] wrData(big)");
            data.wrDataList = Reader.getWrData();
            Console.WriteLine($"          |  Done: {data.wrDataList.Count} found");
            Console.WriteLine(" ");

            return data;
        }






    }
}
