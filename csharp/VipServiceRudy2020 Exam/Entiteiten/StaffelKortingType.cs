using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Entiteiten;

namespace DataLayer
{
    public class StaffelKortingType
    {

        public int Id { get; set; }
        [StringLength(50)]
        public string Naam { get; set; }
        public List<StaffelKorting> StaffelKortings { get; set; }
        public List<KlantenCategorie> KlantenCategories { get; set; }

    }
}
