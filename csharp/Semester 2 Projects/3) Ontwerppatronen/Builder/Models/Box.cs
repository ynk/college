using Builder.Interfaces;

namespace Builder.Models
{
    public class Box : IPacking
    {
        public string Pack
        {
            get { return "Box"; }
        }
    }
}