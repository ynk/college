using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLayer.Exceptions;
using BusinessLayer.Model;
using BusinessLayer.Repositories;

namespace BusinessLayer.Manager
{
    public class BestelManager
    {
        private IUnitOfWork _uow;

        public BestelManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<Bestelling> GetAlleBestellingenVanKlant(int id)
        {
            return _uow.BestellingRepository.ZoekAlleBestellingenVanKlant(id);
        }

        public Bestelling ZoekBestellingOpId(int id)
        {
            var x = _uow.BestellingRepository.ZoekBestelling(id);
            if (x != null)
            {
                return x;
            }
            throw new BestellingException("KlantService: Geen bestelling gevonden");
        }
        public void VerwijderBestellingId(int id)
        {
            var x = ZoekBestellingOpId(id);
            if (x != null)
            {
                _uow.BestellingRepository.VerwijderBestelling(x);
                _uow.Complete();
            }
            else
            {
                throw new BestellingException("KlantService : bestelling does not exist");
            }

            
        }

        public void VoegBestellingToe(Bestelling b)
        {
            /*
            IEnumerable<Bestelling> bestellingen = GetAlleBestellingenVanKlant(b.KlantId);

            if (!bestellingen.Any(x => x.Product == b.Product && x.Aantal == b.Aantal))
            {
                _uow.BestellingRepository.VoegBestellingToe(b);
                _uow.Complete();

            }
            throw new BestellingException("Bestelling bestaat al");
            */

            _uow.BestellingRepository.VoegBestellingToe(b);
            _uow.Complete();


        }
        public void UpdateBestelling(Bestelling b)
        {
            _uow.BestellingRepository.UpdateBestelling(b);
            _uow.Complete();
        }

        public void ResetDB()
        {
            _uow.BestellingRepository.VerwijderAlleBestellingen();
        }
    }

}
