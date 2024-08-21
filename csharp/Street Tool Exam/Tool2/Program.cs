using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using Extentie.Handlers.dbHanlder;
using dbImporter = Extentie.Handlers.dbHanlder.dbImporter;
namespace Tool2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Run eerst tool1.
            string output = @"c:\output";


            if (!Directory.Exists(@"c:\output"))
            {
                Console.WriteLine("Output folder van tool1 iet gevonden \nRun Eerst Tool1!");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Reseting...");
                // We gaan er vaan uit dat er data in onze databank zit dus we gaan dat er eerst uithalen. :)
                Extentie.Handlers.dbHanlder.dbImporter.Reset();
                
                foreach (var file in Directory.GetFiles("c:\\output"))
                {
                    
                    string fileName = file.Remove(0, 10);
                        dbImporter.Reader(fileName);
                }

                
            }

            Console.WriteLine("done");
            Console.ReadLine();

        }
    }
}
