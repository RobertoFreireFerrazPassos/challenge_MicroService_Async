using ApiAppShop.Domain.Events;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace CreditCardProcessor.Events.Producers
{
    public class AppPurchasedStatusConfirmationProducer
    {
        private readonly IPublishEndpoint _publisher;

        public AppPurchasedStatusConfirmationProducer(IPublishEndpoint publisher)
        {
            _publisher = publisher;
        }

        public async Task Publish(AppPurchasedEvent appPurchasedEvent, 
            bool statusConfirmation)
        {
            await _publisher.Publish<AppPurchasedStatusConfirmation>(new AppPurchasedStatusConfirmation
            {
                Id = Guid.NewGuid().ToString(),
                TimeStamp = DateTime.UtcNow,
                AppId = appPurchasedEvent.AppId,
                UserId = appPurchasedEvent.UserId,
                StatusConfirmation = statusConfirmation
            });
        }
    }
}
