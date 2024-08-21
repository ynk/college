namespace DataLayer
{
    using Entiteiten;
    using Entiteiten.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="VipServiceRepository" />.
    /// </summary>
    public class VipServiceRepository : IVipServiceRepository
    {
        /// <summary>
        /// Defines the _context.
        /// </summary>
        private static VipServiceContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="VipServiceRepository"/> class.
        /// </summary>
        public VipServiceRepository()
        {
            _context ??= new VipServiceContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VipServiceRepository"/> class.
        /// </summary>
        /// <param name="environment">The environment<see cref="string"/>.</param>
        public VipServiceRepository(string environment)
        {
            _context ??= new VipServiceContext(environment);
        }

        /// <summary>
        /// The GetKlantenCategorie.
        /// </summary>
        /// <param name="categorieNaam">The categorieNaam<see cref="string"/>.</param>
        /// <returns>The <see cref="KlantenCategorie"/>.</returns>
     /*
        public KlantenCategorie GetKlantenCategorie(string categorieNaam)
        {
            var x = _context.KlantenCategories.Include(x => x.StaffelKortingType)
                .Include(x => x.StaffelKortingType.StaffelKortings)
                .SingleOrDefault(x => x.Naam == categorieNaam);

            //old:   var x = _context.KlantenCategories.FirstOrDefault(x=> x.Naam == categorieNaam);
            return x;
        }
     */
     public List<Reservaties> GetReservaties()
     {
         throw new NotImplementedException();
     }

     /// <summary>
        /// The GetVoertuigPrijsVolgensArragement.
        /// </summary>
        /// <param name="arragement">The arragement<see cref="Arragement"/>.</param>
        /// <param name="voertuig">The voertuig<see cref="Voertuig"/>.</param>
        /// <returns>The <see cref="KlantenCategorie"/>.</returns>
        public VoertuigPrijs GetVoertuigPrijsVolgensArragement(Arragement arragement, Voertuig voertuig)
        {

            var x = _context.Voertuigprijzen.Include(w => w.Arragement).Where(x =>
                 x.ArragementId == arragement.Id && x.VoertuigId.Value == voertuig.Id).FirstOrDefault();
            //       Debug.WriteLine($"Debug querry: id={x.ArragementId} compare to {arragement.Id} voertuig result={x.VoertuigId} compare to {voertuig.Id}");
            if (x == null)
            {
                return null;
            }
            return x;
        }

        /// <summary>
        /// The GetLocatie.
        /// </summary>
        /// <param name="LocatieNaam">The LocatieNaam<see cref="string"/>.</param>
        /// <returns>The <see cref="Locaties"/>.</returns>
        public Locaties GetLocatie(string LocatieNaam)
        {
            var locatie = _context.Locaties.SingleOrDefault(x => x.LocatieNaam == LocatieNaam);
            return locatie;
        }

        /// <summary>
        /// The GetLocaties.
        /// </summary>
        /// <returns>The <see cref="List{Locaties}"/>.</returns>
        public List<Locaties> GetLocaties()
        {
            var locatie = _context.Locaties.ToList();
            return locatie;
        }

        /// <summary>
        /// The GetKlant.
        /// </summary>
        /// <param name="klantNummer">The klantNummer<see cref="int"/>.</param>
        /// <returns>The <see cref="Klant"/>.</returns>
        public Klant GetKlant(int klantNummer)
        {
            //var klant = _context.Klanten.Where(kl => kl.Klantnummer == klantNummer).ToList().First();
            //var klant2 = _context.Klanten.FirstOrDefault(kl => kl.Klantnummer == klantNummer);
            var klant = _context.Klanten.Include(x => x.KlantenCategorie)
                            .SingleOrDefault(kl => kl.Klantnummer == klantNummer);
            return klant;
        }

        /// <summary>
        /// The SaveReservatie.
        /// </summary>
        /// <param name="reservatie">The reservatie<see cref="Reservaties"/>.</param>
        public void SaveReservatie(Reservaties reservatie) //todo: intigration test
        {
            var x = _context.Reservatieses.Add(reservatie);
            _context.SaveChanges();
        }

        /// <summary>
        /// The GetVoertuigById.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Voertuig"/>.</returns>
        public Voertuig GetVoertuigById(int id)
        {
            var x = _context.Voertuigen.Where(x => x.Id == id).FirstOrDefault();
            return x;
        }

        /// <summary>
        /// The GetReservatiesDoorKlantId.
        /// </summary>
        /// <param name="klant">The klant<see cref="Klant"/>.</param>
        /// <returns>The <see cref="List{Reservaties}"/>.</returns>
        public List<Reservaties> GetReservatiesDoorKlantId(Klant klant)
        {
            var x = _context.Reservatieses.Where(x => x.Klant.Id == klant.Id).ToList();
            return x;
        }
        public List<Reservaties> GetReservatiesDoorKlantIdEnDatum(Klant klant,DateTime date)
        {
            var x = _context.Reservatieses.Where(x => x.Klant.Id == klant.Id && x.StartDatum >= date).ToList();
            return x;
        }


        public List<Reservaties> GetReservatiesDoorDate(DateTime date)
        {
            var x = _context.Reservatieses.Where(x => x.StartDatum >= date).ToList();
            return x;
        }
        /// <summary>
        /// The GetVoertuigen.
        /// </summary>
        /// <returns>The <see cref="List{Voertuig}"/>.</returns>
        public List<Voertuig> GetVoertuigen()
        {
            var voertuigen = _context.Voertuigen//.Include(x => x.)
                .ToList();
            return voertuigen;
        }

        /// <summary>
        /// The GetKlanten.
        /// </summary>
        /// <returns>The <see cref="List{Klant}"/>.</returns>
        public List<Klant> GetKlanten()
        {
            var klanten = _context.Klanten.Include(k => k.KlantenCategorie)
                                                    .Include(k => k.KlantenCategorie.StaffelKortingType)
                                                    .ToList();
            return klanten;
        }

        /// <summary>
        /// The GetAlleArragementen.
        /// </summary>
        /// <returns>The <see cref="List{Arragement}"/>.</returns>
        public List<Arragement> GetAlleArragementen()
        {
            var a = _context.Arragements.ToList();
            return a;
        }

        /// <summary>
        /// The GetArragement.
        /// </summary>
        /// <param name="ArragementNaam">The ArragementNaam<see cref="string"/>.</param>
        /// <returns>The <see cref="Arragement"/>.</returns>
        public Arragement GetArragement(string ArragementNaam)
        {
            var a = _context.Arragements.SingleOrDefault(x => x.Naam == ArragementNaam);
            return a;
        }



        /// <summary>
        /// The GetStaffelKortings.
        /// </summary>
        /// <returns>The <see cref="List{StaffelKorting}"/>.</returns>
        /*
        public List<StaffelKorting> GetStaffelKortings()
        {
            var x = _context.StaffelKortings.Include(x => x.StaffelKortingType).ToList();
            return x;
        }
        */
        /// <summary>
        /// The GetVoertuigListById.
        /// </summary>
        /// <param name="voertuig">The voertuig<see cref="Voertuig"/>.</param>
        /// <param name="startDateTime">The startDateTime<see cref="DateTime"/>.</param>
        /// <param name="eindDateTime">The eindDateTime<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="List{StaffelKorting}"/>.</returns>
        public Reservaties GetLastReservaties(Voertuig voertuig, DateTime startDateTime, DateTime eindDateTime)
        {
            //old:  var x = _context.Reservatieses.Where(x => x.VoertuigId == voertuig.Id).OrderByDescending(x => x.EindDateTime).First();
            var x = _context.Reservatieses.Include(x => x.Voertuig).Include(x => x.Arragement)
                .Where(x => x.VoertuigId == voertuig.Id &&
                            ((eindDateTime >= x.StartDatum && startDateTime <= x.StartDatum) ||
                             (eindDateTime >= x.EindDateTime && startDateTime <= x.EindDateTime)))
                .OrderByDescending(x => x.EindDateTime).FirstOrDefault();

            if (x == null)
            {
                return null;
            }
            return x;
        }

        /// <summary>
        /// The GetKortingPercentageKlant.
        /// </summary>
        /// <param name="klant">The klant<see cref="Klant"/>.</param>
        /// <param name="date">The date<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public double GetKortingPercentageKlant(Klant klant, DateTime date)
        {
            var aantalReservaties = _context.Reservatieses.Where
                (x => x.StartDatum.Year == date.Year && x.KlantId == klant.Id)
                .Count();
            if (aantalReservaties == 0)
            {
                return 0;
            }

            var x = _context.StaffelKortings.Where(x => x.StaffelKortingTypeId == klant.KlantenCategorie.StaffelKortingTypeId && x.Aantal <= aantalReservaties).ToList().LastOrDefault();
            
            if (x == null)
            {
                return 0;
            }

            return x.Korting;
        }

        /*
        /// <summary>
        /// The GetReservaties.
        /// </summary>
        /// <returns>The <see cref="List{Reservaties}"/>.</returns>
        public List<Reservaties> GetReservaties()
        {
            var r = _context.Reservatieses.Include(k => k.KlantId).Include(x => x.StartLocatie).Include(
                x => x.EindLocatie
            ).Include(v => v.VoertuigId).Include(a => a.ArragementId).ToList();
            return r;
        }
        */
    }
}
