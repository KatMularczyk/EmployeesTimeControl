using EmployeesTimeControl.Models;
using EmployeesTimeControl.Repositories;
using Mapster;

namespace EmployeesTimeControl.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public List<EmployeeDTO> GetEmployeesDTO()
        {
            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
            var employees = _repository.GetEmployees();
            foreach (var employee in employees)
            {
                employeeDTOs.Add(employee.Adapt<EmployeeDTO>());
            }
            return employeeDTOs;

        }

        public EmployeeDTO GetEmployeeDTOById(int id)
        {
            EmployeeDTO employeeDTO = new EmployeeDTO();
            employeeDTO = _repository.GetEmployeesById(id)[0].Adapt<EmployeeDTO>();
            return employeeDTO;
        }

        public int AddEmployeeDTO(EmployeeDTO employeeDTO)
        {
            Employee employee = new Employee();
            employee = employeeDTO.Adapt<Employee>();
            int affectedRows = _repository.AddEmployee(employee);
            return affectedRows;
        }

        public int EditEmployeeDTO(int id, EmployeeDTO emp)
        {
            Employee employee = new Employee();
            employee = emp.Adapt<Employee>();
            int affectedRows = _repository.EditEmployee(id, employee);
            return affectedRows;
        }

        public int DeleteEmployeeDTO(int id)
        {
            int affectedRows = _repository.DeleteEmployee(id);
            return affectedRows;
        }
    }
}
