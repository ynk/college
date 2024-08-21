namespace DataLayer.Repositories
{
    using BusinessLayer.Entities;
    using BusinessLayer.Exceptions;
    using BusinessLayer.Interfaces;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Defines the <see cref="DeliveryRepository" />.
    /// </summary>
    public class DeliveryRepository : IDeliveryRepository
    {
        /// <summary>
        /// Gets the _connectionString.
        /// </summary>
        private string _connectionString { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connectionString<see cref="string"/>.</param>
        public DeliveryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// The AddDelivery.
        /// </summary>
        /// <param name="delivery">The delivery<see cref="Delivery"/>.</param>
        /// <returns>The <see cref="Delivery"/>.</returns>
        public Delivery AddDelivery(Delivery delivery)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = @"INSERT INTO Delivery (DatumOntvangst, DatumLevering) output INSERTED.ID
                                VALUES (@DatumOntvangst, @DatumLevering)";
            //INSERT INTO Author(Name) output INSERTED.ID VALUES(@Name)
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@DatumOntvangst", SqlDbType.DateTime));
                    command.Parameters.Add(new SqlParameter("@DatumLevering", SqlDbType.DateTime));
                    command.CommandText = query;

                    command.Parameters["@DatumOntvangst"].Value = delivery.DatumOntvangst;
                    command.Parameters["@DatumLevering"].Value = delivery.DatumLevering;
                    int id = (int)command.ExecuteScalar();
                    delivery.Id = id;
                    AddDeliveryComics(delivery, connection);

                }
                catch (Exception ex)
                {
                    throw new DeliveryException(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                return delivery;
            }
        }

        /// <summary>
        /// The AddDeliveryComics.
        /// </summary>
        /// <param name="delivery">The delivery<see cref="Delivery"/>.</param>
        /// <param name="conn">The conn<see cref="SqlConnection"/>.</param>
        private void AddDeliveryComics(Delivery delivery, SqlConnection conn)
        {
            string query = @"INSERT INTO DeliveryComic (DeliveryId, ComicId, Aantal ) output INSERTED.ID
                                VALUES (@DeliveryId, @ComicId, @Aantal)";
            //INSERT INTO Author(Name) output INSERTED.ID VALUES(@Name)
            using (SqlCommand command = conn.CreateCommand())
            {
                try
                {
                    command.Parameters.Add(new SqlParameter("@DeliveryId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@ComicId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Aantal", SqlDbType.Int));
                    command.CommandText = query;

                    foreach (var deliveryLine in delivery.DeliveryLines)
                    {
                        command.Parameters["@DeliveryId"].Value = delivery.Id;
                        command.Parameters["@ComicId"].Value = deliveryLine.Comic.Id;
                        command.Parameters["@Aantal"].Value = deliveryLine.Aantal;
                        int id = (int)command.ExecuteScalar();
                        deliveryLine.Id = id;
                    }
                }
                catch (Exception ex)
                {
                    throw new DeliveryException(ex.Message);
                }
            }
        }
    }
}
