using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campground
    {
        private Dictionary<int, string> _months = new Dictionary<int, string>()
        {
            {1, "January" },
            {2, "February" },
            {3, "March" },
            {4, "April" },
            {5, "May" },
            {6, "June" },
            {7, "July" },
            {8, "August" },
            {9, "September" },
            {10, "October" },
            {11, "November" },
            {12, "December" },
        };

        public int Id { get; set; }
        public int Park_Id { get; set; }
        public string Name { get; set; }
        public int OpenFrom { get; set; }
        public int OpenTo { get; set; }
        public double DailyFee { get; set; }

        public string OpenFromMonthstr
        {
            get
            {
                return _months[OpenFrom];
            }
        }

        public string OpenToMonthstr
        {
            get
            {
                return _months[OpenTo];
            }
        }
    }
}
