using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BusinessLayer.Execptions;

namespace BusinessLayer.Models
{
    public class Continent
    {
        public int Id { get; private set; }
        public String Name { get; private set; }
        public int Population { get; private set; } = 0;
        public virtual IEnumerable<Country> Countries { get; private set; } = new List<Country>();

        public Continent()
        {
        }
        public Continent(string name)
        {
            SetName(name);
            SetPopulation();

        }


        

        public void SetName(string name)
        {
            var input = name.Trim();
            if (string.IsNullOrEmpty(input) && input.Length <= 0) throw new ContinentException("Name is null or empty");
            Name = name;
        }

        public void SetPopulation()
        {
            var Population = 0;
            foreach (var x in Countries)
            {

                foreach (City City in x.Cities)
                {
                    Population += City.Population;
                }

            }

      
        }

        public int GetPopulation()
        {
            var Population = 0;
            foreach (var x in Countries)
            {

                foreach (City City in x.Cities)
                {
                    Population += City.Population;
                }

            }

            return Population;
        }

        public void setCountries(List<Country> countries)
        {
            Countries = countries;
        }

        public double GetSurface()
        {
            double Surface = 0;
            foreach (var x in Countries)
            {
                foreach (var City in x.Cities)
                {
                    Surface += City.Surface;
                }
            }

            return Surface;
        }
     
    }
}
