using System;
using Strategy.Interfaces;
using Strategy.Models;

namespace Strategy
{
    public class WalletStrategy : IPaymentStrategy
    {
        public int budget;
        public string username { get; set; }

        public WalletStrategy(int _budget, string userName)
        {
            budget = _budget;
            username = userName;
        }


        public bool Pay(int totalPrice)
        { 
            if(budget > totalPrice)
            {
                Console.WriteLine("Paid using In Game Wallet.");
                return true;
            }
            return false;
        }
    }
}


    
