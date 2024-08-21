using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Exceptions;
using BusinessLayer.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XUnitKlantBestellingService.Model
{
    [TestClass]
    public class BestellingModelTest
    {
        [TestMethod]
        public void Test_Bestelling_Valid()
        {
            Action act = () =>
            {
                Action act = () =>
                {
                    var k = new Klant(1, "Yannick","Dorpsgek 12");
                    new Bestelling(k,Product.Duvel,5);
                };
                //Act
            };
            act.Should().NotThrow();
        }
        [TestMethod]
        public void Test_Bestelling_InvalidAantalIsTeKlein()
        {

                Action act = () =>
                {
                    var k = new Klant(1, "Yannick", "Dorpsgek 12");
                    new Bestelling(k, Product.Duvel, 0);
                };

                act.Should().Throw<BestellingException>().WithMessage("Aantal mag niet lager of gelijk zijn dan nul");
        }
        [TestMethod]
        public void Test_Bestelling_UpdateantalIsTeKlein()
        {
            Action act = () =>
            {
                var k = new Klant(1, "Yannick", "Dorpsgek 12");
                var b = new Bestelling(k, Product.Duvel, 5);
                b.setAantal(0);
            };
            act.Should().Throw<BestellingException>().WithMessage("Aantal mag niet lager of gelijk zijn dan nul");
        }
        [TestMethod]
        public void Test_Bestelling_UpdateAantalValid()
        {
            var k = new Klant(1, "Yannick", "Dorpsgek 12");
            var b = new Bestelling(k, Product.Duvel, 5);
            Action act = () =>
            {
            
                b.setAantal(10);
            };
            act.Should().NotThrow<BestellingException>();
            b.Aantal.Should().Be(10);
        }
        [TestMethod]
        public void Test_Bestelling_UpdateAantalProductValid()
        {
            var k = new Klant(1, "Yannick", "Dorpsgek 12");
            var b = new Bestelling(k, Product.Duvel, 5);
            Action act = () =>
            {

                b.setProduct(Product.Leffe);
            };
            act.Should().NotThrow<BestellingException>();
            b.Product.Should().Be(Product.Leffe);
        }
    }
}
