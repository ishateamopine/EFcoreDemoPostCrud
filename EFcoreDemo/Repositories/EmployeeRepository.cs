using EFcoreDemo.Models.DataContext;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EFcoreDemo.Repositories
{
    public class EmployeeRepository : IEmployeeRepository   
    {
        private readonly DataContext _context;
        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken)
        {
            return await _context.employees
                .Include(e => e.Department)
                .ToListAsync(); // cancelToken hataavi ne check karo
        }
        public async Task<int> InsertEmployeeReturnIdAsync(Employee employee, CancellationToken cancellationToken)
        {
            _context.employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);
            return employee.EmpId; // Return the inserted EmployeeId
        }

    }
}
