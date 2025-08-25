using EFcoreDemo.Models.Domain;
using MediatR;

namespace EFcoreDemo.CQRS.Departments.Command.Update
{
    public record UpdateDepartmentCommand(int DeptId, string DeptName) : IRequest<int>;
}
