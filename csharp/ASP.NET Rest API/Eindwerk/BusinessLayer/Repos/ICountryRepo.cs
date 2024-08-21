using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Models;

namespace BusinessLayer.Repos
{
    public interface ICountryRepo
    {
        void Add(Country c);
        void Update(Country c);
        void Delete(Country c);
        bool Exists(Country c);
        Country SearchById(int id);
        List<Country> GetAll();
        List<Country> getCountriesByContinent(Continent continent);
    }
}
