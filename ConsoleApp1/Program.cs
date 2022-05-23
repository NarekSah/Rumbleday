using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .ConfigureServices((_, services) =>
               services.AddHostedService<Worker>()
                       .AddScoped<IMessageWriter, MessageWriter>());
    }


}
