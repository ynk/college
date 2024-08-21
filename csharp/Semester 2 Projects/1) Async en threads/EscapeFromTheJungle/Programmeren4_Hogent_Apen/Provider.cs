using Prog4_Hogent_Apen.Klassen;
using Prog4_Hogent_Apen.Utils;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using Programmeren4_Hogent_Apen.Utils;

namespace Programmeren4_Hogent_Apen.Klassen
{
    public  class Provider
    {
        public async Task<Map> generateGround(int amountOfTreesToGen, Map groundMap)
            // public static Map generateGround(int amountOfTreesToGen, Map groundMap)
        {

            Random r = new Random(); // :)
            int good_run = 0;
            while (good_run != amountOfTreesToGen)
            {
                Tree newTree = new Tree();
                newTree.X = r.Next(groundMap.xMin, groundMap.xMax);
                newTree.Y = r.Next(groundMap.yMin, groundMap.yMax);
                newTree.bosId = groundMap.id;
                newTree.TreeID = good_run;

                if (!groundMap.TreeList.Any(x => x.X == newTree.X && x.Y == newTree.Y))
                {

                    groundMap.TreeList.Add(newTree);
                    good_run++; // boom is goed aangemaakt

                    //sendToDb
                    Database.Insert.InsertForestTree(newTree);
                }
                else
                {

                    Console.WriteLine($"DUPLICATE FOUND! NOT ADDING:x={newTree.X}y={newTree.Y}");
                    // While loop gaat een nieuwe Boom maken

                }

            }

            Console.WriteLine($"Map:{groundMap.id} Generated: xMin{groundMap.xMin}, xMax{groundMap.xMax} with {good_run} trees!");
            return groundMap;
        }

        public async static Task EscapTask(Map ground)
        {
            Utils.Log.toFile(ground.id, "");
            // Log start :)
            int step = 0;
            bool ApesHaveEscaped = false;
            while (ApesHaveEscaped == false)
            {
                var TaskListForDb = new List<Task>();
                step++;
                //     Console.Write("hit while");//ok
                foreach (var monkey in ground.MonkeyList.Where(x => x.CurrentTree != null))
                {
                    
                    //    Console.WriteLine($"(map:{ground.id}) {monekey.Name} step: {step} jumped from tree {monekey.CurrentTree.TreeID}");
                    var tree = getClosedTree(ground, monkey);
                    Database.Insert.InsertLog(ground, monkey, $"{monkey.Name} is now in tree {monkey.CurrentTree.TreeID} at location ({monkey.CurrentTree.X},{monkey.CurrentTree.Y})");
                    TaskListForDb.Add(Database.Insert.InsertMonkey(ground, monkey, step));
                    if (tree != null)
                    {
                        monkey.visitedTrees.Add(tree);
                        tree.MonkeyInTree = monkey;

                    }
                    

                    Utils.Log.toFile(ground.id, $"step:{step} {monkey.Name} is in {monkey.CurrentTree.TreeID} at ({monkey.CurrentTree.X},{monkey.CurrentTree.Y})");
                    if (monkey.CurrentTree == null)
                    {
                        Utils.Log.LogLine($"(forest:{ground.id}) {monkey.Name} jumped outside and has escaped! in {step}");

                    }

                    {
                        Utils.Log.LogLine($"forest:{ground.id}) {monkey.Name} step:{step} jumped {monkey.CurrentTree.TreeID}");
                    }
                    monkey.CurrentTree.MonkeyInTree = null;
                    monkey.CurrentTree = tree;

                   
                }

                await Task.WhenAll(TaskListForDb);
                ApesHaveEscaped = ground.MonkeyList.All(x => x.CurrentTree == null);
            }
            Console.WriteLine($"All the apes({ground.MonkeyList.Count}) have escaped on map: {ground.id}");
        }

        public static Tree getClosedTree(Map ground, Monkey monkey)
        {
            List<Tree> allTreesAvaible = ground.TreeList.Where(x => monkey.visitedTrees.All(t => t.TreeID != x.TreeID))
                .Where(o => o.MonkeyInTree == null).ToList();

            double shortestDistance = 99999999; // Setup values that we will overwrite 
            Tree closedTree = null; // Setup values
            foreach (var tree in allTreesAvaible)
            {
                var distanceToTree = tree.DistanceTo(monkey.CurrentTree);
                if (distanceToTree < shortestDistance)
                {
                    shortestDistance = distanceToTree; // overwrite our distance
                    closedTree = tree; // same thing here
                }
            }
            if (Distance.getDistanceToBorder(ground, monkey.CurrentTree) < shortestDistance)
            {
                Console.WriteLine($"{monkey.Name} has escaped! (map:{ground.id})");
                closedTree = null; // escaped
            }
            return closedTree;
        }

    }
}
