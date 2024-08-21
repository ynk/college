namespace ImportExportComics
{
    using BusinessLayer;
    using BusinessLayer.Entities;
    using DataLayer;
    using ImportExportComics.JsonClasses;
    using Microsoft.Win32;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Interaction logic for ImportExport.xaml.
    /// </summary>
    public partial class ImportExport : Window
    {
        /// <summary>
        /// Defines the _authorManager.
        /// </summary>
        private readonly AuthorManager _authorManager;

        /// <summary>
        /// Defines the _comicManager.
        /// </summary>
        private readonly ComicManager _comicManager;

        /// <summary>
        /// Defines the _importExportManager.
        /// </summary>
        private readonly ImportExportManager _importExportManager;

        /// <summary>
        /// Defines the _publisherManager.
        /// </summary>
        private readonly PublisherManager _publisherManager;

        /// <summary>
        /// Defines the _serieManager.
        /// </summary>
        private readonly SerieManager _serieManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExport"/> class.
        /// </summary>
        public ImportExport()
        {
            InitializeComponent();
            var uow = new UnitOfWork(Properties.Resources.ConnectionString);
            _importExportManager = new ImportExportManager(uow);
            _authorManager = new AuthorManager(uow);
            _comicManager = new ComicManager(uow);
            _publisherManager = new PublisherManager(uow);
            _serieManager = new SerieManager(uow);
        }

        //zoeken base file
        /// <summary>
        /// The BtnSearchImport_OnClick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void BtnSearchImport_OnClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog().GetValueOrDefault()) InputImport.Text = fileDialog.FileName;
        }

        //zoeken van export file
        /// <summary>
        /// The BtnSearchExport_OnClick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void BtnSearchExport_OnClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.CheckFileExists = false;
            if (fileDialog.ShowDialog().GetValueOrDefault()) InputExport.Text = fileDialog.FileName;
        }

        //effectief importen
        /// <summary>
        /// The BtnImport_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            //Knop mag niet werken als er geen invoer is
            if (File.Exists(InputImport.Text))
            {
                ImportJson(InputImport.Text);
                MessageBox.Show("Imported", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Geen file geselecteerd.", "Error", MessageBoxButton.OK,MessageBoxImage.Error) ;
            }
        }

        //effectief exporten
        /// <summary>
        /// The BtnExport_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExportJson(InputExport.Text);
                MessageBox.Show("Exported", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK,MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// The ImportJson.
        /// </summary>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        public void ImportJson(string filePath)
        {
            var strips = ImportExportManager.JSONSerializer<List<JsonComic>>.DeSerialize(filePath);
            var comicAuthorList = new ConcurrentDictionary<(int, int), ComicAuthor>();
            var errorList = new List<JsonComic>();

            //het omzetten van een jsonComic entity naar een Comic entity 
            foreach (var strip in strips)
            {
                try
                {
                    var title = strip.Titel;

                    var publisherName = strip.Uitgeverij.Naam;
                    var publisher = new Publisher(publisherName);
                    publisher = _publisherManager.GetPublisherIfExistElseCreate(publisher);


                    var serieName = strip.Reeks.Naam;
                    var serie = new Serie(serieName);
                    serie = _serieManager.GetSerieIfExistElseCreate(serie);

                    var serieSeqNumber = strip.Nr;

                    var authors = new List<Author>();
                    foreach (var auteur in strip.Auteurs)
                    {
                        var authorName = auteur.Naam;
                        var author = new Author(authorName);
                        author = _authorManager.GetAuthorIfExistElseCreate(author);
                        authors.Add(author);
                    }
                    var comic = new Comic(title, publisher, serie, authors, serieSeqNumber);
                    comic = _comicManager.GetComicIfExistElseCreate(comic);
                    foreach (var author in authors)
                    {
                        var comicAuthor = new ComicAuthor();
                        comicAuthor.ComicId = comic.Id;
                        comicAuthor.AuthorId = author.Id;
                        //TryAdd, voegt erin als hij nog niet erin zit, anders wordt het niet toegevoegd
                        comicAuthorList.TryAdd((comicAuthor.ComicId, comicAuthor.AuthorId), comicAuthor);
                    }
                }
                catch (Exception)
                {
                    errorList.Add(strip);
                }
            }

            _importExportManager.SaveAllToDB(_comicManager.AllComics, comicAuthorList.Values.ToList());
            var filePathError = filePath.Replace(".json", "_Error.json");
            ImportExportManager.JSONSerializer<List<JsonComic>>.Serialize(errorList, filePathError);
        }

        /// <summary>
        /// The ExportJson.
        /// </summary>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        public void ExportJson(string filePath)
        {
            //ophalen uit db omzetten naarJsonComic
            var comics = _comicManager.GetAllComics();
            var jsonComicList = new List<JsonComic>();
            foreach (var comic in comics)
            {
                var jsonComic = new JsonComic();

                jsonComic.ID = comic.Id;
                jsonComic.Titel = comic.Title;
                jsonComic.Nr = comic.SerieSeqNumber;

                var uitgeverij = new Uitgeverij();
                uitgeverij.ID = comic.Publisher.Id;
                uitgeverij.Naam = comic.Publisher.Name;
                jsonComic.Uitgeverij = uitgeverij;

                var reeks = new Reeks();
                reeks.ID = comic.Serie.Id;
                reeks.Naam = comic.Serie.Name;
                reeks.Strips = new List<string>();
                jsonComic.Reeks = reeks;

                var auteurList = new List<Auteur>();
                foreach (var author in comic.GetAuthors())
                {
                    var auteur = new Auteur();
                    auteur.ID = author.Id;
                    auteur.Naam = author.Name;
                    auteurList.Add(auteur);
                }

                jsonComic.Auteurs = auteurList;
                jsonComicList.Add(jsonComic);
            }
            ImportExportManager.JSONSerializer<List<JsonComic>>.Serialize(jsonComicList, filePath);
        }
    }
}
