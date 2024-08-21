using System;
using System.Collections.Generic;

using System.Linq;

using BusinessLayer.Models;
using BusinessLayer.Repos;
using DataLayer.Execption;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repos
{
    class CountryRepo : ICountryRepo
    {
        private GeoServiceContext _context;

        public CountryRepo(GeoServiceContext context)
        {
            _context = context;
        }
        public void Add(Country c)
        {
            try
            {
                _context.Countries.Add(c);

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }

        public void Update(Country c)
        {
            try
            {
                 _context.Countries.Update(c);

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }

        public void Delete(Country c)
        {

            try
            {
                _context.Countries.Remove(c);
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                throw new ConnectionException("Database connection error");
            }
       
        }

        public bool Exists(Country c)
        {
            try
            {
                return _context.Countries.Any(x => x.Name == c.Name.Trim().ToLower());
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }

        public Country SearchById(int id)
        {

            try
            {
                var x = _context.Countries.Include(x => x.Cities).Include(x=>x.Continent).Include(w=> w.Rivers).Where(x => x.Id == id).SingleOrDefault();
                //  Country x = _context.Countries.Where(x => x.Id == id).FirstOrDefault();
                // List<City> b = _context.Cities.Where(c => c.Country.Id == x.Id).ToList();
                 //  Country x = _context.Countries.Include(x => x.Cities).Single(x => x.Id == id);
                if (x == null) return null;

                return x;
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }


        }

        public List<Country> GetAll()
        {
            try
            {
                return _context.Countries.ToList();

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }

        public List<Country> getCountriesByContinent(Continent continent)
        {
            try
            {
                return GetAll().FindAll(c => c.Continent == continent);

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }
    }
}
