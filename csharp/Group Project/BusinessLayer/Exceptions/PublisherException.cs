using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Exceptions
{
    public class PublisherException : Exception
    {
        public PublisherException(string message):base(message)
        {

        }
    }
}
