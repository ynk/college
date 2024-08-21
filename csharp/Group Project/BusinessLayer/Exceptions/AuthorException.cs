using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Exceptions
{
    public class AuthorException : Exception
    {
        public AuthorException(string message) : base(message)
        {

        }
    }
}
