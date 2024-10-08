﻿using Factory.Interfaces;
using Factory.Models;

namespace Factory
{
    public class RecipientFactory : IRecipientFactory
    {
        public Recipient CreateRecipient(RecipientType type, string address)
        {
            switch (type)
            {
                case RecipientType.EMAIL: return new EmailRecipient(address);
                case RecipientType.CC: return new CCRecipient(address);
                case RecipientType.BCC: return new BCCRecipient(address);
                default: return new EmailRecipient(address);
            }
        }
    }
}