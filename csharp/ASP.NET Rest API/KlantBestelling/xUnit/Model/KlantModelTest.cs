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
    public class KlantModelTest
    {
        [TestMethod]
        public void Model_MaakKlant_Valid()
        {

            Action act = () => { new Klant("Yannick", "DorpStraat 147"); };
                //Act
               
      
            act.Should().NotThrow();
        }
        [TestMethod]
        public void Model_MaakKlant_InvalidAdresThrowExceptionAdresIsTeKort()
        {
       
                Action act = () =>
                {
                    new Klant("Yannick", "Dorp");
                };
                //Act
                
            act.Should().Throw<KlantException>().WithMessage("Adres is te kort");
        }
        [TestMethod]
        public void Test_Klant_MakenMetDezelfdeNaamAlsAdres()
        {

            //Arrange
            Action act = () => { new Klant("YannidckDDDDD", "YannidckDDDDD"); };
            //Act
            act.Should().Throw<KlantException>()
                .WithMessage("Adres is hetzelfde als naam");

        }
        [TestMethod]
        public void Test_Klant_SetNaamHetZelfdeAlsAdress()
        {

            //Arrange
            Action act = () =>
            {
                var x = new Klant("Yannick", "345");
                x.SetNaam("345");
            };
            //Act
            act.Should().Throw<KlantException>()
                .WithMessage("Naam is hetzelfde als adres");

        }
        [TestMethod]
        public void Test_Klant_SetAdressHetZelfdeAlsNaam()
        {

            //Arrange
            Action act = () =>
            {
                var x = new Klant("IkWeetNieWaaromMaarOk", "YannidckDDDDD");
                x.SetAdress("IkWeetNieWaaromMaarOk");
            };
            //Act
            act.Should().Throw<KlantException>()
                .WithMessage("Adres is hetzelfde als naam");

        }
        [TestMethod]
        public void Test_KlantObject_MakenMetTeKortAdres()

        {

            //Arrange
            Action act = () => { new Klant("Yannick", "kort"); };
            //Act
            act.Should().Throw<KlantException>()
                .WithMessage("Adres is te kort");

        }
        [TestMethod]
        public void Test_KlantObject_NaamIsLeeg()

        {

            //Arrange
            Action act = () => { new Klant("", "kort"); };
            //Act
            act.Should().Throw<KlantException>()
                .WithMessage("Naam is leeg");

        }
    }
}
