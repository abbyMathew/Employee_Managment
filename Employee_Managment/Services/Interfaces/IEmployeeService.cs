using Employee_Managment.Models;
using EmployeeManagement.Models;
namespace EmployeeManagement.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<string> CreateEmployee(Employee employee);

        Task<List<Employee>>SearchEmployeeParamAsync(string? name, string? Department, decimal? minSalary, decimal? maxSalary, bool? isactive);

        Task <EmployeeSummary> EmployeeSummaryAsync();
    }
}
