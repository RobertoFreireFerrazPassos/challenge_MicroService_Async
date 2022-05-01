using ApiAppShop.Domain.Enums;
using ApiAppShop.Domain.ValueObjects;
using System;

namespace ApiAppShop.Domain.Dtos.User
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public UserRoleEnum Role { get; set; }
        public string Password { get; set; }
        public Byte[] PasswordHash { get; set; }
        public Byte[] PasswordSalt { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
        public Address Address { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
