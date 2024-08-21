using BusinessLayer;
using BusinessLayer.Entities;
using DataLayer;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Transactions;
using BusinessLayer.Exceptions;

namespace UnitTests
{
    [TestClass]
    public class ComicManagerTests
    {
        private string connectionString =
            "Data Source=0.0.0.0;Initial Catalog=StripCatalogus_test;Persist Security Info=True;User ID=SA;Password=SuperSecretPassword";

        #region AddComic

        [TestMethod]
        public void AddComic_Valid()
        {
            //integration test
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                Publisher publisher = pm.GetAllPublishers().First(x => x.Id == 2004);
                SerieManager sm = new SerieManager(new UnitOfWork(connectionString));
                Serie serie = sm.GetAllSeries().First(x => x.Id == 1);
                AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
                Author author = am.GetAllAuthors().First(x => x.Id == 2);
                List<Author> authors = new List<Author>() { author };
                Comic comic = new Comic("TestComic", publisher, serie, authors, null);
                ComicManager cm = new ComicManager(new UnitOfWork(connectionString));
                //Act
                Action act = () => { cm.AddComic(comic); };
                //Assert
                act.Should().NotThrow<ComicException>();
            }
        }

        [TestMethod]
        public void AddComic_Invalid_AuthorAlreadyAdded()
        {
            //integration test
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                var author = new Author(1, "Steve");
                var comic = new Comic("De parameterloze comic", new Publisher(1, "Rell"), new Serie(1, "Legendes van de league"), new List<Author>() { author, new Author(2, "Yannick") }, 1);

                //Act
                Action act = () => { comic.AddAuthor(author); };
                //Assert
                act.Should().Throw<ComicException>().WithMessage("The author is already added for this comic.");
            }
        }

        [TestMethod]
        public void AddComic_Invalid_EmptyTitle()
        {
            //Act
            Action act = () =>
            {
                new Comic("", new Publisher("Steve"), new Serie("TestjesSchrijven"), new List<Author>(), null);
            };
            //Assert
            act.Should().Throw<ComicException>()
                .WithMessage("Title is empty.");
        }

        [TestMethod]
        public void AddComic_Invalid_IdIsZeroOrNegative()
        {

            //Act
            Action act = () =>
            {
                new Comic(0, "De parameterloze comic", new Publisher(1, "Rell"), new Serie(1, "Legendes van de league"), new List<Author>() { new Author(1, "Steve"), new Author(2, "Yannick") }, 1, 0);
            };
            //Assert
            act.Should().Throw<ComicException>()
                .WithMessage("ComicId is not valid.");
        }

        [TestMethod]
        public void AddComic_Invalid_PublisherNameIsEmpty()
        {
            //Arrange
            var comic = new Comic(1, "De parameterloze comic", new Publisher(1, "Rell"), new Serie(1, "Legendes van de league"), new List<Author>() { new Author(1, "Steve"), new Author(2, "Yannick") }, 1, 0);

            //Act
            Action act = () =>
            {
                comic.SetPublisher(null);
            };
            //Assert
            act.Should().Throw<ComicException>()
                .WithMessage("Publisher name is empty.");
        }

        [TestMethod]
        public void AddComic_Invalid_SerieNameIsEmpty()
        {
            //Arrange
            var comic = new Comic(1, "De parameterloze comic", new Publisher(1, "Rell"), new Serie(1, "Legendes van de league"), new List<Author>() { new Author(1, "Steve"), new Author(2, "Yannick") }, 1, 0);

            //Act
            Action act = () =>
            {
                comic.SetSerie(new Serie(1, ""));
            };
            //Assert
            act.Should().Throw<SerieException>()
                .WithMessage("Serie name is empty.");
        }

        [TestMethod]
        public void AddComic_Invalid_SerieIdIsZeroOrNegative()
        {
            //Arrange
            var comic = new Comic(1, "De parameterloze comic", new Publisher(1, "Rell"), new Serie(1, "Legendes van de league"), new List<Author>() { new Author(1, "Steve"), new Author(2, "Yannick") }, 1, 0);
            var serie = new Serie(1, "Koolio");
            serie.Id = 0;
            //Act
            Action act = () =>
            {
                comic.SetSerie(serie);
            };
            //Assert
            act.Should().Throw<ComicException>()
                .WithMessage("SerieId is empty.");
        }

        [TestMethod]
        public void AddComic_Invalid_EmptyAuthorList()
        {
            //Arrange
            var authorList = new List<Author>();
            //Act
            Action act = () =>
            {
                var comic = new Comic(1, "De parameterloze comic", new Publisher(1, "Rell"), new Serie(1, "Legendes van de league"), authorList, 1, 0);
            };
            //Assert
            act.Should().Throw<ComicException>()
                .WithMessage("Atleast one author needs to be added.");
        }

        [TestMethod]
        public void AddComic_Invalid_AuthorIsAlreadyAdded()
        {
            //Arrange
            var authorList = new List<Author>() { new Author(1, "Kcinnay") };
            var comic = new Comic(1, "De parameterloze comic", new Publisher(1, "Rell"), new Serie(1, "Legendes van de league"), authorList, 1, 0);
            //Act
            Action act = () =>
            {
                comic.SetAuthors(new List<Author>() { new Author(1, "Kcinnay") });
            };
            //Assert
            act.Should().Throw<ComicException>()
                .WithMessage("Author is already added for this comic.");
        }

        [TestMethod]
        public void AddComic_Invalid_SerieSeqNumberIsZeroOrNegative()
        {
            //Arrange
            var authorList = new List<Author>() { new Author(1, "Kcinnay") };

            //Act
            Action act = () =>
            {
                var comic = new Comic(1, "De parameterloze comic", new Publisher(1, "Rell"), new Serie(1, "Legendes van de league"), authorList, 0, 0);
            };
            //Assert
            act.Should().Throw<ComicException>()
                .WithMessage("SerieSeqNumber is not valid.");
        }

        [TestMethod]
        public void AddComic_Invalid_Quantity()
        {
            //Arrange
            var comic = new Comic(1, "De parameterloze comic", new Publisher(1, "Rell"), new Serie(1, "Legendes van de league"), new List<Author>() { new Author(1, "Steve"), new Author(2, "Yannick") }, 1, 0);

            //Act
            Action act = () =>
            {
                comic.AddAantal(0);
            };
            //Assert
            act.Should().Throw<ComicException>()
                .WithMessage("Amount is not valid.");
        }
        [TestMethod]
        public void AddComic_Valid_Quantity()
        {
            //Arrange
            var comic = new Comic(1, "De parameterloze comic", new Publisher(1, "Rell"), new Serie(1, "Legendes van de league"), new List<Author>() { new Author(1, "Steve"), new Author(2, "Yannick") }, 1, 0);

            //Act
            Action act = () =>
            {
                comic.AddAantal(1);
            };
            //Assert
            act.Should().NotThrow<ComicException>();
        }

        [TestMethod]
        public void AddComic_Invalid_RemoveQuantity()
        {
            //Arrange
            var comic = new Comic(1, "De parameterloze comic", new Publisher(1, "Rell"), new Serie(1, "Legendes van de league"), new List<Author>() { new Author(1, "Steve"), new Author(2, "Yannick") }, 1, 0);

            //Act
            Action act = () =>
            {
                comic.RemoveAantal(0);
            };
            //Assert
            act.Should().Throw<ComicException>()
                .WithMessage("Amount is not valid.");
        }
        [TestMethod]
        public void AddComic_Invalid_RemoveQuantity_NotEnoughStock()
        {
            //Arrange
            var comic = new Comic(1, "De parameterloze comic", new Publisher(1, "Rell"), new Serie(1, "Legendes van de league"), new List<Author>() { new Author(1, "Steve"), new Author(2, "Yannick") }, 1, 0);

            //Act
            Action act = () =>
            {
                comic.RemoveAantal(1);
            };
            //Assert
            act.Should().Throw<ComicException>()
                .WithMessage("Not enough stock");
        }

        [TestMethod]
        public void AddComic_Valid_RemoveQuantity_EnoughStock()
        {
            //Arrange
            var comic = new Comic(1, "De parameterloze comic", new Publisher(1, "Rell"), new Serie(1, "Legendes van de league"), new List<Author>() { new Author(1, "Steve"), new Author(2, "Yannick") }, 1, 10);

            //Act
            Action act = () =>
            {
                comic.RemoveAantal(1);
            };
            //Assert
            act.Should().NotThrow<ComicException>();
        }


        [TestMethod]
        public void AddComic_Invalid_ComicExist()
        {
            //Arrange
            ComicManager cm = new ComicManager(new UnitOfWork(connectionString));
            var comic = cm.GetAllComics().First(x => x.Id == 2);
            //Act
            Action act = () => { cm.AddComic(comic); };
            //Assert
            act.Should().Throw<ComicException>()
                .WithMessage("Comic already exist.");
        }

        #endregion

        #region UpdateComic

        [TestMethod]
        public void UpdateComic_Valid()
        {
            //integration test
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                ComicManager cm = new ComicManager(new UnitOfWork(connectionString));
                PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                Comic comic = cm.GetAllComics().First(x => x.Id == 6); // Jens in db
                comic.SetPublisher(pm.GetAllPublishers().First(x => x.Id == 2003));
                //Act
                Action act = () => { cm.UpdateComic(comic); };
                //Assert
                act.Should().NotThrow<ComicException>();
            }
        }

        //[TestMethod]
        //public void UpdateComic_Invalid_DoesNotExist()
        //{
        //    //Act
        //    Action act = () => { new Comic("", new Publisher("Steve"), new Serie("TestjesSchrijven"), new List<Author>(), null); };
        //    //Assert
        //    act.Should().Throw<Exception>();
        //}

        //[TestMethod]
        //public void UpdateComic_Invalid_ComicExist()
        //{
        //    //Arrange
        //    ComicManager cm = new ComicManager(new UnitOfWork(connectionString));
        //    var comic = cm.GetAllComics().First(x => x.Id == 1);
        //    //Act
        //    Action act = () => { cm.AddComic(comic); };
        //    //Assert
        //    act.Should().Throw<ComicException>()
        //        .WithMessage("Comic already exist.");
        //}

        #endregion

        #region SearchComics

        [TestMethod]
        public void SearchComics_Valid()
        {
            //integration test
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                ComicManager cm = new ComicManager(new UnitOfWork(connectionString));
                var comic = cm.GetAllComics().First(x => x.Id == 2);
                //Act
                Action act = () => { cm.SearchComics("Jens is toch niet zo lelijk", null, null, null, null); };
                //Assert
                act.Should().NotThrow<ComicException>();
                act.Equals(comic);
            }
        }


        #endregion

        #region GetAllComics
        [TestMethod]
        public void GetAllComic_IsValid_ReturnsComics()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                ComicManager cm = new ComicManager(new UnitOfWork(connectionString));
                //Act
                Action act = () => { cm.GetAllComics(); };
                var comicList = cm.GetAllComics();
                //Assert
                act.Should().NotThrow<ComicException>();
                comicList.Count.Should().Be(9);


            }
        }

        #endregion

        #region GetComicIfExistElseCreate

        [TestMethod]
        public void GetComicIfExistElseCreate_IsValid_ReturnsComic()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                ComicManager cm = new ComicManager(new UnitOfWork(connectionString));
                var comic = cm.GetAllComics().First(x => x.Id == 2);
                //Act
                Action act = () => { cm.GetComicIfExistElseCreate(comic); };
                //Assert
                act.Equals(comic);
            }
        }

        [TestMethod]
        public void GetComicIfExistElseCreate_IsValid_AddsComic()
        {
            using (var x = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //Arrange
                ComicManager cm = new ComicManager(new UnitOfWork(connectionString));
                PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                var comic = cm.GetAllComics().First(x => x.Id == 2);
                comic.SetPublisher(pm.GetAllPublishers().First(x => x.Id == 4));
                //Act
                Action act = () => { cm.GetComicIfExistElseCreate(comic); };
                //Assert
                act.Should().NotThrow<Exception>();
                act.Equals(comic);
            }
        }

        #endregion


    }
}






