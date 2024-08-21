using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using BusinessLayer.Model;
using BusinessLayer.Repositories;
using DataLayer.Exception;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class KlantRepository : IKlantRepository
    {
        private KlantBestellingContext _context;

        public KlantRepository(KlantBestellingContext context)
        {
            _context = context;
        }



        public bool bestaatKlantAl(Klant k)
        {
            try
            {
                var x  = _context.Klant.Any(x => x.Naam.ToLower() == k.Naam.ToLower() || x.Adres.ToLower() == k.Adres.ToLower());

                return x;

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error, bestelling is niet opgeslagen");
            }
        }

        public void VoegKlantToe(Klant k)
        {

            try
            {
                _context.Klant.Add(k);

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error, bestelling is niet opgeslagen");
            }
          
        }

        public void UpdateKlant(Klant k)
        {
            try
            {
                _context.Update(k);

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error, bestelling is niet opgeslagen");
            }

        
        }

        public void VerwijderKlant(Klant k)
        {
            try
            {
                _context.Klant.Remove(k);

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error, klant is niet verwijderd");
            }
          
        }

        public void VerwijderKlantMetId(int id)
        {
            var x = ZoekKlantMetId(id);
            _context.Klant.Remove(x);
        }

        public Klant ZoekKlantMetId(int id)
        {
            try
            {
                var b = new List<Bestelling>(_context.Bestelling.Where(x => x.KlantId == id).ToList());
                var x = _context.Klant.Where(x => x.Id == id).SingleOrDefault();


                return x;

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error. Klant is niet gevonden in de database omdat er geen connectie is");
            }

        }

        public IEnumerable<Klant> ZoekAlleKlanten()
        {
            try
            {
                return _context.Klant.ToList();

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException(
                    "Database connection error. Klanten lijst is niet gevonden in de database omdat er geen connectie is");
            }
          
        }

        public void VerwijderAlleKlanten()
        {
            try
            {
                _context.Database.ExecuteSqlRaw("delete from dbo.Klant;DBCC CHECKIDENT (Klant, RESEED, 0);");

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error, verwijderen is niet gelukt");
            }
           
        }
    }
}
