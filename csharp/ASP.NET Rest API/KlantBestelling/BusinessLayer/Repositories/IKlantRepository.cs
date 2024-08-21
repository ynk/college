using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Model;

namespace BusinessLayer.Repositories
{
    public interface IKlantRepository
    {
        void VoegKlantToe(Klant k);
        void UpdateKlant(Klant k);
        void VerwijderKlant(Klant k);
        void VerwijderKlantMetId(int id);
        Klant ZoekKlantMetId(int id);
        IEnumerable<Klant> ZoekAlleKlanten();
        public void VerwijderAlleKlanten();
        bool bestaatKlantAl(Klant k);
    }
}
