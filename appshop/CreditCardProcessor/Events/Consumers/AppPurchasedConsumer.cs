using ApiAppShop.Domain.Events;
using CreditCardProcessor.Events.Producers;
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
            bool validCreditCard;
            ValidateCreditCard(context.Message.CreditCard, out validCreditCard);

            var appPurchasedStatusConfirmation = new AppPurchasedStatusConfirmation
            {
                Id = Guid.NewGuid().ToString(),
                TimeStamp = DateTime.UtcNow,
                AppId = context.Message.AppId,
                UserId = context.Message.UserId,
                StatusConfirmation = validCreditCard
            };

            AppPurchasedStatusConfirmationProducer.Publish(appPurchasedStatusConfirmation);            
        }

        private void ValidateCreditCard(CreditCard creditCard, out bool validCreditCard) 
        {
            string creditCardNumber = CreditCardValidator.GetCreditCardLast4Numbers(creditCard);
            validCreditCard = CreditCardValidator.Validate(creditCard);
            PrintCreditCardValidatorMessage(validCreditCard, creditCardNumber);
        }

        private void PrintCreditCardValidatorMessage(bool validCreditCard, string creditCardNumber)
        {
            string message = validCreditCard ? $"Valid Credit Card {creditCardNumber}." : $"Credit Card {creditCardNumber} denied.";
            Console.WriteLine(message);
        }
    }
}
