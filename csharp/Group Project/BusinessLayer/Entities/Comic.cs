using BusinessLayer.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Entities
{
    public class Comic
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public Publisher Publisher { get; private set; }
        public Serie Serie { get; private set; }
        private readonly List<Author> _authors = new List<Author>();
        public int? SerieSeqNumber { get; private set; }
        public int Aantal { get; private set; }
        

        public Comic(string title, Publisher publisher, Serie serie, List<Author> authors, int? serieSeqNumber)
        {
            SetTitle(title);
            SetPublisher(publisher);
            SetSerie(serie);
            SetAuthors(authors);
            SetSerieSeqNumber(serieSeqNumber);
        }

        public Comic(int id, string title, Publisher publisher, Serie serie, List<Author> authors, int? serieSeqNumber,
            int aantal) : this(title, publisher, serie, authors, serieSeqNumber)
        {
            SetId(id);
            Aantal = aantal;
        }

        public IReadOnlyList<Author> GetAuthors()
        {
            return _authors.AsReadOnly();
        }
        public void RemoveAuthor(Author author)
        {
            bool authorExist = DoesComicHasAuthor(author);
            if (!authorExist)
            {
                _authors.Remove(author);
            }
            else
            {
                throw new ComicException("The author does not exist for this comic.");
            }
        }
        public void AddAuthor(Author author)
        {
            bool authorExist = DoesComicHasAuthor(author);
            if (!authorExist)
            {
                _authors.Add(author);
            }
            else
            {
                throw new ComicException("The author is already added for this comic.");
            }
        }
        public bool DoesComicHasAuthor(Author author)
        {
            IReadOnlyList<Author> authors = GetAuthors();
            return authors.Any(x => x.Id == author.Id);
        }

        private void SetId(int id)
        {
            if (id <= 0) throw new ComicException("ComicId is not valid.");
            Id = id;
        }
        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ComicException("Title is empty.");
            Title = title;
        }
        public void SetPublisher(Publisher publisher)
        {
            if (publisher == null) throw new ComicException("Publisher name is empty.");
            Publisher = publisher;

        }
        public void SetSerie(Serie serie)
        {
            if (serie != null)
            {
                if (string.IsNullOrWhiteSpace(serie.Name)) throw new ComicException("Serie name is empty.");
                if (serie.Id <= 0) throw new ComicException("SerieId is empty.");
                Serie = serie;
            }
            else
            {
                Serie = null;
            }
        }
        public void SetAuthors(List<Author> authors)
        {
            if (authors.Count <= 0) throw new ComicException("Atleast one author needs to be added.");
            foreach (Author author in authors)
            {
                if (DoesComicHasAuthor(author)) throw new ComicException("Author is already added for this comic.");
                _authors.Add(author);
            }
        }
        public void SetSerieSeqNumber(int? serieSeqNumber)
        {
            if (serieSeqNumber <= 0) throw new ComicException("SerieSeqNumber is not valid.");
            SerieSeqNumber = serieSeqNumber;
        }

        public void AddAantal(int amount)
        {
            if (amount <= 0) throw new ComicException("Amount is not valid.");
            Aantal += amount;
        }
        public void RemoveAantal(int amount)
        {
            if (amount <= 0) throw new ComicException("Amount is not valid.");
            if(Aantal- amount < 0) throw new ComicException("Not enough stock");
            Aantal -= amount;
        }
    }
}
