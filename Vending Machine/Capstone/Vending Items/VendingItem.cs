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
    public abstract class VendingItem 
    {
        #region Constants
        public const string Candy = "Candy";
        public const string Gum = "Gum";
        public const string Beverage = "Drink";
        public const string Chip = "Chip";
        #endregion
        
        #region Properties
        public string Name { get; private set; }
        public double Price { get; private set; }
        #endregion

        public VendingItem(double price, string name)
        {
            Price = price;
            Name = name;
        }

        #region Methods
        public abstract string Consume();
        #endregion




    }
}
