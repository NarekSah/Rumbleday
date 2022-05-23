using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Rumbleday.Core
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().RunAsync();

           var  rumbleDayLogger = _serviceProvider.GetService<IRumbledayLogger>();


            User user = new User();
            Console.WriteLine("UserName: ");
            user.Login = Console.ReadLine();

            Console.WriteLine("Password: ");
            user.Password = Console.ReadLine();

            if ((user.ValidLogin())) // checks with Rumbleday.Core\bin\Debug\netcoreapp3.1\users.json
            {
                try
                {
                    Car car = new Car(rumbleDayLogger);
                    Console.WriteLine("Adding New Car");
                    Console.WriteLine("Name");
                    car.Name = Console.ReadLine();
                    Console.WriteLine("Price");
                    car.Price = double.Parse(Console.ReadLine());
                    Console.WriteLine("Type:  Passenger =1,Trucks = 2, Utilitary = 3");
                    car.Type = (CarType)int.Parse(Console.ReadLine());

                    Console.WriteLine("Year");
                    var yrs = int.Parse(Console.ReadLine());
                    car.Year = new DateTime(yrs, 1, 1);
                    
                    car.Save();

                    Console.WriteLine(Car.GetAllCars());

                }
                catch (Exception ex)
                {
                   rumbleDayLogger.LogError(ex);
                }                
            }
            else
            {
                Console.WriteLine("Invalid Login");
                rumbleDayLogger.Log("Invalid Login");
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {

            var services = new ServiceCollection();
            services
                .AddLogging(cfg => cfg.AddConsole())
                .AddScoped(typeof(IRumbledayLogger), typeof(RumbledayLogger));

            _serviceProvider = services.BuildServiceProvider();

            return Host.CreateDefaultBuilder(args)
                        .ConfigureServices(s =>
                        {
                            s = services;
                        });            
        }
    }
}
