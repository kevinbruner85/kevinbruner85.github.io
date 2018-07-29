using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ReservationSqlDAL
    {
        private string connectionString;
        //Set connection string
        public ReservationSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
            
        }
        public string SQL_getReservation { get; private set; }
        //Return Available Sites
        public List<Site> ReturnSites(int campGroundSelection, DateTime arrivalDate, DateTime departureDate)
        {
            List<Site> siteList = new List<Site>();
            const string siteSearch = "select top(5) * from site JOIN campground on site.campground_id = campground.campground_id where site.campground_id = @campGroundSelection and site_id not in(select site_id from reservation where @arrivalDate <= to_date and @departureDate >= from_date);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                SqlCommand cmd = new SqlCommand(siteSearch, connection);  
                cmd.CommandText = siteSearch;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@campGroundSelection", campGroundSelection);
                cmd.Parameters.AddWithValue("@arrivalDate", arrivalDate);
                cmd.Parameters.AddWithValue("@departureDate", departureDate);
                cmd.Parameters.AddWithValue("@monthFrom", arrivalDate.Month);
                cmd.Parameters.AddWithValue("@monthTo", departureDate.Month);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Site site = GetSiteFromReader(reader);
                    siteList.Add(site);
                }
                return siteList;
            }
        }
        
        //Insert Reservation
        public bool InsertReservation(int siteID, string resName, DateTime fromDate, DateTime toDate)
        {
            bool wasSuccesful = true;
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                const string SQL_insert = "insert into reservation (site_id, name, from_date, to_date)" +
                                          "values (@siteID, @resName, @fromDate, @toDate);";

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = SQL_insert;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@siteID", siteID);
                cmd.Parameters.AddWithValue("@resName", resName);
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);

                int rowsAffected = cmd.ExecuteNonQuery();
                //int id = (int)cmd.ExecuteScalar();
                if (rowsAffected == 0)
                {
                    wasSuccesful = false;
                }

            }
            return wasSuccesful;
        }

        //Get Reservation Confirmation #
        public int GetReservationID(int siteID, string resName, DateTime fromDate, DateTime toDate)
        {
            int resID = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                const string SQLresIDSearch = "select * FROM reservation WHERE site_id = @siteID and name = @resName and from_date = @fromDate and to_date = @toDate";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLresIDSearch;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@siteID", siteID);
                cmd.Parameters.AddWithValue("@resName", resName);
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Reservation res = new Reservation();

                    resID = Convert.ToInt32(reader["reservation_id"]);
                }
            }
            return resID;
        }

        private Site GetSiteFromReader(SqlDataReader reader)
        {
            Site site = new Site();

            site.SiteNumber = Convert.ToInt32(reader["site_number"]);
            site.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
            site.Accessible = Convert.ToBoolean(reader["accessible"]);
            site.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
            site.Utilities = Convert.ToBoolean(reader["utilities"]);
            site.Id = Convert.ToInt32(reader["site_id"]);

            return site;
        }
    }

    
}
