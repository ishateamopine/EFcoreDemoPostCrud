using MediatR;

namespace EFcoreDemo.CQRS.Employees.Command.Delete
{
    public record DeleteEmployeeCommand(int EmpId) : IRequest<int>;
}
