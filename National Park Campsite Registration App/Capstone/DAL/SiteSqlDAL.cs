using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SiteSqlDAL
    {
        private string connectionString;
        //Set connection string
        public SiteSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public string SQL_getSites { get; private set; }

        //Pull sites from database using campground ID input
        public List<Site> GetSitesInCampground(int campGroundId)
        {
            List<Site> output = new List<Site>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                const string SQL_getSites = "Select site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities from CampGround.dbo.site where campground_id = @campground_id;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL_getSites;
                cmd.Parameters.AddWithValue("@campground_id", campGroundId);
                cmd.Connection = connection;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Site SitesList = new Site();

                    SitesList.Id =  Convert.ToInt32(reader["site_id"]);
                    SitesList.CampGround_Id = Convert.ToInt32(reader["campground_id"]);
                    SitesList.SiteNumber = Convert.ToInt32(reader["site_number"]);
                    SitesList.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                    SitesList.Accessible = Convert.ToBoolean(reader["accessible"]);
                    SitesList.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                    SitesList.Utilities = Convert.ToBoolean(reader["utilities"]);

                    output.Add(SitesList);
                }

                return output;
            }
        }
             
    }
}
