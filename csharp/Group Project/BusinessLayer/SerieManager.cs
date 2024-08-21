using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;

namespace BusinessLayer
{
    /// <summary>
    ///     Defines the <see cref="SerieManager" />.
    /// </summary>
    public class SerieManager
    {
        /// <summary>
        ///     Defines the _allSeries.
        /// </summary>
        private List<Serie> _allSeries;

        /// <summary>
        ///     Defines the _uow.
        /// </summary>
        private readonly IUnitOfWork _uow;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SerieManager" /> class.
        /// </summary>
        /// <param name="_uow">The _uow<see cref="IUnitOfWork" />.</param>
        public SerieManager(IUnitOfWork _uow)
        {
            this._uow = _uow;
        }

        /// <summary>
        ///     Gets the AllSeries.
        /// </summary>
        private List<Serie> AllSeries
        {
            get
            {
                if (_allSeries == null || _allSeries.Count == 0) _allSeries = GetAllSeries();

                return _allSeries;
            }
        }

        /// <summary>
        ///     The AddSerie.
        /// </summary>
        /// <param name="serie">The serie<see cref="Serie" />.</param>
        /// <returns>The <see cref="Serie" />.</returns>
        public Serie AddSerie(Serie serie)
        {
            var serieNameExist = AllSeries.Any(x => x.Name.ToUpper() == serie.Name.ToUpper());
            if (!serieNameExist)
            {
                serie = _uow.SerieRepo.AddSeries(serie);
                AllSeries.Add(serie);
            }
            else
            {
                throw new SerieException("Serie already exist.");
            }

            return serie;
        }

        /// <summary>
        ///     The UpdateSerie.
        /// </summary>
        /// <param name="serie">The serie<see cref="Serie" />.</param>
        public void UpdateSerie(Serie serie)
        {
            //We vullen AllSeries op in zijn get method
            //Alle series oproepen
            //Serie die geupdate moet worden controlleren of deze nog niet bestaat
            var serieNameExist = AllSeries.Any(x => x.Name.ToUpper() == serie.Name.ToUpper());
            if (!serieNameExist)
            { 
                _uow.SerieRepo.UpdateSerie(serie);

            }
            else
            {
                throw new SerieException("Serie already exist.");
            }
        }

        /// <summary>
        ///     The RemoveSerie.
        /// </summary>
        /// <param name="serie">The serie<see cref="Serie" />.</param>
        public void RemoveSerie(Serie serie)
        {
            var allComics = _uow.ComicRepo.AllComics();
            //Controleren of de serie geen comics meer heeft
            var serieHasComics = allComics.Any(x => x.Serie != null && x.Serie.Id == serie.Id);
            if (serieHasComics) throw new SerieException("Can't remove series because it still has comics.");
            _uow.SerieRepo.RemoveSerie(serie);
        }

        /// <summary>
        ///     The SearchBySerieId.
        /// </summary>
        /// <param name="serieId">The serieId<see cref="int" />.</param>
        /// <returns>The <see cref="Serie" />.</returns>
        public Serie SearchBySerieId(int serieId)
        {
            return _uow.SerieRepo.SearchBySerieId(serieId);
        }

        /// <summary>
        ///     The GetAllSeries.
        /// </summary>
        /// <returns>The <see cref="List{Serie}" />.</returns>
        public List<Serie> GetAllSeries()
        {
            return _uow.SerieRepo.AllSeries();
        }

        /// <summary>
        ///     The GetSerieIfExistElseCreate.
        /// </summary>
        /// <param name="serie">The serie<see cref="Serie" />.</param>
        /// <returns>The <see cref="Serie" />.</returns>
        public Serie GetSerieIfExistElseCreate(Serie serie)
        {
            //We spreken AllSeries aan dus het vult zijn eigen automatisch op 
            var existingSerie = AllSeries.Where(x => x.Name == serie.Name).FirstOrDefault();
            if (existingSerie != null) return existingSerie;
            serie = AddSerie(new Serie(serie.Name));

            return serie;
        }
    }
}