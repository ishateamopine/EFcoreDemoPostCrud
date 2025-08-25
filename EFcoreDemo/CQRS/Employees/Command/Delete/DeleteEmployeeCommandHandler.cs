using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Employees.Command.Delete
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<int> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmpId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {request.EmpId} not found.");
            }
            await _employeeRepository.DeleteEmployeeAsync(employee.EmpId);
            return employee.EmpId;
        }
    }
}
