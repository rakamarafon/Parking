using System;
using System.Collections.Generic;
using System.IO;
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
        private enum ErrorsCod
        {
            EmptyList,
            MinusBalance,
            Success,
            NoCar
        }
        private Parking()
        {
            CarList = new List<Car>();
            TransactionList = new List<Transaction>();
        } 

        private Car GetCarById(int id)
        {
            try
            {
                return CarList.Single<Car>(x => x.CarId == id);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch(InvalidOperationException)
            {
                return null;
            }
        }

        private int CalculatePrice(ref Car car, int price)
        {
            if (car.Balance > 0)
            {
                if (car.Balance < price)
                {
                    int temp = price - car.Balance;
                    return (temp * Settings.Fine) + price;
                }
                return price;
            }
            else
            {
                return price * Settings.Fine;
            }
        }
        private void WriteOffByCar(ref Car car)
        {
            int price = 0;
            foreach (var item in Settings.priceDictionary)
            {
                if (item.Key == car.Type) price = item.Value;
            }
            TransactionList.Add(new Transaction() { TransactionDataTime = DateTime.Now, CarId = car.CarId, MoneyPaid = price});
            car.Balance -= CalculatePrice(ref car, price);
            Balance += price;
        }

        public bool AddCar(Car car)
        {
            if (car != null)
            {
                foreach (var item in CarList)
                {
                    if (item.CarId == car.CarId) return false;
                }
                CarList.Add(car);
                return true;
            }
            return false;
        }

        public int RemoveCar(int car_id)
        {
            if (CarList.Count == 0) return (int)ErrorsCod.EmptyList;
            Car car = GetCarById(car_id);
            if (car == null) return (int)ErrorsCod.NoCar;
            if(car.Balance < 0)
            {
                return (int) ErrorsCod.MinusBalance;
            }
            else
            {
                CarList.Remove(car);
                return (int)ErrorsCod.Success;
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
            using (StreamWriter sw = File.AppendText("Transaction.log"))
            {
                int sum = 0;
                foreach (var item in TransactionList)
                {
                    sum += item.MoneyPaid;
                }
                sw.WriteLine("Date \t \t \t \tSumm ");
                sw.WriteLine(String.Format("{0} \t \t {1}", DateTime.Now, sum));
            }
            TransactionList.Clear();
        }

        public void StartDay()
        {
            TimerCallback writteOffCallback = new TimerCallback(WriteOff);
            TimerCallback SaveToFileCallback = new TimerCallback(SaveTransactionToFile);

            Timer timerWritteOff = new Timer(writteOffCallback, null,1000 * Settings.Timeout,1000 * Settings.Timeout);
            Timer timerSaveToFile = new Timer(SaveToFileCallback, null, 60 * 1000, 60 * 1000);
        }

        public int GetBusySpaceOnParking() => CarList.Count;

        public int TotalParkingProfit() => Balance;

        public int ParkingProfitByLastMinute() => TransactionList.Sum(x => x.MoneyPaid);  
    }
}
