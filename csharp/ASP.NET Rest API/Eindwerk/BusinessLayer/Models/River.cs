using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Execptions;

namespace BusinessLayer.Models
{
    public class River
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public double Length { get; private set; }

        public virtual ICollection<Country> Countries { get; private set; } = new List<Country>();

        public River()
        {
        }

        public River(string name, double length,List<Country> countries)
        {
          
            SetName(name);
            SetLength(length);
            SetCountries(countries);
        }

        public void SetName(string name)
        {

            var input = name.Trim();
            if (string.IsNullOrEmpty(input) && input.Length <= 0) throw new RiverException("Name is null or empty");
            Name = name;
        }

        public void SetLength(double length)
        {
            if (length <= 0) throw new RiverException("Length is invalid");
            Length = length;
        }

        public void SetCountries(List<Country> countries)
        {
            if (countries == null) throw new RiverException("List of countries is empty");
            Countries = countries;

        }
        
        
    }
}
