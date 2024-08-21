using BusinessLayer;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using DataLayer;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace UnitTests
{
    [TestClass]
    public class SerieManagerTests
    {
        private string _connectionString = "Data Source=0.0.0.0;Initial Catalog=StripCatalogus_test;Persist Security Info=True;User ID=SA;Password=SuperSecretPassword";

        #region AddSerie

        [TestMethod]
        public void AddSerie_Valid()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                Serie serie = new Serie("Bart Simpson");
                SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));
                //Act
                Action act = () => { sm.AddSerie(serie); };
                //Assert
                act.Should().NotThrow<Exception>();
            }
            
        }

        [TestMethod]
        public void AddSerie_Invalid_EmptyName()
        {

                //Arrange
               
                //Act
                Action act = () => { new Serie("");  };
                //Assert
                act.Should().Throw<SerieException>()
                    .WithMessage("Serie name is empty.");

        }

        [TestMethod]
        public void AddSerie_Invalid_SerieExist()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));
                Serie serie = new Serie(sm.GetAllSeries().Last().Name);
                //Act
                Action act = () => { sm.AddSerie(serie); };
                //Assert
                act.Should().Throw<SerieException>()
                    .WithMessage("Serie already exist.");
            }
        }

        #endregion

        #region UpdateSerie
        [TestMethod]
        public void UpdateSerie_Valid()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));
                Serie serie = sm.GetAllSeries().Last();
                serie.Name = "Rift";
                //Act
                Action act = () => { sm.UpdateSerie(serie); };
                //Assert
                act.Should().NotThrow<SerieException>();
            }
        }

        [TestMethod]
        public void UpdateSerie_Invalid_EmptyName()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));
                List<Serie> series = sm.GetAllSeries();
                Serie p = series.First();
               
                //Act
                Action act = () => { p.SetName(""); };
                //Assert
                act.Should().Throw<SerieException>()
                    .WithMessage("Serie name is empty.");
            }
        }

        [TestMethod]
        public void UpdateSerie_valid_UpdateNaam()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));
                List<Serie> series = sm.GetAllSeries();
                Serie p = series.First();
                p.SetName("Rift");
                //Act
                Action act = () => { sm.UpdateSerie(p); };
                //Assert
                act.Should().NotThrow<SerieException>();
            }
        }

        [TestMethod]
        public void UpdateSerie_Invalid_SerieExist()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));
                List<Serie> allSeries = sm.GetAllSeries();
                Serie serie = sm.GetAllSeries().Last();
                serie.Name = allSeries.First().Name;
                //Act
                Action act = () => { sm.UpdateSerie(serie); };
                //Assert
                act.Should().Throw<SerieException>()
                    .WithMessage("Serie already exist.");
            }
        }
        #endregion
        [TestMethod]
        public void GetSerieIfExistElseCreate_Invalid_SerieExist()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {

                SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));
                Serie Serie = sm.GetAllSeries().First(x => x.Id == 3);
                //Act
                Action act = () => { sm.GetSerieIfExistElseCreate(Serie); };
                //Assert
                act.Should().NotThrow<SerieException>();
                act.Equals(Serie);
            }
        }
        [TestMethod]
        public void GetSerieIfExistElseCreate_Valid_CreateSerie()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {

                SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));
                Serie Serie = new Serie("Mercy");
                //Act
                Action act = () => { sm.GetSerieIfExistElseCreate(Serie); };
                //Assert
                act.Should().NotThrow<SerieException>();
                act.Equals(Serie);
            }
        }
        [TestMethod]
        public void GetSerieById_Valid()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {

                SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));
                
                //Act
                Action act = () => { sm.SearchBySerieId(2); };
                //Assert
                act.Should().NotThrow<SerieException>();
            }
        }
        [TestMethod]
        public void GetSerieById_inValid()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {

                SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));

                //Act
                Action act = () => { sm.SearchBySerieId(1337); };
                //Assert
                act.Should().NotThrow<SerieException>();
                act.Equals(null);
            }
        }
        [TestMethod]
        public void RemoveSerie_Valid()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {

                SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));
                Serie ser = sm.GetAllSeries().First(x => x.Id == 2);
                //Act
                Action act = () => { sm.RemoveSerie(ser); };
                //Assert
                act.Should().NotThrow<SerieException>();
            
            }
        }

    }
}
