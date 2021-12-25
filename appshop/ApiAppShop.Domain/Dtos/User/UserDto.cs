using ApiAppShop.Domain.ValueObjects;
using ApiUser.Domain.Enums;
using System;

namespace ApiAppShop.Domain.Dtos.User
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
        public Address Address { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
