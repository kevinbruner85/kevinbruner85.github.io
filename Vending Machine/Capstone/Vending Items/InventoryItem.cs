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
    public class InventoryItem
    {
        #region Properties
        public VendingItem Item { get; }
        public int Quantity { get; set; } = 5;
        #endregion

        #region Constructor
        public InventoryItem(VendingItem item)
        {
            Item = item;
        }
        #endregion

    }
}
