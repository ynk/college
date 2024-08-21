using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Model;

namespace RestAPI.Model
{
    public class BestellingModel
    {
        public BestellingModel()
        {
           
        }

        public BestellingModel(int klantId, string product, int aantal)
        {
            KlantId = klantId;
            Product = product;
            Aantal = aantal;
        }

        public BestellingModel(string product, int aantal)
        {
            Product = product;
            Aantal = aantal;
        }

        [Required]
        public int KlantId { get; set; }
        [Required]
        public string Product { get; set; }
        [Required]
        public int Aantal { get; set; }
      
    }
}
