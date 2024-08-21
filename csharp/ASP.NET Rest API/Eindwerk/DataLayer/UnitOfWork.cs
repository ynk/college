using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using BusinessLayer;
using BusinessLayer.Repos;
using DataLayer.Repos;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {

        private GeoServiceContext _context;

        public UnitOfWork(GeoServiceContext context)
        {
            _context = context;
            cityRepository = new CityRepo(context);
            continentRepository = new ContinentRepo(context);
            riverRepository = new RiverRepo(context);
            countryRepository = new CountryRepo(context);

        }
        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (System.Exception ex)
                //TODO : SqlExceptions
            {
                Debug.WriteLine(ex.Message);
                throw;

            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IContinentRepo continentRepository { get; }
        public IRiverRepo riverRepository { get; }
        public ICityRepo cityRepository { get; }
        public ICountryRepo countryRepository { get; }
    }
}
