using EFcoreDemo.Models.Domain;
using MediatR;

namespace EFcoreDemo.CQRS.Employees.Query.GetAll
{
    public record GetAllEmployeesCommand() : IRequest<IEnumerable<Employee>>;
}
