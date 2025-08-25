using AutoMapper.Configuration.Annotations;
using Castle.Components.DictionaryAdapter.Xml;
using EFcoreDemo.Models.DataContext;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
            return await _context.employees.Include(e => e.Department).ToListAsync();
        }
        public async Task<int> InsertEmployeeAsync(Employee employee)
        {
            _context.employees.Add(employee);
            return await _context.SaveChangesAsync();
        }
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.employees.Update(employee);
            await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteEmployeeAsync(int id)
        {
            _context.employees.Remove(new Employee { EmpId = id });
            return await _context.SaveChangesAsync();
        }
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.EmpId == id);
        }
        public async Task<int> DeleteEmployeeReturnIdAsync(int employeeId)
        {
            return await _context.Database.ExecuteSqlAsync($"EXEC dbo.Delete_Employee  {employeeId}");
        }
        public async Task<int> ModifyEmployeeAsync(int employeeId, string newName, string email, int salary,int deptId)
        { 
            return await _context.Database.ExecuteSqlAsync($"Exec.dbo.Update_Employee {employeeId},{newName},{email},{salary},{deptId}");
        } 
        public async Task<int> InsertEmployeeReturnIdAsync(Employee employee)
        {
            return await _context.Database.ExecuteSqlAsync($"EXCE dbo.Insert_Employee {employee.EmpName},{employee.EmpEmail},{employee.EmpSalary},{employee.DeptId}");
        }

    }
}
