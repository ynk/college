namespace TestVipService
{
    using BusinessLayer;
    using DataLayer;
    using Entiteiten;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /// <summary>
    /// Defines the <see cref="Integrationtests" />.
    /// </summary>
    [TestClass]
    public class Integrationtests
    {
        /// <summary>
        /// Defines the _reservatieManager.
        /// </summary>
        private ReservatieManager _reservatieManager = new ReservatieManager(new VipServiceRepository("Test"));

        /// <summary>
        /// The GetKlantByKlantNr_Success.
        /// </summary>
        [TestMethod]
        public void GetKlantByKlantNr_Success()
        {

            var k = _reservatieManager.GetKlantByKlantNr(5);

            Assert.AreEqual(5, k.Klantnummer);
            Assert.AreEqual(10, k.Id);
            Assert.AreEqual("Gaia", k.Naam);
            Assert.AreEqual("organisatie", k.KlantenCategorie.Naam);
        }

        /// <summary>
        /// The GetLocatieByNameWhereNameIsGent.
        /// </summary>
        [TestMethod]
        public void GetLocatieByNameWhereNameIsGent()
        {
            var l = _reservatieManager.GetLocatieByNaam("Gent");

            Assert.AreEqual("Gent", l.LocatieNaam);
        }

        /// <summary>
        /// The Check6UurTussenReservatie_ReturnTrue_ShouldPass.
        /// </summary>
        [TestMethod]
        public void Check6UurTussenReservatie_ReturnTrue_ShouldPass()
        {
            Reservaties r = new Reservaties()
            {
                StartDatum = new DateTime(2020, 8, 11, 23, 45, 59),
                EindDateTime = new DateTime(2020, 8, 11, 23, 45, 59),
                Voertuig = new Voertuig()
                {
                    EersteUur = 350,
                    Id = 6
                },
            };
            var x = _reservatieManager.Check6UurTussenReservatie(r);
            Assert.AreEqual(x, true);
        }

        /// <summary>
        /// The StartArgementCheckWedding_ReturnTrue_ShouldPass.
        /// </summary>
        [TestMethod]
        public void StartArgementCheckWedding_ReturnTrue_ShouldPass()
        {
            Arragement start = new Arragement()
            {
                Naam = "Wedding"
            };
            var x = _reservatieManager.StartArgementCheck(start, 14); // 
            Assert.AreEqual(x, true);
        }

        /// <summary>
        /// The StartArgementCheckWedding_ReturnFalse_ShouldPass.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StartArgementCheckWedding_ReturnFalse_ShouldPass()
        {
            Arragement start = new Arragement()
            {
                Naam = "Wedding"
            };
            var x = _reservatieManager.StartArgementCheck(start, 18); // 
        }

        /// <summary>
        /// The GetVoertuigPrijsVolgensArragement_returnVoertuig_ShouldPass.
        /// </summary>
        [TestMethod]

        public void GetVoertuigPrijsVolgensArragement_returnVoertuig_ShouldPass()
        {
            Arragement arragement = new Arragement()
            {
                Id = 6,
                Naam = "Wedding"
            };
            Voertuig voertuig = new Voertuig()
            {
                Id = 15
            };
            var x = _reservatieManager.GetVoertuigPrijsVolgensId(arragement, voertuig);
            Assert.AreEqual(500, x.Prijs);
        }

        /// <summary>
        /// The GetVoertuigPrijsVolgensArragement_returnNull_ShouldPass.
        /// </summary>
        [TestMethod]

        public void GetVoertuigPrijsVolgensArragement_returnNull_ShouldPass()
        {
            Arragement arragement = new Arragement()
            {
                Id = 6,
                Naam = "Wedding"
            };
            Voertuig voertuig = new Voertuig()
            {
                Id = 18 //n.v.t op het papier
            };
            var x = _reservatieManager.GetVoertuigPrijsVolgensId(arragement, voertuig);
            Assert.AreEqual(null, x);
        }

        /// <summary>
        /// The GetLocaties_CountShouldBe5_ShouldPass.
        /// </summary>
        [TestMethod]
        public void GetLocaties_CountShouldBe5_ShouldPass()
        {
            var x = _reservatieManager.GetLocaties().Count;
            Assert.AreEqual(5, x);
        }

        /// <summary>
        /// The GetVoertuigen_CountShouldBe5_ShouldPass.
        /// </summary>
        [TestMethod]
        public void GetVoertuigen_CountShouldBe5_ShouldPass()
        {
            var x = _reservatieManager.GetVoertuigen().Count;
            Assert.AreEqual(20, x);
        }

        /// <summary>
        /// The GetKlanten_CountShouldBe5_ShouldPass.
        /// </summary>
        [TestMethod]
        public void GetKlanten_CountShouldBe5_ShouldPass()
        {
            var x = _reservatieManager.GetKlanten().Count;
            Assert.AreEqual(23, x);
        }

        /// <summary>
        /// The GetAlleArragementen_CountShouldBe5_ShouldPass.
        /// </summary>
        [TestMethod]
        public void GetAlleArragementen_CountShouldBe5_ShouldPass()
        {
            var x = _reservatieManager.GetAlleArragementen().Count;
            Assert.AreEqual(5, x);
        }

        /// <summary>
        /// The GetKlantById_ShouldPass.
        /// </summary>
        [TestMethod]
        public void GetKlantById_ShouldPass()
        {
            var x = _reservatieManager.GetKlantByKlantNr(2);
            Assert.AreEqual("De Pesser Jean", x.Naam);
        }

        /// <summary>
        /// The GetVoertuigById_ShouldPass.
        /// </summary>
        [TestMethod]
        public void GetVoertuigById_ShouldPass()
        {
            var x = _reservatieManager.GetVoertuigById(23);
            Assert.AreEqual("Chrysler 300C Limousine - Tuxedo Crème", x.Naam);
            Assert.AreEqual(23, x.Id);
            Assert.AreEqual(180, x.EersteUur);
        }

        /// <summary>
        /// The GetReservatiesDoorKlantId_ReturnCount.
        /// </summary>
        [TestMethod]
        public void GetReservatiesDoorKlantId_ReturnCount()
        {
            var k = _reservatieManager.GetKlantByKlantNr(1);
            var x = _reservatieManager.GetReservatiesDoorKlantId(k);

            Assert.AreEqual(10, x.Count);
        }

        /// <summary>
        /// The GetReservatiesDoorDoorDate_ReturnCount.
        /// </summary>
        [TestMethod]
        public void GetReservatiesDoorDoorDate_ReturnCount()
        {
            var k = _reservatieManager.GetReservatiesDoorDate(new DateTime(2130, 3, 11, 13, 59, 59));


            Assert.AreEqual(3, k.Count);
        }

        /// <summary>
        /// The GetKortingPercentageKlant_Return5.
        /// </summary>
        [TestMethod]
        public void GetKortingPercentageKlant_Return5()
        {
            var klant = _reservatieManager.GetKlantByKlantNr(3);
            var date = new DateTime(2020, 8, 20);
            var korting = _reservatieManager.GetKortingPercentageKlant(klant, date);



            Assert.AreEqual(5, korting);
        }

        /// <summary>
        /// The HandleTheReservatie_ReturnWellnessReservatie.
        /// </summary>
        [TestMethod]
        public void HandleTheReservatie_ReturnWellnessReservatie()
        {
            var locatie = _reservatieManager.GetLocatieByNaam("Gent");
            var klant = _reservatieManager.GetKlantByKlantNr(1);
            var auto = _reservatieManager.GetVoertuigById(6);
            var arr = _reservatieManager.GetArregement("Wellness");
            var date = new DateTime(2020, 8, 25, 10, 00, 00);
            int huurtijd = 10;
            var x = _reservatieManager.HandleTheReservatie(arr, date, huurtijd, klant, auto, locatie, locatie);

            Assert.AreEqual(1060, x.TotaalBedrag);
            Assert.AreEqual(0, x.Korting); //heeft geen korting op deze reservatie!
        }

        /// <summary>
        /// The HandleTheReservatie_ReturnWellnessReservatie_metKorting.
        /// </summary>
        [TestMethod]
        public void HandleTheReservatie_ReturnWellnessReservatie_metKorting()
        {
            var locatie = _reservatieManager.GetLocatieByNaam("Gent");
            var klant = _reservatieManager.GetKlantByKlantNr(3);
            var auto = _reservatieManager.GetVoertuigById(6);
            var arr = _reservatieManager.GetArregement("Wellness");
            var date = new DateTime(2020, 8, 25, 10, 00, 00);
            int huurtijd = 10;
            var x = _reservatieManager.HandleTheReservatie(arr, date, huurtijd, klant, auto, locatie, locatie);

            Assert.AreEqual(1007, x.TotaalBedrag);
            Assert.AreEqual(5, x.Korting);
        }

        /// <summary>
        /// The HandleTheReservatie_ReturnWellnessReservatie_CatchExcpetion.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HandleTheReservatie_ReturnWellnessReservatie_CatchExcpetion()
        {
            var locatie = _reservatieManager.GetLocatieByNaam("Gent");
            var klant = _reservatieManager.GetKlantByKlantNr(1);
            var auto = _reservatieManager.GetVoertuigById(6);
            var arr = _reservatieManager.GetArregement("Wellness");
            var date = new DateTime(2020, 8, 25, 10, 00, 00);
            int huurtijd = 1;
            var x = _reservatieManager.HandleTheReservatie(arr, date, huurtijd, klant, auto, locatie, locatie);
        }

        /// <summary>
        /// The HandleTheReservatie_ReturnAirportReservatie.
        /// </summary>
        [TestMethod]
        public void HandleTheReservatie_ReturnAirportReservatie()
        {
            var locatie = _reservatieManager.GetLocatieByNaam("Gent");
            var klant = _reservatieManager.GetKlantByKlantNr(1);
            var auto = _reservatieManager.GetVoertuigById(6);
            var arr = _reservatieManager.GetArregement("Airport");
            var date = new DateTime(2020, 8, 25, 10, 00, 00);
            int huurtijd = 10;
            var x = _reservatieManager.HandleTheReservatie(arr, date, huurtijd, klant, auto, locatie, locatie);

            Assert.AreEqual(1537, x.TotaalBedrag);
            Assert.AreEqual(0, x.Korting); //heeft geen korting op deze reservatie!
        }

        /// <summary>
        /// The HandleTheReservatie_ReturnAirportReservatie_MetKorting.
        /// </summary>
        [TestMethod]
        public void HandleTheReservatie_ReturnAirportReservatie_MetKorting()
        {
            var locatie = _reservatieManager.GetLocatieByNaam("Gent");
            var klant = _reservatieManager.GetKlantByKlantNr(3);
            var auto = _reservatieManager.GetVoertuigById(6);
            var arr = _reservatieManager.GetArregement("Airport");
            var date = new DateTime(2020, 8, 25, 10, 00, 00);
            int huurtijd = 10;
            var x = _reservatieManager.HandleTheReservatie(arr, date, huurtijd, klant, auto, locatie, locatie);

            Assert.AreEqual(1460.15, x.TotaalBedrag);
            Assert.AreEqual(5, x.Korting); //heeft geen korting op deze reservatie!
        }

        /// <summary>
        /// The HandleTheReservatie_VoertuigNietvantoepassing_TrhowExpection.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HandleTheReservatie_VoertuigNietvantoepassing_TrhowExpection()
        {
            var locatie = _reservatieManager.GetLocatieByNaam("Gent");
            var klant = _reservatieManager.GetKlantByKlantNr(3);
            var auto = _reservatieManager.GetVoertuigById(6);
            var arr = _reservatieManager.GetArregement("NightLife");
            var date = new DateTime(2020, 8, 25, 10, 00, 00);
            int huurtijd = 7;
            var x = _reservatieManager.HandleTheReservatie(arr, date, huurtijd, klant, auto, locatie, locatie);
        }

        /// <summary>
        /// The HandleTheReservatie_ReturnNightLife_GeenNachuren.
        /// </summary>
        [TestMethod]

        public void HandleTheReservatie_ReturnNightLife_GeenNachuren()
        {
            var locatie = _reservatieManager.GetLocatieByNaam("Gent");
            var klant = _reservatieManager.GetKlantByKlantNr(3);
            var auto = _reservatieManager.GetVoertuigById(14);
            var arr = _reservatieManager.GetArregement("NightLife");
            var date = new DateTime(2020, 8, 25, 22, 00, 00);
            int huurtijd = 7;
            var x = _reservatieManager.HandleTheReservatie(arr, date, huurtijd, klant, auto, locatie, locatie);

            Assert.AreEqual(790, x.TotaalExclusiefBtw);
        }

        /// <summary>
        /// The HandleTheReservatie_ReturnNightLife_MetNachuren.
        /// </summary>
        [TestMethod]

        public void HandleTheReservatie_ReturnNightLife_MetNachuren()
        {
            var locatie = _reservatieManager.GetLocatieByNaam("Gent");
            var klant = _reservatieManager.GetKlantByKlantNr(3);
            var auto = _reservatieManager.GetVoertuigById(14);
            var arr = _reservatieManager.GetArregement("NightLife");
            var date = new DateTime(2020, 8, 25, 21, 00, 00);
            int huurtijd = 7;
            var x = _reservatieManager.HandleTheReservatie(arr, date, huurtijd, klant, auto, locatie, locatie);

            Assert.AreEqual(1070, x.TotaalExclusiefBtw);
        }

        /// <summary>
        /// The HandleTheReservatie_ReturnWedding_ZonderOveruren.
        /// </summary>
        [TestMethod]

        public void HandleTheReservatie_ReturnWedding_ZonderOveruren()
        {
            var locatie = _reservatieManager.GetLocatieByNaam("Gent");
            var klant = _reservatieManager.GetKlantByKlantNr(3);
            var auto = _reservatieManager.GetVoertuigById(14);
            var arr = _reservatieManager.GetArregement("Wedding");
            var date = new DateTime(2020, 8, 25, 10, 00, 00);
            int huurtijd = 7;
            var x = _reservatieManager.HandleTheReservatie(arr, date, huurtijd, klant, auto, locatie, locatie);

            Assert.AreEqual(650, x.TotaalExclusiefBtw);
        }

        /// <summary>
        /// The HandleTheReservatie_ReturnWedding_metOveruren.
        /// </summary>
        [TestMethod]

        public void HandleTheReservatie_ReturnWedding_metOveruren()
        {
            var locatie = _reservatieManager.GetLocatieByNaam("Gent");
            var klant = _reservatieManager.GetKlantByKlantNr(3);
            var auto = _reservatieManager.GetVoertuigById(14);
            var arr = _reservatieManager.GetArregement("Wedding");
            var date = new DateTime(2020, 8, 25, 10, 00, 00);
            int huurtijd = 11;
            var x = _reservatieManager.HandleTheReservatie(arr, date, huurtijd, klant, auto, locatie, locatie);

            Assert.AreEqual(1620, x.TotaalExclusiefBtw);
        }

        /// <summary>
        /// The HandleTheReservatie_ReturnWedding_ThrowExcpetion.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HandleTheReservatie_ReturnWedding_ThrowExcpetion()
        {
            var locatie = _reservatieManager.GetLocatieByNaam("Gent");
            var klant = _reservatieManager.GetKlantByKlantNr(3);
            var auto = _reservatieManager.GetVoertuigById(14);
            var arr = _reservatieManager.GetArregement("Wedding");
            var date = new DateTime(2020, 8, 25, 16, 00, 00);
            int huurtijd = 11;
            var x = _reservatieManager.HandleTheReservatie(arr, date, huurtijd, klant, auto, locatie, locatie);
        }

        /// <summary>
        /// The HandleTheReservatie_ReturnBussiness.
        /// </summary>
        [TestMethod]
        public void HandleTheReservatie_ReturnBussiness()
        {
            var locatie = _reservatieManager.GetLocatieByNaam("Gent");
            var klant = _reservatieManager.GetKlantByKlantNr(3);
            var auto = _reservatieManager.GetVoertuigById(14);
            var arr = _reservatieManager.GetArregement("Business");
            var date = new DateTime(2020, 8, 25, 10, 00, 00);
            int huurtijd = 11;
            var x = _reservatieManager.HandleTheReservatie(arr, date, huurtijd, klant, auto, locatie, locatie);

            Assert.AreEqual(1580, x.TotaalExclusiefBtw);
        }

        /// <summary>
        /// The HandleTheReservatie_ReturnBussiness_metNachuren.
        /// </summary>
        [TestMethod]
        public void HandleTheReservatie_ReturnBussiness_metNachuren()
        {
            var locatie = _reservatieManager.GetLocatieByNaam("Gent");
            var klant = _reservatieManager.GetKlantByKlantNr(3);
            var auto = _reservatieManager.GetVoertuigById(14);
            var arr = _reservatieManager.GetArregement("Business");
            var date = new DateTime(2020, 8, 25, 23, 00, 00);
            int huurtijd = 11;
            var x = _reservatieManager.HandleTheReservatie(arr, date, huurtijd, klant, auto, locatie, locatie);

            Assert.AreEqual(2630, x.TotaalExclusiefBtw);
        }

        [TestMethod]
        public void GetReservatiesDoorKlantIdEnDatum_ReturnCount0()
        {
            var klant = _reservatieManager.GetKlantByKlantNr(3);
            var date = new DateTime(2020, 8, 25, 23, 00, 00);

            var x = _reservatieManager.GetReservatiesDoorKlantIdEnDatum(klant, date);

            Assert.AreEqual(0, x.Count);
        }
        [TestMethod]
        public void GetReservatiesDoorKlantIdEnDatum_ReturnCount3()
        {
            var klant = _reservatieManager.GetKlantByKlantNr(1);
            var date = new DateTime(2020, 8, 25, 23, 00, 00);

            var x = _reservatieManager.GetReservatiesDoorKlantIdEnDatum(klant, date);

            Assert.AreEqual(3, x.Count);
        }
        [TestMethod]
        public void GetKortingPercentageKlant_whereAantalReservatieIs0()
        {
            var klant = _reservatieManager.GetKlantByKlantNr(9);
            var date = new DateTime(2020, 8, 25, 23, 00, 00);

            var x = _reservatieManager.GetKortingPercentageKlant(klant, date);

            Assert.AreEqual(0, x);
        }
   
    }
}
