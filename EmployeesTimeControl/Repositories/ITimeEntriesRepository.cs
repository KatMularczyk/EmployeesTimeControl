using EmployeesTimeControl.Models;
using Npgsql;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesTimeControl.Repositories
{
    public interface ITimeEntriesRepository
    {
        DataTable GetEntries(int id);
        JsonResult AddEntry(int id, TimeEntry ent);
        JsonResult EditEntry(int id, int entryId, TimeEntry ent);
        JsonResult RemoveEntry(int id, int entryId);
    }
}
