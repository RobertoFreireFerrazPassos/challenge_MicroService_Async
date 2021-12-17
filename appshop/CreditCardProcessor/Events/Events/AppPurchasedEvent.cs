using System;

namespace CreditCardProcessor.Events
{
    public class AppPurchasedEvent
    {
        private Guid Id { get; }
        private string Identifier { get; }
        private DateTime TimeStamp { get; }
        public string AppId { get; set; }
        public string UserId { get; set; }
        public CreditCard CreditCard { get; set; }
    }

    public class CreditCard
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string CVV { get; set; }
        public string ExpirationDateMMYYYY { get; set; }
    }
}
