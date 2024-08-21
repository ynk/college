using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Execptions;

namespace BusinessLayer.Models
{
    public class Country
    {


        public int Id { get; private set; }
        public string Name { get; private set; }

        public int Population { get; private set; }
        public double Surface { get; private set; }
        
        public Continent Continent { get; private set; }
        public virtual ICollection<City> Cities { get; private set; } = new List<City>(); // todo: setter private


        public virtual ICollection<River> Rivers { get; private set; } = new List<River>();

        public Country()
        {
        }



        public Country(string name, Continent continent)
        {
            SetName(name);
            SetContinent(continent);
            
        }
        public Country(string name,int population, Continent continent)
        {
            SetName(name);
            SetPopulation(population);
            SetContinent(continent);

        }


        public void SetContinent(Continent c)
        {
            if (c != null)
            {
                Continent = c;
            }
            else
            {
                throw new CountryException("Continent is null");
            }

            

        }

        public void SetCities(List<City> cities)
        {
            Cities = cities;
        }



        public double GetSurface()
        {

            Surface = 0;

            foreach (City city in Cities)
            {
                Surface += city.Surface;
            }

            return Surface;
        }

        public int GetPopulationExcluded(int w)
        {
            var x = 0;

            foreach (var city in Cities)
            {
                if (city.Id != w)
                {
                    x += city.Population;
                }
               
            }

            return x;

        }
        public int GetPopulationFromCities()
        {

            var x  = 0;

            foreach (var city in Cities)
            {
                x += city.Population;
            }


            return x;
        }


        public int GetPopulation()
        {
            
            Population = 0;

            foreach (var city in Cities)
            {
                Population += city.Population;
            }


            return Population;
        }

        public void SetPopulation(int pop)
        {
            if (pop <= 0)
            {
                throw new CountryException($"Population is invalid: {pop}");
            }

            Population = pop;
        }

        public void SetName(string name)
        {
            var input = name.Trim();
            if (string.IsNullOrEmpty(input) && input.Length <= 0) throw new CountryException("Name is null or empty");
            Name = name;
        }

    }
}
