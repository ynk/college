using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI
{
    public static class Logger
    {

        public static void Log(string msg)
        {
            Console.WriteLine($"({DateTime.UtcNow}) - {msg.Trim()}");
        }
    }
}
