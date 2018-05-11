using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    public class Car
    {
        public int CarId { get; set; }
        private int _balance;
        public int Balance
        {
            get
            {
                lock(syncBalance)
                {
                    return _balance;
                }
            }
             set
            {
                lock(syncBalance)
                {
                    _balance = value;
                }
            }
        }
        public CarType Type { get; set; }

        private object syncBalance = new object();
    }
}
