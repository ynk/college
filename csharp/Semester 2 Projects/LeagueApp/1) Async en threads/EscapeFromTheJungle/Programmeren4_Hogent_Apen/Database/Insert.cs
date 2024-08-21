using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prog4_Hogent_Apen.dbUtils;
using Prog4_Hogent_Apen.Klassen;

namespace Programmeren4_Hogent_Apen.Database
{
    public class Insert
    {

        public static void InsertForestTree(Tree tree)
        {
           //Console.WriteLine($"[DB] Sending Tree: {tree.TreeID} | ({tree.bosId} {tree.TreeID} {tree.X} {tree.Y})");


            string Querry = @"insert into dbo.WoodsRecords(woodID,treeID,x,y) values (@woodId,@treeId,@x,@y); ";
            
            using (var cn = new SqlConnection(Connection.connectionString))
            {
                SqlCommand con1 = new SqlCommand(Querry, cn); 
                con1.Parameters.AddWithValue("@woodId", tree.bosId);
                con1.Parameters.AddWithValue("@treeId",tree.TreeID);
                con1.Parameters.AddWithValue("@x", tree.X);
                con1.Parameters.AddWithValue("@y", tree.Y);
                cn.Open();
                con1.ExecuteNonQuery();
                cn.Close();
            }



            //    Console.WriteLine($"[DB] Should be done {tree.treeId}");
        }
        public async static Task InsertMonkey(Map ground,Monkey aapje,int step)
        {
           //  Console.WriteLine($"[DB] Sending Path: {ground.id} | ({aapje.Name} {aapje.CurrentTree.X} {aapje.CurrentTree.Y})");
             string Querry = @"insert into dbo.MonkeyRecords(monkeyID,monkeyName,woodID,seqnr,x,y) values (@monkeyid,@monkeyName,@woodId,@seqNr,@x,@y);";
             using (var cn = new SqlConnection(Connection.connectionString))
             {

                 SqlCommand con = new SqlCommand(Querry, cn);
                con.Parameters.AddWithValue("@monkeyid", aapje.id);
                 con.Parameters.AddWithValue("@monkeyName", aapje.Name);
                 con.Parameters.AddWithValue("@woodId", ground.id);
                 con.Parameters.AddWithValue("@seqNr", step);
                 con.Parameters.AddWithValue("@x", aapje.CurrentTree.X);
                 con.Parameters.AddWithValue("@y", aapje.CurrentTree.Y);
                 cn.Open();
                 con.ExecuteNonQuery();
                 cn.Close();
             }

             //    Console.WriteLine($"[DB] Should be done {tree.treeId}");
        }
        public  static void InsertLog(Map ground, Monkey aapje, String Msg)
        {
   //        Console.WriteLine($"inserting into log.. {ground.id} {aapje.Name} {Msg}");
            string Querry = @"insert into dbo.Log(woodID,monkeyID,message) values (@woodID,@monkeyID,@message);";
            using (var cn = new SqlConnection(Connection.connectionString))
            {

                SqlCommand con = new SqlCommand(Querry, cn);
                con.Parameters.AddWithValue("@woodID", ground.id);
                con.Parameters.AddWithValue("@monkeyID", aapje.id);
                con.Parameters.AddWithValue("@message", Msg);
                cn.Open();
                con.ExecuteNonQuery();
                cn.Close();
            }

        }

    }
}
