namespace BusinessLayer
{
    using BusinessLayer.Entities;
    using BusinessLayer.Exceptions;
    using BusinessLayer.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="ComicManager" />.
    /// </summary>
    public class ComicManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComicManager"/> class.
        /// </summary>
        /// <param name="uow">The uow<see cref="IUnitOfWork"/>.</param>
        public ComicManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Defines the _uow.
        /// </summary>
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// Defines the _allComics.
        /// </summary>
        private List<Comic> _allComics;


        /// <summary>
        /// Gets the AllComics.
        /// </summary>
        public List<Comic> AllComics
        {
            get
            {
                if (_allComics == null || _allComics.Count == 0)
                {

                    _allComics = GetAllComics();
                }
                return _allComics;
            }
        }

        /// <summary>
        /// The AddComic.
        /// </summary>
        /// <param name="comic">The comic<see cref="Comic"/>.</param>
        /// <returns>The <see cref="Comic"/>.</returns>
        public Comic AddComic(Comic comic)
        {
            
            bool comicTitleExistWithSerie = AllComics.Any(x => x.Title.ToLower() == comic.Title.ToLower()
                                            && ((x.Serie != null
                                                    && comic.Serie != null
                                                    && x.Serie.Id == comic.Serie.Id
                                                    && x.SerieSeqNumber == comic.SerieSeqNumber
                                                    && x.Publisher.Id == comic.Publisher.Id
                                                    && x.GetAuthors().SequenceEqual(comic.GetAuthors()))
                                                || (comic.Serie == null
                                                    && x.Publisher.Id == comic.Publisher.Id
                                                    && x.GetAuthors().SequenceEqual(comic.GetAuthors()))));

            bool isSerie_SerieNumberUnique = AllComics.Any(x => x.Serie != null
                                             && comic.Serie != null
                                             && x.Serie.Id == comic.Serie.Id
                                             && x.SerieSeqNumber != null
                                             && x.SerieSeqNumber == comic.SerieSeqNumber
                                             && x.Publisher.Id == comic.Publisher.Id);

            if ((!comicTitleExistWithSerie && !isSerie_SerieNumberUnique))
            {
                comic = _uow.ComicRepo.AddComic(comic);
                AllComics.Add(comic);
            }
            if (comicTitleExistWithSerie)
            {
                throw new ComicException("Comic already exist.");
            }
            if (isSerie_SerieNumberUnique)
            {
                throw new ComicException("Serie and Serie° already exist.");
            }
            
            return comic;
        }

        /// <summary>
        /// The UpdateComic.
        /// </summary>
        /// <param name="comic">The comic<see cref="Comic"/>.</param>
        public void UpdateComic(Comic comic)
        {
            //Checken of de comic reeds bestaat
            //Checken of het een geldig ID is 

            _uow.ComicRepo.UpdateComic(comic);
        }

        /// <summary>
        /// The RemoveComic.
        /// </summary>
        /// <param name="comics">The comics<see cref="List{Comic}"/>.</param>
        public void RemoveComic(List<Comic> comics)
        {
            foreach (Comic comic in comics)
            {
                //Voor elke comicAuthor de waarden controleren,
                //indien correct de comic en comicAuthor verwijderen
                if (comic.Id <= 0) throw new ComicException("No comic has been selected.");
                _uow.ComicRepo.RemoveComic(comic);
            }
        }

        /// <summary>
        /// The SearchComics.
        /// </summary>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <param name="authorName">The authorName<see cref="string"/>.</param>
        /// <param name="serieName">The serieName<see cref="string"/>.</param>
        /// <param name="publisherName">The publisherName<see cref="string"/>.</param>
        /// <param name="serieSeqNumber">The serieSeqNumber<see cref="int?"/>.</param>
        /// <returns>The <see cref="List{ComicResult}"/>.</returns>
        public List<ComicResult> SearchComics(string title, string authorName, string serieName, string publisherName, int? serieSeqNumber)
        {
            //Controleren of er comics gevonden worden,
            //Indien alles correct => lijst weergeven
            List<ComicResult> result = _uow.ComicRepo.SearchComics(title, authorName, serieName, publisherName, serieSeqNumber);

            return result;
        }

        /// <summary>
        /// The GetAllComics.
        /// </summary>
        /// <returns>The <see cref="List{Comic}"/>.</returns>
        public List<Comic> GetAllComics()
        {
            //Alle comics opvragen en weergeven
            return _uow.ComicRepo.AllComics();
        }

        /// <summary>
        /// The GetComic.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Comic"/>.</returns>
        public Comic GetComic(int id)
        {
            //1 opvragen en weergeven
            return _uow.ComicRepo.GetComic(id);
        }

        /// <summary>
        /// The GetComicIfExistElseCreate.
        /// </summary>
        /// <param name="comic">The comic<see cref="Comic"/>.</param>
        /// <returns>The <see cref="Comic"/>.</returns>
        public Comic GetComicIfExistElseCreate(Comic comic)
        {
            var existingComic = AllComics.FirstOrDefault(x => x.Title.ToUpper() == comic.Title.ToUpper() && x.Serie.Id == comic.Serie.Id && x.SerieSeqNumber == comic.SerieSeqNumber);
            if (existingComic != null)
            {
                return existingComic;
            }
            comic = AddComic(comic);

            return comic;
        }
    }
}
