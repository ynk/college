using Extentie.dbStructuur;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Extentie.Handlers.FileHandling
{
    public static class Rapport
    {
        public static void RapportGenereeren(DataModel resultaat)
        {

            Console.Write("[Rapport] Generating ");
            string fileName = "rapport.txt";

            using (var writer = File.CreateText(fileName))
            {
                writer.WriteLine($"StraatenDictionary = {resultaat.StraatenDictionary.Count}");
                writer.WriteLine($"GemeenteDictionary = {resultaat.GemeenteDictionary.Count}");
                writer.WriteLine($"ProvinciesDictionary = {resultaat.ProvinciesDictionary.Count}");
                writer.WriteLine();

                foreach (var provincie in resultaat.ProvinciesDictionary.Values)
                {

                    var straatCounter = resultaat.StraatenDictionary.Values
                        .Where(x => x.Gemeente.Provincie.ProvincieId == provincie.ProvincieId).Count();
                    writer.WriteLine($"{provincie.ProvincieNaam} : {straatCounter}");
                    //  Console.WriteLine($"{provincie.ProvincieNaam} : {straatCounter}");

                }
                writer.WriteLine();
                foreach (var provincie in resultaat.ProvinciesDictionary.Values)
                {
                    writer.WriteLine();

                    var gemeenteLijst = resultaat.GemeenteDictionary.Values
                        .Where(x => x.Provincie.ProvincieId == provincie.ProvincieId).OrderBy(x => x.GemeenteNaam);
                    writer.WriteLine($"Provincie = {provincie.ProvincieNaam}");
                    writer.WriteLine();
                    foreach (var gemeente in gemeenteLijst)
                    {


                        var straatInGemeente =
                            resultaat.StraatenDictionary.Values.Where(x =>
                                x.Gemeente.GemeenteId == gemeente.GemeenteId).OrderBy(x => x.StraatNaam);



                        var totaal = from straat in straatInGemeente orderby StraatMath.getLengthStraat(straat) select straat;
                        var sum = totaal.Sum(x => StraatMath.getLengthStraat(x));

                        writer.WriteLine($"- {gemeente.GemeenteNaam} -  Straten: {straatInGemeente.Count()}  - LengteAlleStraaten = {Math.Round(sum,2)}m ");


                        var orderByResult = from straat in straatInGemeente orderby StraatMath.getLengthStraat(straat) select straat;

                        if (orderByResult == null) { continue; }

                        var korsteStraat = orderByResult.FirstOrDefault();
                        var LangsteStraat = orderByResult.LastOrDefault();
                        if (korsteStraat == null)
                        {
                            continue;
                        }
                        else
                        {
                            writer.WriteLine($"     - {korsteStraat.StraatId} {korsteStraat.StraatNaam} Lengte: {Math.Round(StraatMath.getLengthStraat(orderByResult.FirstOrDefault()),2)}m ");
                        }

                        if (LangsteStraat == null)
                        {
                            continue;

                        }
                        else
                        {
                            writer.WriteLine($"     - {LangsteStraat.StraatId} {LangsteStraat.StraatNaam} Lengte: {Math.Round(StraatMath.getLengthStraat(orderByResult.LastOrDefault()),2)}m ");

                        }

                    }


                }
            }


            Console.WriteLine(" |  Done.");
        }
    }
}
