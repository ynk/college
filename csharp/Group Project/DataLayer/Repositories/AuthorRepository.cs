namespace DataLayer.Repositories
{
    using BusinessLayer.Entities;
    using BusinessLayer.Exceptions;
    using BusinessLayer.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Defines the <see cref="AuthorRepository" />.
    /// </summary>
    public class AuthorRepository : IAuthorRepository
    {
        /// <summary>
        /// Gets the _connectionString.
        /// </summary>
        private string _connectionString { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connectionString<see cref="string"/>.</param>
        public AuthorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// The AddAuthor.
        /// </summary>
        /// <param name="author">The author<see cref="Author"/>.</param>
        /// <returns>The <see cref="Author"/>.</returns>
        public Author AddAuthor(Author author)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = @"INSERT INTO Author (Name) output INSERTED.ID VALUES(@Name)";
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));
                    command.CommandText = query;

                    command.Parameters["@Name"].Value = author.Name;
                    int id = (int)command.ExecuteScalar();
                    return new Author(id, author.Name);
                }
                catch (Exception ex)
                {
                    throw new AuthorException(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// The RemoveAuthor.
        /// </summary>
        /// <param name="author">The author<see cref="Author"/>.</param>
        public void RemoveAuthor(Author author)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = @$"DELETE FROM Author 
                            WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@Id", author.Id));
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
        /// The RemoveAuthors.
        /// </summary>
        /// <param name="authorIds">The authorIds<see cref="List{int}"/>.</param>
        public void RemoveAuthors(List<int> authorIds)
        {
            var idString = string.Join(",", authorIds);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @$"DELETE FROM Author 
                            WHERE Id in ({idString})";
                SqlCommand command = new SqlCommand(query, connection);
                //command.Parameters.Add(new SqlParameter("@Id", author.Id));
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

        /// <summary>
        /// The UpdateAuthor.
        /// </summary>
        /// <param name="author">The author<see cref="Author"/>.</param>
        public void UpdateAuthor(Author author)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = @$"UPDATE Author 
                            SET Name = @Name 
                            WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@Name", author.Name));
            command.Parameters.Add(new SqlParameter("@Id", author.Id));

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
        /// The AllAuthors.
        /// </summary>
        /// <returns>The <see cref="List{Author}"/>.</returns>
        public List<Author> AllAuthors()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = @$"SELECT Id, a.Name 
                            FROM Author a
                            ORDER BY a.Name";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                List<Author> foundAuthorList = new List<Author>();

                while (reader.Read())
                {
                    int id = int.Parse(reader["Id"].ToString());
                    string authorName = reader["Name"].ToString();
                    Author author = new Author(id, authorName);
                    foundAuthorList.Add(author);
                }
                return foundAuthorList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// The GetAllAuthors.
        /// </summary>
        /// <returns>The <see cref="Dictionary{int, Author}"/>.</returns>
        public Dictionary<int, Author> GetAllAuthors()
        {
            //we werken hier met de POCO Author omdat deze 1 op 1 mapt met Author in de databank, terwijl een author met andere classes in dit niet doet
            string selectAllSeries = @$"select Id,Name
                                        from Author";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(selectAllSeries, connection);
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    Dictionary<int, Author> authorList = new Dictionary<int, Author>();
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string authorName = (string)reader["Name"];
                        Author author = new Author(id, authorName);
                        authorList.Add(author.Id, author);
                    }
                    reader.Close();
                    return authorList;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }
    }
}
