using System.ComponentModel.DataAnnotations;

namespace Entiteiten
{
    public class Voertuig
    {
        public int Id { get; set; }
        public int EersteUur { get; set; }
        [StringLength(100)]
        public string Naam { get; set; }
    }
}