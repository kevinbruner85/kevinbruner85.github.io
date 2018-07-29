using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ParkSqlDAL
    {
        private string connectionString;
        //Set connection string
        public ParkSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        //Pull all parks from database into a list
        public List<Park> GetAllParks()
        {
            List<Park> output = new List<Park>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                const string SQL_getParks = "Select * from park;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL_getParks;
                cmd.Connection = connection;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Park parkList = new Park();

                    parkList.Park_Id = Convert.ToInt32(reader["park_id"]);
                    parkList.Name = Convert.ToString(reader["name"]);
                    parkList.Location = Convert.ToString(reader["location"]);
                    parkList.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
                    parkList.Area = Convert.ToInt32(reader["area"]);
                    parkList.Visitors = Convert.ToInt32(reader["visitors"]);
                    parkList.Description = Convert.ToString(reader["description"]);


                    output.Add(parkList);

                }
                return output;
            }
        }
    }
}
