namespace ApiAppShop.Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string AdditionalInfo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
