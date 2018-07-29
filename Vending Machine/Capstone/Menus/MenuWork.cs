using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Vending_Items;

namespace Capstone.Menus
{
    class MenuWork
    {
        #region Member Variable
        private VendingMachine _vMachine = null;
        #endregion

        public int MoneyInput { get; private set; }

        #region Constructor
        public MenuWork(VendingMachine item)
        {
            _vMachine = item;
        }
        #endregion

        #region FeedMoneyMethod
        
        #endregion

        #region WriteVendingMachineMethod

        
        //public void writeVMachine()
        //{
        //    Console.WriteLine("{0, -5}{1, -20}{2, -10}{3, 0}", "\n", "Snack", "Price", "Number in Stock");
        //    Console.WriteLine("--------------------------------------------------");
        //    foreach (KeyValuePair<string, InventoryItem> item in _vMachine._theWholeShebang)
        //    {
        //        if (item.Key.Contains("A1") || item.Key.Contains("A2") || item.Key.Contains("A3") || item.Key.Contains("A4"))
        //        {
        //            Console.WriteLine("{0, -5}{1, -20}{2, -10}{3, 0}", $"{item.Key}:", $"{item.Value.Item.Name}", $"${item.Value.Item.Price}", $"{item.Value.Quantity}");
        //        }
        //        else if (item.Key.Contains("B1") || item.Key.Contains("B2") || item.Key.Contains("B3") || item.Key.Contains("B4"))
        //        {
        //            Console.WriteLine("{0, -5}{1, -20}{2, -10}{3, 0}", $"{item.Key}:", $"{item.Value.Item.Name}", $"${item.Value.Item.Price}", $"{item.Value.Quantity}");
        //        }
        //        else if (item.Key.Contains("C1") || item.Key.Contains("C2") || item.Key.Contains("C3") || item.Key.Contains("C4"))
        //        {
        //            Console.WriteLine("{0, -5}{1, -20}{2, -10}{3, 0}", $"{item.Key}:", $"{item.Value.Item.Name}", $"${item.Value.Item.Price}", $"{item.Value.Quantity}");
        //        }
        //        else if (item.Key.Contains("D1") || item.Key.Contains("D2") || item.Key.Contains("D3") || item.Key.Contains("D4"))
        //        {
        //            Console.WriteLine("{0, -5}{1, -20}{2, -10}{3, 0}", $"{item.Key}:", $"{item.Value.Item.Name}", $"${item.Value.Item.Price}", $"{item.Value.Quantity}");
        //        }
        //    }
        //}
        #endregion

       
    }
}
