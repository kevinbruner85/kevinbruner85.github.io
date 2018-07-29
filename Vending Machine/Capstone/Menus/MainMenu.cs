using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.File_Manipulation;
using Capstone.Menus;
using Capstone.Vending_Items;
using System.IO;

namespace Capstone.Menus
{
    public class MainMenu
    {
        /// <summary>
        /// VendingMachine item to be passed into the constructor, 
        /// allowing engagement with the files brought in from VendingMachine class
        /// </summary>
        #region Member Variable
        private VendingMachine _vMachine = null;
        #endregion

        /// <summary>
        /// Passes VendingMachine object into constructor
        /// </summary>
        /// <param name="item"></param>
        #region Constructor
        public MainMenu(VendingMachine item)
        {
            _vMachine = item;
        }
        #endregion

        /// <summary>
        /// Runs vending machine application in Program; writes out menu to console
        /// Allows user to select options:
        /// 1) views an inventory of vending items
        /// 2) moves user to the Purchase submenu through the PurchaseMenu Run() method
        /// Q) closes vending machine application
        /// </summary>
        #region Run Method
        public void Run()
        {
            bool done = false;
            while (!done)
            {
                DisplayMenu();
                char input = Console.ReadKey().KeyChar;
                try
                {
                    if (input == '1')
                    {
                        WriteContents();
                    }
                    else if (input == 'Q' || input == 'q')
                    {
                        done = true;
                    }
                    else if (input == '2')
                    {
                        PurchaseMenu start = new PurchaseMenu(_vMachine);
                        start.Run();
                    }
                    else
                    {
                        Console.WriteLine("Please enter valid instruction.\nPress any key to try again.");
                        Console.ReadKey();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }
        #endregion

        /// <summary>
        /// Methods used to write out various options in menu
        /// </summary>
        #region Methods
        /// <summary>
        /// Lists vending machine options with location, item name, and the number left in stock
        /// </summary>
        public void WriteContents()
        {
            Console.Clear();
            Console.WriteLine("{0,-20}{1,-30}{2,-30}", "Location", "Item", "Number Left");
            Console.WriteLine("------------------------------------------------------------------");
            foreach (KeyValuePair<string, InventoryItem> item in _vMachine._theWholeShebang)
            {
                Console.WriteLine("{0,-20}{1,-30}{2,-30}",$"{item.Key}",$"{item.Value.Item.Name}",$"{item.Value.Quantity}");
            }
            Console.WriteLine("Press any key to return to Main Menu");
            Console.ReadKey();
        }

        /// <summary>
        /// Writes out Main Menu options to the console
        /// </summary>
        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Menu Options");
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(Q) Quit");
            Console.WriteLine($"Current Balance: ${_vMachine.CurrentMoneyInput}");
        }


        #endregion
    }
}
