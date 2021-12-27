using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Sprout.Exam.DataAccess
{
    public class EmployeeAccess
    { 
        /// <summary>
        /// Gets employee records based on the given parameters
        /// </summary>
        /// <param name="empID">employee ID</param>
        /// <param name="isDeleted">delete status</param>
        /// <returns>returns a datatable containig the records of the specified employee</returns>
        public DataTable GetEmployee(int? empID, bool? isDeleted)
        {
            try
            {
                DataTable dataEmployee = new DataTable();

                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile("AppSettings.json");
                IConfiguration configuration = configurationBuilder.Build();

                using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("GetEmployee", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("empId", empID);
                    sqlCommand.Parameters.AddWithValue("isDeleted", isDeleted);
                    SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                    dataEmployee.Load(sqlReader);
                    sqlConnection.Close();
                    sqlReader.Close();
                }

                return dataEmployee;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets employee types  based on the given parameters
        /// </summary>
        /// <param name="typeID">employee type ID</param>
        /// <returns>returns a datatable containig the records of the specified employee type</returns>
        public DataTable GetEmployeeType(int? typeID)
        {
            try
            {
                DataTable dataEmployeeType = new DataTable();

                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile("AppSettings.json");
                IConfiguration configuration = configurationBuilder.Build();

                using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("GetEmployeeType", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("id", typeID);
                    SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                    dataEmployeeType.Load(sqlReader);
                    sqlConnection.Close();
                    sqlReader.Close();
                }

                return dataEmployeeType;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add, update, or delete an employee record
        /// </summary>
        /// <param name="empID">employee ID</param>
        /// <param name="fullName">employee full name</param>
        /// <param name="birthDate">employee birthdate</param>
        /// <param name="tin">employee tin</param>
        /// <param name="typeId">employee type id</param>
        /// <param name="isDeleted">delete status</param>
        /// <returns>returns a datatable containing the data of the added or edited employee</returns>
        public DataTable AddUpdateDeleteEmployee(int? empID, string fullName, string birthDate, string tin, int typeId, int? isDeleted)
        {
            try
            {
                DataTable dataEmployee = new DataTable();

                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile("AppSettings.json");
                IConfiguration configuration = configurationBuilder.Build();

                using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("AddUpdateDeleteEmployee", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("id", empID);
                    sqlCommand.Parameters.AddWithValue("fullName", fullName);
                    sqlCommand.Parameters.AddWithValue("birthDate", birthDate);
                    sqlCommand.Parameters.AddWithValue("tin", tin);
                    sqlCommand.Parameters.AddWithValue("employeeTypeId", typeId);
                    sqlCommand.Parameters.AddWithValue("isDeleted", isDeleted);
                    SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                    dataEmployee.Load(sqlReader);
                    sqlConnection.Close();
                    sqlReader.Close();
                }

                return dataEmployee;
            }
            catch
            {
                throw;
            }
        }
    }
}
