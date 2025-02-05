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
            try
            {
                var employee = _repository.GetEmployeesById(id);
                return new JsonResult(employee);
            }
            catch (KeyNotFoundException ex) 
            {
                return new JsonResult(NotFound(new { message = ex.Message }));
            }
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
    }
}
