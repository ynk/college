using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Repos;

namespace BusinessLayer
{
    public interface IUnitOfWork : IDisposable
    {
        public int Complete();
        //public void Dispose();
        public IContinentRepo continentRepository { get; }
        public IRiverRepo riverRepository { get; }
        public ICityRepo cityRepository { get; }
        public ICountryRepo countryRepository { get; }
    }
}
