using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.NewFolder.Output
{
    public class CountryDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Continent { get; set; }
        public int CitiesSum { get; set; }
        public int Population { get; set; }
        public double Surface { get; set; }

        public ICollection<string> Cities { get; set; }
        public ICollection<string> Rivers { get; set; }
    }
}
