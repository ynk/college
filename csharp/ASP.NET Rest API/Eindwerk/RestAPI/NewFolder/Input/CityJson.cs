using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.NewFolder.Input
{
    public class CityJson
    {
     
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        [Required]
        public double Surface { get; set; }
        [Required]
        public int Population { get; set; }
        [Required]
        public bool isCapital { get; set; } = false;
    }
}
