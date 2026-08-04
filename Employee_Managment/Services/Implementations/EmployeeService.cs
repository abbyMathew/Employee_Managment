using EmployeeManagement.Services.Interfaces;
using EmployeeManagement.Repository.Interfaces;
using EmployeeManagement.Models;
using Employee_Managment.Models;

namespace EmployeeManagement.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<string> CreateEmployee(Employee employee)
        {
            //if (employee.JoiningDate > DateTime.Now)
            //{
            //    return "Date wrong";
            //}
            await _employeeRepository.CreateEmployeeAsync(employee);
            return "Created";
        }

        public async Task<List<Employee>> SearchEmployeeParamAsync(string? name, string? Department, decimal? minSalary, decimal? maxSalary, bool? isactive)
        {
            return await _employeeRepository.SearchEmployeeParamAsync(name, Department, minSalary, maxSalary, isactive);
        }

        public async Task <EmployeeSummary> EmployeeSummaryAsync()
        {
            return await _employeeRepository.EmployeeSummaryAsync();
        }
    }
}
