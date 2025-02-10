using EmployeesTimeControl.Models;
using EmployeesTimeControl.Repositories;
using EmployeesTimeControl.Services;
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

        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service) 
        {
            _service = service;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var employeeDTOs = _service.GetEmployeesDTO();
            return new JsonResult(employeeDTOs);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public JsonResult Get(int id)
        {
            try
            {
                var employeeDTO = _service.GetEmployeeDTOById(id);
                return new JsonResult(employeeDTO);
            }
            catch (KeyNotFoundException ex) 
            {
                return new JsonResult(NotFound(new { message = ex.Message }));
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult Post(EmployeeDTO emp)
        {
            int affectedRows = _service.AddEmployeeDTO(emp);
            return new JsonResult("Added records: "+affectedRows);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public JsonResult Put(int id, EmployeeDTO emp)
        {
            int affectedRows = _service.EditEmployeeDTO(id, emp);
            return new JsonResult("Updated records: " + affectedRows);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public JsonResult Delete(int id)
        {
            int affectedRows = _service.DeleteEmployeeDTO(id);
            return new JsonResult("Deleted records: " + affectedRows);
        }
    }
}
