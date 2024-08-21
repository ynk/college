using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Execptions;
using BusinessLayer.Models;

namespace BusinessLayer.Manager
{
    public class RiverManager
    {
        public IUnitOfWork _uow;

        public RiverManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public River getById(int id)
        {
            try
            {
                var x = _uow.riverRepository.SearchById(id);
                if (x == null) throw new  RiverException("No rivers found");
                return x;
            }
            catch (Exception e)
            {
                throw new RiverException(e.Message);
            }
        }

        public void Delete(River r)
        {

            try
            {
                _uow.riverRepository.Delete(r);
                _uow.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public void Add(River c)
        {
            try
            {  
                if(!_uow.riverRepository.Exists(c))
                {
                    /* •	Een rivier behoort steeds tot minstens één land. */

                    if (c.Countries.Count == 0)
                    {
                        throw new RiverException("Een rivier behoort steeds tot minstens één land.");
                    }
                
                    _uow.riverRepository.Add(c);
                    _uow.Complete();
                }
                else
                {
                    throw new RiverException("River does already exist");
                }
                
            }
            catch (Exception e)
            {
                throw new RiverException(e.Message);
            }
        }

        public void Update(River r)
        {

            try
            {
                _uow.riverRepository.Update(r);
                _uow.Complete();
            }
            catch (Exception e)
            {
                throw new RiverException(e.Message);
            }
        }
    }
}
