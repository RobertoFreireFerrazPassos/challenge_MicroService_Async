using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CreditCardProcessor.Infrastructure
{
    public class Appsettings
    {
        public static IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
        }
    }
}
