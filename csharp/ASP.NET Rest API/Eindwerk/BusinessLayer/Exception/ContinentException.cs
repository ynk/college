using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Execptions
{
    public class ContinentException : System.Exception
    {
        public ContinentException(string message) : base(message)
        {

        }
    }
}
