namespace DataLayer.Repositories
{
    using BusinessLayer.Entities;
    using BusinessLayer.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Defines the <see cref="ComicAuthorRepository" />.
    /// </summary>
    public class ComicAuthorRepository : IComicAuthorRepository
    {
        /// <summary>
        /// Gets the _connectionString.
        /// </summary>
        private string _connectionString { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComicAuthorRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connectionString<see cref="string"/>.</param>
        public ComicAuthorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// The AddComicAuthor.
        /// </summary>
        /// <param name="comicAuthor">The comicAuthor<see cref="ComicAuthor"/>.</param>
        public void AddComicAuthor(ComicAuthor comicAuthor)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            string addComicAuthor = @"Insert into ComicAuthor(ComicId, AuthorId)
                                     Values(@ComicId,@AuthorId)";

            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@ComicId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@AuthorId", SqlDbType.Int));
                    command.CommandText = addComicAuthor;

                    command.Parameters["@ComicId"].Value = comicAuthor.ComicId;
                    command.Parameters["@AuthorId"].Value = comicAuthor.AuthorId;
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// The RemoveComicAuthor.
        /// </summary>
        /// <param name="comicAuthor">The comicAuthor<see cref="ComicAuthor"/>.</param>
        public void RemoveComicAuthor(ComicAuthor comicAuthor)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = @$"DELETE FROM comicAuthor 
                          WHERE ComicId = @ComicId and AuthorId = @AuthorId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@ComicId", comicAuthor.ComicId));
            command.Parameters.Add(new SqlParameter("@AuthorId", comicAuthor.AuthorId));
            connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// The RemoveAllAuthorsForComicId.
        /// </summary>
        /// <param name="comicId">The comicId<see cref="int"/>.</param>
        public void RemoveAllAuthorsForComicId(int comicId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = @$"DELETE FROM comicAuthor 
                          WHERE ComicId = @ComicId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@ComicId", comicId));
            connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// The RemoveAllComicAuthorsForComicIds.
        /// </summary>
        /// <param name="comicIds">The comicIds<see cref="List{int}"/>.</param>
        public void RemoveAllComicAuthorsForComicIds(List<int> comicIds)
        {
            //We moeten geen zorgen maken over parameters, want we krijgen een list van ints binnen waarvan we een string maken.
            //Dus sql injection is niet mogelijk.
            var idString = string.Join(",", comicIds);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @$"DELETE FROM comicAuthor 
                          WHERE ComicId in ({idString})";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
