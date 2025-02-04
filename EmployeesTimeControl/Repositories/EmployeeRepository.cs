using EmployeesTimeControl.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace EmployeesTimeControl.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DataTable GetEmployees()
        {
            string query = "SELECT * FROM Employees";
            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeesTCCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return table;
        }

        public DataTable GetEmployeesById(int id)
        {
            string query = @"
                SELECT * FROM Employees
                WHERE EmployeeId=@EmployeeId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeesTCCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    var foundRows = myCommand.ExecuteScalar();
                    int i = Convert.ToInt32(foundRows);
                    if (i < 1)
                    {
                        return new DataTable("Id does not exist");
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }

            return table;
        }

        public int AddEmployee(Employee emp)
        {
            string query = @"
                INSERT INTO Employees (FirstName, LastName, Email)
                values(@firstName, @lastName, @email)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeesTCCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {

                    myCommand.Parameters.AddWithValue("@FirstName", emp.firstName);
                    myCommand.Parameters.AddWithValue("@LastName", emp.lastName);
                    myCommand.Parameters.AddWithValue("@Email", emp.email);
                    return myCommand.ExecuteNonQuery();
                }
            }
            

        }

        public int EditEmployee(int id, Employee emp)
        {
            string query = @"
                UPDATE Employees 
                SET FirstName=@firstName, 
                LastName=@lastName, 
                Email=@email
                WHERE EmployeeId = @EmployeeId
            ";

            string sqlDataSource = _configuration.GetConnectionString("EmployeesTCCon");
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {

                    myCommand.Parameters.AddWithValue("@EmployeeId", id);
                    myCommand.Parameters.AddWithValue("@FirstName", emp.firstName);
                    myCommand.Parameters.AddWithValue("@LastName", emp.lastName);
                    myCommand.Parameters.AddWithValue("@Email", emp.email);
                    return myCommand.ExecuteNonQuery();

                }

            }

            
        }

        public int DeleteEmployee(int id)
        {
            string query = @"
                DELETE from Employees 
                WHERE EmployeeId = @employeeId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeesTCCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@employeeId", id);                  
                    myCommand.Parameters.AddWithValue("@employeeId", id);
                    return myCommand.ExecuteNonQuery();
                    myCon.Close();
                }
            }
        }


    }
}
