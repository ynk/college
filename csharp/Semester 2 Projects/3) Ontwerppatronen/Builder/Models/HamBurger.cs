namespace Builder.Models
{
    public class HamBurger : Burger
    {
        public override string Name
        {
            get { return "Hamburger"; }
        }

        public override float Price
        {
            get { return 2.80f; }
        }
    }
}