using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Departments.Queries.GetById
{
    public class GetDepartmentByIdCommandHandler : IRequestHandler<GetDepartmentByIdCommand, Department>
    {
        private readonly IDepartmentRepository _departmentRepository;
        public GetDepartmentByIdCommandHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<Department> Handle(GetDepartmentByIdCommand request, CancellationToken cancellationToken)
        {
            return await _departmentRepository.GetDepartmentByIdAsync(request.id);
        }

    }
}
