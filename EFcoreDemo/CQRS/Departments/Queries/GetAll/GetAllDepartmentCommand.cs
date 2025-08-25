using EFcoreDemo.Models.Domain;
using MediatR;

namespace EFcoreDemo.CQRS.Departments.Queries.GetAll
{
    public record GetAllDepartmentCommand() : IRequest<IEnumerable<Department>>;
}
