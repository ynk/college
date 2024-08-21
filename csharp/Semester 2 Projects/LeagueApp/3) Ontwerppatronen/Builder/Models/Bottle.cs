using Builder.Interfaces;

namespace Builder.Models
{
    public class Bottle : IPacking
    {
        public string Pack
        { 
            get { return "Bottle"; }
        }
    }
}