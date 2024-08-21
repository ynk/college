using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using BusinessLayer.Manager;
using BusinessLayer.Model;
using BusinessLayer.Repositories;
using DataLayer.Exception;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class BestellingRepository : IBestellingRepository
    {
        private KlantBestellingContext _context;


        public BestellingRepository(KlantBestellingContext context)
        {
            _context = context;
        }

        public void VoegBestellingToe(Bestelling b)
        {
           

            try
            {
                _context.Bestelling.Add(b);

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error, bestelling is niet opgeslagen");
            }
        }

        public void UpdateBestelling(Bestelling b)
        {
           
            try
            {
                _context.Bestelling.Update(b);

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error, niks is opgeslagen");
            }

        }

        public void VerwijderBestellingById(int id)
        {
            throw new NotImplementedException();
        }

        public void VerwijderBestelling(Bestelling b)
        {
            
            try
            {
                _context.Bestelling.Remove(b);

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error, niks is opgeslagen");
            }
        }

        public Bestelling ZoekBestelling(int id)
        {

            try
            {
                var x = _context.Bestelling.Where(b => b.Id == id).FirstOrDefault();
                return x;

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error, niks is opgeslagen");
            }
        }

        public IEnumerable<Bestelling> ZoekAlleBestellingen()
        {
            return null;
        }


        public List<Bestelling> ZoekAlleBestellingenVanKlant(int id)
        {
            try
            {
                var x = _context.Bestelling.Where(b => b.KlantId == id).ToList();
                return x;
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                
                throw new ConnectionException("Database connection error, niks is opgeslagen");
            }
        }

        public void VerwijderAlleBestellingen()
        {
      
            try
            {
                _context.Database.ExecuteSqlRaw("delete from dbo.Bestelling;DBCC CHECKIDENT (Bestelling, RESEED, 0);");
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error, niks is opgeslagen");
            }
        }
    }
}
