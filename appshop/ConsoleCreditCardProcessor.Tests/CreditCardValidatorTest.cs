using ConsoleCreditCardProcessor.Tests.Builders.CreditCard;
using CreditCardProcessor.Utils;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

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
        public void When_GetCreditCardLast4Numbers_Must_HaveLengthOfFour(CreditCardImpl creditCard)
        {
            var result = CreditCardUtil.GetCreditCardLast4Numbers(creditCard);

            result.Should().HaveLength(4);
        }
    }
}
