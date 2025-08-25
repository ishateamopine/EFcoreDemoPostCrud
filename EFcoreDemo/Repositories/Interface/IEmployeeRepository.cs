using EFcoreDemo.Models.Domain;

namespace EFcoreDemo.Repositories.Interface
{
    public interface IEmployeeRepository
    {
        // CRUD operations for Employee entity
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<int> InsertEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task<int> DeleteEmployeeAsync(int id);
        // Custom stored procedure methods
        Task<int> InsertEmployeeReturnIdAsync(Employee employee);
        Task<int> ModifyEmployeeAsync(int employeeId, string newName,string email, int salary, int deptId);
        Task<int> DeleteEmployeeReturnIdAsync(int employeeId);
    }
}
