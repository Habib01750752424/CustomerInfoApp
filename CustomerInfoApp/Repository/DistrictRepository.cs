using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerInfoApp.Model;

namespace CustomerInfoApp.Repository
{
    public class DistrictRepository
    {
        string connectionString = @"Server = HABIB; Database = CustomerInfo; Integrated Security = true";

        public List<District> LoadDistrict()
        {
            List<District> districts = new List<District>();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string query = "SELECT ID, Name FROM Districts";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            //DataTable dataTable = new DataTable();
            //int isFill = sqlDataAdapter.Fill(dataTable);
            //return dataTable;

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                District district = new District();
                district.ID = Convert.ToInt32(sqlDataReader["ID"]);
                district.Name = sqlDataReader["Name"].ToString();

                districts.Add(district);
            }
            sqlConnection.Close();

            return districts;
        }
    }
}
