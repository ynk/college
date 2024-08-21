using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Extentie.Structuur;
using Extentie.Handlers;

namespace Extentie.Handlers.FileHandling
{
    public static class Writing
    {
        /*csvHelper trok op niks voor mappers te maken,todo: removed unused code in extentie, fastcsv
        https://www.codeproject.com/Articles/5255318/fastCSV-Tiny-Fast-Standard-Compliant-CSV-Reader-Wr
        /*

        public static void writeKnoopenTEMP(Dictionary<int, Knoop> knoop)
        {
            var writer = new StreamWriter(@"c:\\output\\temp\\knoop.csv");
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                csv.WriteRecords(knoop.Select(x => x.Value).ToString());
            }

        }
        */

        public static void writeProvincie(Dictionary<int, Provincie> provincies)
        {
            var writer = new StreamWriter(@"c:\\output\\Provincies.csv");
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                csv.WriteRecords(provincies.Select(x => x.Value).ToList());
            }

        }

        public static void writeGemeenten(Dictionary<int, Gemeente> gemeentes)
        {
            /*
            var writer = new StreamWriter(@"c:\\output\\Gemeentes.csv");
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
              
                csv.WriteRecords(gemeentes.Select(x => x.Value).ToList());
            }
            */
            fastCSV.WriteFile<Gemeente>
            (
                "C:\\output\\Gemeenten.csv",
                new[] { "GemeenteId", "ProvincieId", "GemeenteNaam" },
                ';',
                gemeentes.Select(s => s.Value).ToList(),
                (gemeente, csv) =>
                {
                    csv.Add(gemeente.GemeenteId);
                    csv.Add(gemeente.Provincie.ProvincieId);
                    csv.Add(gemeente.GemeenteNaam);

                    //
                }


            );


        }

        public static void writeStraaten(Dictionary<int, Straat> straaten)
        {
            fastCSV.WriteFile<Straat>
            (
                "C:\\output\\Straaten.csv",
                new[] { "StraatId", "StraatNaam", "GraafId", "GemeenteId", "StraatLengte" },
                ';',
                straaten.Select(s => s.Value).ToList(),
                (straat, csv) =>
                {
                    csv.Add(straat.StraatId);
                    csv.Add(straat.StraatNaam.Trim());
                    csv.Add(straat.Graaf.GraafId);
                    csv.Add(straat.Gemeente.GemeenteId);
                    csv.Add(System.Math.Round(StraatMath.getLengthStraat(straat), 2));

                }


            );
        }
        
        public static void writeSegmeten(Dictionary<int, Segment> segmenten)
        {
            fastCSV.WriteFile(
                "c:\\output\\Segmenten.csv",
                new[] { "SegmentId", "BeginKnoopId", "EindKnoopId", "PuntenLijst" },
                ';',
                segmenten.Select(x => x.Value).ToList(),
                (segment, columns) =>
                {
                    columns.Add(segment.SegmentId);
                    columns.Add(segment.BeginKnoop.KnoopId);
                    columns.Add(segment.EindKnoop.KnoopId);
                    columns.Add(String.Join(" ", segment.Punten)); //TOSTRING FIXXXXXX
                }



                );

        }

        public static void writeGraaf(Dictionary<int, Graaf> graaf)
        {
            fastCSV.WriteFile(
                "c:\\output\\Graven.csv",
                new string[] { "GraafId" },
                ';',
                graaf.Select(s => s.Value).ToList(),
                (tempData, colum) =>
                {
                    colum.Add(tempData.GraafId);

                }
            );


        }
        public static void writeGraafKnoop(Dictionary<int, Graaf> graven)
        {
            var knoop = new List<(int graafId, int knoopId)>();

            foreach (var cur_graaf in graven)
            {
                knoop.AddRange(cur_graaf.Value.KnoopSegmenten.Select(knoopSegment => (cur_graaf.Value.GraafId, knoopSegment.Key.KnoopId)));
            }

            fastCSV.WriteFile<(int graafId, int knoopId)>(
                "c:\\output\\GraafKnopen.csv",
                new string[] { "GraafId", "KnoopId" },
                ';',
                knoop,
                (tempData, colum) =>
                {
                    colum.Add(tempData.graafId);
                    colum.Add(tempData.knoopId);

                }
            );

        }
        public static void writeGrafen(Dictionary<int, Graaf> graafs)
        {
            var temp = new List<(int GraafId, int KnoopId, int segmentId)>();

            foreach (var graaf in graafs)
            {
                foreach (var knoop in graaf.Value.KnoopSegmenten)
                {
                    temp.AddRange(knoop.Value.Select(seg => (graaf.Value.GraafId, knoop.Key.KnoopId, seg.SegmentId)));
                }


                /*var writer = new StreamWriter(@"c:\\output\\GraafKnoopen.csv");
                using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                {
                    csv.WriteRecords(temp.Select(x => (x.GraafId, x.KnoopId)).ToList());
                }
              */

            }
            fastCSV.WriteFile<(int graafId, int knoopId, int SegmentId)>(
                "c:\\output\\GraafKnoopSegment.csv",
                new string[] { "GraafId", "KnoopId", "SegmentId" },
                ';',
                temp,
                (tempData, colum) =>
                {
                    colum.Add(tempData.graafId);
                    colum.Add(tempData.knoopId);
                    colum.Add(tempData.SegmentId);
                }
            );
        }

        public static void writeKnopen(Dictionary<int, Knoop> knoops)
        {

            fastCSV.WriteFile<Knoop>(
                "c:\\output\\Knopen.csv",
                new string[] { "KnoopId", "Punt" },
                ';',
                knoops.Select(x => x.Value).ToList(),
                (knoop, column) =>
                {
                    column.Add(knoop.KnoopId);
                    column.Add(knoop.Punt);

                });
        }


    }
}
