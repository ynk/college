using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Prog4_Hogent_Apen.Klassen;
using Prog4_Hogent_Apen.Utils;
using Programmeren4_Hogent_Apen.Klassen;
using Programmeren4_Hogent_Apen.Utils;

namespace Programmeren4_Hogent_Apen
{
    class Program
    {
        public static string[] Names = { "Yannick", "Tim", "Zakery", "Mike", "Micheal", "Jo", "Kim", "Bryan", "Guido", "Hilde", "Sona", "Zyra", "Taric", "Igor", "Vlad", "Zoe", "Christana" };
        public static Random r = new Random();
       // public static int MAX_TREE = r.Next(250,1000);
        public static List<Map> playAbleMaps = new List<Map>();
        static async Task Main(string[] args)
        {

            Database.Reset.dbReset();


            Thread t1 = new Thread(() => Exectue(0));
            t1.Start();
            Thread t2 = new Thread(() => Exectue(1));
            t2.Start();

            t2.Join();
            t1.Join();

            await Log.SaveAll(playAbleMaps);

            Console.WriteLine();

            

        }

        public static void Exectue(int id)
        {
            int MAX_TREE = r.Next(250, 1000);
            var mapGen =  Task.Run(() => Generate(id, MAX_TREE));
            Task.WaitAll(mapGen);

        }

        public static async Task Generate(int ID, int MAX_TREE)
        {

            string[] Names = { "Yannick", "Tim", "Zakery", "Mike", "Micheal", "Jo", "Kim", "Bryan", "Guido", "Hilde" };

            Map playground = new Map();
            playground.TreeList = new List<Tree>();
            //setup
            playground.id = ID;
            playground.xMin = 0; 
            playground.xMax = 1000;
            playground.yMin = 0;
            playground.yMax = 1000;
            var monkeyGen = new List<Monkey>();

            Provider ds = new Provider();
            Drawing dw = new Drawing();
            var forest = await ds.generateGround(MAX_TREE, playground);

            var apestogen = r.Next(2, Names.Length);
            var count = 0;
            while (count != apestogen)
            {
                Monkey aapje = new Monkey();
                aapje.Name = Names[r.Next(0, Names.Length)];
                aapje.Kleur = (Color.FromArgb(r.Next(256), r.Next(256), r.Next(256)));
                aapje.id = count;
                // check 
                if (!monkeyGen.Contains(aapje))
                {
                    aapje.CurrentTree = forest.TreeList[r.Next(0, MAX_TREE)];
                    monkeyGen.Add(aapje);
                    count++;
                }
                else
                {
                    Console.WriteLine($"[!] Monkey Already exists");
                }
                Console.WriteLine($"(Forest={ID}) Created -> {aapje.Name} |x='{aapje.CurrentTree.X}'y={aapje.CurrentTree.Y}");
            }


            forest.MonkeyList = monkeyGen;


            playAbleMaps.Add(forest);
            

        }

        public static async Task <List<Monkey>>generateMonkey(Map forest)
        {
            var monkeyGen = new List<Monkey>();

            for (int i = 0; i < r.Next(4, 6); i++)
            {
                Monkey aapje = new Monkey();
                aapje.Name = Names[r.Next(0, Names.Length)];
                aapje.Kleur = (Color.FromArgb(r.Next(256), r.Next(256), r.Next(256)));
                aapje.id = i;
                // check 
                aapje.CurrentTree = forest.TreeList[r.Next(0, forest.TreeList.Count-1)];
                monkeyGen.Add(aapje);
                Console.WriteLine($"(Forest={forest.id}) Created -> {aapje.Name} |x='{aapje.CurrentTree.X}'y={aapje.CurrentTree.Y}");
            }

            return monkeyGen;

        }


    }
}
