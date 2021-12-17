namespace ApiAppShop.Domain.ValueObjects
{
    public class CreditCard
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string CVV { get; set; }
        public string ExpirationDateMMYYYY { get; set; }
    }
}
