namespace DataLayer.Repositories
{
    using BusinessLayer.Entities;
    using BusinessLayer.Exceptions;
    using BusinessLayer.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="ComicRepository" />.
    /// </summary>
    public class ComicRepository : IComicRepository
    {
        /// <summary>
        /// Defines the _connectionString.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComicRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connectionString<see cref="string"/>.</param>
        public ComicRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// The AddComic.
        /// </summary>
        /// <param name="comic">The comic<see cref="Comic"/>.</param>
        /// <returns>The <see cref="Comic"/>.</returns>
        public Comic AddComic(Comic comic)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string queryInsertComic = @"INSERT INTO comic (Title,PublisherId,SerieId,SerieSeqNumber) output INSERTED.ID
                                      VALUES(@Title,@PublisherId,@SerieId,@SerieSeqNumber)";

            string queryInsertComicAuthor = @"INSERT INTO ComicAuthor (ComicId,AuthorId)
                                            VALUES (@ComicId,@AuthorId)";

            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    //Add Comic
                    command.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@PublisherId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@SerieId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@SerieSeqNumber", SqlDbType.Int));
                    command.CommandText = queryInsertComic;

                    command.Parameters["@Title"].Value = comic.Title;
                    command.Parameters["@PublisherId"].Value = comic.Publisher.Id;
                    if (comic.Serie != null)
                    {
                        command.Parameters["@SerieId"].Value = comic.Serie.Id;
                    }
                    else
                    {
                        command.Parameters["@SerieId"].Value = DBNull.Value;
                    }
                    if (comic.SerieSeqNumber != null)
                    {
                        command.Parameters["@SerieSeqNumber"].Value = comic.SerieSeqNumber;
                    }
                    else
                    {
                        command.Parameters["@SerieSeqNumber"].Value = DBNull.Value;
                    }


                    //Returned de id van de laatst toegevoegde comic
                    int id = (int)command.ExecuteScalar();

                    //Insert in de ComicAuthor tabel
                    command.Parameters.Add(new SqlParameter("@ComicId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@AuthorId", SqlDbType.Int));
                    foreach (Author author in comic.GetAuthors())
                    {
                        command.Parameters["@ComicId"].Value = id;
                        command.Parameters["@AuthorId"].Value = author.Id;
                        command.CommandText = queryInsertComicAuthor;
                        command.ExecuteNonQuery();

                    }
                    return new Comic(id, comic.Title, comic.Publisher, comic.Serie, comic.GetAuthors().ToList(), comic.SerieSeqNumber, comic.Aantal);
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
        /// The UpdateComic.
        /// </summary>
        /// <param name="comic">The comic<see cref="Comic"/>.</param>
        public void UpdateComic(Comic comic)
        {
            string updateComic = @$"Update comic
                                  Set Title = @Title, PublisherId = @PublisherId, SerieId = @SerieId , SerieSeqNumber = @SeqNumber
                                  where Id = @Id";

            string insertComicAuthor = @"INSERT INTO comicAuthor(ComicId,AuthorId)
                                        VALUES (@Id, @AuthorId)";

            string deleteComicAuthor = @"DELETE FROM ComicAuthor
                                        where ComicId = @Id";

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(updateComic, connection);
            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
            command.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar));
            command.Parameters.Add(new SqlParameter("@PublisherId", SqlDbType.Int));
            command.Parameters.Add(new SqlParameter("@SerieId", SqlDbType.Int));
            command.Parameters.Add(new SqlParameter("@SeqNumber", SqlDbType.Int));

            command.Parameters["@Id"].Value = comic.Id;
            command.Parameters["@Title"].Value = comic.Title;
            command.Parameters["@PublisherId"].Value = comic.Publisher.Id;
            if (comic.Serie != null)
            {
                command.Parameters["@SerieId"].Value = comic.Serie.Id;
            }
            else
            {
                command.Parameters["@SerieId"].Value = DBNull.Value;
            }
            if (comic.SerieSeqNumber != null)
            {
                command.Parameters["@SeqNumber"].Value = comic.SerieSeqNumber;
            }
            else
            {
                command.Parameters["@SeqNumber"].Value = DBNull.Value;
            }

