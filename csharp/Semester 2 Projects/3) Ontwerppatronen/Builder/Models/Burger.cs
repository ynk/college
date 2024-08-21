using Builder.Interfaces;

namespace Builder.Models
{
    public abstract class Burger : IItem
    {
        public abstract string Name { get; }
        public IPacking Packing
        {
            get
            {
                return new Box();
            }
        }
        public abstract float Price { get; }
    }
}