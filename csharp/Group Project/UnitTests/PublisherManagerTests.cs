using BusinessLayer;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Transactions;

namespace UnitTests
{
    [TestClass]
    public class PublisherManagerTests
    {
        private string connectionString = "Data Source=0.0.0.0;Initial Catalog=StripCatalogus_test;Persist Security Info=True;User ID=SA;Password=SuperSecretPassword";
        
        #region AddPublisher
        [TestMethod]
        public void AddPublisher_Valid()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                Publisher publisher = new Publisher("Lucas");
                PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                //Act
                Action act = () => { pm.AddPublisher(publisher); };
                //Assert
                act.Should().NotThrow<Exception>();
            }
            
        }

        [TestMethod]
        public void AddPublisher_Invalid_EmptyName()
        {

                //Act
                Action act = () => { new Publisher(""); };
                //Assert
                act.Should().Throw<PublisherException>()
                    .WithMessage("Publisher name is empty.");
            
        }

        [TestMethod]
        public void AddPublisher_Invalid_PublisherExist()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                Publisher publisher = new Publisher("Joachim");
                PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                //Act
                Action act = () => { pm.AddPublisher(publisher); };
                //Assert
                act.Should().Throw<PublisherException>()
                    .WithMessage("Publisher already exist.");
            }

        }
        #endregion

        #region UpdatePublisher
        [TestMethod]
        public void UpdatePublisher_Valid()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                List<Publisher> publishers = pm.GetAllPublishers();
                Publisher publisher = publishers.First();
                publisher.SetName("Micheal");

                //Act
                Action act = () => { pm.UpdatePublisher(publisher); };
                //Assert
                act.Should().NotThrow<Exception>();
            }

        }

        [TestMethod]
        public void UpdatePublisher_Invalid_EmptyName()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                List<Publisher> publishers = pm.GetAllPublishers();
                Publisher publisher = publishers.First();
                

                //Act
                Action act = () => { publisher.SetName(""); };
                //Assert
                act.Should().Throw<PublisherException>()
                    .WithMessage("Publisher name is empty.");
            }
        }

        //[TestMethod]
        //public void UpdatePublisher_Invalid_PublisherId()
        //{
        //    //Arrange
        //    PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
        //    List<Publisher> publishers = pm.GetAllPublishers();
        //    Publisher publisher = publishers.Last();
        //    publisher.SetId(publisher.Id+10);

        //    //Act
        //    Action act = () => { pm.UpdatePublisher(publisher); };
        //    //Assert
        //    act.Should().Throw<PublisherException>()
        //        .WithMessage("PublisherId does not exist.");
        //}

        [TestMethod]
        public void UpdatePublisher_Invalid_PublisherExist()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                List<Publisher> publishers = pm.GetAllPublishers();
                Publisher publisher = publishers.Last();
                publisher.SetName(publishers.First().Name);

                //Act
                Action act = () => { pm.UpdatePublisher(publisher); };
                //Assert
                act.Should().Throw<PublisherException>()
                    .WithMessage("Publisher already exist.");
            }
        }

        #endregion

        #region RemovePublisher
        [TestMethod]
        public void RemovePublisher_Valid()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                List<Publisher> publishers = pm.GetAllPublishers();
                Publisher publisher = publishers.Last();

                //Act
                Action act = () => { pm.RemovePublisher(publisher); };
                //Assert
                act.Should().NotThrow<Exception>();
            }
        }

        [TestMethod]
        public void RemovePublisher_Invalid_PublisherHasComics()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                ComicManager cm = new ComicManager(new UnitOfWork(connectionString));
                List<Publisher> publishers = pm.GetAllPublishers();
                List<Comic> comics = cm.GetAllComics();
                Publisher publisher = comics.Last().Publisher;

                //Act
                Action act = () => { pm.RemovePublisher(publisher); };
                //Assert
                act.Should().Throw<PublisherException>()
                    .WithMessage("Publisher still has comics.");
            }
        }

        [TestMethod]
        public void DoesPublisherExist_Invalid_EmptyName()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                Publisher publisher = new Publisher("Rift");
                PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                //Act
                Action act = () => { pm.AddPublisher(publisher); };
                //Assert
                act.Should().NotThrow<PublisherException>();
            }
        }
        #endregion


        [TestMethod]
        public void DoesPublisherExist_Isvalid_IsTrue()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                Publisher publisher = new Publisher("Steve");
                //Act
                var exist = pm.DoesPublisherExist(publisher.Name);
                //Assert
                exist.Should().Be(true);
            }
        }
        [TestMethod]
        public void DoesPublisherExist_IsInvalid_IsFalse()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                Publisher publisher = new Publisher("FortniteAllNite");
                //Act
                var exist =  pm.DoesPublisherExist(publisher.Name); 
                //Assert
                exist.Should().Be(false);
            }
        }
        [TestMethod]
        public void GetPublisherIfExistElseCreate_IsValid_ReturnsPublisher()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                Publisher Publisher = pm.GetAllPublishers().First(x => x.Id == 1);
                //Act
                Action act = () => { pm.GetPublisherIfExistElseCreate(Publisher); };
                //Assert
                act.Equals(Publisher);
            }
        }

        [TestMethod]
        public void GetPublisherIfExistElseCreate_IsValid_AddsPublisher()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                Publisher Publisher = new Publisher("Rift");
                PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                //Act
                Action act = () => { pm.GetPublisherIfExistElseCreate(Publisher); };
                //Assert
                act.Should().NotThrow<PublisherException>();
                act.Equals(Publisher);
            }
        }
       
    }
}
