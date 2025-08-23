using EFcoreDemo.Models.Domain;
using MediatR;

namespace EFcoreDemo.CQRS.Employees.Command.Insert
{
    public record InsertEmployeeCommand (Employee employee) : IRequest<int>;

}