using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Execptions
{
    public class RiverException : System.Exception
    {
        public RiverException(string message) : base(message)
        {

        }
    }
}
