namespace Factory.Models
{
    public class CCRecipient : Recipient
    {
        public CCRecipient(string address)
        {
            base.address = address;
        }
    }
}