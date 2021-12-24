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

        public static string GetCreditCardLast4Numbers(CreditCard creditCard)
        {
            string creditCardNumber = creditCard.Number; 
            int  creditCardNumberLength = creditCardNumber.Length;
            if (creditCardNumberLength <= 4) return "";

            return creditCardNumber.Substring(creditCardNumberLength - 4);
        }
    }
}
