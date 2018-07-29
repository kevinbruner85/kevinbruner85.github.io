using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;
using System.Data.SqlClient;

namespace Capstone

{
    public class ProjectCLI
    {       
        const string DatabaseConnection = @"Data Source =.\SQLEXPRESS;Initial Catalog = CampGround; Integrated Security = True";
        const string Command_Quit = "q";
        public Dictionary<int, Park> _parkDictionary = new Dictionary<int, Park>();
        public Dictionary<int, Campground> _campgroundDictionary = new Dictionary<int, Campground>();
        //Main Run Method
        public void RunCLI()
        {
            bool done = false;
            while (!done)
                try
                {
                    {
                        Console.Clear();
                        PrintMenu();
                        string tempuserInput = Console.ReadLine();
                        if (tempuserInput == "q")
                        {
                            done = true;
                        }
                        else if (_parkDictionary.ContainsKey(int.Parse(tempuserInput)))
                        {
                            var park = _parkDictionary[int.Parse(tempuserInput)];
                            ParkSubMenu(park);
                        }
                        else
                        {
                            Console.WriteLine("Please enter valid input");
                        }
                    }
                }
                catch(Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    Console.WriteLine("Invalid input. Press any key to continue");
                    Console.ReadKey();
                }
        }

    
        ///Print Main Menu
        public void PrintMenu()
        {
            Console.Title = "ASCII Art";
            string title = @"         
_  _      _   _               _   ___          _   
| \| |__ _| |_(_)___ _ _  __ _| | | _ \__ _ _ _| |__
| .` / _` |  _| / _ \ ' \/ _` | | |  _/ _` | '_| / /
|_|\_\__,_|\__|_\___/_||_\__,_|_| |_| \__,_|_| |_\_\

       ,,,.   ,@@@@@@/@@,  .oo8888o.
    ,&%%&%&&%,@@@@@/@@@@@@,8888\88/8o
   ,%&\%&&%&&%,@@@\@@@/@@@88\88888/88'
   %&&%&%&/%&&%@@\@@/ /@@@88888\88888'
   %&&%/ %&%%&&@@\ V /@@' `88\8 `/88'
   `&%\ ` /%&'    |.|        \ '|8'
       |o|        | |         | |
       |.|        | |         | |
 \\/ ._\//_/__/  ,\_//__\\/.  \_//__/_
___                          _   _            ___         _             
| _ \___ ___ ___ _ ___ ____ _| |_(_)___ _ _   / __|_  _ __| |_ ___ _ __  
|   / -_|_-</ -_) '_\ V / _` |  _| / _ \ ' \  \__ \ || (_-<  _/ -_) '  \ 
|_|_\___/__/\___|_|  \_/\__,_|\__|_\___/_||_| |___/\_, /__/\__\___|_|_|_|
                                                    |__/ ";
            Console.WriteLine(title);
            Console.WriteLine("Select a park for further details...");
            PrintParks();
            Console.WriteLine("q) Quit");
        }
        ///Park Submenu
        public void ParkSubMenu(Park park)
        {
            bool isDone = false;
            while (!isDone)
            {
                try
                {
                    var words = park.Description.Split(' ');
                    var lines = words.Skip(1).Aggregate(words.Take(1).ToList(), (l, w) =>
                    {
                        if (l.Last().Length + w.Length >= 80)
                            l.Add(w);
                        else
                            l[l.Count - 1] += " " + w;
                        return l;
                    });
                    Console.Clear();
                    Console.WriteLine(park.Name + " National Park");
                    Console.WriteLine($"Location: {park.Location}");
                    Console.WriteLine($"Established: {park.EstablishDate}");
                    Console.WriteLine($"Area: {park.Area}");
                    Console.WriteLine($"Annual visitors: {park.Visitors}");
                    Console.WriteLine();
                    foreach(var item in lines)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Please Select a Command");
                    Console.WriteLine();
                    Console.WriteLine($"1) View Campgrounds");
                    Console.WriteLine($"2) Return to Previous Screen");

                    string tempuserInput = Console.ReadLine();
                    int userInput = int.Parse(tempuserInput);
                    if (userInput == 2)
                    {
                        isDone = true;
                    }
                    else if (userInput == 1)
                    {
                        SubSubMenu(park.Park_Id);
                    }
                    isDone = true;
                }
                catch(Exception ex)
                {
                    
                    Console.WriteLine("Invalid input. Press any key to continue");
                    Console.ReadKey();
                }
            }
        }

        ///Print All Parks
        public void PrintParks()
        {
            ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
            List<Park> parks = dal.GetAllParks();
            _parkDictionary.Clear();
            
            for (int i = 1; i <= parks.Count; i++)
            {
                Park park = parks[i-1];
                Console.WriteLine($"{i}) {parks[i-1].Name} National Park");
                _parkDictionary.Add(i, park);
            }
        }

        //Print CampGrounds using ParkID
        public void PrintCampGrounds(int parkId)
        {
            CampGroundSqlDAL dal = new CampGroundSqlDAL(DatabaseConnection);
            List<Campground> campgrounds = dal.GetCampGroundsInPark(parkId);
            _campgroundDictionary.Clear();
            int options = 0;
            for (int i = 1; i <= campgrounds.Count; i++)
            {
                options++;
                Campground campground = campgrounds[i - 1];
                Console.WriteLine($"{options, -5}{campgrounds[i - 1].Name,-36}{campgrounds[i - 1].OpenFromMonthstr,-20}{campgrounds[i - 1].OpenToMonthstr,-20}{campgrounds[i - 1].DailyFee.ToString("c"),-20}");
                _campgroundDictionary.Add(i, campground);
            }
        }
       
