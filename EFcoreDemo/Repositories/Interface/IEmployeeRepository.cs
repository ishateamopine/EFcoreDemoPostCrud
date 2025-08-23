using EFcoreDemo.Models.Domain;

namespace EFcoreDemo.Repositories.Interface
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<int> InsertEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        // Task<int> DeleteEmployeeAsync(int id);
        Task<int> InsertEmployeeReturnIdAsync(Employee employee);
        Task<int> ModifyEmployeeAsync(int employeeId, string newName,string email, int salary, int deptId);
        Task<int> DeleteEmployeeReturnIdAsync(int employeeId);
    }
}
