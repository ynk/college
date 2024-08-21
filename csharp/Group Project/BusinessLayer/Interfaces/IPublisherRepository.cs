using System.Collections.Generic;
using BusinessLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IPublisherRepository
    {
        Publisher AddPublisher(Publisher publisher);
        void UpdatePublisher(Publisher publisher);
        void RemovePublisher(Publisher publisher);
        List<Publisher> SearchByPublisher(string publisherName, int? Id);
        List<Publisher> AllPublishers();
        void RemovePublishers(List<int> publisherIds);
    }
}
