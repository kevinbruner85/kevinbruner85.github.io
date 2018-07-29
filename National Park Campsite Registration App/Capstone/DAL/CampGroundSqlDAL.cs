using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    /// <summary>
    /// Interface for accessing campground table
    /// </summary>
   public class CampGroundSqlDAL
    {
        private string connectionString;

        public CampGroundSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public string SQL_getParks { get; private set; }
       
        /// <summary>
        /// Pull campgrounds associated with park from database
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns>Returns a list of campgrounds associated with selected park from database</returns>
        public List<Campground> GetCampGroundsInPark(int parkId)
        {
            List<Campground> campgrounds = new List<Campground>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                const string sqlParkCommand = "SELECT campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee From CampGround.dbo.campground where park_id = @parkid;";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlParkCommand;
                cmd.Parameters.AddWithValue("@parkid", parkId);
                cmd.Connection = connection;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Campground campground = new Campground();

                    campground.Id = Convert.ToInt32(reader["campground_id"]);
                    campground.Park_Id = Convert.ToInt32(reader["park_id"]);
                    campground.Name = Convert.ToString(reader["name"]);
                    campground.OpenFrom = Convert.ToInt32(reader["open_from_mm"]);
                    campground.OpenTo = Convert.ToInt32(reader["open_to_mm"]);
                    campground.DailyFee = Convert.ToDouble(reader["daily_fee"]);

                    campgrounds.Add(campground);
                }
            }
            return campgrounds;
        }
       
        /// <summary>
        ///Returning a list of all the campgrounds in the database
        /// </summary>
        /// <returns>Returning a list of all the campgrounds in the database</returns>
        public List<Campground> GetAllCampGrounds()
        {
            List<Campground> output = new List<Campground>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                const string SQL_getCampgrounds = "Select * from campground;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL_getCampgrounds;
                cmd.Connection = connection;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Campground CampgroundsList = new Campground();

                    CampgroundsList.Id = Convert.ToInt32(reader["campground_id"]);
                    CampgroundsList.Park_Id = Convert.ToInt32(reader["park_id"]);
                    CampgroundsList.Name = Convert.ToString(reader["name"]);
                    CampgroundsList.OpenFrom = Convert.ToInt32(reader["open_from_mm"]);
                    CampgroundsList.OpenTo = Convert.ToInt32(reader["open_to_mm"]);
                    CampgroundsList.DailyFee = Convert.ToDouble(reader["daily_fee"]);

                    output.Add(CampgroundsList);
                }
                
                return output;
            }
        }
    }
}
