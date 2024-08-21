using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace Extentie.Handlers.dbHanlder
{
    public static class dbImporter
    {
        public static void Reader(string fileName)
        {

            string tableNaam = fileName.Substring(0, fileName.Length - 4);
            Console.Write($"[DB] Reading {tableNaam}");
            DataTable rawCsvDataTable = new DataTable();


            var csvReader = new TextFieldParser(@$"c:\output\{fileName}");
            csvReader.SetDelimiters(";");
            csvReader.HasFieldsEnclosedInQuotes = true;
            string[] col = csvReader.ReadFields();

            foreach (var column in col)
            {
                rawCsvDataTable.Columns.Add(column);// onze colum uit cvs
            }

            while (!csvReader.EndOfData)
            {
                string[] data = csvReader.ReadFields();

             
                rawCsvDataTable.Rows.Add(data);


            }
            Console.WriteLine(" | Done");
            Sender(rawCsvDataTable, tableNaam);
        }

        public static void Sender(DataTable csvDataTable, string tableNaam)
        {
            //string tableNaam = fileName.Substring(0, fileName.Length - 4);  // .CSV erafhalen
            Console.Write($"[DB] Sending: {tableNaam}");
            //     Console.Write($"[DB] Inserting: {tableNaam}");
            SqlConnection dbConnection = new SqlConnection(Extentie.connection.connectionString);

            dbConnection.Open();

            SqlBulkCopy sender = new SqlBulkCopy(dbConnection);
            sender.DestinationTableName = ("dbo." + tableNaam);
            foreach (var colum in csvDataTable.Columns)
            {
                sender.ColumnMappings.Add(colum.ToString(), colum.ToString());
            }

            try
            {
                sender.WriteToServer(csvDataTable);

            }
            catch (Exception e)
            {
                Console.WriteLine("DEBUG - ERROR:");
                Console.WriteLine(e);

            }


            Console.WriteLine(" | Done");

        }


        public static void Reset()
        {
            SqlConnection dbConnection = new SqlConnection(Extentie.connection.connectionString);
            dbConnection.Open();
            var deleteStrings = new string[]

            {

                "delete from dbo.Segmenten",
                "delete from dbo.GraafKnoopSegment",
                "delete from dbo.GraafKnopen",
                "delete from dbo.Graven",
                "delete from dbo.Knopen",
                "delete from dbo.Gemeenten",
                "delete from dbo.Straaten",
                "delete from dbo.Provincies"
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
            Console.Clear();
            Console.WriteLine("[DB] Reset done!");


        }
    }
}
