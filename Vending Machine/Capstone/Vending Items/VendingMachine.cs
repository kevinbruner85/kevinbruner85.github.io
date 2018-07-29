using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.File_Manipulation;
using Capstone.Menus;
using Capstone.Vending_Items;
using System.IO;

namespace Capstone.Vending_Items
{
    /// <summary>
    /// Reads in file of vending machine items, allocating them to a dictionary for manipulation
    /// </summary>
    public class VendingMachine
    {
        #region Properties
        /// <summary>
        /// File with vending items being read in
        /// </summary>
        public string FilePath { get; } = @"C:\workspace\Team\team5-c-week4-pair-exercises\c#-capstone\etc\vendingmachine.csv";

        /// <summary>
        /// CurrentMoneyInput holds the current balance input into the vending machine
        /// _theWholeShebang holds a location key and an item value, specifying an item name, price, and quantity
        /// </summary>
        public double CurrentMoneyInput { get; set; } = 0;
        public Dictionary<string, InventoryItem> _theWholeShebang { get; } = new Dictionary<string, InventoryItem>();
        #endregion

        /// <summary>
        /// Implements the ReadFile method when new instance of VendingMachine is called
        /// </summary>
        #region  Constructor               
        public VendingMachine()
        {
            ReadFile();
        }
        #endregion

        
        #region Methods
        /// <summary>
        /// Reads in file of vending machine items
        /// Parses each line by '|' and places each element in dictionary _theWholeShebang
        /// Each key is a letter/number location and each value holds the elements price, name, and quantity(5)
        /// </summary>
        public void ReadFile()
        {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        List<string> lineList = line.Split('|').ToList();

                        if (lineList[3] == VendingItem.Chip)
                        {
                            double.TryParse(lineList[2], out double price);
                            Chips item = new Chips(lineList[1], price);
                            InventoryItem inventory = new InventoryItem(item);
                            _theWholeShebang.Add(lineList[0], inventory);
                        }
                        else if (lineList[3] == VendingItem.Candy)
                        {
                            double.TryParse(lineList[2], out double price);
                            Candy item = new Candy(lineList[1], price);
                            InventoryItem inventory = new InventoryItem(item);
                            _theWholeShebang.Add(lineList[0], inventory);
                        }
                        else if (lineList[3] == VendingItem.Beverage)
                        {
                            double.TryParse(lineList[2], out double price);
                            Beverage item = new Beverage(lineList[1], price);
                            InventoryItem inventory = new InventoryItem(item);
                            _theWholeShebang.Add(lineList[0], inventory);
                        }
                        else if (lineList[3] == VendingItem.Gum)
                        {
                            double.TryParse(lineList[2], out double price);
                            Gum item = new Gum(lineList[1], price);
                            InventoryItem inventory = new InventoryItem(item);
                            _theWholeShebang.Add(lineList[0], inventory);
                        }
                    }
                }
        }

        /// <summary>
        /// Takes in an int and adjusts the balance of money held by the vending machine
        /// </summary>
        /// <param name="moneyInput"></param>
        /// <returns>int</returns>
        public int InputMoney(int moneyInput)
        {
            int result = 0;
            CurrentMoneyInput += moneyInput;
            return result;
        }


        #endregion
    }
}
