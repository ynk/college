using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Extentie.Handlers.dbHanlder
{
    public static class dbQuerryHandler
    {

            public static SqlConnection globalConnection = new SqlConnection(Extentie.connection.connectionString);

    
            public static void selectStraat(string straatId)
            {
                globalConnection.Open();
                var selectFromDB = @$"select s.StraatId, s.StraatNaam,s.StraatLengte, g.GemeenteNaam, p.ProvincieNaam
                from Straaten s
                join Gemeenten g
                on s.GemeenteId = g.GemeenteId
                join Provincies p 
                on g.ProvincieId = p.ProvincieId
                where s.StraatId = @straatId;"; // straatID van parameters

                SqlCommand con = new SqlCommand(selectFromDB, globalConnection);
                con.Parameters.AddWithValue("@straatId", straatId);
                //https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlparametercollection.addwithvalue?view=netframework-4.8

                SqlDataReader reader = con.ExecuteReader();
                Console.WriteLine("### StraatId #### StraatNaam #### StraatLengte ### GemeenteNaam ### ProvincieNaam ####");
                if (reader.Read())
                {

                    Console.WriteLine($"## {reader["StraatId"]} | {reader["StraatNaam"]} | {reader["StraatLengte"]} | {reader["GemeenteNaam"]} | {reader["ProvincieNaam"]} ##");

                }
                Console.WriteLine("#################################################################################################");
                globalConnection.Close();
                Console.WriteLine("Enter om terug te gaan");
                Console.ReadLine();
            }

            public static void findStraat(string straatNaam)
            {
                globalConnection.Open();

                var searchDB = $@"select s.StraatId, s.StraatNaam, s.StraatLengte, g.GemeenteNaam,p.ProvincieNaam
            from Straaten s
            join Gemeenten g
            on s.GemeenteId = g.GemeenteId
            join Provincies p
            on g.ProvincieId = p.ProvincieId
            where s.StraatNaam = @straatNaam";
                //Console.WriteLine(straatNaam);
                SqlCommand con = new SqlCommand(searchDB, globalConnection);
                con.Parameters.AddWithValue("@straatNaam", straatNaam);
                SqlDataReader reader = con.ExecuteReader();
                int result = 0;
                Console.WriteLine("##### ROW #### StraatId #### StraatNaam #### StraatLengte ### GemeenteNaam ### ProvincieNaam ####");
                while (reader.Read())
                {
                    result++;
                    Console.WriteLine($"# {result} | {reader["StraatId"]} | {reader["StraatNaam"]}  | {reader["StraatLengte"]}m  | {reader["GemeenteNaam"]} | {reader["ProvincieNaam"]} #");

                }
                Console.WriteLine("#################################################################################################");
                globalConnection.Close();
                Console.WriteLine($"Gevonden resultaaten: {result}");
                Console.WriteLine("Enter om terug te gaan");
                Console.ReadLine();


            }

            public static void findProvincies()
            {
                globalConnection.Open();

                var searchDB = $@"select * from Provincies";
                SqlCommand con = new SqlCommand(searchDB, globalConnection);

                SqlDataReader reader = con.ExecuteReader();
                var response = reader.Read();
                int result = 0;
                while (reader.Read())
                {
                    result++;
                    Console.WriteLine($"{result} | {reader["ProvincieId"]} | {reader["ProvincieNaam"]}");

                }
                Console.WriteLine($"Gevonden resultaaten: {result}");
                globalConnection.Close();
                Console.WriteLine("Enter om terug te gaan");
                Console.ReadLine();
                Console.Clear();


            }

            public static void findAllGemeenten()
            {
                Console.WriteLine("Sending.....");
                globalConnection.Open();

                var searchDB = $@"select g.GemeenteId,g.GemeenteNaam,p.ProvincieNaam
from Gemeenten g
join Provincies p
on g.ProvincieId = p.ProvincieId
order by p.ProvincieNaam";
                SqlCommand con = new SqlCommand(searchDB, globalConnection);

                SqlDataReader reader = con.ExecuteReader();
                var response = reader.Read();
                int result = 0;
                Console.Clear();

                while (reader.Read())
                {
                    result++;
                    Console.WriteLine($"{result} | {reader["GemeenteId"]} | {reader["GemeenteNaam"]} | {reader["ProvincieNaam"]}");

                }
                Console.WriteLine($"Gevonden resultaaten: {result}");
                globalConnection.Close();
                Console.WriteLine("Enter om terug te gaan");
                Console.ReadLine();
            }

            public static void getGemeentenPerProvincie()
            {
                globalConnection.Open();

                var searchDB = $@"select * from Provincies";
                SqlCommand con = new SqlCommand(searchDB, globalConnection);

                SqlDataReader reader = con.ExecuteReader();

                List<int> allowedCodes = new List<int>();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ProvincieId"]} | {reader["ProvincieNaam"]}");
                    allowedCodes.Add(Int32.Parse(reader["ProvincieId"].ToString()));

                }
                globalConnection.Close();


                int number = 0;
                while (!allowedCodes.Contains(number))
                {
                    //Todo check
                    Console.WriteLine("Enter Provincie code: ");
                    number = Int32.Parse(Console.ReadLine());
                    //Console.WriteLine("TEST:"+ number);
                    if (allowedCodes.Contains(number))
                    {
                        // news sqlConnection
                        Console.WriteLine("OKAY");
                        SqlConnection tempSqlConnection = new SqlConnection(Extentie.connection.connectionString);
                        tempSqlConnection.Open();
                        var searchProByID = @"select g.GemeenteId,g.GemeenteNaam,p.ProvincieNaam
                from Gemeenten g
                join Provincies p
                on g.ProvincieId = p.ProvincieId
                where p.ProvincieId = @number order by g.GemeenteNaam";
                        SqlCommand con1 = new SqlCommand(searchProByID, tempSqlConnection);
                        con1.Parameters.AddWithValue("@number", number);
                        Console.WriteLine(con1.CommandText);
                        SqlDataReader reader1 = con1.ExecuteReader();
                        int result = 0;
                        while (reader1.Read())
                        {
                            Console.WriteLine($"{result} | {reader["GemeenteId"]} | {reader["GemeenteNaam"]} | {reader["ProvincieNaam"]}");

                        }
                        tempSqlConnection.Close();



                    }
                    else
                    {
                        Console.WriteLine("Nummer niet gevonden, Try again..");
                        number = Int32.Parse(Console.ReadLine());
                    }
                }
                globalConnection.Close();
                Console.WriteLine("done");
                Console.ReadLine();
                Console.Clear();

            }

            public static void wildcardGetStraat(string straatNaam)
            {
                globalConnection.Open();

                var searchDB = $@"select s.StraatId, s.StraatNaam, s.StraatLengte, g.GemeenteNaam,p.ProvincieNaam
            from Straaten s
            join Gemeenten g
            on s.GemeenteId = g.GemeenteId
            join Provincies p
            on g.ProvincieId = p.ProvincieId
            where s.StraatNaam LIKE '@straatNaam%'";
                //Console.WriteLine(straatNaam);
                SqlCommand con = new SqlCommand(searchDB, globalConnection);
                con.Parameters.AddWithValue("@straatNaam", straatNaam);
                SqlDataReader reader = con.ExecuteReader();
                int result = 0;
                Console.WriteLine("##### ROW #### StraatId #### StraatNaam #### StraatLengte ### GemeenteNaam ### ProvincieNaam ####");
                while (reader.Read())
                {
                    result++;
                    Console.WriteLine($"# {result} | {reader["StraatId"]} | {reader["StraatNaam"]}  | {reader["StraatLengte"]}m  | {reader["GemeenteNaam"]} | {reader["ProvincieNaam"]} #");

                }
                Console.WriteLine("#################################################################################################");
                globalConnection.Close();
                Console.WriteLine($"Gevonden resultaaten: {result}");
                Console.WriteLine("Enter om terug te gaan");
                Console.ReadLine();

            }

            public static void getStraatenPerGemeente(string gemeenteNaam)
            {

                globalConnection.Open();

                var searchDB = $@"select s.StraatId,s.StraatNaam , s.StraatLengte, g.GemeenteNaam, p.ProvincieNaam
from Straaten s
join Gemeenten g
on s.GemeenteId = g.GemeenteId
join Provincies p
on g.ProvincieId = p.ProvincieId
where g.GemeenteNaam = @gemeenteNaam";
                //Console.WriteLine(straatNaam);
                SqlCommand con = new SqlCommand(searchDB, globalConnection);
                con.Parameters.AddWithValue("@gemeenteNaam", gemeenteNaam);
                SqlDataReader reader = con.ExecuteReader();
                int result = 0;
                Console.WriteLine("## ROW ## StraatId ## StraatNaam ### StraatLengte ### GemeenteNaam ### ProvincieNaam ####");
                while (reader.Read())
                {
                    result++;
                    Console.WriteLine($"# {result} | {reader["StraatId"]} | {reader["StraatNaam"]}  | {reader["StraatLengte"]}m  | {reader["GemeenteNaam"]} | {reader["ProvincieNaam"]}#");

                }
                Console.WriteLine("#################################################################################################");
                globalConnection.Close();
                Console.WriteLine($"Gevonden resultaaten: {result}");
                Console.WriteLine("Enter om terug te gaan");
                Console.ReadLine();


            }

            public static void getOpdracht(int straatId)
            {
                globalConnection.Open();
                var selectFromDB = @$"select s.StraatId, s.StraatNaam, S.GraafId, g.GemeenteNaam, p.ProvincieNaam
                from Straaten s
                join Gemeenten g
                on s.GemeenteId = g.GemeenteId
                join Provincies p 
                on g.ProvincieId = p.ProvincieId
                where s.StraatId = @straatId;"; // straatID van parameters

                SqlCommand con = new SqlCommand(selectFromDB, globalConnection);
                con.Parameters.AddWithValue("@straatId", straatId);
                //https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlparametercollection.addwithvalue?view=netframework-4.8

                SqlDataReader reader = con.ExecuteReader();

                if (reader.Read())
                {

                    Console.WriteLine($"{reader["StraatId"]} | {reader["StraatNaam"]} | {reader["GraafId"]} | {reader["GemeenteNaam"]} | {reader["ProvincieNaam"]}");
                    int graafId = (int)reader["GraafId"];
                    globalConnection.Close();

                    querrySegment(graafId);

                }
                globalConnection.Close();
                Console.WriteLine("Enter om terug te gaan");
                Console.ReadLine();
            }

            public static void querrySegment(int graafId)
            {
                globalConnection.Open();
                var knoopQuerry = @"select k.KnoopId, k.Punt
            from GraafKnopen gk
            join Knopen k
            on gk.KnoopId = k.KnoopId
            where GraafId = @graafId";
                SqlCommand knoopSqlCommand = new SqlCommand(knoopQuerry, globalConnection);
                knoopSqlCommand.Parameters.AddWithValue("@graafId", graafId);
                SqlDataReader reader = knoopSqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"[Knoop={reader["KnoopId"]},[{reader["Punt"].ToString().Replace(' ', ',')}]");

                    // SETUP NEW CONNECTION TO ACCESS SEGMENTEN

                    using (SqlConnection connection = new SqlConnection(Extentie.connection.connectionString))
                    {
                        connection.Open(); // we gebruiken niet onze global connection
                        var segmentQuerry = @"select s.SegmentId, s.BeginKnoopId, s.EindKnoopId, s.PuntenLijst from GraafKnoopSegment gks join Segmenten s on s.SegmentId = gks.SegmentId where gks.GraafId = @graafId";
                        SqlCommand segmentSqlCommand = new SqlCommand(segmentQuerry, connection);

                        segmentSqlCommand.Parameters.AddWithValue("@graafId", graafId);
                        // Lets go. Eindelijk.

                        SqlDataReader segmentDataReader = segmentSqlCommand.ExecuteReader();

                        while (segmentDataReader.Read())
                        {
                            Console.WriteLine($"        [segment[{segmentDataReader["SegmentId"]},start:{segmentDataReader["beginKnoopId"]},eind{segmentDataReader["EindKnoopId"]}]");
                            // puntlijst 
                            var puntLijst = segmentDataReader["PuntenLijst"].ToString().Replace(' ', ',').Split("|");
                            foreach (var punt in puntLijst)
                            {
                                Console.WriteLine($"             ({punt})");
                            }


                        }


                    }


                }

            }

        }
    }
