using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Manager;
using BusinessLayer.Model;

namespace BusinessLayer.Repositories
{
    public interface IBestellingRepository
    {
        void VoegBestellingToe(Bestelling b);
        void UpdateBestelling(Bestelling b);
        //void VerwijderBestellingById(int id);
        void VerwijderBestelling(Bestelling b);
        Bestelling ZoekBestelling(int id);
        public IEnumerable<Bestelling> ZoekAlleBestellingen();
        List<Bestelling> ZoekAlleBestellingenVanKlant(int id);
        public void VerwijderAlleBestellingen();

    }
}
