using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.NewFolder.Output
{
    public class CityDTO
    {
   

        public int Id { get; set; }
        public string Name { get; set; }
        public double Surface { get; set; }
        public int Population { get; set; }

        public Boolean isCapital { get; set; }
        public string Continent { get; set; }
        public string Country { get; set; }
    }

    
}
