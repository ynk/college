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
    public class CityTest
    {
     
        [TestMethod]
        public void Continent_SetName_unvalid_nameIsEmpty()
        {
            City C = new City();

            Action act = () => { C.SetName(""); ; };

            act.Should().Throw<CityException>();
        }
        [TestMethod]
        public void Continent_SetName_unvalid_nameIsSpace()
        {
            City C = new City();

            Action act = () => { C.SetName("         "); ; };

            act.Should().Throw<CityException>();
        }

        [TestMethod] public void Continent_UpdataNameViaSet_Valid()
        {
            City C = new City();

            C.SetName("Disorder");
            Assert.AreEqual("Disorder", C.Name);
            C.SetName("Shepherds");
            Assert.AreEqual("Shepherds", C.Name);
        }
        /*
        [TestMethod]
        public void Continent_SetPopulation_Valid()
        {
            City C = new City();

            C.SetPopulation(1337);
            Assert.AreEqual(1337, C.Population);
        }*/
        [TestMethod]
        public void Continent_SetPopulation_Invalid()
        {
            City C = new City();

            Action act = () => { C.SetPopulation(-1); ; };

            act.Should().Throw<CityException>();
        }/*
        [TestMethod]
        public void Continent_UpdatePopulation_Invalid()
        {
            City C = new City();

            C.SetPopulation(1337);
            Assert.AreEqual(1337, C.Population);
            C.SetPopulation(1478);
            Assert.AreEqual(1478, C.Population);
        }*/

        [TestMethod]
        public void Continent_SetCapitaltrue_Valid()
        {
            City C = new City();

            C.SetCapital(true);
            Assert.AreEqual(true, C.IsCapital);
        }
        [TestMethod]
        public void Continent_SetCapitalfalse_Valid()
        {
            City C = new City();

            C.SetCapital(false);
            Assert.AreEqual(false, C.IsCapital);
        }
        [TestMethod]
        public void Continent_SetCountry_Valid()
        {
            City C = new City();
            Country Land = new Country();
            Land.SetName("Belgie");
            C.SetCountry(Land);
            Assert.AreEqual("Belgie", C.Country.Name);
        }

        [TestMethod]
        public void Continent_SetSurface_Valid()
        {
            City C = new City();

            C.SetSurface(1478.23);
            Assert.AreEqual(1478.23, C.Surface);
        }
        [TestMethod]
        public void Continent_SetSurface_Invalid()
        {
            City C = new City();

            Action act = () => { C.SetSurface(-12.21); ; };

            act.Should().Throw<CityException>();
        }

    }
}
