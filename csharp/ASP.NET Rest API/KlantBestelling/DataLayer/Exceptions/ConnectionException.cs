using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Exception
{
    public class ConnectionException : System.Exception
    {
        public ConnectionException(string message) : base(message)
        {

        }
    }
}
