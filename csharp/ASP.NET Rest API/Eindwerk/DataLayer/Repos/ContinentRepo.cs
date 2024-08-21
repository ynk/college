using System;
using System.Collections.Generic;

using System.Linq;

using BusinessLayer.Models;
using BusinessLayer.Repos;
using DataLayer.Execption;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repos
{
    class ContinentRepo : IContinentRepo
    {

        private GeoServiceContext _context;

        public ContinentRepo(GeoServiceContext context)
        {
            _context = context;
        }

        public bool Exists(Continent c)
        {
            try
            {
              

               return _context.Continents.Any(x => x.Name == c.Name.ToLower().Trim());


            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }
        public void Add(Continent c)
        {
            try
            {
              _context.Continents.Add(c);
              

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }

        public void Update(Continent c)
        {
            try
            {
                _context.Continents.Update(c);

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }

        public void Delete(Continent c)
        {

            try
            {
                _context.Continents.Remove(c);
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                throw new ConnectionException("Database connection error");
            }
        }

        public Continent SearchById(int id)
        {
            try
            {
               // var x = _context.Continents.SingleOrDefault(x => x.Id == id);

               var x = _context.Continents.Include
                       (con => con.Countries).ThenInclude(w=>w.Cities)
                   .Where(x => x.Id == id).FirstOrDefault();
               
               if (x == null) return null;
                return x;
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }

        public List<Continent> GetAll()
        {
            try
            {
                return _context.Continents.ToList();

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }
    }
}
