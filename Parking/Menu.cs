using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    public class Menu
    {    
        private Car AddNewCar()
        {
            Console.Clear();
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
                    Console.Clear();
                    Console.WriteLine("type Passenger has been selected");
                    car.Type = CarType.Passenger;
                    break;
                case '2':
                    Console.Clear();
                    Console.WriteLine("type Truck has been selected");
                    car.Type = CarType.Truck;
                    break;
                case '3':
                    Console.Clear();
                    Console.WriteLine("type Bus has been selected");
                    car.Type = CarType.Bus;
                    break;
                case '4':
                    Console.Clear();
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
            Console.Clear();
            Console.WriteLine("car {0} was successfuly added", id);
            return car;

        }
        private void RemoveCar()
        {
            Console.Clear();
            if(Parking.Instance.GetFreeSpaceOnParking() == Settings.ParkingSpace)
            {
                Console.WriteLine("There are no cars on the parking place");
                return;
            }
            Console.WriteLine("input car id");
            string input_id = Console.ReadLine();
            Console.Clear();
            int id = Convert.ToInt32(input_id);
            int res = Parking.Instance.RemoveCar(id);
            if (res == 2)
            {
                Console.WriteLine("car {0} was successfuly removed", id);
            }
            else if(res == 1)
            {
                Console.WriteLine("can't remove {0} car. Please raisee balance", id);
            }
        }
        private void ShowCarList()
        {
            Console.Clear();
            if (Parking.Instance.CarList.Count == 0)
            {
                Console.WriteLine("There are no cars on the parking place");
                return;
            }
            foreach (var item in Parking.Instance.CarList)
            {
                Console.WriteLine("CarId: {0}\tCarBalance: {1}\tCarType: {2}", item.CarId, item.Balance, item.Type);
            }
            Console.WriteLine(new string('-',100));
        }
        private void AddBalanceForCar()
        {
            Console.Clear();
            if (Parking.Instance.GetFreeSpaceOnParking() == Settings.ParkingSpace)
            {
                Console.WriteLine("There are no cars on the parking place");
                return;
            }
            Console.WriteLine("Please enter car id (click 'b' for back)");
            string input = Console.ReadLine();
            if (input == "b") return;
            int id = Convert.ToInt32(input);
            Console.WriteLine("Enter summ which be add to car balance: ");
            input = Console.ReadLine();
            int sum = Convert.ToInt32(input);
            Parking.Instance.RefillCarBalance(id, sum);
        }
        private bool BeSureBeforeExit()
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to exit?\n1 - Yes\t    2 - No");
            var input = Console.ReadKey();
            switch (input.KeyChar)
            {
                case '1':return true;
                case '2': Console.Clear(); return false;
            }
            return false;
        }
        private void ShowFreeSpace()
        {
            Console.Clear();
            int space = Parking.Instance.GetFreeSpaceOnParking();
            if (space > 0)
            {
                Console.WriteLine("There are {0} free spaces on the parking place", space);
            }
            else
            {
                Console.WriteLine("There are no free space on the parking place");
            }
        }
        private void ShowLogForLastMinute()
        {
            Console.Clear();
            if(Parking.Instance.TransactionList.Count > 0)
            {
                Console.WriteLine("Date \t \t \t CarID \t \t MoneyPaid \t \t");
                foreach (var item in Parking.Instance.TransactionList)
                {                    
                    Console.WriteLine(String.Format("{0} \t {1} \t \t {2}", item.TransactionDataTime.ToString(), item.CarId.ToString(), item.MoneyPaid.ToString()));
                }
            }
            else
            {
                Console.WriteLine("For the last minute there was no transaction");
            }
            
        }
        private void ShowTotalParkingIncome()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Total parking income is {0}",Parking.Instance.Balance);
            Console.ResetColor();
        }
        public void ShowMainMenu()
        {
            while (true)
            {
                //Console.Clear();
                Console.WriteLine("1 - add new car to parking place");
                Console.WriteLine("2 - remove car from parking place");
                Console.WriteLine("3 - add balance  for car");
                Console.WriteLine("4 - show list of cars on parking place");
                Console.WriteLine("5 - show free space on parking place");
                Console.WriteLine("6 - show log for last minute");
                Console.WriteLine("7 - total parking income");
                Console.WriteLine("q - Exit");

                var input = Console.ReadKey();
                switch (input.KeyChar)
                {
                    case '1':
                        Car car = AddNewCar();
                        Parking.Instance.AddCar(car);
                        break;
                    case '2':
                        RemoveCar();
                        break;
                    case '3':
                        AddBalanceForCar();
                        break;
                    case '4':
                        ShowCarList();
                        break;
                    case '5':
                        ShowFreeSpace();
                        break;
                    case '6':
                        ShowLogForLastMinute();
                        break;
                    case '7':
                        ShowTotalParkingIncome();
                        break;
                    case 'q':
                        if(BeSureBeforeExit()) Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Incorect input");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
