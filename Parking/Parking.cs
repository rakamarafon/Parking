﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            car.Balance -= price;
            Balance += price;
        }

        public void AddCar(Car car)
        {
            CarList.Add(car);
        }

        public void RemoveCar(int car_id)
        {
            CarList.Remove(GetCarById(car_id));
        }

        public void RefillCarBalance(int car_id, int sum_to_refill)
        {
            Car car = GetCarById(car_id);
            car.Balance += sum_to_refill;
        }

        public void WriteOff()
        {
            for(int i = 0; i < CarList.Count; i++)
            {
                Car car = CarList[i];
                WriteOffByCar(ref car); 
            }
        }
    }
}
