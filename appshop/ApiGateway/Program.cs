using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHostBuilder builder = new WebHostBuilder();

            builder.ConfigureServices(s => {
            });

            builder.UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();

            var host = builder.Build();

            host.Run();
        }
    }
}
