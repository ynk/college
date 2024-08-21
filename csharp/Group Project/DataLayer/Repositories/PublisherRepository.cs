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
    /// Defines the <see cref="PublisherRepository" />.
    /// </summary>
    public class PublisherRepository : IPublisherRepository
    {
        /// <summary>
        /// Gets the _connectionString.
        /// </summary>
        private string _connectionString { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connectionString<see cref="string"/>.</param>
        public PublisherRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// The AddPublisher.
        /// </summary>
        /// <param name="publisher">The publisher<see cref="Publisher"/>.</param>
        /// <returns>The <see cref="Publisher"/>.</returns>
        public Publisher AddPublisher(Publisher publisher)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = @"INSERT INTO Publisher (Name) output INSERTED.ID VALUES(@Name) ";
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));
                    command.CommandText = query;

                    command.Parameters["@Name"].Value = publisher.Name;
                    int id = (int)command.ExecuteScalar();
                    return new Publisher(id, publisher.Name);
                }
                catch (Exception ex)
                {
                    throw new PublisherException(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// The UpdatePublisher.
        /// </summary>
        /// <param name="publisher">The publisher<see cref="Publisher"/>.</param>
        public void UpdatePublisher(Publisher publisher)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = @$"UPDATE Publisher 
                            SET Name = @Name 
                            WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@Name", publisher.Name));
            command.Parameters.Add(new SqlParameter("@Id", publisher.Id));

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
        /// The RemovePublisher.
        /// </summary>
        /// <param name="publisher">The publisher<see cref="Publisher"/>.</param>
        public void RemovePublisher(Publisher publisher)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = @$"DELETE FROM Publisher 
                            WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@Id", publisher.Id));
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
        /// The RemovePublishers.
        /// </summary>
        /// <param name="publisherIds">The publisherIds<see cref="List{int}"/>.</param>
        public void RemovePublishers(List<int> publisherIds)
        {
            var idString = string.Join(",", publisherIds);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @$"DELETE FROM Publisher 
                                WHERE Id in ({idString})";
                SqlCommand command = new SqlCommand(query, connection);
                //command.Parameters.Add(new SqlParameter("@Id", publisher.Id));
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
        /// The SearchByPublisher.
        /// </summary>
        /// <param name="publisherName">The publisherName<see cref="string"/>.</param>
        /// <param name="Id">The Id<see cref="int?"/>.</param>
        /// <returns>The <see cref="List{Publisher}"/>.</returns>
        public List<Publisher> SearchByPublisher(string publisherName, int? Id)
        {
            var parameters = new List<SqlParameter>();
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = @$"SELECT Id, p.Name 
                            FROM Publisher p";

            if (!string.IsNullOrEmpty(publisherName))
            {
                query += $"where p.Name like @Name";
                SqlParameter para = new SqlParameter();
                para.ParameterName = "@Name";
                para.DbType = DbType.String;
                para.Value = publisherName;
                parameters.Add(para);
            }
            if (Id != null)
            {
                query += "where p.Id = @Id";
                SqlParameter para = new SqlParameter();
                para.ParameterName = "@Id";
                para.Value = Id;
                parameters.Add(para);
            }
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                List<Publisher> foundPublishersList = new List<Publisher>();

                while (reader.Read())
                {
                    int id = int.Parse(reader["Id"].ToString());
                    string name = reader["Name"].ToString();
                    Publisher publisher = new Publisher(id, name);
                    foundPublishersList.Add(publisher);
                }
                return foundPublishersList;
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
        /// The AllPublishers.
        /// </summary>
        /// <returns>The <see cref="List{Publisher}"/>.</returns>
        public List<Publisher> AllPublishers()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = @"SELECT Id, Name
                            FROM publisher";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                List<Publisher> publishers = new List<Publisher>();

                while (reader.Read())
                {
                    int id = int.Parse(reader["Id"].ToString());
                    string publisherName = reader["Name"].ToString();
                    Publisher publisher = new Publisher(id, publisherName);
                    publishers.Add(publisher);
                }
                return publishers;
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
        /// The GetAllPublishers.
        /// </summary>
        /// <returns>The <see cref="Dictionary{int, Publisher}"/>.</returns>
        public Dictionary<int, Publisher> GetAllPublishers()
        {
            string selectAllSeries = @$"SELECT Id, Name
                                     FROM Publisher";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(selectAllSeries, connection);
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    var publisherList = new Dictionary<int, Publisher>();
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string publisherName = (string)reader["Name"];
                        Publisher publisher = new Publisher(id, publisherName);
                        publisherList.Add(publisher.Id, publisher);
                    }
                    reader.Close();
                    return publisherList;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
