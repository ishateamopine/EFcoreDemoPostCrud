using EFcoreDemo.Models.Domain;

namespace EFcoreDemo.Repositories.Interface
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department?> GetDepartmentByIdAsync(int id);
        Task<int> InsertDepartmentAsync(Department department);
        Task<int> UpdateDepartmentAsync(Department department);
        Task<int> DeleteDepartmentAsync(int id);
    }
}
