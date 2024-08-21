namespace DataLayer.Repositories
{
    using BusinessLayer.Interfaces;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Defines the <see cref="ImportExportRepository" />.
    /// </summary>
    public class ImportExportRepository : IImportExportRepository
    {
        /// <summary>
        /// Gets the _connectionString.
        /// </summary>
        private string _connectionString { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExportRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connectionString<see cref="string"/>.</param>
        public ImportExportRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// The InsertDataIntoSQLServerUsingSQLBulkCopy.
        /// </summary>
        /// <param name="stripDatatable">The stripDatatable<see cref="DataTable"/>.</param>
        public void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable stripDatatable)
        {
            using (SqlBulkCopy s = new SqlBulkCopy(_connectionString, SqlBulkCopyOptions.KeepIdentity))
            {
                s.BulkCopyTimeout = 0;
                s.DestinationTableName = "dbo." + stripDatatable.TableName;
                foreach (var column in stripDatatable.Columns)
                    s.ColumnMappings.Add(column.ToString(), column.ToString());
                s.WriteToServer(stripDatatable);
            }
        }
    }
}
