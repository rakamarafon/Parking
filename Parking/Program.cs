using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking
{
    class Program
    {
        static void Main(string[] args)
        {
            Parking.Instance.AddCar(new Car() { CarId = 1, Type = CarType.Passenger, Balance = 110});
            Parking.Instance.AddCar(new Car() { CarId = 2, Type = CarType.Motorcycle, Balance = 120 });
            Parking.Instance.AddCar(new Car() { CarId = 3, Type = CarType.Truck, Balance = 100});

            Thread.Sleep(30 * 1000);
            Parking.Instance.WriteOff();
            Thread.Sleep(30 * 1000);
            Parking.Instance.WriteOff();
            Thread.Sleep(30 * 1000);
            Parking.Instance.WriteOff();

            var temp = Parking.Instance.GetTransactionsByLastMinute();

            foreach (var item in temp)
            {
                Console.WriteLine("TransactionDataTime = {0} \n CarId = {1} \n MoneyPaid = {2}", item.TransactionDataTime, item.CarId, item.MoneyPaid);
                Console.WriteLine("----------------------------------------------------------------------------------");
            }
            Console.WriteLine("Free space on parking: {0}", Parking.Instance.GetFreeSpaceOnParking());
            Console.WriteLine(Parking.Instance.Balance);
            Console.ReadKey();
        }
    }
}
