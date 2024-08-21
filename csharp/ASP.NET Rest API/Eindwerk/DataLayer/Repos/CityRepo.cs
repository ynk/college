using System;
using System.Collections.Generic;

using System.Linq;

using BusinessLayer.Models;
using BusinessLayer.Repos;
using DataLayer.Execption;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repos
{
    class CityRepo : ICityRepo
    {

        private GeoServiceContext _context;

        public CityRepo(GeoServiceContext context)
        {
            _context = context;
        }

        public void  Add(City c)
        {
            try
            {
                _context.Cities.Add(c);

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }

        public void Update(City c)
        {
            try
            {
                _context.Cities.Update(c);
           

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }


        public void Delete(City c)
        {


            try
            {
                _context.Cities.Remove(c);
       
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                Console.WriteLine();
                throw;
            }
        }

        public City SearchById(int id)
        {
            try
            {
                var x = _context.Cities.Include(w=>w.Country.Continent)
                    .Include(c => c.Country).ThenInclude(p=>p.Cities)
                    .Where(x => x.Id == id).SingleOrDefault();
                if (x == null) return null;
                
                // oh btw, continent terug linken
          
                return x;
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }

        public bool Exists(City c)
        {
            try
            {
                return _context.Cities.Any(x => x.Name == c.Name.Trim().ToLower());

                // oh btw, continent terug linken


            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
          
        }

        public List<City> GetAll()
        {
            try
            {
                return _context.Cities.ToList();

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }
    }
}
