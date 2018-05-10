using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    public class Menu
    {    
        private Car AddNewCar()
        {
            Car car = new Car();

            Console.WriteLine("select car type");
            Console.WriteLine("1 - Passenger");
            Console.WriteLine("2 - Truck");
            Console.WriteLine("3 - Bus");
            Console.WriteLine("4 -Motorcycle");

            var input = Console.ReadKey();
            switch (input.KeyChar)
            {
                case '1':
                    Console.WriteLine("type Passenger has been selected");
                    car.Type = CarType.Passenger;
                    break;
                case '2':
                    Console.WriteLine("type Truck has been selected");
                    car.Type = CarType.Truck;
                    break;
                case '3':
                    Console.WriteLine("type Bus has been selected");
                    car.Type = CarType.Bus;
                    break;
                case '4':
                    Console.WriteLine("type Motorcycle has been selected");
                    car.Type = CarType.Motorcycle;
                    break;
            }

            Console.WriteLine("input car id");
            string input_id = Console.ReadLine();
            int id = Convert.ToInt32(input_id);
            //TODO: check valid input
            car.CarId = id;
            Console.WriteLine("input car balance");
            string input_balance = Console.ReadLine();
            int balance = Convert.ToInt32(input_balance);
            //TODO: check valid id
            car.Balance = balance;
            return car;

        }
        private bool RemoveCar()
        {
            Console.WriteLine("input car id");
            string input_id = Console.ReadLine();
            int id = Convert.ToInt32(input_id);
            //TODO: call method from parking class
            return true;
        }
        public void ShowMainMenu()
        {
            Console.WriteLine("1 - add new car to parking place");
            Console.WriteLine("2 - remove car from parking place");
            Console.WriteLine("3 - add balance  for car");
            Console.WriteLine("4 - show free space on parking place");
            Console.WriteLine("5 - show log for last minute");

            var input = Console.ReadKey();
            switch(input.KeyChar)
            {
                case '1':
                    Car car = AddNewCar();                             
                    break;
                case '2':
                    RemoveCar();
                    break;
                case '3':
                    Console.WriteLine();
                    break;
                case '4':
                    Console.WriteLine();
                    break;
            }
        }
    }
}
