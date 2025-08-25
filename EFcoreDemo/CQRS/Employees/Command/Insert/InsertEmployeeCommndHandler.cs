using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Employees.Command.Insert
{
    public class InsertEmployeeCommndHandler : IRequestHandler<InsertEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public InsertEmployeeCommndHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<int> Handle(InsertEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.InsertEmployeeAsync(request.employee);
        }
    }
}
