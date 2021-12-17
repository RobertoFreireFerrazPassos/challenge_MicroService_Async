using MassTransit;
using System;
using System.Threading.Tasks;

namespace CreditCardProcessor.Events.Consumers
{
    class AppPurchasedConsumer : IConsumer<AppPurchasedEvent>
    {
        public Task Consume(ConsumeContext<AppPurchasedEvent> context)
        {
            var userId = context.Message.UserId;
            var appId = context.Message.AppId;

            Console.WriteLine($"App: [{appId}] purchased by {userId}");
            return Task.CompletedTask;
        }
    }
}
