using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using Prog4_Hogent_Apen.Klassen;

namespace Programmeren4_Hogent_Apen.Utils
{
    public class Drawing
    {

        public async static Task DrawBitmap(Map forest)
        {
            Utils.Log.LogLine($"write bitmap routes wood : {forest.id} - start");
            var path = GenBitmap(forest);
            path.Save($@"{forest.id}_escaperoutes.jpg",ImageFormat.Jpeg);
            Utils.Log.LogLine($"wrote bitmap routes for forest({forest.id})");

        }
        public static Bitmap GenBitmap(Map forest)
        {
            Bitmap bm = new Bitmap((forest.xMax - forest.xMin), (forest.yMax - forest.yMin));
            Graphics g = Graphics.FromImage(bm);
            Pen p = new Pen(Color.Chartreuse, 2);
            foreach (var tree in forest.TreeList)
            {
                g.DrawEllipse(p, tree.X, tree.Y, 2, 3);
            }

            foreach (var monkey in forest.MonkeyList)
            {
                Pen p1 = new Pen(monkey.Kleur, 2);
                g.DrawEllipse(p1, monkey.visitedTrees[0].X, monkey.visitedTrees[0].Y, 2, 2);
                for (int i = 0; i < monkey.visitedTrees.Count - 1; i++)
                {

                    g.DrawLine(p1, monkey.visitedTrees[i].X, monkey.visitedTrees[i].Y, monkey.visitedTrees[i + 1].X, monkey.visitedTrees[i + 1].Y);
                }
            }

            return bm;
        }


    }
}
