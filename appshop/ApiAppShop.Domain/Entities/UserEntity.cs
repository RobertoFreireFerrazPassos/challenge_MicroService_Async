using ApiAppShop.Domain.ValueObjects;

namespace ApiAppShop.Domain.Entities
{
    public class UserEntity : Entity
    {
        public string Name { get; set; }
        public CreditCard CreditCard { get; set; }        
        public Address Address { get; set; }
    }
}
