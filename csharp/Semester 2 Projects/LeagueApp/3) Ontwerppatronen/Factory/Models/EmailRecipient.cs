namespace Factory.Models
{
    public class EmailRecipient : Recipient
    {
        public EmailRecipient(string address)
        {
            base.address = address;
        }
    }
}