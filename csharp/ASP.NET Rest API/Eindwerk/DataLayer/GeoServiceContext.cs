using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore;


namespace DataLayer
{
    public class GeoServiceContext : DbContext
    {
        private string connectionString;

        public GeoServiceContext(string db = "Production") : base()
        {
            SetConnectionString(db);
        }
        private void SetConnectionString(string db = "Production")
        {
            switch (db)
            {
                case "Production":
                    connectionString = @"Data Source=YN-PC\SQLEXPRESS;Initial Catalog=geoservice;Integrated Security=True";
                    break;
                case "Test":
                    connectionString = @"Data Source=YN-PC\SQLEXPRESS;Initial Catalog=geoservice_test;Integrated Security=True";
                    break;
            }
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Continent> Continents { get; set; }

        public DbSet<River> Rivers { get; set; }


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
