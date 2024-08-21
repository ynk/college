using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Execptions
{
    public class CountryException : System.Exception
    {
        public CountryException(string message) : base(message)
        {

        }
    }
}
