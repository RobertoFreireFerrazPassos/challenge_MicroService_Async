using ApiAppShop.Domain.Events;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.Consumers
{
    public class AppPurchasedStatusConfirmationConsumer : IConsumer<AppPurchasedStatusConfirmationEvent>
    {
        public async Task Consume(ConsumeContext<AppPurchasedStatusConfirmationEvent> context)
        {
            string message = context.Message.StatusConfirmation ?
                $"Purchase made by user {context.Message.UserId} on app {context.Message.AppId}" :
                $"Purchase denied";
            
            Console.WriteLine(message);
        }
    }
}
