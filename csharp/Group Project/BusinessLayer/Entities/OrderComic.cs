namespace BusinessLayer.Entities
{
    public class OrderComic
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Comic Comic { get; set; }
        public int Aantal { get; set; }
    }
}