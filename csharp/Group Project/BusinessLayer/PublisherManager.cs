namespace BusinessLayer
{
    using BusinessLayer.Entities;
    using BusinessLayer.Exceptions;
    using BusinessLayer.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="PublisherManager" />.
    /// </summary>
    public class PublisherManager
    {
        /// <summary>
        /// Defines the _uow.
        /// </summary>
        private IUnitOfWork _uow;

        /// <summary>
        /// Defines the _allPublishers.
        /// </summary>
        private List<Publisher> _allPublishers;
  
        /// <summary>
        /// Gets the AllPublishers.
        /// </summary>
        private List<Publisher> AllPublishers
        {
            get
            {
                if (_allPublishers == null || _allPublishers.Count == 0)
                {

                    _allPublishers = GetAllPublishers();
                }
                return _allPublishers;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherManager"/> class.
        /// </summary>
        /// <param name="uow">The uow<see cref="IUnitOfWork"/>.</param>
        public PublisherManager(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        /// <summary>
        /// The AddPublisher.
        /// </summary>
        /// <param name="publisher">The publisher<see cref="Publisher"/>.</param>
        /// <returns>The <see cref="Publisher"/>.</returns>
        public Publisher AddPublisher(Publisher publisher)
        {
            //Opmaken van list met alle namen van publishers
            //_allPublishers = GetAllPublishers();
            //Allpublishers doet hetzelfde, maar dan 1 keer 


            //Kijken of de publisher nog niet bestaat
            //Indien niet, de publisher toevoegen

            bool publisherNameExist = AllPublishers.Any(x => x.Name.ToUpper() == publisher.Name.ToUpper());
            if (!publisherNameExist)
            {
                publisher = _uow.PublisherRepo.AddPublisher(publisher);
                AllPublishers.Add(publisher);
            }
            else
            {
                throw new PublisherException("Publisher already exist.");
            }
           
            return publisher;
        }

        /// <summary>
        /// The UpdatePublisher.
        /// </summary>
        /// <param name="publisher">The publisher<see cref="Publisher"/>.</param>
        public void UpdatePublisher(Publisher publisher)
        {
            //Publisher die upgedate moet worden controleren of deze nog niet bestaat
            //Geen duplicaten (bv zelfde naam met verschillend ID)
            bool publisherNameExist = AllPublishers.Any(x => x.Name.ToUpper() == publisher.Name.ToUpper());
            if (!publisherNameExist)
            {
                _uow.PublisherRepo.UpdatePublisher(publisher);
            }
            else
            {
                throw new PublisherException("Publisher already exist.");
            }
        }

        /// <summary>
        /// The RemovePublisher.
        /// </summary>
        /// <param name="publisher">The publisher<see cref="Publisher"/>.</param>
        public void RemovePublisher(Publisher publisher)
        {
            List<Comic> allComics = _uow.ComicRepo.AllComics();
            //controleren of de publisher geen comics meer heeft
            bool publisherHasComics = allComics.Any(x => x.Publisher.Id == publisher.Id);
            if (publisherHasComics) throw new PublisherException("Publisher still has comics.");
            _uow.PublisherRepo.RemovePublisher(publisher);
        }

        /// <summary>
        /// The GetAllPublishers.
        /// </summary>
        /// <returns>The <see cref="List{Publisher}"/>.</returns>
        public List<Publisher> GetAllPublishers()
        {
            //Weergeven van alle publishers
            return _uow.PublisherRepo.AllPublishers();
        }

        /// <summary>
        /// The GetPublisherIfExistElseCreate.
        /// </summary>
        /// <param name="publisher">The publisher<see cref="Publisher"/>.</param>
        /// <returns>The <see cref="Publisher"/>.</returns>
        public Publisher GetPublisherIfExistElseCreate(Publisher publisher)
        {
            //We spreken Allpublishers aan dus het vult zijn eigen automatisch op 
            var existingPublisher = AllPublishers.FirstOrDefault(x => x.Name == publisher.Name);
            if (existingPublisher != null)
            {
                return existingPublisher;
            }
            publisher = AddPublisher(new Publisher(publisher.Name));

            return publisher;
        }

        /// <summary>
        /// The DoesPublisherExist.
        /// </summary>
        /// <param name="publisherName">The publisherName<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool DoesPublisherExist(string publisherName)
        {
            bool doesPublisherExist = AllPublishers.Any(x => x.Name.ToLower().Trim() == publisherName.ToLower().Trim());
            return doesPublisherExist;
        }
    }
}
