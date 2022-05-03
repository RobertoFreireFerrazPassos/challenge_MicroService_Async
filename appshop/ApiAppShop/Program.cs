using ApiAppShop.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;

namespace ApiAppShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.
                        ConfigureLogging((hostingContext, logging) =>
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
                        }).
                        UseStartup<Startup>();
                });
    }
}
