using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DevUpLabs.Models
{
    public class DataAccessLayer
    {
        #region Variables
        private const string StrConnectionString = @"Server=UAUZHM0IT007LT;Database=DevUpLabs;User=DevUpLabs_User;Password=DevUpLabs_User!@#;";
        #endregion

        /// <summary>
        /// List all employees from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Employees> ListAllEmployees()
        {
            var listEmployees = new List<Employees>();

            using (var connection = new SqlConnection(StrConnectionString))
            {
                var command = new SqlCommand()
                {
                    CommandText = "SP_ListAllEmployees",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var employees = new Employees() { };

                    employees.Id = Convert.ToInt32(reader["EmployeeID"]);
                    employees.FirstName = reader["FirstName"].ToString();
                    employees.LastName = reader["LastName"].ToString();
                    employees.JobTitle = reader["JobTitle"].ToString();
                    employees.EmploymentDate = Convert.ToDateTime(reader["EmploymentDate"], new System.Globalization.CultureInfo("uk-UA"));
                    employees.Rate = Convert.ToInt32(reader["Rate"]);

                    listEmployees.Add(employees);
                }
                connection.Close();
            }
            return listEmployees;
        }

        /// <summary>
        /// Add new employee into the database.
        /// </summary>
        /// <param name="employees"></param>
        public void CreateEmployee(Employees employees)
        {
            using (var connection = new SqlConnection(StrConnectionString))
            {
                var command = new SqlCommand()
                {
                    CommandText = "SP_CreateEmployee",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@FirstName", employees.FirstName);
                command.Parameters.AddWithValue("@LastName", employees.LastName);
                command.Parameters.AddWithValue("@JobTitle", employees.JobTitle);
                command.Parameters.AddWithValue("@Rate", employees.Rate);
                command.Parameters.AddWithValue("@EmploymentDate", DateTime.Now);// employees.EmploymentDate);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// Delete the employee from the database.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteEmployee(int? id)
        {
            using (var connection = new SqlConnection(StrConnectionString))
            {
                var command = new SqlCommand()
                {
                    CommandText = "SP_DeleteEmployee",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@EmployeeID", id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

          
        public Employees GetEmployeeData(int? id)
        {
            var employees = new Employees();

            using (var connection = new SqlConnection(StrConnectionString))
            {
                var command = new SqlCommand()
                {
                    Connection = connection,
                    CommandText = "SP_GetEmployeeData",
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@EmployeeID", id);

                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    employees.Id = Convert.ToInt32(reader["EmployeeID"]);
                    employees.FirstName = reader["FirstName"].ToString();
                    employees.LastName = reader["LastName"].ToString();
                    employees.JobTitle = reader["JobTitle"].ToString();
                    employees.Rate = Convert.ToInt32(reader["Rate"]);
                    employees.EmploymentDate = Convert.ToDateTime(reader["EmploymentDate"], new System.Globalization.CultureInfo("uk-UA"));
                }
            }
            return employees;
        }

    }

}