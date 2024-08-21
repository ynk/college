namespace BusinessLayer
{
    using BusinessLayer.Entities;
    using BusinessLayer.Interfaces;

    /// <summary>
    /// Defines the <see cref="StockManager" />.
    /// </summary>
    public class StockManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockManager"/> class.
        /// </summary>
        /// <param name="uow">The uow<see cref="IUnitOfWork"/>.</param>
        public StockManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Defines the _uow.
        /// </summary>
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// The AddDelivery.
        /// </summary>
        /// <param name="delivery">The delivery<see cref="Delivery"/>.</param>
        public void AddDelivery(Delivery delivery)
        {
            if (delivery.DeliveryLines != null)
            {
                foreach (var deliveryLine in delivery.DeliveryLines)
                {
                    deliveryLine.Comic.AddAantal(deliveryLine.Aantal);
                    _uow.ComicRepo.UpdateAantal(deliveryLine.Comic);
                }
                _uow.DeliveryRepository.AddDelivery(delivery);
            }
        }

        /// <summary>
        /// The AddOrder.
        /// </summary>
        /// <param name="order">The order<see cref="Order"/>.</param>
        public void AddOrder(Order order)
        {
            if (order.OrderLines != null)
            {
                foreach (var orderLine in order.OrderLines)
                {
                    orderLine.Comic.RemoveAantal(orderLine.Aantal);
                    _uow.ComicRepo.UpdateAantal(orderLine.Comic);
                }
                _uow.OrderRepository.AddOrder(order);
            }
        }
    }
}
