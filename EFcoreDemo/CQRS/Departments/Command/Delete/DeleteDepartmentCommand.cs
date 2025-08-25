using MediatR;

namespace EFcoreDemo.CQRS.Departments.Command.Delete
{
    public record DeleteDepartmentCommand(int DeptId) : IRequest<int>;
}
