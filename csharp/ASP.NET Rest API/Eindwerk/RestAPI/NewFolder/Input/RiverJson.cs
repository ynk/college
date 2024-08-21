using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.NewFolder.Input
{
    public class RiverJson
    {
        public int id { get; set; }
        [Required]
        public List<int> countryid { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public double length { get; set; }
    }
}
