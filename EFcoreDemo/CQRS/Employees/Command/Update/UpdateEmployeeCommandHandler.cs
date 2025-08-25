using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Employees.Command.Update
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<int> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
           var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmpId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {request.EmpId} not found.");
            }
            employee.EmpName = request.EmpName;
            employee.EmpEmail = request.EmpEmail;
            employee.EmpSalary = request.EmpSalary;
            await _employeeRepository.UpdateEmployeeAsync(employee);
            return employee.EmpId; 
        }
    }
}
