using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BusinessLayer.Exceptions
{
    public  class ProductException : Exception
    {
        public ProductException(string message) : base(message)
        {

        }
    }
}
