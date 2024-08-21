using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Models;

namespace BusinessLayer.Repos
{
    public interface IRiverRepo
    {
        void Add(River c);
        void Update(River c);
        void Delete(River r);
        bool Exists(River c);
        River SearchById(int id);
        List<River> GetAll();
    }
}
