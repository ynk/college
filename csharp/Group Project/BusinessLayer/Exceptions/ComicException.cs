using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Exceptions
{
    public class ComicException : Exception
    {
        public ComicException(string message) : base(message)
        {

        }
    }
}
