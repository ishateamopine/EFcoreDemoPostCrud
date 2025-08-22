using System.ComponentModel.DataAnnotations;

namespace EFcoreDemo.Models.Domain
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
        public string DeptName { get; set; } = string.Empty;

        public IList<Employee> employees { get; set; } = new List<Employee>();
    }
}
