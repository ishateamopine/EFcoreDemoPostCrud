using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Employees.Query.GetById
{
    public class GetEmployeeByIdCommandHandler : IRequestHandler<GetEmployeeByIdCommand, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeeByIdCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<Employee> Handle(GetEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(request.Id);
       
        }
    }
}
