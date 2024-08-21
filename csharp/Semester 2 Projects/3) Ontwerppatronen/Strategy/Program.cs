using System;
using System.Collections.Generic;
using Strategy.Interfaces;
using Strategy.Models;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            Cart shoppingCart = new Cart();
            
            shoppingCart.AddProduct(new Game(50));
            shoppingCart.AddProduct(new Game(15));
            shoppingCart.AddProduct(new Sticker(5));
            shoppingCart.AddProduct(new Poster(10));

            var paypal = new PaypalStrategy("User", "test123");
            var wallet = new WalletStrategy(1200,"User");
            var paymentList = new List<IPaymentStrategy>();
            // paymentList.Add(wallet);
            // paymentList.Add(paypal);
            //  shoppingCart.MakePayment(paymentList);
            shoppingCart.MakePayment(paypal);
            shoppingCart.ClearCart();

            Console.WriteLine("hitting readkey");
            Console.ReadKey();

        }
    }
}
