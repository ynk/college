using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer
{
    public class KlantBestellingContext : DbContext
    {

        private string connectionString;
        public KlantBestellingContext(string db = "Production") : base()
        {
            SetConnectionString(db);
        }
        private void SetConnectionString(string db = "Production")
        {
            switch (db)
            {
                case "Production":
                    connectionString = @"Data Source=DESKTOP-DC4GGIA\SQLEXPRESS;Initial Catalog=KlantBestelling;Integrated Security=True";
                    break;
                case "Test":
                    connectionString = @"Data Source=DESKTOP-DC4GGIA\SQLEXPRESS;Initial Catalog=KlantBestellingTest;Integrated Security=True";
                    break;
            }
        }
        public DbSet<Bestelling> Bestelling { get; set; }
       public DbSet<Klant> Klant { get; set; }

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       {
        //   connectionString = (@"Data Source=DESKTOP-DC4GGIA\SQLEXPRESS;Initial Catalog=KlantBestelling;Integrated Security=True");
            if (connectionString == null)
           {
               SetConnectionString();
           }

           optionsBuilder.UseSqlServer(connectionString);
       }


    }
}
