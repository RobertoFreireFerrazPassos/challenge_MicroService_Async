using ApiAppShop.Domain.Events.Base;
using ApiAppShop.Domain.ValueObjects;

namespace ApiAppShop.Domain.Events
{
    public class AppPurchasedEvent : Event
    {
        private static readonly string Identifier = "AppPurchasedEvent";

        public AppPurchasedEvent() : base(Identifier) { }

        public string AppId { get; set; }
        public string UserId { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
