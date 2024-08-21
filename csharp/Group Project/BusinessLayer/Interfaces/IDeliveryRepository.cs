using BusinessLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IDeliveryRepository
    {
        Delivery AddDelivery(Delivery delivery);
    }
}