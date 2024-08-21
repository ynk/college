using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.NewFolder
{
    public class CountryJson
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Population { get; set; }



    }
}
