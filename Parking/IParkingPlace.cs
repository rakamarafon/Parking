using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    public interface IParkingPlace
    {
        void AddCar(Car car);
        void RemoveCar(int car_id);
        void RefillCarBalance(int car_id, int sum_to_refill);
        void WriteOff();
    }
}
