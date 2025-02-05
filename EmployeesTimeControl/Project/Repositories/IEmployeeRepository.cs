using EmployeesTimeControl.Models;
using System.Data;

namespace EmployeesTimeControl.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> convertToList(DataTable table);
        List<Employee> GetEmployees();
        List<Employee> GetEmployeesById(int id);
        int AddEmployee(Employee emp);
        int EditEmployee(int id, Employee emp);
        int DeleteEmployee(int id);

    }
}
