using System;

namespace ApiUser.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public CreditCardModel CreditCard { get; set; }
    }
}
