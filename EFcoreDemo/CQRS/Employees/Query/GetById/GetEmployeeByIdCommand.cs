using EFcoreDemo.Models.Domain;
using MediatR;

namespace EFcoreDemo.CQRS.Employees.Query.GetById
{
    public record GetEmployeeByIdCommand(int Id) : IRequest<Employee?>;

}
