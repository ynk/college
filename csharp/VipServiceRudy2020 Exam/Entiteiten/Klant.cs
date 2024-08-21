using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entiteiten
{
    public class Klant
    {

        public int Id { get; set; }
        public int Klantnummer { get; set; }
        [StringLength(100)]
        public string Naam { get; set; }
        [StringLength(50)]
        public string btw_nummer { get; set; }
        [StringLength(500)]
        public string Adres { get; set; }

        public int KlantenCategorieId { get; set; }
        public KlantenCategorie KlantenCategorie { get; set; }

        public override string ToString()
        {
            return $"{Naam}"; // Used in wpf
        }
    }
}

