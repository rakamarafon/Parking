using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking
{
    public sealed class Parking : IParkingPlace
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());
        public static Parking Instance { get { return lazy.Value; } }
        public List<Car> CarList { get; set; }
        public List<Transaction> TransactionList { get; set; }
        public int Balance { get; set; }

        private Parking()
        {
            CarList = new List<Car>();
            TransactionList = new List<Transaction>();
        }

        private Car GetCarById(int id)
        {
            return CarList.Single<Car>(x => x.CarId == id);
        }
        private void WriteOffByCar(ref Car car)
        {
            int price = 0;
            foreach (var item in Settings.priceDictionary)
            {
                if (item.Key == car.Type) price = item.Value;
            }
            TransactionList.Add(new Transaction() { TransactionDataTime = DateTime.Now, CarId = car.CarId, MoneyPaid = price});
            car.Balance -= price;
            Balance += price;
        }

        public void AddCar(Car car)
        {
            CarList.Add(car);
        }

        public bool RemoveCar(int car_id)
        {
            Car car = GetCarById(car_id);
            if(car.Balance < 0)
            {
                return false;
            }
            else
            {
                CarList.Remove(car);
                return true;
            }            
        }

        public void RefillCarBalance(int car_id, int sum_to_refill)
        {
            Car car = GetCarById(car_id);
            car.Balance += sum_to_refill;
        }

        public void WriteOff(object obj = null)
        {
            if(CarList.Count > 0)
            {
                for (int i = 0; i < CarList.Count; i++)
                {
                    Car car = CarList[i];
                    WriteOffByCar(ref car);
                }
            }          
        }

        public List<Transaction> GetTransactionsByLastMinute()
        {
            DateTime currentTime = DateTime.Now;
            DateTime fromTIme = currentTime.AddMinutes(-1);
            List<Transaction> returned_list = new List<Transaction>();
            foreach (var item in TransactionList)
            {
                if (item.TransactionDataTime >= fromTIme) returned_list.Add(item);
            }
            return returned_list;
        }

        public int GetFreeSpaceOnParking()
        {
            return Settings.ParkingSpace - CarList.Count;
        }

        public void SaveTransactionToFile(object obj = null)
        {
            Console.WriteLine("SaveTransactionToFile");
        }

        public List<Transaction> GetTransactionLog()
        {
            throw new NotImplementedException();
        }

        public void StartDay()
        {
            TimerCallback writteOffCallback = new TimerCallback(WriteOff);
            TimerCallback SaveToFileCallback = new TimerCallback(SaveTransactionToFile);

            Timer timerWritteOff = new Timer(writteOffCallback, null, 0, Settings.Timeout);
            Timer timerSaveToFile = new Timer(SaveToFileCallback, null, 0, 60 * 1000);
        }
    }
}
