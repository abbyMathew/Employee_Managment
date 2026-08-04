using Employee_Managment.Models;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task CreateEmployeeAsync(Employee employee);

        Task<List<Employee>> SearchEmployeeParamAsync(string?name, string? Department, decimal? minSalary, decimal? maxSalary, bool? isactive);
    
        Task <EmployeeSummary>EmployeeSummaryAsync();
    }
}
