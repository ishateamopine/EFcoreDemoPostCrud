using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Employees.Query.GetAll
{
    public class GetAllEmployeesCommandHandler : IRequestHandler<GetAllEmployeesCommand, IEnumerable<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetAllEmployeesCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeesCommand request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetAllEmployeesAsync(cancellationToken);
        }
    }
}
