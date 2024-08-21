using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Exceptions
{
    public  class BestellingException : Exception
    {
        public BestellingException(string message) : base(message)
        {

        }
    }
}
