using System;
using System.Collections.Generic;
using DataLayer;

namespace Entiteiten.Interfaces
{
    public interface IVipServiceRepository
    {
        Klant GetKlant(int klantNummer);
        void SaveReservatie(Reservaties reservatie);
    //    List<StaffelKorting> GetStaffelKortings();
        double GetKortingPercentageKlant(Klant k, DateTime r);
     //   KlantenCategorie GetKlantenCategorie(string categorieNaam);
        Locaties GetLocatie(string locatieNaam);//Todo: remove
        Reservaties GetLastReservaties(Voertuig voertuig, DateTime startDateTime, DateTime eindDateTime);
        List<Locaties> GetLocaties();
        List<Reservaties> GetReservaties();
        VoertuigPrijs GetVoertuigPrijsVolgensArragement(Arragement arragement, Voertuig voertuig);
        List<Arragement> GetAlleArragementen();
        Arragement GetArragement(string ArragementNaam);
        Voertuig GetVoertuigById(int id);
        List<Voertuig> GetVoertuigen();
        List<Klant> GetKlanten();
        List<Reservaties> GetReservatiesDoorKlantId(Klant id);
        List<Reservaties> GetReservatiesDoorDate(DateTime date);
        List<Reservaties> GetReservatiesDoorKlantIdEnDatum(Klant klant, DateTime date);
    }
}