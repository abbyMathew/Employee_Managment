using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.Services.Interfaces;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _employeeService.CreateEmployee(employee);

            if (result != "Created")
            {
                return BadRequest("Wrong Date");//If the result is not "Created", return a 400 Bad Request response with a message
            }

            return Ok(result);
        }

        [HttpGet("SearchEmployee")]
        public async Task<IActionResult> SearchEmployeeParamAsync(string? name, string? department, decimal? minSalary, decimal? maxSalary, bool? isactive)
        {
            var result = await _employeeService.SearchEmployeeParamAsync(name, department, minSalary, maxSalary, isactive);
            if (!result.Any())
                return NotFound("No employee details"); //If no employee details are found, return a 404 Not Found response with a message
            return Ok(result);
        }

        [HttpGet("EmployeeSummary")]// To get the whole employee summary, we can use this endpoint. It will return the total number of employees, active employees, inactive employees, average salary, highest salaried employee, and department-wise summary.
        public async Task<IActionResult> EmployeeSummaryAsync()
        {
            var result = await _employeeService.EmployeeSummaryAsync();
           
            return Ok(result); // Git Learning: Return the result of the employee summary as an HTTP 200 OK response
        }
    }

}
