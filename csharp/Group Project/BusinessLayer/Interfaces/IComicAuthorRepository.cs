using System.Collections.Generic;
using BusinessLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IComicAuthorRepository
    {
        void RemoveAllAuthorsForComicId(int comicId);
        void RemoveAllComicAuthorsForComicIds(List<int> comicIds);
        void RemoveComicAuthor(ComicAuthor comicAuthor);
    }
}
