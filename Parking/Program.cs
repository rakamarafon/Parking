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
            Parking.Instance.StartDay();
            Menu menu = new Menu();
            menu.ShowMainMenu();         
        }
    }
}
