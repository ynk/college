using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.NewFolder.Output
{
    public class RiverDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public List<string> Countries { get; set; }
    }
}
