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
    public class RiverTest
    {
      


        /*Naam, Length, Countries*/


    [TestMethod]
    public void Continent_SetLength_Invalid()
    {
         River R = new River();
     
         Action act = () => { R.SetLength(0); ; };

        act.Should().Throw<RiverException>();
    }
    [TestMethod]
    public void Continent_SetLength_InvalidNegative()
    {
        River R = new River();

        Action act = () => { R.SetLength(-147.3); ; };

        act.Should().Throw<RiverException>();
    }
    [TestMethod]
    public void Continent_SetLength_Valid_NotThrow()
    {
        River R = new River();

        Action act = () => { R.SetLength(814.21); ; };

        act.Should().NotThrow<RiverException>();
    }
    [TestMethod]
    public void Continent_SetLength_Valid()
    {
        River R = new River();

         R.SetLength(814.21); 

        Assert.AreEqual(814.21, R.Length);

    }
    [TestMethod]
    public void Continent_Update_Valid()
    {
        River R = new River();

        R.SetLength(814.21);
        Assert.AreEqual(814.21, R.Length);
        R.SetLength(41);
        Assert.AreEqual(41, R.Length);


    }
    [TestMethod]
    public void Continent_SetName_Valid()
    {
        River R = new River();

        Action act = () => { R.SetName("Thunderstruck"); ; };

        act.Should().NotThrow<RiverException>();

       

    }
    [TestMethod]
    public void Continent_SetName_inValid()
    {
        River R = new River();

        Action act = () => { R.SetName("     "); ; };

        act.Should().Throw<RiverException>();


    }
    [TestMethod]
    public void Continent_SetName_inValid_empty()
    {
        River R = new River();

        Action act = () => { R.SetName(""); ; };

        act.Should().Throw<RiverException>();


    }


    [TestMethod]
    public void Continent_Update_Name()
    {
        River R = new River();

        R.SetName("Thunderstruck");
        Assert.AreEqual("Thunderstruck", R.Name);
        R.SetName("Thunder");
        Assert.AreEqual("Thunder", R.Name);


    }
    [TestMethod]
    public void Continent_SetCountries_Valid()
    {
        River R = new River();

        List<Country> countries = new List<Country>();
        countries.Add(new Country());
        R.SetCountries(countries);
        Assert.AreEqual(1, R.Countries.Count);
        countries.Add(new Country());
        R.SetCountries(countries);

        Assert.AreEqual(2 ,R.Countries.Count);


    }

    }

}

