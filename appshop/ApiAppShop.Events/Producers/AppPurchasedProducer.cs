using ApiAppShop.Domain.Events;
using ApiAppShop.Domain.Events.Producers;
using MassTransit;
using System.Threading.Tasks;

namespace ApiAppShop.Events.Producers
{
    public class AppPurchasedProducer : IAppPurchasedProducer
    {
        private readonly IPublishEndpoint _publisher;

        public AppPurchasedProducer(IPublishEndpoint publisher) 
        {
            _publisher = publisher;
        }

        public async Task Publish(AppPurchasedEvent appPurchasedEvent) {
            await _publisher.Publish(appPurchasedEvent);
        }
    }
}
