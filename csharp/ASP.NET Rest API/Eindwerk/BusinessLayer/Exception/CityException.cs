using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Execptions
{
    public class CityException : System.Exception
    {
        public CityException(string message) : base(message)
        {

        }
    }
}
