using Employee_Managment.Data;
using Employee_Managment.Models;
using EmployeeManagement.Models;
using EmployeeManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
namespace EmployeeManagement.Repository.Implementations

{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;
        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        private readonly List<Employee> _employees = new List<Employee>();

        public async Task CreateEmployeeAsync(Employee employee)
        {   
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> SearchEmployeeParamAsync(string? name, string? Department, decimal? minSalary, decimal? maxSalary, bool? isactive)
        {
            var result=_context.Employees.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                result = result.Where(e => e.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(Department))
            {
                result = result.Where(e => e.Department == Department);
            }
            if (minSalary.HasValue)
            {
                result = result.Where(e => e.Salary >= minSalary.Value);
            }

            if (maxSalary.HasValue)
            {
                result = result.Where(e => e.Salary <= maxSalary.Value);
            }
            if (isactive.HasValue)
            {
                result = result.Where(e => e.IsActive == isactive.Value);
            }
            return await result.ToListAsync();
        }

        public async Task <EmployeeSummary> EmployeeSummaryAsync()
        {
            var EmployeeSummary = new EmployeeSummary();

            EmployeeSummary.TotalEmployees =await _context.Employees.CountAsync();
            EmployeeSummary.ActiveEmployees = await _context.Employees.CountAsync(e => e.IsActive);
            EmployeeSummary.InactiveEmployees = await _context.Employees.CountAsync(e => !e.IsActive);
            EmployeeSummary.AverageSalary = await _context.Employees.AverageAsync(e => e.Salary);
            EmployeeSummary.HighestSalariedEmployee= await _context.Employees.OrderByDescending(e => e.Salary).Select(e => e.Name).FirstOrDefaultAsync();
            EmployeeSummary.HighestSalary= await _context.Employees.MaxAsync(e => e.Salary);

            EmployeeSummary.Departments=await _context.Employees.GroupBy(e=>e.Department).Select(g => new DepartmentSummary
            {
                Department = g.Key,
                dCount = g.Count(),
            })
                .ToListAsync();

            return EmployeeSummary;
        }
    }
}
