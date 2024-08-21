using System;
using System.Collections.Generic;
using System.Text;
using Entiteiten;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DataLayer
{
    public class VipServiceContext : DbContext
    {
        private string _connectionString;

        public VipServiceContext(string envoirment = "Production")
        {
            SetConnectingString(envoirment);
        }
        public VipServiceContext()
        {

        }
        public DbSet<Arragement> Arragements { get; set; }
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<KlantenCategorie> KlantenCategories { get; set; }

        public DbSet<Locaties> Locaties { get; set; }

        public DbSet<Reservaties> Reservatieses { get; set; }
        public DbSet<StaffelKorting> StaffelKortings { get; set; }

        public DbSet<StaffelKortingType> StaffelKortingTypes { get; set; }
        public DbSet<VoertuigPrijs> Voertuigprijzen { get; set; }
        public DbSet<Voertuig> Voertuigen { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (_connectionString == null)
            {
                SetConnectingString();
            }

            optionsBuilder.UseSqlServer(_connectionString);
        }

        private void SetConnectingString(string db = "Production")
        {
            switch (db)
            {
                case "Production":
                    _connectionString = "Data Source=DESKTOP-NUIL6HO\\SQLEXPRESS;Initial Catalog=prog4_vipservice;Integrated Security=True";
                    break;
                case "Test":
                    _connectionString = "Data Source=DESKTOP-NUIL6HO\\SQLEXPRESS;Initial Catalog=prog4_vipservice_test;Integrated Security=True";
                    break;

            }


        }

        //

    }
}
