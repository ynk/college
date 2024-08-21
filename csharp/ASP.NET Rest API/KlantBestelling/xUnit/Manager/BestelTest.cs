using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLayer.Exceptions;
using BusinessLayer.Manager;
using BusinessLayer.Model;
using DataLayer;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XUnitKlantBestellingService.Test
{
    [TestClass]
    public class BestelTest
    {
        [TestMethod]
        public void Test_Bestel_Toevoegen_Valid()
        {
            KlantenManager km = new KlantenManager(new UnitOfWork(new KlantBestellingContext("Test")));
            BestelManager bm = new BestelManager(new UnitOfWork(new KlantBestellingContext("Test")));
            km.ResetDB();
            Klant k = new Klant("Bart", "Simpson Yup");
            km.VoegKlantToe(k);
            var result = km.ZoekKlantMetId(1);
            int aantal = 5;
            var b = new Bestelling(result, Product.Leffe, aantal);
            bm.VoegBestellingToe(b);
            bm.ResetDB();
        }
        [TestMethod]
        public void Test_Bestel_AantalInValidThrowException()
        {
            KlantenManager km = new KlantenManager(new UnitOfWork(new KlantBestellingContext("Test")));
            BestelManager bm = new BestelManager(new UnitOfWork(new KlantBestellingContext("Test")));
            km.ResetDB();
            Klant k = new Klant("Bart", "Simpson Yup");
            km.VoegKlantToe(k);
            var result = km.ZoekKlantMetId(1);
            Action act = () =>
            {
                bm.VoegBestellingToe(new Bestelling(result, Product.Leffe, 0));
            };
            //Assert

            act.Should().Throw<BestellingException>()
                .WithMessage("Aantal mag niet lager of gelijk zijn dan nul");
            km.ResetDB();
        }

        [TestMethod]
        public void Test_GetAlleBestellingVanKlant_Valid()
        {
          
            BestelManager bm = new BestelManager(new UnitOfWork(new KlantBestellingContext("Test")));
            KlantenManager km = new KlantenManager(new UnitOfWork(new KlantBestellingContext("Test")));
            km.ResetDB();
            bm.ResetDB();
            Klant k = new Klant("Bart", "Simpson Yup");
            km.VoegKlantToe(k);
            var result = km.ZoekKlantMetId(1);
            bm.VoegBestellingToe(new Bestelling(result, Product.Leffe, 2));
            bm.VoegBestellingToe(new Bestelling(result, Product.Duvel, 4));
            bm.VoegBestellingToe(new Bestelling(result, Product.Orval, 5));
            bm.VoegBestellingToe(new Bestelling(result, Product.Westmalle, 1));
            Assert.AreEqual(bm.GetAlleBestellingenVanKlant(result.Id).Count(), 4);
            km.ResetDB();

        }
        [TestMethod]
        public void Test_ZoekBestellingOpId_Valid()
        {

            BestelManager bm = new BestelManager(new UnitOfWork(new KlantBestellingContext("Test")));
            KlantenManager km = new KlantenManager(new UnitOfWork(new KlantBestellingContext("Test")));
            bm.ResetDB();
            km.ResetDB();
            Klant k = new Klant("Bart", "Simpson Yup");
            km.VoegKlantToe(k);
            var result = km.ZoekKlantMetId(1);
            bm.VoegBestellingToe(new Bestelling(result, Product.Leffe, 2));
            Assert.AreEqual(bm.ZoekBestellingOpId(1).Id,result.Id);
            Assert.AreEqual(bm.ZoekBestellingOpId(1).Aantal, 2);
            bm.ResetDB();
            km.ResetDB();
        }
        [TestMethod]
        public void Test_ZoekBestellingOpId_InvalidThrowException()
        {

            BestelManager bm = new BestelManager(new UnitOfWork(new KlantBestellingContext("Test")));


            Action act = () =>
            {
                bm.ZoekBestellingOpId(8);
            };
            act.Should().Throw<BestellingException>().WithMessage("KlantService: Geen bestelling gevonden");
        }

        [TestMethod]
        public void Test_VerwijderBestellingId_Valid()
        {
            BestelManager bm = new BestelManager(new UnitOfWork(new KlantBestellingContext("Test")));
            KlantenManager km = new KlantenManager(new UnitOfWork(new KlantBestellingContext("Test")));
            bm.ResetDB();
            km.ResetDB();
            Klant k = new Klant("Bart", "Simpson Yup");
            km.VoegKlantToe(k);
            var result = km.ZoekKlantMetId(1);
            bm.VoegBestellingToe(new Bestelling(result, Product.Leffe, 2));
            Assert.AreEqual(bm.ZoekBestellingOpId(1).Id, result.Id);
            Assert.AreEqual(bm.ZoekBestellingOpId(1).Aantal, 2);
            bm.ResetDB();
            km.ResetDB();
        }


    }
}
