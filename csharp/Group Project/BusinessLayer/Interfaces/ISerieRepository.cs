using System.Collections.Generic;
using BusinessLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface ISerieRepository
    {
        Serie AddSeries(Serie serie);
        void UpdateSerie(Serie serie);
        void RemoveSerie(Serie serie);
        Serie SearchBySerieId(int serieId);
        List<Serie> AllSeries();
        Dictionary<int, Serie> GetAllSeries();
        void RemoveSeries(List<int> serieIds);
    }
}
