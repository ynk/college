namespace BusinessLayer
{
    using BusinessLayer.Entities;
    using BusinessLayer.Exceptions;
    using BusinessLayer.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="AuthorManager" />.
    /// </summary>
    public class AuthorManager
    {
        /// <summary>
        /// Defines the _uow.
        /// </summary>
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// Defines the _allAuthors.
        /// </summary>
        private List<Author> _allAuthors;

        /// <summary>
        /// Gets the AllAuthors.
        /// </summary>
        private List<Author> AllAuthors
        {
            get
            {
                if (_allAuthors == null || _allAuthors.Count == 0)
                {
                    _allAuthors = GetAllAuthors();
                }
                return _allAuthors;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorManager"/> class.
        /// </summary>
        /// <param name="uow">The uow<see cref="IUnitOfWork"/>.</param>
        public AuthorManager(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        /// <summary>
        /// The AddAuthor.
        /// </summary>
        /// <param name="author">The author<see cref="Author"/>.</param>
        /// <returns>The <see cref="Author"/>.</returns>
        public Author AddAuthor(Author author)
        {
            //Checken of de author met dezelfde naam nog niet bestaat
            //Indien false mag hij toegevoegd worden
           
            bool authorNameExist = AllAuthors.Any(x => x.Name.ToUpper() == author.Name.ToUpper());
            if (!authorNameExist)
            {
                author = _uow.AuthorRepo.AddAuthor(author);
                AllAuthors.Add(author);
            }
            else
            {
                throw new AuthorException("Author already exist.");
            }

            return author;
            
        }

        /// <summary>
        /// The UpdateAuthor.
        /// </summary>
        /// <param name="author">The author<see cref="Author"/>.</param>
        public void UpdateAuthor(Author author)
        {
            //Checken of er nog geen author bestaat met dezelfde naam
            //Indien false mag hij geupdate worden
            bool authorNameExist = AllAuthors.Any(x => x.Name.ToUpper() == author.Name.ToUpper());
            if (!authorNameExist)
            {
                _uow.AuthorRepo.UpdateAuthor(author);
            }
            else
            {
                throw new AuthorException("Author already exist.");
            }
        }

        /// <summary>
        /// The RemoveAuthor.
        /// </summary>
        /// <param name="author">The author<see cref="Author"/>.</param>
        public void RemoveAuthor(Author author)
        {
            List<ComicResult> allComics = _uow.ComicRepo.SearchComics(null, null, null, null, null);

            //Controleren of de author geen comics meer heeft
            bool authorHasComics = allComics.Any(x => x.AuthorName.Contains(author.Name));
            if (authorHasComics) throw new AuthorException("Author still has comics.");
            _uow.AuthorRepo.RemoveAuthor(author);
        }

        /// <summary>
        /// The GetAllAuthors.
        /// </summary>
        /// <returns>The <see cref="List{Author}"/>.</returns>
        public List<Author> GetAllAuthors()
        {
            return _uow.AuthorRepo.AllAuthors();
        }

        /// <summary>
        /// The GetAuthorIfExistElseCreate.
        /// </summary>
        /// <param name="author">The author<see cref="Author"/>.</param>
        /// <returns>The <see cref="Author"/>.</returns>
        public Author GetAuthorIfExistElseCreate(Author author)
        {
            //We spreken AllAuthors aan dus het vult zijn eigen automatisch op 
            var existingAuthor = AllAuthors.Where(x => x.Name == author.Name).FirstOrDefault();
            if (existingAuthor != null)
            {
                return existingAuthor;
            }
            author = AddAuthor(new Author(author.Name));

            return author;
        }

        /// <summary>
        /// The DoesAuthorExist.
        /// </summary>
        /// <param name="authorName">The authorName<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool DoesAuthorExist(string authorName)
        {
            //We spreken AllAuthors aan dus het vult zijn eigen automatisch op 
            bool doesAuthorExist = AllAuthors.Any(x => x.Name.ToLower() == authorName.ToLower());
            return doesAuthorExist;
        }
    }
}
