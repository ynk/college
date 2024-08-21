using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.NewFolder
{
    public class ContinentJson
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int Population { get; set; }

        public List<string> Countries { get; set; }

    }
}

