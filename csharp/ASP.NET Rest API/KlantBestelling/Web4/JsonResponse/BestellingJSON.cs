using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Model;

namespace RestAPI.JsonResponse
{
    public class BestellingJSON
    {
        public string BestelingId { get; set; }
        public string KlantId { get; set; }
        public string Product { get; set; }
        public int Aantal { get; set; }
    }
}
