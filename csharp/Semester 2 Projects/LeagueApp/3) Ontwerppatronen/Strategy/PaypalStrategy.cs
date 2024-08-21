using System;
using Strategy.Interfaces;

namespace Strategy
{
    public class PaypalStrategy : IPaymentStrategy
    {
        private string username;
        private string password;

        public PaypalStrategy(string userName, string _password)
        {
            username = userName;
            password = _password;
        }
        public bool Pay(int totalPrice)
        {
            if (Login())
            {
                Console.WriteLine("Payed Using PayPall");
                return true;
            }
            Console.WriteLine("not logged in"); 
            return false;

            
        }
        public bool Login()
        {
            return 
                username == "User" 
                   && 
                   password == "test123";
        }
      
    }
}