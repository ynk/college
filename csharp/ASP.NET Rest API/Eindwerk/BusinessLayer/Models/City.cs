using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Execptions;

namespace BusinessLayer.Models
{
    public class City
    {
        public int Id { get; private set; }

        public string Name { get; private set; }
        public int Population { get; private set; }
        public virtual Country Country { get; private set; }

        public double Surface { get; private set; }
        public Boolean IsCapital { get; set; }

        public City()
        {
        }

        public City(string name, int population, Country country, bool isCapital, double surface)
        {

            SetName(name);
            SetCountry(country);
            SetPopulation(population);
            
            SetSurface(surface);
            SetCapital(isCapital);
        }

 

    public void SetCapital(bool Capital)
        {
            IsCapital = Capital;
        }
        public void SetName(string name)
        {
            var input = name.Trim();
            if (string.IsNullOrEmpty(input) && input.Length <= 0) throw new CityException("Name is null or empty");
            Name = name;
        }

        public void SetPopulation(int population)
        {
            /*if (population < 0) throw new CityException("Population is under zero");
            Population = population;

            */
            if (population < 0)
            {
                throw new CityException("Population is under zero");
            }

            if (Id == 0)
            {
                if (Country.Population < population)
                {
                    throw new CountryException($"Country pop:{Country.Population}, you have : {population} ");

                }

                Population = population;
            }
            var x = Country.GetPopulationExcluded(Id);

            if ((population + x) > Country.Population)
            {
                throw new CityException($"No more population left,free spots:{x}. You want to add {population}. You are {Country.Population - (population + x)} Over it (without current population) sum {population + x} > {Country.Population}.");
            }

    
            Population = population;
        }

        public void SetCountry(Country c)
        {
            /*Country is een object dus ik kan gewoon checken of het niet null is */

            Country = c ?? throw new CityException("Country is invalid"); // Fancy statement!
        }

        public void SetSurface(double surf)
        {
            if (surf < 0) throw new CityException("Surface is invalid");
            Surface = surf;
        }
        
    }
}
