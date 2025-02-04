using EmployeesTimeControl.Models;
using EmployeesTimeControl.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Npgsql;

namespace EmployeesTimeControl.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class TimeEntriesController : ControllerBase
    {
        private readonly ITimeEntriesRepository _repository;

        public TimeEntriesController(ITimeEntriesRepository repository)
        {
            _repository = repository;
        }
        //TimeEntries APIs
        [HttpGet("{id:int}/time-entries")]
        public JsonResult GetEntries(int id)
        {
            DataTable entries = _repository.GetEntries(id);
            return new JsonResult(entries);
        }


        [Authorize]
        [HttpPost("{id:int}/time-entries")]

        public JsonResult PostEntry(int id, TimeEntry ent)
        {
            JsonResult response = _repository.AddEntry(id, ent);
            return response;

        }

        [Authorize]
        [HttpPut("{id:int}/time-entries/{entryId:int}")]
        public JsonResult PutEntry(int id, int entryId, TimeEntry ent)
        {
            JsonResult response = _repository.EditEntry(id, entryId, ent);
            return response;
        }

        [Authorize]
        [HttpDelete("{id:int}/time-entries/{entryId:int}")]
        public JsonResult DeleteEntry(int id, int entryId)
        {
            JsonResult response = _repository.RemoveEntry(id, entryId);
            return response;
        }
    }
}
