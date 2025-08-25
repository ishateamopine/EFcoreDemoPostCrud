using EFcoreDemo.CQRS.Blogs.Queries.GetAll;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Departments.Queries.GetAll
{
    public class GetAllDepartmentCommandHandler : IRequestHandler<GetAllDepartmentCommand, IEnumerable<Department>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        public GetAllDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<IEnumerable<Department>> Handle(GetAllDepartmentCommand request, CancellationToken cancellationToken)
        {
            return await _departmentRepository.GetAllDepartmentsAsync(cancellationToken);
        }
    }
}
