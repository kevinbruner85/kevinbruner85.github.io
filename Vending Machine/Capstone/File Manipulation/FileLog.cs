using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.File_Manipulation;
using Capstone.Menus;
using Capstone.Vending_Items;
using System.IO;

namespace Capstone.File_Manipulation
{
    public class FileLog
    {
        public Dictionary<string, int> SalesReport { get; set; } = new Dictionary<string, int>();
                
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destinationPath"></param>
        /// <param name="action"></param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public void WriteLog(string destinationPath, string action, double input, double output)
        {
            using (TextWriter sR = new StreamWriter(destinationPath, true))
            {
                //CODE_REVIEW_CR
                //Use padding instead of tabs
                sR.WriteLine($"{DateTime.Now.ToString()}\t\t{action}\t\t${input}\t\t${output}");
            }
        }

        public void ReadSales(string destinationPath, List<InventoryItem> inventoryList)
        {
            bool fileExists = File.Exists(destinationPath);
            if (fileExists)
            {
                using (StreamReader sR = new StreamReader(destinationPath))
                {
                    while (!sR.EndOfStream)
                    {
                        string line = sR.ReadLine();
                        string[] lineArray = line.Split('|');
                        if (lineArray.Length > 1)
                        {
                            string quantityString = lineArray[1];
                            int quantity = int.Parse(quantityString);
                            SalesReport.Add(lineArray[0], quantity);
                        }
                    }
                }
            }
            else
            {
                foreach(var item in inventoryList)
                {
                    SalesReport.Add(item.Item.Name, 0);
                }
            }
        }

        public void WriteSales(string name, string destinationPath, double currentSales)
        {
            if(SalesReport.ContainsKey(name))
            {
                SalesReport[name]++;
            }
            else
            {
                SalesReport.Add(name, 1);
            }
            
            using (StreamWriter sw = new StreamWriter(destinationPath))
            {
                foreach (KeyValuePair<string, int> item in SalesReport)
                {
                    sw.WriteLine($"{item.Key}|{item.Value}");
                }
                sw.WriteLine();

                double totalSales = SalesReport.ElementAt(SalesReport.Count - 1).Value;
                totalSales = totalSales + currentSales;
                sw.WriteLine($"**TOTAL SALES ** ${totalSales}");
            } 
        }
    }
}
