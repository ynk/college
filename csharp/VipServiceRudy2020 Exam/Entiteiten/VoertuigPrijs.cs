using System.Reflection.Metadata;
using Entiteiten;

namespace DataLayer
{
    public class VoertuigPrijs
    {
        public int Id { get; set; }

        public int? ArragementId { get; set; }
        public Arragement Arragement { get; set; }

        public int? VoertuigId { get; set; }

        public Voertuig Voertuig { get; set; }

        public int Prijs { get; set; }


    }
}