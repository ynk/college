using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Models;

namespace BusinessLayer.Repos
{
    public interface ICityRepo
    {
        void Add(City c);
        void Update(City c);
        void Delete(City c);
        City SearchById(int id);
        bool Exists(City c);
        List<City> GetAll();
    }
}
