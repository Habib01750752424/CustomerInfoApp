using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CustomerInfoApp.Model;

namespace CustomerInfoApp.Repository
{
    public class CustomerRepository
    {
        string connectionString = @"Server = HABIB; Database = CustomerInfo; Integrated Security = true";

        public bool Add(Customer customer)
        {
            bool isAdd = false;
            List<District> districts = new List<District>();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string query = "INSERT INTO Customers(Code,Name,Address,Contact,DistrictId)" +
                "VALUES('" + customer.Code + "','" + customer.Name + "','" + customer.Address + "','" + customer.Contact + "'," + customer.DistrictId + ")";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            int isExecute = sqlCommand.ExecuteNonQuery();

            if (isExecute > 0)
            {
                isAdd = true;
            }
            return isAdd;
        }

        public bool CheckIfNumeric(string input)
        {
            return input.IsNumeric();
        }

        public bool IsNameExist(Customer customerName)
        {
            bool existName = false;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string commandString = @"SELECT Name FROM Customers WHERE Name = '" + customerName.Name + "'";
            SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            int isFill = sqlDataAdapter.Fill(dataTable);


            if (isFill > 0)
            {
                existName = true;
            }

            return existName;
        }

        public bool IsContactExist(Customer contact)
        {
            bool existContact = false;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string commandString = @"SELECT Contact FROM Customers WHERE Contact = '" + contact.Contact + "'";
            SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            int isFill = sqlDataAdapter.Fill(dataTable);


            if (isFill > 0)
            {
                existContact = true;
            }

            return existContact;
        }

        public DataTable Display()
        {
            //List<Customer> customers = new List<Customer>();

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string query = "SELECT c.Id,Code,c.Name,d.Name AS District,Address,Contact FROM Customers AS c " +
                "LEFT JOIN Districts AS d ON d.ID = C.DistrictId";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            int isFill = sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;

            //SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            //while (sqlDataReader.Read())
            //{
            //    Customer customer = new Customer();
            //    customer.Id = Convert.ToInt32(sqlDataReader["Id"]);
            //    customer.Name = sqlDataReader["Name"].ToString();
            //    customer.Address = sqlDataReader["Address"].ToString();
            //    customer.Contact = sqlDataReader["Contact"].ToString();
            //    customer.DistrictId = Convert.ToInt32(sqlDataReader["DistrictId"]);

            //    customers.Add(customer);
            //}
            //sqlConnection.Close();
        }

        public bool Update(Customer customer, int id)
        {
            bool isUpdate = false;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string query = "UPDATE Customers SET Code = '"+customer.Code+"', Name = '" + customer.Name + "'" +
                ",Address = '" + customer.Address + "',Contact='" + customer.Contact + "',DistrictId = " + customer.DistrictId+"" +
                "WHERE Id = " + id + "";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            int isExecute = sqlCommand.ExecuteNonQuery();

            if (isExecute > 0)
            {
                isUpdate = true;
            }
            sqlConnection.Close();
            return isUpdate;
        }

        public DataTable Search(Customer customer)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string commandString = @"SELECT c.Id,Code,c.Name,d.Name AS District,Address,Contact FROM Customers AS c INNER JOIN Districts AS d ON d.ID = c.DistrictId WHERE Code = '" + customer.Code+"'" +
                "OR Address = '" + customer.Address + "' OR Contact = '" + customer.Contact + "'";
            SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            int isFill = sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
    }

    public static class StringExtensions
    {
        public static bool IsNumeric(this string input)
        {
            return Regex.IsMatch(input, @"^\d+$");
        }
    }
}
