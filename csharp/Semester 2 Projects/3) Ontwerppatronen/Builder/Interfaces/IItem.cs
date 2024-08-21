namespace Builder.Interfaces
{
    public interface IItem
    {
        string Name { get; }
        IPacking Packing { get; }
        float Price { get; }
    }
}