using EFcoreDemo.Models.Domain;
using MediatR;

namespace EFcoreDemo.CQRS.Departments.Queries.GetById
{
    public record GetDepartmentByIdCommand(int id) : IRequest<Department?>;
}
