using EFcoreDemo.Models.Domain;

namespace EFcoreDemo.Repositories.Interface
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken);
        //  Task<Employee?> GetEmployeeByIdAsync(int id);
        //  Task<int> InsertEmployeeAsync(Employee employee);
        // Task<int> UpdateEmployeeAsync(Employee employee);
        // Task<int> DeleteEmployeeAsync(int id);
        Task<int> InsertEmployeeReturnIdAsync(Employee employee, CancellationToken cancellationToken);
    }
}
