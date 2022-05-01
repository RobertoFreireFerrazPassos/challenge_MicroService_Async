using ApiAppShop.Domain.Enums;
using ApiAppShop.Domain.ValueObjects;
using System;

namespace ApiAppShop.Domain.Entities
{
    public class UserEntity : Entity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public UserRoleEnum Role { get; set; }
        public Byte[] PasswordHash { get; set; }
        public Byte[] PasswordSalt { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
        public CreditCard CreditCard { get; set; }        
        public Address Address { get; set; }
    }
}
