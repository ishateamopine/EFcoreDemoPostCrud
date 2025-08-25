using EFcoreDemo.Repositories.Interface;
using MediatR;
using MediatR.Wrappers;

namespace EFcoreDemo.CQRS.Departments.Command.Update
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, int>
    {
        private readonly IDepartmentRepository _departmentRepository;
        public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<int> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(request.DeptId);
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with ID {request.DeptId} not found.");
            }
            department.DeptName = request.DeptName;
            await _departmentRepository.UpdateDepartmentAsync(department);
            return department.DeptId;
        }
    }
}