            connection.Open();
            try
            {
                command.ExecuteNonQuery();

                command.CommandText = deleteComicAuthor;
                command.ExecuteNonQuery();

                command.CommandText = insertComicAuthor;
                command.Parameters.Add(new SqlParameter("AuthorId", SqlDbType.Int));
                foreach (Author author in comic.GetAuthors())
                {
                    command.Parameters["AuthorId"].Value = author.Id;
                    command.ExecuteNonQuery();
                }
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

        /// <summary>
        /// The RemoveComic.
        /// </summary>
        /// <param name="comic">The comic<see cref="Comic"/>.</param>
        public void RemoveComic(Comic comic)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    string queryRemoveComicAuthor = @$"DELETE FROM comicAuthor 
                                                    WHERE ComicId = @ComicId and AuthorId = @AuthorId";

                    command.Parameters.Add(new SqlParameter("@ComicId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@AuthorId", SqlDbType.Int));
                    command.CommandText = queryRemoveComicAuthor;

                    List<Author> authors = comic.GetAuthors().ToList();

                    foreach (Author author in authors)
                    {
                        command.Parameters["@ComicId"].Value = comic.Id;
                        command.Parameters["@AuthorId"].Value = author.Id;
                        command.ExecuteNonQuery();
                    }

                    string query = @$"DELETE FROM Comic 
                                   WHERE Id = @Id";

                    command.Parameters.Add(new SqlParameter("@Id", comic.Id));
                    command.Parameters["@Id"].Value = comic.Id;
                    command.CommandText = query;
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
        }

