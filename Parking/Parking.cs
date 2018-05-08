using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    public class Parking
    {
        public List<Car> CarList { get; set; }
        public List<Transaction> TransactionList { get; set; }
        public int Balance { get; set; }
    }
}
