using Xceed.Wpf.Toolkit;

namespace TestVipService
{
    using BusinessLayer;
    using Entiteiten;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /// <summary>
    /// Defines the <see cref="Unittests" />.
    /// </summary>
    [TestClass]
    public class Unittests
    {
        /// <summary>
        /// Defines the _reservatieManager.
        /// </summary>
        private ReservatieManager _reservatieManager = new ReservatieManager(new VipServiceDalTest());

        /// <summary>
        /// The GetKlantByKlantNr_Success.
        /// </summary>
        [TestMethod]
        public void GetKlantByKlantNr_Success()
        {

            var k = _reservatieManager.GetKlantByKlantNr(5);

            Assert.AreEqual(5, k.Klantnummer);
            Assert.AreEqual("Yannick", k.Naam);
        }

        /// <summary>
        /// The IsHetEenNachuur_ReturnTrue.
        /// </summary>
        [TestMethod]
        public void IsHetEenNachuur_ReturnTrue()
        {
            var date = new DateTime(2020, 8, 11, 23, 45, 59);
            var k = _reservatieManager.IsHetEenNachuur(date);
            Assert.AreEqual(true, k);
        }

        /// <summary>
        /// The IsHetEenNachuur_ReturnFalse.
        /// </summary>
        [TestMethod]
        public void IsHetEenNachuur_ReturnFalse()
        {
            var date = new DateTime(2020, 8, 11, 11, 45, 59);
            var k = _reservatieManager.IsHetEenNachuur(date);
            Assert.AreEqual(false, k);
        }

        //BerekenPrijsMetBtw
        /// <summary>
        /// The BerekenPrijsMetBtw_RetrunPrijsMetBTW_ShouldPass.
        /// </summary>
        [TestMethod]
        public void BerekenPrijsMetBtw_RetrunPrijsMetBTW_ShouldPass()
        {
            var x = _reservatieManager.BerekenPrijsMetBtw(459);
            Assert.AreEqual(486.54, x);
        }

        /// <summary>
        /// The ReturnNachtUurPrijs_ReturnPrijs_ShouldPass.
        /// </summary>
        [TestMethod]
        public void ReturnNachtUurPrijs_ReturnPrijs_ShouldPass()
        {
            var v = new Voertuig()
            {
                EersteUur = 350,
                Id = 4,
            };

            var x = _reservatieManager.ReturnNachtUurPrijs(v);
            Assert.AreEqual(420, x);
        }

        /// <summary>
        /// The berekenPercentage_ShouldPass.
        /// </summary>
        [TestMethod]
        public void berekenPercentage_ShouldPass()
        {
            var x = _reservatieManager.berekenPercentage(450, 6);
            Assert.AreEqual(27, x);
        }

        /// <summary>
        /// The BerkenPercentageVanAutoPrijs_ShouldPass.
        /// </summary>
        [TestMethod]
        public void BerkenPercentageVanAutoPrijs_ShouldPass()
        {
            var v = new Voertuig()
            {
                EersteUur = 350,
                Id = 4,
            };
            var x = _reservatieManager.BerkenPercentageVanAutoPrijs(v, 6);
            Assert.AreEqual(18, x);
        }

        [TestMethod]
        public void CheckOveruren_ReturnFalse_ShouldPass()
        {
            var x = _reservatieManager.CheckOveruren(3);
            Assert.AreEqual(false, x);
        }

        /// <summary>
        /// The CheckOveruren_ReturnFalse_ShouldPass.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckOveruren_CatchArgumentException_ShouldPass()
        {
            var x = _reservatieManager.CheckOveruren(5);
            Assert.Fail();
        }

        /// <summary>
        /// The Check6UurTussenReservatie_ReturnNull_ShouldPass.
        /// </summary>
        [TestMethod]
        public void Check6UurTussenReservatie_ReturnNull_ShouldPass()
        {
            Reservaties r = null;
            var x = _reservatieManager.Check6UurTussenReservatie(r);
            Assert.AreEqual(false, x);
        }

        /// <summary>
        /// The VeelvoudHandler_ShouldPass.
        /// </summary>
        [TestMethod]
        public void VeelvoudHandler_ShouldPass()
        {

            var x = _reservatieManager.VeelvoudHandler(8);
            Assert.AreEqual(8, x);
        }

        /// <summary>
        /// The CheckForLongerThan11hour_returnFalse.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckForLongerThan11hour_CatchArgumentException()
        {

            var k = _reservatieManager.CheckForLongerThan11hour(12);

           Assert.Fail();
        }

        /// <summary>
        /// The CheckForLongerThan11hour_returnTrue.
        /// </summary>
        [TestMethod]
        public void CheckForLongerThan11hour_returnTrue()
        {
            var k = _reservatieManager.CheckForLongerThan11hour(10);
            Assert.AreEqual(false, k);
        }

        /// <summary>
        /// The CheckIfWellnessIs10Hours_returnTrue.
        /// </summary>
        [TestMethod]
        public void CheckIfWellnessIs10Hours_returnTrue()
        {
            Arragement start = new Arragement()
            {
                Naam = "Wellness"
            };
            var k = _reservatieManager.CheckIfWellnessIs10Hours(start, 10);
            Assert.AreEqual(true, k);
        }

        /// <summary>
        /// The CheckIfWellnessIs10Hours_CatchArgumentException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckIfWellnessIs10Hours_CatchArgumentException()
        {
            Arragement start = new Arragement()
            {
                Naam = "Wellness"
            };
            var k = _reservatieManager.CheckIfWellnessIs10Hours(start, 11);
         
        }


 
    }
}
