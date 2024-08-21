using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLayer.Exceptions;
using BusinessLayer.Manager;
using NUnit;
using BusinessLayer.Model;
using FluentAssertions;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XUnitKlantBestellingService.Test
{
    [TestClass]

    public class KlantTest
    {
        
        [TestMethod]
        public void Test_Klant_Toevoegen()
        {
            
            //Arrange
            KlantenManager km = new KlantenManager(new UnitOfWork(new KlantBestellingContext("Test")));
            km.ResetDB();
            string Naam = "Yannick";
            string Adres = "Dorpstraat 177";
            var klant = new Klant(Naam,Adres);
            //Act
            km.VoegKlantToe(klant);
            km.ResetDB();
        }


      
        [TestMethod]
        public void Test_Klant_ZoekenOpId()
        {
            KlantenManager km = new KlantenManager(new UnitOfWork(new KlantBestellingContext("Test")));
            km.ResetDB();
            km.VoegKlantToe(new Klant("Dorpgek", "Dorpshoek 177"));
            Klant k = km.ZoekKlantMetId(1);
            Assert.AreEqual(k.Naam, "Dorpgek");
            Assert.AreEqual(k.Adres, "Dorpshoek 177");
            km.ResetDB();
        }

        [TestMethod]
        public void Test_Klant_AlleKlanten()
        {
            KlantenManager km = new KlantenManager(new UnitOfWork(new KlantBestellingContext("Test")));
            km.VoegKlantToe(new Klant("zed","A5441561lehja"));
            km.VoegKlantToe(new Klant("ysan", "midl65456165ane"));
            km.VoegKlantToe(new Klant("annie", "straatxd52151"));
            km.VoegKlantToe(new Klant("yasuo", "straatxd5215111"));
            Assert.AreEqual(km.AlleKlanten().Count(), 4);
            km.ResetDB();
        }
        [TestMethod]
        public void Test_Klant_AlleKlantenDeleten()
        {
            KlantenManager km = new KlantenManager(new UnitOfWork(new KlantBestellingContext("Test")));
            km.VoegKlantToe(new Klant("zed", "A5441561lehja"));
            km.VoegKlantToe(new Klant("ysan", "midl65456165ane"));
            km.VoegKlantToe(new Klant("annie", "straatxd52151"));
            km.VoegKlantToe(new Klant("yasuo", "straatxd5215111"));
            Assert.AreEqual(km.AlleKlanten().Count(), 4);
            km.ResetDB();
            Assert.AreEqual(km.AlleKlanten().Count(), 0);
        }

        [TestMethod]
        public void Test_Klant_DeleteById()
        {
            KlantenManager km = new KlantenManager(new UnitOfWork(new KlantBestellingContext("Test")));
            var klant = new Klant("Annie", "Inting Mid Lane 8");
            km.VoegKlantToe(klant);
            Action act = () =>
            {
                km.VerwijderKlant(1);
            };
            act.Should().NotThrow<KlantException>();
        }
        [TestMethod]
        public void Test_Klant_DeleteByIdFailThrowException()
        {
            KlantenManager km = new KlantenManager(new UnitOfWork(new KlantBestellingContext("Test")));
          

            Action act = () => { km.VerwijderKlant(1); };
            //Assert
            act.Should().Throw<KlantException>()
                .WithMessage("Klant bestaat niet");
            km.ResetDB();
        }

        [TestMethod]
        public void Test_Klant_UpdateNaamValid()
        {
            KlantenManager km = new KlantenManager(new UnitOfWork(new KlantBestellingContext("Test")));
            km.ResetDB();
            var klant = new Klant("Annie", "Inting Mid Lane 8");
            km.VoegKlantToe(klant);
            var result = km.ZoekKlantMetId(1);
            result.SetNaam("Inting Sona");
            km.UpdateKlant(result);
            result = km.ZoekKlantMetId(1);
            Assert.AreEqual(result.Naam, "Inting Sona");
            km.ResetDB();
        }
        [TestMethod]
        public void Test_Klant_UpdateAdresValid()
        {
            KlantenManager km = new KlantenManager(new UnitOfWork(new KlantBestellingContext("Test")));
            km.ResetDB();
            var klant = new Klant("Annieke Roomst", "Inting Mid Lane 1337");
            km.VoegKlantToe(klant);
            var result = km.ZoekKlantMetId(1);
            result.SetAdress("Inting top lane 18");
            km.UpdateKlant(result);
            result = km.ZoekKlantMetId(1);
            Assert.AreEqual(result.Adres, "Inting top lane 18");
            km.ResetDB();
        }
        [TestMethod]
        public void Test_Klant_UpdateAdressInValidThrowExceptionAdresZelfdeAlsNaam()
        {
            KlantenManager km = new KlantenManager(new UnitOfWork(new KlantBestellingContext("Test")));
            km.ResetDB();
            var klant = new Klant("Annieke Roomst", "Inting Mid Lane 1337");
            km.VoegKlantToe(klant);
            var result = km.ZoekKlantMetId(1);

            Action act = () =>
            {
                result.SetAdress(klant.Naam);
            };
            act.Should().Throw<KlantException>()
                .WithMessage("Adres is hetzelfde als naam");
            km.ResetDB();
        }
        [TestMethod]
        public void Test_Klant_UpdateAdressInValidThrowExceptionNaamIsZelfdeAlsAdres()
        {
            KlantenManager km = new KlantenManager(new UnitOfWork(new KlantBestellingContext("Test")));
            km.ResetDB();
            var klant = new Klant("Annieke Roomst", "Inting Mid Lane 1337");
            km.VoegKlantToe(klant);
            var result = km.ZoekKlantMetId(1);

            Action act = () =>
            {
                result.SetNaam(klant.Adres);
            };
            act.Should().Throw<KlantException>()
                .WithMessage("Naam is hetzelfde als adres");
            km.ResetDB();
        }




    }
}
