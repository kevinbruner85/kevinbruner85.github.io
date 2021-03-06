﻿using System;
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
    /// 
    /// </summary>
    public class Beverage : VendingItem
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        public Beverage(string name, double price) : base(price, name)
        {

        }
        #endregion

        #region Consume Method
        /// <summary>
        /// 
        /// </summary>
        public override string Consume()
        {
            //CODE_REVIEW_CR
            //Do not write to the console except in CLI classes
            //Return the string instead
            return "Glug Glug, Yum!";
        }
        #endregion  
    }
}
