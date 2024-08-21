using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Model;

namespace RestAPI.JsonResponse
{
    public  class KlantJSON
    {
        public string KlantId { get; set; }
        public string Naam { get; set; }
        public string Adres { get; set; }
        public IEnumerable<string> Bestellingen { get; set; }
    }
}
