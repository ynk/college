using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entiteiten
{
    public class Locaties
    {

        public int Id { get; set; }
        [StringLength(50)]

        public string LocatieNaam { get; set; }
    }
}
