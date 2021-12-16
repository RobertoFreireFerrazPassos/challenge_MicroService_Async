namespace ApiUser.Models
{
    public class CreditCardModel
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string CVV { get; set; }
        public ExpirationDateModel ExpirationDate { get; set; }
    }
}
