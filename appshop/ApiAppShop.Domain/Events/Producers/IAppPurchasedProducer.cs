using System.Threading.Tasks;

namespace ApiAppShop.Domain.Events.Producers
{
    public interface IAppPurchasedProducer
    {
        public Task Publish(AppPurchasedEvent appPurchasedEvent);
    }
}
