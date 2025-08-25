using EFcoreDemo.Repositories.Interface;
using MediatR;
using System.Net.Http.Headers;

namespace EFcoreDemo.CQRS.Departments.Command.Create
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, int>
    {
        private readonly IDepartmentRepository _departmentRepository;
        public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            return await _departmentRepository.InsertDepartmentAsync(request.department);
        }
    }
}
