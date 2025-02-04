using EmployeesTimeControl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;


namespace EmployeesTimeControl.Repositories
{
    public class TimeEntriesRepository : ITimeEntriesRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString = "EmployeesTCCon";

        public TimeEntriesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DataTable GetEntries(int id)
        {
            string query = @"
                SELECT * FROM TimeEntries
                WHERE EmployeeId=@employeeId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString(_connectionString);
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@employeeId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return table;
        }
        public JsonResult AddEntry(int id, TimeEntry ent)
        {
            string query = @"
                    INSERT INTO TimeEntries(EmployeeId, Date, HoursWorked)
                    VALUES (@employeeId, @date, @hoursWorked)
                ";
            //assist query used for validation
            string assistQuery = @"
                    SELECT * FROM TimeEntries
                    WHERE EmployeeId=@employeeId 
                    AND Date = @date
                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString(_connectionString);
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {

                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(assistQuery, myCon))//validation
                {
                    myCommand.Parameters.AddWithValue("@employeeId", id);
                    myCommand.Parameters.AddWithValue("@date", Convert.ToDateTime(ent.date));
                    myCommand.Parameters.AddWithValue("@hoursWorked", ent.hoursWorked);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    var foundRows = myCommand.ExecuteScalar();
                    int i = Convert.ToInt32(foundRows);
                    if (i > 0)
                    {
                        return new JsonResult("Already registered on that date");
                    }
                }
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    try
                    {
                        myCommand.Parameters.AddWithValue("@employeeId", id);
                        myCommand.Parameters.AddWithValue("@date", Convert.ToDateTime(ent.date));
                        myCommand.Parameters.AddWithValue("@hoursWorked", ent.hoursWorked);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                    catch(Npgsql.PostgresException  ex)
                    {
                        return new JsonResult("Empoyee id does not exist!");
                    }
                }
            }

            return new JsonResult("Added successfully");
        }
        public JsonResult EditEntry(int id, int entryId, TimeEntry ent)
        {
            string query = @"
                UPDATE TimeEntries
                SET EmployeeId=@employeeId,
                Date=@date,
                HoursWorked=@hoursWorked
                WHERE EntryId=@entryId
            ";
            string assistQuery = @"
                    SELECT * FROM TimeEntries
                    WHERE EmployeeId=@employeeId 
                    AND Date = @date
                    AND EntryId != @entryId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeesTCCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(assistQuery, myCon))//validation
                {
                    myCommand.Parameters.AddWithValue("@entryId", entryId);
                    myCommand.Parameters.AddWithValue("@employeeId", id);
                    myCommand.Parameters.AddWithValue("@date", Convert.ToDateTime(ent.date));
                    myCommand.Parameters.AddWithValue("@hoursWorked", ent.hoursWorked);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    var foundRows = myCommand.ExecuteScalar();
                    int i = Convert.ToInt32(foundRows);
                    if (i > 0)
                    {
                        return new JsonResult("Already registered on that date");
                    }
                }
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@entryId", entryId);
                    myCommand.Parameters.AddWithValue("@employeeId", id);
                    myCommand.Parameters.AddWithValue("@date", Convert.ToDateTime(ent.date));
                    myCommand.Parameters.AddWithValue("@hoursWorked", ent.hoursWorked);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    int affectedRows = myCommand.ExecuteNonQuery();
                    if (affectedRows != 1)
                    {
                        return new JsonResult("Employee or entry id not found");
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added successfully");
        }

        
        public JsonResult RemoveEntry(int id, int entryId)
        {
            string query = @"
                DELETE FROM TimeEntries
                WHERE EmployeeId=@employeeId AND EntryId=@entryId
            ";

            string assistQuery = @"
                SELECT * FROM TimeEntries
                WHERE EmployeeId=@employeeId AND EntryId=@entryId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeesTCCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(assistQuery, myCon))
                {
                    myCommand.Parameters.AddWithValue("@employeeId", id);
                    myCommand.Parameters.AddWithValue("@entryId", entryId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    var foundRows = myCommand.ExecuteScalar();
                    int i = Convert.ToInt32(foundRows);
                    if (i < 1)
                    {
                        return new JsonResult("Id does not exist");
                    }
                }
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@employeeId", id);
                    myCommand.Parameters.AddWithValue("@entryId", entryId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted successfully");
        }
    }
}
