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
           var employee = new Employee
           {
               EmpName = request.EmpName,
               EmpEmail = request.EmpEmail,
               EmpSalary = request.EmpSalary
           };
             await _employeeRepository.UpdateEmployeeAsync(employee);
            return 1; 
        }
    }
}
