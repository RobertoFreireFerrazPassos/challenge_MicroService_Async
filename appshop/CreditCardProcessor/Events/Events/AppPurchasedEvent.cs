using System;

namespace CreditCardProcessor.Events
{
    public interface AppPurchasedEvent
    {
        public string Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string AppId { get; set; }
        public string UserId { get; set; }
        public CreditCard CreditCard { get; set; }
    }

    public interface CreditCard
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string CVV { get; set; }
        public string ExpirationDateMMYYYY { get; set; }
    }
}
