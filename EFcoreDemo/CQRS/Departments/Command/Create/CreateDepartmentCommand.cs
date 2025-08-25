using EFcoreDemo.Models.Domain;
using MediatR;

namespace EFcoreDemo.CQRS.Departments.Command.Create
{
    public record CreateDepartmentCommand(Department department) : IRequest<int>;

}
