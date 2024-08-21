using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;

namespace Entiteiten
{
    public class StaffelKorting
    {

        public int Id { get; set; }
        public int Aantal { get; set; }
        public double Korting { get; set;}

        public int? StaffelKortingTypeId { get; set; }

        public StaffelKortingType StaffelKortingType { get; set; }


    }
}
