using ApiAppShop.Domain.Enums;
using ApiAppShop.Domain.ValueObjects;
using System;

namespace ApiAppShop.Application.DataContracts.Requests.User
{
    public class SignInRequest
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
        public Address Address { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}


