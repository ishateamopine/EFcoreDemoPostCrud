using EFcoreDemo.Models.DataContext;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EFcoreDemo.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;
        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(CancellationToken cancellationToken)
        {
            return await _context.departments.Include(d => d.employees).ToListAsync(cancellationToken);
        }
        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return await _context.departments.FirstOrDefaultAsync(d => d.DeptId == id);
        }
        public async Task<int> InsertDepartmentAsync(Department department)
        {
            _context.departments.Add(department);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateDepartmentAsync(Department department)
        {
            _context.departments.Update(department);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteDepartmentAsync(int id)
        {
            _context.departments.Remove(new Department { DeptId = id });
            return await _context.SaveChangesAsync();
        }
    }
}
