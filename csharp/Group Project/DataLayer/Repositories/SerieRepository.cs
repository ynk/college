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
    /// Defines the <see cref="SerieRepository" />.
    /// </summary>
    public class SerieRepository : ISerieRepository
    {
        /// <summary>
        /// Gets the _connectionString.
        /// </summary>
        private string _connectionString { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerieRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connectionString<see cref="string"/>.</param>
        public SerieRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// The AddSeries.
        /// </summary>
        /// <param name="serie">The serie<see cref="Serie"/>.</param>
        /// <returns>The <see cref="Serie"/>.</returns>
        public Serie AddSeries(Serie serie)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string Insertquery = @"INSERT INTO Serie (Name) output INSERTED.ID VALUES (@Name)";
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));
                    command.CommandText = Insertquery;

                    command.Parameters["@Name"].Value = serie.Name;
                    int id = (int)command.ExecuteScalar();
                    return new Serie(id, serie.Name);
                }
                catch (Exception ex)
                {
                    throw new SerieException(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// The UpdateSerie.
        /// </summary>
        /// <param name="serie">The serie<see cref="Serie"/>.</param>
        public void UpdateSerie(Serie serie)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string updateQuery = @$"UPDATE Serie
                                SET Name = @Name 
                                WHERE Id = @Id";
            SqlCommand command = new SqlCommand(updateQuery, connection);
            connection.Open();
            try
            {
                command.Parameters.Add(new SqlParameter("@Name", serie.Name));
                command.Parameters.Add(new SqlParameter("@Id", serie.Id));
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
        /// The RemoveSerie.
        /// </summary>
        /// <param name="serie">The serie<see cref="Serie"/>.</param>
        public void RemoveSerie(Serie serie)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string removeQuery = @$"DELETE FROM Serie 
                                WHERE Id = @Id";
            SqlCommand command = new SqlCommand(removeQuery, connection);
            connection.Open();
            try
            {
                command.Parameters.Add(new SqlParameter("@Id", serie.Id));
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
        /// The RemoveSeries.
        /// </summary>
        /// <param name="serieIds">The serieIds<see cref="List{int}"/>.</param>
        public void RemoveSeries(List<int> serieIds)
        {
            var idString = string.Join(",", serieIds);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string removeQuery = @$"DELETE FROM Serie 
                                     WHERE Id in ({idString})";
                SqlCommand command = new SqlCommand(removeQuery, connection);
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
        /// The SearchBySerieId.
        /// </summary>
        /// <param name="serieId">The serieId<see cref="int"/>.</param>
        /// <returns>The <see cref="Serie"/>.</returns>
        public Serie SearchBySerieId(int serieId)
        {

            string searchQuery = @$"SELECT Id,Name 
                                FROM Serie
                                WHERE id = @Id";
            Serie series = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(searchQuery, connection);
                command.Parameters.Add(new SqlParameter("@Id", serieId));
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (series == null)
                        {
                            int Id = int.Parse(reader["Id"].ToString());
                            string serieName = reader["Name"].ToString();
                            Serie serie = new Serie(serieId, serieName);
                        }
                        else
                        {
                            throw new Exception("Meerdere resultaten");
                        }
                    }
                    return series;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }
        }

        /// <summary>
        /// The AllSeries.
        /// </summary>
        /// <returns>The <see cref="List{Serie}"/>.</returns>
        public List<Serie> AllSeries()
        {

            string selectAllSeries = @$"SELECT Id,Name
                                     FROM Serie";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(selectAllSeries, connection);
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<Serie> seriesList = new List<Serie>();
                    while (reader.Read())
                    {
                        int serieId = (int)reader["Id"];
                        string serieName = (string)reader["Name"];
                        Serie serie = new Serie(serieId, serieName);
                        seriesList.Add(serie);
                    }
                    reader.Close();
                    return seriesList;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// The GetAllSeries.
        /// </summary>
        /// <returns>The <see cref="Dictionary{int, Serie}"/>.</returns>
        public Dictionary<int, Serie> GetAllSeries()
        {
            string selectAllSeries = @$"SELECT Id,Name
                                     FROM Serie";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(selectAllSeries, connection);
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    Dictionary<int, Serie> seriesList = new Dictionary<int, Serie>();
                    while (reader.Read())
                    {
                        int serieId = (int)reader["Id"];
                        string serieName = (string)reader["Name"];
                        Serie serie = new Serie(serieId, serieName);
                        seriesList.Add(serie.Id, serie);
                    }
                    reader.Close();
                    return seriesList;
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
