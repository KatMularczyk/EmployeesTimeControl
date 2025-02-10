using EmployeesTimeControl.Models;

namespace EmployeesTimeControl.Services
{
    public interface IEmployeeService
    {
        List<EmployeeDTO> GetEmployeesDTO();
        EmployeeDTO GetEmployeeDTOById(int id);
        int AddEmployeeDTO(EmployeeDTO employeeDTO);
        int EditEmployeeDTO(int id, EmployeeDTO emp);
        int DeleteEmployeeDTO(int id);
    }
}
