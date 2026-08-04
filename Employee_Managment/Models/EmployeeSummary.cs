namespace Employee_Managment.Models
{
    public class EmployeeSummary
    {
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public int InactiveEmployees { get; set; }
        public decimal AverageSalary { get; set; }
        public string HighestSalariedEmployee { get; set; }

        public decimal HighestSalary { get; set; } = 0;
        public List<DepartmentSummary> Departments { get; set; }
    }
}
