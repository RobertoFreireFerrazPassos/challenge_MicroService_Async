using System;

namespace ApiUser.Domain.Entities
{
    public class UserEntity : Entity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
        public Address Address { get; set; }
        public CreditCard CreditCard { get; set; }
}
}
