namespace DataLayer
{
    using BusinessLayer.Interfaces;
    using DataLayer.Repositories;

    /// <summary>
    /// Defines the <see cref="UnitOfWork" />.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Defines the _connectionString.
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="connectionString">The connectionString<see cref="string"/>.</param>
        public UnitOfWork(string connectionString)
        {
            this._connectionString = connectionString;
            AuthorRepo = new AuthorRepository(connectionString);
            ComicAuthorRepo = new ComicAuthorRepository(connectionString);
            ComicRepo = new ComicRepository(connectionString);
            PublisherRepo = new PublisherRepository(connectionString);
            SerieRepo = new SerieRepository(connectionString);
            ImportExportRepo = new ImportExportRepository(connectionString);
            OrderRepository = new OrderRepository(connectionString);
            DeliveryRepository = new DeliveryRepository(connectionString);
        }

        /// <summary>
        /// Gets the AuthorRepo.
        /// </summary>
        public IAuthorRepository AuthorRepo { get; private set; }

        /// <summary>
        /// Gets the ComicAuthorRepo.
        /// </summary>
        public IComicAuthorRepository ComicAuthorRepo { get; private set; }

        /// <summary>
        /// Gets the ComicRepo.
        /// </summary>
        public IComicRepository ComicRepo { get; private set; }

        /// <summary>
        /// Gets the PublisherRepo.
        /// </summary>
        public IPublisherRepository PublisherRepo { get; private set; }

        /// <summary>
        /// Gets the SerieRepo.
        /// </summary>
        public ISerieRepository SerieRepo { get; private set; }

        /// <summary>
        /// Gets the ImportExportRepo.
        /// </summary>
        public IImportExportRepository ImportExportRepo { get; private set; }

        /// <summary>
        /// Gets the OrderRepository.
        /// </summary>
        public IOrderRepository OrderRepository { get; private set; }

        /// <summary>
        /// Gets the DeliveryRepository.
        /// </summary>
        public IDeliveryRepository DeliveryRepository { get; private set; }
    }
}
