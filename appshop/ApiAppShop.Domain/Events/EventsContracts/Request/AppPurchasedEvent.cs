using ApiAppShop.Domain.ValueObjects;
using System;

namespace ApiAppShop.Domain.Events
{
    public class AppPurchasedEvent
    {
        public string Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string AppId { get; set; }
        public string UserId { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
