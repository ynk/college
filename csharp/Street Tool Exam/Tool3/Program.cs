using System;
using Microsoft.VisualBasic;
using dbQuerryHandler =  Extentie.Handlers.dbHanlder.dbQuerryHandler;
namespace Tool3
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Menu();// render het menu
                
            }
        }

        public static void getStraatId()
        {
            Console.Clear();

            Console.Write("Geef een straatID in >");
            string id = Console.ReadLine();
            //  dbGetter.selectStraat(id);
           dbQuerryHandler.selectStraat(id);
        }

        public static void findStraatByNaam()
        {
            Console.Clear();
            Console.Write("Geef straatnaam In (Niet hoofdletter gevoelig) > ");
            string straatNaam = Console.ReadLine();
            Console.WriteLine();
            //  dbGetter.findStraat(straatNaam);
            dbQuerryHandler.findStraat(straatNaam);
        }

        public static void getStraatenPerGemeente()
        {
            Console.Clear();
            Console.Write("Geef een Gemeente in > ");
            string gemeenteNaam = Console.ReadLine();
            Console.WriteLine();
           // dbGetter.getStraatenPerGemeente(gemeenteNaam);
          dbQuerryHandler.getStraatenPerGemeente(gemeenteNaam);
        }

        public static void getOpdracht()
        {
            Console.Clear();
            Console.Write("StraatId? (0 = abort) > ");

            var cReadLine = Console.ReadLine();
            var number = int.Parse(cReadLine.ToString());
            dbQuerryHandler.getOpdracht(number);
        }

        public static void wildcardStraat()
        {
            Console.Clear();
            Console.Write("Vul een deel van de straat in en duuw op enter (0 to abort) >  ");

            var cReadLine = Console.ReadLine();
            if (cReadLine == "0")
            {
                Menu();
                
            }

           dbQuerryHandler.wildcardGetStraat(cReadLine);
        }

        public static void Menu()
        {
            Console.Clear();
            
            int number = 0;
            Console.WriteLine("###################################################");
            Console.WriteLine("# 1) Zoek Straat Door ID                          #");
            Console.WriteLine("# 2) Zoek alleStraaten door naam                  #");
            Console.WriteLine("# 3) Print alle Provincies                        #");
            Console.WriteLine("# 4) Print alle gemeenten                         #");
    //        Console.WriteLine("# 5) Print alle Gemeentens Per Provincie          #");
            Console.WriteLine("# 6) Print alle straaten Per Gemeenten            #");
            Console.WriteLine("# 7) Opdracht Querry                              #");
     //       Console.WriteLine("# 8) Wildcard straat en toon segment              #");
            Console.WriteLine("###################################################");
   //         Console.WriteLine("# Als je uit een functie will stappen type 0      #");
    //        Console.WriteLine("###################################################");
            Console.Write("Type Nummer: ");
            var inputLine = Console.ReadLine();
            number = int.Parse(inputLine.ToString());


            switch (number)
            {
                case 1:
                    getStraatId(); // Zoek straat door ID
                    break;
                case 2:
                    findStraatByNaam(); // Zoek alle straaten
                    break;
                case 3:
                    //dbGetter.findProvincies();
                    dbQuerryHandler.findProvincies();
                    break;
                case 4:
                    Console.Clear();
                    //dbGetter.findAllGemeenten();
                    dbQuerryHandler.findAllGemeenten();
                    break;
                case 5:
                    Console.Clear();
                 //   dbGetter.getGemeentenPerProvincie();
                    dbQuerryHandler.getGemeentenPerProvincie();
                    break;
                case 6:
                    getStraatenPerGemeente();
                    break;
                case 7:
                    getOpdracht();
                    break;

                case 8:
                    wildcardStraat();
                    break;
            }
        }

    }


}
