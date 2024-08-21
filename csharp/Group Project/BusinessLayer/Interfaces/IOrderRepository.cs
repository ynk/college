using BusinessLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IOrderRepository
    {
        Order AddOrder(Order order);
    }
}