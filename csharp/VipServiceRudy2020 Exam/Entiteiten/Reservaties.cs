using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Entiteiten;

namespace Entiteiten
{   
    public class Reservaties
    {
        public int Id { get; set; }

        public int? KlantId { get; set; }
        [ForeignKey("KlantId")]
        public Klant Klant { get; set; }
        
        [StringLength(500)]
        public string Adress { get; set; }

        public DateTime StartDatum { get; set; }
        public DateTime EindDateTime { get; set; }


        public double? TotaalExclusiefBtw { get; set; }


        public int? StartLocatieId { get; set; }
        [ForeignKey("StartLocatieId")]
        public Locaties StartLocatie { get; set; }

        public int? EindLocatieId { get; set; }
        [ForeignKey("EindLocatieId")]
        public Locaties EindLocatie { get; set; }

        public int? VoertuigId { get; set; }
        public Voertuig Voertuig { get; set; }
        public int? ArragementId { get; set; }
        public Arragement Arragement { get; set; }

      //  public BigInteger? TimeSpanInTics { get; set; }
        public int? Eerste_uren { get; set; }
        public int? Nachtuur { get; set; }
        public int? TotaalNachtUren { get; set; }
        public int? OveruurPerUur { get; set; }
        public int? TotaalOveruren { get; set; }
        public double TotaalInclusiefBtw { get; set; }

        public double TotaalBedrag { get; set; }

        public double Korting { get; set; }

    }
}