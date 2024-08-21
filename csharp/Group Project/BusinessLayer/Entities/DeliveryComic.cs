namespace BusinessLayer.Entities
{
    public class DeliveryComic
    {
        public int Id { get; set; }
        public Delivery Delivery { get; set; }
        public Comic Comic { get; set; }
        public int Aantal { get; set; }
    }
}