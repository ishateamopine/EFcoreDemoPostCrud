using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFcoreDemo.Models.Domain
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public string EmpName { get; set; } = string.Empty;
        public string EmpEmail { get; set; } = string.Empty;
        public int EmpSalary { get; set; }
        [ForeignKey("DeptId")]
        public int DeptId { get; set; }
        public Department? Department { get; set; } // Navigation property for Department
    }
}
