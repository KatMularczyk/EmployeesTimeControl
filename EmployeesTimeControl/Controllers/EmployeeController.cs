using EmployeesTimeControl.Models;
using EmployeesTimeControl.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;




namespace EmployeesTimeControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        /*public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }*/
        private readonly IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var employees = _repository.GetEmployees();
            return new JsonResult(employees);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public JsonResult Get(int id)//Get all employees details
        {
            var employee = _repository.GetEmployeesById(id);
            return new JsonResult(employee);
        }

        [Authorize]
        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            int affectedRows = _repository.AddEmployee(emp);
            return new JsonResult("Added records: "+affectedRows);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public JsonResult Put(int id, Employee emp)
        {
            int affectedRows = _repository.EditEmployee(id, emp);
            return new JsonResult("Updated records: " + affectedRows);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public JsonResult Delete(int id)
        {
            int affectedRows = _repository.DeleteEmployee(id);
            return new JsonResult("Deleted records: " + affectedRows);
        }


        //TimeEntries APIs

        
        /*[Authorize]
        [HttpDelete("{id:int}/time-entries/{entryId:int}")]
        public JsonResult DeleteEntry(int id, int entryId)
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
        }*/
    }
}
