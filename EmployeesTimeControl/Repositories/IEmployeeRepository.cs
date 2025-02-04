using EmployeesTimeControl.Models;
using System.Data;

namespace EmployeesTimeControl.Repositories
{
    public interface IEmployeeRepository
    {
        DataTable GetEmployees();

        DataTable GetEmployeesById(int id);

        int AddEmployee(Employee emp);

        int EditEmployee(int id, Employee emp);

        int DeleteEmployee(int id);


    }
}
