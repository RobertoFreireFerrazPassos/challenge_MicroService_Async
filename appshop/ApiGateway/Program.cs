using ApiGateway.Infrastructure.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace ApiGateway
{
    public class Program
    {
        public static void Main()
        {
            IWebHostBuilder builder = new WebHostBuilder();

            builder.ConfigureServices(s => {
            });

            builder.ConfigureLogging((hostingContext, logging) =>
            {
                logging.ClearProviders();

                var config = new LoggerConfiguration
                {
                    LogLevel = LogLevel.Information,
                    Color = ConsoleColor.Red
                };
                logging.AddProvider(new LoggerProvider(config));

                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));

                logging.AddConsole();

                logging.AddDebug();
            });            

            builder.UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();

            var host = builder.Build();

            host.Run();
        }
    }
}
