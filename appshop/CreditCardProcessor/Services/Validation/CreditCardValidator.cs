using ApiAppShop.Domain.Events;
using System;

namespace CreditCardProcessor.Services.Validation
{
    public class CreditCardValidator
    {
        public static bool Validate(CreditCard creditCard)
        {
            // validate credit card
            Random rng = new Random();
            return rng.Next(0, 2) > 0;
        }
    }
}
