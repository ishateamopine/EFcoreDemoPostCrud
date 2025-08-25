using EFcoreDemo.Models.Domain;
using MediatR;

namespace EFcoreDemo.CQRS.Employees.Command.Update
{
    public record UpdateEmployeeCommand(int EmpId,string EmpName, string EmpEmail, int EmpSalary) : IRequest<int>;
}
