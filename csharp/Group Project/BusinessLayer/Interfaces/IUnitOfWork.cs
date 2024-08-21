
namespace BusinessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        public IAuthorRepository AuthorRepo { get; }
        public IComicAuthorRepository ComicAuthorRepo { get; }
        public IComicRepository ComicRepo { get; }
        public IPublisherRepository PublisherRepo { get; }
        public ISerieRepository SerieRepo { get; }
        public IImportExportRepository ImportExportRepo { get; }
        public IOrderRepository OrderRepository { get; }
        public IDeliveryRepository DeliveryRepository { get; }
    }
}
