using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using BusinessLayer.Repositories;
using DataLayer.Repositories;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private KlantBestellingContext _context;

        public UnitOfWork(KlantBestellingContext context)
        {
            _context = context;
            BestellingRepository = new BestellingRepository(context);
            KlantRepository = new KlantRepository(context);
   
        }

        public IKlantRepository KlantRepository { get; }
        public IBestellingRepository BestellingRepository { get; }
     

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
       
    }
}
