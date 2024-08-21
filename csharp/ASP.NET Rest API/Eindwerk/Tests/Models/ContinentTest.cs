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
    public class ContinentTest
    {

        [TestMethod]
        public void Continent_SetName_Valid()
        {
            Continent C = new Continent();
            
            Action act = () => { C.SetName("Flask"); ; };

            act.Should().NotThrow<ContinentException>();

        }
        [TestMethod]
        public void Continent_SetName_unvalid_nameIsEmpty()
        {
            Continent C = new Continent();

            Action act = () => { C.SetName(""); ; };

            act.Should().Throw<ContinentException>();
        }
        [TestMethod]
        public void Continent_SetName_unvalid_nameIsspace()
        {
            Continent C = new Continent();

            Action act = () => { C.SetName("          "); ; };

            act.Should().Throw<ContinentException>();
        }
        [TestMethod]
        public void Continent_UpdataNameViaSet_shouldbe0()
        {
            Continent C = new Continent();

            C.SetName("Disorder");
            Assert.AreEqual("Disorder", C.Name);
            C.SetName("Shepherds");
            Assert.AreEqual("Shepherds", C.Name);
        }


        [TestMethod]
        public void Continent_GetPopulation_shouldbe0()
        {
            Continent C = new Continent();


            Assert.AreEqual(0,C.GetPopulation());
        }
        /*
        [TestMethod]
        public void Continent_GetPopulation_shouldbe1337()
        {
            Continent europa = new Continent("Europa");
            List<City> Cities = new List<City>();
           
            Country Belgie = new Country("Belgie", europa);
            City Gent = new City("Belgie",1337,Belgie,false,1478);

            Belgie.SetCities(Cities);
            Cities.Add(Gent);
            List<Country> countries = new List<Country>();
            countries.Add(Belgie);

            europa.setCountries(countries);



            Assert.AreEqual(1337, europa.GetPopulation());
        }
        [TestMethod]
        public void Continent_GetSurface_shouldbe1478()
        {
            Continent europa = new Continent("Europa");
            List<City> Cities = new List<City>();

            Country Belgie = new Country("Belgie", europa);
            City Gent = new City("Belgie", 69, Belgie, false, 1478.21);

            Belgie.SetCities(Cities);
            Cities.Add(Gent);
            List<Country> countries = new List<Country>();
            countries.Add(Belgie);

            europa.setCountries(countries);




            Assert.AreEqual(1478.21, europa.GetSurface());
        }
    */

    }
}
