namespace Strategy.Models
{
    public abstract class Product
    {
        protected int Price { get; set; }

        public int GetPrice()
        {
            return Price;
        }
    }
}