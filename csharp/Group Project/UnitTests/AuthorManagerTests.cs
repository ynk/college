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
    public class AuthorManagerTests
    {
        private string connectionString = "Data Source=149.56.27.5;Initial Catalog=StripCatalogus_test;Persist Security Info=True;User ID=SA;Password=Projectwerk2020@";

        #region AddAuthor
        [TestMethod]
        public void AddAuthor_Valid()
        {
            //integration test
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                Author author = new Author("Sacul");
                AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
                //Act
                Action act = () => { am.AddAuthor(author); };
                //Assert
                act.Should().NotThrow<AuthorException>();
            }
        }

        [TestMethod]
        public void AddAuthor_Invalid_EmptyName()
        {
            //Act
            Action act = () => { new Author(""); };
            //Assert
            act.Should().Throw<AuthorException>()
                .WithMessage("Author name is empty.");
        }

        [TestMethod]
        public void AddAuthor_Invalid_IdIsBelowZero()
        {
            //Act
            Action act = () => { new Author(-5,"Poggers"); };
            //Assert
            act.Should().Throw<AuthorException>()
                .WithMessage("AuthorId is not valid.");
        }

        [TestMethod]
        public void AddAuthor_Invalid_AuthorExist()
        {
            //Arrange
            Author author = new Author("Den Steve");
            AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
            //Act
            Action act = () => { am.AddAuthor(author); };
            //Assert
            act.Should().Throw<AuthorException>()
                .WithMessage("Author already exist.");
        }


        [TestMethod]
        public void SetAuthorId_Invalid_BadId()
        {
            //Arrange
      
            //Act
            Action act = () => { new Author(0,"Den Steve"); };
            //Assert
            act.Should().Throw<AuthorException>()
                .WithMessage("AuthorId is not valid.");
        }
        [TestMethod]
        public void SetAuthorId_Valid()
        {
            //Arrange

            //Act
            Action act = () => { new Author(9, "Den Steve"); };
            //Assert
            act.Should().NotThrow<AuthorException>();
        }

        #endregion

        #region UpdateAuthor
        [TestMethod]
        public void UpdateAuthor_Valid()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
            List<Author> authors = am.GetAllAuthors();
            Author author = authors.First();
            author.SetName("Riot");

            //Act
            Action act = () => { am.UpdateAuthor(author); };
            //Assert
            act.Should().NotThrow<Exception>();
            }
        }

        [TestMethod]
        public void UpdateAuthor_Invalid_EmptyName()
        {
            //check author, emptyname
            //Act
            Action act = () => { new Author(""); };
            //Assert
            act.Should().Throw<AuthorException>()
                .WithMessage("Author name is empty.");
        }

        [TestMethod]
        public void UpdateAuthor_Invalid_AuthorExist()
        {
            //Arrange
            AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
            List<Author> authors = am.GetAllAuthors();
            Author author = authors.Last();
            author.SetName(authors.First().Name);

            //Act
            Action act = () => { am.UpdateAuthor(author); };
            //Assert
            act.Should().Throw<AuthorException>()
                .WithMessage("Author already exist.");
        }

        #endregion

        #region RemoveAuthor
        [TestMethod]
        public void RemoveAuthor_Valid()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Comics moeten leeg zijn
                //Arrange
                AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
                Author author = am.GetAllAuthors().First(x => x.Id == 2);
                //Act
                Action act = () => { am.RemoveAuthor(author); };
                //Assert
                act.Should().NotThrow<AuthorException>();
            }
        }

        [TestMethod]
        public void RemoveAuthor_Invalid()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
                Author author = am.GetAllAuthors().First(x => x.Id == 1002);
                //Act
                Action act = () => { am.RemoveAuthor(author); };
                //Assert
                act.Should().Throw<AuthorException>()
                .WithMessage("Author still has comics.");
            }
        }
        #endregion

        #region GetAuthorIfExistElseCreate

        [TestMethod]
        public void GetAuthorIfExistElseCreate_IsValid_ReturnsAuthor()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
                Author author = am.GetAllAuthors().First(x => x.Id == 2);
                //Act
                Action act = () => { am.GetAuthorIfExistElseCreate(author); };
                //Assert
                act.Equals(author);
            }
        }

        [TestMethod]
        public void GetAuthorIfExistElseCreate_IsValid_AddsAuthor()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                Author author = new Author("Lucas");
                AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
                //Act
                Action act = () => { am.GetAuthorIfExistElseCreate(author); };
                //Assert
                act.Should().NotThrow<AuthorException>();
                act.Equals(author);
            }
        }

        #endregion

        #region DoesAuthorExist

        [TestMethod]
        public void DoesAuthorExist_IsValid_IsTrue()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
                Author author = am.GetAllAuthors().First(x => x.Id == 2);
                //Act
                Action act = () => { am.DoesAuthorExist(author.Name); };
                //Assert
                act.Should().Equals(true);
            }
        }

        [TestMethod]
        public void DoesAuthorExist_IsInvalid_IsFalse()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
                Author author = new Author("FortniteAllNite");
                //Act
                Action act = () => { am.GetAuthorIfExistElseCreate(author); };
                //Assert
                act.Should().Equals(false);
            }
        }

        #endregion
    }
}
