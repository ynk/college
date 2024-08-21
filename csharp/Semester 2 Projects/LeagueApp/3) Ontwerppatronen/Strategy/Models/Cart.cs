using System.Collections.Generic;
using System.Linq;
using Strategy.Interfaces;

namespace Strategy.Models
{
    public class Cart
    {
        private List<Product> itemsList;

        public Cart()
        {
            itemsList = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            itemsList.Add(product);
        }

        public void ClearCart()
        {
            itemsList.Clear();
        }

        public void MakePayment(IPaymentStrategy paymentStrategy)
        {
            var totalPrice = GetTotalPrice();
            paymentStrategy.Pay(totalPrice);
        }

        public void MakePayment(List<IPaymentStrategy> paymentStrategies)
        {
            var totalPrice = GetTotalPrice();
            foreach (var paymentStrategy in paymentStrategies)
            {
                if (paymentStrategy.Pay(totalPrice))
                {
                    break;
                }
            }
        }
        public int GetTotalPrice()
        {
            var total = itemsList.Sum(x => x.GetPrice()); // uit de pdf 
            return total;
        }
    }
}