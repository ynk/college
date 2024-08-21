using System;
using System.Collections.Generic;

using System.Linq;

using BusinessLayer.Models;
using BusinessLayer.Repos;
using DataLayer.Execption;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repos
{
    class RiverRepo : IRiverRepo    
    {

        private GeoServiceContext _context;

        public RiverRepo(GeoServiceContext context)
        {
            _context = context;
        }
        public void Add(River c)
        {
            try
            {
                _context.Rivers.Add(c);

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }

        public void Update(River c)
        {
            try
            {
                _context.Rivers.Update(c);

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }

        public void Delete(River r)
        {

            try
            {
                _context.Rivers.Remove(r);
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }

        public bool Exists(River c)
        {
            try
            {
                return _context.Rivers.Any(x => x.Name == c.Name.ToLower().Trim());
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }

        public River SearchById(int id)
        {
            try
            {
                var x = _context.Rivers.Include(o=> o.Countries).ThenInclude(w=>w.Continent).Where(x => x.Id == id).SingleOrDefault();
                if (x == null) return null;
                return x;
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }


        }

        public List<River> GetAll()
        {
            try
            {
                return _context.Rivers.ToList();

            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {

                throw new ConnectionException("Database connection error");
            }
        }
    }
}
