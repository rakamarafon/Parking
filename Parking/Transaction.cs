using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    public class Transaction
    {
        public DateTime TransactionDataTime { get; set; }
        public int CarId { get; set; }
        public int MoneyPaid { get; set; }
    }
}
