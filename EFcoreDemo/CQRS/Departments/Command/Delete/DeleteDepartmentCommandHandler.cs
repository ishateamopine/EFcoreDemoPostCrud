using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Departments.Command.Delete
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, int>
    {
        private IDepartmentRepository _departmentRepository;
        public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<int> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(request.DeptId);
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with ID {request.DeptId} not found.");
            }
            return await _departmentRepository.DeleteDepartmentAsync(department.DeptId);
        }
    }
}
