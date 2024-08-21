using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prog4_Hogent_Apen.dbUtils;

namespace Programmeren4_Hogent_Apen.Database
{
    public class Reset
    {


        public static void dbReset()
        {
            SqlConnection dbConnection = new SqlConnection(Connection.connectionString);
            dbConnection.Open();
            var deleteStrings = new string[]

            {
                "delete from dbo.Log",
                "delete from dbo.MonkeyRecords",
                "delete from dbo.WoodsRecords",
            };

            for (int i = 0; i < deleteStrings.Length; i++)
            {
                SqlCommand command = new SqlCommand(deleteStrings[i], dbConnection);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Debug - ERROR:");
                    Console.WriteLine(e);

                }

            }
            dbConnection.Close();

        }

        public static void resetOutputFolder()
        {

          var di = new DirectoryInfo(Directory.GetCurrentDirectory());
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

            }


    }
}