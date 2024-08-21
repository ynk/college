using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Execptions;
using BusinessLayer.Models;

namespace BusinessLayer.Manager
{
    public class CityManager
    {
        public IUnitOfWork _uow;

        public CityManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Add(City c)
        {
            // check if city al exist
            if (!_uow.cityRepository.Exists(c))
            {
                
                _uow.cityRepository.Add(c);
                _uow.Complete();
       
            }
            else
            {
                throw new CityException("City Already Exist");
            }
           
           
        }

        public City GetById(int id)
        {
            try
            {
                return _uow.cityRepository.SearchById(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public void  Delete(City c)
        {
            try
            {
                _uow.cityRepository.Delete(c);
                _uow.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Update(City c)
        {
            try
            {
                _uow.cityRepository.Update(c);
                _uow.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