        /// <summary>
        /// The RemoveComics.
        /// </summary>
        /// <param name="comicIds">The comicIds<see cref="List{int}"/>.</param>
        public void RemoveComics(List<int> comicIds)
        {
            var idString = string.Join(",", comicIds);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @$"DELETE FROM Comic 
                               WHERE Id in ({idString})";

                string removeComicAuthorQuery = @$"DELETE FROM ComicAuthor
                                                  WHERE ComicId in ({idString})";
                SqlCommand command = new SqlCommand(query, connection);
                SqlCommand commandComicAuthor = new SqlCommand(removeComicAuthorQuery, connection);
                //command.Parameters.Add(new SqlParameter("@Ids", comicIds));
                connection.Open();
                try
                {
                    commandComicAuthor.ExecuteNonQuery();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
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
            var parameters = new List<SqlParameter>();
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = @"select c.Id ComicId, c.Title Title, a.Name AuthorName, p.Name PublisherName, s.Name SerieName, c.SerieSeqNumber, c.Aantal Stock
                            from Comic c 
                            inner join ComicAuthor ca on c.Id= ca.ComicId
                            inner join Author a on ca.AuthorId = a.Id
                            inner join Publisher p on c.PublisherId = p.Id
                            left join Serie s on c.SerieId = s.Id
                            ";


            if (!string.IsNullOrEmpty(title))
            {
                query += "where c.title like '%'+@title+'%'";
                SqlParameter para = new SqlParameter();
                para.ParameterName = "@title";
                para.DbType = DbType.String;
                para.Value = title;
                parameters.Add(para);
            }
            if (!string.IsNullOrEmpty(authorName))
            {
                string sql = "where";
                if (!string.IsNullOrEmpty(title))
                {
                    sql = "and";
                }


                query += $"{sql} a.Name like '%'+@authorName+'%'";
                SqlParameter para = new SqlParameter();
                para.ParameterName = "@authorName";
                para.DbType = DbType.String;
                para.Value = authorName;
                parameters.Add(para);
            }
            if (!string.IsNullOrEmpty(serieName))
            {
                string sql = "where";
                if (!string.IsNullOrEmpty(authorName) || !string.IsNullOrEmpty(title))
                {
                    sql = "and";
                }

                query += $" {sql} s.Name like '%'+@serieName+'%'";
                SqlParameter para = new SqlParameter();
                para.ParameterName = "@serieName";
                para.DbType = DbType.String;
                para.Value = serieName;
                parameters.Add(para);
            }
            if (!string.IsNullOrEmpty(publisherName))
            {
                string sql = "where";
                if (!string.IsNullOrEmpty(serieName) || !string.IsNullOrEmpty(authorName) || !string.IsNullOrEmpty(title))
                {
                    sql = "and";
                }

                query += $"{sql} p.Name like '%'+@publisherName+'%'";
                SqlParameter para = new SqlParameter();
                para.ParameterName = "@publisherName";
                para.DbType = DbType.String;
                para.Value = publisherName;
                parameters.Add(para);
            }

            if (serieSeqNumber != null)
            {
                query += " and c.SerieSeqNumber = @serieSeqNumber";
                SqlParameter para = new SqlParameter();
                para.ParameterName = "@serieSeqNumber";
                para.DbType = DbType.Int32;
                para.Value = serieSeqNumber;
                parameters.Add(para);
            }

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.AddRange(parameters.ToArray());
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    var results = new List<ComicResult>();
                    while (reader.Read())
                    {
                        int comicId = (int)reader["ComicId"];
                        string titleComic = (string)reader["Title"];
                        string aN = (string)reader["AuthorName"];
                        string pN = (string)reader["PublisherName"];
                        string sN = string.Empty;
                        if (reader["SerieName"] != DBNull.Value)
                        {
                            sN = (string)reader["SerieName"];
                        }
                        int? serieSeqNum = null;
                        if (reader["SerieSeqNumber"] != DBNull.Value)
                        {
                            serieSeqNum = (int)reader["SerieSeqNumber"];
                        }
                        List<string> authors = new List<string>();
                        authors.Add(aN);
                        int stock = (int)reader["Stock"];
                        ComicResult comicResult = new ComicResult() { ComicId = comicId, Title = titleComic, AuthorName = authors, PublisherName = pN, SerieName = sN, SerieSeqNumber = serieSeqNum, Stock = stock };
                        bool comicExistInAllComics = results.Any(x => x.ComicId == comicResult.ComicId);
                        if (comicExistInAllComics)
                        {
                            ComicResult comicAlreadyExist = results.Where(x => x.ComicId == comicResult.ComicId).First();
                            comicAlreadyExist.AuthorName.Add(aN);
                        }
                        else
                        {
                            results.Add(comicResult);
                        }
                    }
                    reader.Close();
                    return results;
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
        /// The AllComics.
        /// </summary>
        /// <returns>The <see cref="List{Comic}"/>.</returns>
        public List<Comic> AllComics()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            //Als er geen serie, publisher of auteur is in de comic slaan we deze over.
            string query = @"Select c.Id, c.Title, c.PublisherId, c.Aantal ,p.Name as publisherName, c.SerieId,s.Name as serieName, c.SerieSeqNumber, a.Id as AuthorId, a.Name as AuthorName
                            from Comic c join ComicAuthor ca
                            on c.Id = ca.ComicId
                            join Author a
                            on ca.AuthorId = a.Id
                            join Publisher p
                            on p.Id = c.PublisherId
                            left join serie s
                            on s.Id = c.SerieId
                            order by c.Id";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                List<Comic> allComics = new List<Comic>();
                Comic currentComic = null;
                while (reader.Read())
                {
                    //Check voor db null want seq nummer KAN NULL ZIJN
                    int id = (int)reader["Id"];

                    if (currentComic == null || currentComic.Id != id)
                    {
                        string title = (string)reader["Title"];
                        int aantal = (int)reader["Aantal"];
                        int publisherId = (int)reader["PublisherId"];
                        string publisherName = (string)reader["publisherName"];
                        Publisher publisher = new Publisher(publisherId, publisherName);

                        Serie serie = null;
                        int? serieId = reader["SerieId"] == DBNull.Value ? default(int?) : (int?)reader["SerieId"];
                        if (serieId != null)
                        {
                            string serieName = (string)reader["serieName"];
                            serie = new Serie((int)serieId, serieName);
                        }

                        int? serieSeqNumber = reader["SerieSeqNumber"] == System.DBNull.Value ? default(int?) : (int?)reader["SerieSeqNumber"];
                        int authorId = (int)reader["AuthorId"];
                        string authorName = (string)reader["AuthorName"];
                        Author author = new Author(authorId, authorName);
                        List<Author> authors = new List<Author>() { author };
                        currentComic = new Comic(id, title, publisher, serie, authors, serieSeqNumber, aantal);
                        allComics.Add(currentComic);
                    }
                    else
                    {
                        int authorId = (int)reader["AuthorId"];
                        string authorName = (string)reader["AuthorName"];
                        Author author = new Author(authorId, authorName);
                        currentComic.AddAuthor(author);
                    }


                    //Gebruik gemaakt van groeps onderbreking, we hebben lijst met comics met herhalende comics en verschillende auteurs(groep),
                    //zo zoeken we de serie en publisher niet altijd op maar voegen we gewoon een nieuwe auteur toe, zonder meerdere look ups
                    //bool comicExistInAllComics = allComics.Any(x => x.Id == comic.Id);
                    //if (comicExistInAllComics)
                    //{
                    //    Comic comicAlreadyExist = allComics.Where(x => x.Id == comic.Id).First();
                    //    comicAlreadyExist.AddAuthor(author);
                    //}
                    //else
                    //{
                    //    allComics.Add(comic);
                    //}
                }
                reader.Close();
                return allComics;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// The UpdateAantal.
        /// </summary>
        /// <param name="comic">The comic<see cref="Comic"/>.</param>
        public void UpdateAantal(Comic comic)
        {
            string updateComic = @$"Update comic
                                  Set Aantal = @Aantal
                                  where Id = @Id";

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(updateComic, connection);
            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
            command.Parameters.Add(new SqlParameter("@Aantal", SqlDbType.Int));

            command.Parameters["@Id"].Value = comic.Id;
            command.Parameters["@Aantal"].Value = comic.Aantal;
            connection.Open();
            try
            {
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

        /// <summary>
        /// The GetComic.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Comic"/>.</returns>
        public Comic GetComic(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            //Als er geen serie, publisher of auteur is in de comic slaan we deze over.
            string query = @"Select c.Id, c.Title, c.PublisherId, c.Aantal ,p.Name as publisherName, c.SerieId,s.Name as serieName, c.SerieSeqNumber, a.Id as AuthorId, a.Name as AuthorName
                            from Comic c join ComicAuthor ca
                            on c.Id = ca.ComicId
                            join Author a
                            on ca.AuthorId = a.Id
                            join Publisher p
                            on p.Id = c.PublisherId
                            left join serie s
                            on s.Id = c.SerieId
                            where c.Id = @id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
            command.Parameters["@Id"].Value = id;
            connection.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                List<Comic> allComics = new List<Comic>();
                Comic currentComic = null;
                while (reader.Read())
                {
                    //Check voor db null want seq nummer KAN NULL ZIJN
                    if (currentComic == null || currentComic.Id != id)
                    {
                        string title = (string)reader["Title"];
                        int aantal = (int)reader["Aantal"];
                        int publisherId = (int)reader["PublisherId"];
                        string publisherName = (string)reader["publisherName"];
                        Publisher publisher = new Publisher(publisherId, publisherName);

                        Serie serie = null;
                        int? serieId = reader["SerieId"] == DBNull.Value ? default(int?) : (int?)reader["SerieId"];
                        if (serieId != null)
                        {
                            int serieIdsss = (int)reader["SerieId"];
                            string serieName = (string)reader["serieName"];
                            serie = new Serie((int)serieId, serieName);
                        }

                        int? serieSeqNumber = reader["SerieSeqNumber"] == System.DBNull.Value ? default(int?) : (int?)reader["SerieSeqNumber"];
                        int authorId = (int)reader["AuthorId"];
                        string authorName = (string)reader["AuthorName"];
                        Author author = new Author(authorId, authorName);
                        List<Author> authors = new List<Author>() { author };
                        currentComic = new Comic(id, title, publisher, serie, authors, serieSeqNumber, aantal);
                        allComics.Add(currentComic);
                    }
                    else
                    {
                        int authorId = (int)reader["AuthorId"];
                        string authorName = (string)reader["AuthorName"];
                        Author author = new Author(authorId, authorName);
                        currentComic.AddAuthor(author);
                    }

                    //Gebruik gemaakt van groeps onderbreking, we hebben lijst met comics met herhalende comics en verschillende auteurs(groep),
                    //zo zoeken we de serie en publisher niet altijd op maar voegen we gewoon een nieuwe auteur toe, zonder meerdere look ups
                    //bool comicExistInAllComics = allComics.Any(x => x.Id == comic.Id);
                    //if (comicExistInAllComics)
                    //{
                    //    Comic comicAlreadyExist = allComics.Where(x => x.Id == comic.Id).First();
                    //    comicAlreadyExist.AddAuthor(author);
                    //}
                    //else
                    //{
                    //    allComics.Add(comic);
                    //}
                }
                reader.Close();
                if (allComics.Count != 1)
                {
                    throw new ComicException("Geen correcte strip gevonden");
                }
                return allComics.First();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
