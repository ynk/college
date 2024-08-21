namespace DataLayer.Repositories
{
    using BusinessLayer.Entities;
    using BusinessLayer.Exceptions;
    using BusinessLayer.Interfaces;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Defines the <see cref="OrderRepository" />.
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        /// <summary>
        /// Gets the _connectionString.
        /// </summary>
        private string _connectionString { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connectionString<see cref="string"/>.</param>
        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// The AddOrder.
        /// </summary>
        /// <param name="order">The order<see cref="Order"/>.</param>
        /// <returns>The <see cref="Order"/>.</returns>
        public Order AddOrder(Order order)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            // [Order] = Sql server vindt het woord order niet zo leuk doorverwaring
            // Daarom de brackets
            string query = @"INSERT INTO [Order] (OrderDate) output INSERTED.ID
                                VALUES (@OrderDate)";
   
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@OrderDate", SqlDbType.DateTime));
                    command.CommandText = query;

                    command.Parameters["@OrderDate"].Value = order.OrderDate;

                    int id = (int)command.ExecuteScalar();
                    order.Id = id;
                    AddOrderComics(order, connection);
                }
                catch (Exception ex)
                {
                    throw new OrderException(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                return order;
            }
        }

        /// <summary>
        /// The AddOrderComics.
        /// </summary>
        /// <param name="order">The order<see cref="Order"/>.</param>
        /// <param name="conn">The conn<see cref="SqlConnection"/>.</param>
        private void AddOrderComics(Order order, SqlConnection conn)
        {
            string query = @"INSERT INTO OrderComics (OrderId, ComicId, Aantal ) output INSERTED.ID
                                VALUES (@OrderId, @ComicId, @Aantal)";
            //INSERT INTO Author(Name) output INSERTED.ID VALUES(@Name)
            using (SqlCommand command = conn.CreateCommand())
            {
                try
                {
                    foreach (var orderLine in order.OrderLines)
                    {
                        command.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@ComicId", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@Aantal", SqlDbType.Int));
                        command.CommandText = query;

                        command.Parameters["@OrderId"].Value = order.Id;
                        command.Parameters["@ComicId"].Value = orderLine.Comic.Id;
                        command.Parameters["@Aantal"].Value = orderLine.Aantal;
                        int id = (int)command.ExecuteScalar();
                        orderLine.Id = id;
                        orderLine.Order = order;
                    }

                }
                catch (Exception ex)
                {
                    throw new OrderException(ex.Message);
                }
            }
        }
    }
}
