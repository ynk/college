using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Model
{
    public class KlantModel
    {


        public int KlantId { get; set; }
        public string Naam { get; set; }
    
        [MinLength(10)]
        public string Adres { get; set; }

        public KlantModel()
        {
        }

        public KlantModel(string naam, string adres)
        {
            Naam = naam;
            Adres = adres;
        }
    }
}
