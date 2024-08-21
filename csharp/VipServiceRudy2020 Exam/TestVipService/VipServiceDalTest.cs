using System;
using System.Collections.Generic;
using DataLayer;
using Entiteiten;
using Entiteiten.Interfaces;


namespace TestVipService
{
    public class VipServiceDalTest : IVipServiceRepository
    {
        private Klant CreateBaseKlant()
        {
            var klant = new Klant()
            {
                Id = 1,
                Naam = "Yannick",
                Adres = "Straat Ergens 73",
                KlantenCategorieId = 1,
                KlantenCategorie = new KlantenCategorie()
                {
                    Id = 1,
                    Naam = "vip"
                },
                Klantnummer = 1
            };

            return klant;
        }

        public Klant GetKlant(int klantNummer)
        {

            var klant = CreateBaseKlant();
            klant.Klantnummer = klantNummer;
            return klant;
            
        }

        public void SaveReservatie(Reservaties reservatie)
        {
            throw new NotImplementedException();
        }

        public List<StaffelKorting> GetStaffelKortings()
        {
            throw new NotImplementedException();
        }

        public double GetKortingPercentageKlant(Klant k, DateTime r)
        {
            throw new NotImplementedException();
        }

        public KlantenCategorie GetKlantenCategorie(string categorieNaam)
        {
            throw new NotImplementedException();
        }

        public Locaties GetLocatie(string locatieNaam)
        {
           var locatie = new Locaties()
           {   
               Id = 1,
               LocatieNaam = "Gent"
           };
           return locatie;
        }

        public Reservaties GetLastReservaties(Voertuig voertuig, DateTime startDateTime, DateTime eindDateTime)
        {
            throw new NotImplementedException();
        }

        public List<Locaties> GetLocaties()
        {
            throw new NotImplementedException();
        }

        public List<Reservaties> GetReservaties()
        {
            throw new NotImplementedException();
        }

        public VoertuigPrijs GetVoertuigPrijsVolgensArragement(Arragement arragement, Voertuig voertuig)
        {
            throw new NotImplementedException();
        }
        
        public List<Arragement> GetAlleArragementen()
        {
            throw new NotImplementedException();
        }

        public Arragement GetArragement(string ArragementNaam)
        {
            throw new NotImplementedException();
        }

        public Voertuig GetVoertuigById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Voertuig> GetVoertuigen()
        {
            throw new NotImplementedException();
        }

        public List<Klant> GetKlanten()
        {
            throw new NotImplementedException();
        }

        public List<Reservaties> GetReservatiesDoorKlantId(Klant id)
        {
            throw new NotImplementedException();
        }

        public List<Reservaties> GetReservatiesDoorDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<Reservaties> GetReservatiesDoorKlantIdEnDatum(Klant klant, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}