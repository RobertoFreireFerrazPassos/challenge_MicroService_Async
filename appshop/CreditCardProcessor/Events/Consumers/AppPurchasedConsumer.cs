﻿using MassTransit;
using System;
using System.Threading.Tasks;

namespace CreditCardProcessor.Events.Consumers
{
    public class AppPurchasedConsumer : IConsumer<AppPurchasedEvent>
    {
        public async Task Consume(ConsumeContext<AppPurchasedEvent> context)
        {
            var userId = context.Message.UserId;
            var appId = context.Message.AppId;

            Console.WriteLine($"App: [{appId}] purchased by {userId}");
        }
    }
}
