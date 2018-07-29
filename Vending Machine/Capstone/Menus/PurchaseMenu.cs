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
    public class PurchaseMenu
    {
        private string _logPath = @"C:\workspace\Team\team5-c-week4-pair-exercises\c#-capstone\etc\Log.txt";
        public string _salesReportPath = @"C:\workspace\Team\team5-c-week4-pair-exercises\c#-capstone\etc\SalesReport.txt";

        #region Member Variable
        private VendingMachine _vMachine = null;
        #endregion

        #region Properties
        List<InventoryItem> PurchasedInventory { get; set; } = new List<InventoryItem>();
        VendingItem HoldingPlace { get; set; }
        List <VendingItem> PurchasedVending { get; set; } = new List<VendingItem>();
        FileLog ReadFile { get; set; } = new FileLog();
        List<int> Input { get; } = new List<int>();
        List<double> Output { get; } = new List<double>();
        public double CurrentSales { get; set; } = 0;
        public List<string> PurchasedNames { get; set; } = new List<string>();
        public int MoneyInput { get; private set; }
        #endregion

        #region VendingMachine Constructor
        public PurchaseMenu(VendingMachine item)
        {
            _vMachine = item;
        }
        #endregion
        
        #region Run Method
        public void Run()
        {
            bool done = false;
            while (!done)
            {
                try
                {
                    Display();
                    char input = Console.ReadKey().KeyChar;

                    if (input == '1')
                    {
                        FeedMoney();
                        Input.Add(MoneyInput);
                        Output.Add(_vMachine.CurrentMoneyInput);
                        ReadFile.WriteLog(_logPath, "FEED MONEY", MoneyInput, _vMachine.CurrentMoneyInput);
                    }
                    else if (input == '2')
                    {
                        bool finished = false;
                        while (!finished)
                        {
                            Console.Clear();
                            WriteVMachine();
                            string locationInput = Console.ReadLine();
                            if (_vMachine._theWholeShebang.ContainsKey(locationInput) &&
                                _vMachine._theWholeShebang[locationInput].Quantity > 0 &&
                                _vMachine.CurrentMoneyInput >= _vMachine._theWholeShebang[locationInput].Item.Price)
                            {
                                BuyItem(locationInput);
                            }
                            else if (_vMachine._theWholeShebang.ContainsKey(locationInput) &&
                                    _vMachine._theWholeShebang[locationInput].Quantity < 1)
                            {
                                WriteSoldOut();
                            }
                            else if (_vMachine._theWholeShebang.ContainsKey(locationInput) &&
                                    _vMachine.CurrentMoneyInput < _vMachine._theWholeShebang[locationInput].Item.Price)
                            {
                                WriteNotEnoughFunds();
                                finished = true;
                            }
                            else if (locationInput == "m" || locationInput == "M")
                            {
                                finished = true;
                            }
                            else
                            {
                                WriteWrongLocation();
                            }
                        }
                    }
                    else if (input == '3')
                    {
                        FinishTransaction();
                        done = true;
                    }
                    else
                    {
                        WriteWrongLocation();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                #endregion
            }
        }

        #region Methods
        /// <summary>
        /// Writes out purchase submenu, giving options for user selection
        /// 1) Takes user to Feed Money menu
        /// 2) Takes user to Select Product menu
        /// 3) Finishes Transaction, returning user's change, consume() for anything purchased, 
        /// and leading back to the main menu
        /// Also writes out current balance in vending machine
        /// </summary>
        public void Display()
        {
            Console.Clear();
            Console.WriteLine("Purchasing Options");
            Console.WriteLine("(1) Feed Money");
            Console.WriteLine("(2) Select Product");
            Console.WriteLine("(3) Finish Transaction and Return to Main Menu");
            Console.WriteLine($"Current Money Provided: ${_vMachine.CurrentMoneyInput}");
        }

        /// <summary>
        /// Upon purchase:
        /// Item quantity is subtracted by one
        /// Item price is subtracted from Current Balance
        /// Price, name, and quantity are added to PurchasedInventory list
        /// WriteLog() method invoked
        /// Item price is added to current sales
        /// Item name is added to PurchasedNames list
        /// </summary>
        /// <param name="locationInput"></param>
        public void BuyItem(string locationInput)
        {
            _vMachine._theWholeShebang[locationInput].Quantity = _vMachine._theWholeShebang[locationInput].Quantity - 1;
            _vMachine.CurrentMoneyInput = _vMachine.CurrentMoneyInput - _vMachine._theWholeShebang[locationInput].Item.Price;
            PurchasedInventory.Add(_vMachine._theWholeShebang[locationInput]);
            ReadFile.WriteLog(_logPath, _vMachine._theWholeShebang[locationInput].Item.Name + locationInput, _vMachine.CurrentMoneyInput,
                              _vMachine.CurrentMoneyInput - _vMachine._theWholeShebang[locationInput].Item.Price);
            CurrentSales += _vMachine._theWholeShebang[locationInput].Item.Price;
            PurchasedNames.Add(_vMachine._theWholeShebang[locationInput].Item.Name);
        }

        /// <summary>
        /// Allows user to add money to current balance in vending machine
        /// Must be a positive number between 1 and 133 (every item in fully stocked machine is $132.50)
        /// </summary>
        public void FeedMoney()
        {
            bool finished = false;
            while (!finished)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the MONEY ZONE\nFeed me desired amount in $1, $5, or $10 increments");
                Console.WriteLine($"Maximum value is $133. Current Money Provided: ${ _vMachine.CurrentMoneyInput}");
                string moneyString = Console.ReadLine();
                int.TryParse(moneyString, out int moneyInput);
                MoneyInput = moneyInput;
                _vMachine.InputMoney(MoneyInput);
                if (MoneyInput > 0 && _vMachine.CurrentMoneyInput <= 133)
                {
                    Console.Clear();
                    finished = true;
                }
                else if (_vMachine.CurrentMoneyInput > 133)
                {
                    Console.Clear();
                    _vMachine.CurrentMoneyInput -= moneyInput;
                    Console.WriteLine("You have exceeded the maximum value.\nPress any key to return to Purchase Menu");
                    Console.ReadKey();
                    finished = true;
                }
                else
                {
                    Console.WriteLine("Please enter an appropriate amount in $1, $5, or $10 increments");
                    Console.WriteLine("Press any key to try again");
                    Console.ReadKey();
                    finished = false;
                }
            }
        }

        /// <summary>
        /// Used in FinishTransaction() when converting items from InventoryItems to VendingItems
        /// </summary>
        public void GetPurchasedItems()
        {
            foreach (InventoryItem item in PurchasedInventory)
            {
                HoldingPlace = item.Item;
                PurchasedVending.Add(HoldingPlace);
            }
        }

        /// <summary>
        /// Clears screen
        /// Writes change given to Log
        /// Change given is written to screen through Change Class Constructor
        /// CurrentMoneyInput is zeroed
        /// GetPurchasedItems() is called
        /// Purchased items are Consume()d
        /// Sales Report is read in, updated, and written out
        /// User is prompted to press any key, returning them to the main menu
        /// </summary>
        public void FinishTransaction()
        {
            Console.Clear();
            ReadFile.WriteLog(_logPath, "GIVE CHANGE", _vMachine.CurrentMoneyInput, 0.00);
            Change makeChange = new Change(_vMachine.CurrentMoneyInput);
            Console.WriteLine(makeChange);
            _vMachine.CurrentMoneyInput = 0;
            this.GetPurchasedItems();
            foreach (VendingItem item in PurchasedVending)
            {
                Console.WriteLine(item.Consume());
            }
            ReadFile.ReadSales(_salesReportPath, _vMachine._theWholeShebang.Values.ToList());
            foreach (string item in PurchasedNames)
            {
                ReadFile.WriteSales(item, _salesReportPath, CurrentSales);
            }
            Console.WriteLine("\nPress any key to return to Main Menu");
            Console.ReadKey();
        }
        /// <summary>
        /// Writes out vending machine items, price, and quantity left to console
        /// Prompts user for location input. "M" to return to purchase menu
        /// </summary>
        public void WriteVMachine()
        {
            Console.WriteLine("{0, -5}{1, -20}{2, -10}{3, 0}", "\n", "Snack", "Price", "Number in Stock");
            Console.WriteLine("--------------------------------------------------");
            foreach (KeyValuePair<string, InventoryItem> item in _vMachine._theWholeShebang)
            {
                if (item.Key.Contains("A1") || item.Key.Contains("A2") || item.Key.Contains("A3") || item.Key.Contains("A4"))
                {
                    Console.WriteLine("{0, -5}{1, -20}{2, -10}{3, 0}", $"{item.Key}:", $"{item.Value.Item.Name}", $"${item.Value.Item.Price}", $"{item.Value.Quantity}");
                }
                else if (item.Key.Contains("B1") || item.Key.Contains("B2") || item.Key.Contains("B3") || item.Key.Contains("B4"))
                {
                    Console.WriteLine("{0, -5}{1, -20}{2, -10}{3, 0}", $"{item.Key}:", $"{item.Value.Item.Name}", $"${item.Value.Item.Price}", $"{item.Value.Quantity}");
                }
                else if (item.Key.Contains("C1") || item.Key.Contains("C2") || item.Key.Contains("C3") || item.Key.Contains("C4"))
                {
                    Console.WriteLine("{0, -5}{1, -20}{2, -10}{3, 0}", $"{item.Key}:", $"{item.Value.Item.Name}", $"${item.Value.Item.Price}", $"{item.Value.Quantity}");
                }
                else if (item.Key.Contains("D1") || item.Key.Contains("D2") || item.Key.Contains("D3") || item.Key.Contains("D4"))
                {
                    Console.WriteLine("{0, -5}{1, -20}{2, -10}{3, 0}", $"{item.Key}:", $"{item.Value.Item.Name}", $"${item.Value.Item.Price}", $"{item.Value.Quantity}");
                }
            }
            Console.WriteLine($"\nCurrent Money Provided: ${_vMachine.CurrentMoneyInput}");
            Console.Write("Please select location or Press (M) to go back: ");
        }

        /// <summary>
        /// Writes sold out message
        /// </summary>
        public void WriteSoldOut()
        {
            Console.Clear();
            Console.WriteLine("Item is sold out!\nSelect any key to select an alternative");
            Console.ReadKey();
        }

        /// <summary>
        /// Writes out message to user alerting them the price of their desired item is greater than their current balance
        /// </summary>
        public void WriteNotEnoughFunds()
        {
            Console.WriteLine("Not enough funds in current balance\nPress any key to return to purchase menu");
            Console.ReadKey();
        }

        /// <summary>
        /// Writes message alerting user they made an invalid input
        /// </summary>
        public void WriteWrongLocation()
        {
            Console.WriteLine("Invalid input\nPress any key to try again");
            Console.ReadKey();
        }
        #endregion
    }
}
