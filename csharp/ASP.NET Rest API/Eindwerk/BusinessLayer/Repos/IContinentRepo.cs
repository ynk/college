using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Models;

namespace BusinessLayer.Repos
{
    public interface IContinentRepo
    {
        void Add(Continent c);

        bool Exists(Continent c);
        void Update(Continent c);
        void Delete(Continent c);
        Continent SearchById(int id);
        List<Continent> GetAll();
    }
}
