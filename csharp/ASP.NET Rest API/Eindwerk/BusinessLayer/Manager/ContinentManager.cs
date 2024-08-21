using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLayer.Execptions;
using BusinessLayer.Models;

namespace BusinessLayer.Manager
{
    public class ContinentManager
    {
        public IUnitOfWork _uow;

        public ContinentManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Continent FindContinentById(int id)
        {
            var x = _uow.continentRepository.SearchById(id);

            return x;

        }

        public void AddContinent(Continent c)
        {


            if (!_uow.continentRepository.Exists(c))
            {
                _uow.continentRepository.Add(c);
                _uow.Complete();
            }
            else
            {
                throw new ContinentException("Continent Already exists");
            }

           

        }

        public void Remove(Continent c)
        {
            try
            {
                if (c.Countries.Count() != 0)
                {
                    throw new CountryException(
                        $"Unable to remove Continent since it still has {c.Countries.Count()} country. Delete the countries first, then the continent");

                }
                _uow.continentRepository.Delete(c);
                _uow.Complete();
            }
            catch (ContinentException e)
            {
                throw new ContinentException(e.Message);

            }

        }

        public void Update(Continent c)
        {
            try
            {
                _uow.continentRepository.Update(c);
                _uow.Complete();

            }
            catch (ContinentException e)
            {
                throw new ContinentException(e.Message);
            }

        }
    }
}

