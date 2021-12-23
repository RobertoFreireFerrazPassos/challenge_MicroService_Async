using ApiAppShop.Domain.Events;
using CreditCardProcessor.Services.Validation;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.Consumers
{
    public class AppPurchasedConsumer : IConsumer<AppPurchasedEvent>
    {
        public async Task Consume(ConsumeContext<AppPurchasedEvent> context)
        {
            var userId = context.Message.UserId;
            var appId = context.Message.AppId;
            var creditCard = context.Message.CreditCard;
            bool validCreditCard = CreditCardValidator.Validate(creditCard);
            string creditCardNumber = CreditCardValidator.GetCreditCardLast4Numbers(creditCard);

            if (validCreditCard)
            {
                Console.WriteLine($"Valid Credit Card {creditCardNumber}");
                Console.WriteLine($"App: [{appId}] purchased by {userId}");
            }
            else 
            {
                Console.WriteLine($"Credit Card {creditCardNumber} denied");
            }
        }
    }
}
