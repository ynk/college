using System;

namespace BusinessLayer.Exceptions
{
    public class DeliveryException : Exception
    {
        public DeliveryException(string message) : base(message)
        {
        }
    }
}