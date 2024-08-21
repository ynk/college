using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entiteiten
{
   public class Arragement
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Naam { get; set; }

        public int? Aantal_uur { get; set; }
        public int? Min_start { get; set; }
        public int? Max_start { get; set; }
    }
}
