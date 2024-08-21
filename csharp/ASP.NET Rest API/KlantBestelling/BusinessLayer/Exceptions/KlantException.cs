using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Exceptions
{
    public class KlantException : Exception
    {
        public KlantException(string message) : base(message)
        {

        }
    }
}
