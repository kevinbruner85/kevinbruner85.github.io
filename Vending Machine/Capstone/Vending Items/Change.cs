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
    public class Change
    {
        #region Properties
        private const float Quarter = 0.25F;
        private const float Dime = 0.10F;
        private const float Nickel = 0.05F;
        #endregion

        public int NumQuarters { get; private set; } = 0;
        public int NumDimes { get; private set; } = 0;
        public int NumNickels { get; private set; } = 0;
        public double Total
        {
            get
            {
                return NumQuarters * Quarter + NumDimes * Dime + NumNickels * Nickel;
            }
        }

        public Change(double currentMoneyInput)
        {
            ChangeReturned(currentMoneyInput);
        }

        private void ChangeReturned(double currentMoneyInput)
        {
            while (currentMoneyInput != 0)
            {
                if(currentMoneyInput >= Quarter)
                {
                    NumQuarters++;
                    currentMoneyInput -= Quarter;
                }
                else if(currentMoneyInput >= Dime)
                {
                    NumDimes++;
                    currentMoneyInput -= Dime;
                }
                else if(currentMoneyInput >= Nickel)
                {
                    NumNickels++;
                    currentMoneyInput -= Nickel;
                }
                else
                {
                    currentMoneyInput = 0;
                }
            }
        }

        public override string ToString()
        {
            return $"Your change is {NumQuarters} Quarters, {NumDimes} Dimes, and {NumNickels} Nickels, " +
                    $"for a total of {Total.ToString("c")}";
        }
    }
}
