using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Execptions;
using BusinessLayer.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Models
{
    [TestClass]
    public class CountryTest
    {
        [TestMethod]
        public void Country_SetName_Valid()
        {
            Country C = new Country();

            Action act = () => { C.SetName("Flask"); ; };

            act.Should().NotThrow<CountryException>();

        }
        [TestMethod]
        public void Country_SetName_inValid()
        {
            Country C = new Country();

            Action act = () => { C.SetName("       "); ; };

            act.Should().Throw<CountryException>();

        }

        [TestMethod]
        public void Country_UpdataNameViaSet()
        {
            Country C = new Country();

            C.SetName("Disorder");
            Assert.AreEqual("Disorder", C.Name);
            C.SetName("Shepherds");
            Assert.AreEqual("Shepherds", C.Name);
        }
        [TestMethod]
        public void Country_SetContinent_Valid()
        {
            Continent co = null;
            Country C = new Country();

            Action act = () => { C.SetContinent(co); ; };

            act.Should().Throw<CountryException>();

        }
        [TestMethod]
        public void Country_SetCities_Valid()
        {
            Country co = new Country();

            List<City> x = new List<City>();
            x.Add(new City());
            co.SetCities(x);

            Assert.AreEqual(1, co.Cities.Count);

        }
    }
}
