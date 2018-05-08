using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    public static class Settings
    {
        public static int Timeout { get; set; }

        static Dictionary<CarType, int> priceDictionary;
        public static int ParkingSpace { get; set; }
        public static int Fine { get; set; }

        static Settings()
        {
            Timeout = 3;

            priceDictionary = new Dictionary<CarType, int>();
            priceDictionary.Add(CarType.Passenger, 3);
            priceDictionary.Add(CarType.Truck, 5);
            priceDictionary.Add(CarType.Bus, 2);
            priceDictionary.Add(CarType.Motorcycle, 1);

            ParkingSpace = 10;

            Fine = 2;
        }
    }
}
