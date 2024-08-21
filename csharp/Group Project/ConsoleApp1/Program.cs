using BusinessLayer;
using BusinessLayer.Entities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=0.0.0.0;Initial Catalog=StripCatalogus_test;Persist Security Info=True;User ID=SA;Password=SuperSecretPassword";

          
            ComicManager cm = new ComicManager(new UnitOfWork(connectionString));
            //Dictionary<int,Comic> testComic = cm.GetAllComicsDict();
            Publisher publisher = new Publisher(4, "Delete Me");
            Serie serie = new Serie(1, "TestSerie");
            Author author = new Author(1005, "Yanick");
            Author author2 = new Author(1003, "Joachim");
            List<Author> authors = new List<Author>() { author, author2 };
            Comic comic1 = new Comic("Aldi", publisher, null, authors, null);
            cm.AddComic(comic1);


            //AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
            //List<Author> authors = am.AllAuthors();


        }
    }
}
