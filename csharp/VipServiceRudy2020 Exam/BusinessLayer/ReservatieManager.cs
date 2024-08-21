namespace BusinessLayer
{
    using DataLayer;
    using Entiteiten;
    using Entiteiten.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Xceed.Wpf.Toolkit;

    /// <summary>
    /// Defines the <see cref="ReservatieManager" />.
    /// </summary>
    public class ReservatieManager
    {
        /// <summary>
        /// Defines the _vipServiceDal.
        /// </summary>
        private IVipServiceRepository _vipServiceDal;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservatieManager"/> class.
        /// </summary>
        public ReservatieManager()
        {
            _vipServiceDal = new VipServiceRepository(); //note: change me
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservatieManager"/> class.
        /// </summary>
        /// <param name="vipServiceDal">The vipServiceDal<see cref="IVipServiceRepository"/>.</param>
        public ReservatieManager(IVipServiceRepository vipServiceDal)
        {
            _vipServiceDal = vipServiceDal;
        }

        /// <summary>
        /// The GetLocatieByNaam.
        /// </summary>
        /// <param name="locatieNaam">The locatieNaam<see cref="string"/>.</param>
        /// <returns>The <see cref="Locaties"/>.</returns>
        public Locaties GetLocatieByNaam(string locatieNaam)
        {
            return _vipServiceDal.GetLocatie(locatieNaam);
        }

        /// <summary>
        /// The GetLocaties.
        /// </summary>
        /// <returns>The <see cref="List{Locaties}"/>.</returns>
        public List<Locaties> GetLocaties()
        {
            return _vipServiceDal.GetLocaties();
        }

        /// <summary>
        /// The GetKlantByKlantNr.
        /// </summary>
        /// <param name="klantnummer">The klantnummer<see cref="int"/>.</param>
        /// <returns>The <see cref="Klant"/>.</returns>
        public Klant GetKlantByKlantNr(int klantnummer)
        {
            return _vipServiceDal.GetKlant(klantnummer);
        }

        /// <summary>
        /// The GetKortingPercentageKlant.
        /// </summary>
        /// <param name="klant">The klant<see cref="Klant"/>.</param>
        /// <param name="reservatie">The reservatie<see cref="Reservaties"/>.</param>
        /// <returns>The <see cref="decimal"/>.</returns>
        public double GetKortingPercentageKlant(Klant klant, DateTime reservatie)
        {
            return _vipServiceDal.GetKortingPercentageKlant(klant, reservatie);
        }

        /// <summary>
        /// The GetVoertuigen.
        /// </summary>
        /// <returns>The <see cref="List{Voertuig}"/>.</returns>
        public List<Voertuig> GetVoertuigen()
        {
            return _vipServiceDal.GetVoertuigen();
        }

        /// <summary>
        /// The GetAlleArragementen.
        /// </summary>
        /// <returns>The <see cref="List{Arragement}"/>.</returns>
        public List<Arragement> GetAlleArragementen()
        {
            return _vipServiceDal.GetAlleArragementen();
        }

        /// <summary>
        /// The GetArregement.
        /// </summary>
        /// <param name="arrgementNaam">The arrgementNaam<see cref="string"/>.</param>
        /// <returns>The <see cref="Arragement"/>.</returns>
        public Arragement GetArregement(string arrgementNaam)
        {
            return _vipServiceDal.GetArragement(arrgementNaam);
        }

        /// <summary>
        /// The GetKlanten.
        /// </summary>
        /// <returns>The <see cref="List{Klant}"/>.</returns>
        public List<Klant> GetKlanten()
        {
            return _vipServiceDal.GetKlanten();
        }



        /// <summary>
        /// The GetStaffelKortings.
        /// </summary>
        /// <returns>The <see cref="List{StaffelKorting}"/>.</returns>
        /*
        public List<StaffelKorting> GetStaffelKortings()
        {
            return _vipServiceDal.GetStaffelKortings();
        }*/

        /// <summary>
        /// The GetVoertuigPrijsVolgensArragement.
        /// </summary>
        /// <param name="arragement">The arragement<see cref="Arragement"/>.</param>
        /// <param name="voertuig">The voertuig<see cref="Voertuig"/>.</param>
        /// <returns>The <see cref="List{StaffelKorting}"/>.</returns>
        public VoertuigPrijs GetVoertuigPrijsVolgensId(Arragement arragement, Voertuig voertuig)
        {
            return _vipServiceDal.GetVoertuigPrijsVolgensArragement(arragement, voertuig);
        }

        /// <summary>
        /// The SaveReservatie.
        /// </summary>
        /// <param name="reservatie">The reservatie<see cref="Reservaties"/>.</param>
        public void SaveReservatie(Reservaties reservatie)
        {
            _vipServiceDal.SaveReservatie(reservatie);
        }

        /// <summary>
        /// The GetVoertuigById.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Voertuig"/>.</returns>
        public Voertuig GetVoertuigById(int id)
        {
            return _vipServiceDal.GetVoertuigById(id);
        }

        /// <summary>
        /// The GetLastReservaties.
        /// </summary>
        /// <param name="voertuig">The voertuig<see cref="Voertuig"/>.</param>
        /// <param name="startDateTime">The startDateTime<see cref="DateTime"/>.</param>
        /// <param name="eindDateTime">The eindDateTime<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="Reservaties"/>.</returns>
        public Reservaties GetLastReservaties(Voertuig voertuig, DateTime startDateTime, DateTime eindDateTime)
        {
            return _vipServiceDal.GetLastReservaties(voertuig, startDateTime, eindDateTime);
        }

        /// <summary>
        /// The GetReservatiesDoorKlantId.
        /// </summary>
        /// <param name="klant">The klant<see cref="Klant"/>.</param>
        /// <returns>The <see cref="List{Reservaties}"/>.</returns>
        public List<Reservaties> GetReservatiesDoorKlantId(Klant klant)
        {
            return _vipServiceDal.GetReservatiesDoorKlantId(klant);
        }

        /// <summary>
        /// The GetReservatiesDoorDate.
        /// </summary>
        /// <param name="date">The date<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="List{Reservaties}"/>.</returns>
        public List<Reservaties> GetReservatiesDoorDate(DateTime date)
        {
            return _vipServiceDal.GetReservatiesDoorDate(date);
        }
        public List<Reservaties> GetReservatiesDoorKlantIdEnDatum(Klant klant,DateTime date)
        {
            return _vipServiceDal.GetReservatiesDoorKlantIdEnDatum(klant,date);
        }
        

        /// <summary>
        /// The HandleTheReservatie.
        /// </summary>
        /// <param name="arragement">The arragement<see cref="Arragement"/>.</param>
        /// <param name="startTijd">The startTijd<see cref="DateTimePicker"/>.</param>
        /// <param name="huurtijd">The huurtijd<see cref="int"/>.</param>
        /// <param name="klant">The klant<see cref="Klant"/>.</param>
        /// <param name="auto">The auto<see cref="Voertuig"/>.</param>
        /// <param name="StartLocatie">The StartLocatie<see cref="Locaties"/>.</param>
        /// <param name="EindLocatie">The EindLocatie<see cref="Locaties"/>.</param>
        /// <returns>The <see cref="Reservaties"/>.</returns>
        public Reservaties HandleTheReservatie(Arragement arragement, DateTime startTijd, int huurtijd,
            Klant klant, Voertuig auto, Locaties StartLocatie, Locaties EindLocatie)
        {

            double prijs = 0;
            DateTime eindTijd = new DateTime();
            DateTime tijdTussen = new DateTime();

            eindTijd = startTijd;
            tijdTussen = startTijd;

            eindTijd = eindTijd.AddHours(huurtijd);

            var OnsArregement = GetArregement(arragement.Naam);

            var nachturen = 0;
            int overuren = 0;

            var x = (huurtijd - OnsArregement.Aantal_uur); // overenuren bereking
            Debug.WriteLine($"Overuren = {x}");
            TimeSpan diff = (TimeSpan)(eindTijd - startTijd);// eerst gaan we tijd bereken voor onze eerste check,


            CheckOveruren(x);

            CheckForLongerThan11hour(huurtijd); // Geen enkel arrgement mag langer zijn dan 11 uur

            switch (arragement.Naam)
            {
                case Configuration.WeddingServiceName:
                    // check of je het echt wel kan huren
                    var voertuigPrijs = GetVoertuigPrijsVolgensId(arragement, auto);

                    if (voertuigPrijs == null)
                    {
                        throw new ArgumentException($"Je kan het voertuig {auto.Naam} niet selecteren voor {arragement.Naam}");

                    }
                    prijs += voertuigPrijs.Prijs;
                    if (StartArgementCheck(arragement, tijdTussen.Hour)) //todo: fixen
                    {
                        for (int i = 0; i < diff.Hours; i++)
                        {
                            if (IsHetEenNachuur(tijdTussen))
                            {
                                prijs += ReturnNachtUurPrijs(auto);
                                nachturen++;
                            }
                            tijdTussen = tijdTussen.AddHours(1);
                        }

                        for (int i = 0; i < x; i++)
                        {
                            Debug.WriteLine($"overuren bereken: {x}");

                            if (IsHetEenNachuur(tijdTussen))
                            {

                                prijs += ReturnNachtUurPrijs(auto);
                                nachturen++;
                            }
                            else
                            {
                                prijs += (Double)BerkenPercentageVanAutoPrijs(auto, Configuration.PercentTweedeUur);
                            }

                            overuren++;
                            tijdTussen = tijdTussen.AddHours(1);
                        }
                    }

                    break;

                case Configuration.NightLifeServiceName:
                    {
                        voertuigPrijs = GetVoertuigPrijsVolgensId(arragement, auto);
                        if (voertuigPrijs == null)
                        {
                            throw new ArgumentException($"Je kan het voertuig {auto.Naam} niet selecteren voor {arragement.Naam}");

                        }
                        prijs += voertuigPrijs.Prijs;
                        if (StartArgementCheck(arragement, tijdTussen.Hour))
                        {

                            for (int i = 0; i < diff.Hours; i++)
                            {
                                if (!IsHetEenNachuur(tijdTussen))
                                {
                                    prijs += ReturnNachtUurPrijs(auto);
                                    Debug.WriteLine($"pr/n{tijdTussen.Hour}");;
                                    nachturen++;
                                }

                                tijdTussen = tijdTussen.AddHours(1);
                            }
                            for (int i = 0; i < x; i++)
                            {
                                overuren++;
                                Debug.WriteLine($"force overuur als nachtuur: {tijdTussen} {tijdTussen.Hour}");
                                prijs += ReturnNachtUurPrijs(auto);


                                tijdTussen = tijdTussen.AddHours(1);
                            }

                        }

                        break;
                    }

                case Configuration.BusinessSericeName:
                case Configuration.AirportServiceName:

                    // prijs per uur berekenen en dan checken of het een nachtuur is of niet
                    for (int i = 0; i < diff.Hours; i++)
                    {

                        switch (i)
                        {
                            case 0:
                                prijs += BerkenPercentageVanAutoPrijs(auto, Configuration.NachtuurPercentVoorHetEersteUur);
                                break;
                            default:
                                {
                                    if (IsHetEenNachuur(tijdTussen))
                                    {
                                        nachturen++;
                                        prijs += BerkenPercentageVanAutoPrijs(auto, Configuration.NachtuurPercentVoorHetEersteUur);
                                    }
                                    else
                                    {

                                        prijs += BerkenPercentageVanAutoPrijs(auto, Configuration.PercentTweedeUur);
                                    }
                                }
                                break;
                        }
                        tijdTussen = tijdTussen.AddHours(1);
                    }
                    break;
                case Configuration.WellnessServiceName:
                    CheckIfWellnessIs10Hours(arragement, diff.Hours);
                    voertuigPrijs = GetVoertuigPrijsVolgensId(arragement, auto);

                    if (voertuigPrijs == null)
                    {
                        throw new ArgumentException($"Je kan het voertuig {auto.Naam} niet selecteren voor {arragement.Naam}");

                    }
                    prijs += voertuigPrijs.Prijs;
                    if (StartArgementCheck(arragement, tijdTussen.Hour))
                    {
                        for (int i = 0; i <= diff.Hours; i++)
                        {
                            if (IsHetEenNachuur(tijdTussen))
                            {
                                prijs += ReturnNachtUurPrijs(auto);
                                nachturen++;
                            }

                            tijdTussen = tijdTussen.AddHours(1);
                        }


                    }
                    break;
            }
            var TotaalMetBTW = BerekenPrijsMetBtw(prijs);
            double korting = GetKortingPercentageKlant(klant, startTijd);
            double KortingAftrekken = berekenPercentage(TotaalMetBTW, korting);
            var r = new Reservaties()
            {
                KlantId = klant.Klantnummer,
                Arragement = arragement,
                Voertuig = auto,
                StartDatum = startTijd,
                Adress = null,
                Klant = klant,
                StartLocatie = StartLocatie,
                StartLocatieId = StartLocatie.Id,
                EindLocatie = EindLocatie,
                EindDateTime = eindTijd,
                EindLocatieId = EindLocatie.Id,
                
                ArragementId = arragement.Id,
                VoertuigId = auto.Id,
                TotaalOveruren = overuren,
                Nachtuur = ReturnNachtUurPrijs(auto),
                TotaalNachtUren = nachturen,
                Korting = korting,
                TotaalExclusiefBtw = prijs,
                TotaalInclusiefBtw = TotaalMetBTW,
                TotaalBedrag = (TotaalMetBTW - KortingAftrekken),
            };

            if (!Check6UurTussenReservatie(r))
            {

                throw new ArgumentException("Deze reservatie zit te dicht op een andere reservatie");
            }




            //    SaveReservatie(r);
            Debug.WriteLine($"Reservering totaaal = {r.TotaalBedrag}");
            return r;
        }

        /// <summary>
        /// The IsHetEenNachuur.
        /// </summary>
        /// <param name="date">The date<see cref="DateTimePicker"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsHetEenNachuur(DateTime date)
        {
            if (date.Hour >= Configuration.NachtuurStart || date.Hour < Configuration.NachtuurStop)
            {
                Debug.WriteLine($"is een nachtuur {date.Hour}");
                return true;
            }
             Debug.WriteLine($"is geen nachtuur {date.Hour}");
            return false;
        }

        /// <summary>
        /// The BerekenPrijsMetBtw.
        /// </summary>
        /// <param name="prijs">The prijs<see cref="decimal"/>.</param>
        /// <returns>The <see cref="decimal"/>.</returns>
        public double BerekenPrijsMetBtw(double prijs)
        {

            var x = (prijs / Configuration.MaxPercent) * Configuration.BTWPct;
            return (prijs + x);
        }

        /// <summary>
        /// The ReturnNachtUurPrijs.
        /// </summary>
        /// <param name="voertuig">The voertuig<see cref="Voertuig"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int ReturnNachtUurPrijs(Voertuig voertuig)
        {
            int prijs = 0;
            prijs += VeelvoudHandler(BerkenPercentageVanAutoPrijs(voertuig, Configuration.NachtuurPercentVoorHetEersteUur));
            return prijs;
        }

        /// <summary>
        /// The berekenPercentage.
        /// </summary>
        /// <param name="getal">The getal<see cref="double"/>.</param>
        /// <param name="percentage">The percentage<see cref="double"/>.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public double berekenPercentage(double getal, double percentage)
        {
            double total = 0;
            total += (getal / Configuration.MaxPercent) * percentage;
            return total;
        }

        /// <summary>
        /// The BerkenPercentageVanAutoPrijs.
        /// </summary>
        /// <param name="auto">The auto<see cref="Voertuig"/>.</param>
        /// <param name="percentage">The percentage<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public double BerkenPercentageVanAutoPrijs(Voertuig auto, int percentage)
        {
            var x = (double)(auto.EersteUur / 100) * percentage;
            Debug.WriteLine($"prijs per uur={auto.EersteUur}| percentage is = {percentage} prijs na percentage={x}");
            return x;
        }

        /// <summary>
        /// The CheckOveruren.
        /// </summary>
        /// <param name="overuren">The overuren<see cref="int"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool CheckOveruren(int? overuren)
        {
            if (overuren == null)
            {
                return false;
            }

            if (overuren <= Configuration.MaxOveruren)
            {
                return false;
            }
            else
            {
                throw
                    new ArgumentException(
                        "Teveel overenuren");
            }

            return true;
        }

        /// <summary>
        /// The Check6UurTussenReservatie.
        /// </summary>
        /// <param name="reservaties">The reservaties<see cref="Reservaties"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Check6UurTussenReservatie(Reservaties reservaties)
        {
            if (reservaties == null)
            {
                return false;
            }

            var eind = reservaties.EindDateTime.AddHours(6);
            var start = reservaties.StartDatum.AddHours(-6);
            var laaste = GetLastReservaties(reservaties.Voertuig, start, eind);
            return (laaste == null);
        }

        /// <summary>
        /// The VeelvoudHandler.
        /// </summary>
        /// <param name="number">The number<see cref="decimal"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int VeelvoudHandler(double number)
        {
            var result = Convert.ToInt32(Math.Ceiling((number / Configuration.Veelvoud) * Configuration.Veelvoud));
            return result;
        }

        /// <summary>
        /// The ArragementCheckerBetweenHours.
        /// </summary>
        /// <param name="arragement">The arragement<see cref="Arragement"/>.</param>
        /// <param name="start_uur">The start_uur<see cref="int"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool StartArgementCheck(Arragement arragement, int start_uur)
        {
            var arg = GetArregement(arragement.Naam);
            if (arg.Min_start <= start_uur && arg.Max_start >= start_uur) // wedding moet tussen 7 en 15 liggen 
            {
                return true; // het is een wedding
            }
            else
            {
                throw new ArgumentException($"arragement begint niet om: {arg.Min_start}->{arg.Max_start} maar het begint om {start_uur}");
            }
        }

        /// <summary>
        /// The CheckForLongerThan11hour.
        /// </summary>
        /// <param name="duur">The duur<see cref="int"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool CheckForLongerThan11hour(int? duur)
        {
            if (duur > Configuration.MaxArragementTime) // 11
            {
                throw new ArgumentException(
                    $"Reservering is te lang! Het mag niet langer zijn dan 11 uur, het is nu {duur} uur lang");
            }

            return false;
        }

        /// <summary>
        /// The CheckIfWellnessIs10Hours.
        /// </summary>
        /// <param name="arragement">The arragement<see cref="Arragement"/>.</param>
        /// <param name="duur">The duur<see cref="int"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool CheckIfWellnessIs10Hours(Arragement arragement, int duur)
        {
            if (arragement.Naam == Configuration.WellnessServiceName && duur == Configuration.WellnessTime)
            {
                return true;
            }
            else
            {
                throw new ArgumentException($"Wellness moet 10 altijd 10 uur zijn niet meer niet minder, Nu is het {duur}");

            }

            return false;
        }
    }
}
