using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLayer.Model
{
    public enum Product
    {
        Leffe, Westmalle, Orval, Duvel
    }

    static public class Tools
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }


}