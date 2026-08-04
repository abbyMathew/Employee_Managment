using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Name cannot be empty")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Format")]
        public string Email { get; set; }
        public string Department { get; set; }

        [Range(1000, 1000000)]
        public decimal Salary { get; set; }
        
        public DateTime JoiningDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
