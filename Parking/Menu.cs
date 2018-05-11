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
        private readonly string MSG_FOR_ID = "ID";
        private readonly string MSG_FOR_BALANCE = "balance";
        private void PrintMainMenu()
        {
            Console.WriteLine("1 - add new car to parking place");
            Console.WriteLine("2 - remove car from parking place");
            Console.WriteLine("3 - add balance  for car");
            Console.WriteLine("4 - show list of cars on parking place");
            Console.WriteLine("5 - show free space on parking place");
            Console.WriteLine("6 - show busy space on parking place");
            Console.WriteLine("7 - show log for last minute");
            Console.WriteLine("8 - total parking income");
            Console.WriteLine("9 - parking income by last minute");
            Console.WriteLine("h - history from Transaction.log");
            Console.WriteLine("q - Exit");
        }
        private void PrintPositiveMsg(string msg, string[] args = null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg,args);
            Console.ResetColor();
        }
        private void PrintNegativeMsg(string msg, string[] args = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg, args);
            Console.ResetColor();
        }
        private void PrintInfoMsg(string msg, string[] args = null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg, args);
            Console.ResetColor();
        }
        private void AfterEachMsg()
        {
            Console.ResetColor();
        }
        private void PrintLongLine(int count = 100)
        {
            Console.WriteLine(new string('-', count));
        }
        private void IncorectInput()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Incorect input");
            Console.ResetColor();
        }
        private void IncorectInputCarInfo(string msg)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Incorect input car {0} Car {1} must be a number", msg, msg);
            Console.ResetColor();
        }
        private Car AddNewCar()
        {
            Console.Clear();
            Car car = new Car();

            Console.WriteLine("select car type");
            Console.WriteLine("1 - Passenger");
            Console.WriteLine("2 - Truck");
            Console.WriteLine("3 - Bus");
            Console.WriteLine("4 -Motorcycle");
            Console.WriteLine("b - back to the main menu");

            var input = Console.ReadKey();
            switch (input.KeyChar)
            {
                case '1':
                    Console.Clear();
                    PrintPositiveMsg("Type Passenger has been selected");
                    car.Type = CarType.Passenger;
                    break;
                case '2':
                    Console.Clear();
                    PrintPositiveMsg("Type Truck has been selected");
                    car.Type = CarType.Truck;
                    break;
                case '3':
                    Console.Clear();
                    PrintPositiveMsg("Type Bus has been selected");
                    car.Type = CarType.Bus;
                    break;
                case '4':
                    Console.Clear();
                    PrintPositiveMsg("type Motorcycle has been selected");
                    car.Type = CarType.Motorcycle;
                    break;
                case 'b':
                    Console.Clear();
                    return null;
                default:
                    AddNewCar();
                    break;
            }

            Console.WriteLine("input car id\nb - back to main menu");
            Console.Write("Car ID:\t");
            string input_id = Console.ReadLine();
            if (input_id == "b")
            {
                Console.Clear();
                return null;
            }
            int id;

            if (Int32.TryParse(input_id, out id))
            {
                Console.WriteLine(car.CarId = id);
            }
            else
            {
                IncorectInputCarInfo(MSG_FOR_ID);
                return null;
            }
            Console.Clear();
            Console.WriteLine("input car balance\nb - back to main menu");
            Console.Write("Car Balance:\t");
            string input_balance = Console.ReadLine();
            if (input_balance == "b")
            {
                Console.Clear();
                return null;
            }
            int balance;
            if (Int32.TryParse(input_balance, out balance))
            {
                Console.WriteLine(car.Balance = balance);
            }
            else
            {
                IncorectInputCarInfo(MSG_FOR_BALANCE);
                return null;
            }
            Console.Clear();            
            return car;

        }
        private void RemoveCar()
        {
            Console.Clear();
            if(Parking.Instance.GetFreeSpaceOnParking() == Parking.Instance.ParkingSpace)
            {
                PrintNegativeMsg("There are no cars on the parking place");                
                return;
            }
            Console.WriteLine("input car id");
            string input_id = Console.ReadLine();
            Console.Clear();
            int id;
            if (Int32.TryParse(input_id, out id))
            {
                int res = Parking.Instance.RemoveCar(id);
                if (res == 2)
                {
                    PrintPositiveMsg("car with ID {0} was successfuly removed", new string[]{ id.ToString()});
                }
                else if (res == 1)
                {
                    PrintNegativeMsg("Can't remove car with ID {0}. Please raisee balance", new string[] { id.ToString()});
                }
                else if(res == 0)
                {
                    PrintNegativeMsg("There are no cars on the parking place");
                }
                else if(res == 3)
                {
                    PrintNegativeMsg("Can not found car with ID {0}", new string[] { id.ToString()});
                }
            }
            else
            {
                IncorectInputCarInfo("id!");
                return;
            }                     
        }
        private void ShowCarList()
        {
            Console.Clear();
            if (Parking.Instance.CarList.Count == 0)
            {
                PrintInfoMsg("There are no cars on the parking place");
                return;
            }
            foreach (var item in Parking.Instance.CarList)
            {
                Console.WriteLine("CarId: {0}\tCarBalance: {1}\t \t CarType: {2}", item.CarId, item.Balance, item.Type);
            }
            PrintLongLine();
        }
        private void AddBalanceForCar()
        {
            Console.Clear();
            if (Parking.Instance.GetFreeSpaceOnParking() == Parking.Instance.ParkingSpace)
            {
                PrintInfoMsg("There are no cars on the parking place");
                return;
            }
            Console.WriteLine("Please enter car ID (click 'b' for back)");
            string input = Console.ReadLine();
            if (input == "b")
            {
                Console.Clear();
                return;
            }
            int id;
            Console.Clear();
            if (!Int32.TryParse(input, out id))
            {
                IncorectInputCarInfo(MSG_FOR_ID);
                return;
            }
            Console.WriteLine("Enter summ which be add to car balance: ");
            input = Console.ReadLine();
            int sum;
            if (!Int32.TryParse(input, out sum))
            {
                IncorectInputCarInfo(MSG_FOR_BALANCE);
                return;
            }
            Parking.Instance.RefillCarBalance(id, sum);
            Console.Clear();
            PrintPositiveMsg("Sum {0} has been added to car ID {1}", new string[] {sum.ToString(),id.ToString()});
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
                default:
                    IncorectInput();
                    break;

            }
            return false;
        }
        private void ShowFreeSpace()
        {
            Console.Clear();
            int space = Parking.Instance.GetFreeSpaceOnParking();
            if (space > 0)
            {
                PrintInfoMsg("There are {0} free spaces on the parking place", new string[] {space.ToString()});
            }
            else
            {
                PrintInfoMsg("There are no free space on the parking place");
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
                PrintInfoMsg("For the last minute there was no transaction");
            }
            
        }
        private void ShowTotalParkingIncome()
        {
            Console.Clear();
            PrintPositiveMsg("Total parking income is {0}",new string[] { Parking.Instance.TotalParkingProfit().ToString()});
        }
        private void ShowParkingIncomeByLastMinute()
        {
            Console.Clear();
            PrintPositiveMsg("Parking income by last minute is {0}", new string[] { Parking.Instance.ParkingProfitByLastMinute().ToString() });
        }
        private void ShowBusySpace()
        {
            Console.Clear();
            PrintInfoMsg("There are {0} busy place on parking", new string[] { Parking.Instance.GetBusySpaceOnParking().ToString()});
        }
        private void ShowTransactionLogFromFile()
        {
            Console.Clear();
            var list = Parking.Instance.GetTransactionsFromFile();
            if(list != null)
            {
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
                PrintLongLine();
            }  
            else
            {
                PrintNegativeMsg("The transaction file is not available\nLet's try again later");
            }
        }
        public void ShowMainMenu()
        {
            while (true)
            {
                PrintMainMenu();
                var input = Console.ReadKey();
                switch (input.KeyChar)
                {
                    case '1':
                        Car car = AddNewCar();
                        int code = Parking.Instance.AddCar(car);
                        if (code == 2) PrintPositiveMsg("car with ID:{0} was successfuly added", new string[] { car.CarId.ToString()});
                        else if (code == 4)
                        {
                            PrintNegativeMsg("In the parking lot there are no free places");
                        }
                        else if (code == 5)
                        {
                            PrintNegativeMsg("Car with ID:{0} already in the parking place", new string[] {car.CarId.ToString()});
                        }
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
                        ShowBusySpace();
                        break;
                    case '7':
                        ShowLogForLastMinute();
                        break;
                    case '8':
                        ShowTotalParkingIncome();
                        break;
                    case '9':
                        ShowParkingIncomeByLastMinute();
                        break;
                    case 'h':
                        ShowTransactionLogFromFile();
                        break;
                    case 'q':
                        if(BeSureBeforeExit()) Environment.Exit(0);
                        break;
                    default:
                        IncorectInput();
                        break;
                }
            }
        }
    }
}
