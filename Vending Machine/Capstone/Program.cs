using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.File_Manipulation;
using Capstone.Menus;
using Capstone.Vending_Items;
using System.IO;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {

            VendingMachine vMachine = new VendingMachine();


            MainMenu testing = new MainMenu(vMachine);
            testing.Run();

        }
    }
}
