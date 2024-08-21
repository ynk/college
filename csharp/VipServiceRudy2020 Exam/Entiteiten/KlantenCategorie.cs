using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DataLayer;

namespace Entiteiten
{
    public class KlantenCategorie
    {


        public int Id { get; set; }
        [StringLength(50)]
        public string Naam { get; set; }
        public int? StaffelKortingTypeId { get; set; }
        public StaffelKortingType StaffelKortingType { get; set; }
    }
}
