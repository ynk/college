namespace Strategy.Interfaces
{
    public interface IPaymentStrategy
    {
        bool Pay(int totalPrice);
    }
}