using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace Rumbleday.Core
{
    public class Car:IEquatable<Car>
    {
        private readonly IRumbledayLogger _logger;
        private string path = Path.Combine(Directory.GetCurrentDirectory(), "cars.json");

        public Car(IRumbledayLogger logger)
        {
            _logger = logger;
        }

        public string ID { get { return Guid.NewGuid().ToString(); } }
        public string Name { get; set; }
        public double Price { get; set; }
        public CarType Type { get; set; }
        public DateTime Year { get; set; }
        public double PriceWithDiscount
        {
            get { return Price - Price*Discount/100; }          
        }
        public int Discount
        {
            get
            {
                switch (Type)
                {
                    case CarType.Passenger:
                        return 50;
                    case CarType.Trucks:                        
                    case CarType.Utilitary:
                        return 25 + ((DateTime.Now.Year - Year.Year) > 20 ? 20 : DateTime.Now.Year - Year.Year);
                    default: return 0;
                }
            }
        }

        public void Save()
        {
            
            var carsStr = File.ReadAllText(path);
            var cars = JsonConvert.DeserializeObject<List<Car>>(carsStr);          
            if(cars != null)
            {
                cars.Add(this);
            }
            else
            {
                cars = new List<Car>();
                cars.Add(this);
            }

            carsStr = JsonConvert.SerializeObject(cars);
            File.WriteAllText(path, carsStr);
            _logger.Log("Succesfuly added a car");
        }

        public static string GetAllCars()
        {
            return File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "cars.json"));
        }

        public bool Equals(Car other)
        {
            if(other == null)
                return false;
            return (this.Name == other.Name && Price == other.Price && Type == other.Type && Year == other.Year);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
