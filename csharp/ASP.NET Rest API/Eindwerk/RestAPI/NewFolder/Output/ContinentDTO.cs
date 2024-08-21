using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.NewFolder.Output
{
    public class ContinentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Population { get; set; }
        
        public double Surface { get; set; }

        public List<string> Countries { get; set; }
    }
}
