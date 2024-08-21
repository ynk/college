using Builder.Interfaces;

namespace Builder.Models
{
    public abstract class Drink : IItem
    {
        public abstract string Name { get; }
        public IPacking Packing => new Bottle();
        public abstract float Price { get; }
    }
}