namespace BusinessLayer.Interfaces
{
    using BusinessLayer.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="IAuthorRepository" />.
    /// </summary>
    public interface IAuthorRepository
    {
        /// <summary>
        /// The AddAuthor.
        /// </summary>
        /// <param name="author">The author<see cref="Author"/>.</param>
        /// <returns>The <see cref="Author"/>.</returns>
        Author AddAuthor(Author author);

        /// <summary>
        /// The UpdateAuthor.
        /// </summary>
        /// <param name="author">The author<see cref="Author"/>.</param>
        void UpdateAuthor(Author author);

        /// <summary>
        /// The RemoveAuthor.
        /// </summary>
        /// <param name="author">The author<see cref="Author"/>.</param>
        void RemoveAuthor(Author author);

        /// <summary>
        /// The AllAuthors.
        /// </summary>
        /// <returns>The <see cref="List{Author}"/>.</returns>
        List<Author> AllAuthors();

        /// <summary>
        /// The GetAllAuthors.
        /// </summary>
        /// <returns>The <see cref="Dictionary{int, Author}"/>.</returns>
        Dictionary<int, Author> GetAllAuthors();

        /// <summary>
        /// The RemoveAuthors.
        /// </summary>
        /// <param name="authorIds">The authorIds<see cref="List{int}"/>.</param>
        void RemoveAuthors(List<int> authorIds);
    }
}
