using System.Collections.Generic;
using BusinessLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IComicRepository
    {
        Comic AddComic(Comic comic);
        void UpdateComic(Comic comic);
        void RemoveComic(Comic comic);
        List<ComicResult> SearchComics(string title, string authorName, string serieName, string publisherName, int? serieSeqNumber);
        List<Comic> AllComics();
        void RemoveComics(List<int> comicIds);
        void UpdateAantal(Comic comic);
        Comic GetComic(int id);
    }
}