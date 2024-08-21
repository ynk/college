using System;
using System.Collections.Generic;
using BusinessLayer.Execptions;
using BusinessLayer.Models;

namespace BusinessLayer.Manager
{
    public class CountryManager
    {
        private IUnitOfWork _uow;

        public CountryManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Add(Country c)
        {
            if (!_uow.countryRepository.Exists(c))
            {
                _uow.countryRepository.Add(c);
                _uow.Complete();
            }
            else
            {
                throw new CountryException("Country Already exists");
            }

    

        }
        public Country GetCountryById(int id)
        {
            try
            {
                var c = _uow.countryRepository.SearchById(id);
                return c;
            }
            catch (Exception e)
            {
              
                throw new CountryException(e.Message);
            }

        }

        public List<Country> GetAllByContinent(Continent c)
        {
            return _uow.countryRepository.getCountriesByContinent(c);
        }

        public List<Country> GetAllCountries()
        {
            try
            {
                return _uow.countryRepository.GetAll();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var x = _uow.countryRepository.SearchById(id);

                if (x.Cities.Count != 0)
                {
                    throw new CountryException($"Country still has cities, in fact: {x.Cities.Count}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public void Update(Country c)
        {

            try
            {
                _uow.countryRepository.Update(c);
                _uow.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }



        public void Remove(Country c)
        {
            if (c.Cities.Count != 0) throw new CountryException($"Country still has cities: {c.Cities.Count}");
            _uow.countryRepository.Delete(c);
            _uow.Complete();

        }
    }
}
