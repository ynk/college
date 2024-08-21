using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using Prog4_Hogent_Apen.Klassen;
using Programmeren4_Hogent_Apen.Klassen;

namespace Programmeren4_Hogent_Apen.Utils
{
    public class Log
    {
        public static StringBuilder fullLog = new StringBuilder();

        public static void toFile(int id, string msg)
        {
            using (StreamWriter file =
                new StreamWriter($@"{id}_logFile.txt", true))
            {
                file.WriteLine(msg);
            }
        }

        public static void LogLine(string message)
        {
            fullLog.AppendLine(message);
        }

        public static void SaveLog(Map forest)
        {
            string path = $@"OutputLog.txt";
            using (StreamWriter write = new StreamWriter(path))
            {
                write.WriteLine(fullLog.ToString());
            }
        }

        public async static Task SaveAll(List<Map> forestList)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            Parallel.ForEach(forestList, (async (forest) =>
            {
                //Thread t1 = new Thread(() => writeWoodRecord(forest));
                Task t1 = writeWoodRecord(forest);
                await Provider.EscapTask(forest);
              //  Thread t2 = new Thread(() => Drawing.DrawBitmap(forest));
                Task t2 = Drawing.DrawBitmap(forest);
             //   Thread t3 = new Thread(() => writeFinalTask(forest));
              Task t3 = writeFinalTask(forest);
               await Task.WhenAll(t1, t2, t3);
             //  t3.Join();
              // t2.Join();
               //t1.Join();

            }));

            LogLine($"Time elapsed: {stopwatch.Elapsed}");
            stopwatch.Stop();
            LogLine($"End");
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
            Console.WriteLine("hit readline");
            Console.ReadLine();
        }

        private async static Task writeWoodRecord(Map forest)
        {
            LogLine($"write wood {forest.id} to database - start");
            foreach (var tree in forest.TreeList)
            {
                Database.Insert.InsertForestTree(tree);
            }

            LogLine($"write wood {forest.id} to database - end");
        }

        private async static Task writeFinalTask(Map forest)
        {
            LogLine($"wood : {forest.id} writes log - start");
            SaveLog(forest);
        }
    }
}
