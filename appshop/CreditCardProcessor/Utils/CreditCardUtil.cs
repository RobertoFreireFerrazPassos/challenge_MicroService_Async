using ApiAppShop.Domain.Events;

namespace CreditCardProcessor.Utils
{
    public static class CreditCardUtil
    {
        public static string GetCreditCardLast4Numbers(CreditCard creditCard)
        {
            string creditCardNumber = creditCard.Number;
            int creditCardNumberLength = creditCardNumber.Length;
            if (creditCardNumberLength <= 4) return "";

            return creditCardNumber.Substring(creditCardNumberLength - 4);
        }
    }
}
