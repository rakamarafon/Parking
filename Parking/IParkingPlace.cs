﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    public interface IParkingPlace
    {
        int AddCar(Car car);
        int RemoveCar(int car_id);
        void RefillCarBalance(int car_id, int sum_to_refill);
        void WriteOff(object obj);
        List<Transaction> GetTransactionsByLastMinute();
        int GetFreeSpaceOnParking();
        void SaveTransactionToFile(object obj);
        List<string> GetTransactionsFromFile();
        void StartDay();
        int GetBusySpaceOnParking();
        int TotalParkingProfit();
        int ParkingProfitByLastMinute();
    }
}
