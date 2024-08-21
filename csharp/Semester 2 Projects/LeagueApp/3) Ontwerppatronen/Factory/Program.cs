using System;
using Factory.Interfaces;
using Factory.Models;

namespace Factory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IRecipientFactory recipientFactory = new RecipientFactory();
            Recipient emailRecipient = recipientFactory.CreateRecipient(RecipientType.EMAIL, "jef.pieters@gmail.com");
            Recipient ccRecipient = recipientFactory.CreateRecipient(RecipientType.CC, "jan.janssens@hotmail.com");
            Recipient bccRecipient = recipientFactory.CreateRecipient(RecipientType.BCC, "tom.vervoort@live.nl");
            Console.WriteLine($"created a recipient of type: {emailRecipient.GetType()} with address: {emailRecipient.Address}");
            Console.WriteLine($"created a recipient of type: {ccRecipient.GetType()} with address: {ccRecipient.Address}");
            Console.WriteLine($"created a recipient of type: {bccRecipient.GetType()} with address: {bccRecipient.Address}");

            Console.WriteLine("hitting readkey");
            Console.ReadKey();
        }
    }
}
