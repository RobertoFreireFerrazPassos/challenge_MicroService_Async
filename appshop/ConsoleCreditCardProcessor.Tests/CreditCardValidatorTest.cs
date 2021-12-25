using ConsoleCreditCardProcessor.Tests.Builders.CreditCard;
using CreditCardProcessor.Services.Validation;
using CreditCardProcessor.Utils;
using System.Collections.Generic;
using Xunit;
using ValueObjects = ApiAppShop.Domain.ValueObjects;

namespace ConsoleCreditCardProcessor.Tests
{
    public class CreditCardValidatorTest
    {
        public static IEnumerable<object[]> CreditCardData()
        {
            var creditCardBuilder = new CreditCardBuilder();

            return new List<object[]>
            {
                new object[] { creditCardBuilder.BuildRandomCompleteCreditCard()() },
                new object[] { creditCardBuilder.BuildRandomCompleteCreditCard()() },
                new object[] { creditCardBuilder.BuildRandomCompleteCreditCard()() }
            };
        }        

        [Theory]
        [MemberData(nameof(CreditCardData))]
        public void MustGetCreditCardLast4Numbers(CreditCardImpl creditCard)
        {
            string creditCardLast4Numbers = CreditCardUtil.GetCreditCardLast4Numbers(creditCard);

            Assert.True(creditCardLast4Numbers.Length == 4);
        }
    }
}
