using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using BusinessLayer.Exceptions;
using BusinessLayer.Model;
using BusinessLayer.Repositories;

namespace BusinessLayer.Manager
{
    public class KlantenManager
    {

        private IUnitOfWork _uow;
        public KlantenManager(IUnitOfWork uow)
        {
            _uow = uow;
        }
        
        public Klant VoegKlantToe(Klant k)
        {


            if (!_uow.KlantRepository.bestaatKlantAl(k)) 
            {
                _uow.KlantRepository.VoegKlantToe(k);
                _uow.Complete();
            }
            else
            {
                throw new KlantException("Klant bestaat al");
            }
            
            return k;
        }

        public void VerwijderKlant(int id)
        {
            var klant = ZoekKlantMetId(id);
            if (klant == null)
            {
                throw new KlantException("Klant bestaat niet");
            }
            if (klant.Bestellingen.Count != 0)
            {
                throw new KlantException("KlantService : bestelling not empty");
            }

            _uow.KlantRepository.VerwijderKlant(klant);
            _uow.Complete();
            
        }

        public void UpdateKlant(Klant k)
        {
            _uow.KlantRepository.UpdateKlant(k);
            _uow.Complete();

        }


        public Klant ZoekKlantMetId(int id)
        {
            var x = _uow.KlantRepository.ZoekKlantMetId(id);
            if (x == null)
            {
                throw new KlantException("Klant bestaat niet");
            }

            if (x.Bestellingen == null)
            {
                x.SetBestelling(new List<Bestelling>());
            }
            return x;


        }

        public IEnumerable<Klant> AlleKlanten()
        {
            return _uow.KlantRepository.ZoekAlleKlanten();
        }

        public void ResetDB()
        {
            _uow.KlantRepository.VerwijderAlleKlanten();
            _uow.Complete();
        }

    }
}
