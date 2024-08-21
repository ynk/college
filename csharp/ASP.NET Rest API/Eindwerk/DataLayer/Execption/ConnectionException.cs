using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Execption
{
    public class ConnectionException : System.Exception
    {
        public ConnectionException(string message) : base(message)
        {

        }
    }
}