        ///Reservation SubMENU 2
        public void DateRangeSubMenu(int parkID)
        {
            Console.Clear();
            Console.WriteLine("{0, -5}{1, -36}{2, -20}{3, -20}{4, -20}", "ID", "Name", "Open", "Close", "Daily Fee");
            PrintCampGrounds(parkID);
            CampGroundSqlDAL dal = new CampGroundSqlDAL(DatabaseConnection);
            
            Console.WriteLine();
            
            Console.WriteLine($"Which Campground? Select valid ID or choose from the menu options below:");
            Console.WriteLine();
            Console.WriteLine("m) return to main menu");
            Console.WriteLine("p) return to previous screen");
            string campgroundSelection = Console.ReadLine();
            //Console.WriteLine("");
            Console.WriteLine();
           
            
            if(campgroundSelection == "m")
            {
                RunCLI();
            }
            if(campgroundSelection == "p")
            {
                SubSubMenu(parkID);
            }
            int selection = int.Parse(campgroundSelection);
            if (selection == 0)
            {
                RunCLI();
            }
            
            if (_campgroundDictionary.ContainsKey(selection))
            {
                ReservationSubMenu(selection);
            }
        }

        /// <summary>
        /// Captures reservation inputs and stores them in the database
        /// </summary>
        /// <param name="selection"></param>
        public void ReservationSubMenu(int selection)
        {
            try
            {
                int resID = 0;
                Campground campground = _campgroundDictionary[selection];

                Console.Write("What is your arrival date? (yyyy-mm-dd) ");
                DateTime arrivalDate = DateTime.Parse(Console.ReadLine());
                Console.Write("What is your departure date? (yyyy-mm-dd) ");
                DateTime departureDate = DateTime.Parse(Console.ReadLine());
                SiteSqlDAL sitedal = new SiteSqlDAL(DatabaseConnection);
                var listOfSites = sitedal.GetSitesInCampground(campground.Id);
                double dailyFee = campground.DailyFee;
                double totalDays = (departureDate - arrivalDate).TotalDays;
                double tempTotalCost = dailyFee * totalDays;
                string totalCost = tempTotalCost.ToString("c");

                ReservationSqlDAL resDal = new ReservationSqlDAL(DatabaseConnection);

                List<int> siteIDs = new List<int>();
                for (int i = 0; i < listOfSites.Count; i++)
                {
                    siteIDs.Add(listOfSites[i].Id);
                }
                int campID = campground.Id;
                List<Site> availableSites = resDal.ReturnSites(campID, arrivalDate, departureDate);
                Console.WriteLine();
                Console.WriteLine("Results matching your search criteria");
                Console.WriteLine();
                Console.WriteLine("{0, -5}{1, -20}{2, -20}{3, -20}{4, -20}", "ID", "Max Occupancy", "Accesibility", "Max RV Length", "Utilities");
                Console.WriteLine();
                int options = 0;
                foreach (var item in availableSites)
                {
                    options++;
                    Console.WriteLine($"{options,-5}{item.MaxOccupancy,-20}{item.Accessible,-20}{item.MaxRVLength,-20}{item.Utilities,-20}");
                }

                Console.WriteLine("What site should be reserved? (Please input valid ID) ");
                string tempInput = Console.ReadLine();
                int tempIndex = int.Parse(tempInput);
                Site siteRes = availableSites[tempIndex - 1];
                int siteID = siteRes.Id;
                Console.WriteLine();
                Console.WriteLine("What name should the reservation be made under? ");
                string resName = Console.ReadLine();

                bool wasSuccesful = resDal.InsertReservation(siteID, resName, arrivalDate, departureDate);
                if (wasSuccesful)
                {
                    resID = resDal.GetReservationID(siteID, resName, arrivalDate, departureDate);
                    Console.WriteLine("Your total cost will be: " + totalCost);
                    Console.WriteLine("Your reservation number is: " + resID);
                    Console.WriteLine("Thank you!");
                    Console.ReadKey();
                }
            }
            catch
            {
                Console.WriteLine("Invalid input");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
        //Reservation SUBMENU 1
        public void SubSubMenu(int parkId)
        {
            bool isDone = false;
            while (!isDone)
            { 
                try
                {
                    Console.Clear();
                    Console.WriteLine("{0, -5}{1, -36}{2, -20}{3, -20}{4, -20}", "ID", "Name", "Open", "Close", "Daily Fee");
                    PrintCampGrounds(parkId);
                    Console.WriteLine();
                    Console.WriteLine("Select a command:");
                    Console.WriteLine();
                    Console.WriteLine("   " + "1) Search for Available Reservation");
                    Console.WriteLine("   " + "2) Return to Previous Screen");
                    string tempuInput = Console.ReadLine();
                    int uInput = int.Parse(tempuInput);
                    if (uInput == 2)
                    {
                        isDone = true;
                    }
                    else if (uInput == 1)
                    {
                        DateRangeSubMenu(parkId);               
                    }
                  
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Invalid input. Press any key to continue...");
                    Console.ReadKey();
                }
            }

        }  
            
        
    }
}
