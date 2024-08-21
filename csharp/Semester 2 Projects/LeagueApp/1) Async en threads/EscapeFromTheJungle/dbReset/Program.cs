using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbReset
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reset database");
            Programmeren4_Hogent_Apen.Database.Reset.dbReset();
         
        }
    }
}
