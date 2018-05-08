using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Program
    {
        static void Main(string[] args)
        {
            Parking.Instance.AddCar(new Car() { CarId = 1, Type = CarType.Passenger, Balance = 10});
            Parking.Instance.AddCar(new Car() { CarId = 2, Type = CarType.Motorcycle, Balance = 20 });
            Parking.Instance.AddCar(new Car() { CarId = 3, Type = CarType.Truck, Balance = 100});

            Parking.Instance.RemoveCar(2);

            foreach (var item in Parking.Instance.CarList)
            {
                Console.WriteLine("id = {0} \n balance = {1} \n type = {2}", item.CarId, item.Balance, item.Type);
                Console.WriteLine("----------------------------------------------------------------------------------");
            }

            Parking.Instance.WriteOff();

            foreach (var item in Parking.Instance.CarList)
            {
                Console.WriteLine("id = {0} \n balance = {1} \n type = {2}", item.CarId, item.Balance, item.Type);
                Console.WriteLine("----------------------------------------------------------------------------------");
            }
            Console.WriteLine("Parking balance = " + Parking.Instance.Balance);
        }
    }
}
